using ElectronNET.API;
using KamiDownloader.Desktop;

CreateHostBuilder(args).Build().Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseElectron(args);
            webBuilder.UseEnvironment("Development");
            webBuilder.UseStartup<Startup>();
        });