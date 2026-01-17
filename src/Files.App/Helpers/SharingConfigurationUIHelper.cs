// Copyright (c) Files Community
// Licensed under the MIT License.

using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;
using Windows.Win32.UI.Shell;

namespace Files.App.Helpers
{
	internal static partial class SharingConfigurationUIHelper
	{

		[Guid("14aa4ab8-abe3-4a07-a290-1d5dccdd2fc2")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		interface ISharingConfigurationUI
		{
			// ntshrui!CSharingConfiguration::CanShareItems(IShellItemArray *)
			HRESULT __CanShareItems__Stub();

			// ntshrui!CSharingConfiguration::ShowShareItemsUI(HWND *, IShellItemArray *)
			HRESULT ShowShareItemsUI(HWND hwnd, IShellItemArray items);

			// ntshrui!...
		}

		// CLSID_SharingConfigurationManager
		// new SharingConfigurationManager()
		private static readonly Guid SharingConfigurationManagerClsid = new("0A3F6F8E-0B40-4C6A-BA12-8DB83C4BFD1D");

		public static void TryShowShareUI(nint hwnd, string path)
		{
			if (string.IsNullOrWhiteSpace(path))
				return;

			unsafe
			{
				var idlist = PInvoke.ILCreateFromPath(path);
				if (idlist is null)
					return;

				SHCreateShellItemArrayFromIDLists(1, &idlist, out var arrayPtr);

				var hr = PInvoke.CoCreateInstance(
					in SharingConfigurationManagerClsid,
					null,
					CLSCTX.CLSCTX_INPROC_SERVER,
					typeof(ISharingConfigurationUI).GUID,
					out var obj);
				if (hr == 0 && obj is not null && arrayPtr != null)
				{
					var config = (ISharingConfigurationUI)Marshal.GetObjectForIUnknown((IntPtr)obj);
					var shellItemArray = (IShellItemArray)Marshal.GetObjectForIUnknown(new IntPtr(*arrayPtr));
					config.ShowShareItemsUI((HWND)hwnd, shellItemArray);
					Marshal.ReleaseComObject(shellItemArray);
					Marshal.ReleaseComObject(config);
				}

				PInvoke.ILFree(idlist);
			}
		}

		// CsWin32 is stupid
		[DllImport("SHELL32.dll", ExactSpelling = true), DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		[SupportedOSPlatform("windows6.0.6000")]
		public static extern unsafe HRESULT SHCreateShellItemArrayFromIDLists(uint cidl, Windows.Win32.UI.Shell.Common.ITEMIDLIST** rgpidl, out Windows.Win32.UI.Shell.IShellItemArray** ppsiItemArray);
	}
}
