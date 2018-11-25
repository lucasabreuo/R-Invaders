using System;
using System.Drawing;
using System.Windows.Forms;
using static RInvaders.Modelos.Enum;

namespace RInvaders.Modelos
{
  public class Invader
  {
    #region Propriedades
    public Point Location { get; private set; }
    public ShipType InvaderType { get; private set; }
    public Rectangle Area
    {
      get { return new Rectangle(Location, imageArrayInvader[0].Size); }
    }
    public int Score { get; private set; }
    public string Code { get; private set; }
    #endregion

    #region Campos    
    private Bitmap imageInvader;
    private Bitmap[] imageArrayInvader;
    private Font labelsFont;
    private int tamFontCode = 0;
    #endregion

    #region Construtor
    public Invader(ShipType invaderType, Point location, int score, string code, int wave)
    {
      InvaderType = invaderType;
      Location = location;
      Score = score;
      Code = code;
      InvaderImage();
      imageInvader = imageArrayInvader[0];
      tamFontCode = wave == 7 ? 20 : 20;
      labelsFont = new Font(FontFamily.GenericMonospace, tamFontCode, FontStyle.Bold);
    }

    public Invader(ShipType invaderType, Point location)
    {
      InvaderType = invaderType;
      Location = location;
      InvaderImage();
      imageInvader = imageArrayInvader[0];
    }

    public Invader(ShipType invaderType, Point location, int score)
    {
      InvaderType = invaderType;
      Location = location;
      Score = score;
      InvaderImage();
      imageInvader = imageArrayInvader[0];
    }
    #endregion

    #region Métodos do Invader
    /// <summary>
    /// Desenha os invaders na área do jogo.
    /// </summary>
    /// <param name="graphics">Objeto gráfico</param>
    /// <param name="animationCell">Célula de animação</param>
    /// <returns></returns>
    public void Draw(Graphics graphics, int animationCell, bool isMenu)
    {
      imageInvader = imageArrayInvader[animationCell];

      try
      {
        if (!isMenu)
        {
          graphics.DrawImage(imageInvader, Location.X + 50, Location.Y + 25);
          graphics.DrawString(Code, labelsFont, Brushes.Red, Location.X + 61, Location.Y + 50);
        }
        else
          graphics.DrawImage(imageInvader, Location);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }
    }

    /// <summary>
    /// Move a nave na direção passada pelo objeto Game.
    /// </summary>
    /// <param name="direction">Direção</param>
    public void Move(Direction direction)
    {
      switch (direction)
      {
        case Direction.Right:
          Location = new Point(Location.X + Consts.HorizontalInterval, Location.Y);
          break;
        case Direction.Left:
          Location = new Point(Location.X - Consts.HorizontalInterval, Location.Y);
          break;
        case Direction.Down:
          Location = new Point(Location.X, Location.Y + Consts.VerticalInterval);
          break;
        case Direction.Up:
          Location = new Point(Location.X, Location.Y + (-5));
          break;
      }
    }

    /// <summary>
    /// Obtém a imagem do invader.
    /// </summary>
    private void InvaderImage()
    {
      imageArrayInvader = new Bitmap[4];

      switch (InvaderType)
      {
        case ShipType.Bug:
          imageArrayInvader[0] = Properties.Resources.bug1;
          imageArrayInvader[1] = Properties.Resources.bug2;
          imageArrayInvader[2] = Properties.Resources.bug3;
          imageArrayInvader[3] = Properties.Resources.bug4;
          break;
        case ShipType.Satellite:
          imageArrayInvader[0] = Properties.Resources.satellite1;
          imageArrayInvader[1] = Properties.Resources.satellite2;
          imageArrayInvader[2] = Properties.Resources.satellite3;
          imageArrayInvader[3] = Properties.Resources.satellite4;
          break;
        case ShipType.Saucer:
          imageArrayInvader[0] = Properties.Resources.flyingsaucer1;
          imageArrayInvader[1] = Properties.Resources.flyingsaucer2;
          imageArrayInvader[2] = Properties.Resources.flyingsaucer3;
          imageArrayInvader[3] = Properties.Resources.flyingsaucer4;
          break;
        case ShipType.Spaceship:
          imageArrayInvader[0] = Properties.Resources.spaceship1;
          imageArrayInvader[1] = Properties.Resources.spaceship2;
          imageArrayInvader[2] = Properties.Resources.spaceship3;
          imageArrayInvader[3] = Properties.Resources.spaceship4;
          break;
        case ShipType.Star:
          imageArrayInvader[0] = Properties.Resources.star1;
          imageArrayInvader[1] = Properties.Resources.star2;
          imageArrayInvader[2] = Properties.Resources.star3;
          imageArrayInvader[3] = Properties.Resources.star4;
          break;
      }
    }
    #endregion
  }
}
