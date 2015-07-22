// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using Xunit;

namespace System.Runtime.Extensions.Tests.System
{
    public class EnvironmentNewLine
    {
        [Fact]
        public void NewLineTest()
        {
            string expectedNewLine = Interop.IsWindows ? "\r\n" : "\n";
            Assert.Equal(expectedNewLine, Environment.NewLine);
        }
    }




}
