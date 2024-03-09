Public Class Students
    Dim sFullData As String
    Dim editMode As Boolean = False
    Dim PrevData As String
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Dispose()
    End Sub
    Public Sub ClearTextBox()
        tbx_stdntID.Text = Nothing
        tbx_sFname.Text = Nothing
        tbx_sLname.Text = Nothing
        tbx_sAddrs.Text = Nothing
        cbx_sYrLvl.Text = Nothing
        sFullData = Nothing
        btn_save.Text = "Save"
        editMode = False
    End Sub
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
    Private Sub ViewStudent()
        DataGridView1.DataSource = ClsStudent.ViewStudent
        gridFormat()
    End Sub
    Private Sub viewDetails()
        If DataGridView1.Rows.Count = 0 Then Exit Sub
        tbx_stdntID.Text = DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value
        tbx_sFname.Text = DataGridView1.Item(1, DataGridView1.CurrentRow.Index).Value
        tbx_sLname.Text = DataGridView1.Item(2, DataGridView1.CurrentRow.Index).Value
        cbx_sYrLvl.Text = DataGridView1.Item(3, DataGridView1.CurrentRow.Index).Value
        tbx_sAddrs.Text = DataGridView1.Item(4, DataGridView1.CurrentRow.Index).Value

        sFullData = DataGridView1.Item(1, DataGridView1.CurrentRow.Index).Value + DataGridView1.Item(2, DataGridView1.CurrentRow.Index).Value
        PrevData = Replace(sFullData & cbx_sYrLvl.Text & tbx_sAddrs.Text, " ", "")

        'Button2.Location = New Point(9, 202)
        'btn_save.Location = New Point(125, 202)
        'btn_clr.Location = New Point(241, 202)
        btn_save.Text = "Update"
    End Sub
    Private Sub Students_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ViewStudent()
    End Sub

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        viewDetails()
    End Sub
    Private Sub SearchStudent()
        DataGridView1.DataSource = ClsStudent.SearchStudent(tbx_search.Text)
        gridFormat()
    End Sub
    Private Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        If btn_save.Text = "Save" Then
            If ClsStudent.CountFullName(tbx_sFname.Text & tbx_sLname.Text) > 0 Then
                Main.MsgsType = "ERROR"
                Main.lbl_error.Text = "This shit already exists!"
                Main.MesgBox()
            Else
                ClsStudent.SaveStudent(tbx_sFname.Text, tbx_sLname.Text, cbx_sYrLvl.Text, tbx_sAddrs.Text)
                Main.MsgsType = "OK"
                Main.lbl_error.Text = "You have added some shits!"
                Main.MesgBox()
                ViewStudent()
                btn_clr.PerformClick()
            End If
        ElseIf btn_save.Text = "Update" Then
            Dim searchValue As String = tbx_stdntID.Text
            Dim NewData As String
            Dim res As Boolean = False
            Dim nStudentName As String
            nStudentName = Replace(tbx_sFname.Text & tbx_sLname.Text, " ", "")
            NewData = nStudentName & cbx_sYrLvl.Text & tbx_sAddrs.Text
            NewData = Replace(NewData, " ", "")

            If PrevData <> NewData Then
                If sFullData <> nStudentName Then
                    If ClsStudent.CountFullName(tbx_sFname.Text & tbx_sLname.Text) > 0 Then
                        Main.MsgsType = "ERROR"
                        Main.lbl_error.Text = "This shit already exists!"
                        Main.MesgBox()
                        res = False
                    Else
                        ClsStudent.UpdateStudent(tbx_stdntID.Text, tbx_sFname.Text, tbx_sLname.Text, cbx_sYrLvl.Text, tbx_sAddrs.Text)
                        Main.MsgsType = "OK"
                        Main.lbl_error.Text = "You have Updated some shits!"
                        Main.MesgBox()
                        ViewStudent()
                        btn_clr.PerformClick()
                        res = True
                    End If
                Else
                    ClsStudent.UpdateStudent(tbx_stdntID.Text, tbx_sFname.Text, tbx_sLname.Text, cbx_sYrLvl.Text, tbx_sAddrs.Text)
                    Main.MsgsType = "OK"
                    Main.lbl_error.Text = "You have Updated some shits!"
                    Main.MesgBox()
                    ViewStudent()
                    btn_clr.PerformClick()
                    res = True
                End If
            Else
                Main.MsgsType = "ERROR"
                Main.lbl_error.Text = "This shit updated nothing!"
                Main.MesgBox()
            End If
            '//RELOAD DATAGRID SELECT LAST EDITED
            If res = True Then
                Dim rowIndex As Integer = -1
                If DataGridView1.Rows.Count > 0 Then
                    For Each row As DataGridViewRow In DataGridView1.Rows
                        If row.Cells(0).Value IsNot Nothing AndAlso row.Cells(0).Value.ToString() = searchValue Then
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

    Private Sub btn_clr_Click(sender As Object, e As EventArgs) Handles btn_clr.Click
        ClearTextBox()
    End Sub
    Private Sub tbx_sFname_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub tbx_stdntID_Changed_1(sender As Object, e As EventArgs) Handles tbx_stdntID.TextChanged
        If tbx_stdntID.Text <> "" Then
            editMode = True
        Else
            editMode = False
        End If
    End Sub

    Private Sub tbx_search_TextChanged(sender As Object, e As EventArgs) Handles tbx_search.TextChanged
        SearchStudent()
    End Sub
End Class