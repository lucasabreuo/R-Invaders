using ArquivoDeTexto;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using static RInvaders.Modelos.Enum;

namespace RInvaders.Modelos
{
  public class Game : ICaminhoDoArquivo
  {
    #region Variáveis    
    private int wave = 0;
    private int framesSkipped = 6;
    private int currentGameFrame = 1;
    private int currentGameFrameAnswers = 1;
    private int ControlaInvaders = 6;
    private int Nivel = 1;
    private int PontosMaxGanhos = 5;
    private int Count = 0;
    private bool AvancouDeFase = false;
    private bool IniciouTempoTotal = false;
    private bool Salvou = false;
    private string Answer = string.Empty;
    private string CalculaMedia = string.Empty;
    private string User = string.Empty;

    #region Contadores de Tiros
    private int CountFireWave1 = 0;
    private int CountFireWave2 = 0;
    private int CountFireWave3 = 0;
    private int CountFireWave4 = 0;
    private int CountFireWave5 = 0;
    private int CountFireWave6 = 0;
    private int CountFireWave7 = 0;
    private int CountFireWave8 = 0;
    private int CountFireWave9 = 0;
    private int CountFireWave10 = 0;
    private int CountFireWave11 = 0;
    private int CountFireWave12 = 0;
    private int CountFireWave13 = 0;
    private int CountFireWave14 = 0;
    #endregion

    #region Tempos
    private DateTime InicioDoGame;
    private DateTime InicioFase1;
    private DateTime InicioFase2;
    private DateTime InicioFase3;
    private DateTime InicioFase4;
    private DateTime InicioFase5;
    private DateTime InicioFase6;
    private DateTime InicioFase7;
    private DateTime InicioFase8;
    private DateTime InicioFase9;
    private DateTime InicioFase10;
    private DateTime InicioFase11;
    private DateTime InicioFase12;
    private DateTime InicioFase13;
    private DateTime InicioFase14;
    public DateTime InicioFaseBonus;
    public TimeSpan DuracaoFaseBonus;
    private TimeSpan DuracaoTotal;
    private TimeSpan DuracaoFase1;
    private TimeSpan DuracaoFase2;
    private TimeSpan DuracaoFase3;
    private TimeSpan DuracaoFase4;
    private TimeSpan DuracaoFase5;
    private TimeSpan DuracaoFase6;
    private TimeSpan DuracaoFase7;
    private TimeSpan DuracaoFase8;
    private TimeSpan DuracaoFase9;
    private TimeSpan DuracaoFase10;
    private TimeSpan DuracaoFase11;
    private TimeSpan DuracaoFase12;
    private TimeSpan DuracaoFase13;
    private TimeSpan DuracaoFase14;
    #endregion

    #region Score por Fases
    private int CountScoreWave1 = 0;
    private int CountScoreWave2 = 0;
    private int CountScoreWave3 = 0;
    private int CountScoreWave4 = 0;
    private int CountScoreWave5 = 0;
    private int CountScoreWave6 = 0;
    private int CountScoreWave7 = 0;
    private int CountScoreWave8 = 0;
    private int CountScoreWave9 = 0;
    private int CountScoreWave10 = 0;
    private int CountScoreWave11 = 0;
    private int CountScoreWave12 = 0;
    private int CountScoreWave13 = 0;
    private int CountScoreWave14 = 0;
    #endregion

    private Rectangle boundaries;
    private Random random;

    private Direction invaderDirection;
    private Direction invaderAnswerDirection;
    private List<Invader> invaders;
    private List<Invader> invadersMenu;
    public List<Invader> invadersBonus { get; private set; }
    private List<Invader> invadersAnswers;
    private List<Invader> deadInvaders = new List<Invader>();

    private Player player;
    private PointF scoreLocation;
    private PointF livesLocation;
    private PointF waveLocation;
    private PointF codeLocation;
    private PointF messageLocation;
    private PointF prefaceLocation;
    private PointF menuLocation;

    private Stars stars;

    public event EventHandler GameOver;
    public event EventHandler Pause;

    private Font menuFont = new Font(FontFamily.GenericMonospace, 80, FontStyle.Bold);
    private Font labelsFont = new Font(FontFamily.GenericMonospace, 50, FontStyle.Bold);
    private Font estatisticasFont = new Font(FontFamily.GenericMonospace, 15);
    private Font menuEstatisticasFont = new Font(FontFamily.GenericMonospace, 18);
    private Font titleFont = new Font(FontFamily.GenericMonospace, 18, FontStyle.Bold);

    private string DiretorioArquivo { get { return CaminhoArquivo(); } }
    private string Caminho = Directory.GetCurrentDirectory(); //diretório corrente do aplicativo
    Arquivo arquivo;

    private List<string[]> listCodigoFonte;
    private Dictionary<int, string> dicResposta;

    public List<Shot> PlayerShots { get; set; }

    public List<Shot> InvaderShots { get; set; }

    /// <summary>
    /// Placar do Jogo
    /// </summary>
    public int Score { get; private set; }

    /// <summary>
    /// Vidas Restantes
    /// </summary>
    public int LivesLeft { get; set; }

    /// <summary>
    /// Mensagem após terminar uma fase
    /// </summary>
    public bool ShowingMessage { get; set; }

    /// <summary>
    /// Indica se apresenta a mensagem da fase bonus
    /// </summary>
    public bool ShowingMessageBonus { get; set; }

    /// <summary>
    /// Indica se apresenta o texto de introdução
    /// </summary>
    public bool ShowingPreface { get; set; }

    /// <summary>
    /// Indica se apresenta o Menu Principal
    /// </summary>
    public bool ShowingMenu { get; set; }

    /// <summary>
    /// Indica se desenha o texto da Fase Bonus
    /// </summary>
    public bool ShowingBonusText { get; set; }

    /// <summary>
    /// Indica se desenha a Fase Bonus
    /// </summary>
    public bool ShowingBonus { get; set; }

    /// <summary>
    /// Indica se apresenta as questões a serem respondidas
    /// </summary>
    public bool ShowingQuestions { get; set; }    

    /// <summary>
    /// Indica se o jogador acertou a resposta de primeira
    /// </summary>
    public int ContPassouDePrimeira { get; set; }
    #endregion

