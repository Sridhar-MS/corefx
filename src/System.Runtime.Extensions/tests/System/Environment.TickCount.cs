// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Xunit;

namespace System.Runtime.Extensions.Tests.System
{
    public class EnvironmentTickCount
    {
        [Fact]
        public void TickCountTest()
        {
            int start = Environment.TickCount;
            int milliSeconds = 1000;
            Task.Delay(milliSeconds).Wait();
            int end = Environment.TickCount;
            Console.WriteLine("Start - " + start);
            Console.WriteLine("End - " + end);            
            Assert.True(end - start >= milliSeconds);
        }
    }




}
