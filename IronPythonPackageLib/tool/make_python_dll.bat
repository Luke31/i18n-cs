@echo off

rem ====== IronPython のパス設定 ===============
cd /d %~dp0
set PYTHON_DIR=C:\Program Files (x86)\IronPython 2.7\
set IPY=%PYTHON_DIR%ipy.exe
set PYC="%IPY%" "%PYTHON_DIR%Tools\Scripts\pyc.py" /platform:x64
set BIN_DIR=%~dp0\..\..\IronPythonCsharp\bin\Release\
set TEMP_DIR=%BIN_DIR%tmp\
set RESOURCE_DIR=%~dp0\resources\
set RC="C:\Program Files (x86)\Windows Kits\8.1\bin\x86\rc.exe"
set RH="%RESOURCE_DIR%ResHacker.exe"
mkdir %BIN_DIR%
mkdir %TEMP_DIR%

rem ====== それぞれの dll に対応するフォルダを指定 ========
set SAMPLE_DIR=..\sample\
set STD_DIR=%PYTHON_DIR%Lib\
set STD_ENCOD_DIR=%PYTHON_DIR%Lib\encodings\

rem ====== stdipy に含めるモジュールを指定 ======
set STD_MODULES=(locale codecs gettext os ntpath genericpath glob fnmatch shutil site types stat warnings linecache UserDict UserList UserString abc _abcoll ConfigParser traceback optparse types textwrap string xmlrpclib base64 struct random zipfile tempfile uuid pickle StringIO copy _weakrefset collections keyword heapq bisect __future__ hashlib weakref functools)

rem ======= 含める .py ファイル一覧を取得 ========
setlocal enabledelayedexpansion
set SAMPLE=
for /f "usebackq tokens=*" %%i in (`dir "%SAMPLE_DIR%*.py" /B`) do (
  set SAMPLE=!SAMPLE! %%i
)

set STD=
for %%M in %STD_MODULES% do (
  set STD=!STD! %%M.py
)

set STD_ENCOD=
for /f "usebackq tokens=*" %%i in (`dir "%STD_ENCOD_DIR%*.py" /B`) do (
  set STD_ENCOD=!STD_ENCOD! %%i
)

rem ======== dll 生成 =============
cd /d %~dp0%SAMPLE_DIR%
%PYC% /out:%TEMP_DIR%Sample %SAMPLE%
cd %STD_DIR%
%PYC% /out:%TEMP_DIR%stdipy %STD%
cd %STD_ENCOD_DIR%
%PYC% /out:%TEMP_DIR%stdipyencod %STD_ENCOD%

endlocal

rem ========= アセンブリ情報をセットし直す =========
cd %TEMP_DIR%
for %%d in (Sample stdipy stdipyencod) do (
  rem ========= .rc ファイルを .res ファイルに変換する =========
  %RC% %RESOURCE_DIR%%%d.rc
  rem ========= バージョン情報を削除した dll を作成する =========
  %RH% -delete %%d.dll, _%%d.dll, VersionInfo, 1, 0
  if exist %BIN_DIR%%%d.dll del %BIN_DIR%%%d.dll
  rem ========= バージョン情報を削除した dll にアセンブリ情報を追加する =========
  %RH% -addoverwrite _%%d.dll, %BIN_DIR%%%d.dll, %RESOURCE_DIR%%%d.res,,,
)

cd %~dp0
