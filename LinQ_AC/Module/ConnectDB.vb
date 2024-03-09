Imports System.Data.SqlClient

Module ConnectDB
    Public db As New DataClasseLibraryDataContext("Data Source=172.16.10.125;Initial Catalog=Library_DB_AC;Persist Security Info=True;User ID=sa;Password=p@ssw0rd")

End Module
