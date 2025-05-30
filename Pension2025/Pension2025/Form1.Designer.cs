﻿
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
            this.btnHttpList_Api = new System.Windows.Forms.Button();
            this.btnSaveToListApi = new System.Windows.Forms.Button();
            this.bttnCreateDsaLinkList = new System.Windows.Forms.Button();
            this.btnHttpList_Dsa = new System.Windows.Forms.Button();
            this.btnConvertRtfToTxt = new System.Windows.Forms.Button();
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
            this.btnParseDetails.Enabled = false;
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
            // btnHttpList_Api
            // 
            this.btnHttpList_Api.Enabled = false;
            this.btnHttpList_Api.Location = new System.Drawing.Point(307, 360);
            this.btnHttpList_Api.Name = "btnHttpList_Api";
            this.btnHttpList_Api.Size = new System.Drawing.Size(244, 27);
            this.btnHttpList_Api.TabIndex = 3;
            this.btnHttpList_Api.Text = "Debug.Print https list from ListApi folder";
            this.btnHttpList_Api.UseVisualStyleBackColor = true;
            this.btnHttpList_Api.Click += new System.EventHandler(this.btnHttpList_Api_Click);
            // 
            // btnSaveToListApi
            // 
            this.btnSaveToListApi.Location = new System.Drawing.Point(307, 30);
            this.btnSaveToListApi.Name = "btnSaveToListApi";
            this.btnSaveToListApi.Size = new System.Drawing.Size(244, 31);
            this.btnSaveToListApi.TabIndex = 4;
            this.btnSaveToListApi.Text = "Save item list into ListApi.txt";
            this.btnSaveToListApi.UseVisualStyleBackColor = true;
            this.btnSaveToListApi.Click += new System.EventHandler(this.btnSaveToListJson_Click);
            // 
            // bttnCreateDsaLinkList
            // 
            this.bttnCreateDsaLinkList.Location = new System.Drawing.Point(307, 79);
            this.bttnCreateDsaLinkList.Name = "bttnCreateDsaLinkList";
            this.bttnCreateDsaLinkList.Size = new System.Drawing.Size(172, 31);
            this.bttnCreateDsaLinkList.TabIndex = 5;
            this.bttnCreateDsaLinkList.Text = "Create DSA link list";
            this.bttnCreateDsaLinkList.UseVisualStyleBackColor = true;
            this.bttnCreateDsaLinkList.Click += new System.EventHandler(this.btnCreateDsaLinkList_Click);
            // 
            // btnHttpList_Dsa
            // 
            this.btnHttpList_Dsa.Location = new System.Drawing.Point(307, 135);
            this.btnHttpList_Dsa.Name = "btnHttpList_Dsa";
            this.btnHttpList_Dsa.Size = new System.Drawing.Size(244, 27);
            this.btnHttpList_Dsa.TabIndex = 6;
            this.btnHttpList_Dsa.Text = "Debug.Print https list from DSA folder";
            this.btnHttpList_Dsa.UseVisualStyleBackColor = true;
            this.btnHttpList_Dsa.Click += new System.EventHandler(this.btnHttpList_Dsa_Click);
            // 
            // btnConvertRtfToTxt
            // 
            this.btnConvertRtfToTxt.Location = new System.Drawing.Point(307, 185);
            this.btnConvertRtfToTxt.Name = "btnConvertRtfToTxt";
            this.btnConvertRtfToTxt.Size = new System.Drawing.Size(172, 31);
            this.btnConvertRtfToTxt.TabIndex = 7;
            this.btnConvertRtfToTxt.Text = "Convert RTF files to txt";
            this.btnConvertRtfToTxt.UseVisualStyleBackColor = true;
            this.btnConvertRtfToTxt.Click += new System.EventHandler(this.btnConvertRtfToTxt_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnConvertRtfToTxt);
            this.Controls.Add(this.btnHttpList_Dsa);
            this.Controls.Add(this.bttnCreateDsaLinkList);
            this.Controls.Add(this.btnSaveToListApi);
            this.Controls.Add(this.btnHttpList_Api);
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
        private System.Windows.Forms.Button btnHttpList_Api;
        private System.Windows.Forms.Button btnSaveToListApi;
        private System.Windows.Forms.Button bttnCreateDsaLinkList;
        private System.Windows.Forms.Button btnHttpList_Dsa;
        private System.Windows.Forms.Button btnConvertRtfToTxt;
    }
}

