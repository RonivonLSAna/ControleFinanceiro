using ControleFinanceiro.Models;
using ControleFinanceiro.Repositories;

namespace ControleFinanceiro.Services
{
    public class FinanceiroService
    {
        private readonly EntradaRepository entradaRepo = new();
        private readonly DespesaRepository despesaRepo = new();

        public void NovaEntrada()
        {
            Console.Write("Descrição da entrada: ");
            string desc = Console.ReadLine();

            Console.Write("Valor: ");
            decimal valor = decimal.Parse(Console.ReadLine());

            Console.Write("Data (yyyy-MM-dd): ");
            DateTime data = DateTime.Parse(Console.ReadLine());

            entradaRepo.Inserir(new Entrada
            {
                Descricao = desc,
                Valor = valor,
                Data = data
            });

            Console.WriteLine("Entrada registrada!");
        }

        public void NovaDespesa()
        {
            Console.Write("Descrição da despesa: ");
            string desc = Console.ReadLine();

            Console.Write("Valor total: ");
            decimal total = decimal.Parse(Console.ReadLine());

            Console.Write("Parcelas (1 = à vista): ");
            int parcelas = int.Parse(Console.ReadLine());

            decimal valorParcela = Math.Round(total / parcelas, 2);

            Console.Write("Mês inicial: ");
            int mes = int.Parse(Console.ReadLine());

            Console.Write("Ano inicial: ");
            int ano = int.Parse(Console.ReadLine());

            despesaRepo.Inserir(new Despesa
            {
                Descricao = desc,
                ValorTotal = total,
                ParcelasTotais = parcelas,
                ParcelasRestantes = parcelas,
                ValorParcela = valorParcela,
                MesAtual = mes,
                AnoAtual = ano,
                Quitada = false
            });

            Console.WriteLine("Despesa registrada!");
        }

        public void ListarDespesas()
        {
            var list = despesaRepo.ListarAtivas();

            foreach (var d in list)
            {
                Console.WriteLine(
                    $"[{d.Id}] {d.Descricao} - Restante: {d.ParcelasRestantes} - Parcela: {d.ValorParcela} - {d.MesAtual}/{d.AnoAtual}");
            }
        }

        public void AvancarMes()
        {
            var list = despesaRepo.ListarAtivas();

            foreach (var d in list)
            {
                d.ParcelasRestantes--;

                if (d.ParcelasRestantes <= 0)
                {
                    d.ParcelasRestantes = 0;
                    d.Quitada = true;
                }

                d.MesAtual++;
                if (d.MesAtual > 12)
                {
                    d.MesAtual = 1;
                    d.AnoAtual++;
                }

                despesaRepo.Atualizar(d);
            }

            Console.WriteLine("Mês avançado!");
        }

        public void AnteciparParcelas()
        {
            ListarDespesas();

            Console.Write("\nID da despesa: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Quantas parcelas antecipar? ");
            int qtd = int.Parse(Console.ReadLine());

            var list = despesaRepo.ListarAtivas();
            var d = list.First(x => x.Id == id);

            if (qtd > d.ParcelasRestantes)
            {
                Console.WriteLine("\n❌ Número maior do que parcelas restantes!");
                return;
            }

            d.ParcelasRestantes -= qtd;

            if (d.ParcelasRestantes == 0)
                d.Quitada = true;

            despesaRepo.Atualizar(d);

            Console.WriteLine("Parcelas antecipadas!");
        }
    }
}
