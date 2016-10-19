using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDrawer
{
    public class DataSet
    {
        private List<Read> _set;
        private ActionType _type;
        public int Count
        {
            get
            {
                return _set.Count;
            }
        }
        public ActionType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }
        public DataSet(ActionType type)
        {
            _set = new List<Read>();
            _type = type;
        }
        public void AddValue(int time, int voltage)
        {
            Read read = new Read(time, voltage);
            _set.Add(read);
        }
        public void AddValue(Read read)
        {
            _set.Add(read);
        }
        public void AddValue(string value)
        {
            try
            {
                string[] data = value.Split('_');
                int time = Convert.ToInt32(data[0]);
                int voltage= Convert.ToInt32(data[1]);
                AddValue(time,voltage);
            }
            catch
            {
                //
            }
        }
        public List<Read> GetReads()
        {
            return _set;
        }
        public List<Point> GetPoints()
        {
            if(_set.Count>=1400)
            {
                _set.RemoveRange(0, 300);
                
            }
            List<Point> points = new List<Point>();
            for (int i = 0; i < _set.Count; i++)
            {
                Point point = new Point(i, _set[i].Voltage);
                Helpers.ApplyScaleToPoint(ref point);
                points.Add(point);
            }
            return points;
        }
    }
}
