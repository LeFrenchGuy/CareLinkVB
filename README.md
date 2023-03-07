# CareLink

## Download Latest Release from
https://GitHub.com/paul1956/CareLink/releases/

# Requires for running
- .NET Core 6.0.6 or later
- Windows 10 or later

Try it out and send feedback.
This update has a UI to show all the available data and a visual version that mimics the one on iPhone.
![Same display](https://GitHub.com/paul1956/CareLink/blob/master/Screenshot%202022-10-08%20203350.png?raw=true)

## Requires for development
- Visual Studio 2022 Version 17.3.0 Preview 2.0 or later
- .NET Core 6.0.6 or later
- Windows 10 or later

## License
- MIT

## Technologies
  - Windows Forms
  - dotnet-core

## Topics
- Medtronic CareLink data display

## Updated
03/06/2023

## What's New in this release
New in 3.6.0.3
- Prevent Crash with setting up new users

New in 3.6.0.2
- Improve overlap of Callouts, if you don't like it hovering over a blocked callout will bring it to front.

New in 3.6.0.1
- All Graphs have Legends and line colors are editable
- All files associated with application are moved under MyDocuments/CareLink, the first install will move old files to new location. You can delete anything in that directory and if it's still needed it will be recreated. If you had old error files you should probably delete them.

A new Directory MyDocuments/CareLink/Settings contains a Settings File which is initially blank it will contain information about your pump that is not available from CareLink.

- Pump AIT
- Insulin Type from drop down List, currently limited to 5 popular pump insulin type open issue it yours is missing and one listed isn't close
- A check box to allow selection of AIT decay algorithm (one uses pump value and an advanced one that is based on Insulin Type.
- For 780 it allows setting you pump Target SG for 770G its fixed at 120
- Lastly is an area where you can enter your Carb Ratio by time.

## Description

Repo for CareLink Windows App
This is a Visual Basic application that provides a UI to view Medtronic 670G and 770G and 780G.

This shows all available data and is not in any way supported by Medtronic, it was created from publicly available data.
Some data was filtered out because I could not see any use for it. You can turn off filters but performance will suffer.

For the visualization layer I use the open source
System.Windows.Forms.DataVisualization
https://GitHub.com/Kirsan31/WinForms-DataVisualization

#Known Issue
- If you get a 
> "System.Configuration.ConfigurationErrorsException: 'Configuration system failed to initialize'"
> will need to edit CareLink\src\CareLink\bin\Debug\net7.0-windows\CareLink.dll.config and remove the following lines
```
<system.diagnostics>
    <sources>
        <!-- This section defines the logging configuration for My.Application.Log -->
        <source name="DefaultSource" switchName="DefaultSwitch">
            <listeners>
                <add name="FileLog"/>
                <!-- Uncomment the below section to write to the Application Event Log -->
                <!--<add name="EventLog"/>-->
            </listeners>
        </source>
    </sources>
    <switches>
        <add name="DefaultSwitch" value="Information" />
    </switches>
    <sharedListeners>
        <add name="FileLog"
                type="Microsoft.VisualBasic.Logging.FileLogTraceListener, Microsoft.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL"
                initializeData="FileLogWriter"/>
        <!-- Uncomment the below section and replace APPLICATION_NAME with the name of your application to write to the Application Event Log -->
        <!--<add name="EventLog" type="System.Diagnostics.EventLogTraceListener" initializeData="APPLICATION_NAME"/> -->
    </sharedListeners>
</system.diagnostics>
```
