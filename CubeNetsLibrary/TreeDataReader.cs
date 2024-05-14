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

        public List<Tree> GetTrees(int nodes)
        {
            List<Tree> trees = new List<Tree>();

            string name = $"{nodes:D2}.txt";
            FileInfo file = datafiles.Find(df => df.Name == name);
            if (file == null) return trees;

            string line;
            string[] nums;
            List<int> ints = new List<int>();
            using (TextReader tr = new StreamReader(file.FullName))
            {
                while ((line = tr.ReadLine()) != null)
                {
                    nums = line.Split(' ');
                    ints.Clear();
                    foreach (string s in nums) if (int.TryParse(s, out int i)) ints.Add(i);
                    if (ints.Count == nodes) trees.Add(new Tree());
                }
            }

            return trees;
        }

    }
}
