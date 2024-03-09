Public Class Books
    Dim ISBN As String
    Dim PrevData As String
    Dim EditMode As Boolean = False
    Private Sub btn_new_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        If btn_save.Text = "Save" Then

            If tbx_bkID.Text = "" AndAlso tbx_bkName.Text <> "" AndAlso tbx_bkISBN.Text <> "" AndAlso cbx_PubID.Text <> "" Then
                If ClsBooks.CountISBN(tbx_bkISBN.Text) > 0 Then
                    Main.MsgsType = "ERROR"
                    Main.lbl_error.Text = "This shit already exists!"
                    Main.MesgBox()
                Else
                    ClsBooks.SaveBook(tbx_bkName.Text, tbx_bkISBN.Text, ClsPublisher.GetPubID(cbx_PubID.Text))
                    Main.MsgsType = "OK"
                    Main.lbl_error.Text = "You have added some shits!"
                    Main.MesgBox()
                    ViewBook()
                    btn_clr.PerformClick()
                End If
            Else
                Main.MsgsType = "ERROR"
                Main.lbl_error.Text = "This shit must be filled!"
                Main.MesgBox()
            End If


        ElseIf btn_save.Text = "Update" Then
                Dim searchValue As String = tbx_bkName.Text
            Dim res As Boolean = False
            If ISBN <> tbx_bkISBN.Text Then
                If ClsBooks.CountISBN(tbx_bkISBN.Text) > 0 Then
                    Main.MsgsType = "ERROR"
                    Main.lbl_error.Text = "This shit already exists!"
                    Main.MesgBox()
                    res = False
                Else
                    ClsBooks.UpdateBook(tbx_bkID.Text, tbx_bkName.Text, tbx_bkISBN.Text, ClsPublisher.GetPubID(cbx_PubID.Text))
                    Main.MsgsType = "OK"
                    Main.lbl_error.Text = "You have Updated some shits!"
                    Main.MesgBox()
                    ViewBook()
                    btn_clr.PerformClick()
                    res = True
                End If
            Else
                ClsBooks.UpdateBook(tbx_bkID.Text, tbx_bkName.Text, tbx_bkISBN.Text, ClsPublisher.GetPubID(cbx_PubID.Text))
                Main.MsgsType = "OK"
                Main.lbl_error.Text = "You have Updated some shits!"
                Main.MesgBox()
                ViewBook()
                btn_clr.PerformClick()
                res = True
            End If
            If res = True Then
                Dim rowIndex As Integer = -1
                If DataGridView1.Rows.Count > 0 Then
                    For Each row As DataGridViewRow In DataGridView1.Rows
                        If row.Cells(1).Value IsNot Nothing AndAlso row.Cells(1).Value.ToString().Contains(searchValue) Then
                            rowIndex = row.Index
                            Exit For
                        End If
                    Next
                End If
                If rowIndex <> -1 Then
                    DataGridView1.Rows(rowIndex).Selected = True
                    DataGridView1.CurrentCell = DataGridView1.Rows(rowIndex).Cells(1)
                End If
            End If
        End If
    End Sub

    Private Sub cbx_PubID_Click(sender As Object, e As EventArgs) Handles cbx_PubID.Click
        cbx_PubID.DataSource = ClsPublisher.SelectPublisher
    End Sub
    Private Sub gridFormat()

        Dim totalWidth As Integer = DataGridView1.Width
        Dim c0 As Double = 0.05 ' 4%
        Dim c1 As Double = 0.1 ' 63%
        Dim c2 As Double = 0.35 ' 10%
        Dim c3 As Double = 0.15 ' 10%
        Dim c4 As Double = 0.25 ' 10%
        Dim c5 As Double = 0.1 ' 10%

        Dim c0w As Integer = CInt(totalWidth * c0)
        Dim c1w As Integer = CInt(totalWidth * c1)
        Dim c2w As Integer = CInt(totalWidth * c2)
        Dim c3w As Integer = CInt(totalWidth * c3)
        Dim c4w As Integer = CInt(totalWidth * c4)
        Dim c5w As Integer = CInt(totalWidth * c5)

        DataGridView1.Columns(0).Width = c0w
        DataGridView1.Columns(1).Width = c1w
        DataGridView1.Columns(2).Width = c2w
        DataGridView1.Columns(3).Width = c3w
        DataGridView1.Columns(4).Width = c4w
        DataGridView1.Columns(5).Width = c5w

        DataGridView1.Columns(0).HeaderText = ""
        DataGridView1.Columns(1).HeaderText = "Book ID"
        DataGridView1.Columns(2).HeaderText = "Book Name"
        DataGridView1.Columns(3).HeaderText = "ISBN"
        DataGridView1.Columns(4).HeaderText = "Publisher"
        DataGridView1.Columns(5).HeaderText = "Qty"

        DataGridView1.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    End Sub
    Private Sub ViewBook()
        DataGridView1.DataSource = ClsBooks.ViewBook
        gridFormat()
    End Sub
    Public Sub ClearTextBox()
        tbx_bkID.Text = Nothing
        tbx_bkISBN.Text = Nothing
        tbx_bkName.Text = Nothing
        cbx_PubID.Text = Nothing
        ISBN = Nothing
        btn_save.Text = "Save"
        EditMode = False

    End Sub

    Private Sub SearchBook()
        DataGridView1.DataSource = ClsBooks.SearchBook(tbx_search.Text)
        gridFormat()
    End Sub
    Private Sub Books_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ViewBook()
    End Sub

    Private Sub btn_clr_Click(sender As Object, e As EventArgs) Handles btn_clr.Click
        ClearTextBox()
    End Sub

    Private Sub tbx_search_TextChanged(sender As Object, e As EventArgs) Handles tbx_search.TextChanged
        SearchBook()
    End Sub
    Private Sub viewDetails()
        If DataGridView1.Rows.Count = 0 Then Exit Sub
        tbx_bkID.Text = DataGridView1.Item(1, DataGridView1.CurrentRow.Index).Value
        tbx_bkName.Text = DataGridView1.Item(2, DataGridView1.CurrentRow.Index).Value
        tbx_bkISBN.Text = DataGridView1.Item(3, DataGridView1.CurrentRow.Index).Value
        ISBN = DataGridView1.Item(3, DataGridView1.CurrentRow.Index).Value
        cbx_PubID.Text = DataGridView1.Item(4, DataGridView1.CurrentRow.Index).Value

        PrevData = tbx_bkID.Text & tbx_bkName.Text & ISBN & cbx_PubID.Text

        btn_save.Text = "Update"
    End Sub



    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        viewDetails()
        'btn_save.Enabled = False
        EditMode = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Dispose()
    End Sub

    Private Sub tbx_bkName_TextChanged(sender As Object, e As EventArgs) Handles tbx_bkName.TextChanged, tbx_bkISBN.TextChanged, cbx_PubID.TextChanged
        Dim NewData As String
        Dim NesISBN As String = tbx_bkISBN.Text
        NewData = tbx_bkID.Text & tbx_bkName.Text & NesISBN & cbx_PubID.Text

        'If EditMode = True AndAlso PrevData <> NewData Then
        '    btn_save.Enabled = True
        'Else
        '    btn_save.Enabled = False
        'End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If TypeOf DataGridView1.Columns(e.ColumnIndex) Is DataGridViewButtonColumn AndAlso e.RowIndex >= 0 Then

            BookAss.bookAssId = DataGridView1.Rows(e.RowIndex).Cells(1).Value
            BookAss.tbx_bkID.Text = tbx_bkID.Text
            BookAss.tbx_bkName.Text = tbx_bkName.Text
            BookAss.ShowDialog()
        End If
    End Sub
End Class