    #region Construtor
    public Game(Random random, Rectangle boundaries)
    {
      this.boundaries = boundaries;
      this.random = random;
      this.boundaries.Width = Consts.WidthBoundaries;
      boundaries.Width = Consts.WidthBoundaries;
      stars = new Stars(random, boundaries);
      Score = 0;
      LivesLeft = 5;
      Count = 300;
      ShowingMenu = true;
      ShowingPreface = true;
      ShowingBonusText = false;
      ShowingBonus = false;
      ShowingQuestions = true;      
      menuLocation = new PointF(boundaries.Left + 150.0F, boundaries.Top + 250.0F);
      prefaceLocation = new PointF(boundaries.Left + 5.0F, boundaries.Top + 5.0F);
      scoreLocation = new PointF(boundaries.Left + 5.0F, boundaries.Top + 5.0F);
      livesLocation = new PointF(boundaries.Right - 120.0F, boundaries.Top + 5.0F);
      waveLocation = new PointF(boundaries.Left + 5.0F, boundaries.Top + 25.0F);
      codeLocation = new PointF(boundaries.Right + 5.0F, boundaries.Top + 5.0F);
      messageLocation = new Point(boundaries.Width / 4, boundaries.Height / 3);
      player = new Player(boundaries, new Point(boundaries.Width / 2, boundaries.Height - 50));
      PlayerShots = new List<Shot>();
      InvaderShots = new List<Shot>();
      invaders = new List<Invader>();
      invadersMenu = new List<Invader>();
      invadersBonus = new List<Invader>();
      invadersAnswers = new List<Invader>();
      listCodigoFonte = new List<string[]>();
      arquivo = new Arquivo(DiretorioArquivo);

      NextWave();
    }
    #endregion

    #region Métodos do Game
    /// <summary>
    /// Desenha todo o gráfico do jogo.
    /// </summary>
    /// <param name="graphics">Objeto gráfico</param>
    /// <param name="animationCell">Célula de animação</param>
    /// <param name="gameOver">True, se gameOver</param>
    public void Draw(Graphics graphics, int animationCell, bool gameOver, bool pause, string user)
    {
      User = user;

      //Desenha a área do jogo
      graphics.FillRectangle(Brushes.Black, boundaries);

      //Desenha os objetos do jogo [estrelas, invaders, jogador, estatisticas]
      stars.Draw(graphics);

      if (!ShowingBonusText && !ShowingMenu && !ShowingPreface && !ShowingBonus && wave != 15)
      {
        graphics.DrawString("Pontos: " + Score.ToString(), estatisticasFont, Brushes.Red, scoreLocation);
        graphics.DrawString("Vidas: " + LivesLeft.ToString(), estatisticasFont, Brushes.Red, livesLocation);
        graphics.DrawString("Nível: " + Nivel.ToString() + " Fase: " + wave.ToString(), estatisticasFont, Brushes.Red, waveLocation);

        if (!ShowingMessageBonus)
          player.Draw(graphics);
      }

      if (ShowingMessage)
      {
        graphics.DrawString(DefineMensagem() + Environment.NewLine + Environment.NewLine + "Pressione Enter para continuar", estatisticasFont, Brushes.Yellow, messageLocation);
        return;
      }
      else if (ShowingMessageBonus)
      {
        graphics.DrawString(DefineMensagemBonus() + Environment.NewLine + Environment.NewLine + "Pressione Enter para continuar", estatisticasFont, Brushes.Yellow, messageLocation);
        return;
      }

      if (gameOver)
      {
        graphics.DrawString("GAME OVER", labelsFont, Brushes.Red, boundaries.Width / 4, boundaries.Height / 3);
        graphics.DrawString("Q - Para sair \nS - Para voltar ao Menu Principal", menuEstatisticasFont, Brushes.Red, 280, 320);
      }

      if (wave == 15 || gameOver)
      {
        if (!Salvou)
        {
          if (wave >= 14)
            DuracaoFase14 = DateTime.Now - InicioFase14;

          if (wave == 1)
            DuracaoFase1 = DateTime.Now - InicioFase1;

          DuracaoTotal = DateTime.Now - InicioDoGame;

          Salvou = true;
        }

        if (!gameOver)
        {
          graphics.DrawString("FIM DE JOGO", titleFont, Brushes.Yellow, prefaceLocation);
          graphics.DrawString(DescriptionOfPhases.EndGame(Score), estatisticasFont, Brushes.Yellow, prefaceLocation);
        }

        return;
      }

      if (ShowingMenu)
      {
        graphics.DrawString(DescriptionOfPhases.MenuTitle(), menuFont, Brushes.Yellow, menuLocation);
        graphics.DrawString(DescriptionOfPhases.MenuSubTitle(), menuEstatisticasFont, Brushes.Yellow, menuLocation.X + 180, menuLocation.Y + 100);
        InvadersMenu();

        foreach (Invader invader in invadersMenu)
          invader.Draw(graphics, animationCell, true);

        return;
      }      

      if (ShowingPreface)
      {
        graphics.DrawString("Instruções", titleFont, Brushes.Yellow, prefaceLocation);
        graphics.DrawString(DescriptionOfPhases.Preface(), estatisticasFont, Brushes.Yellow, prefaceLocation);
        SolidBrush brush = new SolidBrush(Color.Red);
        graphics.DrawRectangle(new Pen(brush), new Rectangle(700, 700, 150, 35));
        graphics.DrawString("Avançar (N)", estatisticasFont, Brushes.Yellow, prefaceLocation.X + 700, prefaceLocation.Y + 700);
        graphics.DrawImage(Properties.Resources.bug1, 10, 462);
        graphics.DrawImage(Properties.Resources.bug1, 10, 540);
        graphics.DrawString("A", titleFont, Brushes.Red, 22, 580);
        return;
      }

      if (ShowingBonusText)
      {
        graphics.DrawString("FASE BÔNUS", titleFont, Brushes.Yellow, prefaceLocation);
        graphics.DrawString(DescriptionOfPhases.FaseBonus(), estatisticasFont, Brushes.Yellow, prefaceLocation);
        SolidBrush brush = new SolidBrush(Color.Red);
        graphics.DrawRectangle(new Pen(brush), new Rectangle(700, 400, 150, 35));
        graphics.DrawString("Começar (N)", estatisticasFont, Brushes.Yellow, prefaceLocation.X + 700, prefaceLocation.Y + 400);
        return;
      }

      if (ShowingBonus)
      {
        FaseBonus(graphics, animationCell, pause, gameOver);
      }
      else
      {
        foreach (Invader invader in invaders)
          invader.Draw(graphics, animationCell, false);

        if (ShowingQuestions)
        {
          Pause(this, null);
          graphics.DrawString("Atire nos inimigos " + Environment.NewLine + "respondendo a pergunta abaixo: " + Environment.NewLine + Environment.NewLine +
                             FasesGamefication() + "\n\n\nPressione Enter para continuar.", estatisticasFont, Brushes.Yellow, boundaries.Width / 4, 400);

          if (!IniciouTempoTotal)
          {
            InicioDoGame = DateTime.Now;
            IniciouTempoTotal = true;
          }
        }
        else
        {
          TempoPorFases();

          graphics.DrawString(FasesGamefication(), estatisticasFont, Brushes.Yellow, waveLocation.X + 250, waveLocation.Y);
          graphics.DrawString(OpcoesGamefication(), estatisticasFont, Brushes.Yellow, waveLocation.X + 10, waveLocation.Y + 700);

          foreach (Shot shot in PlayerShots)
            shot.Draw(graphics);

          foreach (Shot shot in InvaderShots)
            shot.Draw(graphics);

          foreach (Invader invader in invadersAnswers)
            invader.Draw(graphics, animationCell, false);

          if (wave == 7 || wave == 8 || wave == 9)
            graphics.DrawString(Answer, estatisticasFont, Brushes.Yellow, 60, 600);

          if (wave == 13 || wave == 14)
          {
            if (string.IsNullOrEmpty(CalculaMedia))
              CalculaMedia = "calculaMedia <- ";
            graphics.DrawString(CalculaMedia, estatisticasFont, Brushes.Yellow, 60, 600);
          }

          if (Count % 10 == 0 && !pause && !gameOver)
          {
            InvadersAnswers(graphics, animationCell);
          }
          Count++;

          MontaCodigoGamefication(graphics);
        }

        if (pause)
          graphics.DrawString("PAUSE", labelsFont, Brushes.Red, boundaries.Width / 4, boundaries.Height / 3);
      }
    }

