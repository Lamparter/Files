// Copyright (c) 2018-2024 Files Community
// Licensed under the MIT License. See the LICENSE file in the root directory.

namespace Files.App.Services
{
	/// <inheritdoc cref="IAddItemService"/>
	internal sealed class AddItemService : IAddItemService
	{
		private List<ShellNewEntry> _cached;

		public async Task InitializeAsync()
		{
			_cached = await ShellNewEntryExtensions.GetNewContextMenuEntries();
		}

		public List<ShellNewEntry> GetEntries()
		{
			return _cached;
		}
	}
}
