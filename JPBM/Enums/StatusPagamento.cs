using System.ComponentModel;

namespace JPBM.Enums
{
    public enum StatusPagamento
    {
        [Description("Aguardando Pagamento")]
        AguardandoPagamento = 1,
        [Description("Aguardando Depósito")]
        AguardandoDeposito = 2,
        [Description("Pagamento Recebido")]
        PagamentoRecebido = 3,
        [Description("Pagamento Estornado")]
        PagamentoEstornado = 4
    }
}
