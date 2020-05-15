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
        System.Media.SoundPlayer player;

        public Farmio()
        {
            map.MapGeneration(map);
            hero = new Hero();
            hero.SetSprite(1);

            InitializeComponent();

            System.Windows.Forms.Timer musicTimer = new System.Windows.Forms.Timer();
            musicTimer.Interval = 300000; 
            musicTimer.Tick += new EventHandler(musicTimer_Tick);
            musicTimer.Start();

            pbFon.Image = map.DrawMap();

            Parentize();

            pbHero.Image = (Image)hero.Sprite[4];
            pbHero.Location = new Point(hero.x, hero.y);
            System.IO.Stream str = (System.IO.Stream)global::WindowsFormsApp2.Properties.Resources.ResourceManager.GetObject(SomeFunctions.MusicRandomize());
            pictureBox1.Image = (Bitmap)global::WindowsFormsApp2.Properties.Resources.ResourceManager.GetObject(SomeFunctions.MainPictureRandomize());
            player = new System.Media.SoundPlayer(str);
            player.Play();
            lblGold.Text = hero.Gold.ToString();
            lblEpoch.Text = hero.Level.ToString();
            lblEnergy.Text = hero.Energy.ToString();
        }

        private void Parentize()
        {
            pbFarmio.Parent = pbStart.Parent = pbLoad.Parent = pbExit.Parent = pbStart.Parent = pictureBox1;
            pbName.Parent = pbCDTree.Parent = pbGetStone.Parent = pbCutGrass.Parent = pbCDStump.Parent = pbGetMushroom.Parent = pbGetMushrooms.Parent = pbNameOk.Parent = pbGLEF.Parent = pbFon;
            lblGold.Parent = lblEpoch.Parent = lblEnergy.Parent = label1.Parent = label2.Parent = label3.Parent = pbGLEF;// = pbFon;
            pbHero.Parent = pbFon;
        }

        void musicTimer_Tick(object sender, EventArgs e)
        {
            System.IO.Stream str = (System.IO.Stream)global::WindowsFormsApp2.Properties.Resources.ResourceManager.GetObject(SomeFunctions.MusicRandomize());
            player = new System.Media.SoundPlayer(str);
            player.Play();
        }

        public partial class TransparentPictureBox : PictureBox
        {
            public TransparentPictureBox()
            {
                this.BackColor = Color.Transparent;
            }
            protected override void OnPaint(PaintEventArgs e)
            {
                if (Parent != null && this.BackColor == Color.Transparent)
                {
                    using (var bmp = new Bitmap(Parent.Width, Parent.Height))
                    {
                        Parent.Controls.Cast<Control>()
                              .Where(c => Parent.Controls.GetChildIndex(c) > Parent.Controls.GetChildIndex(this))
                              .Where(c => c.Bounds.IntersectsWith(this.Bounds))
                              .OrderByDescending(c => Parent.Controls.GetChildIndex(c))
                              .ToList()
                              .ForEach(c => c.DrawToBitmap(bmp, c.Bounds));

                        e.Graphics.DrawImage(bmp, -Left, -Top);
                    }
                }
                base.OnPaint(e);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            string str = "";
            Image imag = Image.FromFile(str);
            e.Graphics.DrawImage(imag, new Point(0, 0));
        }

        private void pbStart_Click(object sender, EventArgs e)
        {
            this.Controls.Remove(pbStart);
            this.Controls.Remove(pictureBox1);
            pbGLEF.Visible = true;
            tbName.Visible = true;
            pbNameOk.Visible = true;
            pbName.Visible = true;
            pbHero.Visible = true;
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
                pbName.Parent = pbNameOk.Parent = null;
                this.Controls.Remove(pbName);
                this.Controls.Remove(tbName);
                this.Controls.Remove(pbNameOk);
                start = true;
            }
        }


        private void pbFon_MouseDown(object sender, MouseEventArgs e)
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

        private async void pbCDTree_Click(object sender, EventArgs e)
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
                    Task<int> longRunningTask = HeroMoveHere(x, y);
                    int result = await longRunningTask;
                    hero.Energy = EnergyTmp;
                    hero.addToInventory(tmp.DestroyTree(map));
                    pbFon.Image = map.DrawMap();
                    lblEnergy.Text = hero.Energy.ToString();
                }
            }
        }

        private async void pbGetStone_Click(object sender, EventArgs e)
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
                    Task<int> longRunningTask = HeroMoveHere(x, y);
                    int result = await longRunningTask;
                    hero.Energy = EnergyTmp;
                    hero.addToInventory(tmp.DestroyStone(map));
                    pbFon.Image = map.DrawMap();
                    lblEnergy.Text = hero.Energy.ToString();
                }
            }
        }

        private async void pbCutGrass_Click(object sender, EventArgs e)
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
                    Task<int> longRunningTask = HeroMoveHere(x, y);
                    int result = await longRunningTask;
                    hero.Energy = EnergyTmp;
                    hero.addToInventory(tmp.DestroyGrass(map));
                    pbFon.Image = map.DrawMap();
                    lblEnergy.Text = hero.Energy.ToString();
                }
            }
        }

        private async void pbCDStump_Click(object sender, EventArgs e)
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
                    Task<int> longRunningTask = HeroMoveHere(x, y);
                    int result = await longRunningTask;
                    hero.Energy = EnergyTmp;
                    hero.addToInventory(tmp.DestroyStump(map));
                    pbFon.Image = map.DrawMap();
                    lblEnergy.Text = hero.Energy.ToString();
                }
            }
        }

        private async void pbGetMushrooms_Click(object sender, EventArgs e)
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
                    Task<int> longRunningTask = HeroMoveHere(x, y);
                    int result = await longRunningTask;

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

        private async void pbGetMushroom_Click(object sender, EventArgs e)
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
                    Task<int> longRunningTask = HeroMoveHere(x, y);
                    int result = await longRunningTask;
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
                    pbName.Parent = pbNameOk.Parent = null;
                    this.Controls.Remove(pbName);
                    this.Controls.Remove(tbName);
                    this.Controls.Remove(pbNameOk);
                    start = true;

                }
            }
        }

        public async Task<int> HeroMoveHere(int x, int y)
        {
            //Console.Clear();
            int i = 0;
            while (hero.x-x>5)
            {
                while (!map.isFree(hero.x, hero.y, 'a'))
                {
                    int min1 = 0, min2 = 0;
                    while(!map.isFree(hero.x, hero.y - min1, 'a'))
                        min1++;
                    while (!map.isFree(hero.x, hero.y + min2, 'a')) 
                        min2++;
                    int posY = hero.y;
                    if (min1 > min2)
                        while (hero.y < posY + min2)
                        {
                            hero.y += 5;
                            pbHero.Top = hero.y;
                            await Task.Delay(40);
                        }    
                    else
                        while (hero.y > posY - min2)
                        {
                            hero.y -= 5;
                            pbHero.Top = hero.y;
                            await Task.Delay(40);
                        }
                }

                    hero.x -= 5;
                pbHero.Left = hero.x;
                await Task.Delay(35);
            }
            while (hero.x - x < -5)
            {
                while (!map.isFree(hero.x, hero.y, 'd'))
                {
                    int min1 = 0, min2 = 0;
                    while (!map.isFree(hero.x, hero.y - min1, 'd'))
                        min1++;
                    while (!map.isFree(hero.x, hero.y + min2, 'd'))
                        min2++;
                    int posY = hero.y;
                    if (min1 > min2)
                        while (hero.y < posY + min2)
                        {
                            hero.y += 5;
                            pbHero.Top = hero.y;
                            await Task.Delay(40);
                        }
                    else
                        while (hero.y > posY - min2)
                        {
                            hero.y -= 5;
                            pbHero.Top = hero.y;
                            await Task.Delay(40);
                        }
                }
                hero.x += 5;
                pbHero.Left = hero.x;
                await Task.Delay(35);
            }
            while (hero.y - y > 5)
            {
                while (!map.isFree(hero.x, hero.y, 'w'))
                {
                    int min1 = 0, min2 = 0;
                    while (!map.isFree(hero.x - min1, hero.y, 'w'))
                        min1++;
                    while (!map.isFree(hero.x + min2, hero.y, 'w'))
                        min2++;
                    int posX = hero.x;
                    if (min1 > min2)
                        while (hero.x < posX + min2)
                        {
                            hero.x += 5;
                            pbHero.Left = hero.x;
                            await Task.Delay(40);
                        }
                    else
                        while (hero.x > posX - min2)
                        {
                            hero.y -= 5;
                            pbHero.Left = hero.x;
                            await Task.Delay(40);
                        }
                }
                hero.y -= 5;
                pbHero.Top = hero.y;
                await Task.Delay(35);
            }
            while (hero.y - y < -5)
            {
                while (!map.isFree(hero.x, hero.y, 's'))
                {
                    int min1 = 0, min2 = 0;
                    while (!map.isFree(hero.x - min1, hero.y, 's'))
                        min1++;
                    while (!map.isFree(hero.x + min2, hero.y, 's'))
                        min2++;
                    int posX = hero.x;
                    if (min1 > min2)
                        while (hero.x < posX + min2)
                        {
                            hero.x += 5;
                            pbHero.Left = hero.x;
                            await Task.Delay(40);
                        }
                    else
                        while (hero.x > posX - min2)
                        {
                            hero.y -= 5;
                            pbHero.Left = hero.x;
                            await Task.Delay(40);
                        }
                }
                hero.y += 5;
                pbHero.Top = hero.y;
                await Task.Delay(35);
            }

            return 0;
            //if (Math.Abs(hero.x - x) > Math.Abs(hero.y - y))
            //{
            //    while ((hero.y - y != 0) && Math.Abs(hero.x - x) / Math.Abs(hero.y - y) >= 2 )
            //    {
            //        if (hero.x > x)
            //        {
            //            if (map.isFree(hero.x, hero.y, 'a'))
            //            {
            //                if (i > 8 || i < 6)
            //                    i = 6;

            //                pbHero.Image = hero.Sprite[i];
            //                hero.x -= 5;
            //                i++;
            //            }
            //            else
            //            while (!map.isFree(hero.x, hero.y, 'a'))
            //                {
            //                if (hero.y > y)
            //                {
            //                    hero.y -= 25;
            //                    pbHero.Image = hero.Sprite[0];
            //                }
            //                else
            //                {
            //                    hero.y += 25;
            //                    pbHero.Image = hero.Sprite[3];
            //                }
            //                    pbHero.Top = hero.y;
            //                }
            //        }
            //        else
            //        {
            //            if (map.isFree(hero.x, hero.y, 'd'))
            //            {
            //                if (i > 11 || i < 9)
            //                    i = 9;
            //                pbHero.Image = hero.Sprite[i];
            //                hero.x += 5;
            //                i++;

            //            }

            //            else
            //            while (!map.isFree(hero.x, hero.y, 'd'))
            //                {
            //                if (hero.y > y)
            //                {
            //                    hero.y -= 25;
            //                    pbHero.Image = hero.Sprite[0];
            //                }
            //                else
            //                {
            //                    hero.y += 25;
            //                    pbHero.Image = hero.Sprite[3];
            //                }
            //                    pbHero.Top = hero.y;
            //                }
            //        }
            //        pbHero.Left = hero.x;
            //        await Task.Delay(35);
            //    }


            //    while (Math.Abs(hero.y - y) > 5)
            //    {
            //        if (hero.y > y)
            //        {
            //            if (map.isFree(hero.x, hero.y, 'w'))
            //            {
            //                if (i > 2)
            //                    i = 0;
            //                hero.y -= 5;
            //                pbHero.Image = hero.Sprite[i];
            //                i++;
            //            }
            //            else
            //            while (!map.isFree(hero.x, hero.y, 'w'))
            //                {
            //                if (hero.x > x)
            //                {
            //                    hero.x -= 25;
            //                    pbHero.Image = hero.Sprite[6];
            //                }
            //                else
            //                {
            //                    hero.x += 25;
            //                    pbHero.Image = hero.Sprite[9];
            //                }
            //                    pbHero.Left = hero.x;
            //                }
            //        }
            //        else
            //        {
            //            if (map.isFree(hero.x, hero.y, 's'))
            //            {
            //                if (i > 5 || i < 2)
            //                    i = 3;
            //                pbHero.Image = hero.Sprite[i];
            //                hero.y += 5;
            //                i++;
            //            }
            //            else
            //            while (!map.isFree(hero.x, hero.y, 's'))
            //                {
            //                if (hero.x > x)
            //                {
            //                    hero.x -= 25;
            //                    pbHero.Image = hero.Sprite[6];
            //                }
            //                else
            //                {
            //                    hero.x += 25;
            //                    pbHero.Image = hero.Sprite[9];
            //                }
            //                    pbHero.Left = hero.x;
            //                }
            //        }
            //        pbHero.Top = hero.y;
            //        await Task.Delay(35);
            //    }

            //    while (Math.Abs(hero.x - x) > 5)
            //    {
            //        if (hero.x > x)
            //        {
            //            if (map.isFree(hero.x, hero.y, 'a'))
            //            {
            //                if (i > 8 || i < 6)
            //                    i = 6;
            //                pbHero.Image = hero.Sprite[i];
            //                hero.x -= 5;
            //                i++;
            //            }
            //            else
            //            while (!map.isFree(hero.x, hero.y, 'a'))
            //                {
            //                if (hero.y > y)
            //                {
            //                    hero.y -= 25;
            //                    pbHero.Image = hero.Sprite[0];
            //                }
            //                else
            //                {
            //                    hero.y += 25;
            //                    pbHero.Image = hero.Sprite[3];
            //                }
            //                    pbHero.Top = hero.y;
            //                }
            //        }
            //        else
            //        {
            //            if (map.isFree(hero.x, hero.y, 'd'))
            //            {
            //                if (i > 11 || i < 9)
            //                    i = 9;
            //                pbHero.Image = hero.Sprite[i];
            //                hero.x += 5;
            //                i++;
            //            }
            //            else
            //            while (!(map.isFree(hero.x, hero.y, 'd')))
            //            {
            //                if (hero.y > y)
            //                {
            //                    hero.y -= 25;
            //                    pbHero.Image = hero.Sprite[0];
            //                }
            //                else
            //                {
            //                    hero.y += 25;
            //                    pbHero.Image = hero.Sprite[3];
            //                }
            //                    pbHero.Top = hero.y;
            //                }
            //        }
            //        pbHero.Left = hero.x;
            //        await Task.Delay(35);
            //    }
            //}

            //else
            //{
            //    while (hero.x != x &&Math.Abs(hero.y - y) / Math.Abs(hero.x - x) >= 2 && (hero.x - x != 0))
            //    {
            //        if (hero.y > y)
            //        {
            //            if (map.isFree(hero.x, hero.y, 'w'))
            //            {
            //                if (i > 2)
            //                    i = 0;
            //                hero.y -= 5;
            //                pbHero.Image = hero.Sprite[i];
            //                i++;
            //            }
            //            else
            //                while (!map.isFree(hero.x, hero.y, 'w'))
            //                {
            //                    if (hero.x > x)
            //                    {
            //                        hero.x -= 25;
            //                        pbHero.Image = hero.Sprite[6];
            //                    }
            //                    else
            //                    {
            //                        hero.x += 25;
            //                        pbHero.Image = hero.Sprite[9];
            //                    }
            //                    pbHero.Left = hero.x;
            //                }
            //        }
            //        else
            //        {
            //            if (map.isFree(hero.x, hero.y, 's'))
            //            {
            //                if (i > 5 || i < 2)
            //                    i = 3;
            //                pbHero.Image = hero.Sprite[i];
            //                hero.y += 5;
            //                i++;
            //            }
            //            else
            //                while (!map.isFree(hero.x, hero.y, 'w'))
            //                {
            //                    if (hero.x > x)
            //                    {
            //                        hero.x -= 25;
            //                        pbHero.Image = hero.Sprite[6];
            //                    }
            //                    else
            //                    {
            //                        hero.x += 25;
            //                        pbHero.Image = hero.Sprite[9];
            //                    }
            //                    pbHero.Left = hero.x;
            //                }

            //        }
            //        pbHero.Top = hero.y;
            //        await Task.Delay(35);
            //    }

            //    while (Math.Abs(hero.x - x) > 5)
            //    {
            //        if (hero.x > x)
            //        {
            //            if (map.isFree(hero.x, hero.y, 'a'))
            //            {
            //                if (i > 8 || i < 6)
            //                    i = 6;
            //                pbHero.Image = hero.Sprite[i];
            //                hero.x -= 5;
            //                i++;
            //            }
            //            else
            //                while (!(map.isFree(hero.x, hero.y, 'd')))
            //                {
            //                    if (hero.y > y)
            //                    {
            //                        hero.y -= 25;
            //                        pbHero.Image = hero.Sprite[0];
            //                    }
            //                    else
            //                    {
            //                        hero.y += 25;
            //                        pbHero.Image = hero.Sprite[3];
            //                    }
            //                    pbHero.Top = hero.y;
            //                }
            //        }
            //        else
            //        {
            //            if (map.isFree(hero.x, hero.y, 'd'))
            //            {
            //                if (i > 11 || i < 9)
            //                    i = 9;
            //                pbHero.Image = hero.Sprite[i];
            //                hero.x += 5;
            //                i++;
            //            }
            //            else
            //                while (!(map.isFree(hero.x, hero.y, 'd')))
            //                {
            //                    if (hero.y > y)
            //                    {
            //                        hero.y -= 25;
            //                        pbHero.Image = hero.Sprite[0];
            //                    }
            //                    else
            //                    {
            //                        hero.y += 25;
            //                        pbHero.Image = hero.Sprite[3];
            //                    }
            //                    pbHero.Top = hero.y;
            //                }
            //        }
            //        pbHero.Left = hero.x;
            //        await Task.Delay(35);
            //    }

            //    while (Math.Abs(hero.y - y) > 5)
            //    {
            //        if (hero.y > y)
            //        {
            //            if (map.isFree(hero.x, hero.y, 'w'))
            //            {
            //                if (i > 2)
            //                    i = 0;
            //                hero.y -= 5;
            //                pbHero.Image = hero.Sprite[i];
            //                i++;
            //            }
            //            else
            //                while (!map.isFree(hero.x, hero.y, 'w'))
            //                {
            //                    if (hero.x > x)
            //                    {
            //                        hero.x -= 25;
            //                        pbHero.Image = hero.Sprite[6];
            //                    }
            //                    else
            //                    {
            //                        hero.x += 25;
            //                        pbHero.Image = hero.Sprite[9];
            //                    }
            //                    pbHero.Left = hero.x;
            //                }
            //        }
            //        else
            //        {
            //            if (map.isFree(hero.x, hero.y, 's'))
            //            {
            //                if (i > 5 || i < 2)
            //                    i = 3;
            //                pbHero.Image = hero.Sprite[i];
            //                hero.y += 5;
            //                i++;
            //            }
            //            else
            //                while (!map.isFree(hero.x, hero.y, 'w'))
            //                {
            //                    if (hero.x > x)
            //                    {
            //                        hero.x -= 25;
            //                        pbHero.Image = hero.Sprite[6];
            //                    }
            //                    else
            //                    {
            //                        hero.x += 25;
            //                        pbHero.Image = hero.Sprite[9];
            //                    }
            //                    pbHero.Left = hero.x;
            //                }
            //        }
            //        pbHero.Top = hero.y;
            //        await Task.Delay(35);
            //    }

            //}
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.W)
                {
                    hero.Move('w');
                }
                else if (e.KeyCode == Keys.A)
                {
                    hero.Move('a');
                }
                else if (e.KeyCode == Keys.S)
                {
                    hero.Move('s');
                }
                else if (e.KeyCode == Keys.D)
                {
                    hero.Move('d');
                }
                else if (e.KeyCode == Keys.E)
                {
                    hero.Move('e');
                }
                pbHero.Left = hero.x;
                pbHero.Top = hero.y;
            }


    }
}
