// CppGettext.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
//#include <string>
#include <iostream>
#include <io.h> //for setting outputmode UNICODE
#include <fcntl.h> //contains _O_U16TEXT

// Get gettext(), textdomain(), bindtextdomain() declaration.
#include "libintl.h"
// Define shortcut for gettext().
#define _(string) gettext (string)
//#define _(char *) gettext

#include <locale>
#include <codecvt>
#include <string>

std::wstring_convert<std::codecvt_utf8_utf16<wchar_t>> converter;


std::wstring convertToUnicode(std::string narrow_utf8_source_string) {
	//std::string narrow = converter.to_bytes(wide_utf16_source_string);
	std::wcout << L"Start converting..." << std::endl;
	std::wstring wide = converter.from_bytes(narrow_utf8_source_string);
	std::wcout << L"End converting..." << std::endl;
	return wide;
}

int PrintUserLocale() {
	LCID lcid = GetUserDefaultLCID();
	WCHAR strNameBuffer[LOCALE_NAME_MAX_LENGTH];
	DWORD error = ERROR_SUCCESS;
	//Evaluate locale
	if (LCIDToLocaleName(lcid, strNameBuffer, LOCALE_NAME_MAX_LENGTH, 0) == 0)
	{
		error = GetLastError();
		return 1;
	}
	else
	{
		wprintf(L"Locale: %s\n", strNameBuffer);
		return 0;
	}
}

//wmain: http://stackoverflow.com/a/3299860/2003325
int wmain(int argc, wchar_t* argv[])
{
	//START File-Unicode-Support
	//Set output mode, if set wide strings in file e.g. L"あう" work. However gettext doesn't work in _O_U16TEXT
	//If not set, any output to wcout with wide-characters will break the output
	//_setmode(_fileno(stdout), _O_U16TEXT); //_O_WTEXT (with BOM)
										   //stdout may now be written to file (First character must be ASCII if output is written to file)
	std::wcout << L"Enabling Unicode support" << std::endl;
	//END File-Unicode-Support

	setlocale(LC_ALL, ""); //Set locale to environment
	PrintUserLocale();

	//Output using wprintf
	//wprintf(str.c_str());
	//std::wcout << std::endl;

	////Output using wcout
	//std::wcout << str << std::endl;

	textdomain("cppgettext");
	bindtextdomain("cppgettext", "locale");
	//bind_textdomain_codeset("default", "UTF-8");
		
	std::wcout << _("Hello world") << std::endl;
	std::wcout << _("Hello world2") << std::endl;

	//std::wstring test = convertToUnicode(_("Hello world"));

	//std::wcout << convertToUnicode(_("Hello world")) << std::endl;
	
	std::wcout << "s" << std::endl;

	//Output fixed japanese string in code
	std::wstring wstr = L"こんにちは from Source Code - Console output only visible on Japanese systems, file output any system";
	std::wcout << wstr << std::endl;

	std::wcout << "End" << std::endl;

	return 0;
}

