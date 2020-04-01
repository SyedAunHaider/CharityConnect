using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharityConnect.Backend.BusinessAccess
{
    public class UserRegistration
    {
        public string CNIC { get; set; }
        public string MobileNo { get; set; }
        public int? FamilyMembersCount { get; set; }
        public DateTime? CharityDistributionDate { get; set; }
        public int PConstituencyId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
