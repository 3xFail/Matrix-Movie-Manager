using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestingGround
{
    class Program
    {
        static void Main(string[] args)
        {
            //string path = @"E:\Movies";
            string[] path = { @"E:\Movies", @"E:\TV Shows" };
            string[] filetypes = { ".mkv", ".mp4" };
            Accessor accesser = new Accessor(path, filetypes);


            foreach (string file in accesser.Get_All_Files())
            {
                Console.WriteLine(file);
            }


            Console.WriteLine("pause");
        }
    }


    public class Accessor
    {
        //single path constructor
        public Accessor(string path)
        {
            string[] temp;

            temp = System.IO.Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            foreach (string file in temp)
            {
                all_filenames.Add(Path.GetFileName(file));
            }
        }

        //filtering ctor
        public Accessor(string path, string[] file_types)
        {
            string[] temp;

            if (file_types != null)
            {
                temp = System.IO.Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

                foreach (string file in temp)
                {
                    foreach (string type in file_types)
                    {
                        if (Path.GetFileName(file).Contains(type))
                        {
                            all_filenames.Add(Path.GetFileName(file));
                            break;
                        }
                    }

                }
            }
            else
                throw new InvalidOperationException("File type Was null");
        }

        //multi-path ctor
        public Accessor(string[] paths, string[] file_types)
        {

            if (paths != null && file_types != null)
            {
                foreach (string path in paths)
                {
                    string[] temp;

                    temp = System.IO.Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

                    foreach (string file in temp)
                    {
                        foreach (string type in file_types)
                        {
                            if (Path.GetFileName(file).Contains(type))
                            {
                                all_filenames.Add(Path.GetFileName(file));
                                break;
                            }
                        }
                    }
                }
            }
            else
                throw new InvalidOperationException("Paths was null / File types was null");
        }


        public string[] Get_All_Files()
        {
            return all_filenames.ToArray();
        }

        //public string[] Get_Filtered_Files()
        //{
        //    return filtered_filenames.ToArray();
        //}

        public List<string> all_filenames = new List<string>();
        //public List<string> filtered_filenames =  new List<string>();


    }



}
