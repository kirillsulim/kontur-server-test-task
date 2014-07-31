using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kontur_server_core.Protocol
{
    public interface IProtocolReader
    {
        void WriteString(string index);

        string ReadString();

        void WriteStringArray(string[] words);

        string[] ReadStringArray();

        void Connect(Stream s);

        void Disconnect();
    }
}
