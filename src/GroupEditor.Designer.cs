namespace AVM
{
    partial class GroupEditor
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
            this.renameLabel = new System.Windows.Forms.Label();
            this.renameTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // renameLabel
            // 
            this.renameLabel.AutoSize = true;
            this.renameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.renameLabel.Location = new System.Drawing.Point(12, 9);
            this.renameLabel.Name = "renameLabel";
            this.renameLabel.Size = new System.Drawing.Size(180, 16);
            this.renameLabel.TabIndex = 0;
            this.renameLabel.Text = "What do you want to name it?";
            // 
            // renameTextBox
            // 
            this.renameTextBox.Location = new System.Drawing.Point(12, 28);
            this.renameTextBox.Name = "renameTextBox";
            this.renameTextBox.Size = new System.Drawing.Size(267, 20);
            this.renameTextBox.TabIndex = 1;
            this.renameTextBox.TextChanged += new System.EventHandler(this.renameTextBox_TextChanged);
            this.renameTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.renameTextBox_KeyPress);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(204, 54);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "Rename";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(123, 54);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // GroupEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 85);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.renameTextBox);
            this.Controls.Add(this.renameLabel);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(307, 123);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(307, 123);
            this.Name = "GroupEditor";
            this.Text = "Rename Group";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label renameLabel;
        private System.Windows.Forms.TextBox renameTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}