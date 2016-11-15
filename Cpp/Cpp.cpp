// Cpp.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "resource.h" //Enables us to load our resources -> multi-lang strings
#include <string>
#include <iostream>
#include <io.h> //for setting outputmode UNICODE
#include <fcntl.h> //contains _O_U16TEXT

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

std::wstring LoadStringW(unsigned int id)
{
	const wchar_t* p = nullptr;
	int len = ::LoadStringW(nullptr, id, reinterpret_cast<LPWSTR>(&p), 0);
	if (len > 0)
	{
		return std::wstring(p, static_cast<size_t>(len));
	}
	// Return empty string; optionally replace with throwing an exception.
	return std::wstring();
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

	//Load multi-lang resource
	std::wstring str = LoadStringW(IDS_HW);

	//Hello world
	//Output using wprintf
	std::wcout << "--Hello World in local language 1 (wprintf):--" << std::endl;
	wprintf(str.c_str());
	std::wcout << std::endl << std::endl;

	//Output using wcout
	std::wcout << "--Hello World in local language 2 (wcout):--" << std::endl;
	std::wcout << str << std::endl << std::endl;

	//Output fixed japanese string from source code (only possible if _O_U16TEXT is set)
	std::wcout << "--Hello in Japanese from source code (Console output only visible on Japanese systems, file output any system):--" << std::endl;
	std::wstring wstr = L"こんにちは from Source Code";
	std::wcout << wstr << std::endl;

	return 0;
}


//Old unused?
////Set current system locale (http://stackoverflow.com/a/3130688/2003325)
//std::locale::global(std::locale("")); 