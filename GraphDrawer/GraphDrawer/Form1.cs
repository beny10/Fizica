using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        private void ImageReceived(Image image,ActionType type,DataSet set)
        {
            if (_isStopped)
                return;
            pictureBox1.Image = image;
            switch(type)
            {
                case ActionType.Charging:
                    labelIncarcare.Invoke(new MethodInvoker(delegate
                    {
                        labelIncarcare.Visible = true;
                        labelDescarcare.Visible = false;
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
            for (int i = 0; i < set.Count; i++)
            {
                double value = -set.GetPoints()[i].Y;
                int time = (set.GetPoints()[i].X-10)*80;
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

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;
            _drawer.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            _drawer.Stop();
        }
    }
}
