using System;
using System.Collections.Generic;
using System.IO;

namespace DataHandling
{

    public class DataHandler
    {
        private const string folder = @"Files/";
        private const string extension = ".txt";
     

        public String[] Load(string name)
        {

            String[] lines = File.ReadAllLines(folder + name);
            return lines;
        }

        public void Save(List<string> data)
        {
            File.WriteAllLines(folder + DateTime.Now.ToShortDateString() +"_"+ DateTime.Now.ToFileTimeUtc() + extension, data.ToArray());
        }

        public string[] QueryFilesFromFolder()
        {
            if (!Directory.Exists(folder)) { Directory.CreateDirectory(folder); }
            DirectoryInfo info = new DirectoryInfo(folder);
            var result = info.GetFiles("*" + extension);
            string[] temp = new string[result.Length];
            int i = 0;
            foreach (var item in result)
            {
                temp[i] = item.Name;
                i++;
            }
            return temp;
        }

        public void Delete(string name)
        {
            if (File.Exists(folder + name))
            {
                File.Delete(folder + name);
            }
        }

        public bool CheckIfFileExists(string name)
        {
            return File.Exists(folder + name + extension);
        }
    }
}
