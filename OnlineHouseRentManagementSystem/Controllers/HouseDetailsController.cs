using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineHouseRentManagementSystem.Models;
using System.Data.SqlClient;
using System.Configuration;


namespace OnlineHouseRentManagementSystem.Controllers
{
    public class HouseDetailController : Controller
    {
        private DBuserSignupLoginEntities1 db = new DBuserSignupLoginEntities1();

        // GET: HouseDetail
        public async Task<ActionResult> Index()
        {
            return View(await db.HouseDetails.ToListAsync());
        }
                    

        // GET: HouseDetail/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HouseDetail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdUs,Name,Email,ContactNumber,Location,Bedrooms,HouseRent,Description")] HouseDetail houseDetail)
        {
            if (ModelState.IsValid)
            {
                db.HouseDetails.Add(houseDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("PopulateData", "Seller");
            }

            return View(houseDetail);
        }

        // GET: HouseDetail/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseDetail houseDetail = await db.HouseDetails.FindAsync(id);
            if (houseDetail == null)
            {
                return HttpNotFound();
            }
            return View(houseDetail);
        }

        // POST: HouseDetail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdUs,Name,Email,ContactNumber,Location,Bedrooms,HouseRent,Description")] HouseDetail houseDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(houseDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("PopulateData", "Seller");
            }
            return View(houseDetail);
        }

        // GET: HouseDetail/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseDetail houseDetail = await db.HouseDetails.FindAsync(id);
            if (houseDetail == null)
            {
                return HttpNotFound();
            }
            return View(houseDetail);
        }

        // POST: HouseDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HouseDetail houseDetail = await db.HouseDetails.FindAsync(id);
            db.HouseDetails.Remove(houseDetail);
            await db.SaveChangesAsync();
            return RedirectToAction("PopulateData", "Seller");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
