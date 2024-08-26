// Copyright (c) 2018-2024 Files Community
// Licensed under the MIT License. See the LICENSE file in the root directory.

namespace Files.App.Actions
{
	internal sealed class CloseOtherTabsSelectedAction : CloseTabBaseAction
	{
		public override string Label
			=> "CloseOtherTabs".GetLocalizedResource();

		public override string Description
			=> "CloseOtherTabsSelectedDescription".GetLocalizedResource();

		public CloseOtherTabsSelectedAction()
		{
		}

		public override Task ExecuteAsync(object? parameter = null)
		{
			MultitaskingTabsHelpers.CloseOtherTabs(context.SelectedTabItem, context.Control!);

			return Task.CompletedTask;
		}
	}
}
