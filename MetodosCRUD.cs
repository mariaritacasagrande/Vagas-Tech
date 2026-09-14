  using System;
  using Microsoft.Data.Sqlite;

  public static class MetodosCRUD
  {
      // =====================================================
      // 1. CREATE (Maria)
      // =====================================================

      //---a. Cadastrar Vaga---

      public static void CadastrarVaga(SqliteConnection conexao, int idVaga, string titulo, string empresa, decimal salario)
      {
          string query = @"INSERT INTO VAGAS (ID_VAGA, TITULO, EMPRESA, SALARIO)
                          VALUES (@idVaga, @titulo, @empresa, @salario);";

          using (var comando = new SqliteCommand(query, conexao))
          {
              comando.Parameters.AddWithValue("@idVaga", idVaga);
              comando.Parameters.AddWithValue("@titulo", titulo);
              comando.Parameters.AddWithValue("@empresa", empresa);
              comando.Parameters.AddWithValue("@salario", salario);
              comando.ExecuteNonQuery();
          }

          Console.WriteLine($"[OK] Vaga cadastrada: {titulo} - {empresa} (R$ {salario})");
      }

      //---b. Cadastrar Candidata---

      public static void CadastrarCandidata(SqliteConnection conexao, int idCandidata, string nome, string email)
      {
          string query = @"INSERT INTO CANDIDATAS (ID_CANDIDATA, NOME, EMAIL)
                          VALUES (@idCandidata, @nome, @email);";

          using (var comando = new SqliteCommand(query, conexao))
          {
              comando.Parameters.AddWithValue("@idCandidata", idCandidata);
              comando.Parameters.AddWithValue("@nome", nome);
              comando.Parameters.AddWithValue("@email", email);
              comando.ExecuteNonQuery();
          }

          Console.WriteLine($"[OK] Candidata cadastrada: {nome} ({email})");
      }

      //---c. Enviar Candidatura---

      public static void EnviarCandidatura(SqliteConnection conexao, int idCandidatura, int idVaga, int idCandidata)
      {
          string query = @"INSERT INTO CANDIDATURAS (ID_CANDIDATURA, DATA_ENVIO, ID_VAGA, ID_CANDIDATA)
                          VALUES (@idCandidatura, @dataEnvio, @idVaga, @idCandidata);";

          using (var comando = new SqliteCommand(query, conexao))
          {
              comando.Parameters.AddWithValue("@idCandidatura", idCandidatura);
              comando.Parameters.AddWithValue("@dataEnvio", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
              comando.Parameters.AddWithValue("@idVaga", idVaga);
              comando.Parameters.AddWithValue("@idCandidata", idCandidata);
              comando.ExecuteNonQuery();
          }

          Console.WriteLine($"[OK] Candidatura enviada: Inscrição {idCandidatura} (Vaga {idVaga} / Candidata {idCandidata})");
      }

      // =====================================================
      // 2. READ - Desafio do Double JOIN - Silvia
      // =====================================================
      //--- Consultar candidaturas ---
      public static void ConsultarCandidaturas(SqliteConnection conexao)
      {
          string query = @"SELECT CANDIDATAS.NOME, CANDIDATAS.EMAIL, VAGAS.TITULO, VAGAS.EMPRESA FROM CANDIDATURAS
                            INNER JOIN CANDIDATAS ON CANDIDATURAS.ID_CANDIDATA = CANDIDATAS.ID_CANDIDATA
                            INNER JOIN VAGAS ON CANDIDATURAS.ID_VAGA = VAGAS.ID_VAGA;";
          using(var comando = new SqliteCommand(query, conexao))
          using(var leitor = comando.ExecuteReader())
          {
              Console.WriteLine("\n--- CANDIDATURAS ---");

              while(leitor.Read())
              {
                  Console.WriteLine($"Candidata: {leitor["NOME"]} | " +
                                    $"E-mail: {leitor["EMAIL"]} | " +
                                    $"Vaga: {leitor["TITULO"]} | " +
                                    $"Empresa: {leitor["EMPRESA"]}"
                  );
              }
          }
      }

     // =====================================================
    // 3. UPDATE - Anna
    // =====================================================
    //--- Atualizar Salário da Vaga ---

    public static void AtualizarSalarioVaga(SqliteConnection conexao, int idVaga, decimal novoSalario)
    {
        string query = @"UPDATE VAGAS
                         SET SALARIO = @novoSalario
                         WHERE ID_VAGA = @idVaga;";

        using (var comando = new SqliteCommand(query, conexao))
        {
            comando.Parameters.AddWithValue("@novoSalario", novoSalario);
            comando.Parameters.AddWithValue("@idVaga", idVaga);

            int linhasAfetadas = comando.ExecuteNonQuery();

            if (linhasAfetadas > 0)
            {
                Console.WriteLine($"[OK] Salário da vaga {idVaga} atualizado para R$ {novoSalario}.");
            }
            else
            {
                Console.WriteLine($"[ALERTA] Vaga {idVaga} não encontrada para atualização.");
            }
        }
    }

      // =====================================================
      // DELETE - Kênia
      // =====================================================
      //--- Cancelar Candidatura ---

      public static void CancelarCandidatura(SqliteConnection conexao, int idCandidatura)
      {
          string query = @"DELETE FROM CANDIDATURAS
                          WHERE ID_CANDIDATURA = @idCandidatura;";

          using (var comando = new SqliteCommand(query, conexao))
          {
              comando.Parameters.AddWithValue("@idCandidatura", idCandidatura);

              int linhasAfetadas = comando.ExecuteNonQuery();

              if (linhasAfetadas > 0)
              {
                  Console.WriteLine($"[OK] Candidatura {idCandidatura} cancelada com sucesso.");
              }
              else
              {
                  Console.WriteLine($"[ALERTA] Candidatura {idCandidatura} não encontrada no banco.");
              }
          }
      }
  }
