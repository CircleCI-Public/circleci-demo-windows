using System;
using Xunit;
using WebServer;
using PlaywrightSharp;
using System.Threading.Tasks;

namespace WebBrowserTests
{
    public class WebPageTests : IClassFixture<WebHostServerFixture<Startup>>
    {
        private readonly WebHostServerFixture<Startup> _server;

        public WebPageTests(WebHostServerFixture<Startup> server) => _server = server;

        [Fact]
        public async Task DisplayHomePage()
        {
            await PlaywrightHelpers.InstallAsync();
            using var playwright = await Playwright.CreateAsync();

            // You can use playwright.Firefox, playwright.Chromium, or playwright.WebKit
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = false,
                IgnoreHTTPSErrors = true,
                SlowMo = 250,
            });

            var page = await browser.NewPageAsync();

            // Navigate to the home page
            await page.GoToAsync(_server.RootUri.AbsoluteUri);

            // Get the first h1 element and test the text content
            var header = await page.WaitForSelectorAsync("h1");
            Assert.Equal("Hello, world!", await header.GetTextContentAsync());
        }
    }
}
