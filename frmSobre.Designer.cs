namespace ZonView017
{
    partial class frmSobre
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSobre));
            this.btnOK = new System.Windows.Forms.Button();
            this.txtInfoProj = new System.Windows.Forms.TextBox();
            this.txtInfoSoft = new System.Windows.Forms.TextBox();
            this.picLogoSoft = new System.Windows.Forms.PictureBox();
            this.picLogoProj = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picLogoSoft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogoProj)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(870, 383);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtInfoProj
            // 
            this.txtInfoProj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInfoProj.Location = new System.Drawing.Point(505, 253);
            this.txtInfoProj.Multiline = true;
            this.txtInfoProj.Name = "txtInfoProj";
            this.txtInfoProj.ReadOnly = true;
            this.txtInfoProj.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInfoProj.Size = new System.Drawing.Size(440, 124);
            this.txtInfoProj.TabIndex = 5;
            // 
            // txtInfoSoft
            // 
            this.txtInfoSoft.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInfoSoft.Location = new System.Drawing.Point(12, 253);
            this.txtInfoSoft.Multiline = true;
            this.txtInfoSoft.Name = "txtInfoSoft";
            this.txtInfoSoft.ReadOnly = true;
            this.txtInfoSoft.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInfoSoft.Size = new System.Drawing.Size(440, 124);
            this.txtInfoSoft.TabIndex = 4;
            // 
            // picLogoSoft
            // 
            this.picLogoSoft.Image = global::ZonView017.Properties.Resources.BanerSoftware;
            this.picLogoSoft.Location = new System.Drawing.Point(12, 12);
            this.picLogoSoft.Name = "picLogoSoft";
            this.picLogoSoft.Size = new System.Drawing.Size(440, 235);
            this.picLogoSoft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogoSoft.TabIndex = 7;
            this.picLogoSoft.TabStop = false;
            // 
            // picLogoProj
            // 
            this.picLogoProj.Location = new System.Drawing.Point(505, 12);
            this.picLogoProj.Name = "picLogoProj";
            this.picLogoProj.Size = new System.Drawing.Size(440, 235);
            this.picLogoProj.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogoProj.TabIndex = 8;
            this.picLogoProj.TabStop = false;
            // 
            // frmSobre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 411);
            this.Controls.Add(this.picLogoProj);
            this.Controls.Add(this.picLogoSoft);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtInfoProj);
            this.Controls.Add(this.txtInfoSoft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSobre";
            this.Text = "frmSobre";
            this.Load += new System.EventHandler(this.frmSobre_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picLogoSoft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogoProj)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtInfoProj;
        private System.Windows.Forms.TextBox txtInfoSoft;
        public System.Windows.Forms.PictureBox picLogoSoft;
        public System.Windows.Forms.PictureBox picLogoProj;
    }
}