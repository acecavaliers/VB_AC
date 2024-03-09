Public Class Pub
    Private Sub gridFormat()

        Dim totalWidth As Integer = DataGridView1.Width
        Dim c0 As Double = 0.15 ' 12%
        Dim c1 As Double = 0.85 ' 88%

        Dim c0w As Integer = CInt(totalWidth * c0)
        Dim c1w As Integer = CInt(totalWidth * c1)

        DataGridView1.Columns(0).Width = c0w
        DataGridView1.Columns(1).Width = c1w

        DataGridView1.Columns(0).HeaderText = "PUBLISHER ID"
        DataGridView1.Columns(1).HeaderText = "PUBLISHER NAME"

        DataGridView1.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    End Sub
    Private Sub VIEWME()
        DataGridView1.DataSource = ClsPublisher.ViewPublisher
        gridFormat()
    End Sub
    Private Sub viewDetails()
        If DataGridView1.Rows.Count = 0 Then Exit Sub
        tbx_ID.Text = DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value
        tbx_Name.Text = DataGridView1.Item(1, DataGridView1.CurrentRow.Index).Value

        btn_save.Location = New Point(161, 111)
        btn_delete.Location = New Point(310, 111)
        btn_save.Text = "Update"
        btn_delete.Enabled = True
        btn_new.Visible = True
    End Sub
    Private Sub Pub_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        VIEWME()
        btn_save.Location = New Point(12, 111)
        btn_delete.Location = New Point(161, 111)
    End Sub

    Private Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        If btn_save.Text = "Save" Then
            If ClsPublisher.CountPublisher(tbx_Name.Text) > 0 Then
                Main.MsgsType = "ERROR"
                Main.lbl_error.Text = "This shit already exists!"
                Main.MesgBox()
            Else
                ClsPublisher.SavePublisher(tbx_Name.Text)
                Main.MsgsType = "OK"
                Main.lbl_error.Text = "You have added some shits!"
                Main.MesgBox()
                CLR_TEXT()
                VIEWME()
            End If
        ElseIf btn_save.Text = "Update" Then
            Dim searchValue As String = tbx_Name.Text
            Dim res As Boolean = False
            If ClsPublisher.CountPublisher(tbx_Name.Text) > 0 Then
                Main.MsgsType = "ERROR"
                Main.lbl_error.Text = "This shit already exists!"
                Main.MesgBox()
                res = False
            Else
                ClsPublisher.UpdatePublisher(tbx_Name.Text, tbx_ID.Text)
                Main.MsgsType = "OK"
                Main.lbl_error.Text = "You have Updated some shits!"
                Main.MesgBox()
                CLR_TEXT()
                VIEWME()
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

    Private Sub btn_new_Click(sender As Object, e As EventArgs) Handles btn_new.Click
        tbx_Name.Text = Nothing
        tbx_ID.Text = Nothing
        btn_save.Location = New Point(12, 111)
        btn_delete.Location = New Point(161, 111)
        btn_save.Text = "Save"
        btn_new.Visible = False
    End Sub
    Private Sub SearchPublisher()
        DataGridView1.DataSource = ClsPublisher.SearchPublisher(tbx_search.Text)
        gridFormat()
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles tbx_search.TextChanged
        SearchPublisher()
    End Sub

    Private Sub btn_delete_Click(sender As Object, e As EventArgs) Handles btn_delete.Click
        ClsPublisher.DeletePublisher(tbx_ID.Text)
        Main.MsgsType = "OK"
        Main.lbl_error.Text = "You have Deleted some shits!"
        Main.MesgBox()
        CLR_TEXT()
        VIEWME()
    End Sub

    Private Sub tbx_ID_TextChanged(sender As Object, e As EventArgs) Handles tbx_ID.TextChanged
        If tbx_ID.Text <> "" Then
            btn_delete.Visible = True
        Else
            btn_delete.Visible = False
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Dispose()
    End Sub
    Private Sub CLR_TEXT()
        tbx_ID.Text = Nothing
        tbx_Name.Text = Nothing
    End Sub

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        viewDetails()
    End Sub
End Class