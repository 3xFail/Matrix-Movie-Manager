using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix_Movie_Manager.Framework
{
    public class Accessor
    {
        public Accessor()
        {

        }
        //single path constructor
        public Accessor(string path)
        {
            string[] temp;

            temp = System.IO.Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            foreach (string file in temp)
            {
                all_filenames.Add(Path.GetFileName(file));
                all_paths.Add(file);
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
                            all_filenames.Add(Path.GetFileNameWithoutExtension(file));
                            all_paths.Add(file);
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
                            if (Path.GetFileName(file).Contains(type.ToLower()))
                            {
                                all_filenames.Add(Path.GetFileNameWithoutExtension(file));
                                all_paths.Add(file);
                                break;
                            }
                        }
                    }
                }
            }
            else
                throw new InvalidOperationException("Paths was null / File types was null");
        }


        public List<string> Get_All_Files()
        {
            return all_filenames;
        }

        public List<string> Get_All_Paths()
        {
            return all_paths;
        }

        private List<string> all_filenames = new List<string>();
        private List<string> all_paths = new List<string>();

    }
}
