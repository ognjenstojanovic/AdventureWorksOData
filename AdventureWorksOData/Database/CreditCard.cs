﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AdventureWorksOData.Database
{
    public partial class CreditCard
    {
        public CreditCard()
        {
            PersonCreditCards = new HashSet<PersonCreditCard>();
            SalesOrderHeaders = new HashSet<SalesOrderHeader>();
        }

        [Key]
        public int CreditCardId { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public byte ExpMonth { get; set; }
        public short ExpYear { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<PersonCreditCard> PersonCreditCards { get; set; }
        public virtual ICollection<SalesOrderHeader> SalesOrderHeaders { get; set; }
    }
}
