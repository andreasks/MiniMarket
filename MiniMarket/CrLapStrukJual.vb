Public Class CrLapStrukJual

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        CrystalReportViewer1.PrintReport()
        Me.Close()
        FrmBayar.Close()
        FrmPenjualan.Bersih()
        FrmPenjualan.Show()
    End Sub

    Private Sub BtnTutup_Click(sender As Object, e As EventArgs) Handles BtnTutup.Click
        Me.Close()
        FrmBayar.Close()
        FrmPenjualan.Bersih()
        FrmPenjualan.Show()
    End Sub
End Class