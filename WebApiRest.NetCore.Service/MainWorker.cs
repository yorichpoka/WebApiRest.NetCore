using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebApiRest.NetCore.Service
{
    /// <summary>
    /// TODO
    /// </summary>
    public class MainWorker : BackgroundService
    {
        /// <summary>
        /// TODO
        /// </summary>
        private readonly ILogger<MainWorker> _logger;

        /// <summary>
        /// TODO
        /// </summary>
        private readonly IConfiguration _Configuration;

        /// <summary>
        /// TODO
        /// </summary>
        private HttpClient _HttpClient { get; set; }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="logger">TODO</param>
        /// <param name="configuration">TODO</param>
        public MainWorker(ILogger<MainWorker> logger, IConfiguration configuration)
        {
            this._logger = logger;
            this._Configuration = configuration;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="cancellationToken">TODO</param>
        /// <returns>TODO</returns>
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            // Get value from appsettings.json
            var baseAddressApi = this._Configuration.GetSection("Settings:BaseAddressApi").Value;

            this._HttpClient = new HttpClient();
            this._HttpClient.BaseAddress = new Uri(baseAddressApi);

            // Execute base method
            return base.StartAsync(cancellationToken);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="cancellationToken">TODO</param>
        /// <returns>TODO</returns>
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            // Get value from appsettings.json
            var executionDelay = this._Configuration.GetSection("Settings:ExecutionDelay").Value.ExtConvertTo();
            var pathInputFolder = this._Configuration.GetSection("Settings:PathInputFolder").Value;
            var pathOutputFolder = this._Configuration.GetSection("Settings:PathOutputFolder").Value;

            do
            {
                #region Case 1

                /*
                // Call api
                var httpResponse = await this._HttpClient.GetAsync("/");
                // Get response
                var stringResponse = await httpResponse.Content.ReadAsStringAsync();

                // Check response
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    Log.Information(stringResponse);
                else
                    Log.Error(stringResponse);
                */

                #endregion Case 1

                #region Case 2 (Move)

                // Move any content
                foreach (var path in System.IO.Directory.GetFiles($"{pathInputFolder}\\Move"))
                {
                    // Get name of file
                    var fileName = System.IO.Path.GetFileName(path);
                    // Move file
                    System.IO.File.Move(path, $"{pathOutputFolder}\\Move\\{fileName}", true);
                }

                #endregion Case 2 (Move)

                // Log
                this._logger.LogInformation("MainWorker running at: {0}", DateTime.Now);

                // Waiting for next execution
                await Task.Delay(executionDelay, cancellationToken);
            }
            while (!cancellationToken.IsCancellationRequested);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="cancellationToken">TODO</param>
        /// <returns>TODO</returns>
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            this._HttpClient.Dispose();

            // Execute base method
            return base.StopAsync(cancellationToken);
        }
    }
}