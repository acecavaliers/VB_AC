Imports System.ComponentModel

Public Class BookAss
    Public bookAssId As Integer
    Dim Ctr As Integer
    Private Sub gridFormat()

        Dim totalWidth As Integer = DataGridView1.Width
        Dim c0 As Double = 0.2 ' 10%
        Dim c1 As Double = 0.6 ' 30%
        Dim c2 As Double = 0.2 ' 10%
        'Dim c3 As Double = 0.35 ' 10%

        Dim c0w As Integer = CInt(totalWidth * c0)
        Dim c1w As Integer = CInt(totalWidth * c1)
        Dim c2w As Integer = CInt(totalWidth * c2)
        'Dim c3w As Integer = CInt(totalWidth * c3)

        DataGridView1.Columns(0).Width = c0w
        DataGridView1.Columns(1).Width = c1w
        DataGridView1.Columns(2).Width = c2w
        'DataGridView1.Columns(3).Width = c3w

        DataGridView1.Columns(0).HeaderText = "BookAccessionID"
        DataGridView1.Columns(1).HeaderText = "AccessionCode"
        DataGridView1.Columns(2).HeaderText = "BookID"
        'DataGridView1.Columns(3).HeaderText = "Publisher"

        DataGridView1.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    End Sub
    Private Sub ViewBookAss()
        DataGridView1.DataSource = ClsAss.SearchBookAccession(bookAssId)
        gridFormat()
    End Sub
    Private Sub BookAss_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToParent()
        Ctr = ClsAss.CountBookID(bookAssId)
        If Ctr > 0 Then
            ViewBookAss()
            tbx_qty.Text = Ctr
        Else
            tbx_qty.Text = Ctr
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Dispose()
    End Sub

    Private Sub BookAss_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Me.Dispose()
    End Sub

    Private Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        Dim assCode As String
        Dim qty As Integer
        Dim AssNum As String


        If ClsAss.CountBookID(bookAssId) > Integer.Parse(tbx_qty.Text) Then
            qty = Integer.Parse(tbx_qty.Text)
            Dim recadded As Integer = qty - Ctr

            For i As Integer = qty + 1 To Ctr
                AssNum = i.ToString("0000")
                assCode = tbx_bkID.Text + "-" + AssNum
                ClsAss.DeleteBookAccession(assCode)
            Next i
            Ctr = ClsAss.CountBookID(bookAssId)
            tbx_qty.Text = Ctr
            ClsBooks.UpdateQty(bookAssId, Ctr)
            ViewBookAss()
            MessageBox.Show(recadded & " Shit Deleted")


        Else

            qty = Integer.Parse(tbx_qty.Text)
            Dim recadded As Integer = qty - Ctr

            For i As Integer = Ctr + 1 To qty
                AssNum = i.ToString("0000")
                assCode = tbx_bkID.Text + "-" + AssNum
                ClsAss.SaveBookAccession(assCode, tbx_bkID.Text)
            Next i
            Ctr = ClsAss.CountBookID(bookAssId)
            tbx_qty.Text = Ctr
            ClsBooks.UpdateQty(bookAssId, Ctr)
            ViewBookAss()
            MessageBox.Show(recadded & " Shit Added")

        End If
    End Sub

    Private Sub tbx_qty_TextChanged(sender As Object, e As EventArgs) Handles tbx_qty.TextChanged
        If Integer.Parse(tbx_qty.Text) = Ctr Then
            btn_save.Enabled = False
        Else
            btn_save.Enabled = True
        End If
    End Sub

    Private Sub tbx_qty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbx_qty.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back Then

            e.Handled = True
        End If
    End Sub
End Class