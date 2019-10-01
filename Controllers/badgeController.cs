using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QRCoder;

namespace ConfWebAccess.Controllers
{
    [Authorize]
    public class badgeController : BaseController
    {
        // GET: badge
    
        public ActionResult Index()
        {
            if (CurrentUser != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    string qrcodestr = CurrentUser.Id + ";" + CurrentUser.FirstName + " " + CurrentUser.LastName + ";" + CurrentUser.Email;
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrcodestr, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);
                    using (Bitmap bitMap = qrCode.GetGraphic(20))
                    {
                        bitMap.Save(ms, ImageFormat.Png);
                        ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                    }
                }
            }

            return View(CurrentUser);
        }
    }
}