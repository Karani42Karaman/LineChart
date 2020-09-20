using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Diagnostics;
using ZedGraph;
using System.Threading;

namespace chartline
{
    public partial class Form1 : Form
    {
        GraphPane myPane = new GraphPane();// grafiğin kendisi
        RollingPointPairList listPointsOne = new RollingPointPairList(40000);//nokta yapar anacak şimdilik noktanın boyutu 40 dır
        LineItem myCurveOne;//çizgi çizer 
        RollingPointPairList listPointsTwo = new RollingPointPairList(40);
        LineItem myCurveTwo;
        RollingPointPairList listPointsThree = new RollingPointPairList(40);
        LineItem myCurveThree;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //1
            myPane = zedGraphControl1.GraphPane;
            myPane.XAxis.Title.Text = "Time";
            myPane.YAxis.Title.Text = "Temprature";

            //tipi veriyoruz 
            myPane.XAxis.Title.Text = "Date";
            myPane.XAxis.Type = AxisType.Date;
            myPane.XAxis.Scale.Format = "HH:mm:ss";
            myPane.XAxis.Scale.MajorUnit = DateUnit.Minute;
            myPane.XAxis.Scale.MajorStep = 1;
            //X ekseni için verilen değer aralığı bu değer aralığından dışarı çıkılmaz
            //myPane.XAxis.Scale.Min = new XDate(DateTime.Now);
            //myPane.XAxis.Scale.Max = new XDate(DateTime.Now.AddMinutes(11));

            //  x ekseninde değerlerin hangi parametreye göre artacağının belirlenmesinde:
            myPane.XAxis.Scale.MajorUnit = DateUnit.Second;

            zedGraphControl1.Invalidate();
            zedGraphControl1.AxisChange();
            zedGraphControl1.Refresh();

        }
        /// <summary>
        /// aklımdaki zamanı verecem 
        /// ona göre  gelen sıcak lık da olursa ekrana basacak 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        double i = 0.0; Random rn = new Random();
        private void timer1_Tick(object sender, EventArgs e)
        {
            Thread.Sleep(2000);
            myPane = zedGraphControl1.GraphPane;
            //1 çizgi-1
            double x = (double)new XDate(DateTime.Now.AddMinutes(i + 2));
            i++;
            double y1 = i;

            listPointsOne.Add(x, y1);
            myCurveOne = myPane.AddCurve(null, listPointsOne, Color.Blue, SymbolType.Circle);
            i++;
            //2   çigi-2         
            double y2 = 5.0;
            listPointsTwo.Add(x, y2);
            myCurveTwo = myPane.AddCurve(null, listPointsTwo, Color.Red, SymbolType.Star);
            i++;
            //3  çizgi-3
            double y3 = 10.0;
            listPointsThree.Add(x, y3);
            myCurveThree = myPane.AddCurve(null, listPointsThree, Color.Green, SymbolType.Triangle);

            zedGraphControl1.IsEnableWheelZoom = true;// zedGraph kütüphanesinde zoom yapmak 
            //Grafiği yenileme
            zedGraphControl1.Invalidate();
            zedGraphControl1.AxisChange();
            zedGraphControl1.Refresh();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void zedGraphControl1_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}
