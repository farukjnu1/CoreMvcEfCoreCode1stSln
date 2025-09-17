using CoreMvcEfCoreCode1st.Models;
using CoreMvcEfCoreCode1st.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CoreMvcEfCoreCode1st.Controllers
{
    public class SalesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contacts()
        {
            var listContact = new List<ContactVm>();
            using (var _ctx = new SalesContext())
            {
                listContact = (from x in _ctx.Contact
                                   select new ContactVm
                                   {
                                       Id=x.Id,
                                       ContactName=x.ContactName,
                                       DateOfBirth = x.DateOfBirth,
                                       Gender = x.Gender
                                   }).ToList();
            }
            return Json(listContact);
        }

        [HttpGet]
        public IActionResult Contact(int id)
        {
            var oContact = new ContactVm();
            using (var _ctx = new SalesContext())
            {
                oContact = (from x in _ctx.Contact
                            where x.Id == id
                            select new ContactVm
                            {
                                Id = x.Id,
                                ContactName = x.ContactName,
                                DateOfBirth = x.DateOfBirth,
                                Gender = x.Gender,
                                SubContacts = (from y in _ctx.SubContact
                                               where y.ContactId == id
                                               select new SubContactVm
                                               {
                                                   ContactId = y.ContactId,
                                                   Id = y.Id,
                                                   Name = y.Name
                                               }).ToList()
                            }).FirstOrDefault();
            }
            return Json(oContact);
        }

        //[RequestSizeLimit(2147483648)]
        [HttpPost]
        public IActionResult ContactAdd([FromBody] ContactVm contact)
        {
            object res = null; var _ctx = new SalesContext();
            var oContact = _ctx.Contact.Where(x => x.Id == contact.Id).FirstOrDefault();
            if (oContact == null)
            {
                oContact = new Contact();
                oContact.ContactName = contact.ContactName;
                oContact.DateOfBirth = contact.DateOfBirth;
                oContact.Gender = contact.Gender;
                oContact.ContactType = contact.ContactType;
                _ctx.Add(oContact);
                _ctx.SaveChanges();
                var listSubContactRem = _ctx.SubContact.Where(x => x.ContactId == oContact.Id).ToList();
                _ctx.SubContact.RemoveRange(listSubContactRem);
                _ctx.SaveChanges();
                var listSubContact = new List<SubContact>();
                foreach (var oSC in contact.SubContacts)
                {
                    var oSubContact = new SubContact();
                    oSubContact.Name = oSC.Name;
                    oSubContact.ContactId = oContact.Id;
                    listSubContact.Add(oSubContact);
                }
                _ctx.AddRange(listSubContact);
                _ctx.SaveChanges();
                res = new
                {
                    resstate = true
                };
            }
            return Json(res);
        }

        [HttpPut]
        public IActionResult ContactEdit([FromBody] ContactVm contact)
        {
            object res = null; var _ctx = new SalesContext();
            var oContact = _ctx.Contact.Where(x => x.Id == contact.Id).FirstOrDefault();
            if (oContact != null)
            {
                oContact.ContactName = contact.ContactName;
                oContact.DateOfBirth = contact.DateOfBirth;
                oContact.Gender = contact.Gender;
                oContact.ContactType = contact.ContactType;
                _ctx.SaveChanges();
                var listSubContactRem = _ctx.SubContact.Where(x=>x.ContactId== oContact.Id).ToList();
                _ctx.SubContact.RemoveRange(listSubContactRem);
                _ctx.SaveChanges();
                var listSubContact = new List<SubContact>();
                foreach (var oSC in contact.SubContacts)
                {
                    var oSubContact = new SubContact();
                    oSubContact.Name = oSC.Name;
                    oSubContact.ContactId = oContact.Id;
                    listSubContact.Add(oSubContact);
                }
                _ctx.AddRange(listSubContact);
                _ctx.SaveChanges();
                res = new
                {
                    resstate = true
                };
            }
            return Json(res);
        }

        [HttpDelete]
        public IActionResult ContactRemove([FromQuery] int id)
        {
            object res = null; var _ctx = new SalesContext();
            var oContact = _ctx.Contact.Where(x => x.Id == id).FirstOrDefault();
            if (oContact != null)
            {
                var listSubContactRem = _ctx.SubContact.Where(x => x.ContactId == oContact.Id).ToList();
                _ctx.SubContact.RemoveRange(listSubContactRem);
                _ctx.SaveChanges();
                _ctx.Contact.Remove(oContact);
                _ctx.SaveChanges();
                res = new
                {
                    resstate = true
                };
            }
            return Json(res);
        }
    }
}
