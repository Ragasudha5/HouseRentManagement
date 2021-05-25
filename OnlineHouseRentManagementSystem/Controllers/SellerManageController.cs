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
    public class SellerManageController : Controller
    {
        private DBuserSignupLoginEntities db = new DBuserSignupLoginEntities();

        // GET: SellerManage
        public async Task<ActionResult> Index()
        {
            return View(await db.TBLSellerInfoes.ToListAsync());
        }

        // GET: SellerManage/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLSellerInfo tBLSellerInfo = await db.TBLSellerInfoes.FindAsync(id);
            if (tBLSellerInfo == null)
            {
                return HttpNotFound();
            }
            return View(tBLSellerInfo);
        }

        // GET: SellerManage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SellerManage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdUs,FirstName,LastName,Email,Password,Status")] TBLSellerInfo tBLSellerInfo)
        {
            if (ModelState.IsValid)
            {
                db.TBLSellerInfoes.Add(tBLSellerInfo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tBLSellerInfo);
        }

        // GET: SellerManage/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLSellerInfo tBLSellerInfo = await db.TBLSellerInfoes.FindAsync(id);
            if (tBLSellerInfo == null)
            {
                return HttpNotFound();
            }
            return View(tBLSellerInfo);
        }

        // POST: SellerManage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdUs,FirstName,LastName,Email,Password,Status")] TBLSellerInfo tBLSellerInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBLSellerInfo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("SellerDetails","Admin");
            }
            return View(tBLSellerInfo);
        }

        // GET: SellerManage/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLSellerInfo tBLSellerInfo = await db.TBLSellerInfoes.FindAsync(id);
            if (tBLSellerInfo == null)
            {
                return HttpNotFound();
            }
            return View(tBLSellerInfo);
        }

        // POST: SellerManage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TBLSellerInfo tBLSellerInfo = await db.TBLSellerInfoes.FindAsync(id);
            db.TBLSellerInfoes.Remove(tBLSellerInfo);
            await db.SaveChangesAsync();
            return RedirectToAction("SellerDetails", "Admin");
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
