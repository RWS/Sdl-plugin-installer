; -- setup.iss --
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING .ISS SCRIPT FILES!

[Setup]
AppName=SDL plug-in installer
AppPublisher=SDL Community Developers
AppPublisherURL=https://community.sdl.com/
AppVersion=0.9.1.0
DisableDirPage = yes
DisableWelcomePage = yes
AllowNoIcons = yes
DefaultDirName={pf32}\SDL\SDL Community\SDL plugin installer
Compression=lzma2
SolidCompression=yes
OutputDir=userdocs:Inno Setup Examples Output

[Files]
Source: "c:\Work\Git\Sdl-plugin-installer\Sdl.Community.SdlPluginInstaller\Sdl.Community.SdlPluginInstaller\bin\Debug\Sdl.Community.Controls.dll"; DestDir: "{app}"
Source: "c:\Work\Git\Sdl-plugin-installer\Sdl.Community.SdlPluginInstaller\Sdl.Community.SdlPluginInstaller\bin\Debug\Sdl.Community.SdlPluginInstaller.exe"; DestDir: "{app}"
Source: "c:\Work\Git\Sdl-plugin-installer\Sdl.Community.SdlPluginInstaller\Sdl.Community.SdlPluginInstaller\bin\Debug\Sdl.Core.PluginFramework.PackageSupport.dll"; DestDir: "{app}"
Source: "c:\Work\Git\Sdl-plugin-installer\Sdl.Community.SdlPluginInstaller\Sdl.Community.SdlPluginInstaller\bin\Debug\ObjectListView.dll"; DestDir: "{app}"
 

[Registry]
Root: HKCR; Subkey: ".sdlplugin"; ValueType: string; ValueName: ""; ValueData: "Sdl Plugin Installer"; Flags: uninsdeletevalue
Root: HKCR; Subkey: "Sdl Plugin Installer"; ValueType: string; ValueName: ""; ValueData: "Sdl plugin file type"; Flags: uninsdeletekey
Root: HKCR; Subkey: "Sdl Plugin Installer\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\Sdl.Community.SdlPluginInstaller.exe,0"
Root: HKCR; Subkey: "Sdl Plugin Installer\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\Sdl.Community.SdlPluginInstaller.exe"" ""%1"""
