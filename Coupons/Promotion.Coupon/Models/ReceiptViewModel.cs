using System.Collections.Generic;
using System.IO;
using System.Web;
using Promotion.Coupon.Entity.Entities;

namespace Promotion.Coupon.Models
{
    public class ReceiptViewModel
    {
        public Receipt Receipt { get; set; }
        public List<Receipt> Receipts { get; set; }
        public HttpPostedFileBase ReceiptFile { get; set; }
        public List<Product> ProductList { get; set; }
        public string Status { get; set; }
        public string ErrorCode { get; set; }
        public string cpf { get; set; }
        public string productType { get; set; }
        public Person person { get; set; }

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

            public override int ContentLength => (int)stream.Length;

            public override string ContentType => contentType;

            public override string FileName => fileName;

            public override Stream InputStream => stream;

            public override void SaveAs(string filename)
            {
                filename = filename.Replace("~", "");

                using (var file = File.Open(filename, FileMode.CreateNew))
                    stream.CopyTo(file);
            }
        }
    }
}