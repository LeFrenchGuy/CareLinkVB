﻿' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.
' See the LICENSE file in the project root for more information.

Imports System.ComponentModel
Imports System.IO
Imports System.Text.Json
Imports DataGridViewColumnControls

Public Class InitializeDialog

    Private ReadOnly _aitLengths As New Dictionary(Of String, TimeSpan) From
            {
                {"2:00", New TimeSpan(2, 0, 0)},
                {"2:15", New TimeSpan(2, 15, 0)},
                {"2:30", New TimeSpan(2, 30, 0)},
                {"2:45", New TimeSpan(2, 45, 0)},
                {"3:00", New TimeSpan(3, 0, 0)},
                {"3:15", New TimeSpan(3, 15, 0)},
                {"3:30", New TimeSpan(3, 30, 0)},
                {"3:45", New TimeSpan(3, 45, 0)},
                {"4:00", New TimeSpan(4, 0, 0)},
                {"4:15", New TimeSpan(4, 15, 0)},
                {"4:30", New TimeSpan(4, 30, 0)},
                {"4:45", New TimeSpan(4, 45, 0)},
                {"5:00", New TimeSpan(5, 0, 0)},
                {"5:15", New TimeSpan(5, 15, 0)},
                {"5:30", New TimeSpan(5, 30, 0)},
                {"5:45", New TimeSpan(5, 45, 0)},
                {"6:00", New TimeSpan(6, 0, 0)}
            }

    Private ReadOnly _insulinTypesBindingSource As New BindingSource(
                s_insulinTypes, Nothing)

    Private ReadOnly _midday As String = New TimeOnly(12, 0).ToString(CurrentDateCulture)
    Private ReadOnly _midnight As String = New TimeOnly(0, 0).ToString(CurrentDateCulture)
    Public Property CurrentUser As CurrentUserRecord

    Private Sub Cancel_Button_Click(sender As Object, e As EventArgs) Handles Cancel_Button.Click
        If Me.Cancel_Button.Text = "Confirm Exit!" Then End
        If MsgBox("If you continue, the program will exit!", MsgBoxStyle.RetryCancel, "Exit or Retry") = MsgBoxResult.Cancel Then
            End
        End If
        Me.DialogResult = DialogResult.None
    End Sub

    Private Sub Cancel_Button_GotFocus(sender As Object, e As EventArgs) Handles Cancel_Button.GotFocus
        Me.Cancel_Button.Text = "Confirm Exit!"
    End Sub

    Private Sub InitializeComboList(items As DataGridViewComboBoxCell.ObjectCollection, start As Integer)
        For i As Integer = start To 47
            Dim t As New TimeOnly(i \ 2, (i Mod 2) * 30)
            items.Add(t.ToString(CurrentDateCulture))
        Next
        items.Add(_midnight)

    End Sub

    Private Sub InitializeDataGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles InitializeDataGridView.CellContentClick
        Dim dgv As DataGridView = CType(sender, DataGridView)
        Dim cell As DataGridViewCell = dgv.Rows(e.RowIndex).Cells(e.ColumnIndex)
        Select Case dgv.Columns(e.ColumnIndex).Name
            Case NameOf(ColumnDeleteRow)
                If Not CType(cell, DataGridViewDisableButtonCell).Enabled Then Exit Sub
                dgv.Rows.Remove(dgv.Rows(e.RowIndex))
                Dim currentRow As Integer = e.RowIndex - 1
                With dgv.Rows(currentRow)
                    Dim buttonCell As DataGridViewDisableButtonCell = CType(.Cells(NameOf(ColumnDeleteRow)), DataGridViewDisableButtonCell)
                    buttonCell.Enabled = False
                    buttonCell.ReadOnly = True
                    Dim c As DataGridViewComboBoxCell = CType(.Cells(NameOf(ColumnEnd)), DataGridViewComboBoxCell)
                    Dim startTime As TimeOnly = TimeOnly.Parse(Me.InitializeDataGridView.Rows(currentRow).Cells(NameOf(ColumnEnd)).Value.ToString)
                    Dim value As String = startTime.ToString
                    Me.InitializeComboList(c.Items, CInt(startTime.ToTimeSpan.TotalMinutes / 30))
                    c.Value = _midnight
                    c.ReadOnly = False
                    buttonCell = CType(.Cells(NameOf(ColumnSave)), DataGridViewDisableButtonCell)
                    buttonCell.ReadOnly = False
                    buttonCell.Enabled = True
                End With

            Case NameOf(ColumnStart)
                dgv.CurrentCell = dgv.Rows(e.RowIndex).Cells(NameOf(ColumnEnd))

            Case NameOf(ColumnEnd)
            Case NameOf(ColumnNumericUpDown)

            Case NameOf(ColumnSave)
                With Me.InitializeDataGridView
                    If .Rows(e.RowIndex).Cells(NameOf(ColumnEnd)).Value.ToString = _midnight Then
                        Me.OK_Button.Enabled = True
                        Me.OK_Button.Focus()
                        Dim buttonCell As DataGridViewDisableButtonCell = CType(.Rows(.RowCount - 1).Cells(NameOf(ColumnSave)), DataGridViewDisableButtonCell)
                        buttonCell.ReadOnly = True
                        buttonCell.Enabled = False
                        Me.InitializeDataGridView.Enabled = False
                        Exit Sub
                    End If
                    With .Rows(e.RowIndex)
                        CType(.Cells(NameOf(ColumnDeleteRow)), DataGridViewDisableButtonCell).Enabled = False
                        CType(.Cells(NameOf(ColumnSave)), DataGridViewDisableButtonCell).Enabled = False
                    End With
                    For Each c As DataGridViewCell In .Rows(e.RowIndex).Cells
                        c.ReadOnly = Not c.OwningColumn.HeaderText = "Carb Ratio g/U"
                    Next
                    Me.InitializeDataGridView.Rows.Add()
                    With .Rows(.Rows.Count - 1)
                        Me.OK_Button.Enabled = False
                        Dim c As DataGridViewComboBoxCell = CType(.Cells(NameOf(ColumnStart)), DataGridViewComboBoxCell)
                        Dim columnEndCell As DataGridViewCell = Me.InitializeDataGridView.Rows(e.RowIndex).Cells(NameOf(ColumnEnd))
                        columnEndCell.ErrorText = ""
                        Dim timeOnly As TimeOnly = TimeOnly.Parse(columnEndCell.Value.ToString)
                        Dim value As String = timeOnly.ToString
                        c.Items.Add(value)
                        c.Value = value
                        c = CType(.Cells(NameOf(ColumnEnd)), DataGridViewComboBoxCell)
                        Me.InitializeComboList(c.Items, CInt(timeOnly.ToTimeSpan.TotalMinutes / 30) + 1)
                        c.Value = _midnight
                        .Cells(NameOf(ColumnNumericUpDown)).Value = 15.0
                        CType(.Cells(NameOf(ColumnDeleteRow)), DataGridViewDisableButtonCell).Enabled = True
                    End With
                End With
        End Select

    End Sub

    Private Sub InitializeDataGridView_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles InitializeDataGridView.DataError
        Stop
    End Sub

    Private Sub InitializeDataGridView_Validating(sender As Object, e As CancelEventArgs) Handles InitializeDataGridView.Validating
        Dim cell As DataGridViewCell = Me.InitializeDataGridView.Rows(Me.InitializeDataGridView.RowCount - 1).Cells(NameOf(ColumnEnd))
        If cell.Value.ToString = _midnight Then
            cell.ErrorText = ""
        Else
            cell.ErrorText = $"Value must be {_midnight}"
            Me.InitializeDataGridView.CurrentCell = cell
            Me.DialogResult = DialogResult.None
        End If
    End Sub

    Private Sub InitializeDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.ColumnStart.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
        Me.ColumnEnd.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox

        Me.Text = $"Initialize CareLink{TmChar} For {Me.CurrentUser.UserName}"

        With Me.PumpAitComboBox
            .DataSource = New BindingSource(_aitLengths, Nothing)
            .DisplayMember = "Key"
            .ValueMember = "Value"
            If Me.CurrentUser.Ait Is Nothing Then
                .SelectedIndex = -1
            Else
                .Enabled = True
                .SelectedIndex = .Items.IndexOfValue(Of String, TimeSpan)(CType(Me.CurrentUser.Ait, TimeSpan))
            End If
            .Enabled = True
            .Focus()
        End With

        With Me.InsulinTypeComboBox
            .DataSource = _insulinTypesBindingSource
            .DisplayMember = "Key"
            .ValueMember = "Value"
            If String.IsNullOrWhiteSpace(Me.CurrentUser.InsulinTypeName) Then
                .SelectedIndex = -1
            Else
                .Enabled = True
                .SelectedIndex = .Items.IndexOfKey(Of String, TimeSpan)(Me.CurrentUser.InsulinTypeName)
            End If
        End With

        Me.UseAITAdvancedDecayCheckBox.CheckState = Me.CurrentUser.UseAdvancedAitDecay
        Me.UseAITAdvancedDecayCheckBox.Enabled = True

        With Me.InitializeDataGridView
            .Rows.Clear()
            .Enabled = True
            If Me.CurrentUser.CarbRatios.Count > 0 Then
                For Each i As IndexClass(Of CarbRatioRecord) In Me.CurrentUser.CarbRatios.WithIndex
                    Dim value As CarbRatioRecord = i.Value
                    .Rows.Add()
                    With .Rows(i.Index)
                        Dim buttonCell As DataGridViewDisableButtonCell = CType(.Cells(NameOf(ColumnDeleteRow)), DataGridViewDisableButtonCell)
                        buttonCell.Enabled = i.IsLast
                        Dim c As DataGridViewComboBoxCell = CType(.Cells(NameOf(ColumnStart)), DataGridViewComboBoxCell)
                        c.Items.Add(value.StartTime.ToString(CurrentDateCulture))
                        c.Value = value.StartTime.ToString(CurrentDateCulture)
                        c.ReadOnly = True
                        c = CType(.Cells(NameOf(ColumnEnd)), DataGridViewComboBoxCell)
                        Me.InitializeComboList(c.Items, CInt((New TimeSpan(value.StartTime.Hour, value.StartTime.Minute, 0) / s_30MinuteSpan) + 1))
                        c.Value = value.EndTime.ToString(CurrentDateCulture)
                        c.ReadOnly = i.IsLast AndAlso Not i.IsFirst
                        Dim numericCell As DataGridViewNumericUpDownCell = CType(.Cells(NameOf(ColumnNumericUpDown)), DataGridViewNumericUpDownCell)
                        numericCell.Value = value.CarbRatio
                        numericCell.ReadOnly = False
                    End With
                Next
                Me.InitializeDataGridView.Enabled = True
            Else
                .Rows.Add()
                With .Rows(0)
                    Dim buttonCell As DataGridViewDisableButtonCell = CType(.Cells(NameOf(ColumnDeleteRow)), DataGridViewDisableButtonCell)
                    buttonCell.Enabled = False
                    Dim c As DataGridViewComboBoxCell = CType(.Cells(NameOf(ColumnStart)), DataGridViewComboBoxCell)
                    c.Items.Add(_midnight)
                    c.Value = _midnight
                    c.ReadOnly = True

                    c = CType(.Cells(NameOf(ColumnEnd)), DataGridViewComboBoxCell)
                    Me.InitializeComboList(c.Items, 1)
                    c.Value = _midday
                    Dim numericCell As DataGridViewNumericUpDownCell = CType(.Cells(NameOf(ColumnNumericUpDown)), DataGridViewNumericUpDownCell)
                    numericCell.Value = 15.0
                End With
                Me.InitializeDataGridView.Enabled = False
            End If
            For Each col As DataGridViewColumn In .Columns
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                col.SortMode = DataGridViewColumnSortMode.NotSortable
            Next
        End With

    End Sub

    Private Sub InsulinTypeComboBox_Leave(sender As Object, e As EventArgs) Handles InsulinTypeComboBox.Leave
        Dim c As ComboBox = CType(sender, ComboBox)
        c.Enabled = False
        Me.UseAITAdvancedDecayCheckBox.CausesValidation = True
    End Sub

    Private Sub InsulinTypeComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles InsulinTypeComboBox.SelectedIndexChanged
        Dim c As ComboBox = CType(sender, ComboBox)
        Me.UseAITAdvancedDecayCheckBox.Enabled = c.SelectedIndex > -1
    End Sub

    Private Sub InsulinTypeComboBox_Validating(sender As Object, e As CancelEventArgs) Handles InsulinTypeComboBox.Validating
        Dim c As ComboBox = CType(sender, ComboBox)
        If c.SelectedIndex > -1 Then
            Me.ErrorProvider1.SetError(c, "")
        Else
            Me.ErrorProvider1.SetError(c, $"Value must be {_midnight}")
            e.Cancel = True
        End If

    End Sub

    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        Dim cell As DataGridViewCell = Me.InitializeDataGridView.Rows(Me.InitializeDataGridView.RowCount - 1).Cells(NameOf(ColumnEnd))
        cell.ErrorText = ""
        Me.DialogResult = DialogResult.OK

        Dim selectedValue As TimeSpan = CType(Me.PumpAitComboBox.SelectedValue, TimeSpan)
        Me.CurrentUser.Ait = selectedValue

        Me.CurrentUser.InsulinTypeName = Me.InsulinTypeComboBox.Text
        Me.CurrentUser.InsulinRealAit = CType(Me.InsulinTypeComboBox.SelectedValue, TimeSpan)

        Me.CurrentUser.UseAdvancedAitDecay = Me.UseAITAdvancedDecayCheckBox.CheckState

        Me.CurrentUser.CarbRatios.Clear()
        ' Save all carb ratios
        Dim rowIndex As Integer = 0
        For Each row As DataGridViewRow In Me.InitializeDataGridView.Rows
            Dim carbRecord As New CarbRatioRecord
            cell = row.Cells(NameOf(ColumnStart))
            carbRecord.StartTime = TimeOnly.Parse(cell.Value.ToString, CurrentDateCulture)
            cell = row.Cells(NameOf(ColumnEnd))
            carbRecord.EndTime = TimeOnly.Parse(cell.Value.ToString, CurrentDateCulture)
            Dim numericCell As DataGridViewNumericUpDownCell = CType(row.Cells(NameOf(ColumnNumericUpDown)), DataGridViewNumericUpDownCell)
            carbRecord.CarbRatio = CSng(numericCell.Value)
            Me.CurrentUser.CarbRatios.Add(carbRecord)
        Next
        Dim userSettingsPath As String = $"{Path.Combine(MyDocumentsPath, $"{ProjectName}{My.Settings.CareLinkUserName}")}.json"

        File.WriteAllText(userSettingsPath,
                          JsonSerializer.Serialize(Me.CurrentUser, JsonFormattingOptions))

        Me.Close()
    End Sub

    Private Sub PumpAitComboBoxComboBox_Leave(sender As Object, e As EventArgs) Handles PumpAitComboBox.Leave
        Dim c As ComboBox = CType(sender, ComboBox)
        c.Enabled = False
        Me.InsulinTypeComboBox.CausesValidation = True
    End Sub

    Private Sub PumpAitComboBoxComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PumpAitComboBox.SelectedIndexChanged
        Dim c As ComboBox = CType(sender, ComboBox)
        Me.InsulinTypeComboBox.Enabled = c.SelectedIndex > -1
    End Sub

    Private Sub PumpAitComboBoxComboBox_Validating(sender As Object, e As CancelEventArgs) Handles PumpAitComboBox.Validating
        Dim c As ComboBox = CType(sender, ComboBox)
        If c.SelectedIndex > -1 Then
            Me.ErrorProvider1.SetError(c, "")
            Me.InsulinTypeComboBox.Enabled = True
        Else
            Me.ErrorProvider1.SetError(c, "You must select an AIT Value!")
            c.Enabled = True
            e.Cancel = True
        End If
    End Sub

    Private Sub UseAITAdvancedDecayCheckBox_Click(sender As Object, e As EventArgs) Handles UseAITAdvancedDecayCheckBox.Click
        Dim chkBox As CheckBox = CType(sender, CheckBox)
        Select Case chkBox.CheckState
            Case CheckState.Indeterminate, CheckState.Unchecked
                chkBox.CheckState = CheckState.Checked
            Case CheckState.Checked
                chkBox.CheckState = CheckState.Unchecked
        End Select
        Dim dgv As DataGridView = Me.InitializeDataGridView
        Dim cell As DataGridViewCell = dgv.Rows(Me.InitializeDataGridView.RowCount - 1).Cells(NameOf(ColumnEnd))
        cell.Value = _midnight
        Me.InitializeDataGridView.CausesValidation = True
        Me.InitializeDataGridView.Enabled = True
    End Sub

    Private Sub UseAITAdvancedDecayCheckBox_Leave(sender As Object, e As EventArgs) Handles UseAITAdvancedDecayCheckBox.Leave
        If Me.UseAITAdvancedDecayCheckBox.CheckState <> CheckState.Indeterminate Then
            Me.InitializeDataGridView.Enabled = True
            Me.UseAITAdvancedDecayCheckBox.Enabled = False
            Dim cell As DataGridViewCell = Me.InitializeDataGridView.Rows(Me.InitializeDataGridView.RowCount - 1).Cells(NameOf(ColumnEnd))
            Me.InitializeDataGridView.CurrentCell = cell
        End If
    End Sub

End Class
