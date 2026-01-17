// Copyright (c) Files Community
// Licensed under the MIT License.

using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.Shell;
using System.Windows;
using System.Windows.Interop;

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
		// Matches the sample usage: new SharingConfigurationManager()
		private static readonly Guid SharingConfigurationManagerClsid = new("0A3F6F8E-0B40-4C6A-BA12-8DB83C4BFD1D");

		public static void TryShowShareUI(nint hwnd, string path)
		{
			if (string.IsNullOrWhiteSpace(path))
				return;

			unsafe
			{
				var pidl = PInvoke.ILCreateFromPath(path);
				if (pidl is null)
					return;

				var idlist = PInvoke.ILCreateFromPath(path);
				PInvoke.SHCreateShellItemArrayFromIDLists(1, &idlist, out var array);

				var config = (ISharingConfigurationUI)new SharingConfigurationManager();
				config.ShowShareItemsUI(new HWND(hwnd), array);

				PInvoke.ILFree(idlist);
			}
		}
	}
}
