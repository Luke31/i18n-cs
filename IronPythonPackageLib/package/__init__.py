# -*- coding: utf-8 -*-

import locale
import gettext
import os

current_locale, encoding = locale.getdefaultlocale()
_ = gettext.translation('package', 'locale', [current_locale], fallback = True).ugettext #unicode gettext