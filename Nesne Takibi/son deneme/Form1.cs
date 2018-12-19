using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Imaging;
using AForge.Imaging.Filters;
using System.IO.Ports; //arduino haberleşme



namespace son_deneme
{
    public partial class Form1 : Form
    {
        private FilterInfoCollection VideoCapTureDevices; // VideoCapTureDevices adında tanımladığımız değişken bilgisayara kaç kamera bağlı olduğunu tutar. 
        private VideoCaptureDevice Finalvideo; // Finalvideo ise bizim kullanacağımız aygıt.

        SerialPort sp = new SerialPort("COM5", 9600); // haberleşme verileri

        public Form1()
        {
            InitializeComponent();
            sp.Open(); // port açılıyor
        }

        int R; // Barların değişkenleri
        int G;
        int B;

        private void Form1_Load(object sender, EventArgs e)
        {
            VideoCapTureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice); // VideoCapTureDevices dizisine kullandığımız kamera yollanıyor.
            foreach (FilterInfo VideoCaptureDevice in VideoCapTureDevices)
            {

                comboBox1.Items.Add(VideoCaptureDevice.Name); // kamerayı combobox a yazıyoruz.

            }

            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Butona basıldığında yukarda tanımladığımız Finalvideo değişkenine comboboxta seçili olan kamerayı atıyoruz.
            Finalvideo = new VideoCaptureDevice(VideoCapTureDevices[comboBox1.SelectedIndex].MonikerString);
            Finalvideo.NewFrame += new NewFrameEventHandler(Finalvideo_NewFrame);
            Finalvideo.Start(); // kamerayı başlatma.
            
        }


        void Finalvideo_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap image = (Bitmap)eventArgs.Frame.Clone(); // image e görüntü oluşturuluyor
            Bitmap image1 = (Bitmap)eventArgs.Frame.Clone();
            


            pictureBox1.Image = image; // kameradan alınan görüntüyü picturebox a atıyoruz.

            
            EuclideanColorFiltering filter = new EuclideanColorFiltering(); // filtre oluşturma            
            filter.CenterColor = new RGB(Color.FromArgb(R, G, B)); // R,G,B değerini kendimiz ayarlıyoruz.
            filter.Radius = 120; // 120 nin üstündeyse "0" altındaysa resim aynı şekilde kalır. Formüle göre hesaplarnır.           
            filter.ApplyInPlace(image1); // filtreyi uygulama
            
            nesnebul(image1);  
            
        }

        public void nesnebul(Bitmap image)
        {
            BlobCounter blobCounter = new BlobCounter(); // nesne boyu seçimi için kullanılıyor.
            blobCounter.MinWidth = 10; // min boyut
            blobCounter.MinHeight = 10;
            blobCounter.FilterBlobs = true; // aktif ediyor.
            blobCounter.ObjectsOrder = ObjectsOrder.Size; // nesneyi takip et
           
          
            blobCounter.ProcessImage(image);
            Rectangle[] rects = blobCounter.GetObjectsRectangles(); // dikdörtgen içine alma
            Blob[] blobs = blobCounter.GetObjectsInformation();
            pictureBox2.Image = image;
            

            foreach (Rectangle recs in rects) // takip kodları
            {
                                
                    Rectangle objectRect = rects[0];                    
                    Graphics g = pictureBox1.CreateGraphics();
                    using (Pen pen = new Pen(Color.FromArgb(200, 200, 0), 10)) // renk belirleme ve kenar kalınlığı
                    {
                        g.DrawRectangle(pen, objectRect); // dikdörtgen çizdirme
                    }
                    
                    int objectX = objectRect.X + (objectRect.Width / 2); // koordinat alma
                    int objectY = objectRect.Y + (objectRect.Height / 2);
                    
                    g.Dispose();
                    
                    this.Invoke((MethodInvoker)delegate

                    // this.Invoke : Tüm kontrollerde bulunan bir senkronizasyon mekanizmasıdır. 
                    // MethodInvoker : Invoke'ın beklediği delege adıdır.Invoke, "MethodInvoker" fonksiyonu ile aynı imza da bir fonksiyon verilmesini bekler.
                   
                    {
                        // ekran da belirlediğim koordinatlara göre arduinoya veri gönderiliyor ve ekran da yazdırılıyor.

                        if (objectX < 150 && objectY < 150) 
                        {
                            sp.Write("4");
                            textBox1.Text = "4 yanıyo";
                            button4.BackColor = Color.Red;
                        }
                            
                        else                        
                            button4.BackColor = Color.Black;                        
                        
                        if ((objectX < 150 && objectY > 150) && objectY < 250)
                        {
                            sp.Write("3");
                            textBox1.Text = "3 yanıyo";
                            button3.BackColor = Color.Red;
                        }

                        else
                            button3.BackColor = Color.Black;   

                        if ((objectX < 150 && objectY > 250) && objectY < 350)
                        {
                            sp.Write("2");
                            textBox1.Text = "2 yanıyo";
                            button11.BackColor = Color.Red;
                        }

                        else
                            button11.BackColor = Color.Black;         

                        if ((objectX > 150 && objectX < 250) && objectY < 150)
                        {
                            sp.Write("a");
                            textBox1.Text = "10 yanıyo";
                            button10.BackColor = Color.Red;
                        }

                        else
                            button10.BackColor = Color.Black;         

                        if ((objectX > 150 && objectX < 250) && (objectY > 150 && objectY < 250))
                        {
                            sp.Write("9");
                            textBox1.Text = "9 yanıyo";
                            button9.BackColor = Color.Red;
                        }

                        else
                            button9.BackColor = Color.Black;         

                        if ((objectX > 150 && objectX < 250) && (objectY > 250 && objectY < 400))
                        {
                            sp.Write("8");
                            textBox1.Text = "8 yanıyo";
                            button8.BackColor = Color.Red;
                        }

                        else
                            button8.BackColor = Color.Black;         

                        if (objectX > 250  && objectY < 150)
                        {
                            sp.Write("7");
                            textBox1.Text = "7 yanıyo";
                            button7.BackColor = Color.Red;
                        }

                        else
                            button7.BackColor = Color.Black;         

                        if ((objectX > 250 ) && (objectY > 150 && objectY < 250))
                        {
                            sp.Write("6");
                            textBox1.Text = "6 yanıyo";
                            button6.BackColor = Color.Red;
                        }

                        else
                            button6.BackColor = Color.Black;         

                        if (objectX > 250 && objectY > 250)
                        {
                             sp.Write("5");
                            textBox1.Text = "5 yanıyo";
                            button5.BackColor = Color.Red;
                        }

                        else
                            button5.BackColor = Color.Black;
                        
                        
                        richTextBox1.Text = objectRect.Location.ToString() + "\n" + richTextBox1.Text + "\n"; ; //koordinatların richTextBox a yazılması.
                    });
                                
            }

        }      

        private void button2_Click(object sender, EventArgs e)
        {
                this.Close();
                Application.Exit(); // uygulamadan çıkış.            
                
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            R = trackBar1.Value; // 1. bar a R atanıyor.
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            G = trackBar2.Value;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            B = trackBar3.Value;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
               

    }
}
