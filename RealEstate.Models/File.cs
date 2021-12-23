using RealEstate.Models.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    public class File : BaseEntity
    {
        public byte[] Data { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public int SizeInBytes { get; set; }

        public File()
        {
        }

        public File(string fileName, byte[] data)
        {
            FileName = fileName;
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                Extension = Path.GetExtension(fileName);
            }

            Data = data;
            SizeInBytes = data.Length;
        }
    }
}
