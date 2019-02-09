Public Class SCMaker
    Public applychange As Boolean = False
    Dim 主色 As New System.Drawing.Bitmap(8, 1)
    Dim 复色1 As New System.Drawing.Bitmap(8, 1)
    Dim 复色2 As New System.Drawing.Bitmap(6, 1)
    Dim 眼色 As New System.Drawing.Bitmap(6, 1)
    Public 临时源皮肤 As New System.Drawing.Bitmap(64, 64)
    Public 临时皮肤文件 As New System.Drawing.Bitmap(64, 64)
    Public 调色板 As New System.Drawing.Bitmap(12, 12)
    Public 区域画笔 As System.Drawing.Color
    Dim Coloreditor As New Coloreditor
    '皮肤定义完成
    Private Sub 启动() Handles Me.Loaded
        EycC.Source = BitmapToBitmapSource(眼色)
        MainC.Source = BitmapToBitmapSource(主色)
        SubC.Source = BitmapToBitmapSource(复色1)
        SecC.Source = BitmapToBitmapSource(复色2)
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
    Private Sub 从皮肤取得颜色() Handles OriImg.MouseRightButtonDown
        Dim p As Point = Mouse.GetPosition(OriImg)
        Dim x, y As Single
        x = Int((p.X * 临时源皮肤.Width) / OriImg.Width)
        y = Int((p.Y * 临时源皮肤.Height) / OriImg.Height)
        区域画笔 = 临时源皮肤.GetPixel(x, y)
        ColorNow.Fill = New SolidColorBrush(Media.Color.FromArgb(区域画笔.A, 区域画笔.R, 区域画笔.G, 区域画笔.B))
    End Sub
    Private Sub 应用修改() Handles Outpot.Click
        applychange = True
    End Sub
    Private Sub 不应用修改() Handles Close.Click
        applychange = False
    End Sub
    Private Sub 生成() Handles Process.Click
        Dim ma = System.Drawing.Graphics.FromImage(临时皮肤文件)
        ma.Clear(System.Drawing.Color.Transparent)
        For iy = 0 To 临时源皮肤.Height - 1
            For ix = 0 To 临时源皮肤.Width - 1
                Dim swhclr = 临时源皮肤.GetPixel(ix, iy)
                If swhclr.A > 0 Then
                    If swhclr = 主色.GetPixel(0, 0) Then 临时皮肤文件.SetPixel(ix, iy, System.Drawing.Color.FromArgb(255, 26, 26, 26))
                    If swhclr = 主色.GetPixel(1, 0) Then 临时皮肤文件.SetPixel(ix, iy, System.Drawing.Color.FromArgb(255, 46, 46, 46))
                    If swhclr = 主色.GetPixel(2, 0) Then 临时皮肤文件.SetPixel(ix, iy, System.Drawing.Color.FromArgb(255, 66, 66, 66))
                    If swhclr = 主色.GetPixel(3, 0) Then 临时皮肤文件.SetPixel(ix, iy, System.Drawing.Color.FromArgb(255, 87, 87, 87))
                    If swhclr = 主色.GetPixel(4, 0) Then 临时皮肤文件.SetPixel(ix, iy, System.Drawing.Color.FromArgb(255, 107, 107, 107))
                    If swhclr = 主色.GetPixel(5, 0) Then 临时皮肤文件.SetPixel(ix, iy, System.Drawing.Color.FromArgb(255, 128, 128, 128))
                    If swhclr = 主色.GetPixel(6, 0) Then 临时皮肤文件.SetPixel(ix, iy, System.Drawing.Color.FromArgb(255, 148, 148, 148))
                    If swhclr = 主色.GetPixel(7, 0) Then 临时皮肤文件.SetPixel(ix, iy, System.Drawing.Color.FromArgb(255, 168, 168, 168))
                    If swhclr = 眼色.GetPixel(0, 0) Then 临时皮肤文件.SetPixel(ix, iy, System.Drawing.Color.FromArgb(255, 111, 150, 85))
                    If swhclr = 眼色.GetPixel(1, 0) Then 临时皮肤文件.SetPixel(ix, iy, System.Drawing.Color.FromArgb(255, 125, 171, 99))
                    If swhclr = 眼色.GetPixel(2, 0) Then 临时皮肤文件.SetPixel(ix, iy, System.Drawing.Color.FromArgb(255, 140, 192, 111))
                    If swhclr = 眼色.GetPixel(3, 0) Then 临时皮肤文件.SetPixel(ix, iy, System.Drawing.Color.FromArgb(255, 158, 212, 125))
                    If swhclr = 眼色.GetPixel(4, 0) Then 临时皮肤文件.SetPixel(ix, iy, System.Drawing.Color.FromArgb(255, 172, 231, 137))
                    If swhclr = 眼色.GetPixel(5, 0) Then 临时皮肤文件.SetPixel(ix, iy, System.Drawing.Color.FromArgb(255, 189, 252, 150))
                    If swhclr = 复色1.GetPixel(0, 0) Then 临时皮肤文件.SetPixel(ix, iy, System.Drawing.Color.FromArgb(255, 15, 0, 27))
                    If swhclr = 复色1.GetPixel(1, 0) Then 临时皮肤文件.SetPixel(ix, iy, System.Drawing.Color.FromArgb(255, 27, 0, 46))
                    If swhclr = 复色1.GetPixel(2, 0) Then 临时皮肤文件.SetPixel(ix, iy, System.Drawing.Color.FromArgb(255, 38, 0, 66))
                    If swhclr = 复色1.GetPixel(3, 0) Then 临时皮肤文件.SetPixel(ix, iy, System.Drawing.Color.FromArgb(255, 52, 0, 87))
                    If swhclr = 复色1.GetPixel(4, 0) Then 临时皮肤文件.SetPixel(ix, iy, System.Drawing.Color.FromArgb(255, 62, 0, 108))
                    If swhclr = 复色1.GetPixel(5, 0) Then 临时皮肤文件.SetPixel(ix, iy, System.Drawing.Color.FromArgb(255, 74, 0, 128))
                    If swhclr = 复色1.GetPixel(6, 0) Then 临时皮肤文件.SetPixel(ix, iy, System.Drawing.Color.FromArgb(255, 85, 0, 147))
                    If swhclr = 复色1.GetPixel(7, 0) Then 临时皮肤文件.SetPixel(ix, iy, System.Drawing.Color.FromArgb(255, 97, 0, 168))
                    If swhclr = 复色2.GetPixel(0, 0) Then 临时皮肤文件.SetPixel(ix, iy, System.Drawing.Color.FromArgb(255, 85, 116, 150))
                    If swhclr = 复色2.GetPixel(1, 0) Then 临时皮肤文件.SetPixel(ix, iy, System.Drawing.Color.FromArgb(255, 99, 134, 171))
                    If swhclr = 复色2.GetPixel(2, 0) Then 临时皮肤文件.SetPixel(ix, iy, System.Drawing.Color.FromArgb(255, 111, 150, 192))
                    If swhclr = 复色2.GetPixel(3, 0) Then 临时皮肤文件.SetPixel(ix, iy, System.Drawing.Color.FromArgb(255, 125, 167, 212))
                    If swhclr = 复色2.GetPixel(4, 0) Then 临时皮肤文件.SetPixel(ix, iy, System.Drawing.Color.FromArgb(255, 137, 183, 231))
                    If swhclr = 复色2.GetPixel(5, 0) Then 临时皮肤文件.SetPixel(ix, iy, System.Drawing.Color.FromArgb(255, 150, 200, 252))
                End If
            Next
        Next
        临时皮肤文件.SetPixel(0, 0, System.Drawing.Color.FromArgb(255, 0, 162, 232))
        ClipboardPreview.Source = BitmapToBitmapSource(临时皮肤文件)
    End Sub
    Private Sub 移动窗口() Handles TopBar.MouseLeftButtonDown
        Me.DragMove()
    End Sub
End Class
