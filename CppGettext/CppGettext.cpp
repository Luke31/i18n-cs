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
#include <locale>
#include <codecvt>
#include <string>

//Convert string in current system MultiByte code-page to widechar
const std::wstring stow(const std::string& str)
{
	if (str.empty()) return L"";
	int size_needed = MultiByteToWideChar(CP_ACP, 0, &str[0], (int)str.size(), NULL, 0); //CP_UTF8
	std::wstring wstrTo(size_needed, 0);
	MultiByteToWideChar(CP_ACP, 0, &str[0], (int)str.size(), &wstrTo[0], size_needed); //CP_UTF8, CP_ACP
	return wstrTo;
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
		std::wcout << setlocale(LC_ALL, NULL) << std::endl << std::endl; //Read locale from setlocale
		return 0;
	}
}

//wmain: http://stackoverflow.com/a/3299860/2003325
int wmain(int argc, wchar_t* argv[])
{
	//Set output Unicode without BOM
	_setmode(_fileno(stdout), _O_U16TEXT); //_O_WTEXT (with BOM)
	//stdout may now be written to file (First character must be ASCII if output is written to file)
	std::wcout << L"Enabling Unicode support" << std::endl;

	//Set locale
	setlocale(LC_ALL, ""); //Set locale to environment
	PrintUserLocale();

	//Gettext
	textdomain("cppgettext");
	bindtextdomain("cppgettext", "locale");

	//Hello world
	std::wcout << "--Hello World in local language:--" << std::endl;
	std::string s = _("Hello world");
	std::wstring ws = stow(s);
	std::wcout << ws << std::endl << std::endl;
	
	//Short hello world with ASCII Debug prefix
	std::wcout << "--Hello World in local language with ASCII Debug prefix:--" << std::endl;
	std::wcout << stow(_("Debug Hello world")) << std::endl << std::endl;

	//Output fixed japanese string from source code (only possible if _O_U16TEXT is set)
	std::wcout << "--Hello in Japanese from source code (Console output only visible on Japanese systems, file output any system):--" << std::endl;
	std::wstring wstr = L"こんにちは from Source Code";
	std::wcout << wstr << std::endl;

	return 0;
}

