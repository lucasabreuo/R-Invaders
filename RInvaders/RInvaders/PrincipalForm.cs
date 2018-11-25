using RInvaders.Modelos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static RInvaders.Modelos.Enum;

namespace RInvaders
{
  public partial class PrincipalForm : Form
  {
    #region Listas
    List<Keys> teclasPressionadas = new List<Keys>();
    #endregion

    #region Variáveis 
    public int frame;
    private Game game;
    private bool gameOver;
    private bool pause;
    private bool isScoreMaxFaseBonus = false;
    public string Email = string.Empty;
    private Graphics graphics;
    Random random = new Random();
    #endregion

    #region Propriedades
    public Rectangle Boundaries
    {
      get
      {
        return ClientRectangle;
      }
    }
    #endregion

    public PrincipalForm()
    {
      InitializeComponent();
      frame = 0;
      game = new Game(random, Boundaries);
      gameOver = false;
      pause = false;
      game.Pause += new EventHandler(Pause_Game);
      game.GameOver += new EventHandler(Game_Over);
      animationTimer.Start();
      gameTimer.Start();
    }

    private void PrincipalForm_Paint(object sender, PaintEventArgs e)
    {
      graphics = e.Graphics;
      game.Draw(graphics, frame, gameOver, pause, Email);      
    }

    private void PrincipalForm_KeyDown(object sender, KeyEventArgs e)
    {
      if (game.invadersBonus.Count == 10 && !isScoreMaxFaseBonus)
      {
        game.DuracaoFaseBonus = DateTime.Now - game.InicioFaseBonus;
        isScoreMaxFaseBonus = true;
        game.ShowingBonus = false;
        game.ShowingMessageBonus = true;
        game.LivesLeft += 1;
        game.InvaderShots.Clear();
        game.PlayerShots.Clear();
      }

      if (e.KeyCode == Keys.G)
        game.ShowingMenu = false;

      if (e.KeyCode == Keys.N)
      {        
        game.ShowingPreface = false;

        if (game.ShowingBonusText)
        {
          game.ShowingBonusText = false;
          game.ShowingBonus = true;
        }
      }

      if (e.KeyCode == Keys.Enter)
      {
        if (game.ContPassouDePrimeira == 5 && !isScoreMaxFaseBonus)
          game.ShowingBonusText = true;

        game.ShowingMessage = false;
        game.ShowingMessageBonus = false;
        game.ShowingQuestions = false;

        if (!pause)
          gameTimer.Start();
      }

      if (e.KeyCode == Keys.Q)
        Application.Exit();

      if (e.KeyCode == Keys.S)
      {
        //código para redefinir o jogo e reiniciar os timers
        pause = false;
        gameOver = false;
        game = new Game(random, Boundaries);
        game.Pause += new EventHandler(Pause_Game);
        game.GameOver += new EventHandler(Game_Over);
        gameTimer.Start();

        return;
      }

      if (e.KeyCode == Keys.Space)
        game.FireShot();

      if (e.KeyCode == Keys.P)
      {
        if (!pause)
          Pause_Game(this, null);
        else
        {
          pause = false;
          gameTimer.Start();
        }
      }

      if (teclasPressionadas.Contains(e.KeyCode))
        teclasPressionadas.Remove(e.KeyCode);

      teclasPressionadas.Add(e.KeyCode);
    }

    private void PrincipalForm_KeyUp(object sender, KeyEventArgs e)
    {
      if (teclasPressionadas.Contains(e.KeyCode))
        teclasPressionadas.Remove(e.KeyCode);
    }    

    private void animationTimer_Tick(object sender, EventArgs e)
    {
      if (frame < 3)
        frame++;
      else
        frame = 0;

      game.Twinkle();
      Refresh();
    }

    private void gameTimer_Tick(object sender, EventArgs e)
    {
      game.Go();
      foreach (Keys key in teclasPressionadas)
      {
        if (key == Keys.Left)
        {
          game.MovePlayer(Direction.Left, gameOver, pause);
          return;
        }
        else if (key == Keys.Right)
        {
          game.MovePlayer(Direction.Right, gameOver, pause);
          return;
        }
      }
    }

    private void Game_Over(object sender, EventArgs e)
    {
      gameTimer.Stop();
      gameOver = true;
      Invalidate();
    }

    private void Pause_Game(object sender, EventArgs e)
    {
      gameTimer.Stop();

      if (!game.ShowingMessage && !game.ShowingQuestions && !game.ShowingMessageBonus)
        pause = true;
    }
  }
}
