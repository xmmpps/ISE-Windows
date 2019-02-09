Imports System.Windows.Forms
Public Class Creator
    Public applychange As Boolean = False
    Dim 主色 As New System.Drawing.Bitmap(8, 1)
    Dim 复色1 As New System.Drawing.Bitmap(8, 1)
    Dim 复色2 As New System.Drawing.Bitmap(6, 1)
    Dim 眼色 As New System.Drawing.Bitmap(6, 1)
    Dim 组件源 As New System.Drawing.Bitmap(64, 64)
    Dim 组件改 As New System.Drawing.Bitmap(64, 64)
    Public 临时皮肤文件 As New System.Drawing.Bitmap(64, 64)
    Public 调色板 As New System.Drawing.Bitmap(12, 12)
    Public 区域画笔 As System.Drawing.Color
    Dim Coloreditor As New Coloreditor
    '皮肤定义完成
    Private Sub 启动() Handles Me.Loaded
        AddHandler Coloreditor.Apply.Click, AddressOf 结束颜色编辑
        AddHandler Coloreditor.Close.Click, AddressOf 结束颜色编辑
        EycC.Source = BitmapToBitmapSource(眼色)
        MainC.Source = BitmapToBitmapSource(主色)
        SubC.Source = BitmapToBitmapSource(复色1)
        SecC.Source = BitmapToBitmapSource(复色2)
        DrawedImg.Source = BitmapToBitmapSource(组件改)
        AddonImg.Source = BitmapToBitmapSource(组件源)
        Call VChange()
    End Sub
    Private Sub 移动窗口() Handles TopBar.MouseLeftButtonDown
        Me.DragMove()
    End Sub
    Private Sub 打开文件() Handles Import.Click
        Dim myStream As IO.Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.Filter = "PNG图像文件 (*.png)|*.png"
        openFileDialog1.FilterIndex = 1
        openFileDialog1.Title = "打开皮肤组件文件"
        openFileDialog1.RestoreDirectory = False
        openFileDialog1.ShowDialog()

        If Right(openFileDialog1.FileName, 4) = “.PNG” Or Right(openFileDialog1.FileName, 4) = “.png” Then
            Dim fs As System.IO.FileStream = Nothing
            fs = New System.IO.FileStream(openFileDialog1.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read)
            Dim tmpbmp As System.Drawing.Bitmap
            tmpbmp = System.Drawing.Bitmap.FromStream(fs)
            fs.Close()

            If tmpbmp.Height = 64 And tmpbmp.Width = 64 Then
                If tmpbmp.GetPixel(0, 0) = System.Drawing.Color.FromArgb(0, 162, 232) Then
                    tmpbmp.SetPixel(0, 0, System.Drawing.Color.Transparent)
                    组件源 = tmpbmp
                    AddonImg.Source = BitmapToBitmapSource(组件源)
                Else
                    MessageBox.Show("这不是一个组件素材")
                End If

            Else
                MessageBox.Show("格式大小错误: " & tmpbmp.Size.ToString)
            End If
        End If
    End Sub
    Private Sub HChange() Handles HSlider.ValueChanged
        Location.Width = (HSlider.Value / 2 + 20)
        AddColor.Background = New SolidColorBrush(HSVtoRGB(ASlider.Value, HSlider.Value, SSlider.Value, VSlider.Value))
    End Sub
    Private Sub SChange() Handles SSlider.ValueChanged
        Location.Height = 200 - (SSlider.Value * 1.8)
        AddColor.Background = New SolidColorBrush(HSVtoRGB(ASlider.Value, HSlider.Value, SSlider.Value, VSlider.Value))
    End Sub
    Private Sub VChange() Handles VSlider.ValueChanged
        AddColor.Background = New SolidColorBrush(HSVtoRGB(ASlider.Value, HSlider.Value, SSlider.Value, VSlider.Value))
        If VSlider.Value >= 60 Then
            AddColor.Foreground = New SolidColorBrush(Media.Color.FromArgb(255, 48, 64, 70))
        Else
            AddColor.Foreground = New SolidColorBrush(Media.Color.FromArgb(255, 255, 255, 255))
        End If
    End Sub
    Private Sub AChange() Handles ASlider.ValueChanged
        AddColor.Background = New SolidColorBrush(HSVtoRGB(ASlider.Value, HSlider.Value, SSlider.Value, VSlider.Value))
    End Sub
    Private Sub 设置当前色() Handles AddColor.Click
        区域画笔 = MediaSoildBrushtoGDIC(AddColor.Background)
        ColorNow.Fill = AddColor.Background
    End Sub
    Private Sub 放置颜色() Handles TmpC.MouseLeftButtonDown
        Dim p As Point = Mouse.GetPosition(TmpC)
        Dim x, y As Single
        x = Int((p.X * 调色板.Width) / TmpC.Width)
        y = Int((p.Y * 调色板.Height) / TmpC.Height)
        调色板.SetPixel(x, y, 区域画笔)
        TmpC.Source = BitmapToBitmapSource(调色板)
    End Sub
    Private Sub 取得颜色() Handles TmpC.MouseRightButtonDown
        Dim p As Point = Mouse.GetPosition(TmpC)
        Dim x, y As Single
        x = Int((p.X * 调色板.Width) / TmpC.Width)
        y = Int((p.Y * 调色板.Height) / TmpC.Height)
        区域画笔 = 调色板.GetPixel(x, y)
        ColorNow.Fill = New SolidColorBrush(Media.Color.FromArgb(区域画笔.A, 区域画笔.R, 区域画笔.G, 区域画笔.B))
    End Sub
    Private Sub 主上色放置颜色() Handles MainC.MouseLeftButtonDown
        Dim p As Point = Mouse.GetPosition(MainC)
        Dim x, y As Single
        x = Int((p.X * 主色.Width) / MainC.Width)
        y = Int((p.Y * 主色.Height) / MainC.Height)
        主色.SetPixel(x, y, 区域画笔)
        MainC.Source = BitmapToBitmapSource(主色)
    End Sub
    Private Sub 主上色取得颜色() Handles MainC.MouseRightButtonDown
        Dim p As Point = Mouse.GetPosition(MainC)
        Dim x, y As Single
        x = Int((p.X * 主色.Width) / MainC.Width)
        y = Int((p.Y * 主色.Height) / MainC.Height)
        区域画笔 = 主色.GetPixel(x, y)
        ColorNow.Fill = New SolidColorBrush(Media.Color.FromArgb(区域画笔.A, 区域画笔.R, 区域画笔.G, 区域画笔.B))
    End Sub
    Private Sub 副上色放置颜色() Handles SubC.MouseLeftButtonDown
        Dim p As Point = Mouse.GetPosition(SubC)
        Dim x, y As Single
        x = Int((p.X * 复色1.Width) / SubC.Width)
        y = Int((p.Y * 复色1.Height) / SubC.Height)
        复色1.SetPixel(x, y, 区域画笔)
        SubC.Source = BitmapToBitmapSource(复色1)
    End Sub
    Private Sub 副上色取得颜色() Handles SubC.MouseRightButtonDown
        Dim p As Point = Mouse.GetPosition(SubC)
        Dim x, y As Single
        x = Int((p.X * 复色1.Width) / SubC.Width)
        y = Int((p.Y * 复色1.Height) / SubC.Height)
        区域画笔 = 复色1.GetPixel(x, y)
        ColorNow.Fill = New SolidColorBrush(Media.Color.FromArgb(区域画笔.A, 区域画笔.R, 区域画笔.G, 区域画笔.B))
    End Sub
    Private Sub 眼睛部位放置颜色() Handles EycC.MouseLeftButtonDown
        Dim p As Point = Mouse.GetPosition(EycC)
        Dim x, y As Single
        x = Int((p.X * 眼色.Width) / EycC.Width)
        y = Int((p.Y * 眼色.Height) / EycC.Height)
        眼色.SetPixel(x, y, 区域画笔)
        EycC.Source = BitmapToBitmapSource(眼色)
    End Sub
    Private Sub 眼睛部位取得颜色() Handles EycC.MouseRightButtonDown
        Dim p As Point = Mouse.GetPosition(EycC)
        Dim x, y As Single
        x = Int((p.X * 眼色.Width) / EycC.Width)
        y = Int((p.Y * 眼色.Height) / EycC.Height)
        区域画笔 = 眼色.GetPixel(x, y)
        ColorNow.Fill = New SolidColorBrush(Media.Color.FromArgb(区域画笔.A, 区域画笔.R, 区域画笔.G, 区域画笔.B))
    End Sub
    Private Sub 次上色放置颜色() Handles SecC.MouseLeftButtonDown
        Dim p As Point = Mouse.GetPosition(SecC)
        Dim x, y As Single
        x = Int((p.X * 复色2.Width) / SecC.Width)
        y = Int((p.Y * 复色2.Height) / SecC.Height)
        复色2.SetPixel(x, y, 区域画笔)
        SecC.Source = BitmapToBitmapSource(复色2)
    End Sub
    Private Sub 次上色取得颜色() Handles SecC.MouseRightButtonDown
        Dim p As Point = Mouse.GetPosition(SecC)
        Dim x, y As Single
        x = Int((p.X * 复色2.Width) / SecC.Width)
        y = Int((p.Y * 复色2.Height) / SecC.Height)
        区域画笔 = 复色2.GetPixel(x, y)
        ColorNow.Fill = New SolidColorBrush(Media.Color.FromArgb(区域画笔.A, 区域画笔.R, 区域画笔.G, 区域画笔.B))
    End Sub
    Private Sub 编辑当前颜色() Handles ColorNow.MouseDown
        Coloreditor.subawake = True
        Coloreditor.Show()
        Coloreditor.changeapply = False
        Coloreditor.R.Text = MediaSoildBrushtoGDIC(ColorNow.Fill).R
        Coloreditor.G.Text = MediaSoildBrushtoGDIC(ColorNow.Fill).G
        Coloreditor.B.Text = MediaSoildBrushtoGDIC(ColorNow.Fill).B
        Coloreditor.A.Text = MediaSoildBrushtoGDIC(ColorNow.Fill).A
        Dim hsv = RGBtoHSV(MediaSoildBrushtoGDIC(ColorNow.Fill))
        Coloreditor.H.Text = hsv(0)
        Coloreditor.S.Text = hsv（1）
        Coloreditor.V.Text = hsv（2）
        Coloreditor.PreView.Background = ColorNow.Fill
    End Sub
    Private Sub 结束颜色编辑()
        If Coloreditor.changeapply = True Then
            If Coloreditor.subawake = True Then
                区域画笔 = MediaSoildBrushtoGDIC(Coloreditor.PreView.Background)
                ColorNow.Fill = Coloreditor.PreView.Background
            End If
        End If
        Coloreditor.Hide()
    End Sub
    Private Sub 当前颜色变亮() Handles ColorIncrease.Click
        Dim hsv = RGBtoHSV(MediaSoildBrushtoGDIC(ColorNow.Fill))
        Dim htmp As Integer = Val(hsv(0))
        If Coloreditor.AutoHue.IsChecked = True Then
            If htmp >= 60 And htmp <= 240 Then
                htmp = FormatHue(htmp - Math.Abs(Val(Coloreditor.H_Plus.Text)))
            Else
                htmp = FormatHue(htmp + Math.Abs(Val(Coloreditor.H_Plus.Text)))
            End If
        Else
            htmp = FormatHue(htmp + Val(Coloreditor.H_Plus.Text))
        End If
        '色度确定
        Dim stmp As Integer = Val(hsv(1)) + Val(Coloreditor.S_Plus.Text)
        Dim vtmp As Integer = Val(hsv(2)) + Val(Coloreditor.V_Plus.Text)
        'SV确定
        If Coloreditor.ColorBlock.IsChecked = True Then
            '=======================================确定没有超限===============================================
            If stmp <= 100 And stmp >= 0 And vtmp >= 0 And vtmp <= 100 Then
                Dim tmprgb = HSVtoRGB(MediaSoildBrushtoGDIC(ColorNow.Fill).A, htmp, stmp, vtmp)
                ColorNow.Fill = New SolidColorBrush(Media.Color.FromArgb(tmprgb.A, tmprgb.R, tmprgb.G, tmprgb.B))
                区域画笔 = System.Drawing.Color.FromArgb(tmprgb.A, tmprgb.R, tmprgb.G, tmprgb.B)
            Else
                MsgBox("饱和度或亮度已达到极限，请点击当前色：手动调整或解除限制")
            End If
            '===================================================================================================
        Else
            '直接取极值
            Dim tmprgb = HSVtoRGB(MediaSoildBrushtoGDIC(ColorNow.Fill).A, htmp, Math.Min(Math.Max(stmp, 0), 100), Math.Min(Math.Max(vtmp, 0), 100))
            ColorNow.Fill = New SolidColorBrush(Media.Color.FromArgb(tmprgb.A, tmprgb.R, tmprgb.G, tmprgb.B))
            区域画笔 = System.Drawing.Color.FromArgb(tmprgb.A, tmprgb.R, tmprgb.G, tmprgb.B)
        End If
    End Sub
    Private Sub 当前颜色变暗() Handles ColorDecrease.Click
        Dim hsv = RGBtoHSV(MediaSoildBrushtoGDIC(ColorNow.Fill))
        Dim htmp As Integer = Val(hsv(0))
        If Coloreditor.AutoHue.IsChecked = True Then
            If htmp >= 60 And htmp <= 240 Then
                htmp = FormatHue(htmp + Math.Abs(Val(Coloreditor.H_Plus.Text)))
            Else
                htmp = FormatHue(htmp - Math.Abs(Val(Coloreditor.H_Plus.Text)))
            End If
        Else
            htmp = FormatHue(htmp - Val(Coloreditor.H_Plus.Text))
        End If
        '色度确定
        Dim stmp As Integer = Val(hsv(1)) - Val(Coloreditor.S_Plus.Text)
        Dim vtmp As Integer = Val(hsv(2)) - Val(Coloreditor.V_Plus.Text)
        'SV确定
        If Coloreditor.ColorBlock.IsChecked = True Then
            '=======================================确定没有超限===============================================
            If stmp <= 100 And stmp >= 0 And vtmp >= 0 And vtmp <= 100 Then
                Dim tmprgb = HSVtoRGB(MediaSoildBrushtoGDIC(ColorNow.Fill).A, htmp, stmp, vtmp)
                ColorNow.Fill = New SolidColorBrush(Media.Color.FromArgb(tmprgb.A, tmprgb.R, tmprgb.G, tmprgb.B))
                区域画笔 = System.Drawing.Color.FromArgb(tmprgb.A, tmprgb.R, tmprgb.G, tmprgb.B)
            Else
                MsgBox("饱和度或亮度已达到极限，请点击当前色：手动调整或解除限制")
            End If
            '===================================================================================================
        Else
            '直接取极值
            Dim tmprgb = HSVtoRGB(MediaSoildBrushtoGDIC(ColorNow.Fill).A, htmp, Math.Min(Math.Max(stmp, 0), 100), Math.Min(Math.Max(vtmp, 0), 100))
            ColorNow.Fill = New SolidColorBrush(Media.Color.FromArgb(tmprgb.A, tmprgb.R, tmprgb.G, tmprgb.B))
            区域画笔 = System.Drawing.Color.FromArgb(tmprgb.A, tmprgb.R, tmprgb.G, tmprgb.B)
        End If
    End Sub
    Private Sub 上色() Handles Draw_Nor.Click
        Dim tmpbmp = 组件源
        For iy = 0 To tmpbmp.Height - 1
            For ix = 0 To tmpbmp.Width - 1
                Dim tmpclr = tmpbmp.GetPixel(ix, iy)
                If tmpclr.A > 0 Then
                    tmpbmp.SetPixel(ix, iy, System.Drawing.Color.FromArgb(255, tmpclr.R, tmpclr.G, tmpclr.B))
                End If
            Next
        Next
        Dim ma = System.Drawing.Graphics.FromImage(组件改)
        ma.Clear(System.Drawing.Color.Transparent)
        For iy = 0 To tmpbmp.Height - 1
            For ix = 0 To tmpbmp.Width - 1
                Dim swhclr = tmpbmp.GetPixel(ix, iy)
                If swhclr = System.Drawing.Color.FromArgb(255, 26, 26, 26) Then 组件改.SetPixel(ix, iy, 主色.GetPixel(0, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 27, 27, 27) Then 组件改.SetPixel(ix, iy, 主色.GetPixel(0, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 46, 46, 46) Then 组件改.SetPixel(ix, iy, 主色.GetPixel(1, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 66, 66, 66) Then 组件改.SetPixel(ix, iy, 主色.GetPixel(2, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 87, 87, 87) Then 组件改.SetPixel(ix, iy, 主色.GetPixel(3, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 107, 107, 107) Then 组件改.SetPixel(ix, iy, 主色.GetPixel(4, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 108, 108, 108) Then 组件改.SetPixel(ix, iy, 主色.GetPixel(4, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 128, 128, 128) Then 组件改.SetPixel(ix, iy, 主色.GetPixel(5, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 148, 148, 148) Then 组件改.SetPixel(ix, iy, 主色.GetPixel(6, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 168, 168, 168) Then 组件改.SetPixel(ix, iy, 主色.GetPixel(7, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 110, 150, 86) Then 组件改.SetPixel(ix, iy, 眼色.GetPixel(0, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 111, 150, 85) Then 组件改.SetPixel(ix, iy, 眼色.GetPixel(0, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 125, 171, 99) Then 组件改.SetPixel(ix, iy, 眼色.GetPixel(1, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 140, 191, 111) Then 组件改.SetPixel(ix, iy, 眼色.GetPixel(2, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 140, 192, 111) Then 组件改.SetPixel(ix, iy, 眼色.GetPixel(2, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 157, 212, 125) Then 组件改.SetPixel(ix, iy, 眼色.GetPixel(3, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 158, 212, 125) Then 组件改.SetPixel(ix, iy, 眼色.GetPixel(3, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 172, 232, 137) Then 组件改.SetPixel(ix, iy, 眼色.GetPixel(4, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 172, 231, 137) Then 组件改.SetPixel(ix, iy, 眼色.GetPixel(4, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 188, 252, 151) Then 组件改.SetPixel(ix, iy, 眼色.GetPixel(5, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 189, 252, 150) Then 组件改.SetPixel(ix, iy, 眼色.GetPixel(5, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 15, 0, 27) Then 组件改.SetPixel(ix, iy, 复色1.GetPixel(0, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 27, 0, 46) Then 组件改.SetPixel(ix, iy, 复色1.GetPixel(1, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 38, 0, 66) Then 组件改.SetPixel(ix, iy, 复色1.GetPixel(2, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 52, 0, 87) Then 组件改.SetPixel(ix, iy, 复色1.GetPixel(3, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 62, 0, 108) Then 组件改.SetPixel(ix, iy, 复色1.GetPixel(4, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 74, 0, 128) Then 组件改.SetPixel(ix, iy, 复色1.GetPixel(5, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 85, 0, 147) Then 组件改.SetPixel(ix, iy, 复色1.GetPixel(6, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 97, 0, 168) Then 组件改.SetPixel(ix, iy, 复色1.GetPixel(7, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 85, 116, 150) Then 组件改.SetPixel(ix, iy, 复色2.GetPixel(0, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 99, 134, 171) Then 组件改.SetPixel(ix, iy, 复色2.GetPixel(1, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 111, 150, 192) Then 组件改.SetPixel(ix, iy, 复色2.GetPixel(2, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 125, 167, 212) Then 组件改.SetPixel(ix, iy, 复色2.GetPixel(3, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 137, 183, 231) Then 组件改.SetPixel(ix, iy, 复色2.GetPixel(4, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 150, 200, 252) Then 组件改.SetPixel(ix, iy, 复色2.GetPixel(5, 0))
            Next
        Next
        DrawedImg.Source = BitmapToBitmapSource(组件改)
    End Sub
    Private Sub 保留式上色() Handles Draw_Chg.Click
        Dim tmpbmp = 组件源
        For iy = 0 To tmpbmp.Height - 1
            For ix = 0 To tmpbmp.Width - 1
                Dim tmpclr = tmpbmp.GetPixel(ix, iy)
                If tmpclr.A > 0 Then
                    tmpbmp.SetPixel(ix, iy, System.Drawing.Color.FromArgb(255, tmpclr.R, tmpclr.G, tmpclr.B))
                End If
            Next
        Next
        Dim ma = System.Drawing.Graphics.FromImage(组件改)
        ma.Clear(System.Drawing.Color.Transparent)
        For iy = 0 To tmpbmp.Height - 1
            For ix = 0 To tmpbmp.Width - 1
                组件改.SetPixel(ix, iy, 组件源.GetPixel(ix, iy))
                Dim swhclr = tmpbmp.GetPixel(ix, iy)
                If swhclr = System.Drawing.Color.FromArgb(255, 26, 26, 26) Then 组件改.SetPixel(ix, iy, 主色.GetPixel(0, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 27, 27, 27) Then 组件改.SetPixel(ix, iy, 主色.GetPixel(0, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 46, 46, 46) Then 组件改.SetPixel(ix, iy, 主色.GetPixel(1, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 66, 66, 66) Then 组件改.SetPixel(ix, iy, 主色.GetPixel(2, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 87, 87, 87) Then 组件改.SetPixel(ix, iy, 主色.GetPixel(3, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 107, 107, 107) Then 组件改.SetPixel(ix, iy, 主色.GetPixel(4, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 108, 108, 108) Then 组件改.SetPixel(ix, iy, 主色.GetPixel(4, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 128, 128, 128) Then 组件改.SetPixel(ix, iy, 主色.GetPixel(5, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 148, 148, 148) Then 组件改.SetPixel(ix, iy, 主色.GetPixel(6, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 168, 168, 168) Then 组件改.SetPixel(ix, iy, 主色.GetPixel(7, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 110, 150, 86) Then 组件改.SetPixel(ix, iy, 眼色.GetPixel(0, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 111, 150, 85) Then 组件改.SetPixel(ix, iy, 眼色.GetPixel(0, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 125, 171, 99) Then 组件改.SetPixel(ix, iy, 眼色.GetPixel(1, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 140, 191, 111) Then 组件改.SetPixel(ix, iy, 眼色.GetPixel(2, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 140, 192, 111) Then 组件改.SetPixel(ix, iy, 眼色.GetPixel(2, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 157, 212, 125) Then 组件改.SetPixel(ix, iy, 眼色.GetPixel(3, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 158, 212, 125) Then 组件改.SetPixel(ix, iy, 眼色.GetPixel(3, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 172, 232, 137) Then 组件改.SetPixel(ix, iy, 眼色.GetPixel(4, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 172, 231, 137) Then 组件改.SetPixel(ix, iy, 眼色.GetPixel(4, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 188, 252, 151) Then 组件改.SetPixel(ix, iy, 眼色.GetPixel(5, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 189, 252, 150) Then 组件改.SetPixel(ix, iy, 眼色.GetPixel(5, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 15, 0, 27) Then 组件改.SetPixel(ix, iy, 复色1.GetPixel(0, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 27, 0, 46) Then 组件改.SetPixel(ix, iy, 复色1.GetPixel(1, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 38, 0, 66) Then 组件改.SetPixel(ix, iy, 复色1.GetPixel(2, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 52, 0, 87) Then 组件改.SetPixel(ix, iy, 复色1.GetPixel(3, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 62, 0, 108) Then 组件改.SetPixel(ix, iy, 复色1.GetPixel(4, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 74, 0, 128) Then 组件改.SetPixel(ix, iy, 复色1.GetPixel(5, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 85, 0, 147) Then 组件改.SetPixel(ix, iy, 复色1.GetPixel(6, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 97, 0, 168) Then 组件改.SetPixel(ix, iy, 复色1.GetPixel(7, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 85, 116, 150) Then 组件改.SetPixel(ix, iy, 复色2.GetPixel(0, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 99, 134, 171) Then 组件改.SetPixel(ix, iy, 复色2.GetPixel(1, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 111, 150, 192) Then 组件改.SetPixel(ix, iy, 复色2.GetPixel(2, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 125, 167, 212) Then 组件改.SetPixel(ix, iy, 复色2.GetPixel(3, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 137, 183, 231) Then 组件改.SetPixel(ix, iy, 复色2.GetPixel(4, 0))
                If swhclr = System.Drawing.Color.FromArgb(255, 150, 200, 252) Then 组件改.SetPixel(ix, iy, 复色2.GetPixel(5, 0))

            Next
        Next
        DrawedImg.Source = BitmapToBitmapSource(组件改)
    End Sub
    Private Sub 置入1() Handles Draw_Mix.Click
        For iy = 0 To 组件源.Height - 1
            For ix = 0 To 组件源.Width - 1
                Dim tmpclr = 组件源.GetPixel(ix, iy)
                If tmpclr.A > 127 Then
                    临时皮肤文件.SetPixel(ix, iy, tmpclr)
                End If
            Next
        Next
        FinalImg.Source = BitmapToBitmapSource(临时皮肤文件)
    End Sub
    Private Sub 置入2() Handles Putin.Click
        For iy = 0 To 组件改.Height - 1
            For ix = 0 To 组件改.Width - 1
                Dim tmpclr = 组件改.GetPixel(ix, iy)
                If tmpclr.A > 127 Then
                    临时皮肤文件.SetPixel(ix, iy, tmpclr)
                End If
            Next
        Next
        FinalImg.Source = BitmapToBitmapSource(临时皮肤文件)
    End Sub
    Private Sub 应用修改() Handles Outpot.Click
        applychange = True
    End Sub
End Class
