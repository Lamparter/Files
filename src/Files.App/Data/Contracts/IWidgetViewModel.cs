// Copyright (c) 2018-2024 Files Community
// Licensed under the MIT License. See the LICENSE file in the root directory.

using Microsoft.UI.Xaml.Controls;

namespace Files.App.Data.Contracts
{
	public interface IWidgetViewModel : IDisposable
	{
		string WidgetName { get; }

		string WidgetHeader { get; }

		string AutomationProperties { get; }

		bool IsWidgetSettingEnabled { get; }

		bool ShowMenuFlyout { get; }

		MenuFlyoutItem? MenuFlyoutItem { get; }

		Task RefreshWidgetAsync();
	}
}
