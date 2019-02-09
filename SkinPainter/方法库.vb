

Module 方法库
    Function BitmapToBitmapSource(ByVal bitmap As System.Drawing.Bitmap) As BitmapSource
        Dim ptr As IntPtr = bitmap.GetHbitmap()
        Dim result As BitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ptr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions())
        MainWindow.DeleteObject(ptr)
        Return result
    End Function
    '定义BTBI
    Function 剪切图片(ori As System.Drawing.Bitmap, 点1 As Point, 点2 As Point) As System.Drawing.Bitmap
        Dim sizex As Integer = Math.Abs(点1.X - 点2.X) + 1
        Dim sizey As Integer = Math.Abs(点1.Y - 点2.Y) + 1
        Dim temptmp As New System.Drawing.Bitmap(sizex, sizey)
        For iy = Math.Min(点1.Y， 点2.Y) To Math.Max(点1.Y， 点2.Y)
            For ix = Math.Min(点1.X， 点2.X) To Math.Max(点1.X， 点2.X)
                temptmp.SetPixel(ix - Math.Min(点1.X， 点2.X), iy - Math.Min(点1.Y， 点2.Y), ori.GetPixel(ix, iy))
            Next
        Next
        剪切图片 = temptmp
    End Function
    Function HSVtoRGB(alpha As Single, h As Single, s As Single, v As Single) As Color
        Dim rm, bm, gm As Single
        v = v / 100
        s = s / 100
        If s = 0 Then
            rm = v
            gm = v
            bm = v
        Else
            h /= 60
            Dim i = Int(h)
            Dim f = h - i
            Dim a = v * (1 - s)
            Dim b = v * (1 - s * f)
            Dim c = v * (1 - s * (1 - f))

            Select Case (i)
                Case 0
                    rm = v
                    gm = c
                    bm = a
                Case 1
                    rm = b
                    gm = v
                    bm = a
                Case 2
                    rm = a
                    gm = v
                    bm = c
                Case 3
                    rm = a
                    gm = b
                    bm = v
                Case 4
                    rm = c
                    gm = a
                    bm = v
                Case 5
                    rm = v
                    gm = a
                    bm = b
            End Select
        End If
        HSVtoRGB = Color.FromArgb(alpha, rm * 255, gm * 255, bm * 255)
    End Function
    Function MediaSoildBrushtoGDIC(ori As Media.Brush) As System.Drawing.Color
        Dim tmpcolorstring As String = ori.ToString
        tmpcolorstring = Right(tmpcolorstring, 8)
        MediaSoildBrushtoGDIC = System.Drawing.Color.FromArgb(CLng("&H" & Left(tmpcolorstring, 2)), CLng("&H" & Right(Left(tmpcolorstring, 4), 2)), CLng("&H" & Right(Left(tmpcolorstring, 6), 2)), CLng("&H" & Right(tmpcolorstring, 2)))
    End Function
    Function RGBtoHSV(rgb As System.Drawing.Color) As Array
        Dim r As Single = rgb.R / 255
        Dim g As Single = rgb.G / 255
        Dim b As Single = rgb.B / 255
        Dim max As Single = Math.Max(r, g)
        max = Math.Max(max, b)
        Dim min As Single = Math.Min(r, g)
        min = Math.Min(min, b)
        Dim delta As Single = max - min
        Dim h As Integer
        If delta = 0 Then
            h = 0
        Else
            Select Case max
                Case r
                    h = 60 * ((g - b) / delta)
                Case g
                    h = 60 * ((b - r) / delta + 2)
                Case b
                    h = 60 * ((r - g) / delta + 4)
                Case Else
                    h = 361
            End Select
        End If
        Dim s As Single = 0
        If max > 0 Then
            s = delta / max
        End If
        s = Int(s * 100)
        Dim v As Integer = max * 100
        Dim tmp = h.ToString + "," + s.ToString + "," + v.ToString
        RGBtoHSV = Split(tmp, ",")
    End Function
    Function FormatHue(ori As Integer) As Integer
        If ori >= 0 And ori < 359 Then
            FormatHue = ori
        Else
            If ori >= 360 Then FormatHue = ori - 360
            If ori < 0 Then FormatHue = ori + 360
        End If
    End Function
    Function 粘贴(左上角 As Point, orifront As System.Drawing.Bitmap, oriback As System.Drawing.Bitmap) As System.Drawing.Bitmap
        Dim tmpmap1 = oriback
        For xfront = 0 To orifront.Width - 1
            Dim xback = xfront + 左上角.X
            If xback >= 0 And xback <= oriback.Width - 1 Then
                '==========决定Y=========
                For yfront = 0 To orifront.Height - 1
                    Dim yback = yfront + 左上角.Y
                    If yback >= 0 And yback <= oriback.Height - 1 Then tmpmap1.SetPixel(xback, yback, orifront.GetPixel(xfront, yfront))
                Next yfront
                '========================================
            End If
        Next xfront
        粘贴 = tmpmap1
    End Function
    Function PathLeftTopQuery(skinpath As Integer, IsSecondLevel As Boolean) As Point
        Dim tmpdot = New Point(29, 31)
        If IsSecondLevel = True Then
            If skinpath = 1216 Then tmpdot = New Point(32, 0)
            If skinpath = 1215 Then tmpdot = New Point(0, 32)
            If skinpath = 1214 Then tmpdot = New Point(0, 48)
            If skinpath = 1211 Then tmpdot = New Point(16, 32)
            If skinpath = 1212 Then tmpdot = New Point(48, 48)
            If skinpath = 1213 Then tmpdot = New Point(40, 32)
        Else
            If skinpath = 1216 Then tmpdot = New Point(0, 0)
            If skinpath = 1215 Then tmpdot = New Point(0, 16)
            If skinpath = 1214 Then tmpdot = New Point(16, 48)
            If skinpath = 1211 Then tmpdot = New Point(16, 16)
            If skinpath = 1212 Then tmpdot = New Point(32, 48)
            If skinpath = 1213 Then tmpdot = New Point(40, 16)
        End If
            PathLeftTopQuery = tmpdot
    End Function
    Function PathSizeQuery(skinpath As Integer, IsSlimed As Boolean, type As String) As Integer
        Dim tmp As Integer
        If type = "x" Then
            If skinpath = 1216 Then
                tmp = 8
            Else
                tmp = 4
            End If
        End If
        '如果是y============================================
        If type = "y" Then
            If skinpath = 1216 Then tmp = 8
            If skinpath = 1215 Then tmp = 4
            If skinpath = 1214 Then tmp = 4
            If skinpath = 1211 Then tmp = 8
            If skinpath = 1212 Or skinpath = 1213 Then
                If IsSlimed = True Then
                    tmp = 3
                Else
                    tmp = 4
                End If
            End If
        End If
        If type = "z" Then
            If skinpath = 1216 Then
                tmp = 8
            Else
                tmp = 12
            End If
        End If
            PathSizeQuery = tmp
    End Function
    Function PathFaceLeftTopQuery(skinpath As Integer, face As Integer, Isslimed As Boolean, IsSecondLevel As Boolean) As Point
        Dim x = PathSizeQuery(skinpath, Isslimed, "x")
        Dim y = PathSizeQuery(skinpath, Isslimed, "y")
        Dim z = PathSizeQuery(skinpath, Isslimed, "z")
        Dim OriX = PathLeftTopQuery(skinpath, IsSecondLevel).X
        Dim OriY = PathLeftTopQuery(skinpath, IsSecondLevel).Y
        Dim tmppot2 As New Point
        If face = 7235 Then tmppot2 = New Point(OriX, OriY + x)
        If face = 7236 Then tmppot2 = New Point(OriX + x + y, OriY + x)
        If face = 7238 Then tmppot2 = New Point(OriX + 2 * x + y, OriY + x)
        If face = 7237 Then tmppot2 = New Point(OriX + x, OriY + x)
        If face = 7233 Then tmppot2 = New Point(OriX + x, OriY)
        If face = 7234 Then tmppot2 = New Point(OriX + x + y, OriY)
        PathFaceLeftTopQuery = tmppot2
    End Function
    Function PathFaceRightBottomQuery(skinpath As String, face As String, Isslimed As Boolean, IsSecondLevel As Boolean) As Point
        Dim tmppot = PathFaceLeftTopQuery(skinpath, face, Isslimed, IsSecondLevel)
        Dim x = PathSizeQuery(skinpath, Isslimed, "x")
        Dim y = PathSizeQuery(skinpath, Isslimed, "y")
        Dim z = PathSizeQuery(skinpath, Isslimed, "z")
        Dim tmppot1 As Point
        If face = 7235 Then tmppot1 = New Point(x, z)
        If face = 7236 Then tmppot1 = New Point(x, z)
        If face = 7238 Then tmppot1 = New Point(y, z)
        If face = 7237 Then tmppot1 = New Point(y, z)
        If face = 7233 Then tmppot1 = New Point(y, x)
        If face = 7234 Then tmppot1 = New Point(y, x)
        PathFaceRightBottomQuery = New Point(tmppot.X + tmppot1.X - 1, tmppot.Y + tmppot1.Y - 1)
    End Function
End Module
