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
	_setmode(_fileno(stdout), _O_U16TEXT); //_O_WTEXT (with BOM)
	//stdout may now be written to file (First character must be ASCII if output is written to file)
	std::wcout << L"Enabling Unicode support" << std::endl;

	PrintUserLocale();

	//Load multi-lang resource
	std::wstring str = LoadStringW(IDS_HW);

	//Output using wprintf
	wprintf(str.c_str());
	std::wcout << std::endl;

	//Output using wcout
	std::wcout << str << std::endl;

	return 0;
}


//Old unused?
////Set current system locale (http://stackoverflow.com/a/3130688/2003325)
//std::locale::global(std::locale("")); 