Public Class UCLaporan

    Private Sub BtnLapBarang_Click(sender As Object, e As EventArgs) Handles BtnLapBarang.Click
        FrmMenuUtama.UcLapBarang1.CrystalReportViewer1.SelectionFormula = ""
        FrmMenuUtama.UcLapBarang1.CrystalReportViewer1.RefreshReport()
        FrmMenuUtama.UcLapBarang1.BringToFront()
    End Sub

    Private Sub BtnLapPelanggan_Click(sender As Object, e As EventArgs) Handles BtnLapPelanggan.Click
        FrmMenuUtama.UcLapPelanggan1.CrystalReportViewer1.SelectionFormula = ""
        FrmMenuUtama.UcLapPelanggan1.CrystalReportViewer1.RefreshReport()
        FrmMenuUtama.UcLapPelanggan1.BringToFront()
    End Sub

    Private Sub BtnLapPemasok_Click(sender As Object, e As EventArgs) Handles BtnLapPemasok.Click
        FrmMenuUtama.UcLapPemasok1.CrystalReportViewer1.SelectionFormula = ""
        FrmMenuUtama.UcLapPemasok1.CrystalReportViewer1.RefreshReport()
        FrmMenuUtama.UcLapPemasok1.BringToFront()
    End Sub

    Private Sub BtnLapBeli_Click(sender As Object, e As EventArgs) Handles BtnLapBeli.Click
        FrmMenuUtama.UcCetakLapPembelian1.Bersih()
        FrmMenuUtama.UcCetakLapPembelian1.Refresh()
        FrmMenuUtama.UcCetakLapPembelian1.BringToFront()
    End Sub

    Private Sub BtnLapJual_Click(sender As Object, e As EventArgs) Handles BtnLapJual.Click
        FrmMenuUtama.UcCetakLapPenjualan1.Bersih()
        FrmMenuUtama.UcCetakLapPenjualan1.Refresh()
        FrmMenuUtama.UcCetakLapPenjualan1.BringToFront()
    End Sub
End Class
