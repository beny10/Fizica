using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListView;

namespace GraphDrawer
{
    public partial class Form1 : Form
    {
        private Drawer _drawer;
        private bool _isStopped=false;
        public Form1()
        {
            InitializeComponent();
            _drawer = new Drawer(ImageReceived);
        }
        private void ImageReceived(Image image,DataSet set)
        {
            if (_isStopped)
                return;
            pictureBox1.Image = image;
            switch(set.Type)
            {
                case ActionType.Charging:
                    labelIncarcare.Invoke(new MethodInvoker(delegate
                    {
                        labelIncarcare.Visible = true;
                        labelDescarcare.Visible = false;
                        CalculateCapacity(set);
                    }));
                    break;
                case ActionType.Discharging:
                    labelIncarcare.Invoke(new MethodInvoker(delegate
                    {
                        labelDescarcare.Visible = true;
                        labelIncarcare.Visible = false;
                    }));
                    break;
            }
            listView1.Invoke(new MethodInvoker(delegate
            {
                listView1.Items.Clear();
            }));
            List<Read> reads = set.GetReads();
            for (int i = 0; i < set.Count; i++)
            {
                double value = reads[i].Voltage;
                int time = reads[i].Time;
                value = value * 5.0 / 1024.0;
                string voltage = value.ToString();
                if (voltage.Length > 4)
                    voltage =voltage.Substring(0, 4);
                var item = new ListViewItem(time.ToString());
                item.SubItems.Add(voltage);
                listView1.Invoke(new MethodInvoker(delegate
                {
                    listView1.Items.Add(item);
                }));
            }
            
        }
        private void CalculateCapacity(DataSet set)
        {
            int start = 0;
            List<Read> reads = set.GetReads();
            for (int i = 0; i < reads.Count; i++)
            {
                if (reads[i].Type == ReadType.NewInstruction)
                    start = i;
            }
            int closestIndex =start;
            for (int i = start; i<set.Count; i++)
            {
                if (Helpers.UntilLimit(reads[i].Voltage) < Helpers.UntilLimit(reads[closestIndex].Voltage) && Helpers.UntilLimit(reads[i].Voltage)>0)
                    closestIndex = i;
            }
            double voltage = reads[closestIndex].Voltage * 5.0/ 1024.0;
            richTextBox1.Text=$"Cel mai apropiat voltag este{voltage}\n";
            richTextBox1.Text += $"Timpul citirii este {reads[closestIndex].Time}ms\n";
            double capacitorValueInMicro = reads[closestIndex].Time  / 10.0;
            richTextBox1.Text += $"Cu o rezistenta de 10K capacitatea={capacitorValueInMicro/10000/100}F\n={capacitorValueInMicro} micro farad";
        }
        private void EnableStartButton()
        {
            button1.Enabled = true;
            button2.Enabled = false;
        }
        private void EnableStopButton()
        {
            button1.Enabled = false;
            button2.Enabled = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            EnableStopButton();
            _drawer.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EnableStartButton();
            _drawer.Stop();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _drawer.Charge();
            EnableStopButton();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EnableStopButton();
            _drawer.Discharge();
        }

    }
}
