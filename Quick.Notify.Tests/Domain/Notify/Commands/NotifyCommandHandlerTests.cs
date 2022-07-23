using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Quick.Notification.Domain.Configurations.Exceptions;
using Quick.Notification.Domain.Notify.Commands;
using Quick.Notification.Domain.Notify.Commands.Requests;
using Quick.Notification.Domain.Notify.Commands.Responses;
using Quick.Notification.Domain.Notify.Model;
using Quick.Notification.Domain.Notify.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Quick.Notification.Tests.Domain.Notify.Commands
{
    public class NotifyCommandHandlerTests
    {
        private Fixture _fixture;
        private NotifyCommandHandler _notifyCommandHandler;
        private Mock<ILogger<NotifyCommandHandler>> _logger;
        private Mock<INotifyService> _notifyService;
        private CancellationToken _cancellationToken;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            _logger = new Mock<ILogger<NotifyCommandHandler>>();
            _notifyService = new Mock<INotifyService>();
            _cancellationToken = new CancellationToken();
            _notifyCommandHandler = new NotifyCommandHandler(_logger.Object, _notifyService.Object);
        }

        [Test]
        public void Handle_ReturnResultTrue()
        {
            // Arrange
            var notifyRequestFake = _fixture.Create<NotifyRequest>();
            var notifyCommandResponseFake = _fixture.Create<NotifyCommandResponse>();
            var commandRequestFake = new NotifyCommandRequest(notifyRequestFake);
            _notifyService.Setup(s => s.Process(It.IsAny<NotifyCommandRequest>()))
                .Returns(notifyCommandResponseFake);
            NotifyCommandResponse notifyCommandResponse = null;

            // Action
            Func<Task > action = async ()  => notifyCommandResponse = await _notifyCommandHandler.Handle(commandRequestFake, _cancellationToken);

            // Assert
            action.Should().NotThrow();
            notifyCommandResponse.Result.Should().BeTrue();
        }

        [Test]
        public void Handle_ReturnValidationException_WhenNotifyCommandRequestIsEmpty()
        {
            // Arrange
            var commandRequestFake = new NotifyCommandRequest(new NotifyRequest());
            var notifyCommandResponseFake = _fixture.Create<NotifyCommandResponse>();
            _notifyService.Setup(s => s.Process(It.IsAny<NotifyCommandRequest>()))
                .Returns(notifyCommandResponseFake);

            // Action
            Func<Task> action = async () => await _notifyCommandHandler.Handle(commandRequestFake, _cancellationToken);

            // Assert
            action.Should().Throw<ValidationException>();
        }

        [Test]
        public void Handler_ReturnGenericException_WhenNotifyCommandRequestIsNull()
        {
            // Arrange
            var notifyCommandResponseFake = _fixture.Create<NotifyCommandResponse>();
            _notifyService.Setup(s => s.Process(It.IsAny<NotifyCommandRequest>()))
                .Returns(notifyCommandResponseFake);

            // Action
            Func<Task> action = async () => await _notifyCommandHandler.Handle(null, _cancellationToken);

            // Assert
            action.Should().Throw<GenericException>();
        }
    }
}

