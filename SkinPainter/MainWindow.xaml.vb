Imports System.Windows.Forms
Class MainWindow
    <System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")>
    Shared Function DeleteObject(ByVal hObject As IntPtr) As Boolean
    End Function
    '定义内存清除器
    Public Coloreditor As New Coloreditor
    Public PartDecider As New PartDecider
    Public Creator As New Creator
    Public SCMaker As New SCMaker
    Dim 皮肤文件 As New System.Drawing.Bitmap(64, 64)
    Dim mu = System.Drawing.Graphics.FromImage(Me.皮肤文件)
    Dim 正在绘图 As Boolean = False
    Dim 调色板 As New System.Drawing.Bitmap(12, 12)
    '定义图片文件，定义编辑器
    Public 画笔 As System.Drawing.Color = System.Drawing.Color.FromArgb（255， 0， 162， 232）
    Dim 橡皮 As System.Drawing.Color = System.Drawing.Color.Transparent
    '初始化画笔和橡皮
    Dim IsSlimed As Boolean = False 'False为Steve模型，True为Alex模型
    Dim 文件目录 As String = ""
    Dim ModerStstus As String
    '定义所有变量
    Dim seldot1 As Point = New Point(8, 0)
    Dim seldot2 As Point = New Point(15, 7)
    Dim plsdot1 As Point = New Point(8, 0)
    Dim clipboard As New System.Drawing.Bitmap(8, 8)
    '局部变换功能
    Dim pathfl As System.Drawing.Bitmap
    Dim pathsl As System.Drawing.Bitmap
    '部位编辑功能
    Dim dmX, dmY As Integer
    Private Sub 初始化() Handles Me.Loaded
        AddHandler Coloreditor.Apply.Click, AddressOf 结束颜色编辑
        AddHandler Coloreditor.Close.Click, AddressOf 结束颜色编辑
        AddHandler PartDecider.Close.Click, AddressOf 开启局部编辑
        AddHandler PartDecider.Enter.Click, AddressOf 开启局部编辑
        AddHandler Creator.Outpot.Click, AddressOf 结束组装模式
        AddHandler SCMaker.Close.Click, AddressOf 结束制作器
        AddHandler SCMaker.Outpot.Click, AddressOf 结束制作器
        Call VChange()
        aLL.Source = BitmapToBitmapSource(皮肤文件)
        ClipboardPreview.Source = BitmapToBitmapSource(clipboard)
        TmpC.Source = BitmapToBitmapSource(调色板)
    End Sub
    Private Sub 移动窗口() Handles TopBar.MouseLeftButtonDown
        Me.DragMove()
    End Sub
    Private Sub 关闭() Handles Close.Click
        End
    End Sub
    Private Sub 最小化() Handles Mini.Click
        Me.WindowState = WindowState.Minimized
    End Sub
    Private Sub 正式画图() Handles aLL.MouseLeftButtonDown
        Dim p As Point = Mouse.GetPosition(aLL)
        Dim x, y As Single
        x = Int((p.X * 皮肤文件.Width) / aLL.Width)
        y = Int((p.Y * 皮肤文件.Height) / aLL.Height)
        dmX = x
        dmY = y
        Select Case Moder.Value
            Case 0
                皮肤文件.SetPixel(x, y, 画笔)
                aLL.Source = BitmapToBitmapSource(皮肤文件)
            Case 1
                画笔 = 皮肤文件.GetPixel(x, y)
                ColorNow.Fill = New SolidColorBrush(Media.Color.FromArgb(画笔.A, 画笔.R, 画笔.G, 画笔.B))
            Case 2
                皮肤文件.SetPixel(x, y, 橡皮)
                aLL.Source = BitmapToBitmapSource(皮肤文件)
        End Select
        正在绘图 = True
    End Sub
    Private Sub 连续画图() Handles aLL.MouseMove
        If 正在绘图 = True Then
            Dim p As Point = Mouse.GetPosition(aLL)
            Dim x, y As Single
            x = Int((p.X * 皮肤文件.Width) / aLL.Width)
            y = Int((p.Y * 皮肤文件.Height) / aLL.Height)
            If x > 0 And x < 皮肤文件.Width And y > 0 And y < 皮肤文件.Height Then
                If dmX <> x Or dmY <> y Then
                    Select Case Moder.Value
                        Case 0
                            皮肤文件.SetPixel(x, y, 画笔)
                            aLL.Source = BitmapToBitmapSource(皮肤文件)
                        Case 1
                            画笔 = 皮肤文件.GetPixel(x, y)
                            ColorNow.Fill = New SolidColorBrush(Media.Color.FromArgb(画笔.A, 画笔.R, 画笔.G, 画笔.B))
                        Case 2
                            皮肤文件.SetPixel(x, y, 橡皮)
                            aLL.Source = BitmapToBitmapSource(皮肤文件)
                    End Select
                    dmX = x
                    dmY = y
                End If
            Else
                Call 结束画图()
            End If
        End If
    End Sub
    Private Sub 结束画图() Handles aLL.MouseUp
        正在绘图 = False
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
        画笔 = MediaSoildBrushtoGDIC(AddColor.Background)
        ColorNow.Fill = AddColor.Background
    End Sub
    Private Sub 主画面取色() Handles aLL.MouseRightButtonDown
        ModerStstus = Moder.Value
        Moder.Value = 1
        Dim p As Point = Mouse.GetPosition(aLL)
        Dim x, y As Single
        x = Int((p.X * 皮肤文件.Width) / aLL.Width)
        y = Int((p.Y * 皮肤文件.Height) / aLL.Height)
        If x > 0 And x < 皮肤文件.Width And y > 0 And y < 皮肤文件.Height Then
            画笔 = 皮肤文件.GetPixel(x, y)
            ColorNow.Fill = New SolidColorBrush(Media.Color.FromArgb(画笔.A, 画笔.R, 画笔.G, 画笔.B))
        Else
            Call 取色结束()
        End If
    End Sub
    Private Sub 取色结束() Handles aLL.MouseRightButtonUp
        Moder.Value = ModerStstus
    End Sub
    Private Sub 打开文件() Handles Open.Click
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.Filter = "PNG图像文件 (*.png)|*.png"
        openFileDialog1.FilterIndex = 1
        openFileDialog1.Title = "打开皮肤文件"
        openFileDialog1.RestoreDirectory = False
        openFileDialog1.ShowDialog()

        If Right(openFileDialog1.FileName, 4) = “.PNG” Or Right(openFileDialog1.FileName, 4) = “.png” Then
            Dim fs As System.IO.FileStream = Nothing
            fs = New System.IO.FileStream(openFileDialog1.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read)
            Dim tmpbmp As System.Drawing.Bitmap
            tmpbmp = System.Drawing.Bitmap.FromStream(fs)
            fs.Close()

            If tmpbmp.Height = 64 And tmpbmp.Width = 64 Then
                皮肤文件 = tmpbmp
                Dim mu = System.Drawing.Graphics.FromImage(Me.皮肤文件)
                aLL.Source = BitmapToBitmapSource(皮肤文件)
                文件目录 = openFileDialog1.FileName
            Else
                MessageBox.Show("皮肤格式大小错误: " & tmpbmp.Size.ToString)
            End If
        End If
    End Sub
    Private Sub 保存() Handles SaveAway.Click
        Dim saveFileDialog1 As New SaveFileDialog()
        saveFileDialog1.Filter = "PNG图像文件 (*.png)|*.png"
        saveFileDialog1.Title = "保存为图片文件"
        saveFileDialog1.ShowDialog()
        If saveFileDialog1.FileName <> "" Then
            皮肤文件.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png)
        End If
    End Sub
    Private Sub 更换模型() Handles SkinMode.Click
        IsSlimed = SkinMode.IsChecked
        Dim uriSource As Uri
        If IsSlimed = True Then
            ModelName.Content = "模型(Alex)"
            uriSource = New Uri("pack://application:,,,/SkinPainter;component/Alex.JPG")
        Else
            ModelName.Content = "模型(Steve)"
            uriSource = New Uri("pack://application:,,,/SkinPainter;component/Steve.JPG")
        End If
        Dim decoder As New JpegBitmapDecoder(uriSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default)
        Dim bitmapSource As BitmapSource = decoder.Frames(0)
        Model.Source = bitmapSource
    End Sub
    Private Sub 修改背景() Handles BackgroundSelecterr.ValueChanged
        Select Case BackgroundSelecterr.Value
            Case 0
                Call 更换模型()
                aLL.Opacity = 1
                aLL_Shadow.Opacity = 0.6
            Case 1
                Call 更换模型()
                aLL.Opacity = 0.5
                aLL_Shadow.Opacity = 0
            Case 2
                aLL.Opacity = 1
                Dim a As New System.Drawing.Bitmap(1, 1)
                a.SetPixel(0, 0, System.Drawing.Color.White）
                Model.Source = BitmapToBitmapSource(a)
                aLL_Shadow.Opacity = 0.6
        End Select
    End Sub
    Private Sub 编辑当前颜色() Handles ColorNow.MouseDown
        Coloreditor.subawake = False
        Coloreditor.Show()
        Coloreditor.changeapply = False
        Disedited.Visibility = Visibility.Visible
        Coloreditor.R.Text = MediaSoildBrushtoGDIC(ColorNow.Fill).R
        Coloreditor.G.Text = MediaSoildBrushtoGDIC(ColorNow.Fill).G
        Coloreditor.B.Text = MediaSoildBrushtoGDIC(ColorNow.Fill).B
        Coloreditor.A.Text = MediaSoildBrushtoGDIC(ColorNow.Fill).A
        Dim hsv = RGBtoHSV(MediaSoildBrushtoGDIC(ColorNow.Fill))
        Coloreditor.H.Text = FormatHue(hsv(0))
        Coloreditor.S.Text = hsv（1）
        Coloreditor.V.Text = hsv（2）
        Coloreditor.PreView.Background = ColorNow.Fill
    End Sub
    Private Sub 结束颜色编辑()
        If Coloreditor.changeapply = True Then
            If Coloreditor.subawake = False Then
                画笔 = MediaSoildBrushtoGDIC(Coloreditor.PreView.Background)
                ColorNow.Fill = Coloreditor.PreView.Background
            End If
        End If
        Disedited.Visibility = Visibility.Collapsed
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
                画笔 = System.Drawing.Color.FromArgb(tmprgb.A, tmprgb.R, tmprgb.G, tmprgb.B)
            Else
                MsgBox("饱和度或亮度已达到极限，请点击当前色：手动调整或解除限制")
            End If
            '===================================================================================================
        Else
            '直接取极值
            Dim tmprgb = HSVtoRGB(MediaSoildBrushtoGDIC(ColorNow.Fill).A, htmp, Math.Min(Math.Max(stmp, 0), 100), Math.Min(Math.Max(vtmp, 0), 100))
            ColorNow.Fill = New SolidColorBrush(Media.Color.FromArgb(tmprgb.A, tmprgb.R, tmprgb.G, tmprgb.B))
            画笔 = System.Drawing.Color.FromArgb(tmprgb.A, tmprgb.R, tmprgb.G, tmprgb.B)
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
                画笔 = System.Drawing.Color.FromArgb(tmprgb.A, tmprgb.R, tmprgb.G, tmprgb.B)
            Else
                MsgBox("饱和度或亮度已达到极限，请点击当前色：手动调整或解除限制")
            End If
            '===================================================================================================
        Else
            '直接取极值
            Dim tmprgb = HSVtoRGB(MediaSoildBrushtoGDIC(ColorNow.Fill).A, htmp, Math.Min(Math.Max(stmp, 0), 100), Math.Min(Math.Max(vtmp, 0), 100))
            ColorNow.Fill = New SolidColorBrush(Media.Color.FromArgb(tmprgb.A, tmprgb.R, tmprgb.G, tmprgb.B))
            画笔 = System.Drawing.Color.FromArgb(tmprgb.A, tmprgb.R, tmprgb.G, tmprgb.B)
        End If
    End Sub
    Private Sub 选择模式() Handles SelectorSwitch.Click
        If SelectorSwitch.IsChecked = True Then
            Selector.Visibility = Visibility.Visible
            SelectorBar.Visibility = Visibility.Visible
            Dim tmpx As Integer = Math.Abs(seldot1.X - seldot2.X) + 1
            Dim tmpy As Integer = Math.Abs(seldot1.X - seldot2.X) + 1
            Dim tmpbmp = New System.Drawing.Bitmap(tmpx, tmpy)
            Dim normalback As New System.Drawing.Bitmap(64, 64)
            Dim mi = System.Drawing.Graphics.FromImage(normalback)
            mi.Clear(System.Drawing.Color.FromArgb(160, 0, 0, 0))
            tmpbmp = 粘贴(New Point(Math.Min(seldot1.X， seldot2.X), Math.Min(seldot1.Y， seldot2.Y)), tmpbmp, normalback)
            tmpbmp.SetPixel(seldot1.X, seldot1.Y, System.Drawing.Color.FromArgb(160, 0, 0, 255))
            tmpbmp.SetPixel(seldot2.X, seldot2.Y, System.Drawing.Color.FromArgb(160, 255, 0, 0))
            Selector.Source = BitmapToBitmapSource(tmpbmp)
        Else
            Selector.Visibility = Visibility.Hidden
            SelectorBar.Visibility = Visibility.Collapsed
        End If
    End Sub
    Private Sub 设置点一() Handles Selector.MouseLeftButtonDown
        Dim p As Point = Mouse.GetPosition(Selector)
        Dim x, y As Single
        x = Int((p.X * 皮肤文件.Width) / Selector.Width)
        y = Int((p.Y * 皮肤文件.Height) / Selector.Height)
        If PasteMode.Visibility = Visibility.Visible Then
            '此处写粘贴程序
            plsdot1 = New Point(x, y)
            Dim normalback As New System.Drawing.Bitmap(64, 64)
            Dim mi = System.Drawing.Graphics.FromImage(normalback)
            mi.Clear(System.Drawing.Color.FromArgb(160, 0, 0, 0))
            Selector.Source = BitmapToBitmapSource(粘贴(plsdot1, clipboard, normalback))
        Else
            seldot1 = New Point(x, y)
            Dim tmpx As Integer = Math.Abs(seldot1.X - seldot2.X) + 1
            Dim tmpy As Integer = Math.Abs(seldot1.Y - seldot2.Y) + 1
            Dim normalback As New System.Drawing.Bitmap(64, 64)
            Dim mi = System.Drawing.Graphics.FromImage(normalback)
            mi.Clear(System.Drawing.Color.FromArgb(160, 0, 0, 0))
            Dim tmpbmp = New System.Drawing.Bitmap(tmpx, tmpy)
            tmpbmp = 粘贴(New Point(Math.Min(seldot1.X， seldot2.X), Math.Min(seldot1.Y， seldot2.Y)), tmpbmp, normalback)
            tmpbmp.SetPixel(seldot1.X, seldot1.Y, System.Drawing.Color.FromArgb(160, 0, 0, 255))
            tmpbmp.SetPixel(seldot2.X, seldot2.Y, System.Drawing.Color.FromArgb(160, 255, 0, 0))
            Selector.Source = BitmapToBitmapSource(tmpbmp)
        End If
    End Sub
    Private Sub 设置点二() Handles Selector.MouseRightButtonDown
        Dim p As Point = Mouse.GetPosition(Selector)
        Dim x, y As Single
        x = Int((p.X * 皮肤文件.Width) / Selector.Width)
        y = Int((p.Y * 皮肤文件.Height) / Selector.Height)
        seldot2 = New Point(x, y)
        If PasteMode.Visibility = Visibility.Visible Then
            plsdot1 = New Point(x - clipboard.Width + 1, y - clipboard.Height + 1)
            Dim normalback As New System.Drawing.Bitmap(64, 64)
            Dim mi = System.Drawing.Graphics.FromImage(normalback)
            mi.Clear(System.Drawing.Color.FromArgb(160, 0, 0, 0))
            Selector.Source = BitmapToBitmapSource(粘贴(plsdot1, clipboard, normalback))
            '此处写粘贴程序
        Else
            Dim tmpx As Integer = Math.Abs(seldot1.X - seldot2.X) + 1
            Dim tmpy As Integer = Math.Abs(seldot1.Y - seldot2.Y) + 1
            Dim normalback As New System.Drawing.Bitmap(64, 64)
            Dim mi = System.Drawing.Graphics.FromImage(normalback)
            mi.Clear(System.Drawing.Color.FromArgb(160, 0, 0, 0))
            Dim tmpbmp = New System.Drawing.Bitmap(tmpx, tmpy)
            tmpbmp = 粘贴(New Point(Math.Min(seldot1.X， seldot2.X), Math.Min(seldot1.Y， seldot2.Y)), tmpbmp, normalback)
            tmpbmp.SetPixel(seldot1.X, seldot1.Y, System.Drawing.Color.FromArgb(160, 0, 0, 255))
            tmpbmp.SetPixel(seldot2.X, seldot2.Y, System.Drawing.Color.FromArgb(160, 255, 0, 0))
            Selector.Source = BitmapToBitmapSource(tmpbmp)
        End If
    End Sub
    Private Sub 垂直翻转() Handles ScaleX.Click
        Dim tmpbmp = 剪切图片(皮肤文件, seldot1, seldot2)
        tmpbmp.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipY)
        皮肤文件 = 粘贴(New Point(Math.Min(seldot1.X， seldot2.X), Math.Min(seldot1.Y， seldot2.Y)), tmpbmp, 皮肤文件)
        aLL.Source = BitmapToBitmapSource(皮肤文件)
    End Sub
    Private Sub 水平翻转() Handles ScaleY.Click
        Dim tmpbmp = 剪切图片(皮肤文件, seldot1, seldot2)
        tmpbmp.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipX)
        皮肤文件 = 粘贴(New Point(Math.Min(seldot1.X， seldot2.X), Math.Min(seldot1.Y， seldot2.Y)), tmpbmp, 皮肤文件)
        aLL.Source = BitmapToBitmapSource(皮肤文件)
    End Sub
    Private Sub 填充() Handles Fill.Click
        Dim tmpx As Integer = Math.Abs(seldot1.X - seldot2.X) + 1
        Dim tmpy As Integer = Math.Abs(seldot1.Y - seldot2.Y) + 1
        Dim tmpbmp = New System.Drawing.Bitmap(tmpx, tmpy)
        Dim ma = System.Drawing.Graphics.FromImage(tmpbmp)
        ma.Clear(画笔)
        皮肤文件 = 粘贴(New Point(Math.Min(seldot1.X， seldot2.X), Math.Min(seldot1.Y， seldot2.Y)), tmpbmp, 皮肤文件)
        aLL.Source = BitmapToBitmapSource(皮肤文件)
    End Sub
    Private Sub 清空() Handles Clear.Click
        Dim tmpx As Integer = Math.Abs(seldot1.X - seldot2.X) + 1
        Dim tmpy As Integer = Math.Abs(seldot1.Y - seldot2.Y) + 1
        Dim tmpbmp = New System.Drawing.Bitmap(tmpx, tmpy)
        Dim ma = System.Drawing.Graphics.FromImage(tmpbmp)
        ma.Clear(橡皮)
        皮肤文件 = 粘贴(New Point(Math.Min(seldot1.X， seldot2.X), Math.Min(seldot1.Y， seldot2.Y)), tmpbmp, 皮肤文件)
        aLL.Source = BitmapToBitmapSource(皮肤文件)
    End Sub
    Private Sub 复制到剪贴板() Handles Copy.Click
        clipboard = 剪切图片(皮肤文件, seldot1, seldot2)
        ClipboardPreview.Source = BitmapToBitmapSource(clipboard)
    End Sub
    Private Sub 从剪切板粘贴() Handles Paste.Click
        PasteMode.Visibility = Visibility.Visible
        Dim normalback As New System.Drawing.Bitmap(64, 64)
        Dim mi = System.Drawing.Graphics.FromImage(normalback)
        mi.Clear(System.Drawing.Color.FromArgb(160, 0, 0, 0))
        Selector.Source = BitmapToBitmapSource(粘贴(plsdot1, clipboard, normalback))
    End Sub
    Private Sub 确定粘贴() Handles PasteOK.Click
        皮肤文件 = 粘贴(plsdot1, clipboard, 皮肤文件)
        aLL.Source = BitmapToBitmapSource(皮肤文件)
        PasteMode.Visibility = Visibility.Collapsed
        Dim tmpx As Integer = Math.Abs(seldot1.X - seldot2.X) + 1
        Dim tmpy As Integer = Math.Abs(seldot1.Y - seldot2.Y) + 1
        Dim normalback As New System.Drawing.Bitmap(64, 64)
        Dim mi = System.Drawing.Graphics.FromImage(normalback)
        mi.Clear(System.Drawing.Color.FromArgb(160, 0, 0, 0))
        Dim tmpbmp = New System.Drawing.Bitmap(tmpx, tmpy)
        Selector.Source = BitmapToBitmapSource(粘贴(New Point(Math.Min(seldot1.X， seldot2.X), Math.Min(seldot1.Y， seldot2.Y)), tmpbmp, normalback))
    End Sub
    Private Sub 撤销粘贴() Handles PasteCencel.Click
        PasteMode.Visibility = Visibility.Collapsed
        Dim tmpx As Integer = Math.Abs(seldot1.X - seldot2.X) + 1
        Dim tmpy As Integer = Math.Abs(seldot1.Y - seldot2.Y) + 1
        Dim normalback As New System.Drawing.Bitmap(64, 64)
        Dim mi = System.Drawing.Graphics.FromImage(normalback)
        mi.Clear(System.Drawing.Color.FromArgb(160, 0, 0, 0))
        Dim tmpbmp = New System.Drawing.Bitmap(tmpx, tmpy)
        Selector.Source = BitmapToBitmapSource(粘贴(New Point(Math.Min(seldot1.X， seldot2.X), Math.Min(seldot1.Y， seldot2.Y)), tmpbmp, normalback))
    End Sub
    Private Sub 测试() Handles 测试按钮.Click
        MsgBox(PathSizeQuery(1212, True, "x").ToString)
        MsgBox(PathSizeQuery(1212, True, "y").ToString)
        MsgBox(PathSizeQuery(1212, True, "z").ToString)
    End Sub

    '================================以下为局部绘图功能==================================
    Private Sub 开启局部编辑()
        If PartDecider.changeapply Then
            Dim tmpdot1 = PathFaceLeftTopQuery(PartDecider.PathStatus, PartDecider.FaceStatus, SkinMode.IsChecked, False)
            Dim tmpdot2 = PathFaceRightBottomQuery(PartDecider.PathStatus, PartDecider.FaceStatus, SkinMode.IsChecked, False)
            pathfl = 剪切图片(皮肤文件, tmpdot1, tmpdot2)
            tmpdot1 = PathFaceLeftTopQuery(PartDecider.PathStatus, PartDecider.FaceStatus, SkinMode.IsChecked, True)
            tmpdot2 = PathFaceRightBottomQuery(PartDecider.PathStatus, PartDecider.FaceStatus, SkinMode.IsChecked, True)
            pathsl = 剪切图片(皮肤文件, tmpdot1, tmpdot2)
            FirstLevel.Source = BitmapToBitmapSource(pathfl)
            SecondLevel.Source = BitmapToBitmapSource(pathsl)
            PaintPlace.Visibility = Visibility.Collapsed
            SkinMode.Visibility = Visibility.Collapsed
            LivelBar.Visibility = Visibility.Visible
            SelectorSwitch.Visibility = Visibility.Collapsed
            PathPaintPlace.Visibility = Visibility.Visible
        End If
        Disedited.Visibility = Visibility.Collapsed
    End Sub
    Private Sub 选择部位() Handles PathEnter.Click
        Disedited.Visibility = Visibility.Visible
        PartDecider.Show()
    End Sub
    '===========================第一层画图===========================
    Private Sub 第一层正式画图() Handles FirstLevel.MouseLeftButtonDown
        Dim p As Point = Mouse.GetPosition(FirstLevel)
        Dim x, y As Single
        x = Int((p.X * Math.Max(pathfl.Width, pathfl.Height) / 480))
        y = Int((p.Y * Math.Max(pathfl.Width, pathfl.Height) / 480))
        dmX = x
        dmY = y
        Select Case Moder.Value
            Case 0
                pathfl.SetPixel(x, y, 画笔)
                FirstLevel.Source = BitmapToBitmapSource(pathfl)
            Case 1
                画笔 = pathfl.GetPixel(x, y)
                ColorNow.Fill = New SolidColorBrush(Media.Color.FromArgb(画笔.A, 画笔.R, 画笔.G, 画笔.B))
            Case 2
                pathfl.SetPixel(x, y, 橡皮)
                FirstLevel.Source = BitmapToBitmapSource(pathfl)
        End Select
        正在绘图 = True
    End Sub
    Private Sub 第一层连续画图() Handles FirstLevel.MouseMove
        If 正在绘图 = True Then
            Dim p As Point = Mouse.GetPosition(FirstLevel)
            Dim x, y As Single
            x = Int((p.X * Math.Max(pathfl.Width, pathfl.Height) / 480))
            y = Int((p.Y * Math.Max(pathfl.Width, pathfl.Height) / 480))
            If x > 0 And x < pathfl.Width And y > 0 And y < pathfl.Height Then
                If dmX <> x Or dmY <> y Then
                    Select Case Moder.Value
                        Case 0
                            pathfl.SetPixel(x, y, 画笔)
                            FirstLevel.Source = BitmapToBitmapSource(pathfl)
                        Case 1
                            画笔 = pathfl.GetPixel(x, y)
                            ColorNow.Fill = New SolidColorBrush(Media.Color.FromArgb(画笔.A, 画笔.R, 画笔.G, 画笔.B))
                        Case 2
                            pathfl.SetPixel(x, y, 橡皮)
                            FirstLevel.Source = BitmapToBitmapSource(pathfl)
                    End Select
                    dmX = x
                    dmY = y
                End If
            Else
                    Call 第一层结束画图()
            End If
        End If
    End Sub
    Private Sub 第一层结束画图() Handles FirstLevel.MouseUp
        正在绘图 = False
    End Sub
    Private Sub 第一层取色() Handles FirstLevel.MouseRightButtonDown
        ModerStstus = Moder.Value
        Moder.Value = 1
        Dim p As Point = Mouse.GetPosition(FirstLevel)
        Dim x, y As Single
        x = Int((p.X * Math.Max(pathfl.Width, pathfl.Height) / 480))
        y = Int((p.Y * Math.Max(pathfl.Width, pathfl.Height) / 480))
        If x >= 0 And x < pathfl.Width And y >= 0 And y < pathfl.Height Then
            画笔 = pathfl.GetPixel(x, y)
            ColorNow.Fill = New SolidColorBrush(Media.Color.FromArgb(画笔.A, 画笔.R, 画笔.G, 画笔.B))
        Else
            Call 第一层取色结束()
        End If
    End Sub
    Private Sub 第一层取色结束() Handles FirstLevel.MouseRightButtonUp
        Moder.Value = ModerStstus
    End Sub
    Private Sub 第二层正式画图() Handles SecondLevel.MouseLeftButtonDown
        Dim p As Point = Mouse.GetPosition(SecondLevel)
        Dim x, y As Single
        x = Int((p.X * Math.Max(pathsl.Width, pathsl.Height) / 480))
        y = Int((p.Y * Math.Max(pathsl.Width, pathsl.Height) / 480))
        dmX = x
        dmY = y
        Select Case Moder.Value
            Case 0
                pathsl.SetPixel(x, y, 画笔)
                SecondLevel.Source = BitmapToBitmapSource(pathsl)
            Case 1
                画笔 = pathsl.GetPixel(x, y)
                ColorNow.Fill = New SolidColorBrush(Media.Color.FromArgb(画笔.A, 画笔.R, 画笔.G, 画笔.B))
            Case 2
                pathsl.SetPixel(x, y, 橡皮)
                SecondLevel.Source = BitmapToBitmapSource(pathsl)
        End Select
        正在绘图 = True
    End Sub
    Private Sub 第二层连续画图() Handles SecondLevel.MouseMove
        If 正在绘图 = True Then
            Dim p As Point = Mouse.GetPosition(SecondLevel)
            Dim x, y As Single
            x = Int((p.X * Math.Max(pathsl.Width, pathsl.Height) / 480))
            y = Int((p.Y * Math.Max(pathsl.Width, pathsl.Height) / 480))
            If x > 0 And x < pathsl.Width And y > 0 And y < pathsl.Height Then
                If dmX <> x Or dmY <> y Then
                    Select Case Moder.Value
                        Case 0
                            pathsl.SetPixel(x, y, 画笔)
                            SecondLevel.Source = BitmapToBitmapSource(pathsl)
                        Case 1
                            画笔 = pathsl.GetPixel(x, y)
                            ColorNow.Fill = New SolidColorBrush(Media.Color.FromArgb(画笔.A, 画笔.R, 画笔.G, 画笔.B))
                        Case 2
                            pathsl.SetPixel(x, y, 橡皮)
                            SecondLevel.Source = BitmapToBitmapSource(pathsl)
                    End Select
                    dmX = x
                    dmY = y
                End If
            Else
                    Call 第二层结束画图()
            End If
        End If
    End Sub
    Private Sub 第二层结束画图() Handles SecondLevel.MouseUp
        正在绘图 = False
    End Sub
    Private Sub 第二层取色() Handles SecondLevel.MouseRightButtonDown
        ModerStstus = Moder.Value
        Moder.Value = 1
        Dim p As Point = Mouse.GetPosition(SecondLevel)
        Dim x, y As Single
        x = Int((p.X * Math.Max(pathsl.Width, pathsl.Height) / 480))
        y = Int((p.Y * Math.Max(pathsl.Width, pathsl.Height) / 480))
        If x >= 0 And x < pathsl.Width And y >= 0 And y < pathsl.Height Then
            画笔 = pathsl.GetPixel(x, y)
            ColorNow.Fill = New SolidColorBrush(Media.Color.FromArgb(画笔.A, 画笔.R, 画笔.G, 画笔.B))
        Else
            Call 第二层取色结束()
        End If
    End Sub
    Private Sub 第二层取色结束() Handles SecondLevel.MouseRightButtonUp
        Moder.Value = ModerStstus
    End Sub
    Private Sub 开关第二层() Handles LevelSwitch.Click
        If LevelSwitch.IsChecked = True Then
            SecondLevel.Visibility = Visibility.Visible
        Else
            SecondLevel.Visibility = Visibility.Hidden
        End If
    End Sub
    Private Sub 关闭局部() Handles ClosePath.Click
        PaintPlace.Visibility = Visibility.Visible
        SkinMode.Visibility = Visibility.Visible
        LivelBar.Visibility = Visibility.Collapsed
        SelectorSwitch.Visibility = Visibility.Visible
        PathPaintPlace.Visibility = Visibility.Collapsed
    End Sub
    Private Sub 保存局部() Handles OKPath.Click
        Dim tmpdot1 = PathFaceLeftTopQuery(PartDecider.PathStatus, PartDecider.FaceStatus, SkinMode.IsChecked, False)
        Dim tmpdot2 = PathFaceLeftTopQuery(PartDecider.PathStatus, PartDecider.FaceStatus, SkinMode.IsChecked, True)
        皮肤文件 = 粘贴(tmpdot1, pathfl, 皮肤文件)
        皮肤文件 = 粘贴(tmpdot2, pathsl, 皮肤文件)
        aLL.Source = BitmapToBitmapSource(皮肤文件)
        PaintPlace.Visibility = Visibility.Visible
        SkinMode.Visibility = Visibility.Visible
        LivelBar.Visibility = Visibility.Collapsed
        SelectorSwitch.Visibility = Visibility.Visible
        PathPaintPlace.Visibility = Visibility.Collapsed
    End Sub
    Private Sub 局部变换X() Handles ScaleXPath.Click
        If LevelSwitch.IsChecked = True Then
            pathsl.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipY)
            SecondLevel.Source = BitmapToBitmapSource(pathsl)
        Else
            pathfl.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipY)
            FirstLevel.Source = BitmapToBitmapSource(pathfl)
        End If
    End Sub
    Private Sub 局部变换Y() Handles ScaleYPath.Click
        If LevelSwitch.IsChecked = True Then
            pathsl.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipX)
            SecondLevel.Source = BitmapToBitmapSource(pathsl)
        Else
            pathfl.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipX)
            FirstLevel.Source = BitmapToBitmapSource(pathfl)
        End If
    End Sub
    Private Sub 局部填充() Handles FillPath.Click
        If LevelSwitch.IsChecked = True Then
            Dim ma = System.Drawing.Graphics.FromImage(pathsl)
            ma.Clear(画笔)
            SecondLevel.Source = BitmapToBitmapSource(pathsl)
        Else
            Dim ma = System.Drawing.Graphics.FromImage(pathfl)
            ma.Clear(画笔)
            FirstLevel.Source = BitmapToBitmapSource(pathfl)
        End If
    End Sub
    Private Sub 局部清空() Handles ClearPath.Click
        If LevelSwitch.IsChecked = True Then
            Dim ma = System.Drawing.Graphics.FromImage(pathsl)
            ma.Clear(橡皮)
            SecondLevel.Source = BitmapToBitmapSource(pathsl)
        Else
            Dim ma = System.Drawing.Graphics.FromImage(pathfl)
            ma.Clear(橡皮)
            FirstLevel.Source = BitmapToBitmapSource(pathfl)
        End If
    End Sub
    Private Sub 局部复制() Handles CopyPath.Click
        If LevelSwitch.IsChecked = True Then
            clipboard = pathsl
        Else
            clipboard = pathfl
        End If
        ClipboardPreview.Source = BitmapToBitmapSource(clipboard)
    End Sub
    Private Sub 局部粘贴() Handles PastePath.Click
        If LevelSwitch.IsChecked = True Then
            pathsl = 粘贴(New Point(0, 0), clipboard, pathsl)
            SecondLevel.Source = BitmapToBitmapSource(pathsl)
        Else
            pathfl = 粘贴(New Point(0, 0), clipboard, pathfl)
            FirstLevel.Source = BitmapToBitmapSource(pathfl)
        End If
    End Sub
    Private Sub 撤销() Handles Me.KeyDown
    End Sub

    Private Sub 结束组装模式()
        If Creator.applychange = True Then
            aLL.Source = BitmapToBitmapSource(皮肤文件)
        End If
        调色板 = Creator.调色板
        TmpC.Source = BitmapToBitmapSource(调色板)
        Creator.Hide()
        Disedited.Visibility = Visibility.Collapsed
        ColorNow.Fill = Creator.ColorNow.Fill
        画笔 = Creator.区域画笔
    End Sub
    Private Sub 开启组装模式() Handles CreatorEnter.Click
        Creator.ColorNow.Fill = ColorNow.Fill
        Creator.区域画笔 = 画笔
        Creator.临时皮肤文件 = 皮肤文件
        Creator.FinalImg.Source = BitmapToBitmapSource(Creator.临时皮肤文件)
        Creator.调色板 = 调色板
        Creator.TmpC.Source = BitmapToBitmapSource(调色板)
        Disedited.Visibility = Visibility.Visible
        Creator.Model.Source = Model.Source
        Creator.Show()
    End Sub
    Private Sub 放置颜色() Handles TmpC.MouseLeftButtonDown
        Dim p As Point = Mouse.GetPosition(TmpC)
        Dim x, y As Single
        x = Int((p.X * 调色板.Width) / TmpC.Width)
        y = Int((p.Y * 调色板.Height) / TmpC.Height)
        调色板.SetPixel(x, y, 画笔)
        TmpC.Source = BitmapToBitmapSource(调色板)
    End Sub
    Private Sub 取得颜色() Handles TmpC.MouseRightButtonDown
        Dim p As Point = Mouse.GetPosition(TmpC)
        Dim x, y As Single
        x = Int((p.X * 调色板.Width) / TmpC.Width)
        y = Int((p.Y * 调色板.Height) / TmpC.Height)
        画笔 = 调色板.GetPixel(x, y)
        ColorNow.Fill = New SolidColorBrush(Media.Color.FromArgb(画笔.A, 画笔.R, 画笔.G, 画笔.B))
    End Sub
    Private Sub 关于() Handles AboutMaker.MouseLeftButtonUp
        Dim AboutMe As New AboutMe
        AboutMe.Show()
    End Sub
    Private Sub 结束制作器()
        If SCMaker.applychange = True Then
            皮肤文件 = SCMaker.临时皮肤文件.Clone
            aLL.Source = BitmapToBitmapSource(皮肤文件)
        End If
        调色板 = SCMaker.调色板
        TmpC.Source = BitmapToBitmapSource(调色板)
        SCMaker.Hide()
        Disedited.Visibility = Visibility.Collapsed
        ColorNow.Fill = SCMaker.ColorNow.Fill
        画笔 = SCMaker.区域画笔
    End Sub
    Private Sub 开启制作器() Handles CreatorMaker.Click
        SCMaker.ColorNow.Fill = ColorNow.Fill
        SCMaker.区域画笔 = 画笔
        SCMaker.临时源皮肤 = 皮肤文件
        SCMaker.OriImg.Source = BitmapToBitmapSource(皮肤文件)
        SCMaker.调色板 = 调色板
        SCMaker.TmpC.Source = BitmapToBitmapSource(调色板)
        Disedited.Visibility = Visibility.Visible
        SCMaker.Model.Source = Model.Source
        SCMaker.Show()
    End Sub
End Class