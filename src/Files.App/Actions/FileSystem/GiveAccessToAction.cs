// Copyright (c) Files Community
// Licensed under the MIT License.

namespace Files.App.Actions
{
	[GeneratedRichCommand]
	internal sealed partial class GiveAccessToAction : ObservableObject, IAction
	{
		private IHomePageContext HomePageContext { get; } = Ioc.Default.GetRequiredService<IHomePageContext>();

		public string Label => "GiveAccessTo"; // temp

		public string Description => "GiveAccessToDescription"; // temp

		public RichGlyph Glyph => new(themedIconStyle: "App.ThemedIcons.Share");

		public bool IsExecutable =>
			HomePageContext.IsAnyItemRightClicked &&
			HomePageContext.RightClickedItem?.Path is not null;

		public bool IsAccessibleGlobally => false;

		public Task ExecuteAsync(object? parameter = null)
		{
			var path = HomePageContext.RightClickedItem?.Path;
			if (string.IsNullOrWhiteSpace(path))
				return Task.CompletedTask;

			SharingConfigurationUIHelper.TryShowShareUI(MainWindow.Instance.WindowHandle, path);
			return Task.CompletedTask;
		}
	}
}
