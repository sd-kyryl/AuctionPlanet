using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using AuctionPlanet.BusinessLogic.DataTransferObjects;
using AuctionPlanet.BusinessLogic.Interfaces;
using AuctionPlanet.BusinessLogic.Repositories;
using AuctionPlanet.BusinessLogic.Services;
using AuctionPlanet.DataAccess.Utility;
using AuctionPlanet.WebPresentation.Models;
using static AutoMapper.Mapper;

namespace AuctionPlanet.WebPresentation.Controllers
{
    public class LotViewModelController : Controller
    {
        private readonly ILotService _lotService;

        public LotViewModelController()
        {
            _lotService = new LotService(new DatabaseUnitOfWork());
        }

        // GET: LotViewModel
        public ActionResult Index()
        {
            return View(Map<IEnumerable<LotViewModel>>(_lotService.GetAvailableLots()));
        }

        // GET: LotViewModel/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            LotViewModel lotViewModel = Map<LotViewModel>(_lotService.GetLotInfo(id.Value));

            if (lotViewModel == null)
            {
                return HttpNotFound();
            }
            return View(lotViewModel);
        }

        // GET: LotViewModel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LotViewModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,ImageUrl,StartTime,Duration,StartPrice,CurrentPrice,OriginalOwner,CurrentBidder,Status")] LotViewModel lotViewModel)
        {
            if (ModelState.IsValid)
            {
                lotViewModel.Id = Guid.NewGuid();
                lotViewModel.CurrentPrice = lotViewModel.StartPrice;
                lotViewModel.OriginalOwner = User.Identity.Name;
                lotViewModel.Status = LotStatus.PendingApproval;
                _lotService.CreateLot(Map<LotInfo>(lotViewModel));
                //_db.LotViewModels.Add(lotViewModel);
                //_db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lotViewModel);
        }

        // GET: LotViewModel/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LotViewModel lotViewModel = Map<LotViewModel>(_lotService.GetLotInfo(id.Value));
            if (lotViewModel == null)
            {
                return HttpNotFound();
            }
            return View(lotViewModel);
        }

        // POST: LotViewModel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,ImageUrl,StartTime,Duration,StartPrice,CurrentPrice,OriginalOwner,CurrentBidder,Status")] LotViewModel lotViewModel)
        {
            if (ModelState.IsValid)
            {
                
                //_db.Entry(lotViewModel).State = EntityState.Modified;
                //_db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lotViewModel);
        }

        // GET: LotViewModel/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LotViewModel lotViewModel = Map<LotViewModel>(_lotService.GetLotInfo(id.Value));
            if (lotViewModel == null)
            {
                return HttpNotFound();
            }
            return View(lotViewModel);
        }

        // POST: LotViewModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            //LotViewModel lotViewModel = _db.LotViewModels.Find(id);
            //_db.LotViewModels.Remove(lotViewModel);
            //_db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _lotService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
