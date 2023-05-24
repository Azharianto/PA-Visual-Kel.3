Imports MySql.Data.MySqlClient

Public Class ManajemenQurban
    Private Sub ManajemenQurban_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call koneksi()
        Call Kosong()
        'Call TampilJenis()
        Call TampilManajemen()
        Call AturGrid()
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
    Sub TampilManajemen()
        DA = New MySqlDataAdapter("Select * From tbqurban", CONN)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "qurban")

        DataGridView1.DataSource = DS.Tables("qurban")
        DataGridView1.Refresh()
    End Sub
    Sub AturGrid()
        'untuk mengatur luas kolom
        DataGridView1.Columns(0).Width = 110
        DataGridView1.Columns(1).Width = 120
        DataGridView1.Columns(2).Width = 120
        DataGridView1.Columns(3).Width = 120
        DataGridView1.Columns(4).Width = 180

        'untuk menampilkan judul header
        DataGridView1.Columns(0).HeaderText = "IdQurban"
        DataGridView1.Columns(1).HeaderText = "Nama"
        DataGridView1.Columns(2).HeaderText = "ALamat"
        DataGridView1.Columns(3).HeaderText = "Pembayaran"
        DataGridView1.Columns(4).HeaderText = "Pesan"

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
            If MessageBox.Show("Yakin akan menghapus Data tbqurban" & txtID.Text &
                               " ?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                CMD = New MySqlCommand("Delete From tbqurban Where IdQurban = '" & txtID.Text & "'", CONN)
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
            CMD = New MySqlCommand("Select * From tbqurban where IdQurban like '%" & txtID.Text & "%'", CONN)
            RD = CMD.ExecuteReader
            RD.Read()

            If RD.HasRows Then
                RD.Close()
                DA = New MySqlDataAdapter("Select * From tbqurban where IdQurban like '%" & txtID.Text & "%'", CONN)
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
            Me.txtNama.Text = .Cells(1).Value
            Me.txtAlamat.Text = .Cells(2).Value
            Me.txtPembayaran.Text = .Cells(3).Value
            Me.txtPesan.Text = .Cells(4).Value
        End With
    End Sub
    Private Sub btnUbah_Click(sender As Object, e As EventArgs) Handles btnUbah.Click
        If txtID.Text = "" Then
            MsgBox("ID belum diisi")
            txtID.Focus()
        Else
            Dim Ubah As String = "Update tbqurban set IdQurban = '" & txtID.Text & "', 
                                                        nama ='" & txtNama.Text & "', 
                                                        alamat ='" & txtAlamat.Text & "', 
                                                        pembayaran ='" & txtPembayaran.Text & "', 
                                                        pesan ='" & txtPesan.Text & "'"
            CMD = New MySqlCommand(Ubah, CONN)
            CMD.ExecuteNonQuery()
            MessageBox.Show("Data Sudah Diubah", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Call TampilManajemen()
            Call Kosong()
            txtID.Focus()
        End If
    End Sub
    Private Sub btnReturn_Click(sender As Object, e As EventArgs) Handles btnReturn.Click
        Form1.Show()
        Me.Close()
    End Sub


End Class