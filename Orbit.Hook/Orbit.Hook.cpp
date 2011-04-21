/*
** Sinapse Global Hooking Routines
**
** siwu
*/

#define _WIN32_WINNT		0x0501
//#include <windows.h>
//#include <stdio.h>
//#include <vector>
#include "Orbit.Hook.h"

//#define MAX_KEYS	255

#pragma data_seg(".SHARDATA")
static HWND					gs_hWndServer = NULL;
//static int					g_modifiers = 0;
#pragma data_seg()
#pragma comment(linker, "/section:.SHARDATA,rws")

HINSTANCE			g_hInstance = NULL;
HHOOK				g_mouseHook = NULL;
//HHOOK				g_keyboardHook = NULL;
//bool				g_Capture = false;

//std::vector<int>	g_registeredKeys;
int hotKey;


extern "C"
BOOL WINAPI	DllMain(HANDLE hModule, DWORD ul_reason_for_call, LPVOID lpReserved)
{
	switch (ul_reason_for_call)
	{
		case DLL_PROCESS_ATTACH:
			g_hInstance = (HINSTANCE)hModule;
			return (TRUE);
		case DLL_PROCESS_DETACH:
			if (gs_hWndServer != NULL)
				StopHook(gs_hWndServer);
			return (TRUE);
	}

    return (TRUE);
}

bool StartHook(HWND hWnd)
{
	if (gs_hWndServer != NULL || hWnd == NULL || g_hInstance == NULL)
		return (false);
	hotKey=4;
	gs_hWndServer = hWnd;

	g_mouseHook = SetWindowsHookEx(WH_MOUSE_LL, (HOOKPROC)MouseHookProc, g_hInstance, 0);
	//g_keyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, (HOOKPROC)LowLevelKeyboardHookProc, g_hInstance, 0);
	//if (g_keyboardHook == NULL || g_mouseHook == NULL)
	/*if (g_mouseHook == NULL)
	{
		UnhookWindowsHookEx(g_mouseHook);
		//UnhookWindowsHookEx(g_keyboardHook);
		return (false);
	}*/
	return (true);
}

bool StopHook(HWND hWnd)
{
	if (hWnd != gs_hWndServer)
		return (false);
	//EndKeyCapture();
	//bool mouseUnhooked = UnhookWindowsHookEx(g_keyboardHook) & 1;
	bool mouseUnhooked = UnhookWindowsHookEx(g_mouseHook) & 1;
	//bool keyboardUnhooked = UnhookWindowsHookEx(g_keyboardHook) & 1;
	//if (mouseUnhooked == true && keyboardUnhooked == true)
	if (mouseUnhooked == true)
		gs_hWndServer = NULL;
	//return (mouseUnhooked & keyboardUnhooked);
	return (mouseUnhooked);
}

void SetShortcutKey(int vKey)
{
	hotKey=vKey;
	//MessageBox(0, (LPCTSTR)vKey, "HookDLL", 0);
}

/*void	RegisterKey(int vkCode)
{
	g_registeredKeys.push_back(vkCode);
}*/

/*void	UnregisterKey(int vkCode)
{
	for (std::vector<int>::iterator	it = g_registeredKeys.begin(); it != g_registeredKeys.end(); ++it)
		if ((*it) == vkCode)
		{
			g_registeredKeys.erase(it);
			return;
		}
}*/

/*void	BeginKeyCapture()
{
	g_Capture = true;
}*/

/*void	EndKeyCapture()
{
	g_Capture = false;
}*/

/*bool	CheckForModifiers(int vkCode, int Up)
{
	if (vkCode == VK_RCONTROL || vkCode == VK_LCONTROL)
	{
		if (Up)
			g_modifiers &= ~NetModControl;
		else
			g_modifiers |= NetModControl;
		return (true);
	}
	else if (vkCode == VK_RMENU || vkCode == VK_LMENU)
	{
		if (Up)
			g_modifiers &= ~NetModAlt;
		else
			g_modifiers |= NetModAlt;
		return (true);
	}
	else if (vkCode == VK_RSHIFT || vkCode == VK_LSHIFT)
	{
		if (Up)
			g_modifiers &= ~NetModShift;
		else
			g_modifiers |= NetModShift;
		return (true);
	}

	return (false);
}*/

