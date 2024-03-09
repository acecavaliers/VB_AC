Imports System
Imports System.Data.Linq
Imports System.Collections.Generic
Imports System.Linq
Public Class ClsBooks
    Public Shared Function GetBook() As System.Data.Linq.Table(Of Book)
        Return db.GetTable(Of Book)()
    End Function

    Public Shared Sub SaveBook(ByVal sBookName As String, ByVal sISBN As String, ByVal iPubID As Integer)
        Try
            Dim post As Table(Of Book) = ClsBooks.GetBook
            Dim b As New Book With {
                .BookName = sBookName,
                .ISBN = sISBN,
            .PublisherID = iPubID
            }
            post.InsertOnSubmit(b)
            post.Context.SubmitChanges()

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error Shits!")
            Throw ex
        End Try
    End Sub

    Public Shared Sub UpdateBook(ByVal iBookID As Integer, ByVal sBookName As String, ByVal sISBN As String, ByVal iPubID As Integer)
        Try
            Dim update = (From b In db.GetTable(Of Book)()
                          Where b.BookID = iBookID
                          Select b).SingleOrDefault()

            update.BookName = sBookName
            update.ISBN = sISBN
            update.PublisherID = iPubID
            db.SubmitChanges()

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error Shits!")
            Throw ex
        End Try
    End Sub

    Public Shared Sub UpdateQty(ByVal iBookID As Integer, ByVal iQty As Integer)
        Try
            Dim update = (From b In db.GetTable(Of Book)()
                          Where b.BookID = iBookID
                          Select b).SingleOrDefault()

            update.Qty = iQty
            db.SubmitChanges()

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error Shits!")
            Throw ex
        End Try
    End Sub

    Public Shared Sub DeleteBook(ByVal iBookID As Integer)
        Try
            Dim delete = (From b In db.Books
                          Where b.BookID = iBookID
                          Select b).SingleOrDefault()
            db.Books.DeleteOnSubmit(delete)
            db.SubmitChanges()
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error Shits!")
            Throw ex
        End Try
    End Sub

    Public Shared Function CountISBN(ByVal sISBN As String) As Integer
        Try
            Dim cntISBN = (From b In db.Books
                           Where b.ISBN = sISBN
                           Select b).Count()
            Return cntISBN
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error Shits!")
            Throw ex
        End Try
    End Function

    Public Shared Function ViewBook()

        Try
            Dim qry = (From b In db.Books Join P In db.Publishers On P.PublisherID Equals b.PublisherID
                       Order By b.BookName
                       Select b.BookID, b.BookName, b.ISBN, P.PublisherName, b.Qty).ToList
            Return qry

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error Shits!")
            Throw ex
        End Try
    End Function

    Public Shared Function SearchBook(ByVal sBookname As String)

        Try
            Dim qry = (From b In db.Books
                       Where b.BookName.Contains(sBookname)
                       Order By b.BookName
                       Select b).ToList
            Return qry

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error Shits!")
            Throw ex
        End Try
    End Function

End Class
