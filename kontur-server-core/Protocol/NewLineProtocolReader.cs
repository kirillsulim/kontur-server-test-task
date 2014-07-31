using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kontur_server_core.Protocol
{
    public class NewLineProtocolReader : IProtocolReader
    {
        private Encoding encoding = Encoding.ASCII;

        public void WriteString(Stream stream, string index)
        {
            StreamWriter writer = new StreamWriter(stream);

            writer.WriteLine(index);
            writer.WriteLine();
            writer.Flush();
        }

        public string ReadString(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);

            string res = "";

            string s;
            do
            {
                s = reader.ReadLine();
                res += s;
            }
            while (s != "");

            return res;            
        }

        public void WriteStringArray(Stream stream, string[] words)
        {
            StreamWriter writer = new StreamWriter(stream);

            foreach (var w in words)
            {
                writer.WriteLine(w);
            }            
            writer.WriteLine();
            writer.Flush();            
        }

        public string[] ReadStringArray(Stream stream)
        {
            List<string> res = new List<string>();
            StreamReader reader = new StreamReader(stream);

            string s;
            while(true)
            {
                s = reader.ReadLine();
                if (s == "") break;
                res.Add(s);
            }
            return res.ToArray();
        }
    }
}
