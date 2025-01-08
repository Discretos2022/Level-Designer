using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discretos_Level_Designer
{
    public class LevelDataSet
    {

        public string Name;
        public string Date;
        public string FileSize;
        public string Path;

        public LevelDataSet(string name, string date, string fileSize, string path)
        {

            Name = name;
            Date = date;
            FileSize = fileSize;
            Path = path;

        }

    }
}
