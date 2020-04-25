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
    //    GameMap map = new GameMap();
        Map map = new Map();
        bool start = false;
        string str;
        
        public Farmio()
        {
            map.MapGeneration(map);
            InitializeComponent();
            pbFon.Image = map.DrawMap();
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
                if (map.mapTab[ClickX, ClickY] != null)
                {
                    int x = map.mapTab[ClickX, ClickY].X;
                    int y = map.mapTab[ClickX, ClickY].Y;
                    if (map.mapTab[x, y] is Map.MapMainPoint.Grass)
                    {
                        pbCutGrass.Location = new Point(ClickX + 5, ClickY + 5);
                        pbCutGrass.Visible = true;
                        pbCDTree.Visible = false;
                        pbGetStone.Visible = false;
                    }

                    else if (map.mapTab[x, y] is Map.MapMainPoint.Stone)
                    {
                        pbGetStone.Location = new Point(ClickX + 5, ClickY + 5);
                        pbGetStone.Visible = true;
                        pbCDTree.Visible = false;
                        pbCutGrass.Visible = false;
                    }

                    else if (map.mapTab[x, y] is Map.MapMainPoint.Tree)
                    {
                        pbCDTree.Location = new Point(ClickX + 5, ClickY + 5);
                        pbCDTree.Visible = true;
                        pbGetStone.Visible = false;
                        pbCutGrass.Visible = false;
                    }
                }

                else
                {
                    pbCutGrass.Visible = false;
                    pbCDTree.Visible = false;
                    pbGetStone.Visible = false;
                }
            }
        }

        private void pbCDTree_Click(object sender, EventArgs e)
        {
            pbCDTree.Visible = false;
            int x = map.mapTab[ClickX, ClickY].X;
            int y = map.mapTab[ClickX, ClickY].Y;
            Map.MapMainPoint.Tree tmp = (Map.MapMainPoint.Tree)map.mapTab[x, y];
            tmp.DestroyTree(map);
            pbFon.Image = map.DrawMap();
        }

        private void pbGetStone_Click(object sender, EventArgs e)
        {
            pbGetStone.Visible = false;
            int x = map.mapTab[ClickX, ClickY].X;
            int y = map.mapTab[ClickX, ClickY].Y;
            Map.MapMainPoint.Stone tmp = (Map.MapMainPoint.Stone)map.mapTab[x, y];
            tmp.DestroyStone(map);
            pbFon.Image = map.DrawMap();
        }

        private void pbCutGrass_Click(object sender, EventArgs e)
        {
            pbCutGrass.Visible = false;
            int x = map.mapTab[ClickX, ClickY].X;
            int y = map.mapTab[ClickX, ClickY].Y;
            Map.MapMainPoint.Grass tmp = (Map.MapMainPoint.Grass)map.mapTab[x, y];
            tmp.DestroyGrass(map);
            pbFon.Image = map.DrawMap();
        }

    }
}
