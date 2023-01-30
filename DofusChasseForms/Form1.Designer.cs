namespace DofusChasseForms
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btn_go = new System.Windows.Forms.Button();
            this.tb_name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.EtapeLabel = new System.Windows.Forms.Label();
            this.CbAutoPilot = new System.Windows.Forms.CheckBox();
            this.NumIndice = new System.Windows.Forms.NumericUpDown();
            this.IndiceWordLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.XLabel = new System.Windows.Forms.Label();
            this.YLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NumIndice)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_go
            // 
            this.btn_go.BackColor = System.Drawing.Color.PaleGreen;
            this.btn_go.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_go.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_go.Location = new System.Drawing.Point(12, 200);
            this.btn_go.Name = "btn_go";
            this.btn_go.Size = new System.Drawing.Size(258, 23);
            this.btn_go.TabIndex = 0;
            this.btn_go.Text = "GO !";
            this.btn_go.UseVisualStyleBackColor = false;
            this.btn_go.Click += new System.EventHandler(this.Go_Click);
            // 
            // tb_name
            // 
            this.tb_name.Location = new System.Drawing.Point(12, 174);
            this.tb_name.Name = "tb_name";
            this.tb_name.Size = new System.Drawing.Size(258, 20);
            this.tb_name.TabIndex = 1;
            this.tb_name.Text = "Eithingg";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nom du personnage ingame";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Etape";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "Indice";
            // 
            // EtapeLabel
            // 
            this.EtapeLabel.AutoSize = true;
            this.EtapeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EtapeLabel.Location = new System.Drawing.Point(115, 9);
            this.EtapeLabel.Name = "EtapeLabel";
            this.EtapeLabel.Size = new System.Drawing.Size(17, 17);
            this.EtapeLabel.TabIndex = 7;
            this.EtapeLabel.Text = "1";
            // 
            // CbAutoPilot
            // 
            this.CbAutoPilot.AutoSize = true;
            this.CbAutoPilot.Checked = true;
            this.CbAutoPilot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CbAutoPilot.Location = new System.Drawing.Point(12, 47);
            this.CbAutoPilot.Name = "CbAutoPilot";
            this.CbAutoPilot.Size = new System.Drawing.Size(96, 17);
            this.CbAutoPilot.TabIndex = 9;
            this.CbAutoPilot.Text = "dd autopiloté ?";
            this.CbAutoPilot.UseVisualStyleBackColor = true;
            // 
            // NumIndice
            // 
            this.NumIndice.BackColor = System.Drawing.Color.PeachPuff;
            this.NumIndice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NumIndice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumIndice.Location = new System.Drawing.Point(118, 29);
            this.NumIndice.Name = "NumIndice";
            this.NumIndice.Size = new System.Drawing.Size(27, 16);
            this.NumIndice.TabIndex = 10;
            // 
            // IndiceWordLabel
            // 
            this.IndiceWordLabel.AutoSize = true;
            this.IndiceWordLabel.Location = new System.Drawing.Point(151, 30);
            this.IndiceWordLabel.MaximumSize = new System.Drawing.Size(132, 15);
            this.IndiceWordLabel.Name = "IndiceWordLabel";
            this.IndiceWordLabel.Size = new System.Drawing.Size(120, 15);
            this.IndiceWordLabel.TabIndex = 11;
            this.IndiceWordLabel.Text = "azfazfazfbaif azf azf azf azf azf  fazfazfazfazf azfazf azfazf azf";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "X";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "Y";
            // 
            // XLabel
            // 
            this.XLabel.AutoSize = true;
            this.XLabel.Location = new System.Drawing.Point(32, 71);
            this.XLabel.Name = "XLabel";
            this.XLabel.Size = new System.Drawing.Size(35, 13);
            this.XLabel.TabIndex = 14;
            this.XLabel.Text = "label6";
            // 
            // YLabel
            // 
            this.YLabel.AutoSize = true;
            this.YLabel.Location = new System.Drawing.Point(32, 89);
            this.YLabel.Name = "YLabel";
            this.YLabel.Size = new System.Drawing.Size(35, 13);
            this.YLabel.TabIndex = 15;
            this.YLabel.Text = "label7";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.PeachPuff;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(282, 235);
            this.Controls.Add(this.YLabel);
            this.Controls.Add(this.XLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.IndiceWordLabel);
            this.Controls.Add(this.NumIndice);
            this.Controls.Add(this.CbAutoPilot);
            this.Controls.Add(this.EtapeLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_name);
            this.Controls.Add(this.btn_go);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(298, 274);
            this.MinimumSize = new System.Drawing.Size(298, 274);
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Eynwa Chasse";
            ((System.ComponentModel.ISupportInitialize)(this.NumIndice)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_go;
        private System.Windows.Forms.TextBox tb_name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label EtapeLabel;
        private System.Windows.Forms.CheckBox CbAutoPilot;
        private System.Windows.Forms.NumericUpDown NumIndice;
        private System.Windows.Forms.Label IndiceWordLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label XLabel;
        private System.Windows.Forms.Label YLabel;
    }
}

