using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace ImageSaver.DAL.Entities
{
    public class ImageItem
    {
        public int ID { get; set; }

        public string Url { get; set; }

        public string FileName { get; set; }
        public byte[] ImageData { get;  set; }
    }
}