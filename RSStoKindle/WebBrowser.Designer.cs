namespace RSStoKindle
{
    partial class WebBrowserForm
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
            this.panelMenu = new System.Windows.Forms.Panel();
            this.labelPageHost = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonLoadOriginal = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.buttonAccept = new System.Windows.Forms.Button();
            this.buttonDecline = new System.Windows.Forms.Button();
            this.panelBrowser = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panelMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.Controls.Add(this.labelPageHost);
            this.panelMenu.Controls.Add(this.panel1);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(883, 114);
            this.panelMenu.TabIndex = 0;
            // 
            // labelPageHost
            // 
            this.labelPageHost.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelPageHost.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelPageHost.Location = new System.Drawing.Point(194, 9);
            this.labelPageHost.MinimumSize = new System.Drawing.Size(500, 20);
            this.labelPageHost.Name = "labelPageHost";
            this.labelPageHost.Size = new System.Drawing.Size(500, 20);
            this.labelPageHost.TabIndex = 0;
            this.labelPageHost.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.buttonLoadOriginal);
            this.panel1.Controls.Add(this.buttonBack);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Controls.Add(this.buttonAccept);
            this.panel1.Controls.Add(this.buttonDecline);
            this.panel1.Location = new System.Drawing.Point(194, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 114);
            this.panel1.TabIndex = 0;
            // 
            // buttonLoadOriginal
            // 
            this.buttonLoadOriginal.Location = new System.Drawing.Point(263, 41);
            this.buttonLoadOriginal.Name = "buttonLoadOriginal";
            this.buttonLoadOriginal.Size = new System.Drawing.Size(144, 23);
            this.buttonLoadOriginal.TabIndex = 5;
            this.buttonLoadOriginal.Text = "Załaduj oryginalny HTML";
            this.buttonLoadOriginal.UseVisualStyleBackColor = true;
            this.buttonLoadOriginal.Click += new System.EventHandler(this.buttonLoadOriginal_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(298, 70);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(75, 23);
            this.buttonBack.TabIndex = 5;
            this.buttonBack.Text = "Cofnij akcję";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(14, 70);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(199, 17);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.Text = "usuń wszystko oprócz tego elementu";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(14, 48);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(106, 17);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "usuń ten element";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // buttonAccept
            // 
            this.buttonAccept.Location = new System.Drawing.Point(413, 41);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(75, 23);
            this.buttonAccept.TabIndex = 2;
            this.buttonAccept.Text = "Potwierdź";
            this.buttonAccept.UseVisualStyleBackColor = true;
            // 
            // buttonDecline
            // 
            this.buttonDecline.Enabled = false;
            this.buttonDecline.Location = new System.Drawing.Point(413, 70);
            this.buttonDecline.Name = "buttonDecline";
            this.buttonDecline.Size = new System.Drawing.Size(75, 23);
            this.buttonDecline.TabIndex = 1;
            this.buttonDecline.Text = "Odrzuć";
            this.buttonDecline.UseVisualStyleBackColor = true;
            this.buttonDecline.Click += new System.EventHandler(this.buttonDecline_Click);
            // 
            // panelBrowser
            // 
            this.panelBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBrowser.Location = new System.Drawing.Point(0, 114);
            this.panelBrowser.Name = "panelBrowser";
            this.panelBrowser.Size = new System.Drawing.Size(883, 228);
            this.panelBrowser.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(151, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // WebBrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 342);
            this.Controls.Add(this.panelBrowser);
            this.Controls.Add(this.panelMenu);
            this.Name = "WebBrowserForm";
            this.Text = "WebBrowser";
            this.panelMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelBrowser;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.Button buttonDecline;
        private System.Windows.Forms.Label labelPageHost;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonLoadOriginal;
        private System.Windows.Forms.Button button1;
    }
}