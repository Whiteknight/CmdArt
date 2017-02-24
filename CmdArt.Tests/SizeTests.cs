using FluentAssertions;
using NUnit.Framework;

namespace CmdArt.Tests
{
    [TestFixture]
    public class SizeTests
    {
        [Test]
        public void FitButMaintainAspectRatio_Test1()
        {
            var result = Size.FitButMaintainAspectRatio(new Size(12, 12), 400, 300);
            result.Width.Should().Be(12);
            result.Height.Should().Be(9);
        }

        [Test]
        public void FitButMaintainAspectRatio_Test2()
        {
            var result = Size.FitButMaintainAspectRatio(new Size(12, 12), 300, 400);
            result.Width.Should().Be(9);
            result.Height.Should().Be(12);
        }

        [Test]
        public void FitButMaintainAspectRatio_Test3()
        {
            var result = Size.FitButMaintainAspectRatio(new Size(12, 12), 12, 12);
            result.Width.Should().Be(12);
            result.Height.Should().Be(12);
        }

        [Test]
        public void Equals_Test1()
        {
            var a = new Size(100, 100);
            var b = new Size(100, 100);
            a.Equals(b).Should().BeTrue();
        }

        [Test]
        public void Equals_Test2()
        {
            var a = new Size(100, 100);
            var b = new Size(101, 100);
            a.Equals(b).Should().BeFalse();

            (a == b).Should().BeFalse();
            (a != b).Should().BeTrue();
        }

        [Test]
        public void Equals_Test3()
        {
            var a = new Size(100, 100);
            var b = new Size(100, 101);
            a.Equals(b).Should().BeFalse();

            (a == b).Should().BeFalse();
            (a != b).Should().BeTrue();
        }

        [Test]
        public void Equals_Test4()
        {
            var a = new Size(100, 100);
            var b = new Size(200, 200);
            a.Equals(b).Should().BeFalse();

            (a == b).Should().BeFalse();
            (a != b).Should().BeTrue();
        }

        [Test]
        public void Equals_Test5()
        {
            var a = new Size(100, 100);
            a.Equals(null).Should().BeFalse();

            (a == null).Should().BeFalse();
            (a != null).Should().BeTrue();
        }
    }
}
