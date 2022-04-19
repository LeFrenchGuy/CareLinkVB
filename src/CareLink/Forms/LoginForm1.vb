﻿' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.
' See the LICENSE file in the project root for more information.

Public Class LoginForm1
    Public Property Client As CareLinkClient

    Private Sub Cancel_Click(sender As Object, e As EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

    Private Sub LoginForm1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.UsernameTextBox.Text = My.Settings.username
        Me.PasswordTextBox.Text = My.Settings.password

        Me.CountryComboBox.DataSource = New BindingSource(s_countryCodeList, Nothing)
        Me.CountryComboBox.DisplayMember = "Key"
        Me.CountryComboBox.ValueMember = "Value"
        Me.CountryComboBox.SelectedValue = My.Settings.CountryCode
    End Sub

    Private Sub OK_Click(sender As Object, e As EventArgs) Handles OK.Click
        Me.OK.Enabled = False
        Me.Cancel.Enabled = False
        My.Settings.CountryCode = Me.CountryComboBox.SelectedValue.ToString
        Me.Client = New CareLinkClient(Me.UsernameTextBox.Text, Me.PasswordTextBox.Text, Me.CountryComboBox.SelectedValue.ToString)
        If Not Me.Client.LoggedIn Then
            Dim recentData As Dictionary(Of String, String) = Me.Client.GetRecentData()
            If recentData IsNot Nothing AndAlso recentData.Count > 0 Then
                Me.OK.Enabled = True
                Me.Cancel.Enabled = True
                If Me.SaveCredentials.CheckState = CheckState.Checked Then
                    My.Settings.username = Me.UsernameTextBox.Text
                    My.Settings.password = Me.PasswordTextBox.Text
                End If

                My.Settings.Save()
                Me.Hide()
                Exit Sub
            End If
        Else
            Me.OK.Enabled = True
            Me.Cancel.Enabled = True
            Me.Hide()
            Exit Sub
        End If

        If MsgBox("Login Unsuccessful. try again? If no program will exit!", Buttons:=MsgBoxStyle.YesNo, Title:="Login Failed") = MsgBoxResult.No Then
            End
        End If
        Me.OK.Enabled = True
        Me.Cancel.Enabled = True
    End Sub

End Class
