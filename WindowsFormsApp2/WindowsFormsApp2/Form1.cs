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
using Microsoft.Xna.Framework.Input;
using MonoGame.Utilities.Png;

namespace Farmio

{
    public partial class Farmio : Form
    {

        private Hero hero;
        private int ClickX;
        private int ClickY;
        Map map = new Map();
        bool start = false;
        string str;
        Task<int> longRunningTask;
        List<Label> inventoryLabels = new List<Label>();
        List<Label> storageLabels = new List<Label>();
        List<PictureBox> inventoryIcons = new List<PictureBox>();
        List<PictureBox> StorageIcons = new List<PictureBox>();
        List<PictureBox> CraftingIcons = new List<PictureBox>();
        List<Label> CraftingLabels = new List<Label>();
        List<Label> FurnaceLabels = new List<Label>();
        List<PictureBox> FurnaceIcons = new List<PictureBox>();
        List<Image> windows = new List<Image>();
        int hour;
        int minutes;
        System.Windows.Forms.Timer TimeOfDayTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer InventoryPlusTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer OneSecondTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer fishingTimer = new System.Windows.Forms.Timer();
        int windowFrame = 0;
        bool StorageIsOpen = false;
        bool Crafting = false;
        Map.MapMainPoint.Building CurrentBuilding;
        Map.MapMainPoint.PlowedSoil CurrentPlowedSoil;
        Item.CraftableItem.Furnace CurrentFurnace;
        bool heroHasShovel = true; // tmp
        bool Seeding = false;
        bool Fishing = false;
        bool Baking = false;
        bool FurnaceIsOpen;
        Item.CraftableItem craftedItem = new Item.CraftableItem(1);
        Item.BakedItem itemToBake = new Item.BakedItem(1);
        List<Item.CraftableItem.Furnace> Furnaces = new List<Item.CraftableItem.Furnace>();


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
            fishingTimer.Interval = 5000;
            fishingTimer.Tick += new EventHandler(fishingTimer_Tick);
            OneSecondTimer.Interval = 1000;
            OneSecondTimer.Tick += new EventHandler(OneSecondTimer_Tick);
            InventoryPlusTimer.Interval = 1000;
            InventoryPlusTimer.Tick += new EventHandler(InventoryPlusTimer_Tick);
            OneSecondTimer.Start();
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
            inventoryIcons.Add(v);
            inventoryIcons.Add(pbInventoryArrow2);
            inventoryIcons.Add(pbInventoryArrow3);
            inventoryIcons.Add(pbInventoryArrow4);
            inventoryIcons.Add(pbInventoryArrow5);
            inventoryIcons.Add(pbInventoryArrow01);
            inventoryIcons.Add(pbInventoryArrow02);
            inventoryIcons.Add(pbInventoryArrow03);
            inventoryIcons.Add(pbInventoryArrow04);
            inventoryIcons.Add(pbInventoryArrow05);
            inventoryIcons.Add(pbInventoryAnvil1);
            inventoryIcons.Add(pbInventoryAnvil2);
            inventoryIcons.Add(pbInventoryAnvil3);
            inventoryIcons.Add(pbInventoryAnvil4);
            inventoryIcons.Add(pbInventoryAnvil5);
            inventoryIcons.Add(pbInventorySeed1);
            inventoryIcons.Add(pbInventorySeed2);
            inventoryIcons.Add(pbInventorySeed3);
            inventoryIcons.Add(pbInventorySeed4);
            inventoryIcons.Add(pbInventorySeed5);
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
            StorageIcons.Add(pbStorageAnvil1);
            StorageIcons.Add(pbStorageAnvil2);
            StorageIcons.Add(pbStorageAnvil3);
            StorageIcons.Add(pbStorageAnvil4);
            StorageIcons.Add(pbStorageAnvil5);
            StorageIcons.Add(pbStorageAnvil6);
            StorageIcons.Add(pbStorageAnvil7);
            StorageIcons.Add(pbStorageAnvil8);
            StorageIcons.Add(pbStorageAnvil9);
            StorageIcons.Add(pbStorageAnvil10);
            CraftingIcons.Add(pbCraftingItem1);
            CraftingIcons.Add(pbCraftingItem2);
            CraftingIcons.Add(pbCraftingItem3);
            CraftingIcons.Add(pbCraftingItem4);
            CraftingLabels.Add(lblCraftingItem1);
            lblCraftingItem4.Text = "!!!";
            CraftingLabels.Add(lblCraftingItem2);
            CraftingLabels.Add(lblCraftingItem3);
            CraftingLabels.Add(lblCraftingItem4);
            CraftingLabels.Add(CraftingNumOfItem1);
            CraftingLabels.Add(CraftingNumOfItem2);
            CraftingLabels.Add(CraftingNumOfItem3);
            CraftingLabels.Add(CraftingNumOfItem4);
            FurnaceLabels.Add(lblFurnace1);
            FurnaceLabels.Add(lblFurnace2);
            FurnaceLabels.Add(lblFurnace3);
            FurnaceLabels.Add(lblFurnace4);
            FurnaceLabels.Add(lblFurnace1Num);
            FurnaceLabels.Add(lblFurnace2Num);
            FurnaceLabels.Add(lblFurnace3Num);
            FurnaceLabels.Add(lblFurnace4Num);
            FurnaceIcons.Add(pbFurnace01Arrow);
            FurnaceIcons.Add(pbFurnace02Arrow);
            FurnaceIcons.Add(pbFurnace03Arrow);
            FurnaceIcons.Add(pbFurnace04Arrow);
            FurnaceIcons.Add(pbFurnace1Arrow);
            FurnaceIcons.Add(pbFurnace2Arrow);
            FurnaceIcons.Add(pbFurnace3Arrow);
            FurnaceIcons.Add(pbFurnace4Arrow);
            pbFon.Image = map.DrawMap();

            for (int i = 1; i <= 20; i++)
            {
                windows.Add(Image.FromFile(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "Resources\\window" + i.ToString() + ".png"))); 
            }
            Parentize();

