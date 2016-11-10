# -*- coding: utf-8 -*-

from sample import _
from package import side

def runModule():
	# Translators: This comment is visible to translators in the .po file
	print(_('Hello world'))

	numb1 = 1
	numb2 = 2
	#Former format string: 'This is a formatted string: %2d %d' % (numb1, numb2)
	#For translated strings: use keys or at least indexes for translaters to change position of replaced strings!
	print(_('This is a formatted string: {n1:2d} {n2:d}').format(n1=numb1, n2=numb2))
	print(_('This is a formatted string: {0:2d} {1:d}').format(numb1, numb2))
	print(_('This is a formatted string: %(n1)2d %(n2)d') % {'n1':numb1, 'n2':numb2})
	
	side.runSide()  
