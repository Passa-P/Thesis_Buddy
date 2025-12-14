Public Class Result_Recommendation
	Protected Overrides Sub OnFormClosed(e As FormClosedEventArgs)
		MyBase.OnFormClosed(e)
		Application.Exit()
	End Sub
End Class