Imports Microsoft.VisualBasic

Public Class PrintView

    ''' <summary>
    ''' If the current page must be rendered as printview.
    ''' Valid only for the current request.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Property needPrintView() As Boolean
        Get
            If System.Web.HttpContext.Current.Items(C_Names.C_needPrintView) Is Nothing Then
                If (Not System.Web.HttpContext.Current.Request.QueryString(Q_Names.Q_PrintView) Is Nothing) _
                  AndAlso printViewEnabled Then
                    Return True
                End If
                Return False
            Else
                Return System.Web.HttpContext.Current.Items(C_Names.C_needPrintView)
            End If
        End Get
        Set(ByVal value As Boolean)
            System.Web.HttpContext.Current.Items(C_Names.C_needPrintView) = value
        End Set
    End Property

    Public Shared Property printViewEnabled() As Boolean
        Get
            If System.Web.HttpContext.Current.Items(C_Names.C_printViewEnabled) Is Nothing Then
                Return True
            Else
                Return System.Web.HttpContext.Current.Items(C_Names.C_printViewEnabled)
            End If
        End Get
        Set(ByVal value As Boolean)
            System.Web.HttpContext.Current.Items(C_Names.C_printViewEnabled) = value
        End Set
    End Property
End Class
