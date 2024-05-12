using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CubeNetsLibrary
{
    public class TreeDataReader
    {
        List<FileInfo> datafiles;

        public TreeDataReader() {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var dataPath = Path.Combine(basePath, "TreeData");
            var files = Directory.GetFiles(dataPath, "*.txt");

            datafiles = files.Select(f => new FileInfo(f)).ToList();
        }

        public bool TreeDataAvailable(int nodes)
        {
            string name = $"{nodes:D2}.txt";
            return datafiles.Any(df => df.Name == name);
        }

    }
}
