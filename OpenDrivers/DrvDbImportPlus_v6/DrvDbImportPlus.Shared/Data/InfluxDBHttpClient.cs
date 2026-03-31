// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Text;

namespace Scada.Comm.Drivers.DrvDbImportPlus
{
    /// <summary>
    /// HTTP client for InfluxDB 2.x/3.x with InfluxDB 1.x style API compatibility.
    /// <para>HTTP клиент для InfluxDB 2.x/3.x с совместимостью с API InfluxDB 1.x.</para>
    /// </summary>
    public class InfluxDBHttpClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly string _database;
        private bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public InfluxDBHttpClient(string url, string token, string database, bool ignoreSslErrors = false)
        {
            if (ignoreSslErrors)
            {
                _httpClient = new HttpClient(new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                });
            }
            else
            {
                _httpClient = new HttpClient();
            }

            _httpClient.Timeout = TimeSpan.FromSeconds(10); // Уменьшил таймаут для тестов
            _baseUrl = url.TrimEnd('/');
            _token = token;
            _database = database;

            // Очищаем заголовки
            _httpClient.DefaultRequestHeaders.Clear();

            // Правильное добавление Bearer токена
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            // Добавляем остальные заголовки
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "DrvDbImportPlus/1.0");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            Debug.WriteLine($"InfluxDBHttpClient created: URL={_baseUrl}, Database={_database}");
        }

        /// <summary>
        /// Disposes the client.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the client.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _httpClient?.Dispose();
                }

                _disposed = true;
            }
        }

        ~InfluxDBHttpClient()
        {
            Dispose(false);
        }

        /// <summary>
        /// Executes a query asynchronously.
        /// </summary>
        public async Task<string> ExecuteQueryAsync(string query, bool useGet = true)
        {
            try
            {
                Debug.WriteLine($"=== ExecuteQueryAsync START ===");
                Debug.WriteLine($"Query: {query}");
                Debug.WriteLine($"Method: {(useGet ? "GET" : "POST")}");

                string result;
                HttpResponseMessage response;

                if (useGet)
                {
                    // GET запрос с параметрами в URL
                    string encodedQuery = Uri.EscapeDataString(query);
                    string encodedDatabase = Uri.EscapeDataString(_database);
                    string requestUrl = $"{_baseUrl}/query?db={encodedDatabase}&q={encodedQuery}";

                    Debug.WriteLine($"GET Request URL: {requestUrl}");

                    var stopwatch = Stopwatch.StartNew();
                    response = await _httpClient.GetAsync(requestUrl).ConfigureAwait(false);
                    stopwatch.Stop();

                    Debug.WriteLine($"GET request completed in {stopwatch.ElapsedMilliseconds} ms");
                }
                else
                {
                    // POST запрос с form-data
                    string requestUrl = $"{_baseUrl}/query";
                    var formData = new Dictionary<string, string>
                    {
                        { "db", _database },
                        { "q", query }
                    };

                    Debug.WriteLine($"POST Request URL: {requestUrl}");
                    Debug.WriteLine($"Form data: db={_database}, q={query}");

                    var content = new FormUrlEncodedContent(formData);

                    var stopwatch = Stopwatch.StartNew();
                    response = await _httpClient.PostAsync(requestUrl, content).ConfigureAwait(false);
                    stopwatch.Stop();

                    Debug.WriteLine($"POST request completed in {stopwatch.ElapsedMilliseconds} ms");
                }

                Debug.WriteLine($"Response status: {(int)response.StatusCode} {response.StatusCode}");

                result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                Debug.WriteLine($"Response content length: {result.Length}");

                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"Error response: {result}");
                    throw new HttpRequestException($"HTTP {response.StatusCode}: {result}");
                }

                Debug.WriteLine($"Response preview: {(result.Length > 200 ? result.Substring(0, 200) + "..." : result)}");
                Debug.WriteLine($"=== ExecuteQueryAsync END ===");

                return result;
            }
            catch (TaskCanceledException)
            {
                Debug.WriteLine($"Request timed out after {_httpClient.Timeout.TotalSeconds} seconds");
                throw new TimeoutException($"Request timed out after {_httpClient.Timeout.TotalSeconds} seconds");
            }
            catch (HttpRequestException ex)
            {
                Debug.WriteLine($"HTTP Request Exception: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in ExecuteQueryAsync: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Executes a query and returns the result as a list of dictionaries.
        /// </summary>
        public List<Dictionary<string, object>> ExecuteQueryToDictionary(string query)
        {
            var json = ExecuteQueryAsync(query).Result;
            return ParseInfluxJsonResponse(json);
        }

        /// <summary>
        /// Executes a query and returns the result as a DataTable.
        /// </summary>
        public DataTable ExecuteQueryToDataTable(string query)
        {
            var json = ExecuteQueryAsync(query).Result;
            return ConvertInfluxJsonToDataTable(json);
        }

        /// <summary>
        /// Parses InfluxDB JSON response to a list of dictionaries.
        /// </summary>
        private List<Dictionary<string, object>> ParseInfluxJsonResponse(string json)
        {
            var result = new List<Dictionary<string, object>>();

            try
            {
                Debug.WriteLine($"Parsing JSON response, length: {json.Length}");

                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                if (jsonObj.results != null && jsonObj.results.Count > 0)
                {
                    var firstResult = jsonObj.results[0];

                    // Проверяем на ошибки
                    if (firstResult.error != null)
                    {
                        string error = firstResult.error;
                        Debug.WriteLine($"Error in response: {error}");
                        throw new InvalidOperationException($"InfluxDB error: {error}");
                    }

                    if (firstResult.series != null && firstResult.series.Count > 0)
                    {
                        var series = firstResult.series[0];
                        var columns = ((IEnumerable<dynamic>)series.columns).Select(c => (string)c).ToList();
                        var values = ((IEnumerable<dynamic>)series.values).ToList();

                        Debug.WriteLine($"Parsed: {columns.Count} columns, {values.Count} rows");

                        foreach (var rowValues in values)
                        {
                            var row = new Dictionary<string, object>();
                            for (int i = 0; i < columns.Count; i++)
                            {
                                row[columns[i]] = rowValues[i];
                            }
                            result.Add(row);
                        }
                    }
                    else
                    {
                        Debug.WriteLine("No series in response (empty result)");
                    }
                }
                else
                {
                    Debug.WriteLine("No results in response");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to parse JSON response: {ex.Message}");
                throw new InvalidOperationException($"Failed to parse JSON response: {ex.Message}", ex);
            }

            return result;
        }

        /// <summary>
        /// Converts InfluxDB JSON response to DataTable.
        /// </summary>
        private DataTable ConvertInfluxJsonToDataTable(string json)
        {
            var dataTable = new DataTable();

            Debug.WriteLine($"Converting JSON to DataTable, length: {json.Length}");

            try
            {
                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                if (jsonObj.results != null && jsonObj.results.Count > 0)
                {
                    var firstResult = jsonObj.results[0];

                    // Проверяем на ошибки
                    if (firstResult.error != null)
                    {
                        string error = firstResult.error;
                        Debug.WriteLine($"Error in response: {error}");
                        throw new InvalidOperationException($"InfluxDB error: {error}");
                    }

                    if (firstResult.series != null && firstResult.series.Count > 0)
                    {
                        var series = firstResult.series[0];
                        var columns = ((IEnumerable<dynamic>)series.columns).Select(c => (string)c).ToList();
                        var values = ((IEnumerable<dynamic>)series.values).ToList();

                        Debug.WriteLine($"Creating DataTable with {columns.Count} columns and {values.Count} rows");

                        // Создаем колонки
                        foreach (var column in columns)
                        {
                            dataTable.Columns.Add(column, typeof(object));
                        }

                        // Добавляем строки
                        foreach (var rowValues in values)
                        {
                            var row = dataTable.NewRow();
                            for (int i = 0; i < columns.Count; i++)
                            {
                                row[i] = rowValues[i];
                            }
                            dataTable.Rows.Add(row);
                        }
                    }
                    else
                    {
                        Debug.WriteLine("No series in response, returning empty DataTable");
                    }
                }
                else
                {
                    Debug.WriteLine("No results in response, returning empty DataTable");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to convert JSON to DataTable: {ex.Message}");
                throw new InvalidOperationException($"Failed to convert JSON to DataTable: {ex.Message}", ex);
            }

            Debug.WriteLine($"DataTable created with {dataTable.Rows.Count} rows and {dataTable.Columns.Count} columns");
            return dataTable;
        }

        /// <summary>
        /// Tests the connection synchronously (but safely for WinForms)
        /// </summary>
        public bool TestConnectionSync(bool useGet = false)
        {
            try
            {
                Debug.WriteLine($"Testing InfluxDB connection synchronously...");

                // Используем Task.Run чтобы уйти из UI контекста
                var testTask = Task.Run(async () =>
                {
                    try
                    {
                        var result = await ExecuteQueryAsync("SHOW MEASUREMENTS", useGet).ConfigureAwait(false);
                        return Tuple.Create(true, result, (Exception)null);
                    }
                    catch (Exception ex)
                    {
                        return Tuple.Create(false, (string)null, ex);
                    }
                });

                // Ждем с таймаутом
                if (testTask.Wait(TimeSpan.FromSeconds(10)))
                {
                    var result = testTask.Result;

                    if (result.Item1) // Success
                    {
                        Debug.WriteLine($"Connection test successful");
                        Debug.WriteLine($"Response length: {result.Item2?.Length ?? 0}");
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine($"Connection test failed: {result.Item3?.Message}");
                        return false;
                    }
                }
                else
                {
                    Debug.WriteLine($"Connection test timed out after 10 seconds");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Connection test exception: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Tests both GET and POST methods to see which works.
        /// </summary>
        public async Task<string> TestBothMethods()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine($"Testing connection to: {_baseUrl}");
            result.AppendLine($"Database: {_database}");

            // Test GET method  
            result.AppendLine("\n=== Testing GET method ===");
            try
            {
                var getResult = await ExecuteQueryAsync("SHOW MEASUREMENTS", true).ConfigureAwait(false);
                result.AppendLine($"GET: SUCCESS");
                result.AppendLine($"Response length: {getResult.Length}");
            }
            catch (Exception ex)
            {
                result.AppendLine($"GET: FAILED - {ex.Message}");
            }

            // Test POST method
            result.AppendLine("\n=== Testing POST method ===");
            try
            {
                var postResult = await ExecuteQueryAsync("SHOW MEASUREMENTS", false).ConfigureAwait(false);
                result.AppendLine($"POST: SUCCESS");
                result.AppendLine($"Response length: {postResult.Length}");
            }
            catch (Exception ex)
            {
                result.AppendLine($"POST: FAILED - {ex.Message}");
            }

            return result.ToString();
        }

        public static async Task<string> QuickTestAsync(string url, string token, string database)
        {
            try
            {
                Debug.WriteLine("=== QUICK TEST START ===");

                using var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(5);
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                string testUrl = $"{url}/query?db={Uri.EscapeDataString(database)}&q={Uri.EscapeDataString("SHOW MEASUREMENTS")}";
                Debug.WriteLine($"Test URL: {testUrl}");

                var response = await client.GetAsync(testUrl).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                Debug.WriteLine($"Status: {response.StatusCode}");
                Debug.WriteLine($"Response length: {content.Length}");
                Debug.WriteLine("=== QUICK TEST END ===");

                return content;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Quick test failed: {ex.Message}");
                return $"Error: {ex.Message}";
            }
        }
    }
}