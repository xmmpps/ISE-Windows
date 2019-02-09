Public Class PartDecider
    Public changeapply As Boolean
    Public FaceStatus As Integer
    Public PathStatus As Integer
    Private Sub 点击1() Handles Body.Checked
        PathStatus = 1211
    End Sub
    Private Sub 点击2() Handles Head.Checked
        PathStatus = 1216
    End Sub
    Private Sub 点击4() Handles LeftArm.Checked
        PathStatus = 1212
    End Sub
    Private Sub 点击5() Handles RightArm.Checked
        PathStatus = 1213
    End Sub
    Private Sub 点击6() Handles RightLeg.Checked
        PathStatus = 1215
    End Sub
    Private Sub 点击3() Handles LeftLeg.Checked
        PathStatus = 1214
    End Sub
    Private Sub 点击7() Handles Left.Checked
        FaceStatus = 7235
    End Sub
    Private Sub 点击8() Handles Right.Checked
        FaceStatus = 7236
    End Sub
    Private Sub 点击9() Handles Front.Checked
        FaceStatus = 7237
    End Sub
    Private Sub 点击10() Handles Back.Checked
        FaceStatus = 7238
    End Sub
    Private Sub 点击11() Handles Top.Checked
        FaceStatus = 7233
    End Sub
    Private Sub 点击12() Handles Buttom.Checked
        FaceStatus = 7234
    End Sub
    Private Sub 移动() Handles TopBar.MouseLeftButtonDown
        Me.DragMove()
    End Sub
    Private Sub 关闭() Handles Close.Click
        changeapply = False
        Me.Hide()
    End Sub
    Private Sub 确认() Handles Enter.Click
        changeapply = True
        Me.Hide()
    End Sub
End Class
