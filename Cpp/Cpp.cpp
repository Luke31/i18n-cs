// Cpp.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "resource.h"

//char * LoadStringFromResource(unsigned int id)
//{
//	// szBuffer is a globally pre-defined buffer of some maximum length
//	LoadString(ghInst, id, szBuffer, bufferSize);
//	// yes, I know that strdup has problems. But you get the idea.
//	return strdup(szBuffer);
//}

int _tmain(int argc, _TCHAR* argv[])
{
	LCID lcid = GetUserDefaultLCID();
	WCHAR strNameBuffer[LOCALE_NAME_MAX_LENGTH];
	DWORD error = ERROR_SUCCESS;

	// Get the name for locale 0x10407 (German (German), with phonebook sort)
	if (LCIDToLocaleName(lcid, strNameBuffer, LOCALE_NAME_MAX_LENGTH, 0) == 0)
	{
		// There was an error
		error = GetLastError();
	}
	else
	{
		// Success, display the locale name we found
		wprintf(L"Locale: %s\n", strNameBuffer);
	}

	//int resourceName = IDS_HW;
	////_TCHAR* test = LoadString(NULL, TEXT("HW"), 

	//char* errMem = LoadStringFromResource(IDS_ERROR_MEMORY);
	//char* errText = LoadStringFromResource(IDS_ERROR_TEXT);
	//MessageBox(NULL, errMem, errText, MB_OK | MB_ICONERROR);
	//free(errMem);
	//free(errText);

	//HRSRC hResource;
	//HGLOBAL hResourceData;
	//LPCSTR usageStr, buf;
	//DWORD dwResourceSize, bufSize;
	//hResource = FindResource(NULL, MAKEINTRESOURCE(IDS_HW), IDS_HW); //TEXT("TEXT")
	//hResourceData = LoadResource(NULL, hResource);
	//usageStr = (LPCSTR)LockResource(hResourceData);
	//dwResourceSize = SizeofResource(NULL, hResource);
	//bufSize = dwResourceSize + sizeof(char);
	//buf = (LPCSTR)malloc(bufSize);
	//memset((LPVOID)buf, 0, bufSize);
	//memcpy((LPVOID)buf, usageStr, dwResourceSize);
	////DBGPRINT(LOG_LEVEL_INFO, "%hs", buf);
	//free((LPVOID)buf);

	
	//printf("%s\n", LoadStringFromResource());

	system("PAUSE");
	return 0;
}
