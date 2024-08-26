// Copyright (c) 2018-2024 Files Community
// Licensed under the MIT License. See the LICENSE file in the root directory.

namespace Files.App.Data.EventArguments
{
    public record SelectedTagChangedEventArgs(IEnumerable<(string path, bool isFolder)> Items);
}
