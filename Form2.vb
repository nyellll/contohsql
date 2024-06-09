Public Class Form2

    Dim IC As String

    Public Property SelectedID As String


    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SelectedID = TextBox1.Text
        Dim form4Instance As New Form4()
        form4Instance.SelectedID = SelectedID
        form4Instance.Show()
    End Sub
End Class