namespace ZonView017
{
    partial class frmFormatRel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFormatRel));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.grbCalcVal = new System.Windows.Forms.GroupBox();
            this.radOmitirArea = new System.Windows.Forms.RadioButton();
            this.radPercEstudo = new System.Windows.Forms.RadioButton();
            this.radPercSubGrupo = new System.Windows.Forms.RadioButton();
            this.radPercGrupo = new System.Windows.Forms.RadioButton();
            this.radSoma = new System.Windows.Forms.RadioButton();
            this.grbFormatacao = new System.Windows.Forms.GroupBox();
            this.cboClasse = new System.Windows.Forms.ComboBox();
            this.cboSubGrupo = new System.Windows.Forms.ComboBox();
            this.cboGrupo = new System.Windows.Forms.ComboBox();
            this.lblClasse = new System.Windows.Forms.Label();
            this.lblSubGrupo = new System.Windows.Forms.Label();
            this.lblGrupo = new System.Windows.Forms.Label();
            this.prgAndamento = new System.Windows.Forms.ProgressBar();
            this.ckbAreaPerc = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nudArredonda = new System.Windows.Forms.NumericUpDown();
            this.grbCalcVal.SuspendLayout();
            this.grbFormatacao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudArredonda)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(309, 330);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 11;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(223, 330);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 12;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // grbCalcVal
            // 
            this.grbCalcVal.Controls.Add(this.radOmitirArea);
            this.grbCalcVal.Controls.Add(this.radPercEstudo);
            this.grbCalcVal.Controls.Add(this.radPercSubGrupo);
            this.grbCalcVal.Controls.Add(this.radPercGrupo);
            this.grbCalcVal.Controls.Add(this.radSoma);
            this.grbCalcVal.Location = new System.Drawing.Point(6, 171);
            this.grbCalcVal.Name = "grbCalcVal";
            this.grbCalcVal.Size = new System.Drawing.Size(384, 137);
            this.grbCalcVal.TabIndex = 18;
            this.grbCalcVal.TabStop = false;
            this.grbCalcVal.Text = "Calculo de valores:";
            // 
            // radOmitirArea
            // 
            this.radOmitirArea.AutoSize = true;
            this.radOmitirArea.Location = new System.Drawing.Point(6, 111);
            this.radOmitirArea.Name = "radOmitirArea";
            this.radOmitirArea.Size = new System.Drawing.Size(127, 17);
            this.radOmitirArea.TabIndex = 14;
            this.radOmitirArea.TabStop = true;
            this.radOmitirArea.Text = "Omitir cálculo de area";
            this.radOmitirArea.UseVisualStyleBackColor = true;
            // 
            // radPercEstudo
            // 
            this.radPercEstudo.AutoSize = true;
            this.radPercEstudo.Location = new System.Drawing.Point(6, 42);
            this.radPercEstudo.Name = "radPercEstudo";
            this.radPercEstudo.Size = new System.Drawing.Size(273, 17);
            this.radPercEstudo.TabIndex = 13;
            this.radPercEstudo.Text = "Porcentagem da área em relação à região de estudo";
            this.radPercEstudo.UseVisualStyleBackColor = true;
            // 
            // radPercSubGrupo
            // 
            this.radPercSubGrupo.AutoSize = true;
            this.radPercSubGrupo.Location = new System.Drawing.Point(6, 88);
            this.radPercSubGrupo.Name = "radPercSubGrupo";
            this.radPercSubGrupo.Size = new System.Drawing.Size(244, 17);
            this.radPercSubGrupo.TabIndex = 12;
            this.radPercSubGrupo.Text = "Porcentagem da área em relação ao subgrupo";
            this.radPercSubGrupo.UseVisualStyleBackColor = true;
            // 
            // radPercGrupo
            // 
            this.radPercGrupo.AutoSize = true;
            this.radPercGrupo.Location = new System.Drawing.Point(6, 65);
            this.radPercGrupo.Name = "radPercGrupo";
            this.radPercGrupo.Size = new System.Drawing.Size(227, 17);
            this.radPercGrupo.TabIndex = 11;
            this.radPercGrupo.Text = "Porcentagem da área em relação ao grupo";
            this.radPercGrupo.UseVisualStyleBackColor = true;
            // 
            // radSoma
            // 
            this.radSoma.AutoSize = true;
            this.radSoma.Checked = true;
            this.radSoma.Location = new System.Drawing.Point(6, 19);
            this.radSoma.Name = "radSoma";
            this.radSoma.Size = new System.Drawing.Size(250, 17);
            this.radSoma.TabIndex = 10;
            this.radSoma.TabStop = true;
            this.radSoma.Text = "Área absoluta (área de polígonos selecionados)";
            this.radSoma.UseVisualStyleBackColor = true;
            // 
            // grbFormatacao
            // 
            this.grbFormatacao.Controls.Add(this.nudArredonda);
            this.grbFormatacao.Controls.Add(this.cboClasse);
            this.grbFormatacao.Controls.Add(this.label1);
            this.grbFormatacao.Controls.Add(this.cboSubGrupo);
            this.grbFormatacao.Controls.Add(this.ckbAreaPerc);
            this.grbFormatacao.Controls.Add(this.cboGrupo);
            this.grbFormatacao.Controls.Add(this.lblClasse);
            this.grbFormatacao.Controls.Add(this.lblSubGrupo);
            this.grbFormatacao.Controls.Add(this.lblGrupo);
            this.grbFormatacao.Location = new System.Drawing.Point(6, 2);
            this.grbFormatacao.Name = "grbFormatacao";
            this.grbFormatacao.Size = new System.Drawing.Size(384, 153);
            this.grbFormatacao.TabIndex = 19;
            this.grbFormatacao.TabStop = false;
            this.grbFormatacao.Text = "Formatação";
            // 
            // cboClasse
            // 
            this.cboClasse.FormattingEnabled = true;
            this.cboClasse.Location = new System.Drawing.Point(65, 69);
            this.cboClasse.Name = "cboClasse";
            this.cboClasse.Size = new System.Drawing.Size(313, 21);
            this.cboClasse.TabIndex = 20;
            // 
            // cboSubGrupo
            // 
            this.cboSubGrupo.FormattingEnabled = true;
            this.cboSubGrupo.Location = new System.Drawing.Point(65, 43);
            this.cboSubGrupo.Name = "cboSubGrupo";
            this.cboSubGrupo.Size = new System.Drawing.Size(313, 21);
            this.cboSubGrupo.TabIndex = 19;
            // 
            // cboGrupo
            // 
            this.cboGrupo.FormattingEnabled = true;
            this.cboGrupo.Location = new System.Drawing.Point(64, 14);
            this.cboGrupo.Name = "cboGrupo";
            this.cboGrupo.Size = new System.Drawing.Size(314, 21);
            this.cboGrupo.TabIndex = 18;
            // 
            // lblClasse
            // 
            this.lblClasse.AutoSize = true;
            this.lblClasse.Location = new System.Drawing.Point(6, 77);
            this.lblClasse.Name = "lblClasse";
            this.lblClasse.Size = new System.Drawing.Size(38, 13);
            this.lblClasse.TabIndex = 17;
            this.lblClasse.Text = "Classe";
            // 
            // lblSubGrupo
            // 
            this.lblSubGrupo.AutoSize = true;
            this.lblSubGrupo.Location = new System.Drawing.Point(6, 51);
            this.lblSubGrupo.Name = "lblSubGrupo";
            this.lblSubGrupo.Size = new System.Drawing.Size(53, 13);
            this.lblSubGrupo.TabIndex = 16;
            this.lblSubGrupo.Text = "Subgrupo";
            // 
            // lblGrupo
            // 
            this.lblGrupo.AutoSize = true;
            this.lblGrupo.Location = new System.Drawing.Point(6, 22);
            this.lblGrupo.Name = "lblGrupo";
            this.lblGrupo.Size = new System.Drawing.Size(36, 13);
            this.lblGrupo.TabIndex = 15;
            this.lblGrupo.Text = "Grupo";
            // 
            // prgAndamento
            // 
            this.prgAndamento.Location = new System.Drawing.Point(12, 338);
            this.prgAndamento.Name = "prgAndamento";
            this.prgAndamento.Size = new System.Drawing.Size(205, 15);
            this.prgAndamento.TabIndex = 20;
            // 
            // ckbAreaPerc
            // 
            this.ckbAreaPerc.AutoSize = true;
            this.ckbAreaPerc.Checked = true;
            this.ckbAreaPerc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbAreaPerc.Location = new System.Drawing.Point(9, 96);
            this.ckbAreaPerc.Name = "ckbAreaPerc";
            this.ckbAreaPerc.Size = new System.Drawing.Size(180, 43);
            this.ckbAreaPerc.TabIndex = 21;
            this.ckbAreaPerc.Text = "Nos cálculos de porcentagem,\r\nconsiderar toda a área do grupo,\r\nsubgrupo ou área " +
                "de estudo.";
            this.ckbAreaPerc.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(295, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 26);
            this.label1.TabIndex = 22;
            this.label1.Text = "Casas decimais\r\npara as áreas";
            // 
            // nudArredonda
            // 
            this.nudArredonda.Location = new System.Drawing.Point(298, 127);
            this.nudArredonda.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudArredonda.Name = "nudArredonda";
            this.nudArredonda.Size = new System.Drawing.Size(80, 20);
            this.nudArredonda.TabIndex = 23;
            this.nudArredonda.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            //this.nudArredonda.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // frmFormatRel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 362);
            this.Controls.Add(this.prgAndamento);
            this.Controls.Add(this.grbFormatacao);
            this.Controls.Add(this.grbCalcVal);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmFormatRel";
            this.Text = "Formatação do relatório";
            this.Load += new System.EventHandler(this.frmFormatRel_Load);
            this.grbCalcVal.ResumeLayout(false);
            this.grbCalcVal.PerformLayout();
            this.grbFormatacao.ResumeLayout(false);
            this.grbFormatacao.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudArredonda)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.GroupBox grbCalcVal;
        private System.Windows.Forms.RadioButton radPercSubGrupo;
        private System.Windows.Forms.RadioButton radPercGrupo;
        private System.Windows.Forms.RadioButton radSoma;
        private System.Windows.Forms.GroupBox grbFormatacao;
        private System.Windows.Forms.ComboBox cboClasse;
        private System.Windows.Forms.ComboBox cboSubGrupo;
        private System.Windows.Forms.ComboBox cboGrupo;
        private System.Windows.Forms.Label lblClasse;
        private System.Windows.Forms.Label lblSubGrupo;
        private System.Windows.Forms.Label lblGrupo;
        private System.Windows.Forms.RadioButton radPercEstudo;
        private System.Windows.Forms.ProgressBar prgAndamento;
        private System.Windows.Forms.RadioButton radOmitirArea;
        private System.Windows.Forms.CheckBox ckbAreaPerc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudArredonda;
    }
}