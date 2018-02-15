using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ShiftInc.Raizen.ShellTanqueCheio.Web.Models
{
    public class ReceiptViewModel
    {
        public Entity.Receipt Receipt { get; set; }
        public List<Entity.Receipt> Receipts { get; set; }
        public HttpPostedFileBase ReceiptFile { get; set; }
        public List<Entity.Product> ProductList { get; set; }
        public string Status { get; set; }
        public string ErrorCode { get; set; }
        public string cpf { get; set; }
        public string productType { get; set; }
        public Entity.Person person { get; set; }
    }

    public class MemoryFile : HttpPostedFileBase
    {
        Stream stream;
        string contentType;
        string fileName;

        public MemoryFile(Stream stream, string contentType, string fileName)
        {
            this.stream = stream;
            this.contentType = contentType;
            this.fileName = fileName;
        }

        public override int ContentLength
        {
            get { return (int)stream.Length; }
        }

        public override string ContentType
        {
            get { return contentType; }
        }

        public override string FileName
        {
            get { return fileName; }
        }

        public override Stream InputStream
        {
            get { return stream; }
        }

        public override void SaveAs(string filename)
        {
            filename = filename.Replace("~", "");

            using (var file = File.Open(filename, FileMode.CreateNew))
                stream.CopyTo(file);
        }
    }
}