using ControleFinanceiro.Database;
using ControleFinanceiro.Models;
using MySql.Data.MySqlClient;

namespace ControleFinanceiro.Repositories
{
    public class DespesaRepository
    {
        public void Inserir(Despesa d)
        {
            using var conn = MySqlConnectionFactory.GetConnection();
            conn.Open();

            string sql = @"
                INSERT INTO despesas 
                (descricao, valor_total, parcelas_totais, parcelas_restantes, valor_parcela, mes_atual, ano_atual, quitada)
                VALUES
                (@desc, @total, @pt, @pr, @vp, @mes, @ano, 0);";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@desc", d.Descricao);
            cmd.Parameters.AddWithValue("@total", d.ValorTotal);
            cmd.Parameters.AddWithValue("@pt", d.ParcelasTotais);
            cmd.Parameters.AddWithValue("@pr", d.ParcelasRestantes);
            cmd.Parameters.AddWithValue("@vp", d.ValorParcela);
            cmd.Parameters.AddWithValue("@mes", d.MesAtual);
            cmd.Parameters.AddWithValue("@ano", d.AnoAtual);

            cmd.ExecuteNonQuery();
        }

        public List<Despesa> ListarAtivas()
        {
            List<Despesa> lista = new();

            using var conn = MySqlConnectionFactory.GetConnection();
            conn.Open();

            string sql = "SELECT * FROM despesas WHERE quitada = 0 ORDER BY ano_atual, mes_atual";

            using var cmd = new MySqlCommand(sql, conn);
            using var r = cmd.ExecuteReader();

            while (r.Read())
            {
                lista.Add(new Despesa
                {
                    Id = r.GetInt32("id"),
                    Descricao = r.GetString("descricao"),
                    ValorTotal = r.GetDecimal("valor_total"),
                    ParcelasTotais = r.GetInt32("parcelas_totais"),
                    ParcelasRestantes = r.GetInt32("parcelas_restantes"),
                    ValorParcela = r.GetDecimal("valor_parcela"),
                    MesAtual = r.GetInt32("mes_atual"),
                    AnoAtual = r.GetInt32("ano_atual"),
                    Quitada = r.GetBoolean("quitada")
                });
            }

            return lista;
        }

        public void Atualizar(Despesa d)
        {
            using var conn = MySqlConnectionFactory.GetConnection();
            conn.Open();

            string sql = @"
                UPDATE despesas 
                SET parcelas_restantes=@pr, mes_atual=@mes, ano_atual=@ano, quitada=@q 
                WHERE id=@id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@pr", d.ParcelasRestantes);
            cmd.Parameters.AddWithValue("@mes", d.MesAtual);
            cmd.Parameters.AddWithValue("@ano", d.AnoAtual);
            cmd.Parameters.AddWithValue("@q", d.Quitada);
            cmd.Parameters.AddWithValue("@id", d.Id);

            cmd.ExecuteNonQuery();
        }
    }
}
