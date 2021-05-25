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
    public class TBLUserInfoesController : Controller
    {
        private DBuserSignupLoginEntities db = new DBuserSignupLoginEntities();

        // GET: TBLUserInfoes
       
        // POST: TBLUserInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        
        // GET: TBLUserInfoes/Edit/5
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

        // POST: TBLUserInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdUs,FirstName,LastName,Email,Password")] TBLUserInfo tBLUserInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBLUserInfo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Buyer");
            }
            return View(tBLUserInfo);
        }

        
        
    }
}
