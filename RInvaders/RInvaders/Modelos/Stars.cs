using System;
using System.Collections.Generic;
using System.Drawing;

namespace RInvaders.Modelos
{
  public class Stars
  {
    #region Campos
    private List<Star> stars;
    private Rectangle boundaries;
    #endregion

    #region Construtor
    public Stars(Random random, Rectangle boundaries)
    {
      this.boundaries = boundaries;

      stars = new List<Star>();

      for (int i = 1; i < 300; i++)
      {
        addStar(random);
      }
    }
    #endregion

    #region Métodos de Stars
    /// <summary>
    /// Desenha as estrelas da área do jogo.
    /// </summary>
    /// <param name="graphics">Objeto gráfico</param>
    /// <returns></returns>
    public Graphics Draw(Graphics graphics)
    {
      Graphics starG = graphics;

      foreach (Star star in stars)
      {
        starG.FillRectangle(star.brush, star.point.X, star.point.Y, 1, 1);
      }

      return starG;
    }

    /// <summary>
    /// Método responsável por fazerem as estrelas brilharem.
    /// </summary>
    /// <param name="random"></param>
    public void Twinkle(Random random)
    {
      stars.RemoveRange(0, 4);

      for (int i = 0; i < 4; i++)
      {
        addStar(random);
      }
    }

    /// <summary>
    /// Adiciona as estrelas na área do jogo.
    /// </summary>
    /// <param name="random">Random</param>
    private void addStar(Random random)
    {
      int height = boundaries.Height;
      int widht = boundaries.Width;
      Point location = new Point(random.Next(0, widht), random.Next(0, height));
      Star newStar = new Star(location, Brushes.Yellow);
      stars.Add(newStar);
    }
    #endregion

  }
}
