Option Explicit On
Option Infer On

Imports System.Data
Imports System.Text
Imports System.Windows.Controls

Class MainWindow
    Dim _dataTable As DataTable
    Private newPropertyValue As DataTable
    Private listNames As List(Of Person)

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.      
    End Sub

    Public Property TableProperty() As DataTable
        Get
            Return _dataTable
        End Get
        Set(ByVal value As DataTable)
            value = _dataTable
        End Set
    End Property

    Private Sub Window_Loaded_1(sender As Object, e As RoutedEventArgs)

        'Test methods
        SetColorCell()
        'PaintRow()
        'GetAllRows()
        'SelectRowsBasedOnIndexes()

    End Sub

    ''' <summary>
    ''' Set ItemSource with DataTable
    ''' </summary>
    Private Sub BindUsingDataTable()
        _dataTable = New DataTable("table")
        _dataTable.Columns.Add("ID")
        _dataTable.Columns.Add("Name")
        _dataTable.Columns.Add("Department")
        _dataTable.Rows.Add("1", "John Bono", "Dept1")
        _dataTable.Rows.Add("2", "Mike Puntera", "Dept2")
        _dataTable.Rows.Add("3", "Troy Brown", "Dept3")
        _dataTable.Rows.Add("4", "Luisa Montecarlo", "Dept1")
        grid1.ItemsSource = Me.TableProperty.DefaultView
    End Sub

    ''' <summary>
    ''' Set ItemSource with List
    ''' </summary>
    Private Sub BindUsingList()
        listNames = New List(Of Person)
        listNames.Add(New Person() With {.ID = 1, .Name = "John Bono", .Department = "Dept1"})
        listNames.Add(New Person() With {.ID = 2, .Name = "Mike Puntera", .Department = "Dept2"})
        listNames.Add(New Person() With {.ID = 3, .Name = "Troy Brown", .Department = "Dept3"})
        listNames.Add(New Person() With {.ID = 4, .Name = "Luisa Montecarlo", .Department = "Dept1"})
        grid1.ItemsSource = listNames

    End Sub

    ''' <summary>
    ''' Paint Cell Method
    ''' </summary>
    Private Sub SetColorCell()
        BindUsingDataTable()

        'set cell backbround working code
        For index = 0 To grid1.Items.Count - 1 Step 1
            Dim firstRow As DataGridRow = grid1.GetRow(index)
            Dim firstColumnInFirstRow As Controls.DataGridCell = TryCast(grid1.Columns(0).GetCellContent(firstRow).Parent, Controls.DataGridCell)
            'set background
            firstColumnInFirstRow.Background = Brushes.Green
        Next

        'replace color cell based on value
        'For index = 0 To grid1.Items.Count - 1 Step 1
        '    Dim firstRow As DataGridRow = grid1.GetRow(index)
        '    Dim cell As Controls.DataGridCell = TryCast(grid1.Columns(2).GetCellContent(firstRow).Parent, Controls.DataGridCell)
        '    'set background

        '    If TryCast(cell.Content, TextBlock).Text = "Dept1" Then
        '        cell.Background = Brushes.Green
        '    End If
        'Next

    End Sub

    ''' <summary>
    ''' Paint Row Method
    ''' </summary>
    Private Sub PaintRow()
        BindUsingDataTable()

        For index = 0 To grid1.Items.Count - 1 Step 1
            Dim firstRow As DataGridRow = grid1.GetRow(index)
            Dim item As DataRowView = TryCast(firstRow.Item, DataRowView)

            If item(2).ToString().Equals("Dept1") Then
                'set background
                firstRow.Background = Brushes.Red
            End If
        Next

    End Sub

    ''' <summary>
    ''' Return All DataGrid Rows
    ''' </summary>
    Private Sub GetAllRows()
        BindUsingList()
        Dim names As New StringBuilder
        Dim name As String = String.Empty
        Dim item As New Person

        Dim allRows = grid1.GetDataGridRows()
        names.AppendLine("Names in Grid")
        names.AppendLine("-------------")

        'loop through rows
        For Each row As DataGridRow In allRows
            Dim cell As DataGridCell = grid1.GetCell(row, 1)
            names.AppendLine(TryCast(cell.Content, TextBlock).Text)
        Next

        'get content from item of type Person
        item = TryCast(allRows(0).Item, Person)
        name = item.Name 'assign name

        'display names
        MessageBox.Show(names.ToString())

    End Sub

    ''' <summary>
    ''' Highlight or Select Rows based on indexes
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectRowsBasedOnIndexes()
        BindUsingList()
        grid1.SelectDataGridRowByIndexes(0, 1, 3)
    End Sub

    ''' <summary>
    ''' Get Cell Values from selected row
    ''' </summary>
    Private Sub grid1_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles grid1.SelectionChanged
        Dim selectedRow = grid1.GetSelectedRow()
        Dim columnCell = grid1.GetCell(selectedRow, 1)
        Dim result As String

        result = (String.Format("Name: {0} at Row {1}", TryCast(columnCell.Content, TextBlock).Text, selectedRow.GetIndex() + 1))
        
    End Sub

End Class




