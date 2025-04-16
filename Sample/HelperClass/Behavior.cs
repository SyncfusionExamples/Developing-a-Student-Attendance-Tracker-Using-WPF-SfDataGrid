using Microsoft.Win32;
using Microsoft.Xaml.Behaviors;
using Syncfusion.Data;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Converter;
using Syncfusion.UI.Xaml.Grid.Helpers;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace StudentAttendenceTrackerDemo
{
    public class AttendenceBehavior: Behavior<MainWindow>
    {
        SfDataGrid studentdataGrid;
        StudentsViewModel viewModel;
        SfDataGrid monthlyDataGrid;
        ComboBox studentnamescomboBox;

        #region OnAttached
        protected override void OnAttached()
        {
           // Student Overview Grid
            studentdataGrid = AssociatedObject.dataGrid;           

            if (studentdataGrid.DataContext != null)
            {
                viewModel = AssociatedObject.DataContext as StudentsViewModel;
            }

            //Table summary cell renderer customization
            studentdataGrid.CellRenderers.Remove("TableSummary");
            studentdataGrid.CellRenderers.Add("TableSummary", new GridTableSummaryCellRendererExt());

            //DatePicker and ToolBar settings
            AssociatedObject.datePicker.ValueChanged += DatePicker_ValueChanged;
            AssociatedObject.ascButton.Click += AscButton_Click;
            AssociatedObject.desButton.Click += DesButton_Click;
            AssociatedObject.clearSortButton.Click += ClearSortButton_Click;
            AssociatedObject.enableFilterButton.Click += EnableFilterButton_Click;
            AssociatedObject.disableFilterButton.Click += DisableFilterButton_Click;

            //Monthly GridView Customization
            monthlyDataGrid = AssociatedObject.monthlyDataGrid;
            studentnamescomboBox = AssociatedObject.namesComboBox;

           
            var selectedValue = DateTime.Now.ToString("MMM", CultureInfo.InvariantCulture);
            AssociatedObject.monthComboBox.SelectedValue = selectedValue;

            //ComboBox and Button events
            studentnamescomboBox.SelectionChanged += ComboBox_SelectionChanged;
            AssociatedObject.monthComboBox.SelectionChanged += MonthComboBox_SelectionChanged;
            AssociatedObject.exportButton.Click += ExportButton_Click;
            

            base.OnAttached();
        }
        #endregion

        #region Student Overview Grid events

        private void DatePicker_ValueChanged(System.Windows.DependencyObject d, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null && Convert.ToDateTime(e.NewValue).Date > DateTime.Now)
                viewModel.Students = viewModel.GenerateStudents(true);
            else
                viewModel.Students = viewModel.GenerateStudents(false);
            studentdataGrid.ItemsSource = viewModel.Students;
        }
        private void DisableFilterButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            studentdataGrid.AllowFiltering = false;            
        }

        private void EnableFilterButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            studentdataGrid.AllowFiltering = true;
        }

        private void ClearSortButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            studentdataGrid.SortColumnDescriptions.Clear();
        }

        private void DesButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var sortdescription = new SortColumnDescription()
            {
                ColumnName = "Name",
                SortDirection = System.ComponentModel.ListSortDirection.Descending
            };
            if (studentdataGrid.SortColumnDescriptions == null) return;

            if (studentdataGrid.SortColumnDescriptions.Count == 0)
                studentdataGrid.SortColumnDescriptions.Add(sortdescription);
            else if (studentdataGrid.SortColumnDescriptions.FirstOrDefault().ColumnName == sortdescription.ColumnName)
            {
                studentdataGrid.SortColumnDescriptions.Clear();
                studentdataGrid.SortColumnDescriptions.Add(sortdescription);
            }
                
        }

        private void AscButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var sortdescription = new SortColumnDescription()
            {
                ColumnName = "Name",
                SortDirection = System.ComponentModel.ListSortDirection.Ascending
            };
            if (studentdataGrid.SortColumnDescriptions == null) return;

            if (studentdataGrid.SortColumnDescriptions.Count == 0)
                studentdataGrid.SortColumnDescriptions.Add(sortdescription);
            else if (studentdataGrid.SortColumnDescriptions.FirstOrDefault().ColumnName == sortdescription.ColumnName)
            {
                studentdataGrid.SortColumnDescriptions.Clear();
                studentdataGrid.SortColumnDescriptions.Add(sortdescription);
            }
        }

        #endregion

        #region Monthy Grid View events
        private void ExportButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var options = new ExcelExportingOptions();
            options.ExcelVersion = ExcelVersion.Excel2013;
            var excelEngine = monthlyDataGrid.ExportToExcel(monthlyDataGrid.View, options);
            var workBook = excelEngine.Excel.Workbooks[0];

            SaveFileDialog sfd = new SaveFileDialog
            {
                FilterIndex = 2,
                Filter = "Excel 97 to 2003 Files(*.xls)|*.xls|Excel 2007 to 2010 Files(*.xlsx)|*.xlsx|Excel 2013 File(*.xlsx)|*.xlsx"
            };

            if (sfd.ShowDialog() == true)
            {
                using (Stream stream = sfd.OpenFile())
                {

                    if (sfd.FilterIndex == 1)
                        workBook.Version = ExcelVersion.Excel97to2003;

                    else if (sfd.FilterIndex == 2)
                        workBook.Version = ExcelVersion.Excel2010;

                    else
                        workBook.Version = ExcelVersion.Excel2013;
                    workBook.SaveAs(stream);
                }

                //Message box confirmation to view the created workbook.

                if (MessageBox.Show("Do you want to view the workbook?", "Workbook has been created",
                                    MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {

                    //Launching the Excel file using the default Application.[MS Excel Or Free ExcelViewer]
                    System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo(sfd.FileName);
                    info.UseShellExecute = true;
                    System.Diagnostics.Process.Start(info);
                }
            }
        }

        private void MonthComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var dateTime = GetMonthNumberFromAbbreviation(e.AddedItems[0].ToString());
            viewModel.MonthlyRecords = viewModel.GenerateMonthlyRecords(dateTime);
            monthlyDataGrid.ItemsSource = viewModel.MonthlyRecords;
        }

        public DateTime GetMonthNumberFromAbbreviation(string monthAbbreviation)
        {
            if (DateTime.TryParseExact(monthAbbreviation, "MMM", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                return parsedDate;
            }

            throw new ArgumentException($"Invalid month abbreviation: {monthAbbreviation}");
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {           
            viewModel.MonthlyRecords = viewModel.GenerateMonthlyRecords(DateTime.Now);
            monthlyDataGrid.ItemsSource = viewModel.MonthlyRecords;
        }

        #endregion        

        #region OnDetaching
        protected override void OnDetaching()
        {
            AssociatedObject.datePicker.ValueChanged -= DatePicker_ValueChanged;          
            AssociatedObject.ascButton.Click -= AscButton_Click;
            AssociatedObject.desButton.Click -= DesButton_Click;
            AssociatedObject.clearSortButton.Click -= ClearSortButton_Click;
            AssociatedObject.enableFilterButton.Click -= EnableFilterButton_Click;
            AssociatedObject.disableFilterButton.Click -= DisableFilterButton_Click;


            studentnamescomboBox.SelectionChanged -= ComboBox_SelectionChanged;
            AssociatedObject.monthComboBox.SelectionChanged -= MonthComboBox_SelectionChanged;
            AssociatedObject.exportButton.Click -= ExportButton_Click;

            base.OnDetaching();
        }

        #endregion
    }
}
