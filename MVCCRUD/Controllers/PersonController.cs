using MVCCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCCRUD.Controllers
{
    
    public class PersonController : Controller
    {
        Database001Entities Contextmodel = new Database001Entities();
        public ActionResult Register()
        {
            return View("Register");
        }
        public ActionResult AddPerson()
        {
            PersonModel model = new PersonModel();
            return View("AddPerson",model);   
        }
        
        [HttpPost]
        public ActionResult AddPerson(PersonModel model)
        {
           
            Contextmodel.People.Add(new Person() { PersonName = model.PersonName, PersonAddress = model.PersonAddress });
            Contextmodel.SaveChanges();
            return View("AddPerson", model);
        }
        public  ActionResult DisplayPersons()
        {
            
            var PersonRecords = Contextmodel.People.ToList();
            return View("DisplayPersons", PersonRecords);
        }
        public ActionResult EditPerson(int personid)
        {
            var personRec = (from item in Contextmodel.People
                             where item.PersonId == personid
                             select item).First();
            return View("EditPerson",personRec);
        }
        [HttpPost]
        public ActionResult EditPerson(Person model)
        {
            var personRec = (from item in Contextmodel.People
                             where item.PersonId == model.PersonId
                             select item).First();

            personRec.PersonName = model.PersonName;
            personRec.PersonAddress = model.PersonAddress;

            Contextmodel.SaveChanges();

            var PersonRecords = Contextmodel.People.ToList();
            return View("DisplayPersons", PersonRecords);
        }

        public ActionResult DeletePerson(int personId)
        {

            var personRec = (from item in Contextmodel.People
                             where item.PersonId == personId
                             select item).First();
            Contextmodel.People.Remove(personRec);
            Contextmodel.SaveChanges();
            var PersonRecords = Contextmodel.People.ToList();
            return View("DisplayPersons", PersonRecords);
        }
    }
}