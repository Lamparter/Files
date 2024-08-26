// Copyright (c) 2018-2024 Files Community
// Licensed under the MIT License. See the LICENSE file in the root directory.

namespace Files.App.ViewModels.Dialogs
{
	public sealed class FileTooLargeDialogViewModel: ObservableObject
	{
		public IEnumerable<string> Paths { get; private set; }

		public FileTooLargeDialogViewModel(IEnumerable<string> paths) 
		{ 
			Paths = paths;
		}
	}
}
