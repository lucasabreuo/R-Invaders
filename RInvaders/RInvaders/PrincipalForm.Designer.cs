﻿namespace RInvaders
{
  partial class PrincipalForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrincipalForm));
      this.animationTimer = new System.Windows.Forms.Timer(this.components);
      this.gameTimer = new System.Windows.Forms.Timer(this.components);
      this.SuspendLayout();
      // 
      // animationTimer
      // 
      this.animationTimer.Enabled = true;
      this.animationTimer.Tick += new System.EventHandler(this.animationTimer_Tick);
      // 
      // gameTimer
      // 
      this.gameTimer.Interval = 50;
      this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
      // 
      // PrincipalForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(984, 761);
      this.DoubleBuffered = true;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "PrincipalForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "R-Invaders";
      this.Paint += new System.Windows.Forms.PaintEventHandler(this.PrincipalForm_Paint);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PrincipalForm_KeyDown);
      this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.PrincipalForm_KeyUp);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Timer animationTimer;
    private System.Windows.Forms.Timer gameTimer;
  }
}

