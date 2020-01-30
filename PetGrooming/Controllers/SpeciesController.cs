using System;
using System.Collections.Generic;
using System.Data;
//required for SqlParameter class
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetGrooming.Data;
using PetGrooming.Models;
using System.Diagnostics;

namespace PetGrooming.Controllers
{
    public class SpeciesController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }
        private PetGroomingContext db = new PetGroomingContext();
      
        public ActionResult List()
        {
           //Getting list of species in List.cshtml
            List<Species> specie = db.Species.SqlQuery("Select * from Species").ToList();
            return View(specie);
        }

        // Show
        public ActionResult Show(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //getting details of species of respective species upon clicking
            Species Species = db.Species.SqlQuery("select * from species where speciesid=@SpeciesID", new SqlParameter("@SpeciesID", id)).FirstOrDefault();
            if (Species == null)
            {
                return HttpNotFound();
            }
            return View(Species);
        }

        //add
        public ActionResult Add()
        {
            return View();
        }
        // [HttpPost] Add
        [HttpPost]
        public ActionResult Add(string speciesName)
        {
            // Inserting into species table their respective name 
            string query = "insert into species(Name) values(@speciesName)";
            SqlParameter[] sqlparams = new SqlParameter[1];
            sqlparams[0] = new SqlParameter("@speciesName", speciesName);
            db.Database.ExecuteSqlCommand(query,sqlparams);
            //going back to species list after adding new species
            return RedirectToAction("List");
        }
        
        // Update
        public ActionResult Update(int id)
        {
            //selecting details of species for the respective textbox
            Species selectedspecie = db.Species.SqlQuery("Select * from species where speciesid = @speciesID", new SqlParameter("@speciesID", id)).FirstOrDefault();
            //need the species data
            return View(selectedspecie);
        }
        // [HttpPost] Update
        [HttpPost]
        public ActionResult Update(string speciesName, int id)
        {
            //pulling  data
            Debug.WriteLine("I am pulling data of " + speciesName);
            //updating species details
            string query = "update species set Name = @speciesName where speciesid =" + id;
            SqlParameter[] sqlparams = new SqlParameter[1];
            sqlparams[0] = new SqlParameter("@speciesName", speciesName);
            db.Database.ExecuteSqlCommand(query, sqlparams);
            //going  back to species update
            return RedirectToAction("List");
        }
        // (optional) delete
        public ActionResult Delete(int? id)
        {
            //deleting respective species
            string query = "delete from species where speciesid = " + id;
            db.Database.ExecuteSqlCommand(query);
            //going back to species list after deletion
            return RedirectToAction("List");
        }
        
    }
}