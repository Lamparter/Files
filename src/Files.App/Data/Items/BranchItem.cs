// Copyright (c) 2018-2024 Files Community
// Licensed under the MIT License. See the LICENSE file in the root directory.

namespace Files.App.Data.Items
{
	public record BranchItem(string Name, bool IsHead, bool IsRemote, int? AheadBy, int? BehindBy);
}
