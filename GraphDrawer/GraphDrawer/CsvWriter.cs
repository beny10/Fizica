using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDrawer
{
    public class CsvWriter
    {
        private StreamWriter _writer;
        private string _path;
        private bool _isClosed = false;
        public bool IsClosed
        {
            get
            {
                return _isClosed;
            }
        }
        public CsvWriter(string path)
        {
            _path = path;
            try
            {
                _writer = new StreamWriter(path);
            }
            catch(Exception ee)
            {
                System.Windows.Forms.MessageBox.Show(ee.Message);
            }
        }
        private void CalculateCapacity(DataSet set)
        {
            int start = 0;
            int closestIndex = start;
            var reads = set.GetReads();
            for (int i = start; i < set.Count; i++)
            {
                if (Helpers.UntilLimit(reads[i].Voltage) < Helpers.UntilLimit(reads[closestIndex].Voltage) && Helpers.UntilLimit(reads[i].Voltage) > 0)
                    closestIndex = i;
            }
            double voltage = reads[closestIndex].Voltage * 5.0 / 1024.0;
            _writer.WriteLine($"Cel mai apropiat voltag este{voltage}");
            _writer.WriteLine($"Timpul citirii este {reads[closestIndex].Time}ms");
            double capacitorValueInMicro = reads[closestIndex].Time / 10.0;
            _writer.WriteLine($"Cu o rezistenta de 10K capacitatea={capacitorValueInMicro / 10000 / 100}F\n={capacitorValueInMicro} micro farad");
        }
        public void Write(DataSet set)
        {
            CalculateCapacity(set);
            var data = set.GetReads();
            foreach(Read read in data)
            {
                _writer.WriteLine($"{read.Time},{Helpers.ConvertToVoltage(read.Voltage)}");
            }
        }
        public string CloseFile()
        {
            _writer.Close();
            _isClosed = true;
            return _path;
        }
    }
}
