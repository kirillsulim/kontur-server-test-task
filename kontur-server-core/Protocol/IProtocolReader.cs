using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kontur_server_core.Protocol
{
    /// <summary>
    /// Interface for protocol which can read
    /// </summary>
    public interface IProtocolReader
    {
        void WriteString(Stream s, string index);

        string ReadString(Stream s);

        void WriteStringArray(Stream s, string[] words);

        string[] ReadStringArray(Stream s);
    }
}
