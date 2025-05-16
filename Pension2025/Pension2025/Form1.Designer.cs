
namespace Pension2025
{
  partial class Form1
  {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.btnParseList01 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnParseList01
            // 
            this.btnParseList01.Location = new System.Drawing.Point(30, 30);
            this.btnParseList01.Name = "btnParseList01";
            this.btnParseList01.Size = new System.Drawing.Size(172, 31);
            this.btnParseList01.TabIndex = 0;
            this.btnParseList01.Text = "Parse list files into List01.txt";
            this.btnParseList01.UseVisualStyleBackColor = true;
            this.btnParseList01.Click += new System.EventHandler(this.btnParseList01_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnParseList01);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

    }

        #endregion

        private System.Windows.Forms.Button btnParseList01;
    }
}

