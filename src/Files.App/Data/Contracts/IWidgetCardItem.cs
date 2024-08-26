// Copyright (c) 2018-2024 Files Community
// Licensed under the MIT License. See the LICENSE file in the root directory.

using Microsoft.UI.Xaml.Media.Imaging;

namespace Files.App.Data.Contracts
{
	public interface IWidgetCardItem<T>
	{
		T Item { get; }

		BitmapImage Thumbnail { get; }

		Task LoadCardThumbnailAsync();
	}
}
