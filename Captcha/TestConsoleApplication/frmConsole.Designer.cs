namespace TestConsoleApplication
{
    partial class frmConsole
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
            this.pbCaptcha = new System.Windows.Forms.PictureBox();
            this.btnValidate = new System.Windows.Forms.Button();
            this.txtEntry = new System.Windows.Forms.TextBox();
            this.btnGenerateNew = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbCaptcha)).BeginInit();
            this.SuspendLayout();
            // 
            // pbCaptcha
            // 
            this.pbCaptcha.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbCaptcha.Location = new System.Drawing.Point(12, 12);
            this.pbCaptcha.Name = "pbCaptcha";
            this.pbCaptcha.Size = new System.Drawing.Size(400, 150);
            this.pbCaptcha.TabIndex = 0;
            this.pbCaptcha.TabStop = false;
            // 
            // btnValidate
            // 
            this.btnValidate.Location = new System.Drawing.Point(272, 201);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(75, 23);
            this.btnValidate.TabIndex = 1;
            this.btnValidate.Text = "Validate";
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // txtEntry
            // 
            this.txtEntry.Location = new System.Drawing.Point(12, 201);
            this.txtEntry.Name = "txtEntry";
            this.txtEntry.Size = new System.Drawing.Size(254, 20);
            this.txtEntry.TabIndex = 2;
            // 
            // btnGenerateNew
            // 
            this.btnGenerateNew.Location = new System.Drawing.Point(13, 168);
            this.btnGenerateNew.Name = "btnGenerateNew";
            this.btnGenerateNew.Size = new System.Drawing.Size(195, 23);
            this.btnGenerateNew.TabIndex = 3;
            this.btnGenerateNew.Text = "Generate New Captcha 1";
            this.btnGenerateNew.UseVisualStyleBackColor = true;
            this.btnGenerateNew.Click += new System.EventHandler(this.btnGenerateNew_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(12, 243);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(223, 168);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(189, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Generate New Captcha 2";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 447);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnGenerateNew);
            this.Controls.Add(this.txtEntry);
            this.Controls.Add(this.btnValidate);
            this.Controls.Add(this.pbCaptcha);
            this.Name = "frmConsole";
            this.Text = "Test Console Application";
            ((System.ComponentModel.ISupportInitialize)(this.pbCaptcha)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbCaptcha;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.TextBox txtEntry;
        private System.Windows.Forms.Button btnGenerateNew;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button button1;
    }
}

