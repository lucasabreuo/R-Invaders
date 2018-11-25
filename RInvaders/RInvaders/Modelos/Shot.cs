using System.Drawing;
using static RInvaders.Modelos.Enum;

namespace RInvaders.Modelos
{
  public class Shot
  {
    #region Propriedades
    public Point Location { get; private set; }
    #endregion

    #region Campos
    private Direction direction;
    private Rectangle boundaries;
    #endregion

    #region Construtor
    public Shot(Point location, Direction direction, Rectangle boundaries)
    {
      Location = location;
      this.direction = direction;
      this.boundaries = boundaries;
    }
    #endregion

    #region Métodos do Shot
    /// <summary>
    /// Move o tiro verificando se o mesmo não ultrapassou a área do jogo.
    /// </summary>
    /// <returns>Retorna true, se ainda está na área do jogo</returns>
    public bool Move()
    {
      Point newLocation = Point.Empty;

      //Move o tiro, de acordo com a direção
      if (direction == Direction.Down)
        newLocation = new Point(Location.X, (Location.Y + Consts.MoveInterval));
      else if (direction == Direction.Up)
        newLocation = new Point(Location.X, (Location.Y - Consts.MoveInterval));

      if (newLocation.Y < boundaries.Height && newLocation.Y > 0)
      {
        Location = newLocation;
        return true;
      }
      else
        return false;
    }

    /// <summary>
    /// Desenha os tiros na tela.
    /// </summary>
    /// <param name="graphics">Objeto gráfico</param>
    public void Draw(Graphics graphics)
    {
      graphics.FillRectangle(Brushes.Red, Location.X, Location.Y, Consts.Width, Consts.Height);
    }
    #endregion
  }
}
