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
        private List<Point> _set;
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
        }
        public DataSet(ActionType type)
        {
            _set = new List<Point>();
            _type = type;
        }
        public void AddValue(int value)
        {
            Point point = new Point(Count+1 , value);
            Helpers.ApplyScaleToPoint(ref point);
            _set.Add(point);
        }
        public void AddValue(string value)
        {
            try
            {
                AddValue(Convert.ToInt32(value));
            }
            catch
            {
                //
            }
        }
        public List<Point> GetPoints()
        {
            return _set;
        }
    }
}
