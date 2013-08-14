Imports Microsoft.VisualBasic

Public Class DebugHelper

    Public Shared ReadOnly Property IsDebugMode() As Boolean
        Get
            Return MyAppSettings.IsDebugMode
        End Get
    End Property


End Class
