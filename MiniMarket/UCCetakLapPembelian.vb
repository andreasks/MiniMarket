Imports System.Data.OleDb
Public Class UCCetakLapPembelian

    Sub Bersih()
        CmbNo.Items.Clear()
        Call Koneksi()
        Tampil.Connection = Database
        Tampil.CommandType = CommandType.Text
        Tampil.CommandText = "Select * from TblPembelian order by No_Transaksi desc"
        Tampilkan = Tampil.ExecuteReader

        If Tampilkan.HasRows = True Then
            While Tampilkan.Read()
                CmbNo.Items.Add(Tampilkan("No_Transaksi"))
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

    Private Sub UCCetakLapPembelian_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            FrmMenuUtama.UcLapPembelian1.CrystalReportViewer1.SelectionFormula = ""
            FrmMenuUtama.UcLapPembelian1.CrystalReportViewer1.RefreshReport()
            FrmMenuUtama.UcLapPembelian1.BringToFront()
        ElseIf RadioButton2.Checked = True Then
            If CmbNo.Text = "" Then
                MsgBox("Silahkan pilih No No_Transaksi terlebih dahulu", vbInformation + vbOKOnly, "Pesan")
                Exit Sub
            End If
            'Cetak Laporan Berdasarkan Nomor No_Transaksi
            FrmMenuUtama.UcLapPembelian1.CrystalReportViewer1.SelectionFormula = "{TblPembelian.No_Transaksi} ='" & CmbNo.Text & "' "
            FrmMenuUtama.UcLapPembelian1.CrystalReportViewer1.RefreshReport()
            FrmMenuUtama.UcLapPembelian1.BringToFront()
        ElseIf RadioButton3.Checked = True Then
            'Cetak Laporan Berdasarkan range tanggal
            FrmMenuUtama.UcLapPembelian1.CrystalReportViewer1.SelectionFormula = "{TblPembelian.Tgl_Transaksi} >= date('" & _
                Format(DTPMulai.Value, "yyyy/MM/dd") & "') and {TblPembelian.Tgl_Transaksi} <= date('" & _
                Format(DTPAkhir.Value, "yyyy/MM/dd") & "')"
            FrmMenuUtama.UcLapPembelian1.CrystalReportViewer1.RefreshReport()
            FrmMenuUtama.UcLapPembelian1.BringToFront()
        End If
    End Sub
End Class
