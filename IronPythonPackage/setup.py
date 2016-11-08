# -*- coding: utf-8 -*-

from distutils.core import setup

setup(name='Sample',
      version='1.0',
      description='Sample application for localization in IronPython with .exe result',
      author='Lukas Schmid',
      author_email='lukas.m.schmid@gmail.com',
      #url='https://www.python.org/sigs/distutils-sig/',
      packages=['sample'],
      #data_files=[('', ['prereqs/IronPython.dll'])] #Too late, only in install
      package_data={'': ['prereqs/IronPython.dll']}
      #entry_points = {
      #  'console_scripts': ['sample-cmd=sample.core:runModule'],
      #}
)

