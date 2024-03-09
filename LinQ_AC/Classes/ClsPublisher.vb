Imports System
Imports System.Data.Linq
Imports System.Collections.Generic
Imports System.Linq

Public Class ClsPublisher
    Public Shared Function GetPublisher() As System.Data.Linq.Table(Of Publisher)
        Return db.GetTable(Of Publisher)()
    End Function

    Public Shared Sub SavePublisher(ByVal sPubname As String)
        Try
            Dim post As Table(Of Publisher) = ClsPublisher.GetPublisher
            Dim p As New Publisher With {
                .PublisherName = sPubname
            }
            post.InsertOnSubmit(p)
            post.Context.SubmitChanges()

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error Shits!")
            Throw ex
        End Try
    End Sub

    Public Shared Sub UpdatePublisher(ByVal sPubnamme As String, ByVal iPubID As Integer)
        Try
            Dim update = (From p In db.GetTable(Of Publisher)()
                          Where p.PublisherID = iPubID
                          Select p).SingleOrDefault()

            update.PublisherName = sPubnamme
            db.SubmitChanges()

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error Shits!")
            Throw ex
        End Try
    End Sub

    Public Shared Sub DeletePublisher(ByVal iPubID As Integer)
        Try
            Dim delete = (From p In db.Publishers
                          Where p.PublisherID = iPubID
                          Select p).SingleOrDefault()
            db.Publishers.DeleteOnSubmit(delete)
            db.SubmitChanges()
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error Shits!")
            Throw ex
        End Try
    End Sub

    Public Shared Function CountPublisher(ByVal sPublishername As String) As Integer
        Try
            Dim cnt = (From p In db.Publishers
                       Where p.PublisherName = sPublishername
                       Select p).Count()
            Return cnt
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error Shits!")
            Throw ex
        End Try
    End Function

    Public Shared Function ViewPublisher()

        Try
            Dim qry = (From p In db.Publishers
                       Select p Order By p.PublisherName).ToList
            Return qry

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error Shits!")
            Throw ex
        End Try
    End Function

    Public Shared Function SelectPublisher()

        Try
            Dim qry = (From p In db.Publishers
                       Order By p.PublisherName
                       Select p.PublisherName).ToList
            Return qry

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error Shits!")
            Throw ex
        End Try
    End Function
    Public Shared Function GetPubID(ByVal sPublishername As String)

        Try
            Dim qry = (From p In db.Publishers
                       Where p.PublisherName.Contains(sPublishername)
                       Order By p.PublisherName
                       Select p.PublisherID).Single
            Return qry

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error Shits!")
            Throw ex
        End Try
    End Function
    Public Shared Function SearchPublisher(ByVal sPublishername As String)

        Try
            Dim qry = (From p In db.Publishers
                       Where p.PublisherName.Contains(sPublishername)
                       Order By p.PublisherName
                       Select p).ToList
            Return qry

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error Shits!")
            Throw ex
        End Try
    End Function


End Class
