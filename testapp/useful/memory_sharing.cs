using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testapp.useful
{
   
    class memory_sharing
    {
        MemoryMappedFile mmf;
        public memory_sharing()
        {

            mmf = MemoryMappedFile.CreateOrOpen("ppptttt", 4096);

        }

        public int  write2memory(string data) {
            try
            {
                if (mmf == null)
                {

                    mmf = MemoryMappedFile.CreateOrOpen("ppptttt", 4096);
                }
                using (var accessor = mmf.CreateViewAccessor(0, 1024))
                {

                    char[] m = new char[512];
                    char[] p = data.ToArray();
                    Array.Copy(p, m, p.Length);

                    accessor.WriteArray(0, m, 0, m.Length);
                }
                return 1;
            }
            catch {

                return -1;
            }

        }

        public int memory2str(out string data ) {
            try
            {
                data = "";
                if (mmf == null)
                {

                    mmf = MemoryMappedFile.CreateOrOpen("ppptttt", 4096);
                }
                using (var accessor = mmf.CreateViewAccessor(0, 1024))
                {
                    char[] m = new char[1024];
                    Array.Clear(m, 0, 1024);
                    if (accessor.CanRead)
                    {


                        accessor.ReadArray(0, m, 0, 1024);

                    }
                    int i = 0;
                    while ((int)m[++i] != 0) ;
                    char[] temp = new char[i == 0 ? 1 : i];
                    Array.Copy(m, temp, i);

                    string t = new string(temp).Trim();

                    data = t;
                    Array.Clear(m, 0, 1024);
                    accessor.WriteArray(0, m, 0, m.Length);
                }
                return 1;

            }
            catch {
                data = "";
                return -1;
            }





        }

        ~memory_sharing() {

            mmf.Dispose();

        }

    }
}
