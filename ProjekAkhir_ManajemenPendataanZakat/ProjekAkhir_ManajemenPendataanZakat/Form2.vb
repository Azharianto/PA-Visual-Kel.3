Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class Form2
    Dim kode As String
    Private Sub txtuser_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then txtpass.Focus()
    End Sub

    Private Sub txtpass_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then btnlogin.Focus()
    End Sub
    Sub kodeauto()
        Call koneksi()
        CMD = New MySqlCommand("Select * from tbakun order by idAkun desc", CONN)
        RD = CMD.ExecuteReader
        RD.Read()
        If RD.HasRows Then
            kode = Val(Microsoft.VisualBasic.Mid(RD.Item("idAkun").ToString, 4, 3)) + 1
            If Len(kode) = 1 Then
                kode = "akn" & "00" & kode
            ElseIf Len(kode) = 2 Then
                kode = "akn" & "0" & kode
            ElseIf Len(kode) = 3 Then
                kode = "akn" & kode
            End If
        Else
            kode = "001"
        End If
        RD.Close()
    End Sub
    Sub Clear()
        txtREmail.Clear()
        txtpass.Clear()
        txtUName.Clear()
        txtRPassword.Clear()
        txtEmail.Clear()
        txtNama.Clear()
        txtREmail.Focus()

    End Sub

    Private Sub btnlogin_Click(sender As Object, e As EventArgs) Handles btnlogin.Click
        Try
            If txtEmail.Text = "" Or txtpass.Text = "" Then
                MessageBox.Show("Isi username dan password terlebih dahulu!!", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Call koneksi()
                CMD = New MySqlCommand("select * from tbakun where username = '" & txtEmail.Text & "' and password = '" & txtpass.Text & "'", CONN)
                RD = CMD.ExecuteReader
                RD.Read()
                If RD.HasRows Then
                    If RD("status").ToString = "ADMIN" Then
                        Me.Hide()
                        Form1.Show()
                        Me.Visible = False
                        Form1.Show()
                        Form1.ToolStripStatusLabel1.Text = RD.GetString(0)
                        Form1.ToolStripStatusLabel2.Text = RD.GetString(2)
                        Form1.ToolStripStatusLabel3.Text = RD.GetString(3)
                        RD.Close()
                    Else
                        Me.Hide()
                        form6.Show()
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call koneksi()
    End Sub


    Private Sub btnPLogin_Click(sender As Object, e As EventArgs) Handles btnPLogin.Click
        Panel_login.BringToFront()

        btnPLogin.BackColor = Color.FromArgb(0, 128, 55)
        btnSignUp.BackColor = Color.FromArgb(243, 233, 159)
        pbarLogin.BackColor = Color.FromArgb(243, 233, 159)


        'btnSignUp.BackColor = Color.Black

    End Sub

    Private Sub btnSignUp_Click(sender As Object, e As EventArgs) Handles btnSignUp.Click
        PanelRegistrasi.BringToFront()

        btnSignUp.BackColor = Color.FromArgb(0, 128, 55)
        btnPLogin.BackColor = Color.FromArgb(243, 233, 159)
        pRegistrasi.BackColor = Color.FromArgb(243, 233, 159)

        'btnPLogin.BackColor = Color.Black
    End Sub

    Private Sub btnRegistrasi_Click(sender As Object, e As EventArgs) Handles btnRegistrasi.Click
        If txtREmail.Text = "" Or txtRPassword.Text = "" Or txtREmail.Text = "" Or txtNama.Text = "" Then
            MsgBox("Data Belum Lengkap")
            txtpass.Focus()
            Exit Sub
        Else
            kodeauto()
            STR = "insert into tbakun values (@idAkun, @username, @password, @email, @nama, @status)"
            CMD = CONN.CreateCommand
            With CMD
                .CommandText = STR
                .Connection = CONN
                .Parameters.Add("idAkun", MySqlDbType.String).Value = kode
                .Parameters.Add("username", MySqlDbType.String).Value = txtUName.Text
                .Parameters.Add("password", MySqlDbType.String).Value = txtRPassword.Text
                .Parameters.Add("email", MySqlDbType.String).Value = txtREmail.Text
                .Parameters.Add("nama", MySqlDbType.String).Value = txtNama.Text
                .Parameters.Add("status", MySqlDbType.String).Value = "USER"
                .ExecuteNonQuery()
            End With
            MessageBox.Show("Data Berhasil disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Call Clear()
            PanelRegistrasi.BringToFront()
            txtREmail.Focus()

        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Dim Tutup As String
        Tutup = MessageBox.Show("Yakin tutup form ini ?", "Konfirmasi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Tutup = MsgBoxResult.Yes Then
            End
        Else
            Exit Sub
        End If

    End Sub


End Class