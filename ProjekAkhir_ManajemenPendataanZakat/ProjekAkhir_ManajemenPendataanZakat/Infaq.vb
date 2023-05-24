Imports MySql.Data.MySqlClient

Public Class Infaq
    Dim kode As String

    Private Sub Qurban_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call koneksi()
        Call tampilInfaq()
        Call Kosong()
    End Sub
    Sub Kosong()
        txtID.Clear()
        txtNama.Clear()
        txtAlamat.Clear()
        txtPembayaran.Clear()
        txtPesan.Clear()
        txtNama.Focus()
    End Sub
    Sub Isi()
        txtNama.Clear()
        txtAlamat.Clear()
        txtPembayaran.Clear()
        txtPesan.Clear()
        txtNama.Focus()
    End Sub
    Sub tampilInfaq()
        DA = New MySqlDataAdapter("Select * From tbinfaq", CONN)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "tbinfaq")
        DataGridView1.DataSource = DS.Tables("tbinfaq")
        DataGridView1.Refresh()
    End Sub

    Sub kodeauto()
        Call koneksi()
        CMD = New MySqlCommand("Select * from tbinfaq order by IdInfaq desc", CONN)
        RD = CMD.ExecuteReader
        RD.Read()
        If RD.HasRows Then
            kode = Val(Microsoft.VisualBasic.Mid(RD.Item("IdInfaq").ToString, 4, 3)) + 1
            If Len(kode) = 1 Then
                kode = "IFQ" & "00" & kode
            ElseIf Len(kode) = 2 Then
                kode = "IFQ" & "0" & kode
            ElseIf Len(kode) = 3 Then
                kode = "IFQ" & kode
            End If
        Else
            kode = "001"
        End If
        RD.Close()
    End Sub

    Private Sub btnsimpan_Click(sender As Object, e As EventArgs) Handles btnsimpan.Click
        If txtNama.Text = "" Or txtAlamat.Text = "" Or txtPembayaran.Text = "" Or txtPesan.Text = "" Then
            MsgBox("Data Belum Lengkap")
            txtNama.Focus()
            Exit Sub
        Else
            kodeauto()
            STR = "insert into tbinfaq values (@IdInfaq, @nama, @alamat, @pembayaran, @pesan)"
            CMD = CONN.CreateCommand
            With CMD
                .CommandText = STR
                .Connection = CONN
                .Parameters.Add("IdInfaq", MySqlDbType.String).Value = kode
                .Parameters.Add("nama", MySqlDbType.String).Value = txtNama.Text
                .Parameters.Add("alamat", MySqlDbType.String).Value = txtAlamat.Text
                .Parameters.Add("pembayaran", MySqlDbType.UInt64).Value = txtPembayaran.Text
                .Parameters.Add("pesan", MySqlDbType.String).Value = txtPesan.Text
                .ExecuteNonQuery()
            End With
            MessageBox.Show("Data Berhasil disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Call Kosong()
        End If
    End Sub
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim i As Integer
        i = Me.DataGridView1.CurrentRow.Index
        With DataGridView1.Rows.Item(i)
            Me.txtID.Text = .Cells(0).Value
            Me.txtNama.Text = .Cells(1).Value
            Me.txtAlamat.Text = .Cells(2).Value
            Me.txtPembayaran.Text = .Cells(3).Value
            Me.txtPesan.Text = .Cells(4).Value
        End With
    End Sub
    Private Sub txtkode_KeyPress(sender As Object, e As KeyPressEventArgs)
        txtID.MaxLength = 2
        If e.KeyChar = Chr(13) Then
            CMD = New MySqlCommand("Select * From tbinfaq where IdInfaq='" & txtID.Text & "'", CONN)
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
    Private Sub btnReturn_Click(sender As Object, e As EventArgs) Handles btnReturn.Click
        form6.Show()
        Me.Close()
    End Sub


End Class