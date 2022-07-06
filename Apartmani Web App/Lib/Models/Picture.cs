using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Models
{
    public class Picture
    {
        public int ID { get; set; }
        public string Guid { get; set; }
        public string Base64 { get; set; }
        public string Name { get; set; }
        public bool IsRepresentative { get; set; }
        public string Base64ForHtml
        {
            get => "data:image/jpeg;base64," + Base64;
        }

        public override string ToString() => Base64;
    }
}
