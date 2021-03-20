@echo off
SET DEST_DIR=C:\ProgramData\blnk
md "%DEST_DIR%"
copy /y blnk.exe "%DEST_DIR%\"
copy /y lnk-to-blnk.vbs "%DEST_DIR%\"
if NOT ["%errorlevel%"]==["0"] pause
explorer "%DEST_DIR%"
pause