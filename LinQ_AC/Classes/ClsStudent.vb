Imports System
Imports System.Data.Linq
Imports System.Collections.Generic
Imports System.Linq
Public Class ClsStudent
    Public Shared Function GetStudent() As System.Data.Linq.Table(Of Student)
        Return db.GetTable(Of Student)()
    End Function

    Public Shared Sub SaveStudent(ByVal sFname As String, ByVal sLname As String, ByVal sYrLvl As String, ByVal sAddrss As String)
        Try
            Dim post As Table(Of Student) = ClsStudent.GetStudent
            Dim s As New Student With {
            .First_Name = sFname,
            .Last_Name = sLname,
            .Year_Lvl = sYrLvl,
            .Address = sAddrss
            }
            post.InsertOnSubmit(s)
            post.Context.SubmitChanges()

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error Shits!")
            Throw ex
        End Try
    End Sub

    Public Shared Sub UpdateStudent(ByVal iStudent_ID As Integer, ByVal sFname As String, ByVal sLname As String, ByVal sYrLvl As String, ByVal sAddrss As String)
        Try
            Dim update = (From s In db.GetTable(Of Student)()
                          Where s.Student_ID = iStudent_ID
                          Select s).SingleOrDefault()

            update.First_Name = sFname
            update.Last_Name = sLname
            update.Year_Lvl = sYrLvl
            update.Address = sAddrss
            db.SubmitChanges()

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error Shits!")
            Throw ex
        End Try
    End Sub

    Public Shared Sub DeleteStudent(ByVal iStudent_ID As Integer)
        Try
            Dim delete = (From s In db.Students
                          Where s.Student_ID = iStudent_ID
                          Select s).SingleOrDefault()
            db.Students.DeleteOnSubmit(delete)
            db.SubmitChanges()
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error Shits!")
            Throw ex
        End Try
    End Sub

    Public Shared Function CountFullName(ByVal sFullName As String) As Integer
        Try
            Dim cntISBN = (From s In db.Students
                           Where s.First_Name + s.Last_Name = sFullName
                           Select s).Count()
            Return cntISBN
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error Shits!")
            Throw ex
        End Try
    End Function

    Public Shared Function ViewStudent()

        Try
            Dim qry = (From s In db.Students
                       Order By s.Last_Name
                       Select s.Student_ID, s.First_Name, s.Last_Name, s.Year_Lvl, s.Address).ToList
            Return qry

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error Shits!")
            Throw ex
        End Try
    End Function


    Public Shared Function transactions()

        Try
            Dim qry = (From t In db.View_Details
                       Join a In db.TransHeaders On a.TransID Equals t.TransID
                       Select t.StudentName, t.Year_Lvl, t.BookName, t.ISBN, t.DateBorrowed, t.DateToReturn, t.TransID, a.Student_ID).ToList
            Return qry

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error Shits!")
            Throw ex
        End Try
    End Function

    Public Shared Function SearchStudent(ByVal sStudentname As String)
        Try
            Dim qry = (From s In db.Students
                       Where (String.Concat(s.First_Name, "", s.Last_Name)).Contains(sStudentname)
                       Order By s.Student_ID
                       Select s).ToList
            Return qry

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error Shits!")
            Throw ex
        End Try
    End Function
End Class
