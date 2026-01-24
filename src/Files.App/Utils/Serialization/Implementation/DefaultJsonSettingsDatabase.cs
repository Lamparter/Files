// Copyright (c) Files Community
// Licensed under the MIT License.

using Files.App;
using Files.App.Extensions;
using Files.App.Helpers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.Json;
using System.Threading;

namespace Files.App.Utils.Serialization.Implementation
{
	internal class DefaultJsonSettingsDatabase : IJsonSettingsDatabase
	{
		private IDialogService DialogService { get; } = Ioc.Default.GetRequiredService<IDialogService>();

		protected ISettingsSerializer SettingsSerializer { get; }

		protected IJsonSettingsSerializer JsonSettingsSerializer { get; }

		private int _isShowingInvalidSettingsDialog;

		public DefaultJsonSettingsDatabase(ISettingsSerializer settingsSerializer, IJsonSettingsSerializer jsonSettingsSerializer)
		{
			SettingsSerializer = settingsSerializer;
			JsonSettingsSerializer = jsonSettingsSerializer;
		}

		protected IDictionary<string, object?> GetFreshSettings()
		{
			string data = SettingsSerializer.ReadFromFile();

			if (string.IsNullOrWhiteSpace(data))
			{
				data = "null";
			}

			try
			{
				return JsonSettingsSerializer.DeserializeFromJson<ConcurrentDictionary<string, object?>?>(data) ?? new();
			}
			catch (Exception ex)
			{
				// Occurs if the settings file has invalid json
				HandleInvalidSettings(ex);
				return JsonSettingsSerializer.DeserializeFromJson<ConcurrentDictionary<string, object?>?>("null") ?? new();
			}
		}

		private async void HandleInvalidSettings(Exception exception)
		{
			if (JsonSettingsSerializer is null || SettingsSerializer is null)
			{
				return;
			}

			if (Interlocked.Exchange(ref _isShowingInvalidSettingsDialog, 1) == 1)
			{
				return;
			}

			try
			{
				SaveSettings(new ConcurrentDictionary<string, object?>());
			}
			catch (Exception resetEx)
			{
				Debug.WriteLine(resetEx);
			}

			try
			{
				DialogDisplayHelper.ShowDialogAsync(
					"Failed to reload settings",
					exception.Message,
					"OK").Wait();
			}
			finally
			{
				_isShowingInvalidSettingsDialog = 0;
			}
		}

		protected bool SaveSettings(IDictionary<string, object?> data)
		{
			var jsonData = JsonSettingsSerializer.SerializeToJson(data);

			return SettingsSerializer.WriteToFile(jsonData);
		}

		public virtual TValue? GetValue<TValue>(string key, TValue? defaultValue = default)
		{
			var data = GetFreshSettings();

			if (data.TryGetValue(key, out var objVal))
			{
				return GetValueFromObject<TValue>(objVal) ?? defaultValue;
			}
			else
			{
				SetValue(key, defaultValue);
				return defaultValue;
			}
		}

		public virtual bool SetValue<TValue>(string key, TValue? newValue)
		{
			var data = GetFreshSettings();

			if (!data.TryAdd(key, newValue))
				data[key] = newValue;

			return SaveSettings(data);
		}

		public virtual bool RemoveKey(string key)
		{
			var data = GetFreshSettings();

			return data.Remove(key) && SaveSettings(data);
		}

		public bool FlushSettings()
		{
			// The settings are always flushed automatically, return true.
			return true;
		}

		public virtual bool ImportSettings(object? import)
		{
			try
			{
				// Try convert
				var data = (IDictionary<string, object?>?)import;
				if (data is null)
				{
					return false;
				}

				// Serialize
				var serialized = JsonSettingsSerializer.SerializeToJson(data);

				// Write to file
				return SettingsSerializer.WriteToFile(serialized);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				Debugger.Break();

				return false;
			}
		}

		public object? ExportSettings()
		{
			return GetFreshSettings();
		}

		protected static TValue? GetValueFromObject<TValue>(object? obj)
		{
			if (obj is JsonElement jElem)
			{
				try
				{
					return jElem.Deserialize<TValue>();
				}
				catch (JsonException)
				{
					// Deserialization failed (e.g., incompatible type in settings file)
					// Return null to fall back to the default value
					return default;
				}
			}

			return (TValue?)obj;
		}
	}
}
