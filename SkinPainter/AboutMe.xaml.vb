Public Class AboutMe
    Private Sub 关闭() Handles Close.Click
        Me.Hide()
    End Sub
    Private Sub 移动窗口() Handles TopBar.MouseLeftButtonDown
        Me.DragMove()
    End Sub

    Private Sub Draw_Nor_Click(sender As Object, e As RoutedEventArgs) Handles Draw_Nor.Click
        System.Diagnostics.Process.Start("https://i.vory.work/isw/")
    End Sub
End Class
