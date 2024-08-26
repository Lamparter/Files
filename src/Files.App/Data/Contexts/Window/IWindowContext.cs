// Copyright (c) 2018-2024 Files Community
// Licensed under the MIT License. See the LICENSE file in the root directory.

namespace Files.App.Data.Contexts
{
	public interface IWindowContext : INotifyPropertyChanged
	{
		bool IsCompactOverlay { get; }

		/// <inheritdoc cref="IWindowsSecurityService.IsAppElevated"/>
		bool IsRunningAsAdmin { get; }

		/// <inheritdoc cref="IWindowsSecurityService.CanDragAndDrop"/>
		bool CanDragAndDrop { get; }
	}
}
