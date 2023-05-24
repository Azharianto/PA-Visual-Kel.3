Imports MySql.Data.MySqlClient

Public Class Form4
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call koneksi()
        Call Kosong()
        Call TampilJenis()
        Call TampilManajemen()
        Call AturGrid()
    End Sub
    Sub Kosong()
        txtID.Clear()
        txtNama.Clear()
        txtAlamat.Clear()
        txtNo.Clear()
        cmbkodejenis.Text = ""
        txtJumlah.Clear()
        txtID.Focus()
    End Sub
    Sub Isi()
        txtNama.Clear()
        txtAlamat.Clear()
        txtNo.Clear()
        cmbkodejenis.Text = ""
        txtJumlah.Clear()
        cmbkodejenis.Focus()
    End Sub

    Sub TampilManajemen()
        DA = New MySqlDataAdapter("Select * From tbmanajemen", CONN)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "manajemen")

        DataGridView1.DataSource = DS.Tables("manajemen")
        DataGridView1.Refresh()
    End Sub

    Sub TampilJenis()
        CMD = New MySqlCommand("Select kodeJenis from tbJenis", CONN)
        RD = CMD.ExecuteReader
        Do While RD.Read
            cmbkodejenis.Items.Add(RD.Item(0))
        Loop
        RD.Close()
    End Sub

    Sub AturGrid()
        'untuk mengatur luas kolom
        DataGridView1.Columns(0).Width = 120
        DataGridView1.Columns(1).Width = 140
        DataGridView1.Columns(2).Width = 140
        DataGridView1.Columns(3).Width = 140
        DataGridView1.Columns(4).Width = 140
        DataGridView1.Columns(5).Width = 140

        'untuk menampilkan judul header
        DataGridView1.Columns(0).HeaderText = "ID"
        DataGridView1.Columns(1).HeaderText = "Kode Jenis"
        DataGridView1.Columns(2).HeaderText = "Nama"
        DataGridView1.Columns(3).HeaderText = "Alamat"
        DataGridView1.Columns(4).HeaderText = "No Hp"
        DataGridView1.Columns(5).HeaderText = "Jumlah"

    End Sub

    Private Sub txtNama_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNama.KeyPress
        txtNama.MaxLength = 50
        If e.KeyChar = Chr(13) Then
            txtNama.Text = UCase(txtNama.Text)
        End If
    End Sub
    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        Call Kosong()
        Call TampilManajemen()
    End Sub
    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        If txtID.Text = "" Then
            MsgBox("ID belum diisi!")
            txtID.Focus()
        Else
            If MessageBox.Show("Yakin akan menghapus Data tbmanajemen " & txtID.Text &
                               " ?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                CMD = New MySqlCommand("Delete From tbmanajemen Where id = '" & txtID.Text & "'", CONN)
                CMD.ExecuteNonQuery()
                Call Kosong()
                Call TampilManajemen()
            Else
                Call Kosong()
            End If
        End If
    End Sub
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Dim tutup As String
        tutup = MessageBox.Show("Yakin Ingin tutup?", "Byeee byeee!!",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If tutup = MsgBoxResult.Yes Then
            End
        Else
            Exit Sub
        End If
    End Sub
    Private Sub txtCari_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCari.KeyPress, txtNama.KeyPress
        If e.KeyChar = Chr(13) Then
            CMD = New MySqlCommand("Select * From tbmanajemen where id like '%" & txtID.Text & "%'", CONN)
            RD = CMD.ExecuteReader
            RD.Read()

            If RD.HasRows Then
                RD.Close()
                DA = New MySqlDataAdapter("Select * From tbmanajemen where id like '%" & txtID.Text & "%'", CONN)
                DS = New DataSet
                DA.Fill(DS, "Dapat")
                DataGridView1.DataSource = DS.Tables("Dapat")
                DataGridView1.ReadOnly = True
            Else
                RD.Close()
                MsgBox("Data Tidak ditemukan!")
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim i As Integer
        i = Me.DataGridView1.CurrentRow.Index
        With DataGridView1.Rows.Item(i)
            Me.txtID.Text = .Cells(0).Value
            Me.cmbkodejenis.Text = .Cells(1).Value
            Me.txtNama.Text = .Cells(2).Value
            Me.txtAlamat.Text = .Cells(3).Value
            Me.txtNo.Text = .Cells(4).Value
            Me.txtJumlah.Text = .Cells(5).Value
        End With
    End Sub
    Private Sub cmbkodejenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbkodejenis.SelectedIndexChanged
        CMD = New MySqlCommand("Select * From tbjenis where kodeJenis ='" & cmbkodejenis.Text & "'", CONN)
        RD = CMD.ExecuteReader
        RD.Read()

        If RD.HasRows = True Then
            txtJumlah.Text = RD.Item(2)
        Else
            MsgBox("Jenis ini tidak terdaftar")
        End If
        RD.Close()
    End Sub


    Private Sub btnReturn_Click(sender As Object, e As EventArgs) Handles btnReturn.Click
        Form1.Show()
        Me.Close()
    End Sub

    Private Sub txtNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNo.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
            MessageBox.Show("Data Yang Di Inputkan Harus Berupa Angka.")
        End If
    End Sub

    Private Sub txtNama_TextChanged(sender As Object, e As EventArgs) Handles txtNama.TextChanged
        Dim input As String = txtNama.Text.Trim()

        If input.Length > 0 AndAlso Not Char.IsUpper(input(0)) Then
            MessageBox.Show("Teks harus diawali dengan huruf besar.", "Peringatan")
        End If
    End Sub

    Private Sub txtAlamat_TextChanged(sender As Object, e As EventArgs) Handles txtAlamat.TextChanged
        Dim input As String = txtAlamat.Text.Trim()

        If input.Length > 0 AndAlso Not Char.IsUpper(input(0)) Then
            MessageBox.Show("Teks harus diawali dengan huruf besar.", "Peringatan")
        End If
    End Sub

    Private Sub btnUbah_Click_1(sender As Object, e As EventArgs) Handles btnUbah.Click
        If txtID.Text = "" Then
            MsgBox("ID belum diisi")
            txtID.Focus()
        Else
            Dim Ubah As String = "Update tbmanajemen set id = '" & txtID.Text & "', 
                                                        kodejenis ='" & cmbkodejenis.Text & "', 
                                                        nama ='" & txtNama.Text & "', 
                                                        alamat ='" & txtAlamat.Text & "', 
                                                        noHp ='" & txtNo.Text & "', 
                                                        Jumlah ='" & txtJumlah.Text & "'
                                                where id = '" & txtID.Text & "'"
            CMD = New MySqlCommand(Ubah, CONN)
            CMD.ExecuteNonQuery()
            MessageBox.Show("Data Sudah Diubah", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Call TampilManajemen()
            Call Kosong()
            txtID.Focus()
        End If
    End Sub
End Class