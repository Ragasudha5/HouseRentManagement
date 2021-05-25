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
    public class BuyerManageController : Controller
    {
        private DBuserSignupLoginEntities db = new DBuserSignupLoginEntities();

        // GET: BuyerManage
        public async Task<ActionResult> Index()
        {
            return View(await db.TBLUserInfoes.ToListAsync());
        }

        // GET: BuyerManage/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLUserInfo tBLUserInfo = await db.TBLUserInfoes.FindAsync(id);
            if (tBLUserInfo == null)
            {
                return HttpNotFound();
            }
            return View(tBLUserInfo);
        }

        // GET: BuyerManage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BuyerManage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdUs,FirstName,LastName,Email,Password,Status")] TBLUserInfo tBLUserInfo)
        {
            if (ModelState.IsValid)
            {
                db.TBLUserInfoes.Add(tBLUserInfo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tBLUserInfo);
        }

        // GET: BuyerManage/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLUserInfo tBLUserInfo = await db.TBLUserInfoes.FindAsync(id);
            if (tBLUserInfo == null)
            {
                return HttpNotFound();
            }
            return View(tBLUserInfo);
        }
        // POST: BuyerManage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdUs,FirstName,LastName,Email,Password,Status")] TBLUserInfo tBLUserInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBLUserInfo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("BuyerDetails", "Admin");
            }
            return View(tBLUserInfo);
        }

        // GET: BuyerManage/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLUserInfo tBLUserInfo = await db.TBLUserInfoes.FindAsync(id);
            if (tBLUserInfo == null)
            {
                return HttpNotFound();
            }
            return View(tBLUserInfo);
        }

        // POST: BuyerManage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TBLUserInfo tBLUserInfo = await db.TBLUserInfoes.FindAsync(id);
            db.TBLUserInfoes.Remove(tBLUserInfo);
            await db.SaveChangesAsync();
            return RedirectToAction("BuyerDetails", "Admin");
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