    /// <summary>
    /// Tempo do usuário em cada fase do jogo
    /// </summary>
    private void TempoPorFases()
    {
      if (wave == 1)
      {
        if (InicioFase1 == DateTime.MinValue)
          InicioFase1 = DateTime.Now;
      }
      else if (wave == 2)
      {
        if (DuracaoFase1 == TimeSpan.Zero)
          DuracaoFase1 = DateTime.Now - InicioFase1;

        if (InicioFase2 == DateTime.MinValue)
          InicioFase2 = DateTime.Now;
      }
      else if (wave == 3)
      {
        if (DuracaoFase2 == TimeSpan.Zero)
          DuracaoFase2 = DateTime.Now - InicioFase2;

        if (InicioFase3 == DateTime.MinValue)
          InicioFase3 = DateTime.Now;
      }
      else if (wave == 4)
      {
        if (DuracaoFase3 == TimeSpan.Zero)
          DuracaoFase3 = DateTime.Now - InicioFase3;

        if (InicioFase4 == DateTime.MinValue)
          InicioFase4 = DateTime.Now;
      }
      else if (wave == 5)
      {
        if (DuracaoFase4 == TimeSpan.Zero)
          DuracaoFase4 = DateTime.Now - InicioFase4;

        if (InicioFase5 == DateTime.MinValue)
          InicioFase5 = DateTime.Now;
      }
      else if (wave == 6)
      {
        if (DuracaoFase5 == TimeSpan.Zero)
          DuracaoFase5 = DateTime.Now - InicioFase5;

        if (InicioFase6 == DateTime.MinValue)
          InicioFase6 = DateTime.Now;
      }
      else if (wave == 7)
      {
        if (DuracaoFase6 == TimeSpan.Zero)
          DuracaoFase6 = DateTime.Now - InicioFase6;

        if (InicioFase7 == DateTime.MinValue)
          InicioFase7 = DateTime.Now;
      }
      else if (wave == 8)
      {
        if (DuracaoFase7 == TimeSpan.Zero)
          DuracaoFase7 = DateTime.Now - InicioFase7;

        if (InicioFase8 == DateTime.MinValue)
          InicioFase8 = DateTime.Now;
      }
      else if (wave == 9)
      {
        if (DuracaoFase8 == TimeSpan.Zero)
          DuracaoFase8 = DateTime.Now - InicioFase8;

        if (InicioFase9 == DateTime.MinValue)
          InicioFase9 = DateTime.Now;
      }
      else if (wave == 10)
      {
        if (DuracaoFase9 == TimeSpan.Zero)
          DuracaoFase9 = DateTime.Now - InicioFase9;

        if (InicioFase10 == DateTime.MinValue)
          InicioFase10 = DateTime.Now;
      }
      else if (wave == 11)
      {
        if (DuracaoFase10 == TimeSpan.Zero)
          DuracaoFase10 = DateTime.Now - InicioFase10;

        if (InicioFase11 == DateTime.MinValue)
          InicioFase11 = DateTime.Now;
      }
      else if (wave == 12)
      {
        if (DuracaoFase11 == TimeSpan.Zero)
          DuracaoFase11 = DateTime.Now - InicioFase11;

        if (InicioFase12 == DateTime.MinValue)
          InicioFase12 = DateTime.Now;
      }
      else if (wave == 13)
      {
        if (DuracaoFase12 == TimeSpan.Zero)
          DuracaoFase12 = DateTime.Now - InicioFase12;

        if (InicioFase13 == DateTime.MinValue)
          InicioFase13 = DateTime.Now;
      }
      else if (wave == 14)
      {
        if (DuracaoFase13 == TimeSpan.Zero)
          DuracaoFase13 = DateTime.Now - InicioFase13;

        if (InicioFase14 == DateTime.MinValue)
          InicioFase14 = DateTime.Now;
      }
    }

    /// <summary>
    /// Configura o cenário da Fase Bônus
    /// </summary>
    /// <param name="graphics"></param>
    /// <param name="animationCell"></param>
    /// <param name="pause"></param>
    private void FaseBonus(Graphics graphics, int animationCell, bool pause, bool gameOver)
    {
      if (InicioFaseBonus == DateTime.MinValue)
        InicioFaseBonus = DateTime.Now;

      foreach (Invader invader in invadersBonus)
        invader.Draw(graphics, animationCell, true);

      player.Draw(graphics);

      foreach (Shot shot in PlayerShots)
        shot.Draw(graphics);

      foreach (Shot shot in InvaderShots)
        shot.Draw(graphics);

      graphics.DrawString("Pontos: " + Score.ToString(), estatisticasFont, Brushes.Red, scoreLocation);
      graphics.DrawString("Vidas: " + LivesLeft.ToString(), estatisticasFont, Brushes.Red, livesLocation);
      graphics.DrawString("Nível: BÔNUS Fase: BÔNUS ", estatisticasFont, Brushes.Red, waveLocation);

      if (pause)
        graphics.DrawString("PAUSE", labelsFont, Brushes.Red, boundaries.Width / 4, boundaries.Height / 3);

      if (gameOver)
        graphics.DrawString("GAME OVER", labelsFont, Brushes.Red, boundaries.Width / 4, boundaries.Height / 3);
    }

    private void MontaCodigoGamefication(Graphics graphics)
    {
      DicRespostaCode(wave);

      if (deadInvaders.Count > 0)
      {
        foreach (Invader invader in deadInvaders)
        {
          if (dicResposta.ContainsValue(invader.Code))
          {
            WaveAnswer7_8_9(graphics, invader);
            WaveAnswer13_14(graphics);
            ProximaFase();
          }
        }
        if (AvancouDeFase)
          deadInvaders.Clear();
      }
    }

