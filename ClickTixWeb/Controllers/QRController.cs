using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using ZXing;
using ZXing.QrCode;

namespace ClickTixWeb.Controllers
{
    public class QRController : Controller
    {


        public IActionResult ShowQRCode(string data)
        {
            var qrCodeWriter = new ZXing.BarcodeWriterPixelData
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    DisableECI = true,
                    CharacterSet = "UTF-8",
                    Width = 150,
                    Height = 150
                }
            };

            var pixelData = qrCodeWriter.Write(data);

            using (MemoryStream ms = new MemoryStream())
            {
                var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

                try
                {
                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }

                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                return File(ms.ToArray(), "image/png");
            }
        }
        // GET: QRController
        public ActionResult Index()
        {
            return View();
        }

        // GET: QRController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: QRController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QRController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QRController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QRController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QRController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QRController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
