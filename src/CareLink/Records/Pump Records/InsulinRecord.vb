﻿' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.
' See the LICENSE file in the project root for more information.

Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations.Schema

Public Class InsulinRecord
    Private _dateTime As Date

#If True Then ' Prevent reordering

    <DisplayName(NameOf(RecordNumber))>
    <Column(Order:=0)>
    Public Property RecordNumber As Integer

    <DisplayName(NameOf(type))>
    <Column(Order:=1)>
    Public Property type As String

    <DisplayName(NameOf(index))>
    <Column(Order:=2)>
    Public Property index As Integer

    <DisplayName(NameOf(kind))>
    <Column(Order:=3)>
    Public Property kind As String

    <DisplayName(NameOf(version))>
    <Column(Order:=4)>
    Public Property version As Integer

    <DisplayName(NameOf([dateTime]))>
    <Column(Order:=5)>
    Public Property [dateTime] As Date
        Get
            Return _dateTime
        End Get
        Set
            _dateTime = Value
        End Set
    End Property

    <DisplayName(NameOf(dateTimeAsString))>
    <Column(Order:=6)>
    Public Property dateTimeAsString As String

    <DisplayName("OAdatetime")>
    <Column(Order:=7)>
    Public ReadOnly Property OAdateTime As OADate
        Get
            Return New OADate(_dateTime)
        End Get
    End Property

    <DisplayName(NameOf(relativeOffset))>
    <Column(Order:=8)>
    Public Property relativeOffset As Integer

    <DisplayName(NameOf(programmedExtendedAmount))>
    <Column(Order:=9)>
    Public Property programmedExtendedAmount As Single

    <DisplayName(NameOf(activationType))>
    <Column(Order:=10)>
    Public Property activationType As String

    <DisplayName(NameOf(deliveredExtendedAmount))>
    <Column(Order:=11)>
    Public Property deliveredExtendedAmount As Single

    <DisplayName(NameOf(programmedFastAmount))>
    <Column(Order:=12)>
    Public Property programmedFastAmount As Single

    <DisplayName(NameOf(programmedDuration))>
    <Column(Order:=13)>
    Public Property programmedDuration As Integer

    <DisplayName(NameOf(deliveredFastAmount))>
    <Column(Order:=14)>
    Public Property deliveredFastAmount As Single

    <DisplayName(NameOf(id))>
    <Column(Order:=15)>
    Public Property id As Integer

    <DisplayName(NameOf(effectiveDuration))>
    <Column(Order:=16)>
    Public Property effectiveDuration As Integer

    <DisplayName(NameOf(completed))>
    <Column(Order:=17)>
    Public Property completed As Boolean

    <DisplayName(NameOf(bolusType))>
    <Column(Order:=18)>
    Public Property bolusType As String

#End If  ' Prevent reordering
End Class
