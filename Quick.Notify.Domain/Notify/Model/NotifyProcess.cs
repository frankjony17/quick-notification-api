using System;

namespace Quick.Notify.Domain.Notify.Model
{
    public class NotifyProcess
    {
        public DateTime MessageDate { get => DateTime.Now; }
        public string Protocolo { get; set; }

        public long Evento { get; set; }

        public NotifyMessage Mensagem { get; set; }

        public string HashMensagem { get; set; }

        public DateTimeOffset DataHora { get; set; }
    }
}
