﻿using System;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Wabbajack.Models;
using Wabbajack.Paths;
using Wabbajack.Paths.IO;

namespace Wabbajack.WebAutomation
{
    public class Driver : IDisposable
    {
        //private readonly IWebBrowser _browser;
        private readonly dynamic _driver;

        public Driver(ILogger logger, CefService service)
        {

            //_browser = new ChromiumWebBrowser();
            //_driver = new CefSharpWrapper(logger, _browser, service);
        }
        public async Task<Uri?> NavigateTo(Uri uri, CancellationToken? token = null)
        {
            try
            {
                //await _driver.NavigateTo(uri, token);
                return await GetLocation();
            }
            catch (TaskCanceledException ex)
            {
                await DumpState(uri, ex);
                throw;
            }
        }

        private async Task DumpState(Uri uri, Exception ex)
        {
            var file = KnownFolders.EntryPoint.Combine("CEFStates", DateTime.UtcNow.ToFileTimeUtc().ToString())
                .WithExtension(new Extension(".html"));
            file.Parent.CreateDirectory();
            var source = await GetSourceAsync();
            //var cookies = await Helpers.GetCookies();
            //var cookiesString = string.Join('\n', cookies.Select(c => c.Name + " - " + c.Value));
            //await file.WriteAllTextAsync(uri + "\n " + source + "\n" + ex + "\n" + cookiesString);
        }

        public async Task<long> NavigateToAndDownload(Uri uri, AbsolutePath absolutePath, bool quickMode = false, CancellationToken? token = null)
        {
            try
            {
                //return await _driver.NavigateToAndDownload(uri, absolutePath, quickMode: quickMode, token: token);
                return 0;
            }
            catch (TaskCanceledException ex) {
                await DumpState(uri, ex);
                throw;
            }
        }

        public async ValueTask<Uri?> GetLocation()
        {
            /*
            try
            {
                return new Uri(_browser.Address);
            }
            catch (UriFormatException)
            {
                return null;
            }*/
            return null;
        }

        public async ValueTask<string> GetSourceAsync()
        {
            //return await _browser.GetSourceAsync();
            return "";
        }
        
        public async ValueTask<HtmlDocument> GetHtmlAsync()
        {
            var body = await GetSourceAsync();
            var doc = new HtmlDocument();
            doc.LoadHtml(body);
            return doc;
        }

        public Action<Uri?> DownloadHandler { 
            set => _driver.DownloadHandler = value;
        }

        public Task<string> GetAttr(string selector, string attr)
        {
            return _driver.EvaluateJavaScript($"document.querySelector(\"{selector}\").{attr}");
        }

        public Task<string> EvalJavascript(string js)
        {
            return _driver.EvaluateJavaScript(js);
        }

        public void Dispose()
        {
            //_browser.Dispose();
        }

        public static void ClearCache()
        {
            //Helpers.ClearCookies();
        }

        public async Task DeleteCookiesWhere(Func<DTOs.Logins.Cookie, bool> filter)
        {
            //await Helpers.DeleteCookiesWhere(filter);
        }
    }
}
