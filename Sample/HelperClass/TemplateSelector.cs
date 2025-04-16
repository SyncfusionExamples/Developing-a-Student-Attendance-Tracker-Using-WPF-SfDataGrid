using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Syncfusion.UI.Xaml.Grid.Cells;

namespace StudentAttendenceTrackerDemo
{
    public class CustomCellTemplateSelector : DataTemplateSelector
    {       
        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            if (item == null)
                return null;
            var dataContext = item as DataContextHelper;
            if (dataContext.Record != null)
            {
                var data = dataContext.Record as MonthlyRecordsModel;

                if (data.Day == "Saturday" || data.Day == "Sunday")
                    return Application.Current.MainWindow.FindResource("AlternateTemplate") as DataTemplate;

                else
                    return Application.Current.MainWindow.FindResource("DefaultTemplate") as DataTemplate;
            }
            return Application.Current.MainWindow.FindResource("DefaultTemplate") as DataTemplate;
        }
    }
}
