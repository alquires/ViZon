namespace ZonView017
{
    partial class frmRelatorio
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
            this.wbrRelatorio = new System.Windows.Forms.WebBrowser();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbImprimir = new System.Windows.Forms.ToolStripButton();
            this.tsbVerImpressao = new System.Windows.Forms.ToolStripButton();
            this.tsbExportar = new System.Windows.Forms.ToolStripButton();
            this.tsbSair = new System.Windows.Forms.ToolStripButton();
            this.tsbPDF = new System.Windows.Forms.ToolStripButton();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // wbrRelatorio
            // 
            this.wbrRelatorio.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.wbrRelatorio.Location = new System.Drawing.Point(0, 28);
            this.wbrRelatorio.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbrRelatorio.Name = "wbrRelatorio";
            this.wbrRelatorio.Size = new System.Drawing.Size(997, 500);
            this.wbrRelatorio.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbImprimir,
            this.tsbVerImpressao,
            this.tsbExportar,
            this.tsbSair,
            this.tsbPDF});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1000, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbImprimir
            // 
            this.tsbImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbImprimir.Image = global::ZonView017.Properties.Resources.Printer;
            this.tsbImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImprimir.Name = "tsbImprimir";
            this.tsbImprimir.Size = new System.Drawing.Size(23, 22);
            this.tsbImprimir.Text = "toolStripButton3";
            this.tsbImprimir.Click += new System.EventHandler(this.tsbImprimir_Click);
            // 
            // tsbVerImpressao
            // 
            this.tsbVerImpressao.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbVerImpressao.Image = global::ZonView017.Properties.Resources.preview;
            this.tsbVerImpressao.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbVerImpressao.Name = "tsbVerImpressao";
            this.tsbVerImpressao.Size = new System.Drawing.Size(23, 22);
            this.tsbVerImpressao.Text = "toolStripButton2";
            this.tsbVerImpressao.Click += new System.EventHandler(this.tsbVerImpressao_Click);
            // 
            // tsbExportar
            // 
            this.tsbExportar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExportar.Image = global::ZonView017.Properties.Resources._1305827470_make;
            this.tsbExportar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExportar.Name = "tsbExportar";
            this.tsbExportar.Size = new System.Drawing.Size(23, 22);
            this.tsbExportar.Text = "toolStripButton1";
            this.tsbExportar.Click += new System.EventHandler(this.tsbExportar_Click);
            // 
            // tsbSair
            // 
            this.tsbSair.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSair.Image = global::ZonView017.Properties.Resources.minus1;
            this.tsbSair.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSair.Name = "tsbSair";
            this.tsbSair.Size = new System.Drawing.Size(23, 22);
            this.tsbSair.Text = "toolStripButton4";
            this.tsbSair.Click += new System.EventHandler(this.tsbSair_Click);
            // 
            // tsbPDF
            // 
            this.tsbPDF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPDF.Image = global::ZonView017.Properties.Resources._1315312629_stock_save_pdf;
            this.tsbPDF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPDF.Name = "tsbPDF";
            this.tsbPDF.Size = new System.Drawing.Size(23, 22);
            this.tsbPDF.Text = "toolStripButton1";
            this.tsbPDF.ToolTipText = "Exportar para PDF";
            this.tsbPDF.Click += new System.EventHandler(this.tsbPDF_Click);
            // 
            // frmRelatorio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 529);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.wbrRelatorio);
            this.Name = "frmRelatorio";
            this.Text = "Relatorio";
            this.Load += new System.EventHandler(this.frmRelatorio_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbrRelatorio;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbExportar;
        private System.Windows.Forms.ToolStripButton tsbVerImpressao;
        private System.Windows.Forms.ToolStripButton tsbImprimir;
        private System.Windows.Forms.ToolStripButton tsbSair;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.ToolStripButton tsbPDF;
    }
}