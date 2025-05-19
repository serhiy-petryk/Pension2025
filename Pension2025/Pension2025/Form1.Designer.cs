
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
            this.btnParseList = new System.Windows.Forms.Button();
            this.btnParseDetails = new System.Windows.Forms.Button();
            this.btnRemoveUselessTagsOfDetails = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnParseList
            // 
            this.btnParseList.Enabled = false;
            this.btnParseList.Location = new System.Drawing.Point(30, 30);
            this.btnParseList.Name = "btnParseList";
            this.btnParseList.Size = new System.Drawing.Size(172, 31);
            this.btnParseList.TabIndex = 0;
            this.btnParseList.Text = "Parse list files into List.txt";
            this.btnParseList.UseVisualStyleBackColor = true;
            this.btnParseList.Click += new System.EventHandler(this.btnParseList_Click);
            // 
            // btnParseDetails
            // 
            this.btnParseDetails.Location = new System.Drawing.Point(30, 131);
            this.btnParseDetails.Name = "btnParseDetails";
            this.btnParseDetails.Size = new System.Drawing.Size(172, 31);
            this.btnParseDetails.TabIndex = 1;
            this.btnParseDetails.Text = "Parse details files";
            this.btnParseDetails.UseVisualStyleBackColor = true;
            this.btnParseDetails.Click += new System.EventHandler(this.btnParseDetails_Click);
            // 
            // btnRemoveUselessTagsOfDetails
            // 
            this.btnRemoveUselessTagsOfDetails.Enabled = false;
            this.btnRemoveUselessTagsOfDetails.Location = new System.Drawing.Point(30, 79);
            this.btnRemoveUselessTagsOfDetails.Name = "btnRemoveUselessTagsOfDetails";
            this.btnRemoveUselessTagsOfDetails.Size = new System.Drawing.Size(231, 31);
            this.btnRemoveUselessTagsOfDetails.TabIndex = 2;
            this.btnRemoveUselessTagsOfDetails.Text = "RemoveUselessTags of detail htmls";
            this.btnRemoveUselessTagsOfDetails.UseVisualStyleBackColor = true;
            this.btnRemoveUselessTagsOfDetails.Click += new System.EventHandler(this.btnRemoveUselessTagsOfDetails_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnRemoveUselessTagsOfDetails);
            this.Controls.Add(this.btnParseDetails);
            this.Controls.Add(this.btnParseList);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

    }

        #endregion

        private System.Windows.Forms.Button btnParseList;
        private System.Windows.Forms.Button btnParseDetails;
        private System.Windows.Forms.Button btnRemoveUselessTagsOfDetails;
    }
}

