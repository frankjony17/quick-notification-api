using AutoFixture;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;
using CompanySource.Infra.Abstractions.MessagingBroker;
using NUnit.Framework;
using Quick.Notify.Domain.Abstractions.Services;
using Quick.Notify.Domain.Configurations.Services;
using Quick.Notify.Domain.Notify.Commands.Requests;
using Quick.Notify.Domain.Notify.Commands.Responses;
using Quick.Notify.Domain.Notify.Model;
using Quick.Notify.Domain.Notify.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Quick.Notify.Tests.Domain.Notify.Services
{
    class NotifyServiceTests
    {
        private Fixture _fixture;
        private Mock<IMessageDispatcher> _messageDispatcher;
        private NotifyService _notifyService;
        private Mock<IValidator<NotifyProcess>> _validator;
        private Mock<ILogger<NotifyService>> _logger;

        [SetUp]
        public void SetUp()
        {
            _logger = new Mock<ILogger<NotifyService>>();
            _messageDispatcher = new Mock<IMessageDispatcher>();
            _fixture = new Fixture();
            _notifyService = new NotifyService(_messageDispatcher.Object, _logger.Object, _fixture.Create<RequestContextHolder>());
            _validator = new Mock<IValidator<NotifyProcess>>();
        }

        [Test]
        public void Process_ReturnsNok_WhenNotifyRequestIsEmpty()
        {
            // Arrange
            NotifyCommandResponse notifyCommandResponse = null;
            var emptyRequest = new NotifyRequest();
            var commandRequest = new NotifyCommandRequest(emptyRequest);

            // Action
            Action act = () => notifyCommandResponse = _notifyService.Process(commandRequest);

            // Assert
            act.Should().NotThrow();
            notifyCommandResponse.DescricaoMensagemRetorno.Should().Be("NOK");
            notifyCommandResponse.Result.Should().BeFalse();
            _messageDispatcher.Verify(p => p.PublishToQueue(It.IsAny<string>(), It.IsAny<IMessage>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void Process_ReturnsNok_WhenNotifyRequestMessageIsEmpty()
        {
            // Arrange
            NotifyCommandResponse notifyCommandResponse = null;
            var emptyRequestWithMessage = new NotifyRequest
            {
                Mensagem = "{}"
            };
            var commandRequest = new NotifyCommandRequest(emptyRequestWithMessage);

            // Action
            Action act = () => notifyCommandResponse = _notifyService.Process(commandRequest);

            // Assert
            act.Should().NotThrow();
            notifyCommandResponse.DescricaoMensagemRetorno.Should().Be("NOK");
            notifyCommandResponse.Result.Should().BeFalse();
            _messageDispatcher.Verify(p => p.PublishToQueue(It.IsAny<string>(), It.IsAny<IMessage>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void Process_ReturnsOk_WhenMessageIsValid()
        {
            // Arrange
            NotifyCommandResponse notifyCommandResponse = null;
            var validMsgFake = "{\"EndToendId\":\"E5958811120210215110659184PV3FMX\",\"ValorOperacao\":\"1.00\",\"CpfCnpjUsuarioRecebedor\":\"76719563092\",\"StatusOperacao\":1}";
            var requestWithValidMessage = new NotifyRequest
            {
                Mensagem = validMsgFake
            };
            var commandRequest = new NotifyCommandRequest(requestWithValidMessage);

            // Action
            Action act = () => notifyCommandResponse = _notifyService.Process(commandRequest);

            // Assert
            act.Should().NotThrow();
            notifyCommandResponse.DescricaoMensagemRetorno.Should().Be("OK");
            notifyCommandResponse.CodigoRetorno.Should().Be("200");
            notifyCommandResponse.Result.Should().BeTrue();
            _messageDispatcher.Verify(p => p.PublishToQueue(It.IsAny<string>(), It.IsAny<IMessage>(), It.IsAny<string>()), Times.Once);
        }
    }
}
