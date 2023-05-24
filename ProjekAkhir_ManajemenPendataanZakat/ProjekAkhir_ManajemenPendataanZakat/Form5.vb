Imports MySql.Data.MySqlClient
Public Class Form5
    Sub Kosong()
        txtkode.Clear()
        txtnama.Clear()
        txtjumlah.Clear()
        txtkode.Focus()
    End Sub
    Sub Isi()
        txtkode.Clear()
        txtnama.Clear()
        txtjumlah.Clear()
        txtkode.Focus()
        txtnama.Focus()
        txtjumlah.Focus()
    End Sub
    Sub TampilZakat()
        DA = New MySqlDataAdapter("Select * From tbjenis", CONN)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "tbjenis")
        DataGridView1.DataSource = DS.Tables("tbjenis")
        DataGridView1.Refresh()
    End Sub
    Sub AturGrid()
        DataGridView1.Columns(0).Width = 250
        DataGridView1.Columns(1).Width = 260
        DataGridView1.Columns(2).Width = 260


        DataGridView1.Columns(0).HeaderText = "KODE JENIS"
        DataGridView1.Columns(1).HeaderText = "NAMA JENIS"
        DataGridView1.Columns(2).HeaderText = "JUMLAH"
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call koneksi()
        Call TampilZakat()
        Call Kosong()
        Call AturGrid()
    End Sub
    Private Sub btnadd_Click(sender As Object, e As EventArgs) Handles btnadd.Click
        If txtkode.Text = "" Or txtnama.Text = "" Or txtjumlah.Text = "" Then
            MsgBox("Data Belum Lengkap")
            txtkode.Focus()
            Exit Sub
        Else
            CMD = New MySqlCommand("Select * From tbjenis where kodeJenis='" & txtkode.Text & "'", CONN)
            RD = CMD.ExecuteReader
            RD.Read()
            RD.Close()

            If Not RD.HasRows Then
                Dim Simpan As String = "insert into tbJenis(kodeJenis,namaJenis,jumlah)values ('" & txtkode.Text & "','" & txtnama.Text & "','" & txtjumlah.Text & "')"
                CMD = New MySqlCommand(Simpan, CONN)
                CMD.ExecuteNonQuery()
                MsgBox("Data Telah Tersimpan!!", MsgBoxStyle.Information, "Perhatian")
            End If
            Call TampilZakat()
            Call Kosong()
            txtkode.Focus()
        End If

    End Sub
    Private Sub txtjenis_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtnama.KeyPress
        txtnama.MaxLength = 50
        If e.KeyChar = Chr(13) Then
            txtnama.Text = UCase(txtnama.Text)
        End If
    End Sub
    Private Sub btnclear_Click(sender As Object, e As EventArgs) Handles btnclear.Click
        Kosong()
    End Sub
    Private Sub btnexit_Click(sender As Object, e As EventArgs) Handles btnexit.Click
        Dim Tutup As String
        Tutup = MessageBox.Show("Yakin untuk menutup form ini?", "Konfirmasi",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Tutup = MsgBoxResult.Yes Then
            End
        Else
            Exit Sub
        End If
    End Sub
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim i As Integer
        i = Me.DataGridView1.CurrentRow.Index
        With DataGridView1.Rows.Item(i)
            Me.txtkode.Text = .Cells(0).Value
            Me.txtnama.Text = .Cells(1).Value
            Me.txtjumlah.Text = .Cells(2).Value
        End With
    End Sub
    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        If txtkode.Text = "" Then
            MsgBox("Kode jenis belum diisi!")
            txtkode.Focus()
        Else
            If MessageBox.Show("Yakin akan menghapus Data Jenis " & txtkode.Text &
                               " ?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                CMD = New MySqlCommand("Delete From tbjenis Where kodeJenis = '" & txtkode.Text & "'", CONN)
                CMD.ExecuteNonQuery()
                Call Kosong()
                Call TampilZakat()
            Else
                Call Kosong()
            End If
        End If
    End Sub
    Private Sub btnUbah_Click(sender As Object, e As EventArgs) Handles btnUbah.Click
        If txtkode.Text = "" Then
            MsgBox("Kode Jenis belum diisi")
            txtkode.Focus()
        Else
            Dim Ubah As String = "Update tbbarang set kodeJenis = '" & txtkode.Text & "', namaJenis =
                                 '" & txtnama.Text & "', jumlah = '" & txtjumlah.Text & "'"
            CMD = New MySqlCommand(Ubah, CONN)
            CMD.ExecuteNonQuery()
            MessageBox.Show("Data Sudah Diubah", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Call TampilZakat()
            Call Kosong()
            txtkode.Focus()
        End If
    End Sub
    Private Sub TxtCari_KeyPress_1(sender As Object, e As KeyPressEventArgs) Handles TxtCari.KeyPress
        If e.KeyChar = Chr(13) Then
            CMD = New MySqlCommand("Select * From tbJenis where kodeJenis like '%" & TxtCari.Text & "%'", CONN)
            RD = CMD.ExecuteReader
            RD.Read()

            If RD.HasRows Then

                DA = New MySqlDataAdapter("Select * From tbJenis where kodeJenis like '%" & TxtCari.Text & "%'", CONN)
                DS = New DataSet
                RD.Close()
                DA.Fill(DS, "Dapat")
                DataGridView1.DataSource = DS.Tables("Dapat")
                DataGridView1.ReadOnly = True

            Else
                RD.Close()
                MsgBox("Data Tidak ditemukan!")
            End If
        End If
    End Sub
    Private Sub btnreturn_Click(sender As Object, e As EventArgs) Handles btnreturn.Click
        'Form4.Show()
        Me.Close()
    End Sub

    Private Sub txtkode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtkode.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
            MessageBox.Show("Data Yang Di Inputkan Harus Berupa Angka.")
        End If
    End Sub

    Private Sub txtnama_TextChanged(sender As Object, e As EventArgs) Handles txtnama.TextChanged
        Dim input As String = txtnama.Text.Trim()

        If input.Length > 0 AndAlso Not Char.IsUpper(input(0)) Then
            MessageBox.Show("Teks harus diawali dengan huruf besar.", "Peringatan")
        End If
    End Sub
End Class