namespace ControleFinanceiro.Models
{
    public class Despesa
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal ValorTotal { get; set; }
        public int ParcelasTotais { get; set; }
        public int ParcelasRestantes { get; set; }
        public decimal ValorParcela { get; set; }
        public int MesAtual { get; set; }
        public int AnoAtual { get; set; }
        public bool Quitada { get; set; }
    }
}