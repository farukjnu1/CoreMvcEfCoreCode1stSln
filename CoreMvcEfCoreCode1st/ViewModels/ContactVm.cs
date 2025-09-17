using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcEfCoreCode1st.ViewModels
{
    public class ContactVm
    {
        public int Id { get; set; }
        public string ContactName { get; set; }
        public string ContactType { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public List<SubContactVm> SubContacts { get; set; } = new List<SubContactVm>();
    }
}
