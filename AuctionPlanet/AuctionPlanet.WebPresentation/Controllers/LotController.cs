using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AuctionPlanet.BusinessLogic.DataTransferObjects;
using AuctionPlanet.BusinessLogic.Exceptions;
using AuctionPlanet.BusinessLogic.Interfaces;
using AuctionPlanet.BusinessLogic.Repositories;
using AuctionPlanet.BusinessLogic.Services;
using AuctionPlanet.DataAccess.Entities;
using AuctionPlanet.DataAccess.Utility;
using AuctionPlanet.WebPresentation.Models;
using static AutoMapper.Mapper;

namespace AuctionPlanet.WebPresentation.Controllers
{
    [Authorize(Roles = "user")]
    public class LotController : Controller
    {
        private readonly ILotService _lotService;

        /*public static List<string> ExtensionList = new List<string>
        {
            "jpg",
            "jpeg",
            "gif",
            "png",
            "bmp",
            "tif",
            "tiff"
        };*/

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

        public ActionResult CreatedLots(string userName)
        {
            return View("Index", Map<IEnumerable<LotViewModel>>(_lotService.GetCreatedLots(userName)));
        }

        public ActionResult CurrentlyHeldLots(string userName)
        {
            _lotService.DisposeOfExpiredLots();
            return View("Index", Map<IEnumerable<LotViewModel>>(_lotService.GetCurrentlyHeldLots(userName)));
        }

        public ActionResult BoughtLots(string userName)
        {
            _lotService.DisposeOfExpiredLots();
            return View("Index", Map<IEnumerable<LotViewModel>>(_lotService.GetBoughtLots(userName)));
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
        public ActionResult Create([Bind(Include = "Id,Title,Description,StartTime,Duration,StartPrice,CurrentPrice,OriginalOwner,CurrentBidder,Status,LotImage")] LotViewModel lotViewModel, HttpPostedFileBase upload)
        {
            bool isValid = true;

            if (upload != null && !upload.ContentType.StartsWith("image"))
            {
                ModelState.AddModelError("", "Non-image file was chosen");
                isValid = false;
            }

            if (lotViewModel.StartPrice <= decimal.Zero)
            {
                ModelState.AddModelError("", "The price must be positive");
                isValid = false;
            }

            if (lotViewModel.Duration.Ticks < 0L)
            {
                ModelState.AddModelError("", "Duration must be positive");
                isValid = false;
            }

            if (!isValid)
            {
                return View(lotViewModel);
            }
                
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    byte[] content;

                    using (var reader = new BinaryReader(upload.InputStream))
                    {
                        content = reader.ReadBytes(upload.ContentLength);
                    }

                    LotImage lotImage = new LotImage
                    {
                        Id = Guid.NewGuid(),
                        ImageType = upload.ContentType,
                        ImageData = content
                    };

                    lotViewModel.Image = lotImage;
                }
                lotViewModel.Id = Guid.NewGuid();
                lotViewModel.CurrentPrice = lotViewModel.StartPrice;
                lotViewModel.OriginalOwner = User.Identity.Name;
                lotViewModel.Status = LotStatus.PendingApproval;
                _lotService.CreateLot(Map<LotInfo>(lotViewModel));
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
        public ActionResult Edit(LotViewModel lotViewModel)
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

            _lotService.DeleteLot(id);
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

        public ActionResult Renew(Guid? id)
        {
            if (!User.IsInRole("admin")) return View("UnauthorizedAccess");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                _lotService.RenewTheLot(id.Value);
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

            _lotService.DisposeOfExpiredLots();
            LotInfo lotInfo = _lotService.GetLotInfo(id.Value);

            if (lotInfo.Status != LotStatus.Available)
            {
                return RedirectToAction("Details", new {id = id});
            }

            BidViewModel bidViewModel = new BidViewModel
            {
                LotId = id.Value,
                NewBidder = User.Identity.Name,
                LotTitle = lotInfo.Title,
                CurrentBidder = lotInfo.CurrentBidder,
                CurrentPrice = lotInfo.CurrentPrice
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
                    return RedirectToAction("Index");
                }
                catch (UnavailableServiceActionException exception)
                {
                    ViewBag.FailureMessage = exception.Message;
                }
                catch (UnacceptablePriceException exception)
                {
                    ModelState.AddModelError("", exception.Message);
                }
            }

            return View(bidViewModel);
        }

        public ActionResult Search(string searchCriteria)
        {
            _lotService.DisposeOfExpiredLots();
            return View("Index", Map<IEnumerable<LotViewModel>>(_lotService.SearchLotInfos(searchCriteria)));
        }

        public ActionResult File(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var lot = _lotService.GetLotInfo(id.Value);

            if (lot == null) return HttpNotFound();

            if (lot.Image == null) return File(new byte[0], "image\\jpeg");

            return File(lot.Image.ImageData, lot.Image.ImageType);
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
