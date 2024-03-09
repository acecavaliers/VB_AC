Public Class BorrowHdr

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        BorrowDetails.ShowDialog()
    End Sub
    Private Sub gridFormat()

        Dim totalWidth As Integer = DataGridView1.Width
        Dim c0 As Double = 0.2 ' 4%
        Dim c1 As Double = 0.2 ' 63%
        Dim c2 As Double = 0.25 ' 10%
        Dim c3 As Double = 0.1 ' 10%
        Dim c4 As Double = 0.1 ' 10%
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

        DataGridView1.Columns(0).HeaderText = "Student"
        DataGridView1.Columns(1).HeaderText = "Year Lvl"
        DataGridView1.Columns(2).HeaderText = "Book Name"
        DataGridView1.Columns(3).HeaderText = "ISBN"
        DataGridView1.Columns(4).HeaderText = "Date Borrowed"
        DataGridView1.Columns(5).HeaderText = "Date to return"
        DataGridView1.Columns(6).HeaderText = "transID"
        DataGridView1.Columns(7).HeaderText = "StudentID"

        DataGridView1.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView1.Columns(6).Visible = False
        DataGridView1.Columns(7).Visible = False

    End Sub
    Private Sub ViewTrans()
        DataGridView1.DataSource = ClsStudent.transactions()
        gridFormat()
    End Sub

    Private Sub BorrowHdr_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ViewTrans()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick
        BorrowDetails.tbx_transID.Text = DataGridView1.Rows(e.RowIndex).Cells(6).Value
        BorrowDetails.tbx_SID.Text = DataGridView1.Rows(e.RowIndex).Cells(7).Value
        BorrowDetails.tbx_sname.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value

        BorrowDetails.DateTimePicker1.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value
        BorrowDetails.DateTimePicker2.Text = DataGridView1.Rows(e.RowIndex).Cells(5).Value

        BorrowDetails.DataGridView2.DataSource = DataGridView1
        DataGridView1.Columns(0).Visible = False
        DataGridView1.Columns(1).Visible = False
        DataGridView1.Columns(2).Visible = True
        DataGridView1.Columns(3).Visible = False
        DataGridView1.Columns(4).Visible = True
        DataGridView1.Columns(5).Visible = True
        DataGridView1.Columns(6).Visible = False
        DataGridView1.Columns(7).Visible = False
        BorrowDetails.ShowDialog()
    End Sub
End Class