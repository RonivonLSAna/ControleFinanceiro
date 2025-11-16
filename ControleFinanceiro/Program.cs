﻿using ControleFinanceiro.Services;

FinanceiroService service = new();

while (true)
{
    Console.WriteLine("\n==== CONTROLE FINANCEIRO ====");
    Console.WriteLine("1 - Cadastrar Entrada");
    Console.WriteLine("2 - Cadastrar Despesa");
    Console.WriteLine("3 - Listar Despesas");
    Console.WriteLine("4 - Avançar Mês");
    Console.WriteLine("5 - Antecipar Parcelas");
    Console.WriteLine("0 - Sair");
    Console.Write("Escolha: ");
    string op = Console.ReadLine();

    switch (op)
    {
        case "1": service.NovaEntrada(); break;
        case "2": service.NovaDespesa(); break;
        case "3": service.ListarDespesas(); break;
        case "4": service.AvancarMes(); break;
        case "5": service.AnteciparParcelas(); break;
        case "0": return;
        default: Console.WriteLine("Opção inválida"); break;
    }
}
