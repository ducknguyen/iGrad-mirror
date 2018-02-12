﻿using IGrad.Models.User;
using PdfSharp.Pdf;
using PdfSharp.Pdf.AcroForms;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            PdfDocument document = PdfReader.Open("C:/Users/jpratt/Documents/GitHub/igrad/IGrad/IGrad/media/documents/iGradFirstForm.pdf");

            PdfTextField firstName = (PdfTextField)(document.AcroForm.Fields["StudentFirstName"]);
            firstName.Value = new PdfString(user.Name.FName);

            PdfTextField lastName = (PdfTextField)(document.AcroForm.Fields["StudentLastName"]);
            firstName.Value = new PdfString(user.Name.LName);

            PdfTextField middleInitial = (PdfTextField)(document.AcroForm.Fields["MI"]);
            middleInitial.Value = new PdfString(user.Name.MName);
            
            PdfTextField birthday = (PdfTextField)(document.AcroForm.Fields["Birthday"]);
            birthday.Value = new PdfString(user.Birthday.ToString());

            document.Save("C:/Users/jpratt/Documents/GitHub/igrad/IGrad/IGrad/media/documents/iGradComplete.pdf");

            return null;

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