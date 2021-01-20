using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AdventureWorksOData.Database
{
    public partial class PhoneNumberType
    {
        public PhoneNumberType()
        {
            PersonPhones = new HashSet<PersonPhone>();
        }

        [Key]
        public int PhoneNumberTypeId { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<PersonPhone> PersonPhones { get; set; }
    }
}
