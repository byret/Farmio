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
        string str;
        
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
            lblGold.Parent = lblEpoch.Parent = label1.Parent = label2.Parent = pbName.Parent = labelName.Parent = pbNameOk.Parent = pbCDTree.Parent = pbGetStone.Parent = pbCutGrass.Parent = pbFon;
            ///pbCDTree.BackgroundImage = pbFon.Image;
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
            str = tbName.Text;
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
                str = map.formArray[ClickX, ClickY];
                if(str != null)
                {
                    string caseSwitch = str.Substring(0, 2);

                    switch (caseSwitch)
                    {
                        case "g1":
                        case "g2":
                        case "g3":
                        case "g4":
                        case "g5":
                        case "g6":
                        case "g7":
                        case "g8":
                        case "g9":
                            pbCutGrass.Location = new Point(ClickX + 5, ClickY + 5);
                            pbCutGrass.Visible = true;
                            break;
                        case "t1":
                        case "t2":
                        case "t3":
                            pbCDTree.Location = new Point (ClickX + 5, ClickY + 5);
                            pbCDTree.Visible = true;
                            break;
                        case "s1":
                        case "s2":
                        case "s3":
                            pbGetStone.Location = new Point(ClickX + 5, ClickY + 5);
                            pbGetStone.Visible = true;
                            break;
                        default:
                            break;
                    }
                }

            }
        }

        public void CutGrass(int x, int y, string st)
        {
            int index = st.IndexOf("-");
            st = st.Substring(0, 2);
            string strX = (map.formArray[x, y]).Substring(2, index - 2);
            string strY = (map.formArray[x, y]).Substring(index + 1);
            int X = Int32.Parse(strX);
            int Y = Int32.Parse(strY);

            int h = 0, w = 0;
            if (st == "g1")
            {
                w = 11;
                h = 8;
            }
            else if (st == "g2")
            {
                w = 10;
                h = 8;
            }
            else if (st == "g3")
            {
                w = 16;
                h = 14;
            }
            else if (st == "g4")
            {
                w = 13;
                h = 12;
            }
            else if (st == "g5")
            {
                w = 10;
                h = 13;
            }
            else if (st == "g6")
            {
                w = 12;
                h = 11;
            }
            else if (st == "g7")
            {
                w = 13;
                h = 15;
            }
            else if (st == "g8")
            {
                w = 15;
                h = 13;
            }
            else if (st == "g9")
            {
                w = 25;
                h = 21;
            }

            pbFon.Image = map.MapCutGrass(X, Y, h, w, (Bitmap)pbFon.Image);
        }


        public void GetStone(int x, int y, string st)
        {
            int index = st.IndexOf("-");
            st = st.Substring(0, 2);
            string strX = (map.formArray[x, y]).Substring(2, index - 2);
            string strY = (map.formArray[x, y]).Substring(index + 1);
            int X = Int32.Parse(strX);
            int Y = Int32.Parse(strY);

            int h = 0, w = 0;
            if (st == "s1")
            {
                w = 28;
                h = 16;
            }
            else if (st == "s2")
            {
                w = 24;
                h = 16;
            }
            else if (st == "s3")
            {
                w = 46;
                h = 39;
            }

            pbFon.Image = map.MapGetStone(X, Y, h, w, (Bitmap)pbFon.Image);
        }


        public void CutDTree(int x, int y, string st)
        {
            int index = st.IndexOf("-");
            st = st.Substring(0, 2);
            string strX = (map.formArray[x, y]).Substring(2, index - 2);
            string strY = (map.formArray[x, y]).Substring(index+1);
            int X = Int32.Parse(strX);
            int Y = Int32.Parse(strY);
            int h = 0, w = 0;
            if (st == "t1")
            {
                w = 64;
                h = 95;
            }
            else if (st == "t2")
            {
                w = 112;
                h = 96;
            }
            else if (st == "t3")
            {
                w = 69;
                h = 111;
            }

            pbFon.Image = map.MapCutDTree(X, Y, h, w, (Bitmap)pbFon.Image);
        }

        private void pbCDTree_Click(object sender, EventArgs e)
        {
            pbCDTree.Visible = false;
            CutDTree(ClickX, ClickY, str);
        }

        private void pbGetStone_Click(object sender, EventArgs e)
        {
            pbGetStone.Visible = false;
            GetStone(ClickX, ClickY, str);
        }

        private void pbCutGrass_Click(object sender, EventArgs e)
        {
            pbCutGrass.Visible = false;
            CutGrass(ClickX, ClickY, str);
        }
    }
}
