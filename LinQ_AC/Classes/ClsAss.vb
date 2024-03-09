Imports System
Imports System.Data.Linq
Imports System.Collections.Generic
Imports System.Linq
Public Class ClsAss
    Public Shared Function GetBookAccession() As System.Data.Linq.Table(Of BookAccession)
        Return db.GetTable(Of BookAccession)()
    End Function

    Public Shared Sub SaveBookAccession(ByVal sAccessionCode As String, ByVal iBookID As Integer)
        Try
            Dim post As Table(Of BookAccession) = ClsAss.GetBookAccession
            Dim a As New BookAccession With {
                .AccessionCode = sAccessionCode,
                .BookID = iBookID
            }
            post.InsertOnSubmit(a)
            post.Context.SubmitChanges()

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error Shits!")
            Throw ex
        End Try
    End Sub



    Public Shared Sub DeleteBookAccession(ByVal sAccessionCode As String)
        Try
            Dim delete = (From a In db.BookAccessions
                          Where a.AccessionCode = sAccessionCode
                          Select a).SingleOrDefault()
            db.BookAccessions.DeleteOnSubmit(delete)
            db.SubmitChanges()
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error Shits!")
            Throw ex
        End Try
    End Sub

    Public Shared Function CountBookID(ByVal iBookID As Integer) As Integer
        Try
            Dim cntISBN = (From a In db.BookAccessions
                           Where a.BookID = iBookID
                           Select a).Count()
            Return cntISBN
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error Shits!")
            Throw ex
        End Try
    End Function
    Public Shared Function CountAss(ByVal sAssCode As String) As Integer
        Try
            Dim cntISBN = (From a In db.BookAccessions
                           Where a.AccessionCode = sAssCode
                           Select a).Count()
            Return cntISBN
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error Shits!")
            Throw ex
        End Try
    End Function

    'Public Shared Function ViewBookAccession()

    '    Try
    '        Dim qry = (From a In db.BookAccessions Join b In db.Books On b.BookID Equals a.BookID
    '                   Order By a.BookAccessionID
    '                   Select a.BookAccessionID, a.AccessionCode, a.BookID).ToList
    '        Return qry

    '    Catch ex As Exception
    '        MsgBox(ex.Message, vbCritical, "Error Shits!")
    '        Throw ex
    '    End Try
    'End Function

    Public Shared Function SearchBookAccession(ByVal iBookID As Integer)

        Try
            Dim qry = (From a In db.BookAccessions
                       Where a.BookID = iBookID
                       Order By a.AccessionCode
                       Select a.BookAccessionID, a.AccessionCode, a.BookID).ToList
            Return qry

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error Shits!")
            Throw ex
        End Try
    End Function
    Public Shared Function ViewTrans(ByVal iBookID As Integer)

        Try
            Dim qry = (From a In db.BookAccessions
                       Where a.BookID = iBookID
                       Order By a.AccessionCode
                       Select a.BookAccessionID, a.AccessionCode, a.BookID).ToList
            Return qry

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error Shits!")
            Throw ex
        End Try
    End Function

    Public Shared Function SearchAss(ByVal sAssCodde As String)

        Try
            Dim qry = (From a In db.BookAccessions
                       Where a.AccessionCode = sAssCodde
                       Select a).ToList
            Return qry

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error Shits!")
            Throw ex
        End Try
    End Function


    Public Shared Function SearchMyAss(ByVal iAssID As String)

        Try
            Dim qry = (From b In db.Books
                       Join a In db.BookAccessions On a.BookID Equals b.BookID
                       Where a.AccessionCode.Equals(iAssID)
                       Select a.BookAccessionID, a.AccessionCode, a.BookID, b.ISBN, b.BookName).ToList
            Return qry

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error Shits!")

        End Try
    End Function

End Class
