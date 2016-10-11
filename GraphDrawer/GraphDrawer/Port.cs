using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDrawer
{
    public class Port
    {
        private SerialPort _port;
        private Action<DataSet> _packetReceived;
        private DataSet _set = new DataSet();
        public Port(Action<DataSet> packetReceived)
        {
            _port = new SerialPort("COM3");
            _port.Open();
            _packetReceived = packetReceived;
            _port.DataReceived += _port_DataReceived;
        }
        private void Read()
        {
            string startValue = _port.ReadLine();
            if (startValue.Contains("-1"))
            {
                _packetReceived(_set);
                _set = new DataSet();
            }
            while (_port.BytesToRead > 0)
            {
                string value = _port.ReadLine().Replace("\r", "");
                if (value != "")
                    _set.AddValue(value);
            }
        }
        private void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Read();
        }
    }
}
