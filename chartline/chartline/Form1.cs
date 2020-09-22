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
            //crete data table
            dataGridView1.ColumnCount = 2;
            dataGridView1.Columns[0].Name = "Temprature ('C)";
            dataGridView1.Columns[0].Width = 490;
            
            dataGridView1.Columns[1].Name = "Date Time";
            dataGridView1.Columns[1].Width = 490;


        }
    
        /// <summary>
        /// bu timer1_Tick methodu nu  kullanma amacım
        /// elime veri girmek yerine zamanlaycnın tetiklenmesine göre 
        /// otomatik veri olup grafik çizmesi için kullanılıyor.
        /// </summary>
        double i = 0.0;
        int j = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            Thread.Sleep(2000);// grafiğin hızlı gitmememsi için thraed atma ihtiyacı duydum
            myPane = zedGraphControl1.GraphPane;
            //1 çizgi-1
            double x = (double)new XDate(DateTime.Now.AddMinutes(i + 2));
            i++;
            double y1 = i;
            listPointsOne.Add(x, y1);
            myCurveOne = myPane.AddCurve(null, listPointsOne, Color.Blue, SymbolType.Circle);
            i++;

             dataGridView1.Rows.Add(10000);// satırın ne kadar veri tutacağını önceden veriyoruz içeriye
            // data gridviews for points added
            dataGridView1.Rows[j].HeaderCell.Value = "Daire";
            dataGridView1.Rows[j].Cells[0].Value = y1;
            dataGridView1.Rows[j].Cells[1].Value = DateTime.Now.AddMinutes(i + 2);
            j++;
            
            //2   çigi-2         
            double y2 = 5.0;
            listPointsTwo.Add(x, y2);
            myCurveTwo = myPane.AddCurve(null, listPointsTwo, Color.Red, SymbolType.Star);
            i++;

            dataGridView1.Rows[j].HeaderCell.Value = "Yıldız";
            dataGridView1.Rows[j].Cells[0].Value = y1;
            dataGridView1.Rows[j].Cells[1].Value = DateTime.Now.AddMinutes(i + 2);
            j++;
            
            //3  çizgi-3
            double y3 = 10.0;
            listPointsThree.Add(x, y3);
            myCurveThree = myPane.AddCurve(null, listPointsThree, Color.Green, SymbolType.Triangle);

            dataGridView1.Rows[j].HeaderCell.Value = "Üçgen";
            dataGridView1.Rows[j].Cells[0].Value = y1;
            dataGridView1.Rows[j].Cells[1].Value = DateTime.Now.AddMinutes(i + 2);
            j++;

            dataGridView1.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);//bu otomatik boyut verir
            zedGraphControl1.IsEnableWheelZoom = true;// zedGraph kütüphanesinde zoom yapmak 
            //Grafiği yenileme
            zedGraphControl1.Invalidate();
            zedGraphControl1.AxisChange();
            zedGraphControl1.Refresh();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// grafik başlatır
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
        /// <summary>
        /// grafik durdurur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void zedGraphControl1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
