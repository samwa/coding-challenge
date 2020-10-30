using Logic.Services.Report;
using Repository;
using System;
using System.Data;
using System.Linq;
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
			return View(service.GetDisadges(null));
		}

		public ActionResult ReportGrid(string stateId, bool showAll = false)
		{
			int id = stateId != null ? int.Parse(stateId) : -1;
			return PartialView(service.GetDisadges(id, showAll));
		}
	}
}
