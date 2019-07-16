namespace AVM
{
    partial class NodeViewer
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
            this.episodeNumberLabel = new System.Windows.Forms.Label();
            this.seasonNumberLabel = new System.Windows.Forms.Label();
            this.episodeNameLabel = new System.Windows.Forms.Label();
            this.audioCodecLabel = new System.Windows.Forms.Label();
            this.containerLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.videoCodecLabel = new System.Windows.Forms.Label();
            this.moreInfoButton = new System.Windows.Forms.Button();
            this.playButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // episodeNumberLabel
            // 
            this.episodeNumberLabel.AutoSize = true;
            this.episodeNumberLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.episodeNumberLabel.Location = new System.Drawing.Point(12, 85);
            this.episodeNumberLabel.Name = "episodeNumberLabel";
            this.episodeNumberLabel.Size = new System.Drawing.Size(62, 16);
            this.episodeNumberLabel.TabIndex = 26;
            this.episodeNumberLabel.Text = "Episode:";
            // 
            // seasonNumberLabel
            // 
            this.seasonNumberLabel.AutoSize = true;
            this.seasonNumberLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.seasonNumberLabel.Location = new System.Drawing.Point(12, 60);
            this.seasonNumberLabel.Name = "seasonNumberLabel";
            this.seasonNumberLabel.Size = new System.Drawing.Size(58, 16);
            this.seasonNumberLabel.TabIndex = 25;
            this.seasonNumberLabel.Text = "Season:";
            // 
            // episodeNameLabel
            // 
            this.episodeNameLabel.AutoSize = true;
            this.episodeNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.episodeNameLabel.Location = new System.Drawing.Point(12, 35);
            this.episodeNameLabel.Name = "episodeNameLabel";
            this.episodeNameLabel.Size = new System.Drawing.Size(102, 16);
            this.episodeNameLabel.TabIndex = 24;
            this.episodeNameLabel.Text = "Episode Name:";
            // 
            // audioCodecLabel
            // 
            this.audioCodecLabel.AutoSize = true;
            this.audioCodecLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.audioCodecLabel.Location = new System.Drawing.Point(12, 135);
            this.audioCodecLabel.Name = "audioCodecLabel";
            this.audioCodecLabel.Size = new System.Drawing.Size(89, 16);
            this.audioCodecLabel.TabIndex = 23;
            this.audioCodecLabel.Text = "Audio Codec:";
            // 
            // containerLabel
            // 
            this.containerLabel.AutoSize = true;
            this.containerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.containerLabel.Location = new System.Drawing.Point(12, 160);
            this.containerLabel.Name = "containerLabel";
            this.containerLabel.Size = new System.Drawing.Size(103, 16);
            this.containerLabel.TabIndex = 22;
            this.containerLabel.Text = "Container Type:";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.Location = new System.Drawing.Point(12, 10);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(48, 16);
            this.nameLabel.TabIndex = 21;
            this.nameLabel.Text = "Name:";
            // 
            // videoCodecLabel
            // 
            this.videoCodecLabel.AutoSize = true;
            this.videoCodecLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.videoCodecLabel.Location = new System.Drawing.Point(12, 110);
            this.videoCodecLabel.Name = "videoCodecLabel";
            this.videoCodecLabel.Size = new System.Drawing.Size(90, 16);
            this.videoCodecLabel.TabIndex = 20;
            this.videoCodecLabel.Text = "Video Codec:";
            // 
            // moreInfoButton
            // 
            this.moreInfoButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moreInfoButton.Location = new System.Drawing.Point(12, 188);
            this.moreInfoButton.Name = "moreInfoButton";
            this.moreInfoButton.Size = new System.Drawing.Size(75, 24);
            this.moreInfoButton.TabIndex = 28;
            this.moreInfoButton.Text = "More Info";
            this.moreInfoButton.UseVisualStyleBackColor = true;
            this.moreInfoButton.Click += new System.EventHandler(this.moreInfoButton_Click);
            // 
            // playButton
            // 
            this.playButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playButton.Location = new System.Drawing.Point(157, 188);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(75, 24);
            this.playButton.TabIndex = 27;
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // NodeViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 220);
            this.Controls.Add(this.moreInfoButton);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.episodeNumberLabel);
            this.Controls.Add(this.seasonNumberLabel);
            this.Controls.Add(this.episodeNameLabel);
            this.Controls.Add(this.audioCodecLabel);
            this.Controls.Add(this.containerLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.videoCodecLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "NodeViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Viewer";
            this.Load += new System.EventHandler(this.NodeViewer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label episodeNumberLabel;
        private System.Windows.Forms.Label seasonNumberLabel;
        private System.Windows.Forms.Label episodeNameLabel;
        private System.Windows.Forms.Label audioCodecLabel;
        private System.Windows.Forms.Label containerLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label videoCodecLabel;
        private System.Windows.Forms.Button moreInfoButton;
        private System.Windows.Forms.Button playButton;
    }
}