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
    public partial class CsvForm : Form
    {
        private Port _port;
        private CsvWriter _writer;
        public CsvForm()
        {
            InitializeComponent();
            _port = new Port(PacketReceived);
            InitializingWriter();
        }
        private void InitializingWriter()
        {
            _writer = new CsvWriter($"{Helpers.GetTimeStamp()}.csv");
        }
        private void PacketReceived(DataSet set)
        {
            if (_writer.IsClosed)
                InitializingWriter();
            _writer.Write(set);
            System.Diagnostics.Process.Start(_writer.CloseFile());
        }
        public void Charge()
        {
            _port.SendByte(1);
        }
        public void Discharge()
        {
            _port.SendByte(0);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Charge();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Discharge();
        }
    }
}
