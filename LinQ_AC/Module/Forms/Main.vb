Public Class Main
    Dim btnfile As Boolean = True
    Dim btntrans As Boolean = True
    Dim btn_type As String
    Private prevButton As Button = Nothing
    Public MsgsType As String
    Public Sub MesgBox()
        If MsgsType = "OK" Then
            PanelMessage.BackColor = Color.LimeGreen
            lbl_Icon.Image = My.Resources.ok_20
            PanelMessage.Visible = True
            Timer2.Interval = 3500
            Timer2.Start()
        ElseIf MsgsType = "ERROR" Then
            PanelMessage.BackColor = Color.Red
            lbl_Icon.Image = My.Resources.important_20
            PanelMessage.Visible = True
            Timer2.Interval = 3500
            Timer2.Start()
        End If
    End Sub
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        PanelMessage.Visible = False
        Timer2.Stop()
    End Sub

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        PanelFile.Height = 0
        PanelTrans.Height = 0
        btn_File.Image = My.Resources.arrowBack
        btn_Trans.Image = My.Resources.arrowBack
        Me.CenterToScreen()
        Image.MdiParent = Me
        Image.Dock = DockStyle.Fill
        Image.Show()
        Image.BringToFront()
    End Sub

    Private Sub PublisherToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub BooksToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub btn_pub_Click_1(sender As Object, e As EventArgs) Handles Button5.Click, btn_borrow.Click, btn_stdnt.Click, btn_pub.Click, btn_book.Click, btn_borrow.Click

        DirectCast(sender, Button).BackColor = Color.Goldenrod
        If prevButton IsNot Nothing AndAlso prevButton IsNot sender Then
            prevButton.BackColor = Color.WhiteSmoke
        End If
        prevButton = DirectCast(sender, Button)

        If sender Is btn_pub Then
            Pub.MdiParent = Me
            Pub.Dock = DockStyle.Fill
            Pub.Show()
            Pub.BringToFront()
        ElseIf sender Is btn_book Then
            Books.MdiParent = Me
            Books.Dock = DockStyle.Fill
            Books.Show()
            Books.BringToFront()
        ElseIf sender Is btn_stdnt Then
            Students.MdiParent = Me
            Students.Dock = DockStyle.Fill
            Students.Show()
            Students.BringToFront()
        ElseIf sender Is btn_borrow Then
            BorrowHdr.MdiParent = Me
            BorrowHdr.Dock = DockStyle.Fill
            BorrowHdr.Show()
            BorrowHdr.BringToFront()
        ElseIf sender Is btn_borrow Then
            PanelMessage.Visible = False
        ElseIf sender Is Button5 Then
            PanelMessage.Visible = True
        End If
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If btn_type = "File" Then
            If btnfile = True Then
                If PanelFile.Height = PanelFile.MaximumSize.Height Then
                    Timer1.Stop()
                    btnfile = False
                Else
                    btn_File.Image = My.Resources.arrowDown
                    PanelFile.Height += 10
                End If
            ElseIf btnfile = False Then
                If PanelFile.Height = PanelFile.MinimumSize.Height Then
                    Timer1.Stop()
                    btnfile = True
                Else
                    btn_File.Image = My.Resources.arrowBack
                    PanelFile.Height -= 10
                End If
            End If

        ElseIf btn_type = "Trans" Then

            If btntrans = True Then
                If PanelTrans.Height = PanelTrans.MaximumSize.Height Then
                    Timer1.Stop()
                    btntrans = False
                Else
                    btn_Trans.Image = My.Resources.arrowDown
                    PanelTrans.Height += 10
                End If
            ElseIf btntrans = False Then
                If PanelTrans.Height = PanelTrans.MinimumSize.Height Then
                    Timer1.Stop()
                    btntrans = True
                Else
                    btn_Trans.Image = My.Resources.arrowBack
                    PanelTrans.Height -= 10
                End If
            End If

        End If
    End Sub
    Private Sub Button1_Click_2(sender As Object, e As EventArgs) Handles btn_Trans.Click, btn_File.Click

        If sender Is btn_File Then
            btn_File.FlatAppearance.BorderSize = 0
            btn_type = "File"
            Timer1.Start()
        ElseIf sender Is btn_Trans Then
            btn_Trans.FlatAppearance.BorderSize = 0
            btn_type = "Trans"
            Timer1.Start()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        BorrowDetails.Show()
    End Sub
End Class