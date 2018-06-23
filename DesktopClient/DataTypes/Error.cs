using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DesktopClient.DataTypes
{
    public class Error
    {
        public string SourceName { get; set; }
        public string Date { get; set; }
        public string SourceError { get; set; }
        public BitmapImage Image { get; set; }

        public Error(string sourceName, string date, string sourceError, BitmapImage image)
        {
            SourceName = sourceName;
            Date = date;
            SourceError = sourceError;
            Image = image;
        }
    }
}