Public Class SearchStudent

    Private Sub gridFormat()

        Dim totalWidth As Integer = DataGridView1.Width
        Dim c0 As Double = 0.1 ' 10%
        Dim c1 As Double = 0.2 ' 20%
        Dim c2 As Double = 0.2 ' 20%
        Dim c3 As Double = 0.2 ' 20%
        Dim c4 As Double = 0.3 ' 30%

        Dim c0w As Integer = CInt(totalWidth * c0)
        Dim c1w As Integer = CInt(totalWidth * c1)
        Dim c2w As Integer = CInt(totalWidth * c2)
        Dim c3w As Integer = CInt(totalWidth * c3)
        Dim c4w As Integer = CInt(totalWidth * c4)

        DataGridView1.Columns(0).Width = c0w
        DataGridView1.Columns(1).Width = c1w
        DataGridView1.Columns(2).Width = c2w
        DataGridView1.Columns(3).Width = c3w
        DataGridView1.Columns(4).Width = c4w

        DataGridView1.Columns(0).HeaderText = "Sudent ID"
        DataGridView1.Columns(1).HeaderText = "First Name"
        DataGridView1.Columns(2).HeaderText = "Last Name"
        DataGridView1.Columns(3).HeaderText = "Year Level"
        DataGridView1.Columns(4).HeaderText = "Address"

        DataGridView1.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub
    Private Sub SearchStudent()
        DataGridView1.DataSource = ClsStudent.SearchStudent(tbx_search.Text)
        gridFormat()
    End Sub
    Private Sub tbx_search_TextChanged(sender As Object, e As EventArgs) Handles tbx_search.TextChanged
        SearchStudent()
    End Sub

    Private Sub SearchStudent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToParent()
    End Sub
End Class