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
        private int _valuesReaded = 40;
        public Port(Action<DataSet> packetReceived)
        {
            _port = new SerialPort("COM3");
            _port.Open();
            _packetReceived = packetReceived;
            _port.DataReceived += _port_DataReceived;
        }
        private void Read()
        {
            if(_port.BytesToRead> _valuesReaded)
            {
                string startValue = _port.ReadLine();
                if(!startValue.Contains("-1"))
                {
                    Read();
                    return;
                }
                DataSet set = new DataSet();
                for (int i = 0; i < _valuesReaded; i++)        
                {
                    string value = _port.ReadLine().Replace("\r", "");
                    if(value!="")
                        set.AddValue(value);
                }
                _packetReceived(set);
            }
        }
        private void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Read();
        }
    }
}
