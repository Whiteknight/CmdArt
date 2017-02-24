using CmdArt.Colors;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace CmdArt.Tests
{
    [TestFixture]
    public class ConsoleColorUtilitiesTest
    {
        [Test]
        public void ConsoleColorUtilities_ColorsToByte()
        {
            ConsoleColorUtilities.ColorsToByte(ConsoleColor.Magenta, ConsoleColor.Green).Should().Be(0xDA);
        }

        [Test]
        public void ConsoleColorUtilities_GetForegroundBackground()
        {
            ConsoleColorUtilities.GetForeground(0xDA).Should().Be(ConsoleColor.Magenta);
            ConsoleColorUtilities.GetBackground(0xDA).Should().Be(ConsoleColor.Green);
        }
    }
}
