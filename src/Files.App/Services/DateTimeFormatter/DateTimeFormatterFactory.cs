// Copyright (c) 2018-2024 Files Community
// Licensed under the MIT License. See the LICENSE file in the root directory.

using System;

namespace Files.App.Services.DateTimeFormatter
{
	public sealed class DateTimeFormatterFactory : IDateTimeFormatterFactory
	{
		public IDateTimeFormatter GetDateTimeFormatter(DateTimeFormats dateTimeFormat) => dateTimeFormat switch
		{
			DateTimeFormats.Application => new ApplicationDateTimeFormatter(),
			DateTimeFormats.System => new SystemDateTimeFormatter(),
			DateTimeFormats.Universal => new UniversalDateTimeFormatter(),
			_ => throw new ArgumentException(nameof(dateTimeFormat)),
		};
	}
}
