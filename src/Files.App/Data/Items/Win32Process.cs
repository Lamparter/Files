// Copyright (c) 2018-2024 Files Community
// Licensed under the MIT License. See the LICENSE file in the root directory.

namespace Files.App.Data.Items
{
	public sealed class Win32Process
	{
		public string Name { get; set; }

		public int Pid { get; set; }

		public string FileName { get; set; }

		public string AppName { get; set; }
	}
}
