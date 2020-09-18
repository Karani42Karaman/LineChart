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

namespace chartline
{
    public partial class Form1 : Form
    {
        GraphPane myPane = new GraphPane();
        RollingPointPairList listPointsOne = new RollingPointPairList(40);
        LineItem myCurveOne;
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
        private void timer1_Tick(object sender, EventArgs e)
        {

            int sayi = 0;
            sayi++;
            label10.Text = sayi.ToString();
            //
            myPane = zedGraphControl1.GraphPane;
            //1
            double x = Convert.ToDouble(label10.Text);
            double y1 = double.Parse("0", System.Globalization.CultureInfo.InvariantCulture);
            listPointsOne.Add(x, y1);
            myCurveOne = myPane.AddCurve(null, listPointsOne, Color.Blue, SymbolType.Circle);
            
            //2            
            double y2 = double.Parse("0", System.Globalization.CultureInfo.InvariantCulture);
            listPointsOne.Add(x, y2);
            myCurveTwo = myPane.AddCurve(null, listPointsTwo, Color.Red, SymbolType.Star);
            
            //3
            double y3 = double.Parse("0", System.Globalization.CultureInfo.InvariantCulture);
            listPointsOne.Add(x, y3);
            myCurveThree = myPane.AddCurve(null, listPointsThree, Color.Green, SymbolType.Triangle);
            
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
            timer1.Enabled= true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }
    }
}
