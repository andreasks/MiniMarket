Imports System.Data.OleDb
Public Class UCCetakLapPenjualan
    Sub Bersih()
        CmbNo.Items.Clear()
        Call Koneksi()
        Tampil.Connection = Database
        Tampil.CommandType = CommandType.Text
        Tampil.CommandText = "Select * from TblPenjualan order by No_Faktur desc"
        Tampilkan = Tampil.ExecuteReader

        If Tampilkan.HasRows = True Then
            While Tampilkan.Read()
                CmbNo.Items.Add(Tampilkan("No_Faktur"))
            End While
        End If

        CmbNo.Enabled = False
        CmbNo.Text = ""
        DTPMulai.Value = Today
        DTPMulai.Enabled = False
        DTPAkhir.Value = Today
        DTPAkhir.Enabled = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FrmMenuUtama.UcLaporan1.BringToFront()
    End Sub


    Private Sub UCCetakLapPenjualan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call Bersih()
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        CmbNo.Enabled = False
        CmbNo.Text = ""
        DTPMulai.Value = Today
        DTPMulai.Enabled = False
        DTPAkhir.Value = Today
        DTPAkhir.Enabled = False
        BtnCetak.Focus()
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        CmbNo.Enabled = True
        CmbNo.Text = ""

        DTPMulai.Value = Today
        DTPMulai.Enabled = False
        DTPAkhir.Value = Today
        DTPAkhir.Enabled = False
        CmbNo.Focus()
    End Sub

    Private Sub RadioButton3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton3.CheckedChanged
        CmbNo.Enabled = False
        CmbNo.Text = ""
        DTPMulai.Value = Today
        DTPMulai.Enabled = True
        DTPAkhir.Value = Today
        DTPAkhir.Enabled = True
        DTPMulai.Focus()
    End Sub

    Private Sub BtnCetak_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCetak.Click
        If RadioButton1.Checked = True Then
            'Cetak Semua Data
            FrmMenuUtama.UcLapPenjualan1.CrystalReportViewer1.SelectionFormula = ""
            FrmMenuUtama.UcLapPenjualan1.CrystalReportViewer1.RefreshReport()
            FrmMenuUtama.UcLapPenjualan1.BringToFront()
        ElseIf RadioButton2.Checked = True Then
            If CmbNo.Text = "" Then
                MsgBox("Silahkan pilih No No_Faktur terlebih dahulu", vbInformation + vbOKOnly, "Pesan")
                Exit Sub
            End If
            'Cetak Laporan Berdasarkan Nomor No_Faktur
            FrmMenuUtama.UcLapPenjualan1.CrystalReportViewer1.SelectionFormula = "{TblPenjualan.No_Faktur} ='" & CmbNo.Text & "' "
            FrmMenuUtama.UcLapPenjualan1.CrystalReportViewer1.RefreshReport()
            FrmMenuUtama.UcLapPenjualan1.BringToFront()
        ElseIf RadioButton3.Checked = True Then
            'Cetak Laporan Berdasarkan range tanggal
            FrmMenuUtama.UcLapPenjualan1.CrystalReportViewer1.SelectionFormula = "{TblPenjualan.Tgl_Transaksi} >= date('" & _
                Format(DTPMulai.Value, "yyyy/MM/dd") & "') and {TblPenjualan.Tgl_Transaksi} <= date('" & _
                Format(DTPAkhir.Value, "yyyy/MM/dd") & "')"
            FrmMenuUtama.UcLapPenjualan1.CrystalReportViewer1.RefreshReport()
            FrmMenuUtama.UcLapPenjualan1.BringToFront()
        End If
    End Sub
End Class
