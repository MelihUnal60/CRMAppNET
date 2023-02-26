using CRMAppNET.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMAppNET.Domain.Entities
{
    public sealed class Opportunity:AuditEntity
    {
        public string Customer { get; set; }

        public string Status { get; set; }

        public string Owner { get; set; }

        public int CategoryId { get; set; }
    }
}
