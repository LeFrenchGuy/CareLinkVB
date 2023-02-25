﻿' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.
' See the LICENSE file in the project root for more information.

Imports System.Globalization
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Text.Json
Imports System.Windows.Forms.Design.AxImporter

Friend Module Form1LoginHelpers

    <Extension>
    Friend Function DoOptionalLoginAndUpdateData(MainForm As Form1, UpdateAllTabs As Boolean, fileToLoad As FileToLoadOptions) As Boolean
        MainForm.ServerUpdateTimer.Stop()
        Debug.Print($"In {NameOf(DoOptionalLoginAndUpdateData)}, {NameOf(MainForm.ServerUpdateTimer)} stopped at {Now.ToLongTimeString}")
        s_listOfAutoBasalDeliveryMarkers.Clear()
        s_listOfManualBasal.Clear()
        Select Case fileToLoad
            Case FileToLoadOptions.LastSaved
                MainForm.Text = $"{SavedTitle} Using Last Saved Data"
                CurrentDateCulture = LastDownloadWithPath.ExtractCultureFromFileName(SavedLastDownloadName)
                MainForm.RecentData = Loads(File.ReadAllText(LastDownloadWithPath))
                MainForm.MenuShowMiniDisplay.Visible = Debugger.IsAttached
                MainForm.LastUpdateTime.Text = $"{File.GetLastWriteTime(LastDownloadWithPath).ToShortDateTimeString} from file"
                SetUpCareLinkUser(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, s_testSettingsFileName))
            Case FileToLoadOptions.TestData
                MainForm.Text = $"{SavedTitle} Using Test Data from 'SampleUserData.json'"
                CurrentDateCulture = New CultureInfo("en-US")
                Dim testDataWithPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SampleUserData.json")
                MainForm.RecentData = Loads(File.ReadAllText(testDataWithPath))
                MainForm.MenuShowMiniDisplay.Visible = Debugger.IsAttached
                MainForm.LastUpdateTime.Text = $"{File.GetLastWriteTime(testDataWithPath).ToShortDateTimeString} from file"
                SetUpCareLinkUser(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, s_testSettingsFileName))
            Case FileToLoadOptions.Login
                MainForm.Text = SavedTitle
                Do Until MainForm.LoginDialog.ShowDialog() <> DialogResult.Retry
                Loop

                If MainForm.Client Is Nothing OrElse Not MainForm.Client.LoggedIn Then
                    MainForm.ServerUpdateTimer.Interval = CInt(s_5MinutesInMilliseconds)
                    MainForm.ServerUpdateTimer.Start()
                    Debug.Print($"In {NameOf(DoOptionalLoginAndUpdateData)}, {NameOf(MainForm.ServerUpdateTimer)} started at {Now.ToLongTimeString}")
                    If NetworkDown Then
                        ReportLoginStatus(MainForm.LoginStatus)
                        Return False
                    End If

                    MainForm.LastUpdateTime.Text = "Unknown"
                    Return False
                End If

                Dim currentUserSettingsFileName As String = $"{ProjectName}{My.Settings.CareLinkUserName}Settings.json"
                Dim userSettingsPath As String = Path.Combine(MyDocumentsPath, currentUserSettingsFileName)
                SetUpCareLinkUser(userSettingsPath)

                MainForm.AITComboBox.SelectedIndex = MainForm.AITComboBox.FindStringExact($"AIT {CType(CurrentUser.Ait, TimeSpan).ToString("hh\:mm").Substring(1)}")
                MainForm.RecentData = MainForm.Client.GetRecentData(MainForm)
                MainForm.ServerUpdateTimer.Interval = CInt(s_1MinutesInMilliseconds)
                MainForm.ServerUpdateTimer.Start()
                Debug.Print($"In {NameOf(DoOptionalLoginAndUpdateData)}, {NameOf(MainForm.ServerUpdateTimer)} started at {Now.ToLongTimeString}")

                If NetworkDown Then
                    ReportLoginStatus(MainForm.LoginStatus)
                    Return False
                End If

                ReportLoginStatus(MainForm.LoginStatus, MainForm.RecentData Is Nothing OrElse MainForm.RecentData.Count = 0, MainForm.Client.GetLastErrorMessage)

                MainForm.MenuShowMiniDisplay.Visible = True
        End Select
        MainForm.FinishInitialization()
        If UpdateAllTabs Then
            MainForm.UpdateAllTabPages()
        End If
        Return True
    End Function

    Private Sub SetUpCareLinkUser(userSettingsPath As String)
        Dim contents As String
        If Path.Exists(userSettingsPath) Then
            contents = File.ReadAllText(userSettingsPath)
            CurrentUser = JsonSerializer.Deserialize(Of CurrentUserRecord)(contents, JsonFormattingOptions)
        Else
            CurrentUser = New CurrentUserRecord(My.Settings.CareLinkUserName)
            Dim f As New InitializeDialog With {.CurrentUser = CurrentUser}
            f.ShowDialog()
            CurrentUser = f.CurrentUser
        End If
    End Sub

    <Extension>
    Friend Sub FinishInitialization(MainForm As Form1)
        MainForm.Cursor = Cursors.Default
        Application.DoEvents()

        MainForm.InitializeSummaryTabCharts()
        MainForm.InitializeActiveInsulinTabChart()
        MainForm.InitializeTimeInRangeArea()

        MainForm.Initialized = True
    End Sub

End Module
