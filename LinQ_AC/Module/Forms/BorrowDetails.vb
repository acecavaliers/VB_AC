Imports System.ComponentModel

Public Class BorrowDetails
    Private Sub BorrowDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToParent()
        DateTimePicker2.Value = DateTimePicker2.Value.AddDays(3)
    End Sub


    Private Sub find_ass()
        Dim dt As DataTable()
        If ClsAss.CountAss(tbx_search.Text) > 0 Then


            'dt.DataSource = ClsAss.SearchMyAss(tbx_search.Text)

            DataGridView2.DataSource = ClsAss.SearchMyAss(tbx_search.Text)

            Dim Ass = ClsAss.SearchMyAss(tbx_search.Text)



            tbx_search.Text = Nothing
        Else
            MessageBox.Show("No Ass Found Please try again.")

        End If
    End Sub
    Private Sub tbx_search_KeyDown(sender As Object, e As KeyEventArgs) Handles tbx_search.KeyDown
        If e.KeyCode = Keys.Enter Then
            find_ass()
        End If
    End Sub
    Private Sub tbx_search_TextChanged(sender As Object, e As EventArgs) Handles tbx_search.TextChanged

    End Sub

    Private Sub tbx_search_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbx_search.KeyPress
        'If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back Then
        '    e.Handled = True
        'End If
    End Sub

    Private Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        SearchStudent.ShowDialog()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Dispose()
    End Sub

    Private Sub BorrowDetails_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Me.Dispose()
    End Sub
End Class