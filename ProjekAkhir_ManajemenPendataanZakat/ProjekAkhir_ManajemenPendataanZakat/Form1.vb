Public Class Form1
    Private Sub MenuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MenuToolStripMenuItem.Click

    End Sub

    Private Sub PendataanZakatToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PendataanZakatToolStripMenuItem.Click
        Form3.Show()
    End Sub

    Private Sub ManajemenZakatToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ManajemenZakatToolStripMenuItem.Click
        Form4.Show()
    End Sub
    Private Sub JenisPembayaranToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JenisPembayaranToolStripMenuItem.Click
        Form5.Show()
    End Sub

    Private Sub KeluarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KeluarToolStripMenuItem.Click

    End Sub

    Private Sub ManajemenQakatToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ManajemenQakatToolStripMenuItem.Click
        ManajemenQurban.Show()
    End Sub

    Private Sub ManajemenemInfaqToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ManajemenemInfaqToolStripMenuItem.Click
        ManajemenInfaq.Show()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Dim Tutup As String
        Tutup = MessageBox.Show("Yakin untuk menutup form ini?", "Konfirmasi",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Tutup = MsgBoxResult.Yes Then
            End
        Else
            Exit Sub
        End If
    End Sub

    Private Sub LogoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogoutToolStripMenuItem.Click
        Dim Tutup As String
        Tutup = MessageBox.Show("Yakin Logout ini ?", "Konfirmasi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Tutup = MsgBoxResult.Yes Then
            Form2.Show()
            Me.Close()
        Else
            Exit Sub
        End If
    End Sub
End Class
