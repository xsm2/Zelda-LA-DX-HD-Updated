namespace LADXHD_Patcher
{
    partial class Form_MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_MainForm));
            this.button_Patch = new System.Windows.Forms.Button();
            this.button_Exit = new System.Windows.Forms.Button();
            this.button_ChangeLog = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_Patch
            // 
            this.button_Patch.Location = new System.Drawing.Point(9, 222);
            this.button_Patch.Name = "button_Patch";
            this.button_Patch.Size = new System.Drawing.Size(100, 30);
            this.button_Patch.TabIndex = 0;
            this.button_Patch.Text = "Patch";
            this.button_Patch.UseVisualStyleBackColor = true;
            this.button_Patch.Click += new System.EventHandler(this.button_Patch_Click);
            // 
            // button_Exit
            // 
            this.button_Exit.Location = new System.Drawing.Point(225, 222);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.Size = new System.Drawing.Size(100, 30);
            this.button_Exit.TabIndex = 1;
            this.button_Exit.Text = "Exit";
            this.button_Exit.UseVisualStyleBackColor = true;
            this.button_Exit.Click += new System.EventHandler(this.button_Exit_Click);
            // 
            // button_ChangeLog
            // 
            this.button_ChangeLog.Location = new System.Drawing.Point(117, 222);
            this.button_ChangeLog.Name = "button_ChangeLog";
            this.button_ChangeLog.Size = new System.Drawing.Size(100, 30);
            this.button_ChangeLog.TabIndex = 3;
            this.button_ChangeLog.Text = "Changelog";
            this.button_ChangeLog.UseVisualStyleBackColor = true;
            this.button_ChangeLog.Click += new System.EventHandler(this.button_ChangeLog_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = global::LADXHD_Patcher.Properties.Resources.la;
            this.pictureBox1.Image = global::LADXHD_Patcher.Properties.Resources.la;
            this.pictureBox1.InitialImage = global::LADXHD_Patcher.Properties.Resources.la;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(334, 123);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // Form_MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 261);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button_ChangeLog);
            this.Controls.Add(this.button_Exit);
            this.Controls.Add(this.button_Patch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Link\'s Awakening DX HD Patcher vX.X.X";
            this.Load += new System.EventHandler(this.Form_MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button_Patch;
        private System.Windows.Forms.Button button_Exit;
        private System.Windows.Forms.Button button_ChangeLog;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

