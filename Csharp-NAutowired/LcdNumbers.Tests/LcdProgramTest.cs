﻿using System.Collections.Generic;
using NAutowired.Core;
using NAutowired.Console;
using Xunit;

namespace Org.Codecop.Lcdnumbers.Tests
{
    public class LcdProgramTest
    {
        [Fact]
        public void LcdAllDigitsOfSize2()
        {
            var args = new string[] { "1234567890", "2" };
            // when(args.getNonOptionArgs()).thenReturn(Arrays.asList("1234567890", "2"));
            var consoleHost = ConsoleHost.CreateDefaultBuilder(new List<string> { "LcdNumbers" }, args).
                Build();

            var output = Capture.ConsoleOutput(() => consoleHost.Run<Program>());

            Assert.Equal(Input.Join(Input.AllDigitsSize2()), output);
        }

    }
}