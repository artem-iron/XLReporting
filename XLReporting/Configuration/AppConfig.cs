﻿namespace XLReporting.Configuration;

public class AppConfig : IAppConfig
{
    public string? Environment { get; set; }
    public string? LicenseKey { get; set; }
}