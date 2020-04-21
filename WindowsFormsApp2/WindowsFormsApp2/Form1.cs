using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

using Engine; 

namespace Farmio

{
    public partial class Farmio : Form
    {
       
        private Hero _hero;
        private int ClickX;
        private int ClickY;
        GameMap map = new GameMap();
        bool start = false;
        
        public Farmio()
        {
            map.ArrayGen();
            InitializeComponent();
            pbFon.Image = map.MapGen();
            Parentize();
            System.IO.Stream str = (System.IO.Stream)global::WindowsFormsApp2.Properties.Resources.ResourceManager.GetObject(SomeFunctions.MusicRandomize());
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(str);
            //player.Play();

            player.PlayLooping();

            pictureBox1.Image = (Bitmap)global::WindowsFormsApp2.Properties.Resources.ResourceManager.GetObject(SomeFunctions.MainPictureRandomize());
            _hero = new Hero();
            _hero.Gold = 0;
            _hero.Level = 1;
            lblGold.Text = _hero.Gold.ToString();
            lblEpoch.Text = _hero.Level.ToString();
        }

        private void Parentize()
        {
            pbFarmio.Parent = pbStart.Parent = pbLoad.Parent = pbExit.Parent = pbStart.Parent = pictureBox1;
            lblGold.Parent = lblEpoch.Parent = label1.Parent = label2.Parent = pbName.Parent = labelName.Parent = pbNameOk.Parent = pbFon;
        }

        private void pbStart_Click(object sender, EventArgs e)
        {
            this.Controls.Remove(pbStart);
            this.Controls.Remove(pictureBox1);
            
            //pbNameFon.Visible = true;
           // pbName.Parent = labelName.Parent = pbNameOk.Parent = pbNameFon; :(
            tbName.Visible = true;
            labelName.Visible = true;
            pbNameOk.Visible = true; 
            pbName.Visible = true;
        }

        private void pbExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pbNameOk_Click(object sender, EventArgs e)
        {
            string str = tbName.Text;
            if (str!="")
            {
                _hero.Name = str;
                pbName.Parent = labelName.Parent = pbNameOk.Parent = null;
                this.Controls.Remove(pbName);
                this.Controls.Remove(labelName);
                this.Controls.Remove(tbName);
                this.Controls.Remove(pbNameOk);
                start = true;

            }
        }

        private void pbFon_Click(object sender, EventArgs e)
        {
                if (start)
                {
 
                MouseEventArgs me = (MouseEventArgs)e;
                ClickX = me.X;
                ClickY = me.Y;
                string caseSwitch = map.formArray[ClickX, ClickY];

                switch (caseSwitch)
                {
                    case "g":
                    case "g1":
                    case "g2":
                    case "g3":
                    case "g4":
                    case "g5":
                    case "g6":
                    case "g7":
                    case "g8":
                        Console.WriteLine("zebrać nasiona");
                        break;
                    case "t1":
                    case "t2":
                    case "t3":
                    Console.WriteLine("wyciąć drzewo");
                    break;
                    case "s1":
                    case "s2":
                    case "s3":
                        Console.WriteLine("zdobyć kamień");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