    /// <summary>
    /// Monta a resposta das fases 7, 8 e 9 do 2º Nível
    /// </summary>
    /// <param name="graphics"></param>
    /// <param name="invader"></param>
    private void WaveAnswer7_8_9(Graphics graphics, Invader invader)
    {
      if (wave == 7 || wave == 8 || wave == 9)
      {
        string chaves = string.Empty;

        if (wave == 8)
        {
          chaves = @" (i in 0:9) {
}";
          graphics.DrawString(chaves, estatisticasFont, Brushes.Yellow, 60, 400);

          Answer += chaves;
        }
        else if (wave == 9)
        {
          Answer = string.Empty;
          chaves = string.Empty;
          chaves = @"for (i in 0:9) {

print(i)

}";
          Answer = chaves;
          graphics.DrawString(chaves, estatisticasFont, Brushes.Yellow, 60, 400);
        }
        else
        {
          graphics.DrawString(invader.Code, estatisticasFont, Brushes.Yellow, 60, 400);
          Answer += invader.Code;
        }
      }
    }

    /// <summary>
    /// Monta a resposta das fases 13 e 14 do 3º Nível 
    /// </summary>
    /// <param name="graphics"></param>
    private void WaveAnswer13_14(Graphics graphics)
    {
      if (wave == 13 || wave == 14)
      {
        string chaves = string.Empty;

        if (wave == 13)
        {
          chaves = @"function(a) {

}";
          CalculaMedia += chaves;
          graphics.DrawString(CalculaMedia, estatisticasFont, Brushes.Yellow, 60, 600);
        }
        else
        {
          CalculaMedia = string.Empty;
          chaves = string.Empty;
          chaves = @"calculaMedia <- function(a) {

mean(a)

}";
          CalculaMedia = chaves;
          graphics.DrawString(chaves, estatisticasFont, Brushes.Yellow, 60, 600);
        }
      }
    }

    /// <summary>
    /// Prepara o Game para a próxima fase.
    /// </summary>
    private void ProximaFase()
    {
      ShowingQuestions = true;
      ShowingMessage = true;
      InvaderShots.Clear();
      PlayerShots.Clear();
      Pause(this, null);
      invaders.Clear();
      invadersAnswers.Clear();

      if (invaders.Count < 1)
        NextWave();

      AvancouDeFase = true;
      PontosMaxGanhos = 5;
      Count = 0;
    }

    /// <summary>
    /// Método responsável por fazerem as estrelas brilharem.
    /// </summary>
    public void Twinkle()
    {
      stars.Twinkle(random);
    }

    /// <summary>
    /// Movimenta o jogador.
    /// </summary>
    /// <param name="direction">Direção, direita ou esquerda</param>
    /// <param name="gameOver">True, se gameOver</param>
    public void MovePlayer(Direction direction, bool gameOver, bool pause)
    {
      if (!gameOver && !pause)
        player.Move(direction);
    }

    /// <summary>
    /// Método para adicionar o tiro do jogador na tela.
    /// </summary>
    public void FireShot()
    {
      if (PlayerShots.Count < 4)
      {
        Shot shotPlayer = new Shot(new Point(player.Location.X + (player.image.Width / 2), player.Location.Y), Direction.Up, boundaries);

        PlayerShots.Add(shotPlayer);

        CountFireShot();
      }
    }

    /// <summary>
    /// Contadores de Tiros do Jogador por Fase
    /// </summary>
    private void CountFireShot()
    {
      if (wave == 1)
      {
        CountFireWave1++;
      }
      else if (wave == 2)
      {
        CountFireWave2++;
      }
      else if (wave == 3)
      {
        CountFireWave3++;
      }
      else if (wave == 4)
      {
        CountFireWave4++;
      }
      else if (wave == 5)
      {
        CountFireWave5++;
      }
      else if (wave == 6)
      {
        CountFireWave6++;
      }
      else if (wave == 7)
      {
        CountFireWave7++;
      }
      else if (wave == 8)
      {
        CountFireWave8++;
      }
      else if (wave == 9)
      {
        CountFireWave9++;
      }
      else if (wave == 10)
      {
        CountFireWave10++;
      }
      else if (wave == 11)
      {
        CountFireWave11++;
      }
      else if (wave == 12)
      {
        CountFireWave12++;
      }
      else if (wave == 13)
      {
        CountFireWave13++;
      }
      else if (wave == 14)
      {
        CountFireWave14++;
      }
    }

    /// <summary>
    /// Controla os objetos do jogo.
    /// </summary>
    public void Go()
    {
      if (ShowingMenu)
        return;

      if (ShowingPreface)
        return;

      if (player.Alive)
      {
        #region Controle do Tiro do Jogador
        List<Shot> deadPlayerShots = new List<Shot>();

        foreach (Shot shot in PlayerShots)
        {
          if (!shot.Move())
            deadPlayerShots.Add(shot);
        }

        foreach (Shot shot in deadPlayerShots)
        {
          PlayerShots.Remove(shot);
        }
        #endregion

        #region Controle do Tiro do Invader
        List<Shot> deadInvaderShots = new List<Shot>();

        foreach (Shot shot in InvaderShots)
        {
          if (!shot.Move())
            deadInvaderShots.Add(shot);
        }

        foreach (Shot shot in deadInvaderShots)
        {
          InvaderShots.Remove(shot);
        }
        #endregion

        if (ShowingBonus)
        {
          MoveInvadersBonus();
          ReturnFireBonus();
          CheckForPlayerCollisions();
          CheckForInvaderBonusCollisions();
        }
        else
        {
          MoveInvaders();
          MoveInvadersAnswers();
          ReturnFire();
          CheckForPlayerCollisions();
          CheckForInvaderCollisions();
          CheckForInvaderAnswersCollisions();
        }

        if (invadersBonus.Count < 1 && ShowingBonus)
        {
          NextWaveBonus();
        }
      }
    }

    /// <summary>
    /// Move todos invaders que contêm a resposta da pergunta.
    /// </summary>
    private void MoveInvadersAnswers()
    {
      if (currentGameFrameAnswers > framesSkipped)
      {
        #region Movimenta os Invaders para Cima
        if (invaderAnswerDirection == Direction.Up)
        {
          var borderInvaders = from invader in invadersAnswers
                               where invader.Location.Y < (-5)
                               select invader;

          if (borderInvaders.Count() > 0)
          {
            invadersAnswers.RemoveAll(x => x.Location.Y < (-5));
          }
          else
          {
            foreach (Invader invader in invadersAnswers)
            {
              invader.Move(Direction.Up);
            }
          }
        }
        #endregion               

        foreach (Invader invader in invadersAnswers)
        {
          invader.Move(invaderAnswerDirection);
        }
      }
      currentGameFrameAnswers++;

      if (currentGameFrameAnswers > 6)
        currentGameFrameAnswers = 1;
    }

    /// <summary>
    /// Move todos os invaders.
    /// </summary>
    private void MoveInvaders()
    {
      if (currentGameFrame > framesSkipped)
      {
        #region Movimenta os Invaders para a Direita
        if (invaderDirection == Direction.Right)
        {
          var borderInvaders = from invader in invaders
                               where invader.Location.X > (boundaries.Width - 150)
                               select invader;

          if (borderInvaders.Count() > 0)
          {
            invaderDirection = Direction.Left;

            foreach (Invader invader in invaders)
            {
              invader.Move(Direction.Down);
            }
          }
          else
          {
            foreach (Invader invader in invaders)
            {
              invader.Move(Direction.Right);
            }
          }
        }
        #endregion

        #region Movimenta os Invaders para a Esquerda
        if (invaderDirection == Direction.Left)
        {
          var borderInvaders = from invader in invaders
                               where invader.Location.X < 1
                               select invader;

          if (borderInvaders.Count() > 0)
          {
            invaderDirection = Direction.Right;

            foreach (Invader invader in invaders)
            {
              invader.Move(Direction.Down);
            }
          }
          else
          {
            foreach (Invader invader in invaders)
            {
              invader.Move(Direction.Left);
            }
          }
        }
        #endregion

        #region Game Over Invaders
        //Verifica se os invaders alcançaram a área do jogador para finalizar o jogo
        var gOverInvaders = from invader in invaders
                            where invader.Location.Y > player.Location.Y - 80
                            select invader;

        if (gOverInvaders.Count() > 0)
          GameOver(this, null);

        foreach (Invader invader in invaders)
        {
          invader.Move(invaderDirection);
        }
        #endregion
      }
      currentGameFrame++;

      if (currentGameFrame > 6)
        currentGameFrame = 1;
    }

    /// <summary>
    /// Move todos os invaders.
    /// </summary>
    private void MoveInvadersBonus()
    {
      if (currentGameFrame > framesSkipped)
      {
        #region Movimenta os Invaders para a Direita
        if (invaderDirection == Direction.Right)
        {
          var borderInvaders = from invader in invadersBonus
                               where invader.Location.X > (boundaries.Width - 100)
                               select invader;

          if (borderInvaders.Count() > 0)
          {
            invaderDirection = Direction.Left;

            foreach (Invader invader in invadersBonus)
            {
              invader.Move(Direction.Down);
            }
          }
          else
          {
            foreach (Invader invader in invadersBonus)
            {
              invader.Move(Direction.Right);
            }
          }
        }
        #endregion

        #region Movimenta os Invaders para a Esquerda
        if (invaderDirection == Direction.Left)
        {
          var borderInvaders = from invader in invadersBonus
                               where invader.Location.X < 100
                               select invader;

          if (borderInvaders.Count() > 0)
          {
            invaderDirection = Direction.Right;

            foreach (Invader invader in invadersBonus)
            {
              invader.Move(Direction.Down);
            }
          }
          else
          {
            foreach (Invader invader in invadersBonus)
            {
              invader.Move(Direction.Left);
            }
          }
        }
        #endregion

        #region Game Over Invaders
        //Verifica se os invaders alcançaram a área do jogador para finalizar o jogo
        var gOverInvaders = from invader in invadersBonus
                            where invader.Location.Y > player.Location.Y
                            select invader;

        if (gOverInvaders.Count() > 0)
          GameOver(this, null);

        foreach (Invader invader in invadersBonus)
        {
          invader.Move(invaderDirection);
        }
        #endregion
      }
      currentGameFrame++;

      if (currentGameFrame > 6)
        currentGameFrame = 1;
    }

    /// <summary>
    /// Controla o tiro dos invaders contra o jogador.
    /// </summary>
    private void ReturnFire()
    {
      int rColumnInt = 0;

      //IF's: Controle para que os invaders não atirem toda hora contra o jogador
      if (InvaderShots.Count == GetNumMaxFireShotInvader())
        return;

      if (random.Next(10) < (10 - wave))
        return;

      var invadersColumns = from invader in invaders
                            group invader by invader.Location.X into columns
                            select columns;

      rColumnInt = random.Next(invadersColumns.Count());

      if (rColumnInt == 0)
        return;

      var rColumn = invadersColumns.ElementAt(rColumnInt);

      var invadersRow = from invader in rColumn
                        orderby invader.Location.Y descending
                        select invader;

      Invader shot = invadersRow.First();

      //Cria a posição do tiro do invader e adiciona o tiro na lista de tiros
      Point shotLocation = new Point(shot.Location.X + (shot.Area.Width / 2), shot.Location.Y + shot.Area.Height);
      Shot shotInvaders = new Shot(shotLocation, Direction.Down, boundaries);
      InvaderShots.Add(shotInvaders);
    }

    /// <summary>
    /// Controla o tiro dos invaders contra o jogador.
    /// </summary>
    private void ReturnFireBonus()
    {
      int rColumnInt = 0;

      //IF's: Controle para que os invaders não atirem toda hora contra o jogador
      if (InvaderShots.Count == 8)
        return;

      if (random.Next(10) < (10 - wave))
        return;

      var invadersColumns = from invader in invadersBonus
                            group invader by invader.Location.X into columns
                            select columns;

      rColumnInt = random.Next(invadersColumns.Count());

      if (rColumnInt == 0)
        return;

      var rColumn = invadersColumns.ElementAt(rColumnInt);

      var invadersRow = from invader in rColumn
                        orderby invader.Location.Y descending
                        select invader;

      Invader shot = invadersRow.First();

      //Cria a posição do tiro do invader e adiciona o tiro na lista de tiros
      Point shotLocation = new Point(shot.Location.X + (shot.Area.Width / 2), shot.Location.Y + shot.Area.Height);
      Shot shotInvaders = new Shot(shotLocation, Direction.Down, boundaries);
      InvaderShots.Add(shotInvaders);
    }

    /// <summary>
    /// Controla a quantidade de tiros dos invaders contra o jogador em cada nível.
    /// </summary>
    /// <returns>Retorna o número máximo de tiros do invader na tela</returns>
    private int GetNumMaxFireShotInvader()
    {
      int numMaxFireShot = 0;

      if (Nivel == 1)
        numMaxFireShot = wave > 4 ? 4 : wave;

      if (Nivel == 2)
        numMaxFireShot = wave > 3 ? 3 : wave;

      if (Nivel == 3)
        numMaxFireShot = wave > 2 ? 2 : wave;

      return numMaxFireShot;
    }
    /// <summary>
    /// Método para verificar se o jogador foi atingido por algum tiro dos invaders.
    /// </summary>
    private void CheckForPlayerCollisions()
    {
      List<Shot> deadInvaderShots = new List<Shot>();

      foreach (Shot shot in InvaderShots)
      {
        if (player.Area.Contains(shot.Location))
        {
          deadInvaderShots.Add(shot);
          LivesLeft--;
          player.Alive = false;

          if (LivesLeft == 0)
            GameOver(this, null);
        }
      }

      //Remove o tiro que já atingiu o jogador
      foreach (Shot shot in deadInvaderShots)
      {
        InvaderShots.Remove(shot);
      }
    }

    /// <summary>
    /// Método para verificar se o invader foi atingido por algum tiro do jogador.
    /// </summary>
    private void CheckForInvaderAnswersCollisions()
    {
      List<Shot> deadPlayerShots = new List<Shot>();

      foreach (Shot shot in PlayerShots)
      {
        foreach (Invader invader in invadersAnswers)
        {
          if (invader.Area.Contains(shot.Location.X - 40, shot.Location.Y))
          {
            deadInvaders.Add(invader);
            deadPlayerShots.Add(shot);

            if (dicResposta.ContainsValue(invader.Code))
            {
              if (PontosMaxGanhos == 5)
                ContPassouDePrimeira++;

              Score += PontosMaxGanhos;

              CountScoreGame();
            }
            else
            {
              PontosMaxGanhos--;

              if (PontosMaxGanhos == 0)
                PontosMaxGanhos = 1;
            }
          }
        }

        //Remove o invader atingido pelo tiro do jogador
        foreach (Invader invader in deadInvaders)
        {
          invadersAnswers.Remove(invader);
        }
      }

      //Remove os tiros que já atingiram invaders mortos
      foreach (Shot shot in deadPlayerShots)
      {
        PlayerShots.Remove(shot);
      }
    }

    /// <summary>
    /// Pontos do Jogador ao acertar a resposta correta.
    /// </summary>
    private void CountScoreGame()
    {
      if (wave == 1)
      {
        CountScoreWave1 = PontosMaxGanhos;
      }
      else if (wave == 2)
      {
        CountScoreWave2 = PontosMaxGanhos;
      }
      else if (wave == 3)
      {
        CountScoreWave3 = PontosMaxGanhos;
      }
      else if (wave == 4)
      {
        CountScoreWave4 = PontosMaxGanhos;
      }
      else if (wave == 5)
      {
        CountScoreWave5 = PontosMaxGanhos;
      }
      else if (wave == 6)
      {
        CountScoreWave6 = PontosMaxGanhos;
      }
      else if (wave == 7)
      {
        CountScoreWave7 = PontosMaxGanhos;
      }
      else if (wave == 8)
      {
        CountScoreWave8 = PontosMaxGanhos;
      }
      else if (wave == 9)
      {
        CountScoreWave9 = PontosMaxGanhos;
      }
      else if (wave == 10)
      {
        CountScoreWave10 = PontosMaxGanhos;
      }
      else if (wave == 11)
      {
        CountScoreWave11 = PontosMaxGanhos;
      }
      else if (wave == 12)
      {
        CountScoreWave12 = PontosMaxGanhos;
      }
      else if (wave == 13)
      {
        CountScoreWave13 = PontosMaxGanhos;
      }
      else if (wave == 14)
      {
        CountScoreWave14 = PontosMaxGanhos;
      }
    }

    /// <summary>
    /// Método para ver se algum dos invaders foi atingido por tiros do jogador.
    /// </summary>
    private void CheckForInvaderCollisions()
    {
      List<Shot> deadPlayerShots = new List<Shot>();

      foreach (Shot shot in PlayerShots)
      {
        foreach (Invader invader in invaders)
        {
          if (invader.Area.Contains(shot.Location.X - 40, shot.Location.Y))
          {
            deadInvaders.Add(invader);
            deadPlayerShots.Add(shot);
            Score += invader.Score;
          }
        }

        //Remove o invader atingido pelo tiro do jogador
        foreach (Invader invader in deadInvaders)
        {
          invaders.Remove(invader);
        }
      }

      //Remove os tiros que já atingiram invaders mortos
      foreach (Shot shot in deadPlayerShots)
      {
        PlayerShots.Remove(shot);
      }
    }

    /// <summary>
    /// Método para ver se algum dos invaders foi atingido por tiros do jogador.
    /// </summary>
    private void CheckForInvaderBonusCollisions()
    {
      List<Shot> deadPlayerShots = new List<Shot>();

      foreach (Shot shot in PlayerShots)
      {
        foreach (Invader invader in invadersBonus)
        {
          if (invader.Area.Contains(shot.Location.X - 23, shot.Location.Y))
          {
            deadInvaders.Add(invader);
            deadPlayerShots.Add(shot);
            Score += invader.Score;
          }
        }

        //Remove o invader atingido pelo tiro do jogador
        foreach (Invader invader in deadInvaders)
        {
          invadersBonus.Remove(invader);
        }
      }

      //Remove os tiros que já atingiram invaders mortos
      foreach (Shot shot in deadPlayerShots)
      {
        PlayerShots.Remove(shot);
      }
    }

    /// <summary>
    /// Método para criar a próxima onda de invaders (Próxima fase).
    /// </summary>
    private void NextWave()
    {
      /*if (listCodigoFonte == null || listCodigoFonte.Count == 0)
        LerArquivoCodigoFonte();*/

      wave++;

      invaderDirection = Direction.Right;
      invaderAnswerDirection = Direction.Up;

      if (wave <= 15) //A constante é de acordo com o número de fases 
        framesSkipped = ControlaInvaders - 1;
      else
        framesSkipped = 0;

      //Aumenta a dificuldade de movimento dos invaders conforme avança de nível
      if (wave == 6)
      {
        ControlaInvaders = 5;
        Nivel = 2;
      }
      else if (wave == 11)
      {
        ControlaInvaders = 4;
        Nivel = 3;
      }

      InvadersGame();
    }

    private void NextWaveBonus()
    {
      invaderDirection = Direction.Right;

      if (wave <= 15) //A constante é de acordo com o número de fases 
        framesSkipped = ControlaInvaders - 1;
      else
        framesSkipped = 0;

      InvadersBonus();
    }

    /// <summary>
    /// Cria os invaders das Fases do Jogo
    /// </summary>
    private void InvadersGame()
    {
      int currentInvaderYPosition = 0;

      for (int i = 0; i < 4; i++)
      {
        ShipType currentShipType = (ShipType)i;
        currentInvaderYPosition += Consts.InvaderYPosition;

        int currentInvaderXPosition = 0;

        for (int j = 0; j < 5; j++)
        {
          currentInvaderXPosition += Consts.InvaderXPosition;

          Point newInvaderPoint = new Point(currentInvaderXPosition, currentInvaderYPosition);

          Invader newInvader = new Invader(currentShipType, newInvaderPoint, 1);

          invaders.Add(newInvader);
        }
      }
    }

    /// <summary>
    /// Cria os invaders que contém as respostas do Jogo
    /// </summary>
    private void InvadersAnswers(Graphics graphics, int animationCell)
    {
      int shipType = random.Next(0, 4);
      int currentInvaderYPosition = random.Next(60, 500);
      int currentInvaderXPosition = 0;

      ShipType currentShipType = (ShipType)shipType;
      currentInvaderXPosition = random.Next(60, 820);

      Point newInvaderPoint = new Point(currentInvaderXPosition, currentInvaderYPosition);

      string resposta = GeraResposta();

      Invader newInvader = new Invader(currentShipType, newInvaderPoint, PontosMaxGanhos, resposta, wave);

      invadersAnswers.Add(newInvader);
    }

    /// <summary>
    /// Monta a resposta das perguntas.
    /// </summary>
    /// <returns>Retorna as opções de respostas aleatoriamente</returns>
    private string GeraResposta()
    {
      string resposta;
      int opcoes;

      if (wave == 8 || wave == 13)
        opcoes = random.Next(1, 4);
      else
        opcoes = random.Next(1, 6);

      switch (opcoes)
      {
        case 1:
          resposta = wave == 7 ? "para" : "A";
          return resposta;
        case 2:
          resposta = wave == 7 ? "for" : "B";
          return resposta;
        case 3:
          resposta = wave == 7 ? "if" : "C";
          return resposta;
        case 4:
          resposta = wave == 7 ? "while" : "D";
          return resposta;
        case 5:
          resposta = wave == 7 ? "repeat" : "E";
          return resposta;
        default:
          resposta = "A";
          return resposta;
      }
    }

    /// <summary>
    /// Cria os invaders presentes no Menu Principal
    /// </summary>
    private void InvadersMenu()
    {
      int currentInvaderYPosition = 0;
      int shipType = 0;
      currentInvaderYPosition = Consts.InvaderYPosition;

      int currentInvaderXPosition = 0;

      for (int j = 0; j < 15; j++)
      {
        ShipType currentShipType = (ShipType)shipType;
        currentInvaderXPosition += Consts.InvaderXPosition;

        Point newInvaderPoint = new Point(currentInvaderXPosition, currentInvaderYPosition);

        Invader newInvader = new Invader(currentShipType, newInvaderPoint);
        invadersMenu.Add(newInvader);

        if (j == 2 || j == 5 || j == 8 || j == 11)
          shipType++;
      }
    }

    /// <summary>
    /// Cria os invaders da Fase Bonus
    /// </summary>
    private void InvadersBonus()
    {
      int currentInvaderYPosition = 0;

      for (int i = 0; i < 5; i++)
      {
        ShipType currentShipType = (ShipType)i;
        currentInvaderYPosition += Consts.InvaderYPosition;

        int currentInvaderXPosition = 0;

        for (int j = 0; j < 5; j++)
        {
          currentInvaderXPosition += Consts.InvaderXPosition;

          Point newInvaderPoint = new Point(currentInvaderXPosition, currentInvaderYPosition);

          Invader newInvader = new Invader(currentShipType, newInvaderPoint, 1);
          invadersBonus.Add(newInvader);
        }
      }
    }

    /// <summary>
    /// Mensagem do Desafio Bônus.
    /// </summary>
    /// <returns></returns>
    private string DefineMensagemBonus()
    {
      return $@"Parabéns! Você passou pelo Desafio BÔNUS :D

Até o momento você possui {Score} pontos e {LivesLeft} vidas.

Continue assim, vamos lá!";
    }

    /// <summary>
    /// Mensagem das Fases do Jogo.
    /// </summary>
    /// <returns></returns>
    private string DefineMensagem()
    {
      switch (wave - 1)
      {
        case 1:
          return $@"Parabéns! Você possui {Score} pontos

A atribuição de valor a uma 
variável é utilizando o símbolo

        <- 

entre a váriável e o valor.";

        case 2:
          return $@"Parabéns! Você possui {Score} pontos

Em R as variáveis não são fortemente tipadas.
Desta forma, uma variável pode receber 
diferentes tipos de conteúdo.

    Logo, o valor de X é 'b'";
        case 3:
          return $@"Parabéns! Você possui {Score} pontos

Para criar um vetor com tamanho definido,
basta instânciar a propriedade 'length'
do vetor - desta maneira:

    x <- c(length=5)";
        case 4:
          return $@"Parabéns! Você possui {Score} pontos

Assim como no vetor, para criar uma matriz
com quantidade de linhas e colunas definidas,
é necessário instanciar o número de linhas e 
colunas previamente da seguinte forma:

    x <- matrix(nrow=2, ncol=2)";
        case 5:
          return $@"Parabéns! Você possui {Score} pontos

Para criar uma sequência em R, pode-se
utilizar o comando seq() ou 1:10. 
Portanto,  a única maneira incorreta é:

    x <- c(1-10)";
        case 6:
          return @"Parabéns! Você possui " + Score + @" pontos

O comando de condição em R é criado utilizando:
    if (condição){
        código
    }";
        case 7:
          return $@"Parabéns! Você possui {Score} pontos

Um dos comandos de repetição possível do R é o
    FOR";
        case 8:
          return $@"Parabéns! Você possui {Score} pontos

O FOR pode ser criado utilizando a variável i
que é declarada dentro do comando 

    (i in 0:9)

Neste caso, i varia de 0 até 9";
        case 9:
          return $@"Parabéns! Você possui {Score} pontos

Em R, o comando para imprimir valores
na tela é o :

    print(i)";
        case 10:
          return $@"Parabéns! Você possui {Score} pontos

Para interromper a execução de alguma função
em R, utiliza-se o comando:

    break";
        case 11:
          return $@"Parabéns! Você possui {Score} pontos

Para consultar a ajuda de algum pacote em R,
utiliza-se a seguinte sintaxe:

    ?NomeFuncao";
        case 12:
          return $@"Parabéns! Você possui {Score} pontos

Para utilizar recursos de algum pacote em R
é necessário importar esse pacote,
após instalado, da seguinte forma:

    library NomePacote";
        case 13:
          return $@"Parabéns! Você possui {Score} pontos

A maneira correta de criar a funcão em R é:

calculaMedia <- function(a)";
        case 14:
          return $@"Parabéns! Você possui {Score} pontos

O R possui funções primitivas, entre elas
uma que calcula a média de vetor:

mean(a)";
        default:
          return $@"Fase BONUS";
      }
    }

    /// <summary>
    /// Método que representa as fases do jogo.
    /// </summary>
    /// <remarks>
    /// Wave (onda dos invaders) representa as fases do jogo:
    /// Wave = 1 -> Fase 1
    /// Wave = 2 -> Fase 2
    /// </remarks>
    /// <returns>Retorna a fase de acordo com a onda atual do jogo</returns>
    private string FasesGamefication()
    {
      string fase = string.Empty;

      switch (wave)
      {
        #region 1º Nível - Perguntas
        case 1:
          fase = "Qual é o símbolo que atribui valor" + Environment.NewLine + "a uma variável?";
          return fase;
        case 2:
          fase = "x <- 1; x <- 'b';. Qual é o valor de x?";
          return fase;
        case 3:
          fase = "Qual é a maneira correta de declarar" + Environment.NewLine + "um vetor com 5 posições em x? x <- ?";
          return fase;
        case 4:
          fase = "Qual é a maneira correta de declarar" + Environment.NewLine + "uma matriz 2x2 em x? x <- matrix(?,?)";
          return fase;
        case 5:
          fase = "Qual é a maneira incorreta de se declarar" + Environment.NewLine + "um vetor valorado de 1 até 10 em x?" + Environment.NewLine + "x <- ?";
          return fase;
        #endregion

        #region 2º Nível - Perguntas
        case 6:
          fase = "Qual é a declaração correta do comando" + Environment.NewLine + "de condição IF ?";
          return fase;
        case 7:
          fase = "Qual é a declaração correta do comando" + Environment.NewLine + "de repetição FOR ?";
          return fase;
        case 8:
          fase = "Qual é a declaração correta que comple-" + Environment.NewLine + "menta o comando de repetição FOR ?";
          return fase;
        case 9:
          fase = "Qual é a função que imprime o elemen-" + Environment.NewLine + "to i do comando de repetição FOR ?";
          return fase;
        case 10:
          fase = "Como interromper a execução de um" + Environment.NewLine + "loop ?";
          return fase;
        #endregion

        #region 3º Nível - Perguntas
        case 11:
          fase = "Qual é o comando para verificar o help" + Environment.NewLine + "de uma função ?";
          return fase;
        case 12:
          fase = "Qual é a sintaxe correta para importar" + Environment.NewLine + "um pacote/biblioteca ?";
          return fase;
        case 13:
          fase = "Foi criada uma variável calculaMedia." + Environment.NewLine + "Crie uma função que calcule a média de" + Environment.NewLine + "um parâmetro a.";
          return fase;
        case 14:
          fase = "Complemente a criação da função que" + Environment.NewLine + "calcula a média de um parâmetro a.";
          return fase;
          #endregion
      }
      return fase;
    }

    /// <summary>
    /// Método responsável por apresentar as opções de respostas dos problemas propostos
    /// </summary>
    /// <returns>Retorna as opções objetivas de acordo com a fase</returns>
    private string OpcoesGamefication()
    {
      string opcResposta = string.Empty;

      switch (wave)
      {
        #region 1º Nível - Opções de Resposta
        case 1:
          opcResposta = "A) = B) == C) := D) <- E) -> ";
          return opcResposta;
        case 2:
          opcResposta = "A) 1 B) b C) nulo D) vazio E) erro ";
          return opcResposta;
        case 3:
          opcResposta = "A) c(length = 5) B) c(5) C) c[5] D) c[length = 5] E) [5] ";
          return opcResposta;
        case 4:
          opcResposta = "A) nrow=2,ncol=2 B) lin=2, col=2 C) 2,2 D) [2,2] E) nrow<-2,ncol<-2 ";
          return opcResposta;
        case 5:
          opcResposta = "A) seq(1,10) B) seq(1:10) C) seq(1,10,length=10) D) c(1:10) E) c(1-10) ";
          return opcResposta;
        #endregion

        #region 2º Nível - Opções de Resposta
        case 6:
          opcResposta = "A) else B) se(execução) C) if(condição) D) se(condição) E) if(execução)";
          return opcResposta;
        case 7:
          return opcResposta;
        case 8:
          opcResposta = "A) for item in range(10): B) (i in 0:9) C) for (int i = 0; i < 5; i++)";
          return opcResposta;
        case 9:
          opcResposta = "A) System.out.print(i) B) Console.Write(i) C) echo $i D) N/A E) print(i)";
          return opcResposta;
        case 10:
          opcResposta = "A) break B) continue C) stop D) goto E) end";
          return opcResposta;
        #endregion

        #region 3º Nível - Opções de Resposta
        case 11:
          opcResposta = "A) ??funcao B) !funcao C) !!funcao D) ?funcao E) ??(funcao)";
          return opcResposta;
        case 12:
          opcResposta = "A) library B) using C) import D) required E) lib";
          return opcResposta;
        case 13:
          opcResposta = "A) def calculaMedia(a) B) double calculaMedia(a) C) function(a)";
          return opcResposta;
        case 14:
          opcResposta = "A) sum(a) B) median(a) C) abs(a) D) ceiling(a) E) mean(a)";
          return opcResposta;
          #endregion
      }
      return opcResposta;
    }

    /// <summary>
    /// Monta um dicionário com as respostas do jogo
    /// </summary>
    /// <param name="wave">Representa a fase do jogo</param>
    private void DicRespostaCode(int wave)
    {
      switch (wave)
      {
        #region 1º Nível - Respostas
        case 1:
          dicResposta = new Dictionary<int, string>()
          {
            {0, "D" },
          };
          break;
        case 2:
          dicResposta = new Dictionary<int, string>()
          {
            {0, "B" },
          };
          break;
        case 3:
          dicResposta = new Dictionary<int, string>()
          {
            {0, "A" },
          };
          break;
        case 4:
          dicResposta = new Dictionary<int, string>()
          {
            {0, "A" },
          };
          break;
        case 5:
          dicResposta = new Dictionary<int, string>()
          {
            {0, "E" },
          };
          break;
        #endregion

        #region 2º Nível - Respostas
        case 6:
          dicResposta = new Dictionary<int, string>()
          {
            {0, "C" },
          };
          break;
        case 7:
          dicResposta = new Dictionary<int, string>()
          {
            {0, "for" },
          };
          break;
        case 8:
          dicResposta = new Dictionary<int, string>()
          {
            {0, "B" },
          };
          break;
        case 9:
          dicResposta = new Dictionary<int, string>()
          {
            {0, "E" },
          };
          break;
        case 10:
          dicResposta = new Dictionary<int, string>()
          {
            {0, "A" },
          };
          break;
        #endregion

        #region 3º Nível - Resposta          
        case 11:
          dicResposta = new Dictionary<int, string>()
          {
            {0, "D" },
          };
          break;
        case 12:
          dicResposta = new Dictionary<int, string>()
          {
            {0, "A" },
          };
          break;
        case 13:
          dicResposta = new Dictionary<int, string>()
          {
            {0, "C" },
          };
          break;
        case 14:
          dicResposta = new Dictionary<int, string>()
          {
            {0, "E" },
          };
          break;
          #endregion
      }
    }

    #region Arquivo de Texto    
    /// <summary>
    /// Caminho do Arquivo de Texto.
    /// </summary>
    /// <returns>Retorna o Caminho do Arquivo de Texto</returns>
    public string CaminhoArquivo()
    {
      return Path.Combine(Caminho, ToString() + ".txt");
    }

    /// <summary>
    /// Salva os dados do jogo do usuário.
    /// </summary>
    /// <returns>Retorna true, se salvou com sucesso</returns>
    public bool SalvarResultado()
    {
      return arquivo.SalvarDados(User, "Tempos: ", DuracaoFase1, DuracaoFase2, DuracaoFase3, DuracaoFase4, DuracaoFase5, DuracaoFase6, DuracaoFase7, DuracaoFase8, DuracaoFase9,
                                 DuracaoFase10, DuracaoFase11, DuracaoFase12, DuracaoFase13, DuracaoFase14, DuracaoFaseBonus, DuracaoTotal,
                                 "Tiros: ", CountFireWave1, CountFireWave2, CountFireWave3, CountFireWave4, CountFireWave5, CountFireWave6, CountFireWave7, CountFireWave8,
                                 CountFireWave9, CountFireWave10, CountFireWave11, CountFireWave12, CountFireWave13, CountFireWave14,
                                 "Pontuação: ", CountScoreWave1, CountScoreWave2, CountScoreWave3, CountScoreWave4, CountScoreWave5, CountScoreWave6, CountScoreWave7,
                                 CountScoreWave8, CountScoreWave9, CountScoreWave10, CountScoreWave11, CountScoreWave12, CountScoreWave13, CountScoreWave14,
                                 "Pts. Total e Vida: ", Score, LivesLeft);
    }
    #endregion

    #endregion
  }
}