using AutoFixture;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Quick.Notify.Api.Command.Controllers.V1;
using Quick.Notify.Domain.Configurations.Services;
using Quick.Notify.Domain.Notify.Model;
using System.Threading.Tasks;

namespace Quick.Notify.Tests.Api.Controllers.V1
{
    public class NotifyControllerTests
    {
        private Fixture _fixture;
        private Mock<IMediator> _mediator;
        private Mock<ILogger<NotifyController>> _logger;

        private const string OK = "Microsoft.AspNetCore.Mvc.OkObjectResult";
        private const string BAD_REQUEST = "Microsoft.AspNetCore.Mvc.BadRequestObjectResult";

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            _mediator = new Mock<IMediator>();
            _logger = new Mock<ILogger<NotifyController>>();
        }

        [Test]
        public async Task PostShouldReturnOk()
        {
            var controller = new NotifyController(_logger.Object, _mediator.Object, _fixture.Create<RequestContextHolder>());
            var request = new NotifyRequest
            {
                Mensagem = "TEST MENSAGEM",
                Protocolo = "TEST PROTOCOLO"
            };

            var response = await controller.Post(request);
            response.ToString().Should().Be(OK);
        }

        [Test]
        public async Task PostShouldReturnBadRequest()
        {
            var controller = new NotifyController(_logger.Object, _mediator.Object, _fixture.Create<RequestContextHolder>());
            var request = new NotifyRequest();

            var response = await controller.Post(request);
            response.ToString().Should().Be(BAD_REQUEST);
        }
    }
}
