Imports System.Windows.Forms

Public Class BufferedFlowLayoutPanel
    Inherits FlowLayoutPanel

    Public Sub New()
        Me.DoubleBuffered = True
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw, True)
        Me.UpdateStyles()
    End Sub

    Protected Overrides Sub OnScroll(se As ScrollEventArgs)
        MyBase.OnScroll(se)
        Me.Invalidate()
    End Sub
End Class
