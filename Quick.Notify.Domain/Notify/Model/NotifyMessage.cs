namespace Quick.Notify.Domain.Notify.Model
{
    public class NotifyMessage
    {
        public string EndToendId { get; set; }
        public string ReturnId { get; set; }
        public long? StatusOperacao { get; set; }
        public string DescricaoStatusOperacao { get; set; }
        public string DescricaoErro { get; set; }
        public string PagadorIspb { get; set; }
        public string NomeUsuarioPagador { get; set; }
        public string AgenciaUsuarioPagador { get; set; }
        public string ContaUsuarioPagador { get; set; }
        public string IspbUsuarioRecebedor { get; set; }
        public string CpfCnpjUsuarioRecebedor { get; set; }
        public string NomeUsuarioRecebedor { get; set; }
        public string AgenciaUsuarioRecebedor { get; set; }
        public string ContaUsuarioRecebedor { get; set; }
        public string IdConciliacaoRecebedor { get; set; }
        public string ValorOperacao { get; set; }
        public string InformacaoEntreUsuarios { get; set; }
        public string DataHoraLiquidacao { get; set; }
        public string DataContabil { get; set; }
        public string valorPagamentoOriginal { get; set; }
        public string dataHoraPagamentoOriginal { get; set; }
        public string idConciliacaoOriginal { get; set; }
    }
}
