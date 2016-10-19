using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDrawer
{
    public class Drawer
    {
        private Port _port;
        private Action<Image, DataSet> _drawAction;
        private int _margin = 100;
        private Pen _pen = new Pen(Color.Black,3);
        private Size _imageSize = new Size(2000, 1300);
        public Drawer(Action<Image,DataSet> drawAction)
        {
            _port = new Port(PacketReceived);
            _drawAction = drawAction;
        }
        private void DrawLines(Graphics graph)
        {
            Pen pen = new Pen(Color.Red,5);
            Pen penTicker = new Pen(Color.Red);
            List<int> pointsWhereWrite = new List<int>(new int[] { 0, 1, 2, 3, 4, 5 });
            RectangleF location;
            for (int i = 0; i < pointsWhereWrite.Count; i++)
            { 
                int value = i * 1024 ;
                value /= 5;
                location = new RectangleF(-_margin, -value - 20, _margin, 100);
                graph.DrawString($"{pointsWhereWrite[i]}V", new Font("Arial", 21), new SolidBrush(Color.Black), location);
                graph.DrawLine(penTicker, new Point(0, -value), new Point(_imageSize.Width, -value));
            }
            graph.DrawLine(pen, new Point(-_margin, 0), new Point(_margin + _imageSize.Width, 0));
            graph.DrawLine(pen, new Point(0, _margin), new Point(0, -_margin - _imageSize.Width));
        }
        public void Stop()
        {
            _port.StopProccess();
        }
        public void Start()
        {
            _port.StartProccess();
        }
        public void Charge()
        {
            _port.SendByte(1);
        }
        public void Discharge()
        {
            _port.SendByte(0);
        }
        private void PacketReceived(DataSet set)
        {
            Bitmap image = new Bitmap(_imageSize.Width, _imageSize.Height);
            Graphics graph = Graphics.FromImage(image);
            graph.TranslateTransform(_margin+10, image.Height- _margin);
            DrawLines(graph);
            List<Point> points = set.GetPoints();
            for (int i = 1; i < set.Count; i++)
            {
                if (set.GetReads()[i].Type == ReadType.NewInstruction)
                    graph.DrawLine(new Pen(Color.Red), new Point(points[i].X, 0), new Point(points[i].X, -_imageSize.Height));
                else
                    graph.DrawLine(_pen, points[i-1], points[i]);
            }
            _drawAction(image,set);
        }
    }
}
