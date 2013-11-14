Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Public Class DangerousBehavoirControl
    Public Shared Property RecordsList As ListDictionary
        Get
            Dim ausgabe As ListDictionary = System.Web.HttpContext.Current.Application(App_NAMES.App_Form_submission_counters)
            If ausgabe Is Nothing Then ausgabe = New ListDictionary
            Return ausgabe
        End Get
        Set(ByVal value As ListDictionary)
            System.Web.HttpContext.Current.Application(App_NAMES.App_Form_submission_counters) = value
        End Set
    End Property
    ''' <summary>
    ''' This function prevents submitting the registration form multiple times within short time span from same ip address.
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub ClientSubmitedTheRegistration()
        Dim clientIP As String = System.Web.HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        If String.IsNullOrEmpty(clientIP) Then clientIP = System.Web.HttpContext.Current.Request.UserHostAddress
        If String.IsNullOrEmpty(clientIP) Then Exit Sub

        Dim timeSpan As Integer = 10 'min
        Dim maxCounter As Integer = 10

        'retriving the record about ip from the application variable
        Dim records As ListDictionary = RecordsList
        If Not records.Contains(clientIP) Then records.Add(clientIP, New Pair(0, Now))
        Dim recordPair As Pair = records(clientIP)
        Dim recordCounter As Integer = recordPair.First
        Dim recordTime As DateTime = recordPair.Second
        If DateDiff(Microsoft.VisualBasic.DateInterval.Minute, recordTime, Now) > timeSpan Then
            'initializing new timespan
            recordCounter = 1
            recordPair.First = recordCounter
            recordPair.Second = Now
        Else
            'multiple submissions within current timeSpan! we're increasing the counter
            recordCounter += 1
            recordPair.First = recordCounter
        End If

        If recordCounter > maxCounter Then
            'we are gentle. giving possibility to submit 2 times
            recordPair.First = maxCounter - 2
            records(clientIP) = recordPair
            RecordsList = records
            Throw New DangerousBehavoirException("DangerousBehavoir: Registration Form was submited " + maxCounter.ToString + " times within " + timeSpan.ToString + " minutes. Time: " + Now.ToString + ". The clientIP was " + clientIP)
        Else
            records(clientIP) = recordPair
            RecordsList = records
        End If


    End Sub

    Public Shared Function ListRecords() As List(Of String)
        Dim ausgabe As New List(Of String)
        For Each k As String In RecordsList.Keys
            ausgabe.Add("IP: " + k + "; Count: " + CType(RecordsList(k), Pair).First.ToString + "; TimeSpanStart: " + CType(RecordsList(k), Pair).Second.ToString())
        Next

        Return ausgabe
    End Function
End Class

Public Class DangerousBehavoirException
    Inherits Exception

    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub
End Class