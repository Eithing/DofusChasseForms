using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DofusChasseForms
{
    public partial class Form1 : Form
    {
        Chasse chasse = new Chasse();

        public Form1()
        {
            InitializeComponent();
            IndiceWordLabel.DataBindings.Add("Text", chasse, "Indice");
            EtapeLabel.DataBindings.Add("Text", chasse, "EtapeCount");
            NumIndice.DataBindings.Add("Text", chasse, "IndiceCount");
            XLabel.DataBindings.Add("Text", chasse, "X");
            YLabel.DataBindings.Add("Text", chasse, "Y");
            tb_name.DataBindings.Add("Text", chasse, "Pseudo");
            CbAutoPilot.DataBindings.Add("Checked", chasse, "IsAutoPilot");
        }

        private void Go_Click(object sender, EventArgs e)
        {
            if(btn_go.Text == "GO !")
            {
                chasse.IsPaused = false;
                this.btn_go.BackColor = Color.DarkRed;
                chasse.Launch();
                this.btn_go.Text = "STOP !";
            }
            else
            {
                chasse.IsPaused = true;
                this.btn_go.BackColor = Color.PaleGreen;
                this.btn_go.Text = "GO !";

                chasse.IndiceCount = 1;
                chasse.EtapeCount = 1;
                chasse.Indice = "1";
                chasse.X = "0";
                chasse.Y = "0";
                chasse.IsAutoPilot = true;
    }
        }
    }
}
