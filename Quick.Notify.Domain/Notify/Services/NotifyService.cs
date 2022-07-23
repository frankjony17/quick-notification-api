using FluentValidation;
using Microsoft.Extensions.Logging;
using NeonSource.Infra.Abstractions.Logging;
using NeonSource.Infra.Abstractions.MessagingBroker;
using Newtonsoft.Json;
using Quick.Notification.Domain.Abstractions.Services;
using Quick.Notification.Domain.Notify.Commands.Requests;
using Quick.Notification.Domain.Notify.Commands.Responses;
using Quick.Notification.Domain.Notify.Model;
using Quick.Notification.Domain.Notify.Validations;
using System;
using System.Threading.Tasks;

namespace Quick.Notification.Domain.Notify.Services
{
    public class NotifyService : INotifyService
    {
        public const string ExchangeName = "payment-notify-exchange";
        private readonly IMessageDispatcher _messageDispatcher;
        private readonly IValidator<NotifyProcess> _validator;
        private readonly ILogger<NotifyService> _logger;
        private readonly IRequestContextHolder _requestContextHolder;

        public NotifyService(IMessageDispatcher messageDispatcher,
                           ILogger<NotifyService> logger,
                           IRequestContextHolder requestContextHolder)
        {
            _logger = logger;
            _messageDispatcher = messageDispatcher;
            _requestContextHolder = requestContextHolder;
            _validator = new NotifyProcessValidator();
        }

        public NotifyCommandResponse Process(NotifyCommandRequest notifyCommandRequest)
        {
            _logger.BeginCorrelationIdScope(_requestContextHolder.CorrelationId);
            var response = new NotifyCommandResponse();
            response.Result = true;
            try
            {
                var notifyMessage = Parser(notifyCommandRequest.NotifyRequest);
                _logger.LogInformation("Parser performed successfully.");

                Validate(notifyMessage);
                _logger.LogInformation($"Notification message is valid. EndToEndId = [{notifyMessage.Mensagem.EndToendId}]");

                var message = new Message<NotifyProcess>
                {
                    Data = notifyMessage,
                    CorrelationId = _requestContextHolder.CorrelationId
                };
                _messageDispatcher.PublishToQueue(ExchangeName, message);
                response.CodigoRetorno = "200";
                response.DescricaoMensagemRetorno = "OK";
                _logger.LogInformation("Message sent to queue successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Message = [{ex.Message}]");
                response.CodigoRetorno = "";
                response.DescricaoMensagemRetorno = "NOK";
                response.Error = ex.Message;
                response.Result = false;
            }
            return response;
        }

        private NotifyProcess Parser(NotifyRequest notifyRequest)
            => new NotifyProcess()
            {
                DataHora = notifyRequest.DataHora,
                Evento = notifyRequest.Evento,
                HashMensagem = notifyRequest.HashMensagem,
                Protocolo = notifyRequest.Protocolo,
                Mensagem = JsonConvert.DeserializeObject<NotifyMessage>(notifyRequest.Mensagem)
            };

        private void Validate(NotifyProcess notifyProcess)
        {
            var validationResult = _validator.Validate(notifyProcess);
            if (!validationResult.IsValid)
                throw new ArgumentException(validationResult.ToString());
        }
    }
}
