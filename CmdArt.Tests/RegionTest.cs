using FluentAssertions;
using NUnit.Framework;

namespace CmdArt.Tests
{

    [TestFixture]
    public class RegionTest
    {
        [Test]
        public void Region_RelativeToAbsolute_Test()
        {
            Region target = new Region(5, 6, 7, 9);
            Region result = target.RelativeToAbsolute(new Region(1, 2, 3, 10));
            result.Left.Should().Be(6);
            result.Top.Should().Be(8);
            result.Width.Should().Be(3);
            result.Height.Should().Be(7);
        }

        [Test]
        public void Region_ToString_Test()
        {
            Region target = new Region(5, 6, 7, 9);
            target.ToString().Should().Be("(5,6) 7x9");
        }

        [Test]
        public void Region_ToString_Format()
        {
            Region target = new Region(5, 6, 7, 9);
            target.ToString("LTWH").Should().Be("5679");
            target.ToString(@"\L\T\W\H").Should().Be("LTWH");
            target.ToString("TEST").Should().Be("6ES6");
        }

        [Test]
        public void Region_Move()
        {
            Region target = new Region(5, 5, 5, 5);
            Region result = target.Move(1, -1);

            result.Width.Should().Be(5);
            result.Height.Should().Be(5);
            result.Left.Should().Be(6);
            result.Top.Should().Be(4);
        }

        [Test]
        public void Region_Adjust()
        {
            Region target = new Region(5, 5, 5, 5);
            Region result = target.Adjust(1, -1, 2, -2);

            result.Width.Should().Be(7);
            result.Height.Should().Be(3);
            result.Left.Should().Be(6);
            result.Top.Should().Be(4);
        }

        [Test]
        public void Region_ShrinkToFit()
        {
            Region target = new Region(5, 5, 5, 5);
            Region result = target.ShrinkToFit(6, 4);
            result.Width.Should().Be(5);
            result.Height.Should().Be(4);
            result.Left.Should().Be(5);
            result.Top.Should().Be(5);

            result = target.ShrinkToFit(4, 6);
            result.Width.Should().Be(4);
            result.Height.Should().Be(5);
            result.Left.Should().Be(5);
            result.Top.Should().Be(5);

            result = target.ShrinkToFit(6, 6);
            result.Width.Should().Be(5);
            result.Height.Should().Be(5);
            result.Left.Should().Be(5);
            result.Top.Should().Be(5);

            result = target.ShrinkToFit(4, 4);
            result.Width.Should().Be(4);
            result.Height.Should().Be(4);
            result.Left.Should().Be(5);
            result.Top.Should().Be(5);
        }

        [Test]
        public void Region_Overlaps()
        {
            Region target = new Region(5, 5, 5, 5);

            target.Overlaps(new Region(5, 5, 5, 5)).Should().BeTrue();
            target.Overlaps(new Region(6, 6, 3, 3)).Should().BeTrue();

            target.Overlaps(new Region(0, 5, 1, 5)).Should().BeFalse();
            target.Overlaps(new Region(5, 0, 5, 1)).Should().BeFalse();
            target.Overlaps(new Region(11, 5, 5, 5)).Should().BeFalse();
            target.Overlaps(new Region(5, 11, 1, 5)).Should().BeFalse();

            target.Overlaps(new Region(0, 0, 5, 5)).Should().BeTrue();
            target.Overlaps(new Region(0, 10, 5, 5)).Should().BeTrue();
            target.Overlaps(new Region(10, 0, 5, 5)).Should().BeTrue();
            target.Overlaps(new Region(10, 10, 5, 5)).Should().BeTrue();
        }

        [Test]
        public void Offset_Test()
        {
            var target = new Region(1, 2, 3, 4);
            var result = target.Offset(new Location(4, 5));

            result.Left.Should().Be(5);
            result.Top.Should().Be(7);
            result.Width.Should().Be(3);
            result.Height.Should().Be(4);
        }

        [Test]
        public void IsCompletelyContainedBy_Test()
        {
            new Region(10, 10, 10, 10).IsCompletelyContainedBy(new Region(10, 10, 10, 10)).Should().BeTrue();
            new Region(10, 10, 10, 10).IsCompletelyContainedBy(new Region(0, 0, 30, 30)).Should().BeTrue();
            new Region(10, 10, 10, 10).IsCompletelyContainedBy(new Region(5, 5, 10, 10)).Should().BeFalse();
            new Region(10, 10, 10, 10).IsCompletelyContainedBy(new Region(10, 10, 5, 5)).Should().BeFalse();
            new Region(10, 10, 10, 10).IsCompletelyContainedBy(new Region(5, 5, 5, 5)).Should().BeFalse();
        }

        [Test]
        public void CompletelyContains_Test()
        {
            new Region(10, 10, 10, 10).CompletelyContains(new Region(10, 10, 10, 10)).Should().BeTrue();
            new Region(10, 10, 10, 10).CompletelyContains(new Region(0, 0, 30, 30)).Should().BeFalse();
            new Region(10, 10, 10, 10).CompletelyContains(new Region(5, 5, 10, 10)).Should().BeFalse();
            new Region(10, 10, 10, 10).CompletelyContains(new Region(10, 10, 5, 5)).Should().BeTrue();
            new Region(10, 10, 10, 10).CompletelyContains(new Region(5, 5, 5, 5)).Should().BeFalse();
        }

        [Test]
        public void Equals_ISize_Test1()
        {
            var a = new Region(10, 10, 10, 10);
            var b = new Size(10, 10);

            a.Equals(b).Should().BeTrue();
        }

        [Test]
        public void Equals_ISize_Test2()
        {
            var a = new Region(10, 10, 10, 10);
            var b = new Size(11, 11);

            a.Equals(b).Should().BeFalse();
        }
    }
}
