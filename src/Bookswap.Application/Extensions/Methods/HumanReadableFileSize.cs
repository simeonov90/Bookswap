using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookswap.Application.Extensions.Methods
{
    public static class HumanReadableFileSize
    {
        public static string ReadableFileSize(long fileLength)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = fileLength;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }

            return string.Format("{0:0.##} {1}", len, sizes[order]);
        }
    }
}
