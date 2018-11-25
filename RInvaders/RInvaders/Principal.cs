using System;
using System.Windows.Forms;

namespace RInvaders
{
  static class Principal
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new PrincipalForm());
    }
  }
}
