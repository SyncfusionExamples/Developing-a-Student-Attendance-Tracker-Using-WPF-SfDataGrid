# Student Attendance Tracker Using WPF SfDataGrid

The development of a WPF application that helps track student attendance using the powerful and flexible Syncfusion SfDataGrid. This solution is ideal for educational institutions or academic projects that need to monitor attendance in an organized, visual, and interactive way.

## Overview

This Student Attendance Tracker is a WPF-based desktop application built with Syncfusion SfDataGrid for managing student attendance across multiple subjects. The application features a dual-tab interface: "All Tab" for viewing all students and "Individual Tab" for tracking specific students monthly.

## Key Features

- **Dual-Tab Interface**: Manage all students or track individual student records by month
- **Interactive DataGrid**: Edit attendance records with CheckBox columns for present/absent marking
- **Sorting & Filtering**: Toolbar buttons for organizing and searching attendance data
- **Date Selection**: Integrated date picker for selecting specific dates
- **Summary Statistics**: Table summary rows display total students present per subject
- **Excel Export**: Export individual student monthly records to Excel
- **Multiple Columns**: GridNumericColumn (ID/Date), GridTextColumn (Names), GridCheckBoxColumn (Attendance), GridTemplateColumn (Custom templates)
- **Subject Tracking**: Monitors attendance for Mathematics, History, Science, and English
- **Modern Theme**: Windows 11 Light theme with SfSkinManager for professional appearance

## Code Snippets

### SfDataGrid Definition with Columns
```xaml
<syncfusion:SfDataGrid x:Name="dataGrid" 
                Grid.Row="2"
                Height="412"
                Width="750"                                
                Margin="10,0,10,0" 
                RowHeight="24"      
                ItemsSource="{Binding Students}"
                AllowEditing="True"
                AddNewRowPosition="FixedTop"
                ShowRowHeader="True"
                AddNewRowText="Add New Student details">
    <syncfusion:SfDataGrid.Columns>
        <syncfusion:GridNumericColumn MappingName="Id" Width="50" AllowEditing="True" NumberDecimalDigits="0"/>
        <syncfusion:GridTextColumn MappingName="Name" Width="150" AllowEditing="False"/>
        <syncfusion:GridCheckBoxColumn MappingName="Mathematics" ColumnSizer="Star" />
        <syncfusion:GridCheckBoxColumn MappingName="History" ColumnSizer="Star" />
        <syncfusion:GridCheckBoxColumn MappingName="Science" ColumnSizer="Star"/>
        <syncfusion:GridCheckBoxColumn MappingName="English" ColumnSizer="Star"/>
    </syncfusion:SfDataGrid.Columns>
```

### Table Summary Rows with Custom Aggregate
```xaml
<syncfusion:SfDataGrid.TableSummaryRows>
    <syncfusion:GridTableSummaryRow ShowSummaryInRow="False" TitleColumnCount="2" Title="No of Students Present in class:" Position="Bottom">
        <syncfusion:GridSummaryRow.SummaryColumns>
            <syncfusion:GridSummaryColumn Name="Mathematics"
                                          Format="'{PresentCount:d}'"
                                          MappingName="Mathematics"
                                          SummaryType="Custom"
                                          CustomAggregate="{StaticResource customAggregate}"/>
            <syncfusion:GridSummaryColumn Name="History"
                                          Format="'{PresentCount:d}'"
                                          MappingName="History"
                                          SummaryType="Custom" 
                                          CustomAggregate="{StaticResource customAggregate}"/>
        </syncfusion:GridSummaryRow.SummaryColumns>
    </syncfusion:GridTableSummaryRow>
</syncfusion:SfDataGrid.TableSummaryRows>
```

### Date Picker and Toolbar Controls
```xaml
<TextBlock Text="Date:" Height="15" Width="36" HorizontalAlignment="Right"/>
<syncfusion:SfDatePicker x:Name="datePicker" Width="150" Height="30" HorizontalAlignment="Right"  />

<syncfusion:ToolBarAdv ToolBarName="Settings" Grid.Row="1" Height="30">
    <Button x:Name="ascButton" ToolTip="Sort Ascending">
        <Image Source="Images\sort-alpha-up.png" Width="20" Height="20"/>
    </Button>
    <Button x:Name="desButton" ToolTip="Sort Decending">
        <Image Source="Images\sort-alpha-up-reversed.png" Width="20" Height="20"/>
    </Button>
    <Button x:Name="clearSortButton" ToolTip="Clear Sort">
        <Image Source="/Images/close.png" Width="20" Height="20"/>
    </Button>
    <Button x:Name="enableFilterButton" ToolTip="Enable Filter">
        <Image Source="/Images/filter.png" Width="20" Height="20"/>
    </Button>
</syncfusion:ToolBarAdv>
```

### Template Column with Cell Selector
```xaml
<syncfusion:GridTemplateColumn MappingName="Mathematics" ColumnSizer="Star" 
                               CellTemplateSelector="{StaticResource cellTemplateSelector}" 
                               SetCellBoundValue="True">
</syncfusion:GridTemplateColumn>

<DataTemplate x:Key="DefaultTemplate">
    <CheckBox IsChecked="{Binding Path=Value}" HorizontalAlignment="Center" />
</DataTemplate>

<DataTemplate x:Key="AlternateTemplate">
    <TextBlock Background="DeepSkyBlue" Foreground="White"
               Text="- - - Holiday - - -" TextAlignment="Center" />
</DataTemplate>
```

### Dual-Tab Interface
```xaml
<syncfusion:TabControlExt Name="tabControlExt">
    <syncfusion:TabItemExt Header="All">
        <!-- All students attendance grid -->
    </syncfusion:TabItemExt>
    <syncfusion:TabItemExt Header="Individual">
        <!-- Individual student monthly grid -->
    </syncfusion:TabItemExt>
</syncfusion:TabControlExt>
```

### Individual Student Monthly View
```xaml
<syncfusion:SfDataGrid x:Name="monthlyDataGrid" 
                       Grid.Row="1"
                       Height="450"
                       Width="750"
                       GridLinesVisibility="Vertical"
                       Margin="10,0,10,0"
                       AutoGenerateColumns="False" 
                       ItemsSource="{Binding MonthlyRecords}"> 
    <syncfusion:SfDataGrid.Columns>
        <syncfusion:GridNumericColumn MappingName="Date" Width="50" NumberDecimalDigits="0"/>
        <syncfusion:GridTextColumn MappingName="Day" Width="150"/>
        <syncfusion:GridTemplateColumn MappingName="Mathematics" ColumnSizer="Star" 
                                       CellTemplateSelector="{StaticResource cellTemplateSelector}" 
                                       SetCellBoundValue="True">
        </syncfusion:GridTemplateColumn>
    </syncfusion:SfDataGrid.Columns>
</syncfusion:SfDataGrid>
```

## Support and Resources

For more information on Syncfusion SfDataGrid, visit [Syncfusion Documentation](https://www.syncfusion.com/wpf-controls).