﻿Imports System.Data.OleDb
Imports System.Data.Sql
Module ModKoneksi
    Public Database As New OleDb.OleDbConnection
    Public Tampil As New OleDb.OleDbCommand
    Public Tampil2 As New OleDb.OleDbCommand
    Public Tampilkan As OleDb.OleDbDataReader
    Public Tampilkan2 As OleDb.OleDbDataReader
    Public Grid As New OleDb.OleDbDataAdapter
    Public Ds As New DataSet
    Public Tabel As String
    Public record As New BindingSource
    Public DMLSql As New OleDb.OleDbCommand
    Public TerbilangIndo As New ClsTerbilangIndo

    'Ubah nama server sesuai nama server anda
    Public Sub Koneksi()
        Try
            Database.Close()
            Database.ConnectionString = "Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=MiniMarket;Data Source=AZIZ-PC"
            Database.Open()
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub
End Module
