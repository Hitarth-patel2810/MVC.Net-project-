using ConnectionLibrary.Interface;
using ConnectionLibrary.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using project7thSem.Models;
using System.Diagnostics;
using PagedList;
using PagedList.Mvc;
using ConnectionLibrary.Extension;
using project7thSem.Model;
using ConnectionLibrary.Model;
using System.Net.Mail;
using System.Net;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.WebRequestMethods;

namespace project7thSem.Controllers
{
    public class HomeController : Controller
    {
        private IConnectionClass _connectionClass { get; set; }

        public HomeController(IConnectionClass connectionClass)
        {
            _connectionClass = connectionClass;
        }

        public IActionResult Index(int page = 1)
        {
                var model = new SearchResult();

                var d = _connectionClass.Select(" SELECT  OurRefNo,TenderAmount,SubmDate,WorkDesc,AgencyName,\r\n (SELECT Name fROM Country_Mast cm WHERE cm.CountryID=gi.CountryId) \r\n as Countryname FROM GlobalFreshTenderInfo gi ORDER BY TenderAmount DESC ");

                model.TenderDetails = Helper.ConvertDataTable<DataList>(d);

                const int pageSize = 10;
                if (page < 1)
                    page = 1;

                int recsCount = model.TenderDetails.Count();
                var pager = new Pager(recsCount, page, pageSize);
                int recSkip = (page - 1) * pageSize;
                model.TenderDetails = model.TenderDetails.Skip(recSkip).Take(pager.PageSize).ToList();

                this.ViewBag.Pager = pager;

                return View(model);
        }
        [Route("Home/search/{Search?}")]
        public IActionResult search(string Search,int page = 1)
        {
            var model = new SearchResult();

            var query = _connectionClass.Select($" SELECT OurRefNo,TenderAmount,SubmDate,WorkDesc,AgencyName,\r\n (SELECT Name fROM Country_Mast cm WHERE cm.CountryID=gi.CountryId) \r\n as Countryname FROM GlobalFreshTenderInfo gi  where FREETEXT (SearchText,'\"{Search}\"') ORDER BY TenderAmount DESC");

            model.TenderDetails = Helper.ConvertDataTable<DataList>(query);

            int TenderCount = model.TenderDetails.Count();
            const int pageSize = 10;
            if (page < 1)
                page = 1;

            int recsCount = model.TenderDetails.Count();
            var pager = new Pager(recsCount, page, pageSize);
            int recSkip = (page - 1) * pageSize;
            model.TenderDetails = model.TenderDetails.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            return View(model);
        }

        [Route("Notice")]
        public IActionResult Notice(int OurRefNo)
        {
            var model = new SearchResult();
            var d = _connectionClass.Select($"SELECT OurRefNo,TenderAmount,SubmDate,WorkDesc,AgencyName,Name FROM GlobalFreshTenderInfo LEFT JOIN Country_Mast ON GlobalFreshTenderInfo.CountryID=Country_Mast.CountryID Where OurRefNo = {OurRefNo}");
            
            model.tenderFields = Helper.ConvertDataTable<Tender_Filed>(d);
            return View(model);
        }

