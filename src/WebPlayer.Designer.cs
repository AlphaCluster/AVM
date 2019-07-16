namespace AVM
{
    partial class WebPlayer
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
            this.videoWebBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // videoWebBrowser
            // 
            this.videoWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.videoWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.videoWebBrowser.Name = "videoWebBrowser";
            this.videoWebBrowser.ScrollBarsEnabled = false;
            this.videoWebBrowser.Size = new System.Drawing.Size(565, 366);
            this.videoWebBrowser.TabIndex = 0;
            this.videoWebBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.videoWebBrowser_Navigating);
            // 
            // WebPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 366);
            this.Controls.Add(this.videoWebBrowser);
            this.Name = "WebPlayer";
            this.Text = "WebPlayer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WebPlayer_FormClosing);
            this.Resize += new System.EventHandler(this.WebPlayer_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser videoWebBrowser;
    }
}