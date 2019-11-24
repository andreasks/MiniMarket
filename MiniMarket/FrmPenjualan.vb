Imports System.Data.OleDb
Public Class FrmPenjualan

    Sub SubTotal()
        Dim Diskon As Long
        Diskon = Val(TxtHarga.Text) * (Val(TxtDiskon.Text) / 100)
        TxtSubTtl.Text = (Val(TxtHarga.Text) - Diskon) * Val(TxtJml.Text)
    End Sub

    'Nomor Faktur Ototmatis
    Sub FakturOtomatis()
        Call Koneksi()
        Tampil.Connection = Database
        Tampil.CommandType = CommandType.Text
        Tampil.CommandText = "Select * from TblPenjualan where No_Faktur in (select max(No_Faktur) from TblPenjualan) order by No_Faktur desc"
        Tampilkan = Tampil.ExecuteReader
        Tampilkan.Read()
        Dim urutan As String
        Dim hitung As Long

        If Not Tampilkan.HasRows Then
            urutan = Format(Now, "yyMMdd") + "0001"
        Else
            If Microsoft.VisualBasic.Left(Tampilkan.GetString(0), 6) <> Format(Now, "yyMMdd") Then
                urutan = Format(Now, "yyMMdd") + "0001"
            Else
                hitung = Tampilkan.GetString(0) + 1
                urutan = Format(Now, "yyMMdd") + Microsoft.VisualBasic.Right("0000" & hitung, 4)
            End If
        End If
        TxtFaktur.Text = urutan
    End Sub

    'Fungsi untuk membuat kolom pada DataGridView
    Sub BuatKolomBaru()
        DGV.Columns.Add("Kode", "Kode Barang")
        DGV.Columns.Add("Nama", "Nama Barang")
        DGV.Columns.Add("Harga", "Harga")
        DGV.Columns.Add("Diskon Rp", "Diskon Rp")
        DGV.Columns.Add("Satuan", "Satuan")
        DGV.Columns.Add("Jumlah", "Jumlah")
        DGV.Columns.Add("Total", "SubTotal")
        Call AturLebarKolom()
    End Sub

    'Mengatur Lebar kolom grid
    Sub AturLebarKolom()
        DGV.Columns(0).Width = 180
        DGV.Columns(1).Width = 300
        DGV.Columns(2).Width = 150
        DGV.Columns(3).Width = 150
        DGV.Columns(4).Width = 150
        DGV.Columns(5).Width = 150
        DGV.Columns(6).Width = 150


        'Atur Aligmn
        DGV.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGV.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGV.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGV.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGV.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'kunci kolom grid
        DGV.Columns(1).ReadOnly = True
        DGV.Columns(2).ReadOnly = True
        DGV.Columns(3).ReadOnly = True
        DGV.Columns(4).ReadOnly = True
        DGV.Columns(6).ReadOnly = True
    End Sub

    Sub Bersih()
        TotalLbl.Text = "0"
        TerbilangLbl.Text = "Nol"
        TxtKodeBarcode.Text = ""
        TxtKodeBrg.Text = ""
        TxtNmBrg.Text = ""
        TxtSatuan.Text = ""
        TxtHarga.Text = ""
        TxtJml.Text = ""
        TxtDiskon.Text = ""
        TxtSubTtl.Text = ""

        LblKdPel.Text = "0001"
        LblNmPel.Text = "Umum"
        LblHari.Text = Format(Now, "dddd, dd-MMMM-yyyy")

        DGV.Columns.Clear()
        Call FakturOtomatis()
        Call BuatKolomBaru()
        TxtKodeBarcode.Focus()
    End Sub

    Sub Bersih2()
        TxtKodeBarcode.Text = ""
        TxtKodeBrg.Text = ""
        TxtNmBrg.Text = ""
        TxtSatuan.Text = ""
        TxtHarga.Text = ""
        TxtJml.Text = ""
        TxtDiskon.Text = ""
        TxtSubTtl.Text = ""
        TxtKodeBarcode.Focus()
    End Sub

    Private Sub hitung()
        Try

            TotalLbl.Text = "0"

            'Fungsi ini merupakan fungsi yang dapat menghitung kolom total harga pembelian berdasarkan nilai
            'penjumlahan yang ada pada kolom grid kelima dari tabel pembelian
            Dim i As Integer
            i = DGV.CurrentRow.Index
            For i = 0 To DGV.Rows.Count - 1
                TotalLbl.Text = Format(Val(Replace(TotalLbl.Text, ".", "")) + Val(DGV.Item(6, i).Value), "#,#")
            Next

            TerbilangLbl.Text = TerbilangIndo.TerbilangIndonesia(Replace(TotalLbl.Text, ".", "")) & "*"

        Catch ex As Exception
            TotalLbl.Text = "0"
        End Try
    End Sub

    Private Sub FrmPenjualan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call Bersih()
    End Sub

    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        FrmMenuUtama.SidePanel.Height = FrmMenuUtama.BtnHome.Height
        FrmMenuUtama.SidePanel.Top = FrmMenuUtama.BtnHome.Top
        FrmMenuUtama.UcMenuUtama1.BringToFront()
        Me.Close()
    End Sub

    Private Sub TxtKodeBarcode_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtKodeBarcode.KeyDown
        Select Case e.KeyCode
            'Jika tombol F1 ditekan, maka cursor fokus ke text bayar
            Case Keys.F1
                If Val(Replace(TotalLbl.Text, ".", "")) = 0 Then
                    MsgBox("Transaksi Tidak Boleh Kosong", vbInformation, "Pesan") : Exit Sub
                End If
                FrmBayar.TotalLbl.Text = TotalLbl.Text
                FrmBayar.TerbilangLbl.Text = TerbilangLbl.Text
                FrmBayar.Show()
                FrmBayar.TxtBayar.Show()
            Case Keys.F2
                FrmDataBarang.ShowDialog()
                'Jika tombol F2 ditekan, maka tampil form data barang
            Case Keys.F3
                FrmDataPelanggan.ShowDialog()
            Case Keys.Escape
                DGV.Columns.Clear()
                Call Bersih()
        End Select
    End Sub

    Private Sub TxtKodeBarcode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKodeBarcode.KeyPress
        If e.KeyChar = Chr(13) Then
            If TxtKodeBarcode.Text = "" Then FrmDataBarang.Show() : FrmDataBarang.TxtCari.Focus() : Exit Sub

            Call Koneksi()

            Tampil.Connection = Database
            Tampil.CommandType = CommandType.Text

            Tampil.CommandText = "select * from TblBarang where Kode_Barcode = '" & TxtKodeBarcode.Text & "' "
            Tampilkan = Tampil.ExecuteReader
            If Tampilkan.HasRows = True Then
                Tampilkan.Read()
                TxtKodeBrg.Text = Tampilkan("Kode_Barang")
                TxtKodeBarcode.Text = Tampilkan("Kode_Barcode")
                TxtNmBrg.Text = Tampilkan("Nama_Barang")
                TxtSatuan.Text = Tampilkan("satuan")
                TxtHarga.Text = Tampilkan("Harga_Jual")
                TxtDiskon.Text = Tampilkan("Diskon_Jual")
                TxtStock.Text = Tampilkan("Stock")
                TxtJml.Text = "1"
                TxtJml.Focus()
            Else
                MsgBox("Kode Barcode Tidak Ada", MsgBoxStyle.Information)
                TxtKodeBarcode.Text = ""
                TxtKodeBarcode.Focus()
            End If
        End If
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled() = True
    End Sub

    Private Sub DGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV.CellContentClick

    End Sub

    Private Sub TxtJml_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtJml.KeyDown
        Select Case e.KeyCode
            'Jika tombol F1 ditekan, maka cursor fokus ke text bayar
            Case Keys.F1
                If Val(Replace(TotalLbl.Text, ".", "")) = 0 Then
                    MsgBox("Transaksi Tidak Boleh Kosong", vbInformation, "Pesan") : Exit Sub
                End If
                FrmBayar.TotalLbl.Text = TotalLbl.Text
                FrmBayar.TerbilangLbl.Text = TerbilangLbl.Text
                FrmBayar.Show()
                FrmBayar.TxtBayar.Show()
            Case Keys.F2
                FrmDataBarang.ShowDialog()
                'Jika tombol F2 ditekan, maka tampil form data barang
            Case Keys.F3
                FrmDataPelanggan.ShowDialog()
            Case Keys.Escape
                Call Bersih2()
        End Select
    End Sub

    Private Sub TxtJml_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtJml.KeyPress
        If e.KeyChar = Chr(13) Then
            If Val(TxtStock.Text) < Val(TxtJml.Text) Or _
            TxtJml.Text = "0" Or TxtJml.Text = "" Then
                MsgBox("Stock Barang tidak mencukupi / Jumlah tidak boleh kosong")
                TxtJml.Text = ""
                TxtJml.Focus()
            Else
                Dim JmlDiskon As Long
                JmlDiskon = Val(TxtHarga.Text) * (Val(TxtDiskon.Text) / 100)

                Call SubTotal()

                DGV.Rows.Add(TxtKodeBrg.Text, TxtNmBrg.Text, TxtHarga.Text, JmlDiskon, TxtSatuan.Text, TxtJml.Text, TxtSubTtl.Text)
                Call hitung()
                Call Bersih2()
            End If
        End If
        'untuk input hanya angka saja
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled() = True
    End Sub

    Private Sub TxtJml_TextChanged(sender As Object, e As EventArgs) Handles TxtJml.TextChanged
        Call SubTotal()
    End Sub

    Private Sub LblKdPel_Click(sender As Object, e As EventArgs) Handles LblKdPel.Click

    End Sub

    Private Sub LblNmPel_Click(sender As Object, e As EventArgs) Handles LblNmPel.Click

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        LblJam.Text = Format(Now, "hh:mm:ss")
    End Sub

    Private Sub TxtFaktur_Click(sender As Object, e As EventArgs) Handles TxtFaktur.Click

    End Sub
End Class