using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kontur_server_core.Protocol
{
    public class NumberedProtocolReader : IProtocolReader
    {
        private Encoding encoding = Encoding.ASCII;

        public void WriteString(Stream stream, string index)
        {
            byte[] bytes = encoding.GetBytes(index);
            int count = bytes.Length;

            BinaryWriter bWriter = new BinaryWriter(stream);
            bWriter.Write(count);
            bWriter.Write(bytes);
        }

        public string ReadString(Stream stream)
        {
            BinaryReader bReader = new BinaryReader(stream);
            int count = bReader.ReadInt32();
            byte[] bytes = bReader.ReadBytes(count);

            return encoding.GetString(bytes);
        }

        public void WriteStringArray(Stream stream, string[] words)
        {
            StringBuilder b = new StringBuilder();
            foreach (var s in words)
            {
                b.Append(s + "\n");
            }
            var zs = b.ToString();
            byte[] bytes = encoding.GetBytes(zs);
            int count = bytes.Length;

            BinaryWriter bWriter = new BinaryWriter(stream);
            bWriter.Write(count);
            bWriter.Write(bytes);
        }

        public string[] ReadStringArray(Stream stream)
        {
            BinaryReader bReader = new BinaryReader(stream);
            int count = bReader.ReadInt32();
            byte[] bytes = bReader.ReadBytes(count);

            return encoding.GetString(bytes)
                .Split('\n').Where(x => x.Length >0).ToArray();
        }
    }
}
