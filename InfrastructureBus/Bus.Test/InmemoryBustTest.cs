using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SuitSupply.Infrastructure.Bus;
using SuitSupply.Infrastructure.Bus.Command;
using SuitSupply.Infrastructure.Bus.Contracts;
using SuitSupply.Infrastructure.Bus.Contracts.Command;
using SuitSupply.Infrastructure.Bus.Contracts.Query;
using SuitSupply.Infrastructure.Bus.Query;
using SuitSupply.Infrastructure.SLAdapter.MsDependency;
using SuitSupply.Infrastructure.Validator.Contract;

namespace Bus.Test
{
    [TestClass]
    public class InmemoryBustTest : TestBase
    {
        [TestMethod]
        public async Task SendMustSucceed()
        {
            var handler = new Mock<SuitCommandHandler<TestCommand>>();
            handler.Setup(p => p.Validate(It.IsAny<TestCommand>())).Returns(Task.FromResult(new SuitValidationResult()));
            handler.Setup(p => p.Handle(It.IsAny<TestCommand>())).Returns(Task.FromResult(new CommandResponse
            {
                Success = true,
                Result = "I am from Command"
            }));

            RegisterMockCommandHandler(handler.Object);
            var bus = CommonServiceLocator.ServiceLocator.Current.GetInstance<ISuitBus>();
            var response = await bus.Send(new TestCommand());
            Assert.AreEqual("I am from Command", response.Result);

        }

        [TestMethod]
        public async Task QueryMustSucceed()
        {
            var handler = new Mock<SuitQueryHandler<string, TestQuery>>();
            handler.Setup(p => p.Validate(It.IsAny<TestQuery>())).Returns(Task.FromResult(new SuitValidationResult()));
            handler.Setup(p => p.Handle(It.IsAny<TestQuery>())).Returns(Task.FromResult(new QueryResponse<string>()
            {
                Data = "query",
                Success = true
            }));

            RegisterMockQueryHandler(handler.Object);
            var bus = CommonServiceLocator.ServiceLocator.Current.GetInstance<ISuitBus>();
            var response = await bus.Query(new TestQuery());
            Assert.AreEqual("query", response.Data);

        }
    }
}
