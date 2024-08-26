// Copyright (c) 2018-2024 Files Community
// Licensed under the MIT License. See the LICENSE file in the root directory.

using Microsoft.UI.Xaml;

namespace Files.App.Data.Parameters
{
	public sealed class PropertiesPageNavigationParameter
	{
		public CancellationTokenSource CancellationTokenSource;

		public object Parameter;

		public IShellPage AppInstance;

		public Window Window;
	}
}
