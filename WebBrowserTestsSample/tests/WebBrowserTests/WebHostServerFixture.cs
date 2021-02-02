using System;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public abstract class WebHostServerFixture : IDisposable
{
    private readonly Lazy<Uri> _rootUriInitializer;

    public Uri RootUri => _rootUriInitializer.Value;
    public IHost Host { get; set; }

    public WebHostServerFixture()
    {
        _rootUriInitializer = new Lazy<Uri>(() => new Uri(StartAndGetRootUri()));
    }

    protected static void RunInBackgroundThread(Action action)
    {
        using var isDone = new ManualResetEvent(false);

        ExceptionDispatchInfo edi = null;
        new Thread(() =>
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                edi = ExceptionDispatchInfo.Capture(ex);
            }

            isDone.Set();
        }).Start();

        if (!isDone.WaitOne(TimeSpan.FromSeconds(10)))
            throw new TimeoutException("Timed out waiting for: " + action);

        if (edi != null)
            throw edi.SourceException;
    }

    protected virtual string StartAndGetRootUri()
    {
        // As the port is generated automatically, we can use IServerAddressesFeature to get the actual server URL
        Host = CreateWebHost();
        RunInBackgroundThread(Host.Start);
        return Host.Services.GetRequiredService<IServer>().Features
            .Get<IServerAddressesFeature>()
            .Addresses.Single();
    }

    public virtual void Dispose()
    {
        Host?.Dispose();
        Host?.StopAsync();
    }

    protected abstract IHost CreateWebHost();
}

public class WebHostServerFixture<TStartup> : WebHostServerFixture
    where TStartup : class
{
    protected override IHost CreateWebHost()
    {
        return new HostBuilder()
            .ConfigureWebHost(webHostBuilder => webHostBuilder
                .UseKestrel()
                // .UseSolutionRelativeContentRoot(typeof(TStartup).Assembly.GetName().Name)
                .UseStaticWebAssets()
                .UseStartup<TStartup>()
                .UseUrls($"http://127.0.0.1:0")) // :0 allows to choose a port automatically
            .Build();
    }
}