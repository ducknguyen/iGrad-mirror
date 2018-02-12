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

namespace IGrad.Controllers
{
    public class PDFFillerController : Controller
    {
        // GET: PDFFiller
        public ActionResult Index()
        {
            return View();
        }
        

        public ActionResult FillPdf(UserModel user)
        {
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/media/documents/iGradFirstForm.pdf");
            PdfDocument document = PdfReader.Open(filePath);
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

            int age = DateTime.Now.Year - user.Birthday.Year;
            PdfTextField formAge = (PdfTextField)(document.AcroForm.Fields["Age"]);
            formAge.Value = new PdfString(age.ToString());
            formAge.ReadOnly = true;


            byte[] fileContents = null;
            using(MemoryStream stream = new MemoryStream())
            {
                document.Save(stream);
                fileContents = stream.ToArray();
                return File(fileContents, MediaTypeNames.Application.Octet ,"FirstPage.pdf");
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