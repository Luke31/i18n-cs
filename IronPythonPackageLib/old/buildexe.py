# -*- coding: utf-8 -*-

import shutil, errno
import pyc

pyc.Main(['/main:main.py']) #generates main.exe and main.dll

#copy prereqs/* to /bin
#move main.dll and main.exe to /bin

