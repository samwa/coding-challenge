using Data.ViewModel;
using Logic.Services.Report;
using Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Controllers
{
	public class ReportController : Controller
	{
        private readonly ReportService service;

        public ReportController()
        {
            LGAContext context = new LGAContext();
            service = new ReportService(context);
        }

		// GET: Disadvs
		public ActionResult Index()
		{

			ViewData["States"] = service.States.Select(p => new SelectListItem()
			{
				Value = p.StateId.ToString(),
				Text = p.StateName,
				Selected = (p.StateName.Equals("Victoria", StringComparison.CurrentCultureIgnoreCase))
			})
			.ToList();

            var cached = System.Web.HttpContext.Current.Cache["ReportController:Index"] as IEnumerable<ReportModel>;
            if (cached == null)
            {
                cached = service.GetDisadges(null);
                System.Web.HttpContext.Current.Cache["ReportController:Index"] = cached;
            }
            return View(cached);
        }

		public ActionResult ReportGrid(string stateId, bool showAll = false)
		{
			int id = stateId != null ? int.Parse(stateId) : -1;

            var cached = System.Web.HttpContext.Current.Cache["ReportController:ReportGrid:"+ stateId + ":"+showAll] as IEnumerable<ReportModel>;
            if (cached == null)
            {
                cached = service.GetDisadges(id, showAll);
                System.Web.HttpContext.Current.Cache["ReportController:ReportGrid:" + stateId + ":" + showAll] = cached;
            }
            return PartialView(cached);
		}
	}
}
