using System;

namespace Quick.Notify.Domain.Notify.Model
{
    public class NotifyRequest
    {
        public string Protocolo { get; set; }

        public long Evento { get; set; }

        public string Mensagem { get; set; }

        public string HashMensagem { get; set; }

        public DateTimeOffset DataHora { get; set; }
    }
}
