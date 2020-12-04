using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Altın_Toplama_Oyunu
{
    public partial class Form1 : Form
    {
        PictureBox[] altinlar = new PictureBox[80]; // Altınların tutulduğu dizi
        int[] altinMiktari = new int[80]; // Her altının miktarını tutan dizi
        Image[] altinResimler = new Image[4]; //her altının farklı resmi var onları tutan dizi
        int[] toplananAltinSayisi = new int[4]; // Toplanan Altın Sayıları
        public Form1()
        {
            
            InitializeComponent(); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            altinFonksiyon(); // altınları panele dağıtma
        }

        private void altinFonksiyon()
        {
            altinResimler[0] = Properties.Resources._5gold; // altınlara resim atama
            altinResimler[1] = Properties.Resources._10gold;
            altinResimler[2] = Properties.Resources._15gold;
            altinResimler[3] = Properties.Resources._20gold;

            Random sec = new Random();
            
            for(int i = 0; i<80; i++)
            {
                int sec1 = sec.Next(0, 4);
                if(sec1 == 0) // 0 gelirse 5 puanlık resim atanıyor
                {
                    altinMiktari[i] = 5;
                }
                else if(sec1 == 1) // 1 gelirse 10 puanlık resim atanıyor
                {
                    altinMiktari[i] = 10;
                }
                else if(sec1 == 2) // 2 gelirse 15 puanlık resim atanır
                {
                    altinMiktari[i] = 15;
                }
                else if(sec1 == 3) // 3 gelirse 20 puanlık resim atanır
                {
                    altinMiktari[i] = 20;
                }
                PictureBox altinEkle = new PictureBox(); // yeni nesne oluşturuyoruz
                altinEkle.Width = oyuncu1.Width;
                altinEkle.Height = oyuncu1.Height;
                altinEkle.Image = altinResimler[sec1];
                altinEkle.SizeMode = PictureBoxSizeMode.StretchImage;
                altinEkle.SendToBack();
                int s = 0;

                newKonum:
                int yatay = sec.Next(0, 19);
                int dikey = sec.Next(0, 19);
                altinEkle.Location = new Point((altinEkle.Width*yatay), (altinEkle.Height*dikey)); // altının rastgele konumu belirliyoruz
                altinlar[i] = altinEkle;

                while (s != i) // herhagi iki altına aynı konum atanmaz
                {
                   if(altinlar[s].Location == altinEkle.Location)
                    {
                        goto newKonum;
                    }
                    s++;
                }
                panel1.Controls.Add(altinEkle); // Arayüze altını ekliyoruz

                if(i >= 72) // altınların %10 u gizli olarak atanıyor
                {
                    altinEkle.Visible = false;
                    altinEkle.Image = Properties.Resources.gizli;
                }

            }

        }
        private void konumAta() // yeni konum atama fonksiyonu
        {
            Random rastgele = new Random();
            int yatay = rastgele.Next(0, 19);
            int dikey = rastgele.Next(0, 19);


        }
        int oyuncu1Puan = 200, oyuncu2Puan = 200, oyuncu3Puan = 200, oyuncu4Puan = 200; // oyuncu puanları
        int kasadakiAltin = 0;
        int bulunanGizliAltinSayisi = 0;

        int sezmeKonumX = 0,sezmeKonumY = 0; // D oyuncusunun diğerlerinin hareketini sezme değişkeni

        int ones1 = 0, ones2 = 0, ones3 = 0, ones4 = 0;
        bool kontroll = false;
        private void Konumaİlerle(PictureBox x) // Rastgele konum atama
        {
            Random rastgele = new Random();
            int konumx = x.Location.X;
            int konumy = x.Location.Y;
            bool hareket3 = false;

            int xx = 25, yy = 25;
            for (int i = 0; i < 80; i++)
            {

                for (int r = 1; r < 4; r++)
                {
                    xx = 25 * r;
                    if ((x.Location.X == altinlar[i].Location.X) && (x.Location.Y - xx == altinlar[i].Location.Y)) // yukarı
                    {
                        hareket3 = true;
                    }
                    else if ((x.Location.X == altinlar[i].Location.X) && (x.Location.Y + xx == altinlar[i].Location.Y)) // aşağı
                    {
                        hareket3 = true;
                    }
                    else if ((x.Location.X + xx == altinlar[i].Location.X) && (x.Location.Y == altinlar[i].Location.Y)) // sağ
                    {
                        hareket3 = true;
                    }
                    else if ((x.Location.X - xx == altinlar[i].Location.X) && (x.Location.Y == altinlar[i].Location.Y)) // sol
                    {
                        hareket3 = true;
                    }

                    if(hareket3)
                    {
                        x.Location = altinlar[i].Location; // altını alma
                        ones1 = i;
                        kontroll = true;
                        altinlar[i].Location = new Point(-500, -500); // altını form dışında bir konuma atama
                        panel1.Controls.Remove(altinlar[i]); // altını panelden silme
                        

                        break;
                    }

                }

                if (hareket3)
                {
                    break;
                }
            }


        geriGel: // goto etiketi
            int hareketEt = rastgele.Next(0, 4);

            bool rr = false;
            
        //    int xx = 75;

            
            if(hareket3 == false)
            {

                if (hareketEt == 0) // yukarı git
                {
                    if (konumy - 75 < 0)
                    {
                        goto geriGel;
                    }
                    else
                    {

                        for (int i = 0; i < 80; i++)
                        {
                            if ((x.Location.X == altinlar[i].Location.X) && (x.Location.Y - 50 == altinlar[i].Location.Y))
                            {
                                x.Location = new Point(konumx, konumy - 50); // 25 birim yukarı git
                                rr = true;
                                break;
                            }
                        }
                        if (rr == false)
                        {
                            x.Location = new Point(konumx, konumy - 75); // 25 birim yukarı git

                        }


                        if (x != oyuncu3)
                        {
                            gizliMi(x.Location.X, x.Location.Y);
                        }
                    }
                }
                else if (hareketEt == 1)
                {
                    if (konumy + 75 > 481) // aşağı git
                    {
                        goto geriGel;
                    }
                    else
                    {
                        x.Location = new Point(konumx, konumy + 75);
                        if (x != oyuncu3)
                        {
                            gizliMi(x.Location.X, x.Location.Y);
                        }
                    }
                }
                else if (hareketEt == 2)
                {
                    if (konumx + 75 > 472) // sağa git
                    {
                        goto geriGel;
                    }
                    else
                    {
                        x.Location = new Point(konumx + 75, konumy);
                        if (x != oyuncu3)
                        {
                            gizliMi(x.Location.X, x.Location.Y);
                        }
                    }
                }
                else if (hareketEt == 3)
                {
                    if (konumx - 75 < 0) // sola git
                    {
                        goto geriGel;
                    }
                    else
                    {
                        x.Location = new Point(konumx - 75, konumy);
                        if (x != oyuncu3)
                        {
                            gizliMi(x.Location.X, x.Location.Y);
                        }
                    }
                }

            }



        }

        private void oyuncu1Hareket() // A oyuncusunun hareketini sağlayan fonksiyon
        {
            bool hareket = false;
            Random rastgele = new Random();

            for (int i = 0; i < 80; i++)
            {
                if (altinlar[i].Visible == true) // görünür olan altınlar
                {
                    int xx = 25, yy = 25;
                    for(int r = 1; r<4; r++)
                    {
                        xx = 25* r;
                        if ((oyuncu1.Location.X == altinlar[i].Location.X) && (oyuncu1.Location.Y - xx == altinlar[i].Location.Y)) // yukarı
                        {
                            hareket = true; // oyuncu hareket etti
                        }
                        else if ((oyuncu1.Location.X == altinlar[i].Location.X) && (oyuncu1.Location.Y + xx == altinlar[i].Location.Y)) // aşağı
                        {
                            hareket = true;
                        }
                        else if ((oyuncu1.Location.X + xx == altinlar[i].Location.X) && (oyuncu1.Location.Y == altinlar[i].Location.Y)) // sağ
                        {
                            hareket = true;
                        }
                        else if ((oyuncu1.Location.X - xx == altinlar[i].Location.X) && (oyuncu1.Location.Y == altinlar[i].Location.Y)) // sol
                        {
                            hareket = true;
                        }

                        if (hareket)
                        {
                            oyuncu1.Location = altinlar[i].Location; // altını alma
                            oyuncu1Puan -= 10; // oyuncunun puanını düşürme
                            oyuncu1Puan += altinMiktari[i]; // oyuncunun aldığı altını bütçesine ekleme
                            kasadakiAltin += 10; // kasadaki altın miktarını arttırma
                            toplananAltinSayisi[0] += 10; // toplanan altın sayısını arttırma
                            panel1.Controls.Remove(altinlar[i]); // altını panelden silme
                            altinlar[i].Location = new Point(-500, -500); // altını form dışında bir konuma atama
                            altinMiktari[i] = 0; // altın miktarını sıfırlama
                            break;
                        }

                    }

                    if(hareket)
                    {
                        break;
                    }

                }
            }
           

                if (hareket == false) // oyuncu hareket etmemişse 
                {
                    Konumaİlerle(oyuncu1); //  konum belirleme

                    oyuncu1Puan -= 10;
                    kasadakiAltin += 10;

                if(kontroll)
                {
                    oyuncu1Puan += altinMiktari[ones1]; // oyuncunun aldığı altını bütçesine ekleme
                    toplananAltinSayisi[0] += altinMiktari[ones1]; // toplanan altın sayısını arttırma
                    kontroll = false;
                }

                }
                oyuncu1Lbl.Text = oyuncu1Puan.ToString(); // labeli güncelleme
                kasadakiAltinLbl.Text = kasadakiAltin.ToString();
        }

        int ekle = 0;
        private bool enKarliOlan(PictureBox send) // En Karlı olan altına gitme fonksiyonu
        {
            int miktarUp = 0, miktarDown = 0, miktarRight = 0, miktarLeft = 0;
            int indisUp = 0, indisDown = 0, indisRight = 0, indisLeft = 0;
            bool hareket1 = false;

            for (int i = 0; i < 80; i++)
            {
                if (altinlar[i].Visible == true)
                {
                    if ((send.Location.X == altinlar[i].Location.X) && (send.Location.Y - 25 == altinlar[i].Location.Y)) // yukarı
                    {
                        miktarUp = altinMiktari[i];
                        indisUp = i;
                        hareket1 = true;
                    }
                    if ((send.Location.X == altinlar[i].Location.X) && (send.Location.Y + 25 == altinlar[i].Location.Y)) // aşağı
                    {
                        miktarDown = altinMiktari[i];
                        indisDown = i;
                        hareket1 = true;
                    }
                    if ((send.Location.X + 25 == altinlar[i].Location.X) && (send.Location.Y == altinlar[i].Location.Y)) // sağ
                    {
                        miktarRight = altinMiktari[i];
                        indisRight = i;
                        hareket1 = true;
                    }
                    if ((send.Location.X - 25 == altinlar[i].Location.X) && (send.Location.Y == altinlar[i].Location.Y)) // sol
                    {
                        miktarLeft = altinMiktari[i];
                        indisLeft = i;
                        hareket1 = true;
                    }
                    ///////////////////////


                    // En Büyük olan miktara yönelme

                }
            }

            if (hareket1)
            { /// En büyük altın miktarına yönelten fonksiyon
                if (miktarUp > miktarDown)
                {
                    if (miktarUp > miktarRight)
                    {
                        if (miktarUp > miktarLeft) // en büyük yukarı
                        {

                            send.Location = new Point(send.Location.X, send.Location.Y - 25); // Yukarı Git
                            ekle = altinMiktari[indisUp]; // altın miktarını oyuncu puanına ekleyen değişken
                            altinMiktari[indisUp] = 0; // altın miktarını sıfırlama
                            panel1.Controls.Remove(altinlar[indisUp]); // altını panelden silme
                            altinlar[indisUp].Location = new Point(-500, -500); // altını panelden atma

                        }
                        else // en büyük sol
                        {
                            send.Location = new Point(send.Location.X - 25, send.Location.Y); // soll
                            ekle = altinMiktari[indisLeft];
                            altinMiktari[indisLeft] = 0;
                            panel1.Controls.Remove(altinlar[indisLeft]);
                            altinlar[indisLeft].Location = new Point(-500, -500);
                        }
                    }
                    else
                    {
                        if (miktarRight > miktarLeft) // en büyük sağ 
                        {
                            send.Location = new Point(send.Location.X + 25, send.Location.Y); // sağ
                            ekle = altinMiktari[indisRight];
                            altinMiktari[indisRight] = 0;
                            panel1.Controls.Remove(altinlar[indisRight]);
                            altinlar[indisRight].Location = new Point(-500, -500);
                        }
                        else // en büyük sol
                        {
                            send.Location = new Point(send.Location.X - 25, send.Location.Y); // soll
                            ekle = altinMiktari[indisLeft];
                            altinMiktari[indisLeft] = 0;
                            panel1.Controls.Remove(altinlar[indisLeft]);
                            altinlar[indisLeft].Location = new Point(-500, -500);
                        }
                    }
                }
                else
                {

                    if (miktarDown > miktarRight)
                    {
                        if (miktarDown > miktarLeft) // en büyük aşağı
                        {
                            send.Location = new Point(send.Location.X, send.Location.Y + 25); // Aşağı Git
                            ekle = altinMiktari[indisDown];
                            altinMiktari[indisDown] = 0;
                            panel1.Controls.Remove(altinlar[indisDown]);
                            altinlar[indisDown].Location = new Point(-500, -500);
                        }
                        else // en büyük sol
                        {
                            send.Location = new Point(send.Location.X - 25, send.Location.Y); // soll
                            ekle = altinMiktari[indisLeft];
                            altinMiktari[indisLeft] = 0;
                            panel1.Controls.Remove(altinlar[indisLeft]);
                            altinlar[indisLeft].Location = new Point(-500, -500);
                        }
                    }
                    else
                    {
                        if (miktarRight > miktarLeft) // en büyük sağ
                        {
                            send.Location = new Point(send.Location.X + 25, send.Location.Y); // sağ
                            ekle = altinMiktari[indisRight];
                            altinMiktari[indisRight] = 0;
                            panel1.Controls.Remove(altinlar[indisRight]);
                            altinlar[indisRight].Location = new Point(-500, -500);
                        }
                        else // en büyük sol
                        {
                            send.Location = new Point(send.Location.X - 25, send.Location.Y); // soll
                            ekle = altinMiktari[indisLeft];
                            altinMiktari[indisLeft] = 0;
                            panel1.Controls.Remove(altinlar[indisLeft]);
                            altinlar[indisLeft].Location = new Point(-500, -500);
                        }
                    }

                }



            }

            return hareket1;
        }

        private void oyuncu2Hareket() // B oyuncusunun hareketini sağlayan fonksiyon
        {

            if(enKarliOlan(oyuncu2)) // En karlı hedefe yönelme
            {
                oyuncu2Puan += ekle; // oyuncu puanını arttırma
                toplananAltinSayisi[1] += ekle; // toplam puanı güncelleme
            }
            else // Rastgele
            {
                Konumaİlerle(oyuncu2); // rastgele konum atama

                if (kontroll)
                {
                    oyuncu2Puan += altinMiktari[ones1]; // oyuncunun aldığı altını bütçesine ekleme
                    toplananAltinSayisi[1] += altinMiktari[ones1]; // toplanan altın sayısını arttırma
                    kontroll = false;
                }

            }

            oyuncu2Puan -= 15; // puanını azaltma
            kasadakiAltin += 15; // kasa puanını arttırma
            oyuncu2Lbl.Text = oyuncu2Puan.ToString(); // label güncelleme
            kasadakiAltinLbl.Text = kasadakiAltin.ToString();
        }

        int sira = 1;
        int hamle1 = 0, hamle2 = 0, hamle3 = 0, hamle4 = 0; // hamle sayısının tutulduğu fonksiyon

        private void button4_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            oyuncu3Hareket();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            oyuncu4Hareket();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            button1.Enabled = false;
            MessageBox.Show("Oyun Bitti.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            oyuncu2Hareket();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            oyuncu1Hareket();
        }

        private void main() // ana fonksiyon
        {
            while(true) // döngü başlattık
            {

                int sayac = 0; // toplam kaç oyuncunun oynadığını tutan değişken
                if (oyuncu1Puan > 0) // oyuncu 1 elendi mi diye kontrol ediyoruz.
                {
                    oyuncu1Hareket(); // fonksiyonnu çağırma
                    sira = 1; // sirayı tutan değişken
                    label6.Text = sira.ToString(); // labeli güncelleme
                    sayac++; 
                    hamle1++; // hamle sayısını arttırma
                    oyuncu1Lbl.Refresh(); // labeli yenileme
                    label6.Refresh();
                    oyuncu1.Refresh();
                    System.Threading.Thread.Sleep(1000); // 1 saniye bekletme
                    oyuncu1.Refresh();
                }
                else // oyuncu elenirse labelin rengini kırmızı yapma
                {
                    label2.ForeColor = Color.Red;
                    label2.Refresh();
                }
                if (oyuncu2Puan > 14)
                {
                    oyuncu2Hareket();
                    sira = 2;
                    label6.Text = sira.ToString();
                    sayac++;
                    hamle2++;
                    oyuncu2Lbl.Refresh();
                    label6.Refresh();
                    oyuncu2.Refresh();
                    System.Threading.Thread.Sleep(1000);
                    oyuncu2.Refresh();
                }
                else
                {
                    label3.ForeColor = Color.Red;
                    label3.Refresh();
                }
                if (oyuncu3Puan > 19)
                {
                    oyuncu3Hareket();
                    sira = 3;
                    label6.Text = sira.ToString();
                    sayac++;
                    hamle3++;
                    oyuncu3Lbl.Refresh();
                    label6.Refresh();
                    oyuncu3.Refresh();
                    for(int i = 0; i<80; i++)
                    {
                        altinlar[i].Refresh();
                    }
                    System.Threading.Thread.Sleep(1000);
                    oyuncu3.Refresh();
                }
                else
                {
                    label4.ForeColor = Color.Red;
                    label4.Refresh();
                }
                if (oyuncu4Puan > 24)
                {
                    oyuncu4Hareket();
                    sira = 4;
                    label6.Text = sira.ToString();
                    sayac++;
                    hamle4++;
                    oyuncu4Lbl.Refresh();
                    label6.Refresh();
                    oyuncu4.Refresh();
                    System.Threading.Thread.Sleep(1000);
                    oyuncu4.Refresh();
                }
                else
                {
                    label5.ForeColor = Color.Red;
                    label5.Refresh();
                }

                if (sayac < 2) // tek oyuncu kalırsa oyun biter
                {
                    if (oyuncu1Puan >= 5) // kazananı ekrana basma
                    {
                        MessageBox.Show("Oyun Bitti. Kazanan Oyuncu 1 !");
                    }
                    else if (oyuncu2Puan >= 15)
                    {
                        MessageBox.Show("Oyun Bitti. Kazanan Oyuncu 2 !");
                    }
                    else if (oyuncu3Puan >= 20)
                    {
                        MessageBox.Show("Oyun Bitti. Kazanan Oyuncu 3 !");
                    }
                    else if (oyuncu3Puan >= 25)
                    {
                        MessageBox.Show("Oyun Bitti. Kazanan Oyuncu 4 !");
                    }

                    MessageBox.Show("Oyuncu 1 Toplam Adım Sayısı : " + hamle1 + "\n" +
                        "Oyuncu 1 Harcanan Altın Sayısı : " + (200 - oyuncu1Puan) + "\n" +
                        "Oyuncu 1 Mevcut Altın Sayısı : " + oyuncu1Puan + "\n" +
                        "Oyuncu 1 Toplanan Altın Sayısı : " + toplananAltinSayisi[0] + "\n" +
                        "------------------------------------------------------------------------" + "\n" +
                        "Oyuncu 2 Toplam Adım Sayısı : " + hamle2 + "\n" +
                        "Oyuncu 2 Harcanan Altın Sayısı : " + (200 - oyuncu2Puan) + "\n" +
                        "Oyuncu 2 Mevcut Altın Sayısı : " + oyuncu2Puan + "\n" +
                        "Oyuncu 2 Toplanan Altın Sayısı : " + toplananAltinSayisi[1] + "\n" +
                        "------------------------------------------------------------------------" + "\n" +
                        "Oyuncu 3 Toplam Adım Sayısı : " + hamle3 + "\n" +
                        "Oyuncu 3 Harcanan Altın Sayısı : " + (200 - oyuncu3Puan) + "\n" +
                        "Oyuncu 3 Mevcut Altın Sayısı : " + oyuncu3Puan + "\n" +
                        "Oyuncu 3 Toplanan Altın Sayısı : " + toplananAltinSayisi[2] + "\n" +
                        "------------------------------------------------------------------------" + "\n" +
                        "Oyuncu 4 Toplam Adım Sayısı : " + hamle4 + "\n" +
                        "Oyuncu 4 Harcanan Altın Sayısı : " + (200 - oyuncu4Puan) + "\n" +
                        "Oyuncu 4 Mevcut Altın Sayısı : " + oyuncu4Puan + "\n" +
                        "Oyuncu 4 Toplanan Altın Sayısı : " + toplananAltinSayisi[3] + "\n" +
                        "------------------------------------------------------------------------" + "\n" + 
                        "Bulunan Gizli Altın Sayısı : " + bulunanGizliAltinSayisi);
                    break;
                }

            }
        }
        private void oyuncu3Hareket() // C oyuncusu Hareket fonksiyonu
        {

            bool hareket = false,hareket2 = false;
            int xx = 25, yy = 25;
            for (int i = 0; i < 80; i++)
            {
                if ((altinlar[i].Visible == false) && (altinlar[i].Location.X != -500)) // gizli altınların konumu bulma
                {
                    for(int r = 1; r<80; r++)
                    {
                        xx = 25 * r;
                        yy = 25 * r;
                        if ((oyuncu3.Location.X == altinlar[i].Location.X) && (oyuncu3.Location.Y - yy == altinlar[i].Location.Y)) // yukarı
                        {
                            hareket2 = true; // oyuncu hareket etti
                        }
                        else if ((oyuncu3.Location.X + xx == altinlar[i].Location.X) && (oyuncu3.Location.Y == altinlar[i].Location.Y)) // sağ
                        {
                            hareket2 = true;
                        }
                        else if ((oyuncu3.Location.X == altinlar[i].Location.X) && (oyuncu3.Location.Y + yy == altinlar[i].Location.Y)) // aşağı
                        {
                            hareket2 = true;
                        }
                        else if ((oyuncu4.Location.X - xx == altinlar[i].Location.X) && (oyuncu4.Location.Y == altinlar[i].Location.Y)) // sol
                        {
                            hareket2 = true;
                        }
                        else if ((oyuncu4.Location.X + xx == altinlar[i].Location.X) && (oyuncu4.Location.Y + yy == altinlar[i].Location.Y)) // çapraz üst
                        {
                            hareket2 = true;
                        }
                        else if ((oyuncu4.Location.X - xx == altinlar[i].Location.X) && (oyuncu4.Location.Y + yy == altinlar[i].Location.Y)) // çapraz üst
                        {
                            hareket2 = true;
                        }
                        else if ((oyuncu4.Location.X + xx == altinlar[i].Location.X) && (oyuncu4.Location.Y - yy == altinlar[i].Location.Y)) // çapraz üst
                        {
                            hareket2 = true;
                        }
                        else if ((oyuncu4.Location.X - xx == altinlar[i].Location.X) && (oyuncu4.Location.Y - yy == altinlar[i].Location.Y)) // çapraz üst
                        {
                            hareket2 = true;
                        }
                        if(hareket)
                        {
                            altinlar[i].Visible = true;
                            break;
                        }
                    }
                    ////////////////

                    if (hareket2)
                    {
                        altinlar[i].Visible = true;
                        sezmeKonumX = altinlar[i].Location.X; // D oyuncusunun sezme kordinatları
                        sezmeKonumY = altinlar[i].Location.Y;
                        break;
                    }
                }

            }

            if(hareket2 == false)
            {
                for (int i = 0; i < 80; i++)
                {
                    if (altinlar[i].Visible == false)
                    {
                        
                        altinlar[i].Visible = true;
                        sezmeKonumX = altinlar[i].Location.X; ; // D oyuncusunun sezme kordinatları
                        sezmeKonumY = altinlar[i].Location.Y;
                        break;
                    }
                }
            }
            

                if (enKarliOlan(oyuncu3)) // En karlı hedefe yönelme
                {
                    sezmeKonumX = 0;
                    sezmeKonumY = 0;
                    oyuncu3Puan += ekle;
                    toplananAltinSayisi[2] += ekle;
                }
                else 
                {
                sezmeKonumX = 0;
                sezmeKonumY = 0;
                Konumaİlerle(oyuncu3); 
                    

                if (kontroll)
                {
                    oyuncu3Puan += altinMiktari[ones1]; // oyuncunun aldığı altını bütçesine ekleme
                    toplananAltinSayisi[2] += altinMiktari[ones1]; // toplanan altın sayısını arttırma
                    kontroll = false;
                }

            }

            ///////////////////////


            oyuncu3Puan -= 20;
            kasadakiAltin += 20;
            oyuncu3Lbl.Text = oyuncu3Puan.ToString(); // labeller güncelleniyor
            kasadakiAltinLbl.Text = kasadakiAltin.ToString();

        }

        private void oyuncu4Hareket() // D Oyuncusu Hareket Fonksiyonu 
        {
            int farkX = oyuncu4.Location.X - sezmeKonumX; // Oyuncuların hamlelerini sezme işlemleri
            int farkY = oyuncu4.Location.Y - sezmeKonumY;
            int farkOyuncu3X = oyuncu3.Location.X - sezmeKonumX;
            int farkOyuncu3Y = oyuncu3.Location.Y - sezmeKonumY;
            bool hareket = false;

            if(farkX < 0)
            {
                farkX = sezmeKonumX - oyuncu4.Location.X; // Oyuncuların hamlelerini sezme işlemleri
            }
            if(farkY < 0)
            {
                farkY = sezmeKonumY - oyuncu4.Location.Y;
            }
            if(farkOyuncu3X < 0)
            {
                farkOyuncu3X = sezmeKonumX - oyuncu3.Location.X; // Oyuncuların hamlelerini sezme işlemleri
            }
            if(farkOyuncu3Y < 0)
            {
                farkOyuncu3Y = sezmeKonumY - oyuncu3.Location.Y;
            }
            int oyuncu3HamleSayisi = ((farkOyuncu3X / 25) + (farkOyuncu3Y / 25)); //C Oyuncusunun hedefe hamlesi
            int oyuncu4HamleSayisi = ((farkX/25) + (farkY/25)); // D oyuncusunun hedefe hamlesi

            if(oyuncu4HamleSayisi < oyuncu3HamleSayisi)  // Oyuncu3' ün hamlesini önce yapabiliyorsa ondan önce onun konumuna ilerler
            {
                if (oyuncu4.Location.X < sezmeKonumX) //X ekseni için gizli altına yönelme
                {
                    oyuncu4.Location = new Point(oyuncu4.Location.X + 25, oyuncu4.Location.Y); // oyuncunun hamlesini önceden sezdik ve hedefe ilerliyoruz
                    oyuncu4Puan -= 25;
                    kasadakiAltin += 25;
                    hareket = true;
                }
                else if (oyuncu4.Location.X > sezmeKonumX)
                {
                    oyuncu4.Location = new Point(oyuncu4.Location.X - 25, oyuncu4.Location.Y); // oyuncunun hamlesini önceden sezdik ve hedefe ilerliyoruz
                    oyuncu4Puan -= 25;
                    kasadakiAltin += 25;
                    hareket = true;
                }

                if (hareket != true)
                {
                    if (oyuncu4.Location.Y < sezmeKonumY)
                    {
                        oyuncu4.Location = new Point(oyuncu4.Location.X, oyuncu4.Location.Y + 25); // oyuncunun hamlesini önceden sezdik ve hedefe ilerliyoruz
                        oyuncu4Puan -= 25;
                        kasadakiAltin += 25;
                    }
                    else if (oyuncu4.Location.Y > sezmeKonumY)
                    {
                        oyuncu4.Location = new Point(oyuncu4.Location.X, oyuncu4.Location.Y - 25); // oyuncunun hamlesini önceden sezdik ve hedefe ilerliyoruz
                        oyuncu4Puan -= 25;
                        kasadakiAltin += 25;
                    }
                }


                for (int j = 0; j < 80; j++) //İlerlediği yolda altın varsa altını alır
                {

                    if ((oyuncu4.Location.X == altinlar[j].Location.X) && (oyuncu4.Location.Y == altinlar[j].Location.Y)) // altın varmı
                    { 
                        if (altinlar[j].Visible == false) // gizli altın mı
                        {
                            bulunanGizliAltinSayisi++; // gizli altın bulunda
                            altinlar[j].Visible = true; // altın görünür oldu
                            sezmeKonumX = 0;
                            sezmeKonumY = 0;
                            break;
                        }
                        else
                        {
                            oyuncu4Puan += altinMiktari[j]; // oyuncu puanı arttı
                            toplananAltinSayisi[3] += altinMiktari[j]; 
                            altinMiktari[j] = 0;
                            panel1.Controls.Remove(altinlar[j]);
                            altinlar[j].Location = new Point(-500, -500);
                            break;

                        }

                    }
                }

            }
            else // En karlı olana yönelme
            {
                if (enKarliOlan(oyuncu4)) // En karlı hedefe yönelme
                {
                    oyuncu4Puan += ekle;
                    oyuncu4Puan -= 25;
                    kasadakiAltin += 25;
                    toplananAltinSayisi[3] += ekle;
                }
                else // Rastgele
                {
                    Konumaİlerle(oyuncu4);

                    if (kontroll)
                    {
                        oyuncu4Puan += altinMiktari[ones1]; // oyuncunun aldığı altını bütçesine ekleme
                        toplananAltinSayisi[3] += altinMiktari[ones1]; // toplanan altın sayısını arttırma
                        kontroll = false;
                    }

                    oyuncu4Puan -= 25;
                    kasadakiAltin += 25;
                }
            }


            oyuncu4Lbl.Text = oyuncu4Puan.ToString();
            kasadakiAltinLbl.Text = kasadakiAltin.ToString();
          // sezmeKonumX = 0;
          //  sezmeKonumY = 0;

        }

        private bool gizliMi(int konumx, int konumy) // Gizli altın buldu mu fonksiyonu
        {
            bool sonuc = false;
            for (int i = 0; i < 80; i++)
            {
                if(altinlar[i].Location.X == konumx && altinlar[i].Location.Y == konumy)
                {
                    bulunanGizliAltinSayisi++;
                    altinlar[i].Visible = true;
                    sonuc = true;
                    break;
                }
            }
            return sonuc;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            groupBox1.Enabled = false;

            int[] tt = new int[8];
            PictureBox[] ss = new PictureBox[8];
            int sayac = 0;
            for (int i = 0; i < 80; i++)
            {
                if(altinlar[i].Visible == false)
                {
                    
                    ss[sayac] = altinlar[i];
                    ss[sayac].Visible = true;
                    ss[sayac].Refresh();
                    sayac++;
                }
            }

            System.Threading.Thread.Sleep(2000);

            

            for (int i = 0; i<sayac; i++)
            {
                ss[i].Visible = false;
                ss[i].Refresh();
            }


            main(); // Oyun Başlıyor
          
        }
    }
}
