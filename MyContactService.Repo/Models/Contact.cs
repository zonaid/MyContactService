using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ProtoBuf;

namespace MyContactService.Repo.Models
{
    [Serializable]
    [ProtoBuf.ProtoContract]
    public class Contact
    {
        [ProtoMember(1)]
        public string ID { get; set; }

        [ProtoMember(2)]
        public string FirstName { get; set; }

        [ProtoMember(3)]
        public string LastName { get; set; }

        [ProtoMember(4)]
        public string ContactNo { get; set; }

        [ProtoMember(5)]
        public string Group { get; set; }

        [ProtoMember(6)]
        public string Photo { get; set; }
        public Contact()
        {
            this.ID = Guid.NewGuid().ToString();
        }
    }
}