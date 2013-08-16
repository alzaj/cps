@echo What this script does:
@echo ======================
@echo.
@echo  1) Script checks if on the webserver symbolic links
@echo     on remote shares to remote shares are enabled:
@echo     -^> fsutil behavior query SymlinkEvaluation
@echo        If they are disabled script enables them: 
@echo        -^> fsutil behavior set SymlinkEvaluation R2R:1
@echo  1) Script prompts user to input the full path to the network share
@echo     where the original files and directories reside
@echo     (as example I assume the user inputs: //websrv/share/unimainz)
@echo  2) Script validates if the directory //websrv/share/unimainz
@echo     contains following files and folders:
@echo     - DIR "App_Code"
@echo     - DIR "App_Code\common_code"
@echo     - DIR "App_Themes"
@echo     - DIR "masterpages"
@echo     - DIR "servicescripts"
@echo     - FILE "Web.Config"
@echo  3) Scripts prompts user to input the full path to the target directory
@echo     where the symbolic links will be created3
@echo  4) Script validates if the target directory is empty
@echo  5) Script creates in the target directory following directories and files:
@echo     - DIR "App_Code"
@echo     - DIR "externalsettings"
@echo     - FILE "web.sitemap" with this content:
@echo        ^<?xml version^="1.0" encoding="utf-8" ?^>
@echo        ^<siteMap xmlns^="http://schemas.microsoft.com/AspNet/SiteMap-File-1.0" ^>
@echo          ^<siteMapNode title^="Placeholder" url="~/052.aspx" description=" " ^>
@echo          ^</siteMapNode^>
@echo        ^</siteMap^>
@echo     - FILE "externalsettings\externalAppSettings.config" with this content:
@echo        ^<?xml version^="1.0" encoding="utf-8" ?^>
@echo        ^<appSettings^>
@echo          ^<add key="IsDebugMode" value="1" /^>
@echo        ^</appSettings^>
@echo.
@echo.
@pause