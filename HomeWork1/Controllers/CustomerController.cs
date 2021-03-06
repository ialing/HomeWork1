﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HomeWork1.Models;

namespace HomeWork1.Controllers
{
    public class CustomerController : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();

        // GET: Customer
        public ActionResult Index(string search)
        {
            var data = db.客戶資料.Include(p => p.客戶聯絡人);
            data = data.Where(c => (bool)!c.刪除註記);
            if (!string.IsNullOrEmpty(search))
            { 
                data = db.客戶資料.Where(c =>c.客戶名稱.Contains(search) );
            }

            return View(data);
        }
        public ActionResult ViewList()
        {
            var data = db.VW_CustomerInfo.ToList();
            data = data.Where(c => (bool)!c.刪除註記).ToList();

            return View(data);
        }
        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,刪除註記")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {

                db.客戶資料.Add(客戶資料);
                客戶資料.刪除註記 = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: Customer/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,刪除註記")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                db.Entry(客戶資料).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }
        [HttpPost]
        public ActionResult Update(int id)
        {
            var data = db.客戶資料.Find(id);
            db.SaveChanges();
            return View();
        }
        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            客戶資料.刪除註記 = true;

            var 客戶聯絡人 = db.客戶聯絡人.Where(p => p.客戶Id.Equals(id));
            foreach (var item in 客戶聯絡人)
           {
               item.刪除註記 = true;
            }
             var 客戶銀行資訊 = db.客戶銀行資訊.Where(p => p.客戶Id.Equals(id));
             foreach (var item in 客戶銀行資訊)
             {
                 item.刪除註記 = true;
             } 
            db.SaveChanges();
            return RedirectToAction("Index");
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
