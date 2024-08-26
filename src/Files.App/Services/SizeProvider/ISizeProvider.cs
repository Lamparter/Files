// Copyright (c) 2018-2024 Files Community
// Licensed under the MIT License. See the LICENSE file in the root directory.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Files.App.Services.SizeProvider
{
	public interface ISizeProvider : IDisposable
	{
		event EventHandler<SizeChangedEventArgs> SizeChanged;

		Task CleanAsync();
		Task ClearAsync();

		Task UpdateAsync(string path, CancellationToken cancellationToken);
		bool TryGetSize(string path, out ulong size);
	}
}
