# i18n-cs
This is a collection of Visual Studio 2015 Applications to demonstrate internationalization in C# Code, WinForms, WPF and WIX installers.
The default language is English and translations to Japanese are added. This approach is recommended so systems other than Japanese or English will see an English interface.

**For the specific documentation see the [Github Wiki of this project](https://github.com/Luke31/i18n-cs/wiki)**

##General tips and how to ditch the most common pitfalls of globalization and localization:

* Be aware how your application will react, if a system with an unsupported locale will run your application. Best practice to have a **fallback to the default language**.

* Be aware which default language you set. Best practice is to use **English**.

* Write your source code including comments and messages in English where possible. If the English skills of your developers are insufficient, allow comments in their local language (Japanese). Default language for the messages should be English however.

* Dates and time: Be aware how you calculate date-and time differences in your code.

* Dates, times and currencies: Display them according either to the users-locale or in a fixed format. Currencies may be round differently depending on the type of currency or locale.

* Daylight-saving time (summer/winter time): Some countries change their time-zone in spring and autumn between usually 2am and 3am. Be aware of this especially for e.g. crone-jobs and backup-tasks.

* If you have formatted strings with inserted values, do not rely on the sequence of the values. The sequence may change depending on the language, for example:

	English: "You have a {color} {car}"
	
	French: "Vous avez une {car} {color}"
	
	Source: [Translation of formatted strings](http://inventwithpython.com/blog/2014/12/20/translate-your-python-3-program-with-the-gettext-module/#comment-205535)
	
