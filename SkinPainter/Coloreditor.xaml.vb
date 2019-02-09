Public Class Coloreditor
    Public changeapply As Boolean
    Public subawake As Boolean = False
    Private Sub HSV改变() Handles H.LostFocus
        Dim htmp As Integer = Val(H.Text)
        Dim vtmp As Integer = Val(V.Text)
        Dim stmp As Integer = Val(S.Text)
        If htmp >= 0 And htmp < 360 And stmp >= 0 And stmp <= 100 And vtmp >= 0 And vtmp <= 100 Then
            Dim tmprgb = HSVtoRGB(255, htmp, stmp, vtmp)
            R.Text = tmprgb.R
            G.Text = tmprgb.G
            B.Text = tmprgb.B
            PreView.Background = New SolidColorBrush(Media.Color.FromArgb(255, tmprgb.R, tmprgb.G, tmprgb.B))
        Else
            Dim hsv = RGBtoHSV(MediaSoildBrushtoGDIC(PreView.Background))
            H.Text = hsv(0)
            S.Text = hsv（1）
            V.Text = hsv（2）
            MsgBox("HSV数值超出定义范围")
        End If
    End Sub
    Private Sub S改变() Handles S.LostFocus
        Call HSV改变()
    End Sub
    Private Sub V改变() Handles V.LostFocus
        Call HSV改变()
    End Sub
    Private Sub ARGB改变() Handles A.LostFocus
        Dim atmp As Integer = Val（A.Text）
        Dim rtmp As Integer = Val（R.Text）
        Dim gtmp As Integer = Val（G.Text）
        Dim btmp As Integer = Val（B.Text）
        If atmp >= 0 And atmp <= 255 And gtmp >= 0 And gtmp <= 255 And rtmp >= 0 And rtmp <= 255 And btmp >= 0 And gtmp <= 255 Then
            Dim hsv = RGBtoHSV(System.Drawing.Color.FromArgb(255, rtmp, gtmp, btmp))
            H.Text = FormatHue(hsv(0))
            S.Text = hsv（1）
            V.Text = hsv（2）
            AutoHue.Tag = atmp
            PreView.Background = New SolidColorBrush(Media.Color.FromArgb(255, rtmp, gtmp, btmp))
        Else
            Dim rgb = MediaSoildBrushtoGDIC(PreView.Background)
            R.Text = rgb.R
            G.Text = rgb.G
            B.Text = rgb.B
            A.Text = AutoHue.Tag
            MsgBox("ARGB数值超出定义范围(0-255)")
        End If
    End Sub
    Private Sub R改变() Handles R.LostFocus
        Call ARGB改变()
    End Sub
    Private Sub G改变() Handles G.LostFocus
        Call ARGB改变()
    End Sub
    Private Sub B改变() Handles B.LostFocus
        Call ARGB改变()
    End Sub
    Private Sub H加成改变() Handles H_Plus.LostFocus
        Dim hptmp = Val(H_Plus.Text)
        If hptmp <= -180 Or hptmp > 180 Then
            H_Plus.Text = 2
            MsgBox("H+数值超出定义范围(-180,180]")
        End If
    End Sub
    Private Sub S加成改变() Handles S_Plus.LostFocus
        Dim hptmp = Val(S_Plus.Text)
        If hptmp <= -100 Or hptmp >= 100 Then
            S_Plus.Text = -2
            MsgBox("S+数值超出定义范围[-100,100]")
        End If
    End Sub
    Private Sub V加成改变() Handles V_Plus.LostFocus
        Dim hptmp = Val(V_Plus.Text)
        If hptmp <= -100 Or hptmp >= 100 Then
            V_Plus.Text = 4
            MsgBox("V+数值超出定义范围[-100,100]")
        End If
    End Sub
    Private Sub 应用修改颜色() Handles Apply.Click
        changeapply = True
        Me.Hide()
    End Sub
    Private Sub 不应用修改颜色() Handles Close.Click
        changeapply = False
        Me.Hide()
    End Sub
    Private Sub 解除限制警告() Handles ColorBlock.Unchecked
        MsgBox("建议不要解除限制以免丢失HSV信息（超出极限后自动渐变将不可逆）")
    End Sub
    Private Sub 移动() Handles TopBar.MouseLeftButtonDown
        Me.DragMove()
    End Sub
End Class
