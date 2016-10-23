using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDrawer
{
    public static class Helpers
    {
        public static Size Scale = new Size(1, -1);
        public static void ApplyScaleToPoint(ref Point point)
        {
            point = new Point(point.X * Scale.Width, point.Y * Scale.Height);
        }
        public static int UntilLimit(int voltage)
        {
            return 645 - voltage;
        }
    }
}
