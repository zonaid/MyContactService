using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ProtoBuf;

namespace MyContactService.Repo.Models
{
    [Serializable]
    public class Contact
    {

        public string ID { get; set; }


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