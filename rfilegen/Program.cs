using System;
using System.IO;
using System.Security.Cryptography;


namespace rfilegen
{
    class Program
    {
        private static RNGCryptoServiceProvider CSP = new RNGCryptoServiceProvider();
        /// <summary>
        /// Random file generator
        /// Usage: rfilegen filename kbytes
        ///    where 
        ///         filename = full file name and path of file to be generated
        ///         kbytes = the number of kbytes to generate     
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string fname;
            int flength;
            Byte[] randombytes = new Byte[1024];  // 1K byte array

            
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: rfilegen filename length(kbytes)");
                return;
            }

            fname = args[0];
            flength = int.Parse(args[1]) * 1024;

            DateTime tstart = DateTime.Now;
            FileStream stream = new FileStream(fname, FileMode.Append,FileAccess.Write,FileShare.None,32768); 
                     
            for (int i = 0; i < flength; i++)
            {
                CSP.GetBytes(randombytes);
                stream.Write(randombytes, 0, 1024);
            }
            stream.Close();

            DateTime tend = DateTime.Now;
            TimeSpan t = tend.Subtract(tstart);
            Console.Write("     time: " + t.TotalSeconds.ToString("0.000") + " seconds  (" + ((long) (flength)/ t.TotalSeconds).ToString("0.0") + " KB/s)\n");

        }

    }
}
