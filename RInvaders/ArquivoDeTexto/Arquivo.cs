using System;
using System.Collections.Generic;
using System.IO;

namespace ArquivoDeTexto
{
  public class Arquivo
  {
    public FileStream arq;
    private string arquivo;
    private string separador = "|";

    #region Construtor
    public Arquivo(string caminhoArquivo)
    {
      arquivo = caminhoArquivo;
      if (!File.Exists(arquivo))
      {
        FileStream f = new FileStream(arquivo, FileMode.OpenOrCreate);
        f.Close();
      }
    }
    #endregion

    #region Métodos Auxiliares
    public bool SalvarDados(params object[] dados)
    {
      return SalvarDados(dados, separador);
    }

    private bool SalvarDados(object[] dados, string separador)
    {
      try
      {
        using (StreamWriter escrever = new StreamWriter(arquivo, true))
        {
          string linha = string.Join(separador, dados);
          escrever.WriteLine(linha);
        }
      }
      catch (Exception)
      {
        return false;
      }
      return true;
    }

    public List<string[]> Ler()
    {
      List<string[]> list = new List<string[]>();
      using (StreamReader sr = new StreamReader(arquivo))
      {
        while (!sr.EndOfStream)
        {
          string linha = sr.ReadLine();
          string[] dados = linha.Split(separador[0]);
          list.Add(dados);
        }
      }
      return list;
    }

    public void Excluir()
    {
      File.Delete(arquivo);
    }
    #endregion
  }
}
