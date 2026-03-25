using System.Reflection;
using System.Runtime.InteropServices;

// [MANDATORY] The following GUID is used as a unique identifier of the plugin
[assembly: Guid("a1b2c3d4-5678-9abc-def0-112233445566")]

// [MANDATORY] The assembly versioning
//Should be incremented for each new release build of a plugin
[assembly: AssemblyVersion("2.0.0.1")]
[assembly: AssemblyFileVersion("2.0.0.1")]

// [MANDATORY] The name of your plugin
[assembly: AssemblyTitle("InfluxDB Exporter Louie")]
// [MANDATORY] A short description of your plugin
[assembly: AssemblyDescription("Forked InfluxDB Exporter with bug fixes: shared client, safety timestamp guard, DEC distance fix")]

// The following attributes are not required for the plugin per se, but are required by the official manifest meta data

// Your name
[assembly: AssemblyCompany("Louie Observatory")]
// The product name that this plugin is part of
[assembly: AssemblyProduct("InfluxDB Exporter Louie")]
[assembly: AssemblyCopyright("Copyright © 2022-2025 Dale Ghent, 2026 Louie Observatory (fork)")]

// The minimum Version of N.I.N.A. that this plugin is compatible with
[assembly: AssemblyMetadata("MinimumApplicationVersion", "3.2.0.1000")]

// The license your plugin code is using
[assembly: AssemblyMetadata("License", "MPL-2.0")]
// The url to the license
[assembly: AssemblyMetadata("LicenseURL", "https://www.mozilla.org/en-US/MPL/2.0/")]
// The repository where your pluggin is hosted
[assembly: AssemblyMetadata("Repository", "https://github.com/daleghent/nina-influxdb-exporter")]

//[Optional] Common tags that quickly describe your plugin
[assembly: AssemblyMetadata("Tags", "influx, influxdb")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]
// [Unused]
[assembly: AssemblyConfiguration("")]
// [Unused]
[assembly: AssemblyTrademark("")]
// [Unused]
[assembly: AssemblyCulture("")]
