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
using System.Media;
using System.Windows.Media;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;

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
        Task<int> longRunningTask;
        List<Label> inventoryLabels = new List<Label>();
        // System.Windowl


        public Farmio()
        {
            map.MapGeneration(map);
            hero = new Hero();
            hero.SetSprite(1);

            InitializeComponent();
            longRunningTask = HeroMoveHere(hero.x, hero.y, 0);
            System.Windows.Forms.Timer musicTimer = new System.Windows.Forms.Timer();
            musicTimer.Interval = 300000; 
            musicTimer.Tick += new EventHandler(musicTimer_Tick);
            musicTimer.Start();

            inventoryLabels.Add(item1);
            inventoryLabels.Add(item2);
            inventoryLabels.Add(item3);
            inventoryLabels.Add(item4);
            inventoryLabels.Add(item5);
            inventoryLabels.Add(lblNumOfItem1);
            inventoryLabels.Add(lblNumOfItem2);
            inventoryLabels.Add(lblNumOfItem3);
            inventoryLabels.Add(lblNumOfItem4);
            inventoryLabels.Add(lblNumOfItem5);
            pbFon.Image = map.DrawMap();

            Parentize();

            pbHero.Image = (Image)hero.Sprite[4];
            pbHero.Location = new Point(hero.x, hero.y);
            pictureBox1.Image = (Bitmap)global::WindowsFormsApp2.Properties.Resources.ResourceManager.GetObject(SomeFunctions.MainPictureRandomize());
            string startupPath = System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "Resources\\", SomeFunctions.MusicRandomize()) ;
            //SoundPlay(startupPath);
            lblGold.Text = hero.Gold.ToString();
            lblEpoch.Text = hero.Level.ToString();
            lblEnergy.Text = hero.Energy.ToString();

        }

        private void SoundPlay(string audioPath)
        {
            MediaPlayer myPlayer = new MediaPlayer();
            myPlayer.Open(new System.Uri(audioPath));
            myPlayer.Play();
        }

        private void Parentize()
        {
            pbFarmio.Parent = pbStart.Parent = pbLoad.Parent = pbExit.Parent = pbStart.Parent = pictureBox1;
            pbName.Parent = pbCDTree.Parent = pbGetStone.Parent = pbCutGrass.Parent = pbCDStump.Parent = pbGetMushroom.Parent = pbGetMushrooms.Parent = pbNameOk.Parent = pbGLEF.Parent = pbInventoryOpen.Parent =  pbFon;
            lblInventory.Parent = lblGold.Parent = lblEpoch.Parent = lblEnergy.Parent = label1.Parent = label2.Parent = label3.Parent = pbGLEF;
            pbHero.Parent = pbFon;
        }

        void musicTimer_Tick(object sender, EventArgs e)
        {
            SoundPlay(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "Resources\\", SomeFunctions.MusicRandomize()));
        }

        public partial class TransparentPictureBox : PictureBox
        {
            public TransparentPictureBox()
            {
                this.BackColor = System.Drawing.Color.Transparent;
            }
            protected override void OnPaint(PaintEventArgs e)
            {
                if (Parent != null && this.BackColor == System.Drawing.Color.Transparent)
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
            if (pbInventoryOpen.Visible)
                InventoryClose();
            else if (start && longRunningTask.IsCompleted)
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
                    longRunningTask = HeroMoveHere(ClickX, ClickY, 0);
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
                    longRunningTask = HeroMoveHere(x, y, 30);
                    int result = await longRunningTask;
                    SoundPlay(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "Resources\\chopping-wood.wav"));
                    await Task.Delay(300);
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
                    longRunningTask = HeroMoveHere(x, y, 10);
                    int result = await longRunningTask;
                    SoundPlay(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "Resources\\stone-hit.wav"));
                    await Task.Delay(400);
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
                    longRunningTask = HeroMoveHere(x, y, 0);
                    int result = await longRunningTask;
                    SoundPlay(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "Resources\\cutting-grass.wav"));
                    await Task.Delay(200);
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
                    longRunningTask = HeroMoveHere(x, y, 0);
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
                    longRunningTask = HeroMoveHere(x, y, 0);
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
                    longRunningTask = HeroMoveHere(x, y, 0);
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

        public async Task<int> HeroMoveHere(int x, int y, int xyplus)
        {
            //Console.Clear();
            char c = ' ';
            var player = new SoundPlayer(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "Resources\\footsteps.wav")) ;
            player.PlayLooping();
            if (xyplus == 0)
                xyplus = 5;
            int i = 0;
            while (hero.x-x > xyplus)
            {
                c = 'a';
                while (!map.isFree(hero.x, hero.y, c))
                {
                    int min1 = 0, min2 = 0;
                    while(!map.isFree(hero.x, hero.y - min1, c))
                        min1++;
                    while (!map.isFree(hero.x, hero.y + min2, c)) 
                        min2++;
                    int posY = hero.y;
                    if (min1 > min2)
                        while (hero.y < posY + min2)
                        {
                            if (i > 5 || i < 2)
                                i = 3;
                            pbHero.Image = hero.Sprite[i];
                            i++;
                            hero.y += 5;
                            pbHero.Top = hero.y;
                            await Task.Delay(40);
                        }    
                    else
                        while (hero.y > posY - min2)
                        {
                            if (i > 2)
                                i = 0;
                            pbHero.Image = hero.Sprite[i];
                            i++;
                            hero.y -= 5;
                            pbHero.Top = hero.y;
                            await Task.Delay(40);
                        }
                }
                if (i > 8 || i < 6)
                    i = 6;
                pbHero.Image = hero.Sprite[i];
                i++;
                hero.x -= 5;
                pbHero.Left = hero.x;
                await Task.Delay(35);
            }
            while (x - hero.x > xyplus)
            {
                c = 'd';
                while (!map.isFree(hero.x, hero.y, c))
                {
                    int min1 = 0, min2 = 0;
                    while (!map.isFree(hero.x, hero.y - min1, c))
                        min1++;
                    while (!map.isFree(hero.x, hero.y + min2, c))
                        min2++;
                    int posY = hero.y;
                    if (min1 > min2)
                        while (hero.y < posY + min2)
                        {
                            if (i > 5 || i < 2)
                                i = 3;
                            pbHero.Image = hero.Sprite[i];
                            i++;
                            hero.y += 5;
                            pbHero.Top = hero.y;
                            await Task.Delay(40);
                        }
                    else
                        while (hero.y > posY - min2)
                        {
                            if (i > 2)
                                i = 0;
                            pbHero.Image = hero.Sprite[i];
                            i++;
                            hero.y -= 5;
                            pbHero.Top = hero.y;
                            await Task.Delay(40);
                        }
                }
                if (i > 11 || i < 9)
                    i = 9;
                pbHero.Image = hero.Sprite[i];
                i++;
                hero.x += 5;
                pbHero.Left = hero.x;
                await Task.Delay(35);
            }
            while (hero.y - y > xyplus)
            {
                c = 'w';
                while (!map.isFree(hero.x, hero.y, c))
                {
                    int min1 = 0, min2 = 0;
                    while (!map.isFree(hero.x - min1, hero.y, c))
                        min1++;
                    while (!map.isFree(hero.x + min2, hero.y, c))
                        min2++;
                    int posX = hero.x;
                    if (min1 > min2)
                        while (hero.x < posX + min2)
                        {
                            if (i > 11 || i < 9)
                                i = 9;
                            pbHero.Image = hero.Sprite[i];
                            i++;
                            hero.x += 5;
                            pbHero.Left = hero.x;
                            await Task.Delay(40);
                        }
                    else
                        while (hero.x > posX - min2)
                        {
                            if (i > 8 || i < 6)
                                i = 6;
                            pbHero.Image = hero.Sprite[i];
                            i++;
                            hero.x -= 5;
                            pbHero.Left = hero.x;
                            await Task.Delay(40);
                        }
                }
                if (i > 2)
                    i = 0;
                pbHero.Image = hero.Sprite[i];
                i++;
                hero.y -= 5;
                pbHero.Top = hero.y;
                await Task.Delay(35);
            }
            while (y - hero.y > xyplus)
            {
                c = 's';
                while (!map.isFree(hero.x, hero.y, c))
                {
                    int min1 = 0, min2 = 0;
                    while (!map.isFree(hero.x - min1, hero.y, c))
                        min1++;
                    while (!map.isFree(hero.x + min2, hero.y, c))
                        min2++;
                    int posX = hero.x;
                    if (min1 > min2)
                        while (hero.x < posX + min2)
                        {
                            if (i > 11 || i < 9)
                                i = 9;
                            pbHero.Image = hero.Sprite[i];
                            i++;
                            hero.x += 5;
                            pbHero.Left = hero.x;
                            await Task.Delay(40);
                        }
                    else
                        while (hero.x > posX - min2)
                        {
                            if (i > 8 || i < 6)
                                i = 6;
                            pbHero.Image = hero.Sprite[i];
                            i++;
                            hero.x -= 5;
                            pbHero.Left = hero.x;
                            await Task.Delay(40);
                        }
                }
                if (i > 5 || i < 2)
                    i = 3;
                pbHero.Image = hero.Sprite[i];
                i++;
                hero.y += 5;
                pbHero.Top = hero.y;
                await Task.Delay(35);
            }
            if (c == 'w')
                pbHero.Image = hero.Sprite[0];
            else if (c == 's')
                pbHero.Image = hero.Sprite[3];
            else if (c == 'a')
                pbHero.Image = hero.Sprite[6];
            else if (c == 'd')
                pbHero.Image = hero.Sprite[9];
            player.Stop();
            return 0;

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

        private void lblInventory_Click(object sender, EventArgs e)
        {
            SoundPlay(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "Resources\\inventory.wav"));
            InventoryLoad();
        }

        private void InventoryLoad()
        {
            pbInventoryOpen.Visible = true;
            int i = 0;
            foreach (Item item in hero.Inventory)
            {
                if (item.Number == 0)
                    continue;
                item1ThrowAway.Visible = true;
                if (item.Number == 1)
                    inventoryLabels[i].Text = item.Name;
                else
                    inventoryLabels[i].Text = item.NamePlural;
                inventoryLabels[i].Visible = true;
                inventoryLabels[i + 5].Text = " " + item.Number.ToString();
                inventoryLabels[i + 5].BringToFront();

                i++;
                if (i > 4)
                    break;
            }
        }

        private void InventoryClose()
        {
            pbInventoryOpen.Visible = false; int i;
            for (i = 0; i < 5; i++)
                inventoryLabels[i].Visible = false;
            for (i = 5; i < 10; i++)
                inventoryLabels[i].SendToBack();
        }

        private void item1ThrowAway_Click(object sender, EventArgs e)
        {
            foreach (Item item in hero.Inventory)
            {
                if (item.Name == inventoryLabels[0].Text || item.NamePlural == inventoryLabels[0].Text)
                    item.Number--;
                if (item.Number == 0)
                {
                    hero.Inventory.Remove(item);
                    InventoryClose();
                }
                InventoryLoad();
                break;
            }
        }
    }
}
