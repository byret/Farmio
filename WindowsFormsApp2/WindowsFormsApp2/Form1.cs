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
using SharpDX.Direct3D11;

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
        List<Label> storageLabels = new List<Label>();
        List<PictureBox> inventoryIcons = new List<PictureBox>();
        List<PictureBox> StorageIcons = new List<PictureBox>();
        List<Image> windows = new List<Image>();
        int hour;
        int minutes;
        System.Windows.Forms.Timer TimeOfDayTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer InventoryPlusTimer = new System.Windows.Forms.Timer();
        int windowFrame = 0;
        bool StorageIsOpen = false;
        Map.MapMainPoint.Building BuildingToAddIn;

        public Farmio()
        {
            map.MapGeneration(map);
            hero = new Hero();
            hero.SetSprite(1);
            hour = 8;
            minutes = 30;

            InitializeComponent();
            longRunningTask = HeroMoveHere(hero.x, hero.y, 0);
            System.Windows.Forms.Timer musicTimer = new System.Windows.Forms.Timer();
            musicTimer.Interval = 300000;
            musicTimer.Tick += new EventHandler(musicTimer_Tick);
            musicTimer.Start();
            InventoryPlusTimer.Interval = 1000;
            InventoryPlusTimer.Tick += new EventHandler(InventoryPlusTimer_Tick);
            TimeOfDayTimer.Interval = 7500;
            TimeOfDayTimer.Tick += new EventHandler(TimeOfDayTimer_Tick);

            inventoryLabels.Add(item1);
            inventoryLabels.Add(item2);
            inventoryLabels.Add(item3);
            inventoryLabels.Add(item4);
            inventoryLabels.Add(item5);
            inventoryLabels.Add(item1NumberOf);
            inventoryLabels.Add(item2NumberOf);
            inventoryLabels.Add(item3NumberOf);
            inventoryLabels.Add(item4NumberOf);
            inventoryLabels.Add(item5NumberOf);
            inventoryIcons.Add(item1ThrowAway);
            inventoryIcons.Add(item2ThrowAway);
            inventoryIcons.Add(item3ThrowAway);
            inventoryIcons.Add(item4ThrowAway);
            inventoryIcons.Add(item5ThrowAway);
            inventoryIcons.Add(useIcon1);
            inventoryIcons.Add(useIcon2);
            inventoryIcons.Add(useIcon3);
            inventoryIcons.Add(useIcon4);
            inventoryIcons.Add(useIcon5);
            inventoryIcons.Add(pbInventoryArrow1);
            inventoryIcons.Add(pbInventoryArrow2);
            inventoryIcons.Add(pbInventoryArrow3);
            inventoryIcons.Add(pbInventoryArrow4);
            inventoryIcons.Add(pbInventoryArrow5);
            inventoryIcons.Add(pbInventoryArrow01);
            inventoryIcons.Add(pbInventoryArrow02);
            inventoryIcons.Add(pbInventoryArrow03);
            inventoryIcons.Add(pbInventoryArrow04);
            inventoryIcons.Add(pbInventoryArrow05);
            storageLabels.Add(lblStorageItem1);
            storageLabels.Add(lblStorageItem2);
            storageLabels.Add(lblStorageItem3);
            storageLabels.Add(lblStorageItem4);
            storageLabels.Add(lblStorageItem5);
            storageLabels.Add(lblStorageItem6);
            storageLabels.Add(lblStorageItem7);
            storageLabels.Add(lblStorageItem8);
            storageLabels.Add(lblStorageItem9);
            storageLabels.Add(lblStorageItem10);
            storageLabels.Add(StorageNumOfItem1);
            storageLabels.Add(StorageNumOfItem2);
            storageLabels.Add(StorageNumOfItem3);
            storageLabels.Add(StorageNumOfItem4);
            storageLabels.Add(StorageNumOfItem5);
            storageLabels.Add(StorageNumOfItem6);
            storageLabels.Add(StorageNumOfItem7);
            storageLabels.Add(StorageNumOfItem8);
            storageLabels.Add(StorageNumOfItem9);
            storageLabels.Add(StorageNumOfItem10);
            StorageIcons.Add(pbStorageArrow1);
            StorageIcons.Add(pbStorageArrow2);
            StorageIcons.Add(pbStorageArrow3);
            StorageIcons.Add(pbStorageArrow4);
            StorageIcons.Add(pbStorageArrow5);
            StorageIcons.Add(pbStorageArrow6);
            StorageIcons.Add(pbStorageArrow7);
            StorageIcons.Add(pbStorageArrow8);
            StorageIcons.Add(pbStorageArrow9);
            StorageIcons.Add(pbStorageArrow10);
            StorageIcons.Add(pbStorageArrow01);
            StorageIcons.Add(pbStorageArrow02);
            StorageIcons.Add(pbStorageArrow03);
            StorageIcons.Add(pbStorageArrow04);
            StorageIcons.Add(pbStorageArrow05);
            StorageIcons.Add(pbStorageArrow06);
            StorageIcons.Add(pbStorageArrow07);
            StorageIcons.Add(pbStorageArrow08);
            StorageIcons.Add(pbStorageArrow09);
            StorageIcons.Add(pbStorageArrow010);
            pbFon.Image = map.DrawMap();

            for (int i = 1; i <= 20; i++)
            {
                windows.Add(Image.FromFile(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "Resources\\window" + i.ToString() + ".png"))); 
            }
            Parentize();

            pbHero.Image = (Image)hero.Sprite[4];
            pbHero.Location = new Point(hero.x, hero.y);
            pictureBox1.Image = (Bitmap)global::WindowsFormsApp2.Properties.Resources.ResourceManager.GetObject(SomeFunctions.MainPictureRandomize());
            string startupPath = System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "Resources\\", SomeFunctions.MusicRandomize());
            //SoundPlay(startupPath);
            lblGold.Text = hero.Gold.ToString();
            lblEpoch.Text = hero.Level.ToString();
            lblEnergy.Text = hero.Energy.ToString() + "/200";
            lblSaturation.Text = hero.Saturation.ToString() + "/100";
            lblHour.Text = hour + ":" + minutes;
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
            pbName.Parent = pbCDTree.Parent = pbGetStone.Parent = pbCutGrass.Parent = pbCDStump.Parent = pbGetMushroom.Parent = pbGetMushrooms.Parent = pbStorage.Parent = pbGoToSleep.Parent = pbNameOk.Parent = pbGLEF.Parent = pbInventoryOpen.Parent = pbFon;
            lblInventory.Parent = lblGold.Parent = lblEpoch.Parent = lblEnergy.Parent = lblSaturation.Parent = lblHour.Parent = label1.Parent = label2.Parent = label3.Parent = label4.Parent = label5.Parent = lblInventoryPlus.Parent = pbGLEF;
            Window.Parent = pbFon;
            pbHero.Parent = Window;

            //foreach (Label label in storageLabels)
               // label.Parent = pbStorageIsOpen;
        }

        void musicTimer_Tick(object sender, EventArgs e)
        {
            SoundPlay(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "Resources\\", SomeFunctions.MusicRandomize()));
        }

        void TimeOfDayTimer_Tick(object sender, EventArgs e)
        {
            minutes += 15;
            if (minutes == 60)
            {
                minutes = 0;
                if (hero.Saturation > 0)
                {
                    hero.Saturation -= 2;
                    if (hero.Saturation < 0)
                        hero.Saturation = 0;
                    lblSaturation.Text = hero.Saturation.ToString() + "/100";
                }
            }

            if (hour == 23 && minutes == 0)
                hour = 0;
            else if (minutes == 0)
                hour++;

            lblHour.Text = (minutes == 0) ? hour + ":00" : hour + ":" + minutes;

            if (hour == 19)
            {
                if (minutes == 0)
                {
                    windowFrame = 1;
                    Window.Image = windows[1];
                }
                else if (minutes == 15)
                {
                    windowFrame = 2;
                    Window.Image = windows[2];
                }
                else if (minutes == 30)
                {
                    windowFrame = 3;
                    Window.Image = windows[3];
                }
                else if (minutes == 45)
                {
                    windowFrame = 4;
                    Window.Image = windows[4];
                }
            }
            else if (hour == 20)
            {
                if (minutes == 0)
                {
                    windowFrame = 5;
                    Window.Image = windows[5];
                }
                else if (minutes == 15)
                {
                    windowFrame = 6;
                    Window.Image = windows[6];
                }
                else if (minutes == 30)
                {
                    windowFrame = 7;
                    Window.Image = windows[7];
                }
                else if (minutes == 45)
                {
                    windowFrame = 8;
                    Window.Image = windows[8];
                }
            }
            else if (hour == 21)
            {
                if (minutes == 0)
                {
                    windowFrame = 9;
                    Window.Image = windows[9];
                }
                else if (minutes == 15)
                {
                    windowFrame = 10;
                    Window.Image = windows[10];
                }
                else if (minutes == 30)
                {
                    windowFrame = 11;
                    Window.Image = windows[11];
                }
                else if (minutes == 45)
                {
                    windowFrame = 12;
                    Window.Image = windows[12];
                }
            }
            else if (hour == 22)
            {
                if (minutes == 0)
                {
                    windowFrame = 13;
                    Window.Image = windows[13];
                }
                else if (minutes == 15)
                {
                    windowFrame = 14;
                    Window.Image = windows[14];
                }
                else if (minutes == 30)
                {
                    windowFrame = 15;
                    Window.Image = windows[15];
                }
                else if (minutes == 45)
                {
                    windowFrame = 16;
                    Window.Image = windows[16];
                }
            }
            else if (hour == 23)
            {
                if (minutes == 0)
                {
                    windowFrame = 17;
                    Window.Image = windows[17];
                }
                else if (minutes == 15)
                {
                    windowFrame = 18;
                    Window.Image = windows[18];
                }
                else if (minutes == 30)
                {
                    windowFrame = 19;
                    Window.Image = windows[19];
                }
            }
            else if (hour == 3)
            {
                if (minutes == 45)
                {
                    windowFrame = 18;
                    Window.Image = windows[18];
                }
            }
            else if (hour == 4)
            {
                if (minutes == 0)
                {
                    windowFrame = 17;
                    Window.Image = windows[17];
                }
                else if (minutes == 15)
                {
                    windowFrame = 16;
                    Window.Image = windows[16];
                }
                else if (minutes == 30)
                {
                    windowFrame = 15;
                    Window.Image = windows[15];
                }
                else if (minutes == 45)
                {
                    windowFrame = 14;
                    Window.Image = windows[14];
                }
            }
            else if (hour == 5)
            {
                if (minutes == 0)
                {
                    windowFrame = 13;
                    Window.Image = windows[13];
                }
                else if (minutes == 15)
                {
                    windowFrame = 12;
                    Window.Image = windows[12];
                }
                else if (minutes == 30)
                {
                    windowFrame = 11;
                    Window.Image = windows[11];
                }
                else if (minutes == 45)
                {
                    windowFrame = 10;
                    Window.Image = windows[10];
                }
            }
            else if (hour == 6)
            {
                if (minutes == 0)
                {
                    windowFrame = 9;
                    Window.Image = windows[9];
                }
                else if (minutes == 15)
                {
                    windowFrame = 8;
                    Window.Image = windows[8];
                }
                else if (minutes == 30)
                {
                    windowFrame = 7;
                    Window.Image = windows[7];
                }
                else if (minutes == 45)
                {
                    windowFrame = 6;
                    Window.Image = windows[6];
                }
            }
            else if (hour == 7)
            {
                if (minutes == 0)
                {
                    windowFrame = 5;
                    Window.Image = windows[5];
                }
                else if (minutes == 15)
                {
                    windowFrame = 4;
                    Window.Image = windows[4];
                }
                else if (minutes == 30)
                {
                    windowFrame = 3;
                    Window.Image = windows[3];
                }
                else if (minutes == 45)
                {
                    windowFrame = 2;
                    Window.Image = windows[2];
                }
            }
            else if (hour == 8)
            {
                if (minutes == 0)
                {
                    windowFrame = 1;
                    Window.Image = windows[1];
                }
                else if (minutes == 15)
                {
                    windowFrame = 0;
                    Window.Image = windows[0];
                }
            }
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
                TimeOfDayTimer.Start();
            }
        }


        private void Window_MouseDown(object sender, MouseEventArgs e)
        {
            if (pbInventoryOpen.Visible)
                InventoryClose();
            if (pbStorageIsOpen.Visible)
                StorageClose();
            else if (start && longRunningTask.IsCompleted)
            {
                ClickX = e.X;
                ClickY = e.Y;
                if (map.mapTab[ClickX, ClickY] != null)
                {
                    int x = map.mapTab[ClickX, ClickY].X;
                    int y = map.mapTab[ClickX, ClickY].Y;
                    Map.MapMainPoint tmp = (Map.MapMainPoint)map.mapTab[x, y];
                    if (tmp is Map.MapMainPoint.Building)
                    {
                        pbStorage.Location = new Point(ClickX + 5, ClickY + 5);
                        pbStorage.Visible = true;
                        pbGoToSleep.Location = new Point(ClickX + 5, ClickY + 5 + 19);
                        pbGoToSleep.Visible = true;
                        pbCDTree.Visible = pbGetStone.Visible = pbCDStump.Visible = pbGetMushrooms.Visible = pbGetMushroom.Visible = false;
                    }

                    else if (tmp is Map.MapMainPoint.Grass)
                    {
                        pbCutGrass.Location = new Point(ClickX + 5, ClickY + 5);
                        pbCutGrass.Visible = true;
                        pbGoToSleep.Visible = pbStorage.Visible = pbCDTree.Visible = pbGetStone.Visible = pbCDStump.Visible = pbGetMushrooms.Visible = pbGetMushroom.Visible = false;
                    }

                    else if (tmp is Map.MapMainPoint.Stone)
                    {
                        pbGetStone.Location = new Point(ClickX + 5, ClickY + 5);
                        pbGetStone.Visible = true;
                        pbGoToSleep.Visible = pbStorage.Visible = pbCutGrass.Visible = pbCDTree.Visible = pbCDStump.Visible = pbGetMushrooms.Visible = pbGetMushroom.Visible = false;
                    }

                    else if (tmp is Map.MapMainPoint.Tree)
                    {
                        pbCDTree.Location = new Point(ClickX + 5, ClickY + 5);
                        pbCDTree.Visible = true;
                        pbGoToSleep.Visible = pbStorage.Visible = pbCutGrass.Visible = pbGetStone.Visible = pbCDStump.Visible = pbGetMushrooms.Visible = pbGetMushroom.Visible = false;
                    }

                    else if (tmp is Map.MapMainPoint.Stump)
                    {
                        pbCDStump.Location = new Point(ClickX + 5, ClickY + 5);
                        pbCDStump.Visible = true;
                        pbGoToSleep.Visible = pbStorage.Visible = pbCutGrass.Visible = pbCDTree.Visible = pbGetStone.Visible = pbGetMushrooms.Visible = pbGetMushroom.Visible = false;
                    }

                    else if (tmp is Map.MapMainPoint.Mushroom)
                    {
                        if (tmp.Name[2] == '1')
                        {
                            pbGetMushroom.Location = new Point(ClickX + 5, ClickY + 5);
                            pbGetMushroom.Visible = true;
                            pbGoToSleep.Visible = pbStorage.Visible = pbCutGrass.Visible = pbCDTree.Visible = pbGetStone.Visible = pbGetMushrooms.Visible = pbCDStump.Visible = false;
                        }

                        else
                        {
                            pbGetMushrooms.Location = new Point(ClickX + 5, ClickY + 5);
                            pbGetMushrooms.Visible = true;
                            pbGoToSleep.Visible = pbStorage.Visible = pbCutGrass.Visible = pbCDTree.Visible = pbGetStone.Visible = pbGetMushroom.Visible = pbCDStump.Visible = false;
                        }
                    }
                }

                else
                {
                    pbGoToSleep.Visible = pbStorage.Visible = pbCutGrass.Visible = pbCDTree.Visible = pbGetStone.Visible = pbCDStump.Visible = pbGetMushrooms.Visible = pbGetMushroom.Visible = false;
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
                    lblInventoryPlus.Text = '+' + hero.addToInventory(tmp.DestroyTree(map)).ToString();
                    pbFon.Image = map.DrawMap();
                    lblEnergy.Text = hero.Energy.ToString() + "/200";
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
                    lblInventoryPlus.Text = '+' + hero.addToInventory(tmp.DestroyStone(map)).ToString();
                    pbFon.Image = map.DrawMap();
                    lblEnergy.Text = hero.Energy.ToString() + "/200";
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
                    lblInventoryPlus.Text = '+' + hero.addToInventory(tmp.DestroyGrass(map)).ToString();
                    pbFon.Image = map.DrawMap();
                    lblEnergy.Text = hero.Energy.ToString() + "/200";
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
                    lblInventoryPlus.Text = '+' + hero.addToInventory(tmp.DestroyStump(map)).ToString();
                    pbFon.Image = map.DrawMap();
                    lblEnergy.Text = hero.Energy.ToString() + "/200";
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
                        lblInventoryPlus.Text = '+' + hero.addToInventory(tmp.DestroyMushroomNE(map)).ToString();
                    else
                        lblInventoryPlus.Text = '+' + hero.addToInventory(tmp.DestroyMushroom(map)).ToString();
                    pbFon.Image = map.DrawMap();
                    lblEnergy.Text = hero.Energy.ToString() + "/200";
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
                        lblInventoryPlus.Text = '+' + hero.addToInventory(tmp.DestroyMushroomNE(map)).ToString();
                    else
                        lblInventoryPlus.Text = '+' + hero.addToInventory(tmp.DestroyMushroom(map)).ToString();
                    pbFon.Image = map.DrawMap();
                    lblEnergy.Text = hero.Energy.ToString() + "/200";
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
            SoundPlayer player = new SoundPlayer(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "Resources\\footsteps1.wav"));
            if (xyplus == 0)
                xyplus = 5;
            int i = 0;
            int n = 0;
            if (hero.Energy < 10 || hero.Saturation > 95)
            {
                n = 35;
                player = new SoundPlayer(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "Resources\\footsteps2.wav"));
            }
                
            else if (hero.Energy < 25 || hero.Saturation > 90)
            {
                n = 20;
                player = new SoundPlayer(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "Resources\\footsteps3.wav"));
            }
                
            else if (hero.Energy < 50)
            {
                n = 10;
                player = new SoundPlayer(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "Resources\\footsteps4.wav"));
            }
                
            player.PlayLooping();
            while (hero.x - x > xyplus)
            {
                c = 'a';
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
                            await Task.Delay(35 + n);
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
                            await Task.Delay(35 + n);
                        }
                }
                if (i > 8 || i < 6)
                    i = 6;
                pbHero.Image = hero.Sprite[i];
                i++;
                hero.x -= 5;
                pbHero.Left = hero.x;
                await Task.Delay(35 + n);
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
                            await Task.Delay(35 + n);
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
                            await Task.Delay(35 + n);
                        }
                }
                if (i > 11 || i < 9)
                    i = 9;
                pbHero.Image = hero.Sprite[i];
                i++;
                hero.x += 5;
                pbHero.Left = hero.x;
                await Task.Delay(35 + n);
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
                            await Task.Delay(35 + n);
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
                            await Task.Delay(35 + n);
                        }
                }
                if (i > 2)
                    i = 0;
                pbHero.Image = hero.Sprite[i];
                i++;
                hero.y -= 5;
                pbHero.Top = hero.y;
                await Task.Delay(35 + n);
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
                            await Task.Delay(35 + n);
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
                            await Task.Delay(35 + n);
                        }
                }
                if (i > 5 || i < 2)
                    i = 3;
                pbHero.Image = hero.Sprite[i];
                i++;
                hero.y += 5;
                pbHero.Top = hero.y;
                await Task.Delay(35 + n);
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
            InventoryClose();
            pbInventoryOpen.Visible = true;
            int i = 0;
            foreach (Item item in hero.Inventory)
            {
                if (item.Number == 0)
                    continue;
                if (item.Number == 1)
                    inventoryLabels[i].Text = item.Name;
                else
                    inventoryLabels[i].Text = item.NamePlural;
                inventoryLabels[i].Visible = true;
                inventoryLabels[i + 5].Text = " " + item.Number.ToString();
                inventoryLabels[i + 5].Visible = true;
                if (!StorageIsOpen)
                {
                    inventoryIcons[i].Visible = true;
                    if (item.IsUsable)
                        inventoryIcons[i + 5].Visible = true;
                }
                else
                {
                    inventoryIcons[i + 10].Visible = true;
                    inventoryIcons[i + 15].Visible = true;
                }

                i++;
                if (i > 4)
                    break;
            }
        }

        private void InventoryClose()
        {
            pbInventoryOpen.Visible = false; int i;
            foreach (Label label in inventoryLabels)
                label.Visible = false;
            foreach (PictureBox pictureBox in inventoryIcons)
                pictureBox.Visible = false;
        }

        private void item1ThrowAway_Click(object sender, EventArgs e)
        {
            RemoveItemFromInventory(0, 1);
        }

        private void item2ThrowAway_Click(object sender, EventArgs e)
        {
            RemoveItemFromInventory(1, 1);
        }

            private void item3ThrowAway_Click(object sender, EventArgs e)
        {
            RemoveItemFromInventory(2, 1);
        }

        private void item4ThrowAway_Click(object sender, EventArgs e)
        {
            RemoveItemFromInventory(3, 1);
        }

        private void item5ThrowAway_Click(object sender, EventArgs e)
        {
            RemoveItemFromInventory(4, 1);
        }

        public Item RemoveItemFromInventory(int n, int i)
        {
            Item itemTmp2 = new Item();
            foreach (Item item in hero.Inventory)
            {
                if (item.Name == inventoryLabels[n].Text || item.NamePlural == inventoryLabels[n].Text)
                {
                    item.Number -= i;
                    if (item.Number <= 0)
                    {
                        hero.Inventory.Remove(item);
                        hero.NumOfItemsInInventory--;
                    }
                    InventoryLoad(); 
                    object o = item.Clone();
                    Item itemTmp = (Item)o;
                    itemTmp.Number = i;
                    return itemTmp;
                }
            }
            return itemTmp2;
        }

        public void Eat(Item.Food food, int n)
        {
            if (food.Energy < 0)
            {
                if (hero.Energy > 5)
                    hero.Energy = 5;
                else hero.Energy = 0;
                SoundPlay(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "Resources\\crunch.wav"));
                windowFrame++; 
                Window.Image = windows[windowFrame];   
            }
            else
            {
                hero.Saturation += food.Energy;
                hero.Energy += food.Energy;
                if (hero.Energy > 200)
                    hero.Energy = 200;
                if (hero.Saturation > 100)
                    hero.Saturation = 100;
                SoundPlay(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "Resources\\crunch.wav"));
            }
            lblEnergy.Text = hero.Energy.ToString() + "/200";
            lblSaturation.Text = hero.Saturation.ToString() + "/100";
            RemoveItemFromInventory(n, 1);
            }

        private void useIcon1_Click(object sender, EventArgs e)
        {
            UseIconClick(0);
        }

        private void useIcon2_Click(object sender, EventArgs e)
        {
            UseIconClick(1);
        }

        private void useIcon3_Click(object sender, EventArgs e)
        {
            UseIconClick(2);
        }

        private void useIcon4_Click(object sender, EventArgs e)
        {
            UseIconClick(3);
        }

        private void useIcon5_Click(object sender, EventArgs e)
        {
            UseIconClick(4);
        }

        private void UseIconClick (int i)
        {

            foreach (Item item in hero.Inventory)
            {
                if (item.Name == inventoryLabels[i].Text || item.NamePlural == inventoryLabels[i].Text)
                {
                    if (item is Item.Food)
                        Eat((Item.Food)item, i);
                    break;
                }
            }
        }

        private async void pbGoToSleep_Click(object sender, EventArgs e)
        {
            TimeOfDayTimer.Stop();
            pbGoToSleep.Visible = false;
            pbStorage.Visible = false;
            int x = map.mapTab[ClickX, ClickY].X + 48;
            int y = map.mapTab[ClickX, ClickY].Y + 84;
            longRunningTask = HeroMoveHere(x, y, 0);
            int result = await longRunningTask;
            pbHero.Visible = false;
            if ((hour == 3 && minutes > 15) || (hour > 3 && hour < 8) || (hour == 8 && minutes == 0))
            {
                for (int i = windowFrame; i >= 0; i--)
                {
                    Window.Image = windows[i];
                    await Task.Delay(80);
                }
            }
            else
            {
                for (int i = windowFrame; i < 20; i++)
                {
                    Window.Image = windows[i];
                    await Task.Delay(80);
                }
                map.MapUpdate(map);
                pbFon.Image = map.DrawMap();
                for (int i = 19; i >= 0; i--)
                {
                    Window.Image = windows[i];
                    await Task.Delay(80);
                }
            }
            pbHero.Visible = true;
            hero.Energy += hero.Saturation * 2;
            if (hero.Energy > 200)
                hero.Energy = 200;
            lblEnergy.Text = hero.Energy.ToString() + "/200";
            hour = 8;
            minutes = 15;
            TimeOfDayTimer.Start();
        }

        private void lblEnergy_TextChanged(object sender, EventArgs e)
        {
            string tmp = lblEnergy.Text.Substring(0, lblEnergy.Text.IndexOf("/"));
            int x;
            if (int.TryParse(tmp, out x))
            {
                x = Convert.ToInt32(tmp);
            }
            if (x < 11)
                lblEnergy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(32)))), ((int)(((byte)(20)))));
            else 
                lblEnergy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(189)))), ((int)(((byte)(52)))));
        }
        private void lblSaturation_TextChanged(object sender, EventArgs e)
        {
            string tmp = lblSaturation.Text.Substring(0, lblSaturation.Text.IndexOf("/"));
            int x;
            if (int.TryParse(tmp, out x))
            {
                x = Convert.ToInt32(tmp);
            }
            if (x < 6)
                lblSaturation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(32)))), ((int)(((byte)(20)))));
            else 
                lblSaturation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(189)))), ((int)(((byte)(52)))));
        }

        private void lblInventoryPlus_TextChanged(object sender, EventArgs e)
        {
            if (lblInventoryPlus.Text != "+1000" && lblInventoryPlus.Text != "+0")
                lblInventoryPlus.Visible = true;
            else if (lblInventoryPlus.Text == "+1000")
            {
                lblInventory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(32)))), ((int)(((byte)(20)))));
                InventoryPlusTimer.Interval = 3000;
            }
                
            InventoryPlusTimer.Start();
        }

        void InventoryPlusTimer_Tick(object sender, EventArgs e)
        {
            if (lblInventory.ForeColor == System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(32)))), ((int)(((byte)(20))))))
            {
                lblInventory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(189)))), ((int)(((byte)(52)))));
                InventoryPlusTimer.Interval = 1000;
            }
            else
                lblInventoryPlus.Visible = false;

            InventoryPlusTimer.Stop();
        }

        private async void pbStorage_Click(object sender, EventArgs e)
        {
            pbGoToSleep.Visible = false;
            pbStorage.Visible = false;
            int x = map.mapTab[ClickX, ClickY].X;
            int y = map.mapTab[ClickX, ClickY].Y;
            longRunningTask = HeroMoveHere(x + 48, y + 84, 0);
            int result = await longRunningTask;
            BuildingToAddIn = (Map.MapMainPoint.Building)map.mapTab[x, y];
            StorageLoad();
        }

        private void StorageLoad()
        {
            StorageClose();
            StorageIsOpen = true;
            pbStorageIsOpen.Visible = true;
            int i = 0;
            foreach (Item item in BuildingToAddIn.Storage)
            {
                if (item.Number == 0)
                    continue;
                StorageIcons[i].Visible = true;
                StorageIcons[i].BringToFront();
                StorageIcons[i + 10].Visible = true;
                StorageIcons[i + 10].BringToFront();
                storageLabels[i].Text = (item.Number == 1) ? item.Name : item.NamePlural;
                storageLabels[i].Visible = true;
                storageLabels[i].BringToFront();
                storageLabels[i + 10].Text = item.Number.ToString();
                storageLabels[i + 10].Visible = true;
                storageLabels[i + 10].BringToFront();
                i++;
            }
            InventoryLoad();

        }
        private void StorageClose()
        {
            StorageIsOpen = false;
            int i = 0;
            foreach (Label label in storageLabels)
            {
                Console.WriteLine(label.Text.Length);
                if (label.Text.Length == 0)
                    break;
                StorageIcons[i].Visible = false;
                StorageIcons[i + 10].Visible = false;
                label.Visible = false;
                storageLabels[i + 10].Visible = false;
                i++;
            }

            pbStorageIsOpen.Visible = false;
        }

        private void AddToStorage(Item ItemToAdd)
        {
            if (ItemToAdd.Number != 0)
                BuildingToAddIn.AddToStorage(ItemToAdd);
            int i = 0;
            foreach (Item item in BuildingToAddIn.Storage)
            {
                storageLabels[i].Text = (item.Number == 1) ? item.Name : item.NamePlural; 
                i++;
            }
            StorageLoad();
        }

        private void pbInventoryArrow01_Click(object sender, EventArgs e)
        {
            if (!IsStorageFull(0, 1))
                AddToStorage (RemoveItemFromInventory(0, 1));
        }

        private void pbInventoryArrow02_Click(object sender, EventArgs e)
        {
            if (!IsStorageFull(1, 1))
                AddToStorage(RemoveItemFromInventory(1, 1));
            }

        private void pbInventoryArrow03_Click(object sender, EventArgs e)
        {
            if (!IsStorageFull(2, 1))
                AddToStorage(RemoveItemFromInventory(2, 1));
            }

        private void pbInventoryArrow04_Click(object sender, EventArgs e)
        {
            if (!IsStorageFull(3, 1))
                AddToStorage(RemoveItemFromInventory(3, 1));
            }

        private void pbInventoryArrow05_Click(object sender, EventArgs e)
        {
            if (!IsStorageFull(4, 1))
                AddToStorage(RemoveItemFromInventory(4, 1));
            }

        private void pbInventoryArrow1_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(item1NumberOf.Text);
            if (n > 10)
                n = 10;

            if (!IsStorageFull(0, n))
                AddToStorage(RemoveItemFromInventory(0, n));
            }

        private void pbInventoryArrow2_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(item2NumberOf.Text);
            if (n > 10)
                n = 10;

            if (!IsStorageFull(1, n))
                AddToStorage(RemoveItemFromInventory(1, n));
            }

        private void pbInventoryArrow3_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(item3NumberOf.Text);
            if (n > 10)
                n = 10;

            if (!IsStorageFull(2, n))
                AddToStorage(RemoveItemFromInventory(2, n));
            }

        private void pbInventoryArrow4_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(item4NumberOf.Text);
            if (n > 10)
                n = 10;

            if (!IsStorageFull(3, n))
                AddToStorage(RemoveItemFromInventory(3, n));
        }

        private void pbInventoryArrow5_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(item5NumberOf.Text);
            if (n > 10)
                n = 10;

            if (!IsStorageFull(4, n))
                AddToStorage(RemoveItemFromInventory(4, n));
        }

        private bool IsStorageFull(int n, int i)
        {
            if (BuildingToAddIn.NumOfItemsInStorage < 10)
                return false; 

            foreach (Item item in BuildingToAddIn.Storage)
                if (item.Name == inventoryLabels[n].Text || item.NamePlural == inventoryLabels[n].Text)
                    if (item.Number < 99 - i)
                        return false;
            return true;
        }

        private bool IsInventoryFull(int n, int i)
        {
            if (hero.NumOfItemsInInventory < 5)
                return false;

            foreach (Item item in hero.Inventory)
                if (item.Name == storageLabels[n].Text || item.NamePlural == storageLabels[n].Text)
                    if (item.Number < 99 - i)
                        return false;
            return true;
        }

        private Item RemoveItemFromStorage(int n, int i)
        {
            Item itemTmp2 = new Item();
            foreach (Item item in BuildingToAddIn.Storage)
            {
                if (item.Name == storageLabels[n].Text || item.NamePlural == storageLabels[n].Text)
                {
                    item.Number -= i;
                    if (item.Number <= 0)
                    {
                        BuildingToAddIn.Storage.Remove(item);
                        BuildingToAddIn.NumOfItemsInStorage--;
                    }
                    object o = item.Clone();
                    Item itemTmp = (Item)o;
                    itemTmp.Number = i;
                    return itemTmp;
                }
            }
            return itemTmp2;
        }

        private void pbStorageArrow01_Click(object sender, EventArgs e)
        {
            if (!IsInventoryFull(0, 1))
                hero.addToInventory(RemoveItemFromStorage(0, 1));
            InventoryLoad();
            StorageLoad();
        }

        private void pbStorageArrow02_Click(object sender, EventArgs e)
        {
            if (!IsInventoryFull(1, 1))
                hero.addToInventory(RemoveItemFromStorage(1, 1));
            InventoryLoad();
            StorageLoad();
        }

        private void pbStorageArrow03_Click(object sender, EventArgs e)
        {
            if (!IsInventoryFull(2, 1))
                hero.addToInventory(RemoveItemFromStorage(2, 1));
            InventoryLoad();
            StorageLoad();
        }

        private void pbStorageArrow04_Click(object sender, EventArgs e)
        {
            if (!IsInventoryFull(3, 1))
                hero.addToInventory(RemoveItemFromStorage(1, 1));
            InventoryLoad();
            StorageLoad();
        }

        private void pbStorageArrow05_Click(object sender, EventArgs e)
        {
            if (!IsInventoryFull(4, 1))
                hero.addToInventory(RemoveItemFromStorage(4, 1));
            InventoryLoad();
            StorageLoad();
        }

        private void pbStorageArrow06_Click(object sender, EventArgs e)
        {
            if (!IsInventoryFull(5, 1))
                hero.addToInventory(RemoveItemFromStorage(5, 1));
            InventoryLoad();
            StorageLoad();
        }

        private void pbStorageArrow07_Click(object sender, EventArgs e)
        {
            if (!IsInventoryFull(6, 1))
                hero.addToInventory(RemoveItemFromStorage(6, 1));
            InventoryLoad();
            StorageLoad();
        }

        private void pbStorageArrow08_Click(object sender, EventArgs e)
        {
            if (!IsInventoryFull(7, 1))
                hero.addToInventory(RemoveItemFromStorage(7, 1));
            InventoryLoad();
            StorageLoad();
        }

        private void pbStorageArrow09_Click(object sender, EventArgs e)
        {
            if (!IsInventoryFull(8, 1))
                hero.addToInventory(RemoveItemFromStorage(8, 1));
            InventoryLoad();
            StorageLoad();
        }

        private void pbStorageArrow010_Click(object sender, EventArgs e)
        {
            if (!IsInventoryFull(9, 1))
                hero.addToInventory(RemoveItemFromStorage(9, 1));
            InventoryLoad();
            StorageLoad();
        }

        private void pbStorageArrow1_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(StorageNumOfItem1.Text);
            if (n > 10)
                n = 10;

            if (!IsInventoryFull(0, n))
                hero.addToInventory(RemoveItemFromStorage(0, n));
            InventoryLoad();
            StorageLoad();
        }

        private void pbStorageArrow2_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(StorageNumOfItem2.Text);
            if (n > 10)
                n = 10;

            if (!IsInventoryFull(1, n))
                hero.addToInventory(RemoveItemFromStorage(1, n));
            InventoryLoad();
            StorageLoad();
        }

        private void pbStorageArrow3_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(StorageNumOfItem3.Text);
            if (n > 10)
                n = 10;

            if (!IsInventoryFull(2, n))
                hero.addToInventory(RemoveItemFromStorage(2, n));
            InventoryLoad();
            StorageLoad();
        }

        private void pbStorageArrow4_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(StorageNumOfItem4.Text);
            if (n > 10)
                n = 10;

            if (!IsInventoryFull(3, n))
                hero.addToInventory(RemoveItemFromStorage(3, n));
            InventoryLoad();
            StorageLoad();
        }

        private void pbStorageArrow5_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(StorageNumOfItem5.Text);
            if (n > 10)
                n = 10;

            if (!IsInventoryFull(4, n))
                hero.addToInventory(RemoveItemFromStorage(4, n));
            InventoryLoad();
            StorageLoad();
        }

        private void pbStorageArrow6_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(StorageNumOfItem6.Text);
            if (n > 10)
                n = 10;

            if (!IsInventoryFull(5, n))
                hero.addToInventory(RemoveItemFromStorage(5, n));
            InventoryLoad();
            StorageLoad();
        }

        private void pbStorageArrow7_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(StorageNumOfItem7.Text);
            if (n > 10)
                n = 10;

            if (!IsInventoryFull(6, n))
                hero.addToInventory(RemoveItemFromStorage(6, n));
            InventoryLoad();
            StorageLoad();
        }

        private void pbStorageArrow8_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(StorageNumOfItem8.Text);
            if (n > 10)
                n = 10;

            if (!IsInventoryFull(7, n))
                hero.addToInventory(RemoveItemFromStorage(7, n));
            InventoryLoad();
            StorageLoad();
        }

        private void pbStorageArrow9_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(StorageNumOfItem9.Text);
            if (n > 10)
                n = 10;

            if (!IsInventoryFull(8, n))
                hero.addToInventory(RemoveItemFromStorage(8, n));
            InventoryLoad();
            StorageLoad();
        }

        private void pbStorageArrow10_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(StorageNumOfItem10.Text);
            if (n > 10)
                n = 10;

            if (!IsInventoryFull(9, n))
                hero.addToInventory(RemoveItemFromStorage(9, n));
            InventoryLoad();
            StorageLoad();
        }
    }
}
