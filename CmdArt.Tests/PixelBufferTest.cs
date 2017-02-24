using CmdArt.Screen;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace CmdArt.Tests
{

    [TestFixture]
    public class PixelBufferTest
    {
        [Test]
        public void PixelBuffer_Set_char()
        {
            Palette palette = new Palette(ConsoleColor.Magenta, ConsoleColor.Green);
            PixelBuffer target = new PixelBuffer(3, 3);
            target.Set(0, 0, palette, 'x');
            target.Set(1, 1, palette, 'x');
            target.Set(2, 2, palette, 'x');

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i == j)
                        IsChanged(target.Raw[i, j], palette, 'x');
                    else
                        IsUnchanged(target.Raw[i, j]);
                }
            }
        }

        private void IsChanged(ScreenPixel pixel, Palette palette, char c)
        {
            pixel.Color.Should().Be(palette.ByteValue);
            pixel.Character.Should().Be(c);
            pixel.IsUpdated.Should().BeTrue();
        }

        private void IsUnchanged(ScreenPixel pixel)
        {
            pixel.Color.Should().Be(0);
            pixel.Character.Should().Be('\0');
            pixel.IsUpdated.Should().BeFalse();
        }

        [Test]
        public void PixelBuffer_Set_string()
        {
            Palette palette = new Palette(ConsoleColor.Magenta, ConsoleColor.Green);
            PixelBuffer target = new PixelBuffer(6, 2);
            target.Set(1, 0, palette, "TEST");

            IsUnchanged(target.Raw[0, 0]);
            IsChanged(target.Raw[1, 0], palette, 'T');
            IsChanged(target.Raw[2, 0], palette, 'E');
            IsChanged(target.Raw[3, 0], palette, 'S');
            IsChanged(target.Raw[4, 0], palette, 'T');
            IsUnchanged(target.Raw[5, 0]);

            for (int i = 0; i < 6; i++)
                IsUnchanged(target.Raw[i, 1]);
        }

        [Test]
        public void PixelBuffer_Set_region()
        {
            Palette palette = new Palette(ConsoleColor.Magenta, ConsoleColor.Green);
            PixelBuffer target = new PixelBuffer(3, 3);
            target.Set(new Region(0, 0, 2, 2), palette, 'x');

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i < 2 && j < 2)
                        IsChanged(target.Raw[i, j], palette, 'x');
                    else
                        IsUnchanged(target.Raw[i, j]);
                }
            }
        }

        //[Test]
        //public void PixelBuffer_CopyFrom()
        //{
        //    Palette palette = new Palette(ConsoleColor.Magenta, ConsoleColor.Green);
        //    PixelBuffer source = new PixelBuffer(3, 3);
        //    source.Set(new Region(0, 0, 3, 3), palette, 'x');

        //    PixelBuffer target = new PixelBuffer(3, 3);
        //    target.CopyFrom(source, new Location(0, 0), new Location(0, 0), new Size(2, 2));

        //    for (int i = 0; i < 3; i++)
        //    {
        //        for (int j = 0; j < 3; j++)
        //        {
        //            if (i < 2 && j < 2)
        //                IsChanged(target.Raw[i, j], palette, 'x');
        //            else
        //                IsUnchanged(target.Raw[i, j]);
        //        }
        //    }
        //}
    }
}
