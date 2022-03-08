using LaundryV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Data.Entity;
using System.Collections;

namespace LaundryV3.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var invoices = db.Invoices.Include(i => i.Customer);
            var xs = new ArrayList();
            var ys = new ArrayList();
            foreach (var item in invoices)
            {
                xs.Add(item.Price/10000);
                ys.Add(item.Customer.Name);
            }
            var chartCustomer = new Chart(width: 400, height: 300)
                    .AddTitle("Invoice by customer")
                    .AddSeries(
                    name: "Customer",
                    xValue: ys,
                    yValues: xs);
            chartCustomer.Save(path: "~/Content/img/chartCustomer.jpg");

            xs = new ArrayList();
            ys = new ArrayList();
            foreach (var item in invoices)
            {
                xs.Add(item.Price/10000);
                ys.Add(item.CreateAt.ToShortDateString());
            }
            var myChart = new Chart(width: 400, height: 300)
                    .AddTitle("Invoice by Date")
                    .AddSeries(
                    name: "Date",
                    chartType: "Line",
                    xValue: ys,
                    yValues: xs);
            myChart.Save(path: "~/Content/img/chartDate.jpg");


            xs = new ArrayList();
            ys = new ArrayList();
            var serviceLaundry = db.ServiceLaundries;
            foreach (var item in serviceLaundry)
            {
                double total = 0;
                ys.Add(item.Name);
                foreach (var inv in invoices)
                {
                    foreach (var invIt in inv.ListItems)
                    {
                        if (invIt.ServiceDetail.ServiceLaundryId == item.Id)
                        {
                            total += invIt.Price;
                        }
                    }
                }
                xs.Add(total);
            }


            var pie = new Chart(width: 400, height: 300)
                .AddTitle("Invoice by Service")
                .AddSeries(
                name: "Date",
                chartType: "Pie",
                xValue: ys,
                yValues: xs);
            pie.Save(path: "~/Content/img/chartPie.jpg");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}