        public IActionResult otpsend(string name,string mobile,string email, string tno,string id)
        {
            Random _rdm = new Random();
            var otp = _rdm.Next(1000, 10000);
            var htmlbody = $"<style type=\'text/css\'>#outlook a {{ padding:0; }}\r\n          body {{ margin:0;padding:0;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%; }}\r\n          table, td {{ border-collapse:collapse;mso-table-lspace:0pt;mso-table-rspace:0pt; }}\r\n          img {{ border:0;height:auto;line-height:100%; outline:none;text-decoration:none;-ms-interpolation-mode:bicubic; }}\r\n          p {{ display:block;margin:13px 0; }}</style><!--[if mso]>\r\n        <noscript>\r\n        <xml>\r\n        <o:OfficeDocumentSettings>\r\n          <o:AllowPNG/>\r\n          <o:PixelsPerInch>96</o:PixelsPerInch>\r\n        </o:OfficeDocumentSettings>\r\n        </xml>\r\n        </noscript>\r\n        <![endif]--><!--[if lte mso 11]>\r\n        <style type=\'text/css\'>\r\n          .mj-outlook-group-fix {{ width:100% !important; }}\r\n        </style>\r\n        <![endif]--><!--[if !mso]><!--><link href=\'https://fonts.googleapis.com/css?family=Open+Sans:300,400,500,700\' rel=\'stylesheet\' type=\'text/css\'><style type=\'text/css\'>@import url(https://fonts.googleapis.com/css?family=Open+Sans:300,400,500,700);</style><!--<![endif]--><style type=\'text/css\'>@media only screen and (min-width:480px) {{\r\n        .mj-column-per-100 {{ width:100% !important; max-width: 100%; }}\r\n      }}</style><style media=\'screen and (min-width:480px)\'>.moz-text-html .mj-column-per-100 {{ width:100% !important; max-width: 100%; }}</style><style type=\'text/css\'>@media only screen and (max-width:480px) {{\r\n      table.mj-full-width-mobile {{ width: 100% !important; }}\r\n      td.mj-full-width-mobile {{ width: auto !important; }}\r\n    }}</style></head><body style=\'word-spacing:normal;background-color:#fafbfc;\'><div style=\'background-color:#fafbfc;\'><!--[if mso | IE]><table align=\'center\' border=\'0\' cellpadding=\'0\' cellspacing=\'0\' class=\'\' style=\'width:600px;\' width=\'600\' ><tr><td style=\'line-height:0px;font-size:0px;mso-line-height-rule:exactly;\'><![endif]--><div style=\'margin:0px auto;max-width:600px;\'><table align=\'center\' border=\'0\' cellpadding=\'0\' cellspacing=\'0\' role=\'presentation\' style=\'width:100%;\'><tbody><tr><td style=\'direction:ltr;font-size:0px;padding:20px 0;padding-bottom:20px;padding-top:20px;text-align:center;\'><!--[if mso | IE]><table role=\'presentation\' border=\'0\' cellpadding=\'0\' cellspacing=\'0\'><tr><td class=\'\' style=\'vertical-align:middle;width:600px;\' ><![endif]--><div class=\'mj-column-per-100 mj-outlook-group-fix\' style=\'font-size:0px;text-align:left;direction:ltr;display:inline-block;vertical-align:middle;width:100%;\'><table border=\'0\' cellpadding=\'0\' cellspacing=\'0\' role=\'presentation\' style=\'vertical-align:middle;\' width=\'100%\'><tbody><tr><td align=\'center\' style=\'font-size:0px;padding:25px;word-break:break-word;\'><table border=\'0\' cellpadding=\'0\' cellspacing=\'0\' role=\'presentation\' style=\'border-collapse:collapse;border-spacing:0px;\'><tbody><tr><td style=\'width:550px;\'><img height=\'auto\' src=\'https://www.tenderdetail.com/Content/img/TenderDetail.png\' style=\'border:0;display:block;outline:none;text-decoration:none;height:auto;width:100%;font-size:13px;\' width=\'550\'></td></tr></tbody></table></td></tr></tbody></table></div><!--[if mso | IE]></td></tr></table><![endif]--></td></tr></tbody></table></div><!--[if mso | IE]></td></tr></table><table align=\'center\' border=\'0\' cellpadding=\'0\' cellspacing=\'0\' class=\'\' style=\'width:600px;\' width=\'600\' bgcolor=\'#ffffff\' ><tr><td style=\'line-height:0px;font-size:0px;mso-line-height-rule:exactly;\'><![endif]--><div style=\'background:#ffffff;background-color:#ffffff;margin:0px auto;max-width:600px;\'><table align=\'center\' border=\'0\' cellpadding=\'0\' cellspacing=\'0\' role=\'presentation\' style=\'background:#ffffff;background-color:#ffffff;width:100%;\'><tbody><tr><td style=\'direction:ltr;font-size:0px;padding:20px 0;padding-bottom:20px;padding-top:20px;text-align:center;\'><!--[if mso | IE]><table role=\'presentation\' border=\'0\' cellpadding=\'0\' cellspacing=\'0\'><tr><td class=\'\' style=\'vertical-align:middle;width:600px;\' ><![endif]--><div class=\'mj-column-per-100 mj-outlook-group-fix\' style=\'font-size:0px;text-align:left;direction:ltr;display:inline-block;vertical-align:middle;width:100%;\'><table border=\'0\' cellpadding=\'0\' cellspacing=\'0\' role=\'presentation\' style=\'vertical-align:middle;\' width=\'100%\'><tbody><tr><td align=\'center\' style=\'font-size:0px;padding:10px 25px;padding-right:25px;padding-left:25px;word-break:break-word;\'><div style=\'font-family:open Sans Helvetica, Arial, sans-serif;font-size:16px;line-height:1;text-align:center;color:#000000;\'><span>Hello,</span></div></td></tr><tr><td align=\'center\' style=\'font-size:0px;padding:10px 25px;padding-right:25px;padding-left:25px;word-break:break-word;\'><div style=\'font-family:open Sans Helvetica, Arial, sans-serif;font-size:16px;line-height:1;text-align:center;color:#000000;\'>Please use the verification code below on the <b>Tender Detail</b> website:</div></td></tr><tr><td align=\'center\' style=\'font-size:0px;padding:10px 25px;word-break:break-word;\'><div style=\'font-family:open Sans Helvetica, Arial, sans-serif;font-size:24px;font-weight:bold;line-height:1;text-align:center;color:#000000;\'>{otp}</div></td></tr><tr><td align=\'center\' style=\'font-size:0px;padding:10px 25px;padding-right:16px;padding-left:25px;word-break:break-word;\'><div style=\'font-family:open Sans Helvetica, Arial, sans-serif;font-size:16px;line-height:1;text-align:center;color:#000000;\'>If you didn't request this, you can ignore this email or let us know.</div></td></tr><tr><td align=\'center\' style=\'font-size:0px;padding:10px 25px;padding-right:25px;padding-left:25px;word-break:break-word;\'><div style=\'font-family:open Sans Helvetica, Arial, sans-serif;font-size:16px;line-height:1;text-align:center;color:#000000;\'>Thanks!<br><b>Tender Detail Team</b></div></td></tr></tbody></table></div><!--[if mso | IE]></td></tr></table><![endif]--></td></tr></tbody></table></div><!--[if mso | IE]></td></tr></table><![endif]--></div></body></html>";
            var subjectline = "";
            //var emquery = (int)_connectionClass.Select($"select id from Email_Detail where Email = '{email}'").Rows.Count;
            //if (emquery == 0)
            //{
                if (mailsend(email, htmlbody, subjectline))
                {
                    //var model = new UserEmailOption();
                    var query = $"INSERT INTO Email_Detail (OurRefno, Name, Contact_Number,Email)VALUES ({tno}, '{Name}',{mobile},'{email}')select SCOPE_IDENTITY() as id";
                    var d = _connectionClass.Select(query).Rows[0][0].ToString();
                    return Json(new { Result = true, email_data = email.ToString(), otp = otp.ToString(), id = 1, userid = d });
                }
            //}
            //else
            //{
            //    return Json(new { Result = true, id = 2 });
            //}
            return Json(new { Result = true, id = 0 });
        }

