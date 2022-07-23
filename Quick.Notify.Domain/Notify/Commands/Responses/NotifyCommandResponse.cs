namespace Quick.Notification.Domain.Notify.Commands.Responses
{
    public class NotifyCommandResponse
    {
        public bool Result { get; set; }
        public string Error { get; set; }
        public string CodigoRetorno { get; set; }
        public string DescricaoMensagemRetorno { get; set; }
    }
}
