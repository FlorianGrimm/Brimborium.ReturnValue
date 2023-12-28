using System.Diagnostics;
using System.Runtime.CompilerServices;

using Brimborium.Meaning.Wrapped;
using Brimborium.ReturnValue;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Brimborium.Meaning.Sample;

public class Program {

    public static int Main(string[] args) {
        try {
            var serviceProvider = Configure(args);
            var result = serviceProvider.GetRequiredService<Program>().Execute();
            Console.WriteLine("- fini -");
            return result;
        } catch (Exception error) {
            System.Console.Error.WriteLine(error.ToString());
            return 1;
        }
    }

    public static ServiceProvider Configure(string[] args) {
        IConfigurationRoot configuration;
        {
            var configurationBuilder = new Microsoft.Extensions.Configuration.ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.jsong", optional: true);
            configurationBuilder.AddUserSecrets<Program>();
            configurationBuilder.AddCommandLine(args);
            configuration = configurationBuilder.Build();
        }

        ServiceProvider result;
        {
            var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            services.AddSingleton<IConfiguration>(configuration);
            services.AddOptions();
            services.AddLogging((loggingBuilder) => {
                loggingBuilder.AddConfiguration(configuration);
                loggingBuilder.AddConsole();
            });
            services.AddSingleton<Program>();
            services.AddSingleton<IFileSystemService, FileSystemService>();
            result = services.BuildServiceProvider();
        }
        return result;
    }


    private readonly IServiceProvider _ServiceProvider;
    private readonly ILogger<Program> _Logger;
    private readonly IFileSystemService _FileSystemService;

    public Program(IServiceProvider serviceProvider, ILogger<Program> logger, IFileSystemService fileSystemService) {
        this._ServiceProvider = serviceProvider;
        this._Logger = logger;
        this._FileSystemService = fileSystemService;
    }

    public int Execute() {
        var result = this.CopyDirectory();
        if (result.TryGetError(out var error)) { this._Logger.LogError(error.Exception, "failed"); return 1; }
        if (result.TryGetSuccess(out var exitCode)) { return exitCode; }
        return 2;
    }
    public Result<int> CopyDirectory() {
        FilePath filePathSource = new FilePath(@"c:\temp\src");
        FilePath filePathDestination = new FilePath(@"c:\temp\dst");

        OptionalReason<bool> sourceDirectory = new(new("The source must exists"));
        {
            //Reason reason = (new Reason("The source must exists"));
            //OptionalReason<bool> sourceDirectory = reason.WithNoValue<bool>();
            var sourceDirectoryExists = this._FileSystemService.DirectoryExists(filePathSource);
            if (sourceDirectoryExists.GetErrorOrValue(out var error, out var value)) { return error; };
        }

        OptionalReason<bool> destinationDirectory = new(new("The destination must exists or must be created"));
        {
            //Reason reason = new ("The destination must exists");
            //var destinationDirectoryExists = this._FileSystemService.DirectoryExists(filePathDestination);
            if (this._FileSystemService.DirectoryExists(filePathDestination).GetErrorOrValue(out var error, out var value)) { return error; }
            destinationDirectory = destinationDirectory.WithValue(value);
        }

        return 0;
    }
}
