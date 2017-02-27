using CmdArt.Rendering.Strings;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;

namespace CmdArt.Tests
{
    [TestFixture]
    public class CircularTextBufferTests
    {
        [Test]
        public void Enumerator_Test()
        {
            var target = new CircularTextBuffer(3);

            target.AddLine("A");
            target.AddLine("B");
            target.AddLine("C");
            target.AddLine("D");
            target.AddLine("E");

            var list = target.ToList();
            list.Count.Should().Be(3);
            list[0].Should().Be("C");
            list[1].Should().Be("D");
            list[2].Should().Be("E");
        }
    }
}
