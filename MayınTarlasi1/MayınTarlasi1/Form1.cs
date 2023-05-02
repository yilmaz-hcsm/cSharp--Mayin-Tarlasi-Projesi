using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MayınTarlasi1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        MayınTarlasi MayinTarlamiz;
        List<Mayin> mayinlarimiz;
        Image MayinResmi = Image.FromFile(@"mayin.png");
        Image BayrakResmi = Image.FromFile(@"bayrak.png");
        int bulunan_temiz_alan;
        int BayrakSayisi = 0;
        int SayaçVePuan = 0;
        bool sayacDur = false;
        int toplam_alan;
        



        private void YeniOyunButton_Click(object sender, EventArgs e)
        {
            checkedListBox1.Visible = true;
            panel1.Controls.Clear();
            
            YeniOyunBaslat();
            textBox1.Visible = false;
            pictureBox1.Visible = false;
            textBox2.Visible = false;
            pictureBox2.Visible = false;
            panel1.Visible = false;
            panel1.Enabled = true;

        }




        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (checkedListBox1.SelectedIndex == 0 || checkedListBox1.SelectedIndex == 1 || checkedListBox1.SelectedIndex == 2)
            {
                BaslaButton.Visible = true;
                checkedListBox1.Visible = false;
            }
        }




        private void BaslaButton_Click(object sender, EventArgs e)
        {
            BaslaButton.Visible = false;
            YeniOyunButton.Visible = false;
            YeniOyunBaslat();
            textBox1.Text = (": " + BayrakSayisi);
            textBox1.Visible = true;
            pictureBox1.Visible = true;
            textBox2.Visible = true;
            pictureBox2.Visible = true;
            panel1.Visible = true;
            SayaçVePuan = 0;
            sayacDur = false;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        /// <summary>
        /// ///////////////////////////////////////////
        /// </summary>
        public class Mayin
        {
            Point loc;
            bool dolu;
            bool bakildi;

            public Mayin(Point loca)
            {
                dolu = false;
                loc = loca;
            }

            public Point KonumAl
            {
                get { return loc; }
            }

            public bool mayinVarMi
            {
                get
                {
                    return dolu;
                }
                set
                {
                    dolu = value;
                }
            }

            public bool bakildi_
            {
                get
                {
                    return bakildi;
                }
                set
                {
                    bakildi = value;
                }
            }
        }


        //////////////////////////////////////////////////////


        class MayınTarlasi
        {
            Size buyukluk_;
            List<Mayin> mayinlar;
            int DoluMayinSayisi;
            Random rnd = new Random();





            public MayınTarlasi(Size buyukluk, int mayin_Sayisi)
            {
                mayinlar = new List<Mayin>();
                DoluMayinSayisi = mayin_Sayisi;
                buyukluk_ = buyukluk;
                for (int x = 0; x < buyukluk.Width; x = x + 60)
                {
                    for (int y = 0; y < buyukluk.Height; y = y + 60)
                    {
                        Mayin m = new Mayin(new Point(x, y));
                        MayinEkle(m);
                    }
                }

                MayinlariDoldur();
            }
            public void MayinEkle(Mayin m)
            {
                mayinlar.Add(m);
            }


            private void MayinlariDoldur()
            {
                int sayi = 0;
                while (sayi < DoluMayinSayisi)
                {
                    int i = rnd.Next(0, mayinlar.Count);
                    Mayin item = mayinlar[i];
                    if (item.mayinVarMi == false)
                    {
                        item.mayinVarMi = true;
                        sayi++;
                    }
                }
            }

            public Size buyuklugu
            {
                get
                {
                    return buyukluk_;
                }
            }
            public Mayin MayinAlLoc(Point loc)
            {
                foreach (Mayin item in mayinlar)
                {
                    if (item.KonumAl == loc)
                    {
                        return item;
                    }
                }
                return null;
            }
            public List<Mayin> GetAllMayin
            {
                get
                {
                    return mayinlar;
                }
            }
            public int ToplamMayinSayisi
            {
                get
                {
                    return DoluMayinSayisi;
                }
            }
           


        }

        ///////////////////////////

        public void MayinEkle()
        {
            for (int x = 0; x < panel1.Width; x = x + 60)
            {
                for (int y = 0; y < panel1.Height; y = y + 60)
                {
                    ButtonEkle(new Point(x, y));
                }
            }
        }

        public void ButtonEkle(Point loc)
        {
            Button btn = new Button();
            btn.Name = loc.X + "" + loc.Y;
            btn.Size = new Size(60,60);
            btn.Location = loc;
            btn.Click += new EventHandler(btn_Click);
            btn.MouseUp += new MouseEventHandler(btn_MouseUp);
            panel1.Controls.Add(btn);
        }

        void btn_MouseUp(object sender, MouseEventArgs e)
        {
            Button btn = (sender as Button);
            if (e.Button == MouseButtons.Right)
            {
                if (btn.BackgroundImage != BayrakResmi && BayrakSayisi>0)
                {
                   
                    btn.BackgroundImage = BayrakResmi;/////////////////sağ tıkla bayrak ekleme
                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                    BayrakSayisi--;
                    textBox1.Text = (": " + BayrakSayisi);
                    
                }
                else
                {
                    btn.BackgroundImage = null;
                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                        BayrakSayisi++;
                    textBox1.Text = (": " + BayrakSayisi);

                }
               

            }
           
        }

        public void btn_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);// hangi butona tıklandığında o nun adresini bize ver
            Mayin myn = MayinTarlamiz.MayinAlLoc(btn.Location);
            mayinlarimiz = new List<Mayin>();
            if (myn.mayinVarMi)
            {
                sayacDur = true;
                MessageBox.Show("Kaybettinz.");
                YeniOyunButton.Visible = true;
                Mayinlari_goster();
                panel1.Enabled = false;
            }

            else
            { 
                int s = EtraftaKacMayinVar(myn);
                if (s == 0)
                {

                    mayinlarimiz.Add(myn);
                    for (int i = 0; i < mayinlarimiz.Count; i++)
                    {
                        Mayin item = mayinlarimiz[i];
                        if (item != null)
                        {
                            if (item.bakildi_ == false && item.mayinVarMi == false)
                            {
                                Button btnx = (Button)panel1.Controls.Find(item.KonumAl.X + "" + item.KonumAl.Y, false)[0];
                                if (EtraftaKacMayinVar(mayinlarimiz[i]) == 0)
                                {

                                    btnx.Enabled = false;

                                    cevresindekileri_ekle(item);
                                }
                                else
                                {
                                    btnx.Text = EtraftaKacMayinVar(item).ToString();

                                }
                                bulunan_temiz_alan++;
                                item.bakildi_ = true;
                            }
                        }
                    }
                }
                else
                {
                    btn.Text = s.ToString();
                    bulunan_temiz_alan++;
                }

            }
            if (bulunan_temiz_alan >= toplam_alan - MayinTarlamiz.ToplamMayinSayisi)
            {
                sayacDur = true;
                MessageBox.Show("kazandiniz!!\n"+"Skorunuz:"+ SayaçVePuan);
                Mayinlari_goster();
            }

        }



        public int EtraftaKacMayinVar(Mayin m)
        {
            int sayi = 0;
            if (m.KonumAl.X > 0)
            {
                if (MayinTarlamiz.MayinAlLoc(new Point(m.KonumAl.X - 60, m.KonumAl.Y)).mayinVarMi)
                {
                    sayi++;
                }
            }
            if (m.KonumAl.Y <( panel1.Height -60) && m.KonumAl.X < (panel1.Width-60 ))   
            {
                if (MayinTarlamiz.MayinAlLoc(new Point(m.KonumAl.X + 60, m.KonumAl.Y + 60)).mayinVarMi)
                {
                    sayi++;

                }
            }
            
            if (m.KonumAl.X < panel1.Width - 60)
            {
                if (MayinTarlamiz.MayinAlLoc(new Point(m.KonumAl.X + 60, m.KonumAl.Y)).mayinVarMi)
                {
                    sayi++;
                }
            }
            if (m.KonumAl.X > 0 && m.KonumAl.Y > 0)
            {
                if (MayinTarlamiz.MayinAlLoc(new Point(m.KonumAl.X - 60, m.KonumAl.Y - 60)).mayinVarMi)
                {
                    sayi++;
                }
            }
            if (m.KonumAl.Y > 0)
            {
                if (MayinTarlamiz.MayinAlLoc(new Point(m.KonumAl.X, m.KonumAl.Y - 60)).mayinVarMi)
                {
                    sayi++;
                }
            }
            if (m.KonumAl.X > 0 && m.KonumAl.Y < panel1.Height - 60)
            {
                if (MayinTarlamiz.MayinAlLoc(new Point(m.KonumAl.X - 60, m.KonumAl.Y + 60)).mayinVarMi)
                {
                    sayi++;
                }
            }
            if (m.KonumAl.Y < panel1.Height - 60)
            {
                if (MayinTarlamiz.MayinAlLoc(new Point(m.KonumAl.X, m.KonumAl.Y + 60)).mayinVarMi)
                {
                    sayi++;
                }
            }
            if (m.KonumAl.X > panel1.Width - 60 && m.KonumAl.Y > 0)
            {
                if (MayinTarlamiz.MayinAlLoc(new Point(m.KonumAl.X + 60, m.KonumAl.Y - 60)).mayinVarMi)
                {
                    sayi++;
                }
            }

            return sayi;
        }


        public void cevresindekileri_ekle(Mayin m)
        {
            bool b1 = false;
            bool b2 = false;
            bool b3 = false;
            bool b4 = false;
            if (m.KonumAl.X > 0)
            {
                mayinlarimiz.Add(MayinTarlamiz.MayinAlLoc(new Point(m.KonumAl.X - 60, m.KonumAl.Y)));
                b1 = true;
            }
            if (m.KonumAl.Y > 0)
            {
                mayinlarimiz.Add(MayinTarlamiz.MayinAlLoc(new Point(m.KonumAl.X, m.KonumAl.Y - 60)));
                b2 = true;
            }
            if (m.KonumAl.X < panel1.Width)
            {
                mayinlarimiz.Add(MayinTarlamiz.MayinAlLoc(new Point(m.KonumAl.X + 60, m.KonumAl.Y)));
                b3 = true;
            }
            if (m.KonumAl.Y < panel1.Height)
            {
                mayinlarimiz.Add(MayinTarlamiz.MayinAlLoc(new Point(m.KonumAl.X, m.KonumAl.Y + 60)));
                b4 = true;
            }



            if (b1 && b2)
            {
                mayinlarimiz.Add(MayinTarlamiz.MayinAlLoc(new Point(m.KonumAl.X - 60, m.KonumAl.Y - 60)));
            }
            if (b1 && b4)
            {
                mayinlarimiz.Add(MayinTarlamiz.MayinAlLoc(new Point(m.KonumAl.X - 60, m.KonumAl.Y + 60)));
            }
            if (b2 && b3)
            {
                mayinlarimiz.Add(MayinTarlamiz.MayinAlLoc(new Point(m.KonumAl.X + 60, m.KonumAl.Y - 60)));
            }
            /////
            /* if(b3&& b4)
            {
                mayinlarimiz.Add(MayinTarlamiz.MayinAlLoc(new Point(m.KonumAl.X + 60, m.KonumAl.Y + 60)));
            }
            */
            if (b2 && b4)
            {
                mayinlarimiz.Add(MayinTarlamiz.MayinAlLoc(new Point(m.KonumAl.X , m.KonumAl.Y + 60)));
            }
            if(b3&& b4)
            {
                mayinlarimiz.Add(MayinTarlamiz.MayinAlLoc(new Point(m.KonumAl.X + 60, m.KonumAl.Y + 60)));
            }
            if (b1 && b3)
            {
                mayinlarimiz.Add(MayinTarlamiz.MayinAlLoc(new Point(m.KonumAl.X - 60, m.KonumAl.Y )));
            }
        }
        public void Mayinlari_goster()
        {
            foreach (Mayin item in MayinTarlamiz.GetAllMayin)
            {
                if (item.mayinVarMi)
                {
                    Button btn = (Button)panel1.Controls.Find(item.KonumAl.X + "" + item.KonumAl.Y, false)[0];
                    btn.BackgroundImage = MayinResmi;
                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
        }


        private void YeniOyunBaslat()
            {
                

                if (checkedListBox1.SelectedIndex == 0)
                {
                    MayinTarlamiz = new MayınTarlasi(new Size(600, 480), 10);
                    panel1.Size = MayinTarlamiz.buyuklugu;
                    bulunan_temiz_alan = 0;
                    BayrakSayisi = 10;
                    toplam_alan = 80;
                    MayinEkle();
                }
                if (checkedListBox1.SelectedIndex == 1)
                {
                    MayinTarlamiz = new MayınTarlasi(new Size(720,720),20 );
                    panel1.Size = MayinTarlamiz.buyuklugu;
                    bulunan_temiz_alan = 0;
                    BayrakSayisi = 20;
                    toplam_alan = 144;
                    MayinEkle();

                }

                if (checkedListBox1.SelectedIndex == 2)
                {
                    MayinTarlamiz = new MayınTarlasi(new Size(900, 720), 25);
                    panel1.Size = MayinTarlamiz.buyuklugu;
                    bulunan_temiz_alan = 0;
                    BayrakSayisi = 25;
                    toplam_alan = 180;
                    MayinEkle();
                }


            }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (sayacDur == false)
            {
                SayaçVePuan++;
                textBox2.Text = (": " + SayaçVePuan);
            }
        }
    }
}
