﻿using System;
using System.Collections.Generic;
using NAutowired.Core.Attributes;
using NAutowired.Console;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Org.Codecop.Lcdnumbers
{
    /// <summary>
    /// NAutowired see https://github.com/kirov-opensource/NAutowired/blob/master/README_EN.md
    /// </summary>
    public class Program : NAutowired.Core.Startup
    {
        [Autowired]
        private readonly ICommandLineArguments arguments;

        [Autowired]
        private readonly LcdDisplay lcdDisplay;

        public override void Run(string[] args)
        {
            if (arguments.IsHelpRequired())
            {
                Console.WriteLine("Run this class to see LCD Numbers working:");
                Console.WriteLine("\nRunning the generated exe:");
                Console.WriteLine("LcdNumbers\\bin\\Debug\\netcoreapp3.0\\LcdNumbers.exe 12345 2");
                Console.WriteLine("\nRunning via dotnet:");
                Console.WriteLine("dotnet run --project LcdNumbers 12345 2");
                return;
            }

            int number = arguments.GetNumberToDisplay();
            var scaling = arguments.GetScaling();

            Console.Write(lcdDisplay.ToLcd(number, scaling));
        }
        public static void Main(string[] args)
        {
            BuildConsoleHost(args)
                .Run<Program>();
        }

        public static NAutowired.Core.IConsoleHost BuildConsoleHost(string[] args)
        {
            var config = LoadConfig();
            return ConsoleHost.CreateDefaultBuilder(services =>
            {
                services.AddTransient(typeof(ICommandLineArguments),
                    serviceProvider => new CommandLineArguments(args));
                services.AddTransient(typeof(INumeralSystem),
                    serviceProvider => new NumeralSystem(config.NumeralSystemBase));

            }, new List<string> { "LcdNumbers" }, new string[0])
                .Build();
        }

        private static Config LoadConfig()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();
            var config = new Config();
            configuration.Bind(config);
            return config;
        }
    }

    public class Config
    {
        public int NumeralSystemBase { get; set; }
    }
}
