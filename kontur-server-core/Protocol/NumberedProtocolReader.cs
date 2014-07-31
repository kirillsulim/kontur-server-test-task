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
        private Stream stream;

        private Encoding encoding = Encoding.ASCII;

        public void Connect(Stream stream)
        {
            this.stream = stream;
        }

        public void Disconnect()
        {
            this.stream = null;
        }

        public void WriteString(string index)
        {
            byte[] bytes = encoding.GetBytes(index);
            int count = bytes.Length;

            BinaryWriter bWriter = new BinaryWriter(stream);
            bWriter.Write(count);
            bWriter.Write(bytes);
        }

        public string ReadString()
        {
            BinaryReader bReader = new BinaryReader(stream);
            int count = bReader.ReadInt32();
            byte[] bytes = bReader.ReadBytes(count);

            return encoding.GetString(bytes);
        }

        public void WriteStringArray(string[] words)
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

        public string[] ReadStringArray()
        {
            BinaryReader bReader = new BinaryReader(stream);
            int count = bReader.ReadInt32();
            byte[] bytes = bReader.ReadBytes(count);

            return encoding.GetString(bytes)
                .Split('\n').Where(x => x.Length >0).ToArray();
        }
    }
}
