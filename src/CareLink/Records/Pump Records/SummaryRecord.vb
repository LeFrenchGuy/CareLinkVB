﻿' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.
' See the LICENSE file in the project root for more information.

Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations.Schema

Public Class SummaryRecord
    Implements IComparable

    ''' <summary>
    ''' Used where message needs to be translated
    ''' </summary>
    ''' <param name="recordNumber"></param>
    ''' <param name="row"></param>
    ''' <param name="messages"></param>
    ''' <param name="messageTableName"></param>
    Protected Friend Sub New(recordNumber As Integer, row As KeyValuePair(Of String, String), messages As Dictionary(Of String, String), messageTableName As String)
        Me.New(recordNumber, row)
        Dim message As String = ""
        If Not String.IsNullOrWhiteSpace(row.Value) Then
            If Not messages.TryGetValue(row.Value, message) Then
                If Debugger.IsAttached Then
                    MsgBox($"{row.Value} is unknown message for {messageTableName}")
                End If
                message = row.Value.ToTitleCase
            End If
        End If

        Me.Message = message
    End Sub

    Protected Friend Sub New(recordNumber As Integer, row As KeyValuePair(Of String, String), Optional message As String = "")
        Me.New(recordNumber, row.Key, row.Value, message)
    End Sub

    ''' <summary>
    ''' Handles No Message Case
    ''' </summary>
    ''' <param name="recordNumber"></param>
    ''' <param name="key"></param>
    ''' <param name="value"></param>
    Protected Friend Sub New(recordNumber As Integer, key As String, value As String, message As String)
        Me.RecordNumber = recordNumber
        Me.Key = key
        Me.Value = value
        Me.Message = message
    End Sub

    <DisplayName("Record Number")>
    <Column(Order:=0, TypeName:=NameOf(RecordNumber))>
    Public ReadOnly Property RecordNumber As Integer

    <DisplayName(NameOf(Key))>
    <Column(Order:=1, TypeName:=NameOf([String]))>
    Public ReadOnly Property Key As String

    <DisplayName(NameOf(Value))>
    <Column(Order:=2, TypeName:=NameOf([String]))>
    Public ReadOnly Property Value As String

    <DisplayName(NameOf(Message))>
    <Column(Order:=3, TypeName:=NameOf([String]))>
    Public ReadOnly Property Message As String = ""

    Public Function CompareTo(obj As Object) As Integer Implements IComparable.CompareTo
        Dim bom As SummaryRecord = CType(obj, SummaryRecord)

        If bom IsNot Nothing Then
            Return Me.RecordNumber.CompareTo(bom.RecordNumber)
        Else
            Throw New ArgumentException("Object is not a BomItem")
        End If
    End Function

End Class
