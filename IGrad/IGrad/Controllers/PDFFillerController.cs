using IGrad.Context;
using IGrad.Models.User;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.AcroForms;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace IGrad.Controllers
{
    public class PDFFillerController : Controller
    {
        // GET: PDFFiller
        public ActionResult Index()
        {
            return View();
        }

        private UserModel GetUserData(Guid userId)
        {
            using (UserContext db = new UserContext())
            {
                var data = db.Users
                       .Include(u => u.Name)
                       .Include(u => u.BirthPlace)
                       .Include(u => u.ConsideredRaceAndEthnicity)
                       .Include(u => u.PhoneInfo)
                       .Where(u => u.UserID == userId)
                       .FirstOrDefault<UserModel>();
                PDFFillerController pdfControl = new PDFFillerController();
                return data;
            }
        }

        public ActionResult FillPdf(Guid userId)
        {
            // Get the user data to use in the PDF
            UserModel user = GetUserData(userId);

            // Get the blank form to fill out
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/media/documents/iGradFirstForm.pdf");
            PdfDocument document = PdfReader.Open(filePath);

            // Set the flag so we can flatten once done.
            document.AcroForm.Elements.SetBoolean("/NeedAppearances", true);

            PdfTextField firstName = (PdfTextField)(document.AcroForm.Fields["StudentFirstName"]);
            firstName.Value = new PdfString(user.Name.FName);
            firstName.ReadOnly = true;
            PdfTextField lastName = (PdfTextField)(document.AcroForm.Fields["StudentLastName"]);
            lastName.Value = new PdfString(user.Name.LName);
            lastName.ReadOnly = true;
            PdfTextField middleInitial = (PdfTextField)(document.AcroForm.Fields["MI"]);
            middleInitial.Value = new PdfString(user.Name.MName);
            middleInitial.ReadOnly = true;
            PdfTextField birthday = (PdfTextField)(document.AcroForm.Fields["Birthday"]);
            birthday.Value = new PdfString(user.Birthday.ToString("MM-dd-yyyy"));
            birthday.ReadOnly = true;
            PdfTextField gender = (PdfTextField)(document.AcroForm.Fields["Gender"]);
            gender.Value = new PdfString(user.Gender);
            gender.ReadOnly = true;

            int age = DateTime.Now.Year - user.Birthday.Year;
            PdfTextField formAge = (PdfTextField)(document.AcroForm.Fields["Age"]);
            formAge.Value = new PdfString(age.ToString());
            formAge.ReadOnly = true;

            document.SecuritySettings.PermitFormsFill = false;
            document.SecuritySettings.PermitModifyDocument = false;
            document.SecuritySettings.PermitFullQualityPrint = true;
            document.SecuritySettings.PermitPrint = true;

            byte[] fileContents = null;
            using (MemoryStream stream = new MemoryStream())
            {
                document.Save(stream);
                fileContents = stream.ToArray();
                return File(fileContents, MediaTypeNames.Application.Octet, "FirstPage.pdf");
            }


            //MemoryStream stream = new MemoryStream();
            //document.Save(stream, false);
            //Response.Clear();
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-length", stream.Length.ToString());
            //Response.BinaryWrite(stream.ToArray());
            //Response.Flush();
            //stream.Close();
            //Response.End();

        }
    }
}