/*bool	IsHotkey(int vkCode)
{
	for (std::vector<int>::iterator	it = g_registeredKeys.begin(); it != g_registeredKeys.end(); ++it)
		if ((*it) == vkCode)
			return (true);
	return (false);
}*/
bool IsHotkey(int vkCode)
{
	//MessageBox(0, "bleh", "checking", 0);
	switch(hotKey)
	{
		case 1:
			// left mouse button
			if(vkCode==WM_LBUTTONUP
				|| vkCode==WM_LBUTTONDOWN
				|| vkCode==WM_LBUTTONDBLCLK)
				return true;
			break;
		case 2:
			// right mouse button
			if(vkCode==WM_RBUTTONUP
				|| vkCode==WM_RBUTTONDOWN
				|| vkCode==WM_RBUTTONDBLCLK)
				return true;
			
			break;
		case 4:
			// middle mouse button
			if(vkCode==WM_MBUTTONUP
				|| vkCode==WM_MBUTTONDOWN
				|| vkCode==WM_MBUTTONDBLCLK)
				return true;
			break;
		case 5:
			// action 1
			if(vkCode==WM_XBUTTONUP
				|| vkCode==WM_XBUTTONDOWN
				|| vkCode==WM_XBUTTONDBLCLK)
				return true;
			break;
		case 6:
			// action 2
			if(vkCode==WM_XBUTTONUP
				|| vkCode==WM_XBUTTONDOWN
				|| vkCode==WM_XBUTTONDBLCLK)
				return true;
			break;
	}
	return false;
}

bool ShouldNotify(int vkCode)
{
	if(vkCode==WM_XBUTTONUP
		|| vkCode==WM_MBUTTONUP
		|| vkCode==WM_LBUTTONUP
		|| vkCode==WM_RBUTTONUP)
		return true;
	return false;
}

LRESULT CALLBACK MouseHookProc(int nCode, WPARAM wParam, LPARAM lParam)
{
	if (nCode == HC_ACTION)
	{
		//MessageBox(0, "checking code", "HookDLL", 0);
		//PMOUSEHOOKSTRUCT mouse = (PMOUSEHOOKSTRUCT)lParam;
		//PostMessage(gs_hWndServer, WM_GLOBALMOUSEMOVE, mouse->pt.x, mouse->pt.y);
		if(IsHotkey((int)wParam))
		{
			//MessageBox(0, "got hotkey", "HookDLL", 0);
			if(ShouldNotify((int)wParam))
				PostMessage(gs_hWndServer, WM_GLOBALMOUSECLICK, 0, 0);
			return true;
		}
	}

	return (CallNextHookEx(g_mouseHook, nCode, wParam, lParam));
}

/*LRESULT CALLBACK LowLevelKeyboardHookProc(int nCode, WPARAM wParam, LPARAM lParam)
{
	if (nCode == HC_ACTION)
	{
		PKBDLLHOOKSTRUCT	key = (PKBDLLHOOKSTRUCT)lParam;
		int	pressed = 0;
		if (wParam == WM_SYSKEYUP || wParam == WM_KEYUP)
			pressed = 1;
		CheckForModifiers(key->vkCode, pressed);
		if (g_Capture == true)
		{
			PostMessage(gs_hWndServer, WM_GLOBALKEYCAPTURE, wParam, key->vkCode);
			return (TRUE);
		}
		else if (IsHotkey(key->vkCode | g_modifiers) == true)
		{
			PostMessage(gs_hWndServer, WM_HOTKEY, pressed, key->vkCode | g_modifiers);
			return (TRUE);
		}
	}

	return (CallNextHookEx(g_keyboardHook, nCode, wParam, lParam));
}*/