            pbHero.Image = (Image)hero.Sprite[4];
            pbHero.Location = new Point(hero.x, hero.y);
            pictureBox1.Image = (Bitmap)global::WindowsFormsApp2.Properties.Resources.ResourceManager.GetObject(SomeFunctions.MainPictureRandomize());
           // string startupPath = System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "Resources\\", SomeFunctions.MusicRandomize());
            //SoundPlay(startupPath);
            lblGold.Text = hero.Gold.ToString();
            lblDay.Text = hero.Day.ToString();
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
            pbFurnaceArrow.Parent = pbGoFishing.Parent = pbToHarvest.Parent = pbGetWater.Parent = pbName.Parent = pbCDTree.Parent = pbGetStone.Parent = pbCutGrass.Parent = pbCDStump.Parent = pbGetMushroom.Parent = pbGetMushrooms.Parent = pbStorage.Parent = pbGoToSleep.Parent = pbCraftSmth.Parent = pbNameOk.Parent = pbGLEF.Parent = pbInventoryOpen.Parent = pbCraftingArrow.Parent = pbToPlow.Parent = pbSowSeeds.Parent = pbToWater.Parent = pbFon;
            lblInventory.Parent = lblGold.Parent = lblDay.Parent = lblEnergy.Parent = lblSaturation.Parent = lblHour.Parent = label1.Parent = label2.Parent = label3.Parent = label4.Parent = label5.Parent = lblInventoryPlus.Parent = pbGLEF;
            Window.Parent = pbFon;
            pbHero.Parent = Window;
        }

        void musicTimer_Tick(object sender, EventArgs e)
        {
            //SoundPlay(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "Resources\\", SomeFunctions.MusicRandomize()));
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
                    hero.Day++;
                    lblDay.Text = hero.Day.ToString();
                }
            }
        }

        private void lblDay_TextChanged(object sender, EventArgs e)
        {
            if (hero.Day == 1)
            {
                NPC Elodie = new NPC();
                Elodie.Name = "Elodie";
                Elodie.Gender = true;
                Elodie.Gold = 50;
                Elodie.x = 0;
                Elodie.y = 200;
                Elodie.SetSprite(2);
                PictureBox pbElodie = new PictureBox();
                pbElodie.Image = (Image)Elodie.Sprite[4];
                pbElodie.Location = new Point(Elodie.x, Elodie.y);
                pbElodie.BackColor = System.Drawing.Color.Transparent;
                pbElodie.Size = new System.Drawing.Size(100, 50);
                pbElodie.TabStop = false;
                pbElodie.BringToFront();
                pbElodie.Parent = Window;
                pbElodie.Visible = true;
                NPCMoveHere(Elodie, pbElodie, hero.x, hero.y, 30);
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
            if (pbStorageIsOpen.Visible)
            {
                if (FurnaceIsOpen)
                    FurnaceClose();
                if (Crafting)
                    CraftingClose();
                StorageClose();
            }
            else if (pbInventoryOpen.Visible)
                InventoryClose();
            else if (Fishing)
            {
                Fishing = false;
                fishingTimer.Stop();
            }
            else if (Seeding)
                Seeding = false;

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
                        pbCraftSmth.Visible = true;
                        pbCraftSmth.Location = new Point(ClickX + 5, ClickY + 5 + 38);
                        pbGoFishing.Visible = pbToHarvest.Visible = pbGetWater.Visible = pbSowSeeds.Visible = pbToWater.Visible = pbToPlow.Visible = pbCDTree.Visible = pbGetStone.Visible = pbCDStump.Visible = pbGetMushrooms.Visible = pbGetMushroom.Visible = false;
                    }

                    else if (tmp is Map.MapMainPoint.Grass)
                    {
                        pbCutGrass.Location = new Point(ClickX + 5, ClickY + 5);
                        pbCutGrass.Visible = true;
                        pbGoFishing.Visible = pbToHarvest.Visible = pbGetWater.Visible = pbSowSeeds.Visible = pbToWater.Visible = pbToPlow.Visible = pbCraftSmth.Visible = pbGoToSleep.Visible = pbStorage.Visible = pbCDTree.Visible = pbGetStone.Visible = pbCDStump.Visible = pbGetMushrooms.Visible = pbGetMushroom.Visible = false;
                    }

                    else if (tmp is Map.MapMainPoint.Stone)
                    {
                        pbGetStone.Location = new Point(ClickX + 5, ClickY + 5);
                        pbGetStone.Visible = true;
                        pbGoFishing.Visible = pbToHarvest.Visible = pbGetWater.Visible = pbSowSeeds.Visible = pbToWater.Visible = pbToPlow.Visible = pbCraftSmth.Visible = pbGoToSleep.Visible = pbStorage.Visible = pbCutGrass.Visible = pbCDTree.Visible = pbCDStump.Visible = pbGetMushrooms.Visible = pbGetMushroom.Visible = false;
                    }

                    else if (tmp is Map.MapMainPoint.Tree)
                    {
                        pbCDTree.Location = new Point(ClickX + 5, ClickY + 5);
                        pbCDTree.Visible = true;
                        pbGoFishing.Visible = pbToHarvest.Visible = pbGetWater.Visible = pbSowSeeds.Visible = pbToWater.Visible = pbToPlow.Visible = pbCraftSmth.Visible = pbGoToSleep.Visible = pbStorage.Visible = pbCutGrass.Visible = pbGetStone.Visible = pbCDStump.Visible = pbGetMushrooms.Visible = pbGetMushroom.Visible = false;
                    }

                    else if (tmp is Map.MapMainPoint.Stump)
                    {
                        pbCDStump.Location = new Point(ClickX + 5, ClickY + 5);
                        pbCDStump.Visible = true;
                        pbGoFishing.Visible = pbToHarvest.Visible = pbGetWater.Visible = pbSowSeeds.Visible = pbToWater.Visible = pbToPlow.Visible = pbCraftSmth.Visible = pbGoToSleep.Visible = pbStorage.Visible = pbCutGrass.Visible = pbCDTree.Visible = pbGetStone.Visible = pbGetMushrooms.Visible = pbGetMushroom.Visible = false;
                    }

                    else if (tmp is Map.MapMainPoint.Mushroom)
                    {
                        if (tmp.Name[2] == '1')
                        {
                            pbGetMushroom.Location = new Point(ClickX + 5, ClickY + 5);
                            pbGetMushroom.Visible = true;
                            pbGoFishing.Visible = pbToHarvest.Visible = pbGetWater.Visible = pbSowSeeds.Visible = pbToWater.Visible = pbToPlow.Visible = pbCraftSmth.Visible = pbGoToSleep.Visible = pbStorage.Visible = pbCutGrass.Visible = pbCDTree.Visible = pbGetStone.Visible = pbGetMushrooms.Visible = pbCDStump.Visible = false;
                        }

                        else
                        {
                            pbGetMushrooms.Location = new Point(ClickX + 5, ClickY + 5);
                            pbGetMushrooms.Visible = true;
                            pbGoFishing.Visible = pbToHarvest.Visible = pbGetWater.Visible = pbSowSeeds.Visible = pbToWater.Visible = pbToPlow.Visible = pbCraftSmth.Visible = pbGoToSleep.Visible = pbStorage.Visible = pbCutGrass.Visible = pbCDTree.Visible = pbGetStone.Visible = pbGetMushroom.Visible = pbCDStump.Visible = false;
                        }
                    }

                    else if (tmp is Map.MapMainPoint.PlowedSoil)
                    {
                        if (!((Map.MapMainPoint.PlowedSoil)tmp).isSowed)
                        {
                            pbSowSeeds.Location = new Point(ClickX + 5, ClickY + 5);
                            pbSowSeeds.Visible = true;
                            pbGoFishing.Visible = pbToHarvest.Visible = pbGetWater.Visible = pbToPlow.Visible = pbCraftSmth.Visible = pbGoToSleep.Visible = pbStorage.Visible = pbCutGrass.Visible = pbCDTree.Visible = pbGetStone.Visible = pbGetMushroom.Visible = pbCDStump.Visible = false;
                        }
                        else if (((Map.MapMainPoint.PlowedSoil)tmp).stageOfGrowth == 4)
                        {
                            pbToHarvest.Location = new Point(ClickX + 5, ClickY + 5);
                            pbToHarvest.Visible = true;
                            pbGoFishing.Visible = pbSowSeeds.Visible = pbGetWater.Visible = pbToPlow.Visible = pbCraftSmth.Visible = pbGoToSleep.Visible = pbStorage.Visible = pbCutGrass.Visible = pbCDTree.Visible = pbGetStone.Visible = pbGetMushroom.Visible = pbCDStump.Visible = false;
                        }
                        else if (((Map.MapMainPoint.PlowedSoil)tmp).stageOfWatering < 2)
                        {
                            foreach (Item item in hero.Inventory)
                            {
                                if (item is Item.CraftableItem.Bucket && ((Item.CraftableItem.Bucket)item).hasWater)
                                {
                                    pbToWater.Location = new Point(ClickX + 5, ClickY + 5);
                                    pbToWater.Visible = true;
                                    break;
                                }
                            }
                            pbGoFishing.Visible = pbToHarvest.Visible = pbGetWater.Visible = pbSowSeeds.Visible = pbToPlow.Visible = pbCraftSmth.Visible = pbGoToSleep.Visible = pbStorage.Visible = pbCutGrass.Visible = pbCDTree.Visible = pbGetStone.Visible = pbGetMushroom.Visible = pbCDStump.Visible = false;
                        }
                    }

                    else if (tmp is Map.MapMainPoint.Pond)
                    {
                        int i = 0;
                        foreach (Item item in hero.Inventory)
                        {
                            if (item is Item.CraftableItem.Bucket)
                            {
                                pbGetWater.Location = new Point(ClickX + 5, ClickY + i + 5);
                                pbGetWater.Visible = true;
                                i += 18;
                            }

                            if (item is Item.CraftableItem.FishingRod)
                            {
                                pbGoFishing.Location = new Point(ClickX + 5, ClickY + i + 5);
                                pbGoFishing.Visible = true;
                                i += 18;
                            }
                        }
                        pbToHarvest.Visible = pbSowSeeds.Visible = pbToWater.Visible = pbToPlow.Visible = pbCraftSmth.Visible = pbGoToSleep.Visible = pbStorage.Visible = pbCutGrass.Visible = pbCDTree.Visible = pbGetStone.Visible = pbCDStump.Visible = pbGetMushrooms.Visible = pbGetMushroom.Visible = false;
                    }
                }

                else if (e.Button == MouseButtons.Left)
                {
                    pbGoFishing.Visible = pbGetWater.Visible = pbSowSeeds.Visible = pbToWater.Visible = pbToPlow.Visible = pbCraftSmth.Visible = pbGoToSleep.Visible = pbStorage.Visible = pbCutGrass.Visible = pbCDTree.Visible = pbGetStone.Visible = pbCDStump.Visible = pbGetMushrooms.Visible = pbGetMushroom.Visible = false;
                    longRunningTask = HeroMoveHere(ClickX, ClickY, 0);
                }
                else if (e.Button == MouseButtons.Right)
                {
                    foreach (Item item in hero.Inventory)
                    {
                        if (item is Item.CraftableItem.Shovel)
                        {
                            pbToPlow.Location = new Point(ClickX + 5, ClickY + 5);
                            pbToPlow.Visible = true;
                            break;
                        }
                    }
                     pbGoFishing.Visible = pbGetWater.Visible = pbSowSeeds.Visible = pbToWater.Visible = pbCraftSmth.Visible = pbGoToSleep.Visible = pbStorage.Visible = pbCutGrass.Visible = pbCDTree.Visible = pbGetStone.Visible = pbCDStump.Visible = pbGetMushrooms.Visible = pbGetMushroom.Visible = false;
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
                    lblInventoryPlus.Text = '+' + hero.AddToInventory(tmp.DestroyTree(map)).ToString();
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
                    lblInventoryPlus.Text = '+' + hero.AddToInventory(tmp.DestroyStone(map)).ToString();
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
                    lblInventoryPlus.Text = '+' + hero.AddToInventory(tmp.DestroyGrass(map)).ToString();
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
                    lblInventoryPlus.Text = '+' + hero.AddToInventory(tmp.DestroyStump(map)).ToString();
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
                        lblInventoryPlus.Text = '+' + hero.AddToInventory(tmp.DestroyMushroomNE(map)).ToString();
                    else
                        lblInventoryPlus.Text = '+' + hero.AddToInventory(tmp.DestroyMushroom(map)).ToString();
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
                        lblInventoryPlus.Text = '+' + hero.AddToInventory(tmp.DestroyMushroomNE(map)).ToString();
                    else
                        lblInventoryPlus.Text = '+' + hero.AddToInventory(tmp.DestroyMushroom(map)).ToString();
                    pbFon.Image = map.DrawMap();
                    lblEnergy.Text = hero.Energy.ToString() + "/200";
                }
            }
        }

        private void tbName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
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

        public async Task<int> NPCMoveHere(NPC npc, PictureBox pb, int x, int y, int xyplus)
        {
            char c = ' ';
            SoundPlayer player = new SoundPlayer(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "Resources\\footsteps1.wav"));
            if (xyplus == 0)
                xyplus = 5;
            int i = 0;
            int n = 0;
            
            player.PlayLooping();
            while (npc.x - x > xyplus)
            {
                c = 'a';
                while (!map.isFree(npc.x, npc.y, c))
                {
                    int min1 = 0, min2 = 0;
                    while (!map.isFree(npc.x, npc.y - min1, c))
                        min1++;
                    while (!map.isFree(npc.x, npc.y + min2, c))
                        min2++;
                    int posY = npc.y;
                    if (min1 > min2)
                        while (npc.y < posY + min2)
                        {
                            if (i > 5 || i < 2)
                                i = 3;
                            pb.Image = npc.Sprite[i];
                            i++;
                            npc.y += 5;
                            pb.Top = npc.y;
                            await Task.Delay(35 + n);
                        }
                    else
                        while (npc.y > posY - min2)
                        {
                            if (i > 2)
                                i = 0;
                            pb.Image = npc.Sprite[i];
                            i++;
                            npc.y -= 5;
                            pb.Top = npc.y;
                            await Task.Delay(35 + n);
                        }
                }
                if (i > 8 || i < 6)
                    i = 6;
                pb.Image = npc.Sprite[i];
                i++;
                npc.x -= 5;
                pb.Left = npc.x;
                await Task.Delay(35 + n);
            }
            while (x - npc.x > xyplus)
            {
                c = 'd';
                while (!map.isFree(npc.x, npc.y, c))
                {
                    int min1 = 0, min2 = 0;
                    while (!map.isFree(npc.x, npc.y - min1, c))
                        min1++;
                    while (!map.isFree(npc.x, npc.y + min2, c))
                        min2++;
                    int posY = npc.y;
                    if (min1 > min2)
                        while (npc.y < posY + min2)
                        {
                            if (i > 5 || i < 2)
                                i = 3;
                            pb.Image = npc.Sprite[i];
                            i++;
                            npc.y += 5;
                            pb.Top = npc.y;
                            await Task.Delay(35 + n);
                        }
                    else
                        while (npc.y > posY - min2)
                        {
                            if (i > 2)
                                i = 0;
                            pb.Image = npc.Sprite[i];
                            i++;
                            npc.y -= 5;
                            pb.Top = npc.y;
                            await Task.Delay(35 + n);
                        }
                }
                if (i > 11 || i < 9)
                    i = 9;
                pb.Image = npc.Sprite[i];
                i++;
                npc.x += 5;
                pb.Left = npc.x;
                await Task.Delay(35 + n);
            }
            while (npc.y - y > xyplus)
            {
                c = 'w';
                while (!map.isFree(npc.x, npc.y, c))
                {
                    int min1 = 0, min2 = 0;
                    while (!map.isFree(npc.x - min1, npc.y, c))
                        min1++;
                    while (!map.isFree(npc.x + min2, npc.y, c))
                        min2++;
                    int posX = npc.x;
                    if (min1 > min2)
                        while (npc.x < posX + min2)
                        {
                            if (i > 11 || i < 9)
                                i = 9;
                            pb.Image = npc.Sprite[i];
                            i++;
                            npc.x += 5;
                            pb.Left = npc.x;
                            await Task.Delay(35 + n);
                        }
                    else
                        while (npc.x > posX - min2)
                        {
                            if (i > 8 || i < 6)
                                i = 6;
                            pb.Image = npc.Sprite[i];
                            i++;
                            npc.x -= 5;
                            pb.Left = npc.x;
                            await Task.Delay(35 + n);
                        }
                }
                if (i > 2)
                    i = 0;
                pb.Image = npc.Sprite[i];
                i++;
                npc.y -= 5;
                pb.Top = npc.y;
                await Task.Delay(35 + n);
            }
            while (y - npc.y > xyplus)
            {
                c = 's';
                while (!map.isFree(npc.x, npc.y, c))
                {
                    int min1 = 0, min2 = 0;
                    while (!map.isFree(npc.x - min1, npc.y, c))
                        min1++;
                    while (!map.isFree(npc.x + min2, npc.y, c))
                        min2++;
                    int posX = npc.x;
                    if (min1 > min2)
                        while (npc.x < posX + min2)
                        {
                            if (i > 11 || i < 9)
                                i = 9;
                            pb.Image = npc.Sprite[i];
                            i++;
                            npc.x += 5;
                            pb.Left = npc.x;
                            await Task.Delay(35 + n);
                        }
                    else
                        while (npc.x > posX - min2)
                        {
                            if (i > 8 || i < 6)
                                i = 6;
                            pb.Image = npc.Sprite[i];
                            i++;
                            npc.x -= 5;
                            pb.Left = npc.x;
                            await Task.Delay(35 + n);
                        }
                }
                if (i > 5 || i < 2)
                    i = 3;
                pb.Image = npc.Sprite[i];
                i++;
                npc.y += 5;
                pb.Top = npc.y;
                await Task.Delay(35 + n);
            }
            if (c == 'w')
                pb.Image = npc.Sprite[0];
            else if (c == 's')
                pb.Image = npc.Sprite[3];
            else if (c == 'a')
                pb.Image = npc.Sprite[6];
            else if (c == 'd')
                pb.Image = npc.Sprite[9];
            player.Stop();
            return 0;
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
        //    pbHero.Top = hero.y;
        //}

        private void lblInventory_Click(object sender, EventArgs e)
        {
            InventoryLoad(true);
        }

        private void InventoryLoad(bool sound)
        {
            if (sound)
                SoundPlay(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "Resources\\inventory.wav"));
            InventoryClose();
            pbInventoryOpen.Visible = true;
            int i = 0;
            foreach (Item item in hero.Inventory)
            {
                if (item.Number == 1)
                    inventoryLabels[i].Text = item.Name;
                else
                    inventoryLabels[i].Text = item.NamePlural;
                inventoryLabels[i].Visible = true;
                inventoryLabels[i + 5].Text = " " + item.Number.ToString();
                inventoryLabels[i + 5].Visible = true;
                if (!StorageIsOpen && !Crafting && !Seeding)
                {
                    inventoryIcons[i].Visible = true;
                    if (item.IsUsable)
                        inventoryIcons[i + 5].Visible = true;
                }
                else if (StorageIsOpen && !Crafting && !Seeding)
                {
                    inventoryIcons[i + 10].Visible = true;
                    inventoryIcons[i + 15].Visible = true;
                }
                else if (Crafting && !Seeding)
                {
                    inventoryIcons[i + 20].Visible = true;
                }

                else if (Seeding)
                {
                    if (item is Item.Food.Seed)
                        inventoryIcons[i + 25].Visible = true;
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
                    if (!item.IsStacking)
                    {
                        hero.Inventory.RemoveAt(hero.Inventory.IndexOf(item));
                        hero.NumOfItemsInInventory--;
                        i = 1;
                    }
                    else
                    {
                        item.Number -= i;
                        if (item.Number <= 0)
                        {
                            hero.Inventory.RemoveAt(hero.Inventory.IndexOf(item));
                            hero.NumOfItemsInInventory--;
                        }
                    }
                    InventoryLoad(false); 
                    object o = item.Clone();
                    Item itemTmp = (Item)o;
                    itemTmp.Number = i;
                    return itemTmp;
                }
            }
            return itemTmp2;
        }

        public Item CopyItemFromInventory(int n)
        {
            Item itemTmp2 = new Item();
            foreach (Item item in hero.Inventory)
            {
                if (item.Name == inventoryLabels[n].Text || item.NamePlural == inventoryLabels[n].Text)
                {
                    object o = item.Clone();
                    Item itemTmp = (Item)o;
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
        public void Eat(Item.BakedItem.BakedFood food, int n)
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
                    else if (item is Item.BakedItem.BakedFood)
                        Eat((Item.BakedItem.BakedFood)item, i);
                    break;
                }
            }
        }

        private async void pbGoToSleep_Click(object sender, EventArgs e)
        {
            TimeOfDayTimer.Stop();
            pbGoToSleep.Visible = false;
            pbStorage.Visible = false;
            pbCraftSmth.Visible = false;
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
            minutes = 00;
            TimeOfDayTimer_Tick(null, null);
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

        void OneSecondTimer_Tick(object sender, EventArgs e)
        {
            pbFon.Image = map.DrawMap();

            if (Baking)
            {
                foreach (Item.CraftableItem.Furnace furnace in Furnaces)
                {
                    if (furnace.Result.TimeForBaking > 0)
                    {
                        furnace.TimeForBaking -= 5;
                        if (furnace.TimeForBaking <= 0)
                        {
                            furnace.Result.Number++;
                            furnace.TimeForBaking = furnace.Result.TimeForBaking;
                            furnace.Fuel.Number--;
                            bool stop = false;
                            if (furnace.Fuel.Number <= 0)
                            {
                                furnace.Fuel = null;
                                stop = true;
                            }
                            foreach (Item item in furnace.ItemsInFurnace)
                            {
                                item.Number--;
                                if (item.Number <= 0)
                                {
                                    furnace.ItemsInFurnace.Remove(item);
                                    furnace.NumOfItemsInFurnace--;
                                    stop = true;
                                    if (furnace.NumOfItemsInFurnace <= 0)
                                        break;
                                }
                            }
                            if (FurnaceIsOpen && CurrentFurnace == furnace)
                                FurnaceOpen(CurrentFurnace);
                            if (stop)
                            {
                                Furnaces.Remove(furnace);
                                
                                if (Furnaces.Count() == 0)
                                    Baking = false;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private async void pbStorage_Click(object sender, EventArgs e)
        {
            pbGoToSleep.Visible = false;
            pbStorage.Visible = false;
            pbCraftSmth.Visible = false;
            int x = map.mapTab[ClickX, ClickY].X;
            int y = map.mapTab[ClickX, ClickY].Y;
            longRunningTask = HeroMoveHere(x + 48, y + 84, 0);
            int result = await longRunningTask;
            CurrentBuilding = (Map.MapMainPoint.Building)map.mapTab[x, y];
            StorageLoad();
        }

        private void StorageLoad()
        {
            StorageClose();
            StorageIsOpen = true;
            pbStorageIsOpen.Visible = true;
            int i = 0;
            foreach (Item item in CurrentBuilding.Storage)
            {
                if (item.Number == 0)
                    continue;
                if (!Crafting)
                {
                    if (item is Item.CraftableItem.Furnace)
                    {
                        StorageIcons[i + 20].Visible = true;
                        StorageIcons[i + 20].BringToFront();
                    }
                    else
                    {
                        StorageIcons[i].Visible = true;
                        StorageIcons[i].BringToFront();
                    }
                    StorageIcons[i + 10].Visible = true;
                    StorageIcons[i + 10].BringToFront();
                }
                else
                {
                    StorageIcons[i + 20].Visible = true;
                    StorageIcons[i + 20].BringToFront();
                }
                storageLabels[i].Text = (item.Number == 1) ? item.Name : item.NamePlural;
                storageLabels[i].Visible = true;
                storageLabels[i].BringToFront();
                storageLabels[i + 10].Text = item.Number.ToString();
                storageLabels[i + 10].Visible = true;
                storageLabels[i + 10].BringToFront();
                i++;
            }
            InventoryLoad(false);

        }
        private void StorageClose()
        {
            StorageIsOpen = false;
            int i = 0;
            foreach (Label label in storageLabels)
            {
                if (label.Text.Length == 0)
                    break;
                StorageIcons[i].Visible = false;
                StorageIcons[i + 10].Visible = false;
                StorageIcons[i + 20].Visible = false;
                label.Visible = false;
                storageLabels[i + 10].Visible = false;
                i++;
            }

            InventoryClose();
            pbStorageIsOpen.Visible = false;
        }

        private void AddToStorage(Item ItemToAdd)
        {
            if (ItemToAdd.Number != 0)
                CurrentBuilding.AddToStorage(ItemToAdd);
            int i = 0;
            foreach (Item item in CurrentBuilding.Storage)
            {
                storageLabels[i].Text = (item.Number == 1) ? item.Name : item.NamePlural; 
                i++;
            }
            StorageLoad();
        }

        private void InventoryArrowClick(int i, int n)
        {
            Item item = CopyItemFromInventory(i);
            bool IsInFurnace = false;
            if (FurnaceIsOpen && (item.IsFuel || item.IsBakeable))
            {
                if (!IsFurnaceFullInventory(i, n))
                {
                    AddInFurnace(RemoveItemFromInventory(i, n));
                    IsInFurnace = true;
                }
            }
            if (!IsStorageFull(inventoryLabels[i].Text, n) && !IsInFurnace)
                AddToStorage(RemoveItemFromInventory(i, n));
        }

        private void pbInventoryArrow01_Click(object sender, EventArgs e)
        {
            InventoryArrowClick(0, 1);
        }

        private void pbInventoryArrow02_Click(object sender, EventArgs e)
        {
            InventoryArrowClick(1, 1);
        }

        private void pbInventoryArrow03_Click(object sender, EventArgs e)
        {
            InventoryArrowClick(2, 1);
        }

        private void pbInventoryArrow04_Click(object sender, EventArgs e)
        {
            InventoryArrowClick(3, 1);
        }

        private void pbInventoryArrow05_Click(object sender, EventArgs e)
        {
            InventoryArrowClick(4, 1);
        }

        private void pbInventoryArrow1_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(item1NumberOf.Text);
            if (n > 10)
                n = 10;

            InventoryArrowClick(0, n);
        }

        private void pbInventoryArrow2_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(item2NumberOf.Text);
            if (n > 10)
                n = 10;

            InventoryArrowClick(1, n);
        }

        private void pbInventoryArrow3_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(item3NumberOf.Text);
            if (n > 10)
                n = 10;

            InventoryArrowClick(2, n);
        }

        private void pbInventoryArrow4_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(item4NumberOf.Text);
            if (n > 10)
                n = 10;

            InventoryArrowClick(3, n);
        }

        private void pbInventoryArrow5_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(item5NumberOf.Text);
            if (n > 10)
                n = 10;

            InventoryArrowClick(4, n);
        }

        private bool IsStorageFull(string name, int i)
        {
            if (CurrentBuilding.NumOfItemsInStorage < 10)
                return false; 

            foreach (Item item in CurrentBuilding.Storage)
                if (item.Name == name || item.NamePlural == name)
                    //inventoryLabels[n].Text
                    if (item.Number < 99 - i)
                        return false;
            return true;
        }
        private bool IsStorageFullCrafting(int n, int i)
        {
            if (CurrentBuilding.NumOfItemsInStorage < 10)
                return false;


            foreach (Item item in CurrentBuilding.Storage)
                if (item.Name == CurrentBuilding.CraftingTable[n].Name || item.NamePlural == CurrentBuilding.CraftingTable[n].NamePlural)
                    if (item.Number < 99 - i)
                        return false;
            return true;
        }

        private bool IsInventoryFull(string name, int i)
        {
            if (hero.NumOfItemsInInventory < 5)
                return false;

            foreach (Item item in hero.Inventory)
                if (item.Name == name || item.NamePlural == name) 
                    if (item.Number < 99 - i)
                        return false;
            return true;
        }

        private Item RemoveItemFromStorage(int n, int i)
        {
            Item itemTmp2 = new Item();
            foreach (Item item in CurrentBuilding.Storage)
            {
                if (item.Name == storageLabels[n].Text || item.NamePlural == storageLabels[n].Text)
                {
                    item.Number -= i;
                    if (item.Number <= 0)
                    {
                        CurrentBuilding.Storage.Remove(item);
                        CurrentBuilding.NumOfItemsInStorage--;
                    }
                    object o = item.Clone();
                    Item itemTmp = (Item)o;
                    itemTmp.Number = i;
                    return itemTmp;
                }
            }
            return itemTmp2;
        }

        private Item CopyItemFromStorage(int n)
        {
            Item itemTmp2 = new Item();
            foreach (Item item in CurrentBuilding.Storage)
            {
                if (item.Name == storageLabels[n].Text || item.NamePlural == storageLabels[n].Text)
                {
                    object o = item.Clone();
                    Item itemTmp = (Item)o;
                    return itemTmp;
                }
            }
            return itemTmp2;
        }

        private void StorageArrowClick(int i, int n)
        {
            Item item = RemoveItemFromStorage(i, n);
            bool IsInFurnace = false;
            if (FurnaceIsOpen)
            {
                if (item.IsFuel)
                {
                    IsInFurnace = true;
                    if (CurrentFurnace.Fuel != null && CurrentFurnace.Fuel.GetType() == item.GetType())
                        CurrentFurnace.Fuel.Number += n;
                    else if (CurrentFurnace.Fuel == null)
                        CurrentFurnace.Fuel = item;
                    else
                        IsInFurnace = false;
                }
                else if (item.IsBakeable)
                {
                    IsInFurnace = true;
                    if (CurrentFurnace.Raw != null && CurrentFurnace.Raw.GetType() == item.GetType())
                        CurrentFurnace.Raw.Number += n;
                    else if (CurrentFurnace.Raw == null)
                        CurrentFurnace.Raw = item;
                    else
                        IsInFurnace = false;
                }
                FurnaceOpen(CurrentFurnace);
            }
            if (!IsInventoryFull(storageLabels[i].Text, n) && !IsInFurnace)
                hero.AddToInventory(item);
            StorageLoad();
        }

        private void pbStorageArrow01_Click(object sender, EventArgs e)
        {
            StorageArrowClick(0, 1);
        }

        private void pbStorageArrow02_Click(object sender, EventArgs e)
        {
            StorageArrowClick(1, 1);
        }

        private void pbStorageArrow03_Click(object sender, EventArgs e)
        {
            StorageArrowClick(2, 1);
        }

        private void pbStorageArrow04_Click(object sender, EventArgs e)
        {
            StorageArrowClick(3, 1);
        }

        private void pbStorageArrow05_Click(object sender, EventArgs e)
        {
            StorageArrowClick(4, 1);
        }

        private void pbStorageArrow06_Click(object sender, EventArgs e)
        {
            StorageArrowClick(5, 1);
        }

        private void pbStorageArrow07_Click(object sender, EventArgs e)
        {
            StorageArrowClick(6, 1);
        }

        private void pbStorageArrow08_Click(object sender, EventArgs e)
        {
            StorageArrowClick(7, 1);
        }

        private void pbStorageArrow09_Click(object sender, EventArgs e)
        {
            StorageArrowClick(8, 1);
        }

        private void pbStorageArrow010_Click(object sender, EventArgs e)
        {
            StorageArrowClick(9, 1);
        }

        private void pbStorageArrow1_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(StorageNumOfItem1.Text);
            if (n > 10)
                n = 10;

            StorageArrowClick(0, n);
        }

        private void pbStorageArrow2_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(StorageNumOfItem2.Text);
            if (n > 10)
                n = 10;

            StorageArrowClick(1, n);
        }

        private void pbStorageArrow3_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(StorageNumOfItem3.Text);
            if (n > 10)
                n = 10;

            StorageArrowClick(2, n);
        }

        private void pbStorageArrow4_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(StorageNumOfItem4.Text);
            if (n > 10)
                n = 10;

            StorageArrowClick(3, n);
        }

        private void pbStorageArrow5_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(StorageNumOfItem5.Text);
            if (n > 10)
                n = 10;

            StorageArrowClick(4, n);
        }

        private void pbStorageArrow6_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(StorageNumOfItem6.Text);
            if (n > 10)
                n = 10;

            StorageArrowClick(5, n);
        }

        private void pbStorageArrow7_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(StorageNumOfItem7.Text);
            if (n > 10)
                n = 10;

            StorageArrowClick(6, n);
        }

        private void pbStorageArrow8_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(StorageNumOfItem8.Text);
            if (n > 10)
                n = 10;

            StorageArrowClick(7, n);
        }

        private void pbStorageArrow9_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(StorageNumOfItem9.Text);
            if (n > 10)
                n = 10;

            StorageArrowClick(8, n);
        }

        private void pbStorageArrow10_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(StorageNumOfItem10.Text);
            if (n > 10)
                n = 10;

            StorageArrowClick(9, n);
        }

        private async void pbCraftSmth_Click(object sender, EventArgs e)
        {
            pbGoToSleep.Visible = false;
            pbStorage.Visible = false;
            pbCraftSmth.Visible = false;
            int x = map.mapTab[ClickX, ClickY].X;
            int y = map.mapTab[ClickX, ClickY].Y;
            longRunningTask = HeroMoveHere(x + 48, y + 84, 0);
            int result = await longRunningTask;
            CurrentBuilding = (Map.MapMainPoint.Building)map.mapTab[x, y];
            CraftingLoad();
        }

        private void CraftingLoad()
        {
            if (Crafting)
                CraftingClose();
            Crafting = true;
            StorageLoad();
            pbCraftingTable.Visible = true;
            pbCraftingArrow.Visible = true;
            pbCraftingResult.Visible = true;
            int i = 0;
            foreach (Item item in CurrentBuilding.CraftingTable)
            {
                if (item.Number == 0)
                    continue;
                CraftingLabels[i].Text = (item.Number == 1) ? item.Name : item.NamePlural;
                CraftingLabels[i].Visible = true;
                CraftingLabels[i].BringToFront();
                CraftingLabels[i + 4].Text = item.Number.ToString();
                CraftingLabels[i + 4].Visible = true;
                CraftingLabels[i + 4].BringToFront();
                CraftingIcons[i].Visible = true;
                CraftingIcons[i].BringToFront();
                i++;
            }
            lblItemToCraft.Visible = true;
            lblItemToCraft.BringToFront();
        }

        private void CraftingClose()
        {
            Crafting = false;
            pbCraftingTable.Visible = false;
            pbCraftingArrow.Visible = false;
            pbCraftingResult.Visible = false;
            int i = 0;
            foreach (PictureBox picture in CraftingIcons)
            {
                picture.Visible = false;
                CraftingLabels[i].Visible = false;
                CraftingLabels[i + 4].Visible = false;
                i++;
            }
            lblItemToCraft.Visible = false;
            StorageClose();
        }
        private void AddOnCraftingTable(Item ItemToAdd)
        {
            if (ItemToAdd.Number != 0)
                CurrentBuilding.AddOnCraftingTable(ItemToAdd);
            int i = 0;
            foreach (Item item in CurrentBuilding.CraftingTable)
            {
                CraftingLabels[i].Text = (item.Number == 1) ? item.Name : item.NamePlural;
                i++;
            }
            foreach (Item.CraftableItem craftableItem in hero.ListOfCraftableItems)
            {
                if ((CurrentBuilding.CraftingTable.Count == craftableItem.ItemsNeededForCraft.Count))
                {
                    bool isThatItem = true;
                    int n = 0;
                    foreach (Item itemOnTable in CurrentBuilding.CraftingTable)
                    {
                        foreach (Item itemNeeded in craftableItem.ItemsNeededForCraft)
                        {
                            isThatItem = true;
                            if (itemOnTable.GetType() == itemNeeded.GetType())
                            {
                                n++;
                                if (itemOnTable.Number != itemNeeded.Number)
                                {
                                    isThatItem = false;
                                    break;
                                }
                            }
                        }

                    }
                    if (isThatItem && n == craftableItem.ItemsNeededForCraft.Count)
                    {
                        lblItemToCraft.Text = craftableItem.Name;
                        craftedItem = craftableItem;
                        break;
                    }
                    else
                        lblItemToCraft.Text = "";
                }
            }
            CraftingLoad();
        }
        private bool IsCraftingTableFullInventory(int n, int i)
        {
            if (CurrentBuilding.NumOfItemsOnTable < 4)
                return false;

            foreach (Item item in CurrentBuilding.CraftingTable)
                if (item.Name == inventoryLabels[n].Text || item.NamePlural == inventoryLabels[n].Text)
                    if (item.Number < 99 - i)
                        return false;
            return true;
        }
        private bool IsCraftingTableFullStorage(int n, int i)
        {
            if (CurrentBuilding.NumOfItemsOnTable < 4)
                return false;

            foreach (Item item in CurrentBuilding.CraftingTable)
                if (item.Name == storageLabels[n].Text || item.NamePlural == storageLabels[n].Text)
                    if (item.Number < 99 - i)
                        return false;
            return true;
        }

        private void pbInventoryAnvil1_Click(object sender, EventArgs e)
        {
            if (!IsCraftingTableFullInventory(0, 1))
                AddOnCraftingTable(RemoveItemFromInventory(0, 1));
        }

        private void pbInventoryAnvil2_Click(object sender, EventArgs e)
        {
            if (!IsCraftingTableFullInventory(1, 1))
                AddOnCraftingTable(RemoveItemFromInventory(1, 1));
        }

        private void pbInventoryAnvil3_Click(object sender, EventArgs e)
        {
            if (!IsCraftingTableFullInventory(2, 1))
                AddOnCraftingTable(RemoveItemFromInventory(2, 1));
        }

        private void pbInventoryAnvil4_Click(object sender, EventArgs e)
        {
            if (!IsCraftingTableFullInventory(3, 1))
                AddOnCraftingTable(RemoveItemFromInventory(3, 1));
        }

        private void pbInventoryAnvil5_Click(object sender, EventArgs e)
        {
            if (!IsCraftingTableFullInventory(4, 1))
                AddOnCraftingTable(RemoveItemFromInventory(4, 1));
        }

        private void pbStorageAnvil1_Click(object sender, EventArgs e)
        {
            if (lblStorageItem1.Text == "Piec")
                FurnaceOpen((Item.CraftableItem.Furnace)CurrentBuilding.Storage[0]);
            else if (!IsCraftingTableFullStorage(0, 1))
                AddOnCraftingTable(RemoveItemFromStorage(0, 1));
        }

        private void pbStorageAnvil2_Click(object sender, EventArgs e)
        {
            if (lblStorageItem2.Text == "Piec")
                FurnaceOpen((Item.CraftableItem.Furnace)CurrentBuilding.Storage[1]);
            else if (!IsCraftingTableFullStorage(1, 1))
                AddOnCraftingTable(RemoveItemFromStorage(1, 1));
        }

        private void pbStorageAnvil3_Click(object sender, EventArgs e)
        {
            if (lblStorageItem3.Text == "Piec")
                FurnaceOpen((Item.CraftableItem.Furnace)CurrentBuilding.Storage[2]);
            else if (!IsCraftingTableFullStorage(2, 1))
                AddOnCraftingTable(RemoveItemFromStorage(2, 1));
        }

        private void pbStorageAnvil4_Click(object sender, EventArgs e)
        {
            if (lblStorageItem4.Text == "Piec")
                FurnaceOpen((Item.CraftableItem.Furnace)CurrentBuilding.Storage[3]);
            else if (!IsCraftingTableFullStorage(3, 1))
                AddOnCraftingTable(RemoveItemFromStorage(3, 1));
        }

        private void pbStorageAnvil5_Click(object sender, EventArgs e)
        {
            if (lblStorageItem5.Text == "Piec")
                FurnaceOpen((Item.CraftableItem.Furnace)CurrentBuilding.Storage[4]);
            else if (!IsCraftingTableFullStorage(4, 1))
                AddOnCraftingTable(RemoveItemFromStorage(4, 1));
        }

        private void pbStorageAnvil6_Click(object sender, EventArgs e)
        {
            if (lblStorageItem6.Text == "Piec")
                FurnaceOpen((Item.CraftableItem.Furnace)CurrentBuilding.Storage[5]);
            else if (!IsCraftingTableFullStorage(5, 1))
                AddOnCraftingTable(RemoveItemFromStorage(5, 1));
        }

        private void pbStorageAnvil7_Click(object sender, EventArgs e)
        {
            if (lblStorageItem7.Text == "Piec")
                FurnaceOpen((Item.CraftableItem.Furnace)CurrentBuilding.Storage[6]);
            else if (!IsCraftingTableFullStorage(6, 1))
                AddOnCraftingTable(RemoveItemFromStorage(6, 1));
        }

        private void pbStorageAnvil8_Click(object sender, EventArgs e)
        {
            if (lblStorageItem8.Text == "Piec")
                FurnaceOpen((Item.CraftableItem.Furnace)CurrentBuilding.Storage[7]);
            else if (!IsCraftingTableFullStorage(7, 1))
                AddOnCraftingTable(RemoveItemFromStorage(7, 1));
        }

        private void pbStorageAnvil9_Click(object sender, EventArgs e)
        {
            if (lblStorageItem9.Text == "Piec")
                FurnaceOpen((Item.CraftableItem.Furnace)CurrentBuilding.Storage[8]);
            else if (!IsCraftingTableFullStorage(8, 1))
                AddOnCraftingTable(RemoveItemFromStorage(8, 1));
        }

        private void pbStorageAnvil10_Click(object sender, EventArgs e)
        {
            if (lblStorageItem1.Text == "Piec")
                FurnaceOpen((Item.CraftableItem.Furnace)CurrentBuilding.Storage[9]);
            else if (!IsCraftingTableFullStorage(9, 1))
                AddOnCraftingTable(RemoveItemFromStorage(9, 1));
        }

        private void FurnaceOpen(Item.CraftableItem.Furnace furnace)
        {
            FurnaceClose();
            FurnaceIsOpen = true;
            CurrentFurnace = furnace;
            pbFurnace.Visible = pbFurnaceFuel.Visible = pbFurnaceArrow.Visible = pbFurnaceResult.Visible = true;

            int i = 0;

            if (CurrentFurnace.Fuel != null)
            {
                lblFurnaceFuel.Text = (CurrentFurnace.Fuel.Number == 1) ? CurrentFurnace.Fuel.Name : CurrentFurnace.Fuel.NamePlural;
                lblFurnaceFuelNum.Text = CurrentFurnace.Fuel.Number.ToString();
                lblFurnaceFuel.Visible = lblFurnaceFuelNum.Visible = true;
                pbFurnaceFuelArrow.Visible = pbFurnace0FuelArrow.Visible = true;
                pbFurnaceFuelArrow.BringToFront();
                pbFurnace0FuelArrow.BringToFront();
                lblFurnaceFuel.BringToFront();
                lblFurnaceFuelNum.BringToFront();
            }
            if (CurrentFurnace.Result != null)
            {
                furnace.TimeForBaking = furnace.Result.TimeForBaking;
                lblFurnaceResult.Text = (CurrentFurnace.Result.Number < 2) ? CurrentFurnace.Result.Name : CurrentFurnace.Result.NamePlural;
                lblFurnaceResNum.Text = CurrentFurnace.Result.Number.ToString();
                lblFurnaceResult.Visible = lblFurnaceResNum.Visible = true;
                pbFurnace0ResArrow.Visible = pbFurnaceResArrow.Visible = true;
                pbFurnaceResArrow.BringToFront();
                pbFurnace0ResArrow.BringToFront();
                lblFurnaceResult.BringToFront();
                lblFurnaceResNum.BringToFront();
            }

            foreach (Item item in CurrentFurnace.ItemsInFurnace)
            {
                if (item.Number == 0)
                    continue;
                FurnaceLabels[i].Text = (item.Number == 1) ? item.Name : item.NamePlural;
                FurnaceLabels[i].Visible = true;
                FurnaceLabels[i].BringToFront();
                FurnaceLabels[i + 4].Text = item.Number.ToString();
                FurnaceLabels[i + 4].Visible = true;
                FurnaceLabels[i + 4].BringToFront();
                FurnaceIcons[i].Visible = true;
                FurnaceIcons[i].BringToFront();
                FurnaceIcons[i + 4].Visible = true;
                FurnaceIcons[i + 4].BringToFront();
                i++;
            }

        }

        private void FurnaceClose()
        {
            FurnaceIsOpen = false;
            foreach (Label label in FurnaceLabels)
            {
                label.Text = "";
                label.Visible = false;
            }
            foreach (PictureBox pictureBox in FurnaceIcons)
                pictureBox.Visible = false;
            pbFurnace.Visible = pbFurnaceFuel.Visible = pbFurnaceArrow.Visible = pbFurnaceResult.Visible = lblFurnaceFuel.Visible = lblFurnaceFuelNum.Visible = lblFurnaceResult.Visible = lblFurnaceResNum.Visible = pbFurnace0ResArrow.Visible = pbFurnaceResArrow.Visible = pbFurnaceFuelArrow.Visible = pbFurnace0FuelArrow.Visible = false;
            lblFurnaceFuel.Text = lblFurnaceFuelNum.Text = lblFurnaceResult.Text = lblFurnaceResNum.Text  = "";
        }
        private Item RemoveItemFromCraftingTable(int n)
        {
            Item itemTmp2 = new Item();
            foreach (Item item in CurrentBuilding.CraftingTable)
            {
                if (item.Name == CraftingLabels[n].Text || item.NamePlural == CraftingLabels[n].Text)
                {
                    int i = item.Number;
                    CurrentBuilding.CraftingTable.Remove(item);
                    CurrentBuilding.NumOfItemsOnTable--;
                    object o = item.Clone();
                    Item itemTmp = (Item)o;
                    itemTmp.Number = i;
                    return itemTmp;
                }
            }
            foreach (Item.CraftableItem craftableItem in hero.ListOfCraftableItems)
            {
                if ((CurrentBuilding.CraftingTable.Count == craftableItem.ItemsNeededForCraft.Count))
                {
                    bool isThatItem = true;
                    int j = 0;
                    foreach (Item itemOnTable in CurrentBuilding.CraftingTable)
                    {
                        foreach (Item itemNeeded in craftableItem.ItemsNeededForCraft)
                        {
                            isThatItem = true;
                            if (itemOnTable.GetType() == itemNeeded.GetType())
                            {
                                j++;
                                if (itemOnTable.Number != itemNeeded.Number)
                                {
                                    isThatItem = false;
                                    break;
                                }
                            }
                        }

                    }
                    if (isThatItem && n == craftableItem.ItemsNeededForCraft.Count)
                    {
                        lblItemToCraft.Text = craftableItem.Name;
                        break;
                    }
                    else
                        lblItemToCraft.Text = "";
                }
            }
            return itemTmp2;
        }

        private void pbCraftingItem1_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(CraftingNumOfItem1.Text);
            if (!IsStorageFull(lblCraftingItem1.Text, n))
                AddToStorage(RemoveItemFromCraftingTable(0));
            else if (!IsInventoryFull(CraftingLabels[0].Text, n))
                hero.AddToInventory(RemoveItemFromCraftingTable(0));
            CraftingLoad();
        }

        private void pbCraftingItem2_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(CraftingNumOfItem2.Text);

            if (!IsStorageFull(lblCraftingItem2.Text, n))
                AddToStorage(RemoveItemFromCraftingTable(1));
            else if (!IsInventoryFull(CraftingLabels[1].Text, n))
                hero.AddToInventory(RemoveItemFromCraftingTable(1));
            CraftingLoad();
        }

        private void pbCraftingItem3_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(CraftingNumOfItem3.Text);
            if (!IsStorageFull(lblCraftingItem3.Text, n))
                AddToStorage(RemoveItemFromCraftingTable(2));
            else if (!IsInventoryFull(CraftingLabels[2].Text, n))
                hero.AddToInventory(RemoveItemFromCraftingTable(2));
            CraftingLoad();
        }

        private void pbCraftingItem4_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(CraftingNumOfItem4.Text);
            if (!IsStorageFull(lblCraftingItem4.Text, n))
                AddToStorage(RemoveItemFromCraftingTable(3));
            else if (!IsInventoryFull(CraftingLabels[3].Text, n))
                hero.AddToInventory(RemoveItemFromCraftingTable(3));
            CraftingLoad();
        }

        private async void pbToPlow_Click(object sender, EventArgs e)
        {
            if (heroHasShovel)
            {
                pbToPlow.Visible = false;
                if (hero.Energy <= 0)
                    Console.WriteLine("Musisz cos zjesc i odpoczac :(");
                else
                {
                    Map.MapMainPoint.Tree tmp = (Map.MapMainPoint.Tree)map.mapTab[ClickX, ClickY];
                    int EnergyTmp = hero.Energy;
                    EnergyTmp -= 5;
                    if (EnergyTmp < 0) Console.WriteLine("Musisz cos zjesc i odpoczac :(");
                    else
                    {
                        longRunningTask = HeroMoveHere(ClickX, ClickY, 30);
                        int result = await longRunningTask;
                        await Task.Delay(300);
                        SoundPlay(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "Resources\\digging.wav"));
                        hero.Energy = EnergyTmp;
                        map.MakeObjectRightHere(ClickX, ClickY, "ps1", map, false);
                        pbFon.Image = map.DrawMap();
                        lblEnergy.Text = hero.Energy.ToString() + "/200";
                    }
                }
            }
        }

        private async void pbSowSeeds_Click(object sender, EventArgs e)
        {
            pbSowSeeds.Visible = pbToWater.Visible = false;
            Seeding = true;
            int x = map.mapTab[ClickX, ClickY].X;
            int y = map.mapTab[ClickX, ClickY].Y;
            longRunningTask = HeroMoveHere(x + 27, y + 21, 0);
            int result = await longRunningTask;
            CurrentPlowedSoil = (Map.MapMainPoint.PlowedSoil)map.mapTab[x, y];
            InventoryLoad(true);
        }

        private void pbInventorySeed1_Click(object sender, EventArgs e)
        {
            SowSeed(0);
        }

        private void pbInventorySeed2_Click(object sender, EventArgs e)
        {
            SowSeed(1);
        }

        private void pbInventorySeed3_Click(object sender, EventArgs e)
        {
            SowSeed(2);
        }

        private void pbInventorySeed4_Click(object sender, EventArgs e)
        {
            SowSeed(3);
        }

        private void pbInventorySeed5_Click(object sender, EventArgs e)
        {
            SowSeed(4);
        }

        private void SowSeed(int n)
        {
            if (CurrentPlowedSoil.seed == null)
            {
                int i = Int32.Parse(inventoryLabels[n + 5].Text);
                if (i > 4) i = 4;
                CurrentPlowedSoil.seed = (Item.Seed)RemoveItemFromInventory(n, i);
            }

            Seeding = false;
            CurrentPlowedSoil.isSowed = true;
            InventoryClose();
            pbFon.Image = map.DrawMap();
        }

        private async void pbToWater_Click(object sender, EventArgs e)
        {
            pbSowSeeds.Visible = pbToWater.Visible = false;
            int x = map.mapTab[ClickX, ClickY].X;
            int y = map.mapTab[ClickX, ClickY].Y;
            longRunningTask = HeroMoveHere(x + 31, y + 26, 0);
            int result = await longRunningTask;
            SoundPlay(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "Resources\\watering.wav"));
            ((Map.MapMainPoint.PlowedSoil)map.mapTab[x, y]).stageOfWatering = 2;
            foreach (Item item in hero.Inventory)
            {
                if (item is Item.CraftableItem.Bucket)
                {
                    ((Item.CraftableItem.Bucket)item).hasWater = false;
                    ((Item.CraftableItem.Bucket)item).Name = "Wiadro";
                    break;
                }
            }
            pbFon.Image = map.DrawMap();
        }

        private async void pbGetWater_Click(object sender, EventArgs e)
        {
            pbGetWater.Visible = pbGoFishing.Visible = false;
            int x = map.mapTab[ClickX, ClickY].X;
            int y = map.mapTab[ClickX, ClickY].Y;
            longRunningTask = HeroMoveHere(x + 31, y + 26, 0);
            int result = await longRunningTask;
            SoundPlay(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "Resources\\getWater.wav"));
            foreach (Item item in hero.Inventory)
            {
                if (item is Item.CraftableItem.Bucket)
                {
                    ((Item.CraftableItem.Bucket)item).hasWater = true;
                }
            }
        }

        private void lblItemToCraft_Click(object sender, EventArgs e)
        {
            if (lblItemToCraft.Text != "")
            {
                object o = Activator.CreateInstance(craftedItem.GetType(), new object[] { craftedItem.Number });
                var item = (Item)o;
                hero.AddToInventory(item);
                lblItemToCraft.Text = "";
                for (int i = 0; i < 4; i++)
                    RemoveItemFromCraftingTable(i);
                CraftingLoad();
            }
        }

        private async void pbToHarvest_Click(object sender, EventArgs e)
        {
            pbToHarvest.Visible = false;
            int x = map.mapTab[ClickX, ClickY].X;
            int y = map.mapTab[ClickX, ClickY].Y;
            longRunningTask = HeroMoveHere(x + 27, y + 21, 0);
            int result = await longRunningTask;
            CurrentPlowedSoil = (Map.MapMainPoint.PlowedSoil)map.mapTab[x, y];
            hero.AddToInventory(CurrentPlowedSoil.Harvesting());
            pbFon.Image = map.DrawMap();
        }

        private void pbGoFishing_Click(object sender, EventArgs e)
        {
            pbGetWater.Visible = pbGoFishing.Visible = false; 
            int x = map.mapTab[ClickX, ClickY].X;
            int y = map.mapTab[ClickX, ClickY].Y;
            longRunningTask = HeroMoveHere(x + 31, y + 26, 0);
            InventoryLoad(true);
            Fishing = true;
            fishingTimer.Start();
        }

        void fishingTimer_Tick(object sender, EventArgs e)
        {
            Random random = new Random();
            int rand = random.Next(1, 500);
            if (rand < 107)
            {
                InventoryLoad(false);
                if (rand < 10)
                {
                    Item.Food.Fish.Carp fish = new Item.Food.Fish.Carp(1);
                    hero.AddToInventory(fish);
                }
                else if (rand < 11)
                {
                    Item.Food.Fish.GoldenCarp fish = new Item.Food.Fish.GoldenCarp(1);
                    hero.AddToInventory(fish);
                }
                else if (rand < 24)
                {
                    Item.Food.Fish.Pike fish = new Item.Food.Fish.Pike(1);
                    hero.AddToInventory(fish);
                }
                else if (rand < 44)
                {
                    Item.Food.Fish.Perch fish = new Item.Food.Fish.Perch(1);
                    hero.AddToInventory(fish);
                }
                else if (rand < 59)
                {
                    Item.Food.Fish.Asp fish = new Item.Food.Fish.Asp(1);
                    hero.AddToInventory(fish);
                }
                else if (rand < 67)
                {
                    Item.Food.Fish.Catfish fish = new Item.Food.Fish.Catfish(1);
                    hero.AddToInventory(fish);
                }
                else if (rand < 87)
                {
                    Item.Food.Fish.Roach fish = new Item.Food.Fish.Roach(1);
                    hero.AddToInventory(fish);
                }
                else
                {
                    Item.Food.Fish.Bream fish = new Item.Food.Fish.Bream(1);
                    hero.AddToInventory(fish);
                }
            }

        }

        private void lblItemToCraft_TextChanged(object sender, EventArgs e)
        {
            if (lblItemToCraft.Text != "")
                lblItemToCraft.Cursor = System.Windows.Forms.Cursors.Hand;
            else
                lblItemToCraft.Cursor = System.Windows.Forms.Cursors.Arrow;
        }

        private Item RemoveItemFromFurnace(int n, int i)
        {
            Item itemTmp2 = new Item();
            foreach (Item item in CurrentFurnace.ItemsInFurnace)
            {
                if (item.Name == FurnaceLabels[n].Text || item.NamePlural == FurnaceLabels[n].Text)
                {
                    item.Number -= i;
                    if (item.Number <= 0)
                    {
                        CurrentFurnace.ItemsInFurnace.Remove(item);
                        CurrentFurnace.NumOfItemsInFurnace--;
                    }
                    object o = item.Clone();
                    Item itemTmp = (Item)o;
                    itemTmp.Number = i;
                    return itemTmp;
                }
            }
            foreach (Item.BakedItem bakeableItems in hero.BakeableItems)
            {
                if ((CurrentFurnace.ItemsInFurnace.Count == bakeableItems.ItemsNeededForBaking.Count))
                {
                    bool isThatItem = true;
                    int j = 0;
                    foreach (Item itemInFurnace in CurrentFurnace.ItemsInFurnace)
                    {
                        foreach (Item itemNeeded in bakeableItems.ItemsNeededForBaking)
                        {
                            isThatItem = true;
                            if (itemInFurnace.GetType() == itemNeeded.GetType())
                            {
                                j++;
                                if (itemInFurnace.Number != itemNeeded.Number)
                                {
                                    isThatItem = false;
                                    break;
                                }
                            }
                        }

                    }
                    if (isThatItem && n == bakeableItems.ItemsNeededForBaking.Count)
                    {
                        lblItemToCraft.Text = bakeableItems.Name;
                        break;
                    }
                    else
                        lblItemToCraft.Text = "";
                }
            }
            return itemTmp2;
        }

        private void FurnaceArrowClick(int n, int i)
        {
            Item item = RemoveItemFromFurnace(n, i);
            FurnaceOpen(CurrentFurnace);
            if (!IsStorageFull(FurnaceLabels[n].Text, i))
                AddToStorage(item);
            else if (!IsInventoryFull(FurnaceLabels[n].Text, i))
                hero.AddToInventory(item);
        }
        private void pbFurnace01Arrow_Click(object sender, EventArgs e)
        {
            FurnaceArrowClick(0, 1);
        }
        private void pbFurnace02Arrow_Click(object sender, EventArgs e)
        {
            FurnaceArrowClick(1, 1);
        }
        private void pbFurnace03Arrow_Click(object sender, EventArgs e)
        {
            FurnaceArrowClick(2, 1);
        }
        private void pbFurnace04Arrow_Click(object sender, EventArgs e)
        {
            FurnaceArrowClick(3, 1);
        }

        private void pbFurnace1Arrow_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(lblFurnace1Num.Text);
            if (n > 10)
                n = 10;

            FurnaceArrowClick(0, n);
        }
        private void pbFurnace2Arrow_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(lblFurnace2Num.Text);
            if (n > 10)
                n = 10;

            FurnaceArrowClick(1, n);
        }
        private void pbFurnace3Arrow_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(lblFurnace3Num.Text);
            if (n > 10)
                n = 10;

            FurnaceArrowClick(2, n);
        }
        private void pbFurnace4Arrow_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(lblFurnace4Num.Text);
            if (n > 10)
                n = 10;

            FurnaceArrowClick(3, n);
        }

        private bool IsFurnaceFullInventory(int n, int i)
        {
            Item item = CopyItemFromInventory(n);
            if (item.IsBakeable)
            {
                if (CurrentFurnace.NumOfItemsInFurnace < 4)
                    return false;
                foreach (Item itemIn in CurrentFurnace.ItemsInFurnace)
                    if (itemIn.Name == inventoryLabels[n].Text || itemIn.NamePlural == inventoryLabels[n].Text)
                        if (item.Number < 99 - i)
                            return false;
            }
            else if (CurrentFurnace.Raw == null)
                return false;
            else if (CurrentFurnace.Number < 99 - i)
                return false;

            return true;
        }

        private bool IsFurnaceFullStorage(int n, int i)
        {
            Item item = CopyItemFromStorage(n);
            if (item.IsBakeable)
            {
                if (CurrentFurnace.NumOfItemsInFurnace < 4)
                    return false;
                foreach (Item itemIn in CurrentFurnace.ItemsInFurnace)
                    if (itemIn.Name == storageLabels[n].Text || itemIn.NamePlural == storageLabels[n].Text)
                        if (item.Number < 99 - i)
                            return false;
            }
            else if (CurrentFurnace.Raw == null)
                return false;
            else if (CurrentFurnace.Number < 99 - i)
                return false;

            return true;
        }

        private void AddInFurnace(Item ItemToAdd)
        {
            if (ItemToAdd.Number != 0)
                CurrentFurnace.AddInFurnace(ItemToAdd);
            int i = 0;
            //foreach (Item item in CurrentFurnace.ItemsInFurnace)
            //{
            //    FurnaceLabels[i].Text = (item.Number == 1) ? item.Name : item.NamePlural;
            //    i++;
            //}
            if (CurrentFurnace.Fuel != null)
            {
                foreach (Item.BakedItem bakedItem in hero.BakeableItems)
                {
                    if ((CurrentFurnace.ItemsInFurnace.Count == bakedItem.ItemsNeededForBaking.Count))
                    {
                        bool isThatItem = true;
                        int n = 0;
                        foreach (Item itemInFurnace in CurrentFurnace.ItemsInFurnace)
                        {
                            foreach (Item itemNeeded in bakedItem.ItemsNeededForBaking)
                            {
                                isThatItem = true;
                                if (itemInFurnace.GetType() == itemNeeded.GetType())
                                    n++;
                            }
                        }
                        if (isThatItem && n == bakedItem.ItemsNeededForBaking.Count)
                        {
                            lblFurnaceResult.Text = bakedItem.Name;
                            CurrentFurnace.Result = bakedItem;
                            CurrentFurnace.Result.Number = 0;
                            break;
                        }
                        else
                            lblFurnaceResult.Text = "";
                    }
                }
            }
            FurnaceOpen(CurrentFurnace);
        }

        public Item RemoveFuelFromFurnace(int i)
        {
            Item item = CurrentFurnace.Fuel;
            CurrentFurnace.Fuel.Number -= i;
            if (CurrentFurnace.Fuel.Number <= 0)
                CurrentFurnace.Fuel = null;

            item.Number = i;
            return item;
        }

        public Item RemoveResultFromFurnace(int i)
        {
            Item item = CurrentFurnace.Result;
            CurrentFurnace.Result.Number -= i;
            if (CurrentFurnace.Result.Number <= 0)
                CurrentFurnace.Result = null;

            item.Number = i;
            return item;
        }

        private void pbFurnace0FuelArrow_Click(object sender, EventArgs e)
        {
            Item item = RemoveFuelFromFurnace(1);
            FurnaceOpen(CurrentFurnace);
            if (!IsStorageFull(lblFurnaceFuel.Text, 1))
                AddToStorage(item);
            else if (!IsInventoryFull(lblFurnaceFuel.Text, 1))
                hero.AddToInventory(item);
        }

        private void pbFurnaceFuelArrow_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(lblFurnaceFuelNum.Text);
            if (n > 10)
                n = 10;

            Item item = RemoveFuelFromFurnace(n);
            FurnaceOpen(CurrentFurnace);
            if (!IsStorageFull(lblFurnaceFuel.Text, n))
                AddToStorage(item);
            else if (!IsInventoryFull(lblFurnaceFuel.Text, n))
                hero.AddToInventory(item);
        }

        private void lblFurnaceResult_TextChanged(object sender, EventArgs e)
        {
            if (this.Text != "")
                pbFurnaceArrow.Cursor = System.Windows.Forms.Cursors.Hand;
            else
                pbFurnaceArrow.Cursor = System.Windows.Forms.Cursors.Arrow;
        }

        private void pbFurnaceArrow_Click(object sender, EventArgs e)
        {
            if (lblFurnaceResult.Text != "")
            {
                Baking = true;
                Furnaces.Add(CurrentFurnace); 
                FurnaceOpen(CurrentFurnace);
            }
        }

        private void pbFurnace0ResArrow_Click(object sender, EventArgs e)
        {
            Item item = RemoveResultFromFurnace(1);
            FurnaceOpen(CurrentFurnace);
            if (!IsStorageFull(lblFurnaceResult.Text, 1))
                AddToStorage(item);
            else if (!IsInventoryFull(lblFurnaceResult.Text, 1))
                hero.AddToInventory(item);
        }

        private void pbFurnaceResArrow_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(lblFurnaceResNum.Text);
            if (n > 10)
                n = 10;

            Item item = RemoveResultFromFurnace(n);
            FurnaceOpen(CurrentFurnace);
            if (!IsStorageFull(lblFurnaceResult.Text, n))
                AddToStorage(item);
            else if (!IsInventoryFull(lblFurnaceResult.Text, n))
                hero.AddToInventory(item);
        }
    }
}
