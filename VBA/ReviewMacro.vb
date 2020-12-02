Sub ApplyColor(searchTerm As String, customField As Integer, customColor As Long)
    Dim lastRow As Long
    Dim logs As Range
    lastRow = Range("A1").End(xlDown).Row
    Set logs = Sheet2.Range("A1:D" & lastRow)
    logs.AutoFilter field:=customField, Criteria1:=searchTerm
    logs.Cells.Interior.color = customColor
    Sheet2.ShowAllData
End Sub

Sub ApplyFormat()
    
    Dim searchTerm As String
    searchTerm = "*" & Sheet1.Range("B2").Value & "*"
       
    'Restore format
    Sheet2.Range("A:D").ClearFormats
    Sheet2.Range("A:C").HorizontalAlignment = xlCenter
    Sheet2.Range("B:B").NumberFormat = "dd/mm/yyyy hh:mm AM/PM"
    
    'Apply the conditional formating
    Call ApplyColor("Fatal", 3, RGB(255, 0, 0))
    Call ApplyColor("Error", 3, RGB(150, 0, 0))
    Call ApplyColor("Warning", 3, RGB(255, 153, 0))
    Call ApplyColor(searchTerm, 4, RGB(0, 102, 255))
    
    'Clear the format on the headers
    Sheet2.Range("A1:D1").ClearFormats
       
End Sub

Sub SplitQueueBySheet()

    Const columnNumber As Integer = 4
    Dim rng As Range
    Dim lastRow As Long
    Dim searchTerm As String
    Dim ticketID As String
    Dim rowIndex As Integer
    Dim customSize As Integer
    Dim i As Integer
    Dim f As Integer
    Dim x As Integer
    Dim ticketList() As String
    Dim indexList() As Integer
    Dim pasteRange As String
    
    searchTerm = "*" & Sheet1.Range("B2").Value & "*"
        
    lastRow = Range("A1").End(xlDown).Row
    Set rng = Sheet2.Range("A1:D" & lastRow)
    rng.AutoFilter field:=4, Criteria1:=searchTerm
    
    customSize = (rng.SpecialCells(xlCellTypeVisible).Count / columnNumber) - 2
    
    'Reasing the arrays lengths
    ReDim ticketList(customSize)
    ReDim indexList(customSize)
    
    'Fills the ticketList & indexList with the found items
    For Each rw In rng.SpecialCells(xlCellTypeVisible)
        If i Mod columnNumber = 0 And i <> 0 Then

            ticketList(f) = Right(Sheet2.Range("D" & rw.Row).Value, 7)
            indexList(f) = rw.Row
           
            f = f + 1
                        
        End If
        i = i + 1
    Next
    
    'Removes all the filters
    Sheet2.ShowAllData
    
    For Each Item In ticketList
        'Generate the range for all the item less for the last
        If x < UBound(ticketList) Then
            pasteRange = "A" & indexList(x) & ":D" & (indexList(x + 1)) - 1
        End If
        
        'Generate the range from the last item
        If x = UBound(ticketList) Then
            lastRow = Sheet2.Range("A1").End(xlDown).Row
            pasteRange = "A" & indexList(x) & ":D" & lastRow
        End If
        
        'Create each sheet from queue item
        Sheets.Add.Name = Item
        Sheet2.Range("A1:D1").Copy Destination:=Sheets(Item).Range("A1:D1")
        Sheet2.Range(pasteRange).Copy Destination:=Sheets(Item).Range("A2")
        x = x + 1
    Next
    
End Sub

Sub Clean()
    Dim lastRow As Integer
    
    'Confirm Macro Run
    If MsgBox("Please confirm that you want to clean the template", vbYesNo, "Alert", vbNull, vbInformation) = vbNo Then
        Exit Sub
    End If
    
    'Delete generated sheets
    Application.ScreenUpdating = False
    Application.DisplayAlerts = False
    For Each sht In Application.ActiveWorkbook.Worksheets
        If sht.Name <> Sheet1.Name And sht.Name <> Sheet2.Name Then
            sht.Delete
        End If
    Next
    Application.DisplayAlerts = True
    Application.ScreenUpdating = True
    
    lastRow = Sheet2.Range("A1").End(xlDown).Row
    Sheet2.Range("A2:D" & lastRow).Delete
    
    'Restore format
    Sheet2.Range("A:D").ClearFormats
    Sheet2.Range("A:C").HorizontalAlignment = xlCenter
    Sheet2.Range("B:B").NumberFormat = "dd/mm/yyyy hh:mm AM/PM"
End Sub


'Excute all the code
Sub main()
    Call ApplyFormat
    Call SplitQueueBySheet
End Sub