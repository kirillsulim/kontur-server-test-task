using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUtils
{
    /// <summary>
    /// Manage streams positions
    /// </summary>
    public class StreamPositionSaver
    {
        private Dictionary<Stream, long> posMap = new Dictionary<Stream, long>();

        /// <summary>
        /// Save stream position
        /// </summary>
        /// <param name="s">stream</param>
        public void SavePosition(Stream s)
        {
            posMap[s] = s.Position;
        }

        /// <summary>
        /// Restore stream position from saved
        /// </summary>
        /// <param name="s">stream</param>
        public void RestorePosition(Stream s)
        {
            s.Position = posMap[s];
        }
    }
}
