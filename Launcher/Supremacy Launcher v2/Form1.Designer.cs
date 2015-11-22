namespace Supremacy_Launcher_v2
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDownloadInstall = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.btnPlay = new System.Windows.Forms.Button();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox_logo = new System.Windows.Forms.PictureBox();
            this.pictureBox_dragarea = new System.Windows.Forms.PictureBox();
            this.pictureBox_navigation = new System.Windows.Forms.PictureBox();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_dragarea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_navigation)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.White;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(982, 19);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(35, 35);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnDownloadInstall
            // 
            this.btnDownloadInstall.BackColor = System.Drawing.Color.Transparent;
            this.btnDownloadInstall.BackgroundImage = global::Supremacy_Launcher_v2.Properties.Resources.btnInstallmod;
            this.btnDownloadInstall.Enabled = false;
            this.btnDownloadInstall.FlatAppearance.BorderSize = 0;
            this.btnDownloadInstall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownloadInstall.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownloadInstall.Location = new System.Drawing.Point(631, 557);
            this.btnDownloadInstall.Name = "btnDownloadInstall";
            this.btnDownloadInstall.Size = new System.Drawing.Size(386, 83);
            this.btnDownloadInstall.TabIndex = 1;
            this.btnDownloadInstall.UseVisualStyleBackColor = false;
            this.btnDownloadInstall.Click += new System.EventHandler(this.btnDownloadInstall_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusProgressBar,
            this.lblStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 746);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1024, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 5;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusProgressBar
            // 
            this.statusProgressBar.Name = "statusProgressBar";
            this.statusProgressBar.Size = new System.Drawing.Size(200, 16);
            // 
            // lblStatus
            // 
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(118, 17);
            this.lblStatus.Text = "toolStripStatusLabel1";
            // 
            // webBrowser1
            // 
            this.webBrowser1.AllowNavigation = false;
            this.webBrowser1.AllowWebBrowserDrop = false;
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.Location = new System.Drawing.Point(9, 78);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(616, 661);
            this.webBrowser1.TabIndex = 7;
            this.webBrowser1.Url = new System.Uri("http://www.your-community-website.com/launcher/news.html", System.UriKind.Absolute);
            this.webBrowser1.WebBrowserShortcutsEnabled = false;
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.Color.Transparent;
            this.btnPlay.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPlay.BackgroundImage")));
            this.btnPlay.Enabled = false;
            this.btnPlay.FlatAppearance.BorderSize = 0;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlay.ForeColor = System.Drawing.Color.White;
            this.btnPlay.Location = new System.Drawing.Point(631, 656);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(386, 83);
            this.btnPlay.TabIndex = 8;
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(270, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "www.sirmre.com";
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.White;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Location = new System.Drawing.Point(788, 19);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(151, 35);
            this.btnReset.TabIndex = 10;
            this.btnReset.Text = "Change ARMA 3 Path";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Visible = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(631, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(151, 35);
            this.button1.TabIndex = 11;
            this.button1.Text = "Set Launch Parameters";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            // 
            // pictureBox_logo
            // 
            this.pictureBox_logo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pictureBox_logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox_logo.Image = global::Supremacy_Launcher_v2.Properties.Resources.logo;
            this.pictureBox_logo.Location = new System.Drawing.Point(13, 19);
            this.pictureBox_logo.Name = "pictureBox_logo";
            this.pictureBox_logo.Size = new System.Drawing.Size(401, 35);
            this.pictureBox_logo.TabIndex = 0;
            this.pictureBox_logo.TabStop = false;
            this.pictureBox_logo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_logo_MouseDown);
            // 
            // pictureBox_dragarea
            // 
            this.pictureBox_dragarea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pictureBox_dragarea.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_dragarea.Name = "pictureBox_dragarea";
            this.pictureBox_dragarea.Size = new System.Drawing.Size(782, 70);
            this.pictureBox_dragarea.TabIndex = 1;
            this.pictureBox_dragarea.TabStop = false;
            this.pictureBox_dragarea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_dragarea_MouseDown);
            // 
            // pictureBox_navigation
            // 
            this.pictureBox_navigation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pictureBox_navigation.Location = new System.Drawing.Point(779, 0);
            this.pictureBox_navigation.Name = "pictureBox_navigation";
            this.pictureBox_navigation.Size = new System.Drawing.Size(245, 70);
            this.pictureBox_navigation.TabIndex = 6;
            this.pictureBox_navigation.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Supremacy_Launcher_v2.Properties.Resources.launcher_bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.btnDownloadInstall);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.pictureBox_logo);
            this.Controls.Add(this.pictureBox_dragarea);
            this.Controls.Add(this.pictureBox_navigation);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "w";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_dragarea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_navigation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_logo;
        private System.Windows.Forms.PictureBox pictureBox_dragarea;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDownloadInstall;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripProgressBar statusProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.PictureBox pictureBox_navigation;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button button1;
    }
}

