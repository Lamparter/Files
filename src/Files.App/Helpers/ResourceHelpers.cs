// Copyright (c) Files Community
// Licensed under the MIT License.

using System.Collections.Concurrent;
using Microsoft.UI.Xaml.Markup;
using Windows.ApplicationModel.Resources;

namespace Files.App.Helpers
{
	[MarkupExtensionReturnType(ReturnType = typeof(string))]
	public sealed partial class ResourceString : MarkupExtension
	{
		private static readonly ResourceLoader resourceLoader = new();

		private static readonly ConcurrentDictionary<string, string> cachedResources = new();

		public string Name { get; set; } = string.Empty;

		protected override object ProvideValue()
		{
			if (cachedResources.TryGetValue(Name, out var value))
			{
				return value;
			}

			value = resourceLoader.GetString(Name);
			cachedResources[Name] = value ?? string.Empty;
			return value;
		}
	}
}
