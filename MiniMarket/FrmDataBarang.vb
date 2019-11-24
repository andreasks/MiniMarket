Public Class FrmDataBarang

    Sub Data_Record()
        Try
            Call Koneksi()
            Ds = New DataSet

            Tabel = "SELECT * FROM TblBarang"
            Grid = New OleDb.OleDbDataAdapter(Tabel, Database)
            Grid.Fill(Ds, "TblBarang")

            Dim GridView As New DataView(Ds.Tables("TblBarang"))
            DGV.DataSource = GridView
            DGV.Columns(0).Width = 100
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub FrmDataBarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TxtCari.Text = ""
        Call Data_Record()
        TxtCari.Focus()
    End Sub

    Private Sub DGV_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DGV.CellMouseClick
        On Error Resume Next
        'Menampilkan data Barang saat Klik Grid
        FrmPenjualan.TxtKodeBrg.Text = DGV.Rows(e.RowIndex).Cells(0).Value
        Call Koneksi()

        Tampil.Connection = Database
        Tampil.CommandType = CommandType.Text

        Tampil.CommandText = "select * from TblBarang where Kode_Barang = '" & FrmPenjualan.TxtKodeBrg.Text & "' "
        Tampilkan = Tampil.ExecuteReader
        If Tampilkan.HasRows = True Then
            Tampilkan.Read()
            FrmPenjualan.TxtKodeBarcode.Text = Tampilkan("Kode_Barcode")
            FrmPenjualan.TxtNmBrg.Text = Tampilkan("Nama_Barang")
            FrmPenjualan.TxtSatuan.Text = Tampilkan("satuan")
            FrmPenjualan.TxtHarga.Text = Tampilkan("Harga_Jual")
            FrmPenjualan.TxtDiskon.Text = Tampilkan("Diskon_Jual")
            FrmPenjualan.TxtStock.Text = Tampilkan("Stock")
            Me.Close()
            FrmPenjualan.TxtJml.Text = "1"
            FrmPenjualan.TxtJml.Focus()

        End If
    End Sub

    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Private Sub TxtCari_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtCari.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Close()
        End Select
    End Sub

    Private Sub TxtCari_TextChanged(sender As Object, e As EventArgs) Handles TxtCari.TextChanged
        Try
            Call Koneksi()
            Ds = New DataSet

            Tabel = "SELECT * FROM TblBarang where nama_barang like '%" & TxtCari.Text & "%'"
            Grid = New OleDb.OleDbDataAdapter(Tabel, Database)
            Grid.Fill(Ds, "TblBarang")

            Dim GridView As New DataView(Ds.Tables("TblBarang"))
            DGV.DataSource = GridView
            DGV.Columns(0).Width = 100
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub
End Class