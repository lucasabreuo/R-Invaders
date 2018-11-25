using System;
using System.Drawing;
using static RInvaders.Modelos.Enum;

namespace RInvaders.Modelos
{
  public class Player
  {
    #region Propriedades
    public Point Location { get; private set; }

    public Rectangle Area
    {
      get
      {
        return new Rectangle(Location, image.Size);
      }
    }

    public bool Alive
    {
      get
      {
        return _alive;
      }
      set
      {
        _alive = value;

        if (!value)
          deathWait = DateTime.Now;
      }
    }
    private bool _alive;
    #endregion

    #region Campos    
    /// <summary>
    /// Imagem da nave do jogador.
    /// </summary>
    public Bitmap image = Properties.Resources.player;

    /// <summary>
    /// Tempo para a "morte" do jogador.
    /// </summary>
    private DateTime deathWait;

    /// <summary>
    /// Área do jogo.
    /// </summary>
    private Rectangle boundaries;

    /// <summary>
    /// Campo para animar a nave sendo lentamente esmagada ao ser atingida por um tiro.
    /// </summary>
    private float deadPlayerShipHeight;
    #endregion

    #region Construtor
    public Player(Rectangle boundaries, Point location)
    {
      this.boundaries = boundaries;
      this.Location = location;
      Alive = true;
      deadPlayerShipHeight = 1.0F;
    }
    #endregion

    #region Métodos do Player
    /// <summary>
    /// Move o jogador na direção recebida como parâmetro.
    /// </summary>
    /// <param name="direction">Direção, esquerda ou direita</param>
    public void Move(Direction direction)
    {
      if (Alive)
      {
        if (direction == Direction.Left)
        {
          Point newLocation = new Point((Location.X - Consts.HorizontalInterval), Location.Y);

          if ((newLocation.X < (boundaries.Width - 100)) && (newLocation.X > 50))
            Location = newLocation;
        }
        else if (direction == Direction.Right)
        {
          Point newLocation = new Point((Location.X + Consts.HorizontalInterval), Location.Y);

          if ((newLocation.X < (boundaries.Width - 100)) && (newLocation.X > 50))
            Location = newLocation;
        }
      }
    }

    /// <summary>
    /// Desenha a nave do jogador.
    /// </summary>
    /// <param name="graphics">Objeto gráfico</param>
    public void Draw(Graphics graphics)
    {
      if (!Alive)
      {
        if ((DateTime.Now - deathWait) > TimeSpan.FromSeconds(1.5))
        {
          deadPlayerShipHeight = 0.0F;
          Alive = true;
        }
        else if ((DateTime.Now - deathWait) > TimeSpan.FromSeconds(1))
        {
          deadPlayerShipHeight = 0.25F;
        }
        else if ((DateTime.Now - deathWait) > TimeSpan.FromSeconds(0.5))
        {
          deadPlayerShipHeight = 0.75F;
        }
        else if ((DateTime.Now - deathWait) > TimeSpan.FromSeconds(0))
        {
          deadPlayerShipHeight = 0.9F;
        }

        graphics.DrawImage(image, Location.X, Location.Y, image.Width, image.Height * deadPlayerShipHeight);
      }
      else
      {
        graphics.DrawImage(image, Location.X, Location.Y - 20);
      }
    }
    #endregion
  }
}
