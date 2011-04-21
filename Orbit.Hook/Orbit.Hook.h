/*
** Sinapse Global Hooking Routines
**
** siwu
*/

#include <windows.h>

enum
{
	WM_GLOBALMOUSECLICK = WM_USER + 5000
	//WM_GLOBALMOUSEMOVE	= WM_USER + 5000,
	//WM_GLOBALKEYPRESS	= WM_USER + 5001,
	//WM_GLOBALKEYCAPTURE	= WM_USER + 5002,
};

/*enum KEY_MODIFIERS
{
	NetModShift		= 1 << 16,
	NetModControl	= 2 << 16,
	NetModAlt		= 4 << 16,
};*/

bool StartHook(HWND hWnd);
bool StopHook(HWND hWnd);
void SetShortcutKey(int vKey);
bool IsHotkey(int vkCode);
//void	BeginKeyCapture();
//void	EndKeyCapture();
LRESULT CALLBACK MouseHookProc(int nCode, WPARAM wParam, LPARAM lParam);
//LRESULT CALLBACK LowLevelKeyboardHookProc(int nCode, WPARAM wParam, LPARAM lParam);