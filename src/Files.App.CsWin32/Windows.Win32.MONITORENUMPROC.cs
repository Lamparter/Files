// Copyright (c) 2018-2024 Files Community
// Licensed under the MIT License. See the LICENSE file in the root directory.

using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.Graphics.Gdi;

[UnmanagedFunctionPointer(CallingConvention.Winapi)]
public unsafe delegate BOOL MONITORENUMPROC([In] HMONITOR param0, [In] HDC param1, [In][Out] RECT* param2, [In] LPARAM param3);
