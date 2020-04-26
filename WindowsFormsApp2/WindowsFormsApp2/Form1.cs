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
            hero = new Hero();
            hero.SetSprite(1);
            InitializeComponent();
            pbHero.Image = (Image)hero.Sprite[4];
            pbHero.Location = new Point(hero.x, hero.y);

            pbFon.Image = map.DrawMap();

            Parentize();
            System.IO.Stream str = (System.IO.Stream)global::WindowsFormsApp2.Properties.Resources.ResourceManager.GetObject(SomeFunctions.MusicRandomize());
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(str);
            player.PlayLooping();

            pictureBox1.Image = (Bitmap)global::WindowsFormsApp2.Properties.Resources.ResourceManager.GetObject(SomeFunctions.MainPictureRandomize());
            lblGold.Text = hero.Gold.ToString();
            lblEpoch.Text = hero.Level.ToString();
            lblEnergy.Text = hero.Energy.ToString();
        }

        private void Parentize()
        {
            pbFarmio.Parent = pbStart.Parent = pbLoad.Parent = pbExit.Parent = pbStart.Parent = pictureBox1;
            pbHero.Parent = lblGold.Parent = lblEpoch.Parent = lblEnergy.Parent = label1.Parent = label2.Parent = label3.Parent = pbName.Parent = labelName.Parent = pbNameOk.Parent = pbCDTree.Parent = pbGetStone.Parent = pbCutGrass.Parent = pbCDStump.Parent = pbGetMushroom.Parent = pbGetMushrooms.Parent = pbFon;
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
            if (str != "")
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

        private void pbFon_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void pbFon_Click(object sender, MouseEventArgs e)
        {
            if (start)
            {
                ClickX = e.X;
                ClickY = e.Y;
                if (map.mapTab[ClickX, ClickY] != null)
                {
                    int x = map.mapTab[ClickX, ClickY].X;
                    int y = map.mapTab[ClickX, ClickY].Y;
                    Map.MapMainPoint tmp = (Map.MapMainPoint)map.mapTab[x, y];
                    if (tmp is Map.MapMainPoint.Grass)
                    {
                        pbCutGrass.Location = new Point(ClickX + 5, ClickY + 5);
                        pbCutGrass.Visible = true;
                        pbCDTree.Visible = pbGetStone.Visible = pbCDStump.Visible = pbGetMushrooms.Visible = pbGetMushroom.Visible = false;
                    }

                    else if (tmp is Map.MapMainPoint.Stone)
                    {
                        pbGetStone.Location = new Point(ClickX + 5, ClickY + 5);
                        pbGetStone.Visible = true;
                        pbCutGrass.Visible = pbCDTree.Visible = pbCDStump.Visible = pbGetMushrooms.Visible = pbGetMushroom.Visible = false;
                    }

                    else if (tmp is Map.MapMainPoint.Tree)
                    {
                        pbCDTree.Location = new Point(ClickX + 5, ClickY + 5);
                        pbCDTree.Visible = true;
                        pbCutGrass.Visible = pbGetStone.Visible = pbCDStump.Visible = pbGetMushrooms.Visible = pbGetMushroom.Visible = false;
                    }

                    else if (tmp is Map.MapMainPoint.Stump)
                    {
                        pbCDStump.Location = new Point(ClickX + 5, ClickY + 5);
                        pbCDStump.Visible = true;
                        pbCutGrass.Visible = pbCDTree.Visible = pbGetStone.Visible = pbGetMushrooms.Visible = pbGetMushroom.Visible = false;
                    }

                    else if (tmp is Map.MapMainPoint.Mushroom)
                    {
                        if (tmp.Name[2] == '1')
                        {
                            pbGetMushroom.Location = new Point(ClickX + 5, ClickY + 5);
                            pbGetMushroom.Visible = true;
                            pbCutGrass.Visible = pbCDTree.Visible = pbGetStone.Visible = pbGetMushrooms.Visible = pbCDStump.Visible = false;
                        }

                        else
                        {
                            pbGetMushrooms.Location = new Point(ClickX + 5, ClickY + 5);
                            pbGetMushrooms.Visible = true;
                            pbCutGrass.Visible = pbCDTree.Visible = pbGetStone.Visible = pbGetMushroom.Visible = pbCDStump.Visible = false;
                        }
                    }
                }

                else
                {
                    pbCutGrass.Visible = pbCDTree.Visible = pbGetStone.Visible = pbCDStump.Visible = pbGetMushrooms.Visible = pbGetMushroom.Visible = false;
                    HeroMoveHere(ClickX, ClickY);
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

        private void pbGetMushrooms_Click(object sender, EventArgs e)
        {
            pbGetMushrooms.Visible = false;
            if (hero.Energy <= 0)
                Console.WriteLine("Musisz cos zjesc i odpoczac :(");
            else
            {
                int x = map.mapTab[ClickX, ClickY].X;
                int y = map.mapTab[ClickX, ClickY].Y;
                Map.MapMainPoint.Mushroom tmp = (Map.MapMainPoint.Mushroom)map.mapTab[x, y];
                int EnergyTmp = hero.Energy;
                EnergyTmp -= tmp.Weight;
                if (EnergyTmp < 0) Console.WriteLine("Musisz cos zjesc i odpoczac :(");
                else
                {
                    hero.Energy = EnergyTmp;
                    if (tmp.Name[1] == 'n')
                        hero.addToInventory(tmp.DestroyMushroomNE(map));
                    else
                        hero.addToInventory(tmp.DestroyMushroom(map));
                    pbFon.Image = map.DrawMap();
                    lblEnergy.Text = hero.Energy.ToString();
                }
            }
        }

        private void pbGetMushroom_Click(object sender, EventArgs e)
        {
            pbGetMushroom.Visible = false;
            if (hero.Energy <= 0)
                Console.WriteLine("Musisz cos zjesc i odpoczac :(");
            else
            {
                int x = map.mapTab[ClickX, ClickY].X;
                int y = map.mapTab[ClickX, ClickY].Y;
                Map.MapMainPoint.Mushroom tmp = (Map.MapMainPoint.Mushroom)map.mapTab[x, y];
                int EnergyTmp = hero.Energy;
                EnergyTmp -= tmp.Weight;
                if (EnergyTmp < 0) Console.WriteLine("Musisz cos zjesc i odpoczac :(");
                else
                {
                    hero.Energy = EnergyTmp;
                    if (tmp.Name[1] == 'n')
                        hero.addToInventory(tmp.DestroyMushroomNE(map));
                    else
                        hero.addToInventory(tmp.DestroyMushroom(map));
                    pbFon.Image = map.DrawMap();
                    lblEnergy.Text = hero.Energy.ToString();
                }
            }
        }

        private void tbName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                str = tbName.Text;
                if (str != "")
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
        }

        private async void HeroMoveHere(int x, int y)
        {
            //pbHero.Visible = false;
            int i = 0;
            if (Math.Abs(hero.x - x) > Math.Abs(hero.y - y))
            {
                while (Math.Abs(hero.x - x) / Math.Abs(hero.y - y) >= 1.5)
                {
                    if (hero.x > x)
                    {
                        if (i > 8 || i < 6)
                            i = 6;
                        pbHero.Image = hero.Sprite[i];
                        hero.x -= 6;
                        i++;
                    }
                    else
                    {
                        if (i > 10 || i < 9)
                            i = 9;
                        pbHero.Image = hero.Sprite[i];
                        hero.x += 6;
                        i++;
                    }
                    pbHero.Left = hero.x;
                    await Task.Delay(40);
                }
                    

                    while (Math.Abs(hero.y - y) > 6)
                    {
                        if (hero.y > y)
                        {
                            if (i > 2)
                                i = 0;
                            hero.y -= 6;
                            pbHero.Image = hero.Sprite[i];
                            i++;
                        }
                        else
                        {
                            if (i > 5 || i < 2)
                                i = 3;
                            pbHero.Image = hero.Sprite[i];
                            hero.y += 6;
                            i++;

                        }
                        pbHero.Top = hero.y;
                        await Task.Delay(40);
                    }

                while (Math.Abs(hero.x - x) > 6)
                {
                    if (hero.x > x)
                    {
                        if (i > 8 || i < 6)
                            i = 6;
                        pbHero.Image = hero.Sprite[i];
                        hero.x -= 6;
                        i++;
                    }
                    else
                    {
                        if (i > 10 || i < 9)
                            i = 9;
                        pbHero.Image = hero.Sprite[i];
                        hero.x += 6;
                        i++;
                    }
                    pbHero.Left = hero.x;
                    await Task.Delay(40);
                }
            }

            else
            {
                    while (Math.Abs(hero.y - y) / Math.Abs(hero.x - x) >= 1.5)
                    {
                        if (hero.y > y)
                        {
                            if (i > 2)
                                i = 0;
                            hero.y -= 6;
                            pbHero.Image = hero.Sprite[i];
                            i++;
                        }
                        else
                        {
                            if (i > 5 || i < 2)
                                i = 3;
                            pbHero.Image = hero.Sprite[i];
                            hero.y += 6;
                            i++;

                        }
                        pbHero.Top = hero.y;
                        await Task.Delay(40);
                    }

                while (Math.Abs(hero.x - x) > 6)
                {
                    if (hero.x > x)
                    {
                        if (i > 8 || i < 6)
                            i = 6;
                        pbHero.Image = hero.Sprite[i];
                        hero.x -= 6;
                        i++;
                    }
                    else
                    {
                        if (i > 10 || i < 9)
                            i = 9;
                        pbHero.Image = hero.Sprite[i];
                        hero.x += 6;
                        i++;
                    }
                    pbHero.Left = hero.x;
                    await Task.Delay(40);
                }

                while (Math.Abs(hero.y - y) > 6)
                {
                    if (hero.y > y)
                    {
                        if (i > 2)
                            i = 0;
                        hero.y -= 6;
                        pbHero.Image = hero.Sprite[i];
                        i++;
                    }
                    else
                    {
                        if (i > 5 || i < 2)
                            i = 3;
                        pbHero.Image = hero.Sprite[i];
                        hero.y += 6;
                        i++;

                    }
                    pbHero.Top = hero.y;
                    await Task.Delay(40);
                }

            }

            Console.WriteLine("hi there");
        }
        //private void Form1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.W)
        //    {
        //        hero.Move('w');
        //    }
        //    else if (e.KeyCode == Keys.A)
        //    {
        //        hero.Move('a');
        //    }
        //    else if (e.KeyCode == Keys.S)
        //    {
        //        hero.Move('s');
        //    }
        //    else if (e.KeyCode == Keys.D)
        //    {
        //        hero.Move('d');
        //    }
        //    else if (e.KeyCode == Keys.E)
        //    {
        //        hero.Move('e');
        //    }
        //    pbHero.Left = hero.x;
        //    //pbHero.Top = hero.y;
        //}
    }
}
