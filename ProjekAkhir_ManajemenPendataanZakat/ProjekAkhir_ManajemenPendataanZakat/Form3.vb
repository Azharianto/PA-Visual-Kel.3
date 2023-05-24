Imports System.Windows
Imports MySql.Data.MySqlClient

Public Class Form3
    Dim kode As String

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call koneksi()
        Call Kosong()
        Call TampilJenis()
    End Sub
    Sub Kosong()
        txtID.Clear()
        txtNama.Clear()
        txtAlamat.Clear()
        txtNo.Clear()
        cmbkodejenis.Text = ""
        txtJumlah.Clear()
        txtNama.Focus()
    End Sub
    Sub Isi()
        txtNama.Clear()
        txtAlamat.Clear()
        txtNo.Clear()
        cmbkodejenis.Text = ""
        txtJumlah.Clear()
        cmbkodejenis.Focus()
    End Sub
    Sub kodeauto()
        Call koneksi()
        CMD = New MySqlCommand("Select * from tbmanajemen order by id desc", CONN)
        RD = CMD.ExecuteReader
        RD.Read()
        If RD.HasRows Then
            kode = Val(Microsoft.VisualBasic.Mid(RD.Item("id").ToString, 4, 3)) + 1
            If Len(kode) = 1 Then
                kode = "mjn" & "00" & kode
            ElseIf Len(kode) = 2 Then
                kode = "mjn" & "0" & kode
            ElseIf Len(kode) = 3 Then
                kode = "mjn" & kode
            End If
        Else
            kode = "001"
        End If
        RD.Close()
    End Sub
    Sub TampilJenis()
        CMD = New MySqlCommand("Select kodeJenis from tbjenis", CONN)
        RD = CMD.ExecuteReader
        Do While RD.Read
            cmbkodejenis.Items.Add(RD.Item(0))
        Loop
        RD.Close()
    End Sub
    Private Sub btnSimpan_Click_1(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If txtNama.Text = "" Or txtAlamat.Text = "" Or txtNo.Text = "" Or cmbkodejenis.Text = "" Then
            MsgBox("Data Belum Lengkap")
            txtNama.Focus()
            Exit Sub
        Else
            kodeauto()
            STR = "insert into tbmanajemen values (@id, @kodejenis, @nama, @alamat, @noHp, @Jumlah)"
            CMD = CONN.CreateCommand
            With CMD
                .CommandText = STR
                .Connection = CONN
                .Parameters.Add("id", MySqlDbType.String).Value = kode
                .Parameters.Add("kodejenis", MySqlDbType.UInt64).Value = cmbkodejenis.Text
                .Parameters.Add("nama", MySqlDbType.String).Value = txtNama.Text
                .Parameters.Add("alamat", MySqlDbType.String).Value = txtAlamat.Text
                .Parameters.Add("noHp", MySqlDbType.UInt64).Value = txtNo.Text
                .Parameters.Add("Jumlah", MySqlDbType.String).Value = txtJumlah.Text
                .ExecuteNonQuery()
            End With
            MessageBox.Show("Data Berhasil disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Call Kosong()
        End If
    End Sub


    Private Sub txtkode_KeyPress(sender As Object, e As KeyPressEventArgs)
        txtID.MaxLength = 2
        If e.KeyChar = Chr(13) Then
            CMD = New MySqlCommand("Select * From tbmanajemen where id='" & txtID.Text & "'", CONN)
            RD = CMD.ExecuteReader
            RD.Read()

            If RD.HasRows = True Then
                txtNama.Text = RD.Item(1)
                txtNama.Focus()
            Else
                Call Isi()
                txtNama.Focus()
            End If
        End If
    End Sub



    Private Sub btnKeluar_Click(sender As Object, e As EventArgs)
        Dim tutup As String
        tutup = MessageBox.Show("Yakin Ingin tutup?", "Byeee byeee!!",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If tutup = MsgBoxResult.Yes Then
            End
        Else
            Exit Sub
        End If
    End Sub
    Private Sub cmbkodejenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbkodejenis.SelectedIndexChanged
        CMD = New MySqlCommand("Select * From tbJenis where kodeJenis ='" & cmbkodejenis.Text & "'", CONN)
        RD = CMD.ExecuteReader
        RD.Read()

        If RD.HasRows = True Then
            txtJumlah.Text = RD.Item(2)
        Else
            MsgBox("Jenis ini tidak terdaftar")
        End If
        RD.Close()
    End Sub

    Private Sub txtNo_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
            MessageBox.Show("Data Yang Di Inputkan Harus Berupa Angka.")
        End If
    End Sub

    Private Sub txtNama_TextChanged(sender As Object, e As EventArgs)
        Dim input As String = txtNama.Text.Trim()

        If input.Length > 0 AndAlso Not Char.IsUpper(input(0)) Then
            MessageBox.Show("Teks harus diawali dengan huruf besar.", "Peringatan")
        End If
    End Sub

    Private Sub txtAlamat_TextChanged(sender As Object, e As EventArgs)
        Dim input As String = txtAlamat.Text.Trim()

        If input.Length > 0 AndAlso Not Char.IsUpper(input(0)) Then
            MessageBox.Show("Teks harus diawali dengan huruf besar.", "Peringatan")
        End If
    End Sub

    Private Sub btnReturn_Click(sender As Object, e As EventArgs) Handles btnReturn.Click
        form6.Show()
        Me.Close()
    End Sub


End Class