#region "copyright"

/*
    Copyright 2023 Dale Ghent <daleg@elemental.org>

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/
*/

#endregion "copyright"

using DaleGhent.NINA.InfluxDbExporter.Interfaces;
using InfluxDB.Client;
using NINA.Core.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaleGhent.NINA.InfluxDbExporter.Utilities {

    public class Utilities {

        private static InfluxDBClient _sharedClient;
        private static string _cachedConfigKey;
        private static readonly object _clientLock = new object();

        public static bool ConfigCheck(IInfluxDbExporterOptions options) {
            if (options == null) { return false; }
            if (!options.AuthWorks) { return false; }
            if (string.IsNullOrEmpty(options.InfluxDbUrl)) { return false; }
            if (string.IsNullOrEmpty(options.InfluxDbBucket)) { return false; }
            if (string.IsNullOrEmpty(options.InfluxDbToken)) { return false; }
            if (string.IsNullOrEmpty(options.InfluxDbOrgId)) { return false; }

            return true;
        }

        internal static long UnixEpoch(DateTime dateTime) {
            return (long)dateTime.ToUniversalTime().Subtract(DateTime.UnixEpoch).TotalSeconds;
        }

        private static string BuildConfigKey(IInfluxDbExporterOptions options) {
            return $"{options.InfluxDbUrl}|{options.InfluxDbToken}|{options.InfluxDbBucket}|{options.InfluxDbOrgId}|{options.TagProfileName}|{options.TagHostname}";
        }

        private static InfluxDBClient GetOrCreateClient(IInfluxDbExporterOptions options) {
            var configKey = BuildConfigKey(options);

            lock (_clientLock) {
                if (_sharedClient != null && _cachedConfigKey == configKey) {
                    return _sharedClient;
                }

                _sharedClient?.Dispose();

                var clientOptions = new InfluxDBClientOptions(options.InfluxDbUrl) {
                    Token = options.InfluxDbToken,
                    Bucket = options.InfluxDbBucket,
                    Org = options.InfluxDbOrgId,
                };

                if (options.TagProfileName) {
                    clientOptions.AddDefaultTag("profile_name", options.ProfileName);
                }

                if (options.TagHostname) {
                    clientOptions.AddDefaultTag("host_name", options.Hostname);
                }

                _sharedClient = new InfluxDBClient(clientOptions);
                _cachedConfigKey = configKey;

                return _sharedClient;
            }
        }

        public static void DisposeClient() {
            lock (_clientLock) {
                _sharedClient?.Dispose();
                _sharedClient = null;
                _cachedConfigKey = null;
            }
        }

        public static async Task<bool> SendPoints(IInfluxDbExporterOptions options, List<InfluxDB.Client.Writes.PointData> points) {
            if (!ConfigCheck(options)) { return false; }
            if (points == null) { return false; }
            if (points.Count == 0) { return false; }

            try {
                var client = GetOrCreateClient(options);
                var writeApi = client.GetWriteApiAsync();
                await writeApi.WritePointsAsync(points);
                return true;
            } catch (Exception ex) {
                Logger.Error($"Failed to send points to InfluxDB: {ex.Message}");
                return false;
            }
        }
    }
}
