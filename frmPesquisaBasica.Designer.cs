namespace ZonView017
{
    partial class frmPesquisaBasica
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPesquisaBasica));
            this.lstLogico = new System.Windows.Forms.ListBox();
            this.lstRelacional = new System.Windows.Forms.ListBox();
            this.btnAdCriterio = new System.Windows.Forms.Button();
            this.btnCancela = new System.Windows.Forms.Button();
            this.btnPesquisa = new System.Windows.Forms.Button();
            this.btnSalva = new System.Windows.Forms.Button();
            this.btnAbrir = new System.Windows.Forms.Button();
            this.btnAjuda = new System.Windows.Forms.Button();
            this.btnLimpa = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSQLSintaxe = new System.Windows.Forms.TextBox();
            this.chkRelatorio = new System.Windows.Forms.CheckBox();
            this.lblAreaEstudo = new System.Windows.Forms.Button();
            this.cboAtributo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboValor = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lstLogico
            // 
            this.lstLogico.FormattingEnabled = true;
            this.lstLogico.Items.AddRange(new object[] {
            "E",
            "OU",
            "NÃO"});
            this.lstLogico.Location = new System.Drawing.Point(415, 184);
            this.lstLogico.Name = "lstLogico";
            this.lstLogico.Size = new System.Drawing.Size(90, 43);
            this.lstLogico.TabIndex = 88;
            // 
            // lstRelacional
            // 
            this.lstRelacional.FormattingEnabled = true;
            this.lstRelacional.Items.AddRange(new object[] {
            "igual",
            "maior",
            "menor",
            "diferente",
            "maior ou igual",
            "menor ou igual"});
            this.lstRelacional.Location = new System.Drawing.Point(415, 50);
            this.lstRelacional.Name = "lstRelacional";
            this.lstRelacional.Size = new System.Drawing.Size(90, 82);
            this.lstRelacional.TabIndex = 87;
            // 
            // btnAdCriterio
            // 
            this.btnAdCriterio.Location = new System.Drawing.Point(415, 275);
            this.btnAdCriterio.Name = "btnAdCriterio";
            this.btnAdCriterio.Size = new System.Drawing.Size(90, 37);
            this.btnAdCriterio.TabIndex = 85;
            this.btnAdCriterio.Text = "Adiciona critério";
            this.btnAdCriterio.UseVisualStyleBackColor = true;
            this.btnAdCriterio.Click += new System.EventHandler(this.btnAdCriterio_Click);
            // 
            // btnCancela
            // 
            this.btnCancela.Location = new System.Drawing.Point(268, 506);
            this.btnCancela.Name = "btnCancela";
            this.btnCancela.Size = new System.Drawing.Size(75, 23);
            this.btnCancela.TabIndex = 84;
            this.btnCancela.Text = "Cancela";
            this.btnCancela.UseVisualStyleBackColor = true;
            this.btnCancela.Click += new System.EventHandler(this.btnCancela_Click);
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.Location = new System.Drawing.Point(430, 506);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(75, 23);
            this.btnPesquisa.TabIndex = 83;
            this.btnPesquisa.Text = "Pesquisa";
            this.btnPesquisa.UseVisualStyleBackColor = true;
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisa_Click);
            // 
            // btnSalva
            // 
            this.btnSalva.Location = new System.Drawing.Point(430, 477);
            this.btnSalva.Name = "btnSalva";
            this.btnSalva.Size = new System.Drawing.Size(75, 23);
            this.btnSalva.TabIndex = 82;
            this.btnSalva.Text = "Salvar...";
            this.btnSalva.UseVisualStyleBackColor = true;
            this.btnSalva.Click += new System.EventHandler(this.btnSalva_Click);
            // 
            // btnAbrir
            // 
            this.btnAbrir.Location = new System.Drawing.Point(349, 477);
            this.btnAbrir.Name = "btnAbrir";
            this.btnAbrir.Size = new System.Drawing.Size(75, 23);
            this.btnAbrir.TabIndex = 81;
            this.btnAbrir.Text = "Abrir...";
            this.btnAbrir.UseVisualStyleBackColor = true;
            this.btnAbrir.Click += new System.EventHandler(this.btnAbrir_Click);
            // 
            // btnAjuda
            // 
            this.btnAjuda.Location = new System.Drawing.Point(268, 477);
            this.btnAjuda.Name = "btnAjuda";
            this.btnAjuda.Size = new System.Drawing.Size(75, 23);
            this.btnAjuda.TabIndex = 80;
            this.btnAjuda.Text = "Verifica";
            this.btnAjuda.UseVisualStyleBackColor = true;
            this.btnAjuda.Click += new System.EventHandler(this.btnAjuda_Click);
            // 
            // btnLimpa
            // 
            this.btnLimpa.Location = new System.Drawing.Point(187, 477);
            this.btnLimpa.Name = "btnLimpa";
            this.btnLimpa.Size = new System.Drawing.Size(75, 23);
            this.btnLimpa.TabIndex = 78;
            this.btnLimpa.Text = "Limpa";
            this.btnLimpa.UseVisualStyleBackColor = true;
            this.btnLimpa.Click += new System.EventHandler(this.btnLimpa_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 75;
            this.label2.Text = "Valores";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 74;
            this.label1.Text = "Atributos";
            // 
            // txtSQLSintaxe
            // 
            this.txtSQLSintaxe.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.txtSQLSintaxe.Location = new System.Drawing.Point(9, 334);
            this.txtSQLSintaxe.Multiline = true;
            this.txtSQLSintaxe.Name = "txtSQLSintaxe";
            this.txtSQLSintaxe.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSQLSintaxe.Size = new System.Drawing.Size(496, 114);
            this.txtSQLSintaxe.TabIndex = 77;
            // 
            // chkRelatorio
            // 
            this.chkRelatorio.AutoSize = true;
            this.chkRelatorio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkRelatorio.Location = new System.Drawing.Point(351, 454);
            this.chkRelatorio.Name = "chkRelatorio";
            this.chkRelatorio.Size = new System.Drawing.Size(154, 17);
            this.chkRelatorio.TabIndex = 89;
            this.chkRelatorio.Text = "Incluir relatório na pesquisa";
            this.chkRelatorio.UseVisualStyleBackColor = true;
            // 
            // lblAreaEstudo
            // 
            this.lblAreaEstudo.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.lblAreaEstudo.Location = new System.Drawing.Point(349, 506);
            this.lblAreaEstudo.Name = "lblAreaEstudo";
            this.lblAreaEstudo.Size = new System.Drawing.Size(75, 23);
            this.lblAreaEstudo.TabIndex = 93;
            this.lblAreaEstudo.Text = "Definir área";
            this.lblAreaEstudo.UseVisualStyleBackColor = true;
            this.lblAreaEstudo.Click += new System.EventHandler(this.lblAreaEstudo_Click);
            // 
            // cboAtributo
            // 
            this.cboAtributo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cboAtributo.FormattingEnabled = true;
            this.cboAtributo.Location = new System.Drawing.Point(9, 20);
            this.cboAtributo.Name = "cboAtributo";
            this.cboAtributo.Size = new System.Drawing.Size(394, 137);
            this.cboAtributo.TabIndex = 94;
            this.cboAtributo.SelectedIndexChanged += new System.EventHandler(this.cboAtributo_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(412, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 95;
            this.label3.Text = "Operadores";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(412, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 96;
            this.label4.Text = "Valores";
            // 
            // cboValor
            // 
            this.cboValor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cboValor.FormattingEnabled = true;
            this.cboValor.Location = new System.Drawing.Point(9, 184);
            this.cboValor.Name = "cboValor";
            this.cboValor.Size = new System.Drawing.Size(394, 137);
            this.cboValor.TabIndex = 97;
            // 
            // frmPesquisaBasica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 534);
            this.Controls.Add(this.cboValor);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboAtributo);
            this.Controls.Add(this.lblAreaEstudo);
            this.Controls.Add(this.chkRelatorio);
            this.Controls.Add(this.lstLogico);
            this.Controls.Add(this.lstRelacional);
            this.Controls.Add(this.btnAdCriterio);
            this.Controls.Add(this.btnCancela);
            this.Controls.Add(this.btnPesquisa);
            this.Controls.Add(this.btnSalva);
            this.Controls.Add(this.btnAbrir);
            this.Controls.Add(this.btnAjuda);
            this.Controls.Add(this.btnLimpa);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSQLSintaxe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPesquisaBasica";
            this.Text = "Formulário de Pesquisa Básica";
            this.Load += new System.EventHandler(this.frmPesquisaBasica_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstLogico;
        private System.Windows.Forms.ListBox lstRelacional;
        private System.Windows.Forms.Button btnAdCriterio;
        private System.Windows.Forms.Button btnCancela;
        private System.Windows.Forms.Button btnPesquisa;
        private System.Windows.Forms.Button btnSalva;
        private System.Windows.Forms.Button btnAbrir;
        private System.Windows.Forms.Button btnAjuda;
        private System.Windows.Forms.Button btnLimpa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSQLSintaxe;
        private System.Windows.Forms.CheckBox chkRelatorio;
        private System.Windows.Forms.Button lblAreaEstudo;
        private System.Windows.Forms.ComboBox cboAtributo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboValor;

    }
}