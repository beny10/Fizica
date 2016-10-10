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
        public Form1()
        {
            InitializeComponent();
            _drawer = new Drawer(ImageReceived);
        }
        private void ImageReceived(Image image)
        {
            pictureBox1.Image = image;
        }
    }
}
