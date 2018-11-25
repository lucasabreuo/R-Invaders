namespace RInvaders.Modelos
{
  public static class DescriptionOfPhases
  {
    #region Menu Principal
    public static string MenuTitle()
    {
      return @"GSInvaders";
    }

    public static string MenuSubTitle()
    {
      return @"

Começar (G)
               
Controles:             
P - Pause
Left Arrow - Move Left
Right Arrow - Move Right
Space - Fire";
    }
    #endregion

    #region Instruções
    public static string Preface()
    {
      return $@"

Caro tripulante, bem-vindo a missão mais divertida de todos os tempos, a 
partir de agora você irá aprender ou revisar os conceitos de uma linguagem 
de programação de um jeito diferente.

O GSInvaders é um game onde você irá relembrar o clássico Space Invaders
que fez sucesso na década de 70.

A linguagem de programação a ser estudada nesse game é muito utilizada nos
ambientes acadêmicos para computação estatística e construção de gráficos,
ela é conhecida como R.               

Antes de começarmos, vamos aprender os atalhos básicos do jogo:

N - para avançar para a próxima instrução
Q - para encerrar a aplicação
S - para voltar ao menu principal

Pontuações:

     - 1 ponto

     
     - 5 a 1 ponto (Caso acerte a resposta correta de primeira, atinge
                    a pontuação máxima de 5 pontos. Senão, para cada 
                    inimigo incorreto abatido, perde-se 1 ponto da
                    pontuação máxima, sendo o mínimo a ser ganho 1
                    ponto.)

Vamos lá!";
    }
    #endregion

    #region Fase Bônus
    public static string FaseBonus()
    {
      return @"

Olá caro tripulante, se você chegou até aqui, meus parabéns, isso significa
que você obteve ótimo desempenho acertando, consecutivamente, 5 respostas 
de primeira. 


Como forma de gratificar-lhe por essa excelente atuação, disponibilizamos uma 
FASE BÔNUS para que você aumente a sua pontuação e ganhe uma vida extra para
continuar nessa jornada interestelar.

Há, mas cuidado, essa fase pode ser mais desafiadora do que você imagina.

Boa sorte!";
    }
    #endregion

    #region Pós-Jogo
    public static string EndGame(int score)
    {
      return $@"

Parabéns, você chegou ao final do Game. Esperamos que tenha gostado!

Sua pontuação final foi de {score} pontos.

Dúvidas, sugestões ou reclamações, entre em contato:

Alunos: Erick Moreira - erickmoreiratc@gmail.com
        Lucas Abreu - lucaso.talentos@gmail.com

Orientador: Luís Fabrício - lfwgoes@yahoo.com.br

Obrigado!


Q - Para sair   S - Para voltar ao Menu Principal

";
    }
    #endregion
  }
}
