using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GraphDrawer
{
    public partial class ModeSelector : Form
    {
        public ModeSelector()
        {
            InitializeComponent();
        }
        private void ShowForm(Form f)
        {
            f.ShowDialog();
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ShowForm(new CsvForm());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowForm(new Form1());
        }
    }
}
