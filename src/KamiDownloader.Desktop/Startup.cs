using ElectronNET.API;
using ElectronNET.API.Entities;
using KamiDownloader.Desktop.Data;

namespace KamiDownloader.Desktop;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        //services.AddSingleton(sg => { var log = sg.GetService<ILogger<Startup>>()});
        services.AddRazorPages();
        services.AddServerSideBlazor();
        services.AddSingleton<WeatherForecastService>();
        //services.AddSingleton<MyService>(); // Other singletons
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseStaticFiles();
        app.UseRouting();

        // For regular
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
        });


        if (HybridSupport.IsElectronActive)
        {
            ElectronCreateWindow();
        }
    }

    public async void ElectronCreateWindow()
    {
        var browserWindowOptions = new BrowserWindowOptions
        {
            Width = 1024,
            Height = 768,
            Show = false, // wait to open it
            WebPreferences = new WebPreferences
            {
                WebSecurity = false
            }
        };

        var browserWindow = await Electron.WindowManager.CreateWindowAsync(browserWindowOptions);
        await browserWindow.WebContents.Session.ClearCacheAsync();

        // Handler to show when it is ready
        browserWindow.OnReadyToShow += () => { browserWindow.Show(); };

        // Close Handler
        browserWindow.OnClose += () => Environment.Exit(0);
    }
}