        public bool mailsend(string  email,string body,string subjectline)
        {
            var tstaus = true;
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("root42155@gmail.com");
            mailMessage.To.Add(email);
            mailMessage.Subject = subjectline;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("root42155@gmail.com", "pxwgliaoyztyoala");
            smtpClient.EnableSsl = true;

            try
            {
                smtpClient.Send(mailMessage);
                 tstaus = true;
            }
            catch (Exception ex)
            {
                tstaus = false;
            }
            return tstaus;
        }

        public IActionResult Email_Verify(string email, string tno,string mobile,string UserID)
        {
            var model = new UserEmailOption();
            var query = _connectionClass.Select($"UPDATE Email_Detail SET IsVerified = 1,IsMailsent = 1 WHERE OurRefno = {tno};");
            return Json(new { Result = true, email_data = email.ToString(),id = 1, ourrefno = tno.ToString(), userid = UserID });
        }
        
        [Route("Home/FullNotice/{userid?}")]
        public IActionResult FullNotice(string userid)
        {
            var model = new SearchResult();
            var query = _connectionClass.Select($"SELECT (SELECT (SELECT name fROM Country_Mast cm WHERE cm.CountryID=gi.CountryId) )as Countryname,(SELECT (SELECT name fROM GeographyRegion_Mast gm WHERE gm.ID=gi.GeoRegID))as GeoRegion,*FROM GlobalFreshTenderInfo gi right JOIN Email_Detail udi ON gi.OurRefNo= udi.OurRefno  Where id ={userid}");
            model.tenderFields = Helper.ConvertDataTable<Tender_Filed>(query);
            var poliregion = model.tenderFields.FirstOrDefault().PoliRegID.Split(",");
            var totalpoliregion = "";
            foreach (var item in poliregion)
            {
                var Query = _connectionClass.Select($"select(select name from PoliticalRegion_Mast where ID = {item}) as PoliRegionName");
                if (totalpoliregion == "")
                {
                    totalpoliregion = Query.Rows[0][0].ToString();
                }
                else
                {
                    totalpoliregion += ','+Query.Rows[0][0].ToString();
                }
            }
            model.tenderFields = Helper.ConvertDataTable<Tender_Filed>(query);
            model.tenderFields.FirstOrDefault().PoliRegionName = totalpoliregion;

            //var CPV = model.tenderFields.FirstOrDefault().CPV.Split(",");
            //var totalcpv = "";
            //foreach (var item in CPV)
            //{
            //    var Query = _connectionClass.Select($"select(select name from PoliticalRegion_Mast where ID = {item}) as CPV_Name");
            //    if (totalcpv == "")
            //    {
            //        totalcpv = Query.Rows[0][0].ToString();
            //    }
            //    else
            //    {
            //        totalcpv += ',' + Query.Rows[0][0].ToString();
            //    }

            //}
            //model.tenderFields = Helper.ConvertDataTable<Tender_Filed>(query);
            //model.tenderFields.FirstOrDefault().CPV_Name = totalcpv;
            return View(model);
        }
        //public IActionResult Search(string Search, int pg = 1)
        //{
        //    var model = new SearchResult();
        //    var d = _connectionClass.Select($"select * from GlobalFreshTenderInfo where contains(SearchText,'\"{Search}\"')");
        //    model.searchFields = Helper.ConvertDataTable<header_modal>(d);
        //    const int pageSize = 10;
        //    if (pg < 1)
        //        pg = 1;

        //    int recsCount = model.TenderDetails.Count();
        //    var pager = new Pager(recsCount, pg, pageSize);
        //    int recSkip = (pg - 1) * pageSize;
        //    model.TenderDetails = model.TenderDetails.Skip(recSkip).Take(pager.PageSize).ToList();

        //    this.ViewBag.Pager = pager;
        //    return View(model);
        //}
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}