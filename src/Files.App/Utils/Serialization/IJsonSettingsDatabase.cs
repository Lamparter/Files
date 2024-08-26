// Copyright (c) 2018-2024 Files Community
// Licensed under the MIT License. See the LICENSE file in the root directory.

namespace Files.App.Utils.Serialization
{
	internal interface IJsonSettingsDatabase
	{
		TValue? GetValue<TValue>(string key, TValue? defaultValue = default);

		bool SetValue<TValue>(string key, TValue? newValue);

		bool RemoveKey(string key);

		bool FlushSettings();

		bool ImportSettings(object? import);

		object? ExportSettings();
	}
}
