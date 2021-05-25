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
    public class TBLSellerInfoesController : Controller
    {
        private DBuserSignupLoginEntities db = new DBuserSignupLoginEntities();

        
        // GET: TBLSellerInfoes/Edit/5
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

        // POST: TBLSellerInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdUs,FirstName,LastName,Email,Password")] TBLSellerInfo tBLSellerInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBLSellerInfo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index","Seller");
            }
            return View(tBLSellerInfo);
        }

        
        
    }
}
