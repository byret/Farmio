using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Engine; 

namespace Farmio
{
    public partial class Farmio : Form
    {
        private Hero _hero;

        public Farmio()
        {
            InitializeComponent();
            _hero = new Hero();
            _hero.Gold = 0;
            _hero.Level = 1;
            lblGold.Text = _hero.Gold.ToString();
            lblEpoch.Text = _hero.Level.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Controls.Remove(button1);
            this.Controls.Remove(pictureBox1);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
