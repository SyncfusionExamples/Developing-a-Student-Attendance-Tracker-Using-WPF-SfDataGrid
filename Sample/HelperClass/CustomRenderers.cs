using Syncfusion.Data;
using Syncfusion.UI.Xaml.Grid.Cells;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace StudentAttendenceTrackerDemo
{
    public class GridTableSummaryCellRendererExt : GridTableSummaryCellRenderer
    {
        
        public override void OnUpdateEditBinding(DataColumnBase column, Syncfusion.UI.Xaml.Grid.GridTableSummaryCell element, object dataContext)
        {

            if (column.GridColumn.MappingName == "Id" || column.GridColumn.MappingName == "Name")
            {
                base.OnUpdateEditBinding(column, element, dataContext);
                return;
            }
               

            //Check whether the datacontext is SummaryRecordEntry
            var record = dataContext as SummaryRecordEntry;
            if (!(dataContext is SummaryRecordEntry))
                return;

            //Process each SummaryColumn and get the display text of corresponding summary
            foreach (ISummaryColumn summaryColumn in record.SummaryRow.SummaryColumns)
            {
                if (!summaryColumn.MappingName.Contains(column.GridColumn.MappingName))
                    continue;
                string summarytext = string.Empty;
                if (record.SummaryRow.ShowSummaryInRow)
                    summarytext = SummaryCreator.GetSummaryDisplayTextForRow(record, this.DataGrid.View);
                else
                    summarytext = SummaryCreator.GetSummaryDisplayText(record, column.GridColumn.MappingName, this.DataGrid.View);

                if (!string.IsNullOrEmpty(summarytext))
                {
                    //Number format is applied to summary columns
                    element.Content = summarytext + @"\" + this.DataGrid.View.Records.Count;
                }
            }
        }
    }

    public class CustomAggregate : ISummaryAggregate
    {

        public CustomAggregate()
        {
        }

        public int PresentCount { get; set; }

        public Action<System.Collections.IEnumerable, string, System.ComponentModel.PropertyDescriptor> CalculateAggregateFunc()
        {
            return (items, property, pd) =>
            {
                if (items == null) return;

                if (pd.Name == "PresentCount")
                {
                    int count = 0;

                    foreach (var item in items)
                    {
                        var itemType = item.GetType();
                        var propInfo = itemType.GetProperty(property);

                        if (propInfo != null)
                        {
                            var value = propInfo.GetValue(item);
                            if (value is bool boolValue && boolValue)
                            {
                                count++;
                            }
                        }
                    }

                    this.PresentCount = count;
                }
            };
        }
    }
}
