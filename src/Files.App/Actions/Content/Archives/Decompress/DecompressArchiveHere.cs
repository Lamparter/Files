// Copyright (c) 2018-2024 Files Community
// Licensed under the MIT License. See the LICENSE file in the root directory.

namespace Files.App.Actions
{
	internal sealed class DecompressArchiveHere : BaseDecompressArchiveAction
	{
		public override string Label
			=> "ExtractHere".GetLocalizedResource();

		public override string Description
			=> "DecompressArchiveHereDescription".GetLocalizedResource();

		public DecompressArchiveHere()
		{
		}

		public override Task ExecuteAsync(object? parameter = null)
		{
			return DecompressArchiveHereAsync();
		}
	}
}
