using FluentAssertions;
using NUnit.Framework;
using System;

namespace CmdArt.Tests
{
    [TestFixture]
    public class PaletteTest
    {
        [Test]
        public void Palette_ToString_Test()
        {
            Palette target = new Palette(ConsoleColor.Magenta, ConsoleColor.Green);
            target.ToString().Should().Be("Magenta on Green");
        }

        [Test]
        public void Palette_ToString_Format()
        {
            Palette target = new Palette(ConsoleColor.Magenta, ConsoleColor.Green);
            target.ToString("N").Should().Be("Magenta on Green");
            target.ToString("C").Should().Be("Magenta,Green");
            target.ToString("B").Should().Be("DA");
        }

        [Test]
        public void Palette_Default_Test()
        {
            Palette target = Palette.Default;
            target.ToString().Should().Be("Gray on Black");
        }

        [Test]
        public void Palette_Swap_Test()
        {
            Palette target = Palette.Default.Swap();
            target.ToString().Should().Be("Black on Gray");
        }

        [Test]
        public void Palette_Invert_Test()
        {
            Palette target = Palette.Default.Invert();
            target.ToString().Should().Be("DarkGray on White");
        }

        [Test]
        public void Palette_ByteValue_Test()
        {
            Palette target = Palette.Default;
            target.ByteValue.Should().Be(0x70);
        }

        [Test]
        public void Palette_Parse_Test()
        {
            Palette.Parse("White on Black").Should().Be(new Palette(ConsoleColor.White, ConsoleColor.Black));
            Palette.Parse("White,Black").Should().Be(new Palette(ConsoleColor.White, ConsoleColor.Black));
            Palette.Parse("F0").Should().Be(new Palette(ConsoleColor.White, ConsoleColor.Black));
        }

        [Test]
        public void Palette_TryParse_Test()
        {
            Palette palette;
            Palette.TryParse("White on Black", out palette).Should().BeTrue();
            palette.Should().Be(new Palette(ConsoleColor.White, ConsoleColor.Black));

            Palette.TryParse("White,Black", out palette).Should().BeTrue();
            palette.Should().Be(new Palette(ConsoleColor.White, ConsoleColor.Black));

            Palette.TryParse("F0", out palette).Should().BeTrue();
            palette.Should().Be(new Palette(ConsoleColor.White, ConsoleColor.Black));

            Palette.TryParse("0xF0", out palette).Should().BeTrue();
            palette.Should().Be(new Palette(ConsoleColor.White, ConsoleColor.Black));
        }
    }
}
