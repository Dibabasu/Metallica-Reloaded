using MediatR;
using Moq;

namespace Trades.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var mediator = new Mock<IMediator>();
            Assert.Pass();
        }
    }
}