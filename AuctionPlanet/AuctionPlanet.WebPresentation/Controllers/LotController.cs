using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using AuctionPlanet.BusinessLogic.DataTransferObjects;
using AuctionPlanet.BusinessLogic.Exceptions;
using AuctionPlanet.BusinessLogic.Interfaces;
using AuctionPlanet.BusinessLogic.Repositories;
using AuctionPlanet.BusinessLogic.Services;
using AuctionPlanet.DataAccess.Utility;
using AuctionPlanet.WebPresentation.Models;
using static AutoMapper.Mapper;

namespace AuctionPlanet.WebPresentation.Controllers
{
    [Authorize(Roles = "user")]
    public class LotController : Controller
    {
        private readonly ILotService _lotService;

        public LotController()
        {
            _lotService = new LotService(new DatabaseUnitOfWork());
        }

        // GET: LotViewModel
        public ActionResult Index()
        {
            _lotService.DisposeOfExpiredLots();
            return View(Map<IEnumerable<LotViewModel>>(_lotService.GetAvailableLots()));
        }

        public ActionResult PendingLots()
        {
            if (!User.IsInRole("admin")) return View("UnauthorizedAccess");

            return View(Map<IEnumerable<LotViewModel>>(_lotService.GetPendingLots()));
        }

        public ActionResult CurrentlyHeldLots()
        {
            _lotService.DisposeOfExpiredLots();
            return View("Index", Map<IEnumerable<LotViewModel>>(_lotService.GetCurrentlyHeldLots(User.Identity.Name)));
        }

        public ActionResult BoughtLots()
        {
            _lotService.DisposeOfExpiredLots();
            return View("Index", Map<IEnumerable<LotViewModel>>(_lotService.GetBoughtLots(User.Identity.Name)));
        }

        public ActionResult SoldLots()
        {
            if (!User.IsInRole("admin")) return View("UnauthorizedAccess");
            _lotService.DisposeOfExpiredLots();
            return View("Index", Map<IEnumerable<LotViewModel>>(_lotService.GetSoldLots()));
        }

        public ActionResult ExpiredLots()
        {
            if (!User.IsInRole("admin")) return View("UnauthorizedAccess");
            _lotService.DisposeOfExpiredLots();
            return View("Index", Map<IEnumerable<LotViewModel>>(_lotService.GetExpiredLots()));
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

        // GET: LotViewModel/
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
            if (!User.IsInRole("admin")) return View("UnauthorizedAccess");

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
            if (!User.IsInRole("admin")) return View("UnauthorizedAccess");

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
            if (!User.IsInRole("admin")) return View("UnauthorizedAccess");

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
            if (!User.IsInRole("admin")) return View("UnauthorizedAccess");

            //LotViewModel lotViewModel = _db.LotViewModels.Find(id);
            //_db.LotViewModels.Remove(lotViewModel);
            //_db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Approve(Guid? id)
        {
            if (!User.IsInRole("admin")) return View("UnauthorizedAccess");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                _lotService.ApproveALot(id.Value);
            }
            catch (UnavailableServiceActionException exception)
            {
                ViewBag.FailureMessage = exception.Message;
                return View("UnavailableOperation");
            }

            return RedirectToAction("Index");
        }

        public ActionResult Bid(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BidViewModel bidViewModel = new BidViewModel
            {
                LotId = id.Value,
                NewBidder = User.Identity.Name,
                LotTitle = _lotService.GetLotInfo(id.Value).Title
            };

            return View("SpecifyBid", bidViewModel);
        }
        
        [HttpPost]
        public ActionResult SpecifyBid(BidViewModel bidViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _lotService.BidOnALot(bidViewModel.LotId, bidViewModel.NewPrice, bidViewModel.NewBidder);
                }
                catch (UnavailableServiceActionException exception)
                {
                    ViewBag.FailureMessage = exception.Message;
                    return View(bidViewModel);
                }
                
                return RedirectToAction("Index");
            }

            return View(bidViewModel);
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
