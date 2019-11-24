Public Class FrmBayar
    Private Sub hitung()
        Try

            TxtKembali.Text = "0"

            'Fungsi ini merupakan fungsi yang dapat menghitung kolom total harga pembelian berdasarkan nilai
            'penjumlahan yang ada pada kolom grid kelima dari tabel pembelian
            
            TxtKembali.Text = Format(Val(TxtBayar.Text) - Val(Replace(TotalLbl.Text, ".", "")), "#,#")
           
        Catch ex As Exception
            TxtKembali.Text = "0"
        End Try
    End Sub

    Private Sub BtnClose_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub BtnClose_Click_1(sender As Object, e As EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Private Sub FrmBayar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BtnSimpan.Enabled = False
    End Sub

    Private Sub TxtKembali_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub TxtBayar_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtBayar.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
              Me.Close()
        End Select
    End Sub

    Private Sub TxtBayar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtBayar.KeyPress
        If e.KeyChar = Chr(13) Then
            If Val(TxtBayar.Text) < Val(Replace(TotalLbl.Text, ".", "")) Then
                MsgBox("Pembayaran Kurang", vbInformation, "Pesan") : Exit Sub
            Else
                Call hitung()

                Dim a As Long
                a = Val(TxtBayar.Text)
                TxtBayar.Text = Format(a, "#,#")
                BtnSimpan.Enabled = True
                BtnSimpan.Focus()
            End If
        End If
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled() = True
    End Sub

    Private Sub TxtBayar_TextChanged(sender As Object, e As EventArgs) Handles TxtBayar.TextChanged

    End Sub

    Private Sub BtnBatal_Click(sender As Object, e As EventArgs) Handles BtnBatal.Click
        Me.Close()
    End Sub

    Private Sub BtnSimpan_Click(sender As Object, e As EventArgs) Handles BtnSimpan.Click
        Call Koneksi()
        DMLSql.Connection = Database
        DMLSql.CommandType = CommandType.Text

        DMLSql.CommandText = "Insert into TblPenjualan values('" & FrmPenjualan.TxtFaktur.Text & "','" & Format(Now, "yyyy/MM/dd") & "','" & _
            Val(Replace(TotalLbl.Text, ".", "")) & "','" & Val(Replace(TxtBayar.Text, ".", "")) & "','" & _
           Val(Replace(TxtKembali.Text, ".", "")) & "','" & FrmPenjualan.LblKdPel.Text & "','" & FrmMenuUtama.LblKodeUser.Text & "')"
        DMLSql.ExecuteNonQuery()

        For baris As Integer = 0 To FrmPenjualan.DGV.Rows.Count - 2
            'simpan ke tabel detail
            Call Koneksi()
            DMLSql.Connection = Database
            DMLSql.CommandType = CommandType.Text
            DMLSql.CommandText = "Insert into TblPenjualan_Rinci values ('" & FrmPenjualan.TxtFaktur.Text & _
                "','" & FrmPenjualan.DGV.Rows(baris).Cells(0).Value & "','" & FrmPenjualan.DGV.Rows(baris).Cells(2).Value & _
                "','" & FrmPenjualan.DGV.Rows(baris).Cells(3).Value & "','" & FrmPenjualan.DGV.Rows(baris).Cells(5).Value & _
                "','" & FrmPenjualan.DGV.Rows(baris).Cells(6).Value & "')"
            DMLSql.ExecuteNonQuery()

            'kurangi stok barang
            Call Koneksi()
            Tampil.Connection = Database
            Tampil.CommandType = CommandType.Text

            Tampil.CommandText = "select * from TblBarang where Kode_Barang = '" & FrmPenjualan.DGV.Rows(baris).Cells(0).Value & "' "
            Tampilkan = Tampil.ExecuteReader
            Tampilkan.Read()
            If Tampilkan.HasRows = True Then
                'Call Koneksi()
                Tampil2.Connection = Database
                Tampil2.CommandType = CommandType.Text
                Tampil2.CommandText = "Update TblBarang set stock= '" & Val(Tampilkan.Item(8) - FrmPenjualan.DGV.Rows(baris).Cells(5).Value) & "' where kode_barang='" & FrmPenjualan.DGV.Rows(baris).Cells(0).Value & "'"
                Tampil2.ExecuteNonQuery()
            End If
        Next baris

        If MessageBox.Show("Cetak Struk..?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            CrLapStrukJual.CrystalReportViewer1.SelectionFormula = "{TblPenjualan.No_Faktur} ='" & FrmPenjualan.TxtFaktur.Text & "' "
            CrLapStrukJual.CrystalReportViewer1.RefreshReport()
            CrLapStrukJual.CrystalReportViewer1.PrintReport()
            Me.Close()
            FrmPenjualan.Bersih()

        Else
            Me.Close()
            FrmPenjualan.Bersih()
        End If
    End Sub
End Class