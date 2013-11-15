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
@echo     (as example I assume the user inputs: \\websrv\share\unimainz)
@echo  2) Script validates if the directory \\websrv\share\unimainz
@echo     contains following files and folders:
@echo     - DIR "App_Code"
@echo     - DIR "App_Code\common_code"
@echo     - DIR "App_Code\individual_code"
@echo     - DIR "externalsettings"
@echo     - DIR "App_Themes"
@echo     - DIR "masterpages"
@echo     - DIR "sscr"
@echo     - FILE "Web.Config"
@echo     - FILE "Global.asax"
@echo     - FILE "default.aspx"
@echo  3) Scripts prompts user to input the full path to the target directory
@echo     where the symbolic links will be created
@echo     (as example I assume the user inputs: C:\inetpub\exampleApp)
@echo  4) Script validates if the target directory is empty
@echo  5) Script creates in the directory C:\inetpub\exampleApp following subdirectories and files:
@echo     - DIR "App_Code"
@echo     - DIR "logs"
@echo  6) Script prompts unser to input the full path to the directory where 
@echo     content files and web.sitemap are stored
@echo     (as example I assume the user inputs: \\flsrv\share\unimainz_content)
@echo  7) Script checks if this directory (\\flsrv\share\unimainz_content)
@echo     contains subdirectory "content" with file web.sitemap and file web.config
@echo     -^> if there is no web.sitemap file, it will be created with following content:
@echo          ^<?xml version^="1.0" encoding="utf-8" ?^>
@echo          ^<siteMap xmlns^="http://schemas.microsoft.com/AspNet/SiteMap-File-1.0" ^>
@echo            ^<siteMapNode title^="Placeholder" url="~/content/052.aspx" description=" " ^>
@echo            ^</siteMapNode^>
@echo          ^</siteMap^>
@echo     -^> if there is no web.config file, it will be created with following content:
@echo          ^<?xml version^="1.0" ?^>
@echo          ^<configuration ^>
@echo            ^<system.web ^>
@echo                ^<!--
@echo                   Make changes to this file (f.e add new line  to this comment)
@echo                   to force the webapplication to recompile it's top-level classes
@echo                --^>
@echo            ^</system.web^>
@echo          ^</configuration^>
@echo  8) Script checks if this directory (\\flsrv\share\unimainz_content) 
@echo     contains subdirectories
@echo     - "individual_code"
@echo     - "externalsettings"
@echo  9) Script copies all files and directories: 
@echo     - from \\websrv\share\unimainz\App_Code\individual_code\* nach \\flsrv\share\unimainz_content\App_Code\individual_code\*
@echo     - from \\websrv\share\unimainz\App_Code\externalsettings\* nach \\flsrv\share\unimainz_content\App_Code\externalsettings\*
@echo  10) Script creates following symbolic links:
@echo     - cd C:\inetpub\exampleApp
@echo     - mklink /D App_Themes \\websrv\share\unimainz\App_Themes
@echo     - mklink /D externalsettings \\flsrv\share\unimainz_content\externalsettings
@echo     - mklink Web.Config \\websrv\share\unimainz\Web.Config
@echo     - mklink Global.asax \\websrv\share\unimainz\Global.asax
@echo     - mklink default.aspx \\websrv\share\unimainz\default.aspx
@echo     - cd App_Code
@echo     - mklink /D common_code \\websrv\share\unimainz\App_Code\common_code
@echo     - mklink /D individual_code \\flsrv\share\unimainz_content\individual_code
@echo  11) Script prompts user to input the name of the website
@echo     (as example I assume the user inputs: unimainzWebSite1)
@echo  12) for the website unimainzWebSite1 script checks following:
@echo      - if the applications pool identity has read permissions for \\websrv\share\unimainz
@echo      - if the applications pool identity has read permissions for C:\inetpub\exampleApp
@echo      - if the applications pool identity has read permissions for \\flsrv\share\unimainz_content\
@echo  13) for the website unimainzWebSite1 script creates following virtual directories:
@echo      - "content" pointing to "\\flsrv\share\unimainz_content\content"
@echo      - "masterpages" pointing to \\websrv\share\unimainz\masterpages
@echo      - "sscr" pointing to \\websrv\share\unimainz\sscr
@echo.
@echo.
@pause


