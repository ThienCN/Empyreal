using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Empyreal.Hubs;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using Empyreal.ServiceLocators;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OfficeOpenXml;

namespace Empyreal.Controllers.Manager
{
    public class StatisticalController : Controller
    {
        private readonly IStatisticalService statisticalService;
        private readonly IProductService productService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private static List<Statistical> statistical;
        private static List<Product> statisticalBestSeller;
        private readonly IHubContext<ChatHub> _hubContext;

        public StatisticalController(IHostingEnvironment hostingEnvironment, IHubContext<ChatHub> hubContext)
        {
            statisticalService = ServiceLocator.Current.GetInstance<IStatisticalService>();
            productService = ServiceLocator.Current.GetInstance<IProductService>();
            _hostingEnvironment = hostingEnvironment;
            _hubContext = hubContext;
        }    

        public IActionResult StatisticalManager()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetAll(string type)
        {
            string query = "Statistical @Type";

            statistical = statisticalService.GetAll(query,
                                    new SqlParameter("@Type", "month"));
            if(statistical.Count > 0)
            {
                return Json(new { isSuccess = true, statistical = statistical });
            }
            return Json(new { isSuccess = false, message = "Cant get statistical" });
        }

        [HttpPost]
        public IActionResult StatisticalBestSeller(string type)
        {
            string query = "StatisticalBestSeller";

            statisticalBestSeller = productService.StatisticalBestSeller(query);
            if (statisticalBestSeller.Count > 0)
            {
                return Json(new { isSuccess = true, statisticalBestSeller = statisticalBestSeller });
            }
            return Json(new { isSuccess = false, message = "Cant get statisticalBestSeller" });
        }

        [HttpPost]
        public void ExportToExcel()
        {
            //string rootFolder = _hostingEnvironment.WebRootPath;
            //var fileName = @"ThongKe.xlsx";

            //FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

            try
            {
                using (ExcelPackage package = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Thống kê"); // Create excel work sheet
                    int totalRows = statistical.Count(); //
                    string day = DateTime.Now.Day.ToString("d2"); // Get current day format dd
                    string month = DateTime.Now.Month.ToString("d2"); // Get current month format MM
                    string year = DateTime.Now.Year.ToString(); // Get current year

                    worksheet.Column(1).Width = 36;
                    for (int c = 2; c <= worksheet.Cells.Columns; c++)
                    {
                        worksheet.Column(c).Width = 12;
                    }
                    for (int r = 1; r <= 11; r++)
                    {
                        worksheet.Row(r).Height = 20;
                    }
                    worksheet.Cells["A:M"].Style.Font.Size = 12;
                    worksheet.Cells[1, 1, 1, 13].Merge = true;
                    worksheet.Cells[1, 1].Value = string.Format("Thống kê hàng tháng (năm {0})", year);
                    worksheet.Cells[3, 1].Value = "Doanh thu từng tháng (VND)";
                    worksheet.Cells[4, 1].Value = "Đơn đặt hàng từng tháng (đơn hàng)";
                    worksheet.Cells[5, 1].Value = "Người dùng mới hàng tháng (người)";
                    worksheet.Cells[1, 1].Style.Font.Bold = true;
                    worksheet.Cells[3, 1].Style.Font.Bold = true;
                    worksheet.Cells[4, 1].Style.Font.Bold = true;
                    worksheet.Cells[5, 1].Style.Font.Bold = true;

                    int i = 0;
                    for (int cell = 2; cell <= totalRows + 1; cell++)
                    {
                        worksheet.Cells[2, cell].Value = "Tháng " + statistical[i].Month;
                        worksheet.Cells[2, cell].Style.Font.Bold = true;
                        worksheet.Cells[3, cell].Value = statistical[i].MonthlyRevenue;
                        worksheet.Cells[4, cell].Value = statistical[i].NumberOfOrders;
                        worksheet.Cells[5, cell].Value = statistical[i].MonthlyNewUser;
                        i++;
                    }

                    worksheet.Cells[8, 4].Value = string.Format("Thống kê trong ngày {0}/{1}/{2}", day, month, year);
                    worksheet.Cells["D8:G8"].Merge = true;
                    worksheet.Cells["D8:G8"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Cells[8, 4].Style.Font.Bold = true;
                    worksheet.Cells["D9:F9"].Merge = true;
                    worksheet.Cells["D10:F10"].Merge = true;
                    worksheet.Cells["D11:F11"].Merge = true;
                    worksheet.Cells[9, 4].Value = "Doanh thu trong ngày (VND)";
                    worksheet.Cells[9, 4].Style.Font.Bold = true;
                    worksheet.Cells[9, 7].Value = statistical[0].Revenue;
                    worksheet.Cells[10, 4].Value = "Đơn đặt hàng hôm nay (đơn hàng)";
                    worksheet.Cells[10, 4].Style.Font.Bold = true;
                    worksheet.Cells[10, 7].Value = statistical[0].Orders;
                    worksheet.Cells[11, 4].Value = "Người dùng mới hôm nay (người)";
                    worksheet.Cells[11, 4].Style.Font.Bold = true;
                    worksheet.Cells[11, 7].Value = statistical[0].NewUser;

                    var range = worksheet.Cells["A1:M5"];
                    {
                        range.Style.WrapText = true;
                        range.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        range.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        range.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        range.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        range.Style.Numberformat.Format = "#,##0";
                    }

                    worksheet.Cells["A1:M2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    range = worksheet.Cells["D8:G11"];
                    {
                        range.Style.WrapText = true;
                        range.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        range.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        range.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        range.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        range.Style.Numberformat.Format = "#,##0";
                    }

                    //package.Save();

                    var fileBytes = package.GetAsByteArray();
                    this.Response.Headers.Add("Content-Disposition",
                        String.Format("attachment; filename={0}", "ThongKe.xlsx"));
                    this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    this.Response.Body.Write(fileBytes);

                    //var memoryStream = new MemoryStream();
                    //this.Response.Headers.Add("Content-Disposition",
                    //    String.Format("attachment; filename=\"{0}\"; creation-date={1}", "ThongKe.xlsx", DateTime.Now.ToString("R")));
                    //this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    //package.SaveAs(memoryStream);
                    //memoryStream.WriteTo(Response.Body);

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }            
        }

        public IActionResult TestSignalR()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TestSignalR(string message = null)
        {
            //var productDetails = new List<ProductDetail>();
            //ProductDetail productDetail;
            //for (int i = 1; i < 3; i++)
            //{
            //    productDetail = new ProductDetail()
            //    {
            //        Id = 11 + i,
            //        Quantity = 0
            //    };
            //    productDetails.Add(productDetail);
            //}

            //message = "Hello";
            //await _hubContext.Clients.All.SendAsync("ReloadQuantity", productDetails);

            // Call SignalR notification
            await _hubContext.Clients.All.SendAsync("OrderSuccess");

            return View();
        }
    }
}