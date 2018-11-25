using System.Drawing;

namespace RInvaders.Modelos
{
  public struct Star
  {
    public Point point;
    public Brush brush;

    public Star(Point point, Brush brush)
    {
      this.point = point;
      this.brush = brush;
    }
  }
}
