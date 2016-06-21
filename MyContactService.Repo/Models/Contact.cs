using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyContactService.Repo.Models
{
    [Serializable]
    [ProtoBuf.ProtoContract]
    public class Contact
    {
 
        public string ID { get; }
 
        public string FirstName { get; set; }
 
        public string LastName { get; set; }
 
        public string ContactNo { get; set; }
 
        public string Group { get; set; }
 
        public string Photo { get; set; }
        public Contact()
        {
            this.ID = Guid.NewGuid().ToString();
        }
    }
}