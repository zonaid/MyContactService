using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyContactService.Repo.Models;
using MyContactService.Repo.Repositories;

namespace MyContactService.Controllers
{
    public class ContactsController : Controller
    {
        private ContactRepository contactDb = new ContactRepository(MyContactService.Repo.RedisCacheFactory.Database);

        // GET: Contacts
        public async Task<ActionResult> Index()
        {
            return View(await contactDb.GetAllAsync());
        }

        // GET: Contacts/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await contactDb.GetByIdAsync(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,ContactNo,Group,Photo")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                await contactDb.InsertAsync(contact.ID, contact);
                
                //contactDb.Commit();
                
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        // GET: Contacts/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await contactDb.GetByIdAsync(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,ContactNo,Group,Photo")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                
                await contactDb.UpdateAsync(contact.ID, contact);
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty( id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await contactDb.GetByIdAsync(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Contact contact = await contactDb.GetByIdAsync(id);
            if (contact != null)
            {
                await contactDb.DeleteAsync(contact.ID);
                await contactDb.CommitAsync();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                contactDb.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
