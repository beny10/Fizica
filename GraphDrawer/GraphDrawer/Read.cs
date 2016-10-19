using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDrawer
{
    public class Read
    {
        private int _voltage;
        private int _time;
        private ReadType _type=ReadType.Read;

        public ReadType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public int Time
        {
            get { return _time; }
            set { _time = value; }
        }

        public int Voltage
        {
            get { return _voltage; }
            set { _voltage = value; }
        }
        public Read(int time, int voltage)
        {
            _voltage = voltage;
            _time = time;
        }
        public Read(int time, int voltage,ReadType type)
        {
            _voltage = voltage;
            _time = time;
            _type = type;
        }
    }
}
