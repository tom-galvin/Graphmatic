#define MyAppName "Graphmatic"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "Thomas Galvin"
#define MyAppURL "http://usn.pw/"
#define MyAppExeName "Graphmatic.exe"

[Setup]
AppId={{3DF2CCA8-54DB-4596-849C-7DC7DAFEB800}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}  
WizardSmallImageFile=SmallImage.bmp
WizardImageFile=LargeImage.bmp
AllowNoIcons=yes
LicenseFile=SetupLicense.txt
OutputBaseFilename=setup
Compression=lzma
SolidCompression=yes

[Registry]
Root: HKCR; Subkey: ".gmd"; ValueType: string; ValueName: ""; ValueData: "GraphmaticDocument"; Flags: uninsdeletevalue
Root: HKCR; Subkey: "GraphmaticDocument"; ValueType: string; ValueName: ""; ValueData: "Graphmatic Document"; Flags: uninsdeletekey
Root: HKCR; Subkey: "GraphmaticDocument\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\GraphmaticDocument.ico"
Root: HKCR; Subkey: "GraphmaticDocument\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#MyAppExeName}"" ""%1"""

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]  
Source: "Graphmatic\bin\Release\Graphmatic.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "Graphmatic\Resources\GraphmaticDocument.ico"; DestDir: "{app}"; Flags: ignoreversion
Source: "dotNetFx40_Client_setup.exe"; DestDir: {tmp}; Flags: deleteafterinstall; Check: not IsRequiredDotNetDetected 
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: {tmp}\dotNetFx40_Client_setup.exe; Parameters: "/q:a /c:""install /l /q"""; Check: not IsRequiredDotNetDetected; StatusMsg: .NET Framework 4.0 Client profile is beïng installed.
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[Code]
// Shamelessly taken from StackOverflow (http://stackoverflow.com/questions/4104011/innosetup-verify-that-net-4-0-is-installed)
function IsDotNetDetected(version: string; service: cardinal): boolean;
// Indicates whether the specified version and service pack of the .NET Framework is installed.
//
// version -- Specify one of these strings for the required .NET Framework version:
//    'v1.1.4322'     .NET Framework 1.1
//    'v2.0.50727'    .NET Framework 2.0
//    'v3.0'          .NET Framework 3.0
//    'v3.5'          .NET Framework 3.5
//    'v4\Client'     .NET Framework 4.0 Client Profile
//    'v4\Full'       .NET Framework 4.0 Full Installation
//
// service -- Specify any non-negative integer for the required service pack level:
//    0               No service packs required
//    1, 2, etc.      Service pack 1, 2, etc. required
var
    key: string;
    install, serviceCount: cardinal;
    success: boolean;
begin
    key := 'SOFTWARE\Microsoft\NET Framework Setup\NDP\' + version;
    // .NET 3.0 uses value InstallSuccess in subkey Setup
    if Pos('v3.0', version) = 1 then begin
        success := RegQueryDWordValue(HKLM, key + '\Setup', 'InstallSuccess', install);
    end else begin
        success := RegQueryDWordValue(HKLM, key, 'Install', install);
    end;
    // .NET 4.0 uses value Servicing instead of SP
    if Pos('v4', version) = 1 then begin
        success := success and RegQueryDWordValue(HKLM, key, 'Servicing', serviceCount);
    end else begin
        success := success and RegQueryDWordValue(HKLM, key, 'SP', serviceCount);
    end;
    result := success and (install = 1) and (serviceCount >= service);
end;

function IsRequiredDotNetDetected(): Boolean;  
begin
    result := IsDotNetDetected('v4\Client', 0);
end;

function InitializeSetup(): Boolean;
begin
    if not IsDotNetDetected('v4\Client', 0) then begin
        MsgBox('MyApp requires Microsoft .NET Framework 4.0 Client Profile.'#13#13
            'The {#MyAppName} installer will attempt to install the framework for you.'#13
            'You must be connected to the internet to do so.', mbInformation, MB_OK);
    end;
    result := true;
end;