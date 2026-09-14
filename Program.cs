// =================================
// PROGRAM - Maria
// =================================


using System;
using Microsoft.Data.Sqlite;

namespace VagasTechApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=/content/vagastech.db";

            using (var conexao = new SqliteConnection(connectionString))
            {
                conexao.Open();
                Console.WriteLine("Conexão com vagastech.db aberta com sucesso.\n");

                // 1. Cadastrar duas vagas
                MetodosCRUD.CadastrarVaga(conexao, 1, "Engenheira de Dados", "Empresa Alfa", 9000m);
                MetodosCRUD.CadastrarVaga(conexao, 2, "Analista de BI", "Empresa Ômega", 7500m);

                // 2. Cadastrar a candidata Mariana Souza
                MetodosCRUD.CadastrarCandidata(conexao, 1, "Mariana Souza", "mariana.souza@email.com");

                // 3. Candidatar Mariana nas duas vagas (inscrições 901 e 902)
                MetodosCRUD.EnviarCandidatura(conexao, 901, 1, 1);
                MetodosCRUD.EnviarCandidatura(conexao, 902, 2, 1);

                // 4. Consultar a lista de candidaturas na tela
                MetodosCRUD.ConsultarCandidaturas(conexao);

                // 5. Atualizar o salário da vaga de Engenheira de Dados para R$ 9.500
                MetodosCRUD.AtualizarSalarioVaga(conexao, 1, 9500m);

                // 6. Cancelar a candidatura de Mariana para Analista de BI (inscrição 902)
                MetodosCRUD.CancelarCandidatura(conexao, 902);

                Console.WriteLine("✅ Lista final das candidatas ativas.");
                // 7. Consultar a lista final de candidaturas ativas
                MetodosCRUD.ConsultarCandidaturas(conexao);

                conexao.Close();
                Console.WriteLine("Pipeline finalizado. Conexão encerrada.");
            }
        }
    }
}
