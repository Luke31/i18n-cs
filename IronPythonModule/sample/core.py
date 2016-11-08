# -*- coding: utf-8 -*-

import locale
import gettext
##import os #not really needed

current_locale, encoding = locale.getdefaultlocale()

print(current_locale)

##current_locale = 'ja' #override current locale?

locale_path = 'locale/' # path from execution point (main.py)
_ = gettext.translation ('messages', locale_path, [current_locale], fallback = True).ugettext

def runModule():
	print(_('Hello world'))

