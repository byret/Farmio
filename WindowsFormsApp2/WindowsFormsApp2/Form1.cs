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
       
        private Hero hero;
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
            hero = new Hero();
            hero.Gold = 0;
            hero.Level = 1;
            hero.Energy = 200;
            lblGold.Text = hero.Gold.ToString();
            lblEpoch.Text = hero.Level.ToString();
            lblEnergy.Text = hero.Energy.ToString();
        }

        private void Parentize()
        {
            pbFarmio.Parent = pbStart.Parent = pbLoad.Parent = pbExit.Parent = pbStart.Parent = pictureBox1;
            lblGold.Parent = lblEpoch.Parent = lblEnergy.Parent = label1.Parent = label2.Parent = label3.Parent = pbName.Parent = labelName.Parent = pbNameOk.Parent = pbCDTree.Parent = pbGetStone.Parent = pbCutGrass.Parent = pbCDStump.Parent = pbFon;
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
                hero.Name = str;
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
                        pbCDStump.Visible = false;
                    }

                    else if (map.mapTab[x, y] is Map.MapMainPoint.Stone)
                    {
                        pbGetStone.Location = new Point(ClickX + 5, ClickY + 5);
                        pbGetStone.Visible = true;
                        pbCDTree.Visible = false;
                        pbCutGrass.Visible = false;
                        pbCDStump.Visible = false;
                    }

                    else if (map.mapTab[x, y] is Map.MapMainPoint.Tree)
                    {
                        pbCDTree.Location = new Point(ClickX + 5, ClickY + 5);
                        pbCDTree.Visible = true;
                        pbGetStone.Visible = false;
                        pbCutGrass.Visible = false;
                        pbCDStump.Visible = false;
                    }

                    else if (map.mapTab[x, y] is Map.MapMainPoint.Stump)
                    {
                        pbCDStump.Location = new Point(ClickX + 5, ClickY + 5);
                        pbCDStump.Visible = true;
                        pbGetStone.Visible = false;
                        pbCutGrass.Visible = false;
                        pbCDTree.Visible = false;
                    }
                }

                else
                {
                    pbCutGrass.Visible = pbCDTree.Visible = pbGetStone.Visible = pbCDStump.Visible = false;
                }
            }
        }

        private void pbCDTree_Click(object sender, EventArgs e)
        {
            pbCDTree.Visible = false;
            if (hero.Energy <= 0)
                Console.WriteLine("Musisz cos zjesc i odpoczac :(");
            else
            {
                int x = map.mapTab[ClickX, ClickY].X;
                int y = map.mapTab[ClickX, ClickY].Y;
                Map.MapMainPoint.Tree tmp = (Map.MapMainPoint.Tree)map.mapTab[x, y];
                int EnergyTmp = hero.Energy;
                EnergyTmp -= tmp.Weight;
                if (EnergyTmp < 0) Console.WriteLine("Musisz cos zjesc i odpoczac :(");
                else
                {
                    hero.Energy = EnergyTmp;
                    hero.addToInventory(tmp.DestroyTree(map));
                    pbFon.Image = map.DrawMap();
                    lblEnergy.Text = hero.Energy.ToString();
                }
            }
        }

        private void pbGetStone_Click(object sender, EventArgs e)
        {
            pbGetStone.Visible = false;
            if (hero.Energy <= 0)
                Console.WriteLine("Musisz cos zjesc i odpoczac :(");
            else
            {
                int x = map.mapTab[ClickX, ClickY].X;
                int y = map.mapTab[ClickX, ClickY].Y;
                Map.MapMainPoint.Stone tmp = (Map.MapMainPoint.Stone)map.mapTab[x, y];
                int EnergyTmp = hero.Energy;
                EnergyTmp -= tmp.Weight;
                if (EnergyTmp < 0) Console.WriteLine("Musisz cos zjesc i odpoczac :(");
                else
                {
                    hero.Energy = EnergyTmp;
                    hero.addToInventory(tmp.DestroyStone(map));
                    pbFon.Image = map.DrawMap();
                    lblEnergy.Text = hero.Energy.ToString();
                }
            }
        }

        private void pbCutGrass_Click(object sender, EventArgs e)
        {
            pbCutGrass.Visible = false;
            if (hero.Energy <= 0)
                Console.WriteLine("Musisz cos zjesc i odpoczac :(");
            else
            {
                int x = map.mapTab[ClickX, ClickY].X;
                int y = map.mapTab[ClickX, ClickY].Y;
                Map.MapMainPoint.Grass tmp = (Map.MapMainPoint.Grass)map.mapTab[x, y];
                int EnergyTmp = hero.Energy;
                EnergyTmp -= tmp.Weight;
                if (EnergyTmp < 0) Console.WriteLine("Musisz cos zjesc i odpoczac :(");
                else
                {
                    hero.Energy = EnergyTmp;
                    hero.addToInventory(tmp.DestroyGrass(map));
                    pbFon.Image = map.DrawMap();
                    lblEnergy.Text = hero.Energy.ToString();
                }
            }
        }

        private void pbCDStump_Click(object sender, EventArgs e)
        {
            pbCDStump.Visible = false;
            if (hero.Energy <= 0)
                Console.WriteLine("Musisz cos zjesc i odpoczac :(");
            else
            {
                int x = map.mapTab[ClickX, ClickY].X;
                int y = map.mapTab[ClickX, ClickY].Y;
                Map.MapMainPoint.Stump tmp = (Map.MapMainPoint.Stump)map.mapTab[x, y];
                int EnergyTmp = hero.Energy;
                EnergyTmp -= tmp.Weight;
                if (EnergyTmp < 0) Console.WriteLine("Musisz cos zjesc i odpoczac :(");
                else
                {
                    hero.Energy = EnergyTmp;
                    hero.addToInventory(tmp.DestroyStump(map));
                    pbFon.Image = map.DrawMap();
                    lblEnergy.Text = hero.Energy.ToString();
                }
            }
        }
    }
}
