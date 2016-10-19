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
        private DataSet _set = new DataSet(ActionType.Charging);
        public Port(Action<DataSet> packetReceived)
        {
            _port = new SerialPort("COM3");
            _port.Open();
            _packetReceived = packetReceived;
            _port.DataReceived += _port_DataReceived;
            //StartProccess();
        }
        public void StartProccess()
        {
            SendByte(2);
        }
        public void StopProccess()
        {
            SendByte(3);
        }
        public void SendByte(int value)
        {
            _port.Write(value.ToString());
            _set.AddValue(new Read(0,0,ReadType.NewInstruction));
        }
        private void Read()
        {
            while (_port.BytesToRead > 0)
            {
                string value = _port.ReadLine().Replace("\r", "");
                if (value.Contains("-20"))
                {
                    _set.Type = ActionType.Charging;
                }
                if (value.Contains("-21"))
                {
                    _set.Type = ActionType.Discharging;
                }
                if(value.Contains("-end-"))
                {
                    _packetReceived(_set);
                }
                if (value != "")
                {
                    _set.AddValue(value);
                }
            }
        }
        private void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Read();
        }
    }
}
