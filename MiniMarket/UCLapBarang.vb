Public Class UCLapBarang

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FrmMenuUtama.SidePanel.Height = FrmMenuUtama.BtnLaporan.Height
        FrmMenuUtama.SidePanel.Top = FrmMenuUtama.BtnLaporan.Top
        FrmMenuUtama.UcLaporan1.BringToFront()
    End Sub
End Class
