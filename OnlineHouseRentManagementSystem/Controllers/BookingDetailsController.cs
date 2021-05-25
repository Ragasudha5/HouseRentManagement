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

namespace OnlineHouseRentManagementSystem.Controllers
{
    public class BookingDetailsController : Controller
    {
        private DBuserSignupLoginEntities4 db = new DBuserSignupLoginEntities4();

        // GET: BookingDetails
        public async Task<ActionResult> Index()
        {
            return View(await db.BookingDetails.ToListAsync());
        }

        // GET: BookingDetails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingDetail bookingDetail = await db.BookingDetails.FindAsync(id);
            if (bookingDetail == null)
            {
                return HttpNotFound();
            }
            return View(bookingDetail);
        }

        // GET: BookingDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookingDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdUs,BuyerEmail,SellerEmail,Location,HouseRent,Description,Status")] BookingDetail bookingDetail)
        {
            if (ModelState.IsValid)
            {
                db.BookingDetails.Add(bookingDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("BookingDetails","Buyer");
            }

            return View(bookingDetail);
        }

        // GET: BookingDetails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingDetail bookingDetail = await db.BookingDetails.FindAsync(id);
            if (bookingDetail == null)
            {
                return HttpNotFound();
            }
            return View(bookingDetail);
        }

        // POST: BookingDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdUs,BuyerEmail,SellerEmail,Location,HouseRent,Description,Status")] BookingDetail bookingDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookingDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("BookingDetails","Admin");
            }
            return View(bookingDetail);
        }

        // GET: BookingDetails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingDetail bookingDetail = await db.BookingDetails.FindAsync(id);
            if (bookingDetail == null)
            {
                return HttpNotFound();
            }
            return View(bookingDetail);
        }

        // POST: BookingDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BookingDetail bookingDetail = await db.BookingDetails.FindAsync(id);
            db.BookingDetails.Remove(bookingDetail);
            await db.SaveChangesAsync();
            return RedirectToAction("BookingDetails","Admin");
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
