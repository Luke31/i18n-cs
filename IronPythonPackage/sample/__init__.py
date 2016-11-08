import locale
import gettext
import os

current_locale, encoding = locale.getdefaultlocale()

##current_locale = 'ja' #override current locale?
print(current_locale)

localedir  = os.path.join(os.path.abspath(os.path.dirname(__file__)), 'locale')
translate = gettext.translation('messages', localedir, [current_locale], fallback = True)
_ = translate.ugettext #unicode gettext