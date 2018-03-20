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
using System.IO.Compression;
using Ionic.Zip;

namespace IGrad.Controllers
{
    public class PDFFillerController : Controller
    {
        private UserModel user;

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
                       .Include(u => u.Guardians.Select(n => n.Name))
                       .Include(u => u.Siblings)
                       .Include(u => u.LanguageHisory)
                       .Include(u => u.LifeEvent)
                       .Include(u => u.SchoolInfo)
                       .Where(u => u.UserID == userId)
                       .FirstOrDefault<UserModel>();
                PDFFillerController pdfControl = new PDFFillerController();
                return data;
            }
        }

        public ActionResult FillPdf(Guid userId)
        {
            // Get the user data to use in the PDF
            this.user = GetUserData(userId);

            using (ZipFile zip = new ZipFile())
            {
                zip.AddEntry("FirstForm.pdf", firstForm());
                zip.AddEntry("HomeLanguageFile.pdf", GetLanguageHistoryPDF());
                zip.AddEntry("ParentQuestionaire.pdf", ParentQuestionareForm());

                MemoryStream output = new MemoryStream();
                output.Position = 0;
                zip.Save(output);
                output.Position = 0;
                return File(output, "application/zip", "StudentPacket.zip");
            }
        }


        private Byte[] GetLanguageHistoryPDF()
        {
            // Get the blank form to fill out
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/media/documents/HomeLanguageSurveyApp.pdf");
            PdfDocument document = PdfReader.Open(filePath);

            // Set the flag so we can flatten once done.
            document.AcroForm.Elements.SetBoolean("/NeedAppearances", true);

            PdfTextField studentName = (PdfTextField)(document.AcroForm.Fields["StudentName"]);
            studentName.Value = new PdfString(this.user.Name.FName + " " + this.user.Name.MName + " " + this.user.Name.LName);
            studentName.ReadOnly = true;

            PdfTextField studentGrade = (PdfTextField)(document.AcroForm.Fields["Grade"]);
            studentGrade.Value = new PdfString(Convert.ToString(this.user.SchoolInfo.CurrentGrade));
            studentGrade.ReadOnly = true;

            PdfTextField todayDate = (PdfTextField)(document.AcroForm.Fields["Date"]);
            todayDate.Value = new PdfString(DateTime.Now.ToString("yyyy-MM-dd"));
            todayDate.ReadOnly = true;

            if (this.user.Guardians.Count != 0)
            {
                PdfTextField parentName = (PdfTextField)(document.AcroForm.Fields["ParentGuardianName"]);
                parentName.Value = new PdfString(this.user.Guardians[0].Name.FName + " " + this.user.Guardians[0].Name.LName);
                parentName.ReadOnly = true;

                PdfSignatureField parentSignature = (PdfSignatureField)(document.AcroForm.Fields["ParentGuardian Signature"]);
                parentSignature.Value = new PdfString(this.user.Guardians[0].Name.FName + " " + this.user.Guardians[0].Name.LName);
                parentSignature.ReadOnly = true;
            }
            #region LanguageInfo
            if (this.user.LanguageHisory != null)
            {
                PdfTextField prefSchoolLanguage = (PdfTextField)(document.AcroForm.Fields["preferedSchoolLanguage"]);
                prefSchoolLanguage.Value = new PdfString(this.user.LanguageHisory.PreferredLanguage);
                prefSchoolLanguage.ReadOnly = true;

                PdfTextField firstLearnedLanguage = (PdfTextField)(document.AcroForm.Fields["firstLanguage"]);
                firstLearnedLanguage.Value = new PdfString(this.user.LanguageHisory.UserFirstLanguageLearned);
                firstLearnedLanguage.ReadOnly = true;

                PdfTextField studenthomeLanguage = (PdfTextField)(document.AcroForm.Fields["homeLanguage"]);
                studenthomeLanguage.Value = new PdfString(this.user.LanguageHisory.StudentPrimaryLanguageAtHome);
                studenthomeLanguage.ReadOnly = true;

                PdfTextField homeLanguage = (PdfTextField)(document.AcroForm.Fields["childsPrimaryLanguage"]);
                homeLanguage.Value = new PdfString(this.user.LanguageHisory.PrimaryLanguageSpokenAtHome);
                homeLanguage.ReadOnly = true;


                // English Support
                PdfCheckBoxField hadSupport = (PdfCheckBoxField)(document.AcroForm.Fields["hadEnglishSupport"]);
                PdfCheckBoxField noSupport = (PdfCheckBoxField)(document.AcroForm.Fields["noEnglishSupport"]);
                PdfCheckBoxField dunno = (PdfCheckBoxField)(document.AcroForm.Fields["noIdeaOfEnglishSupport"]);

                if (this.user.LanguageHisory.StudentReceievedEnglishDevelopmentSupport)
                {
                    hadSupport.Checked = true;
                    hadSupport.ReadOnly = true;
                }
                else if (!this.user.LanguageHisory.StudentReceievedEnglishDevelopmentSupport)
                {
                    noSupport.Checked = true;
                    noSupport.ReadOnly = true;
                }

                else if (this.user.LanguageHisory.unsureOfEnglishSupport)
                {
                    dunno.Checked = true;
                    dunno.ReadOnly = true;
                }
            }
            #endregion

            document.SecuritySettings.PermitFormsFill = false;
            document.SecuritySettings.PermitModifyDocument = false;
            document.SecuritySettings.PermitFullQualityPrint = true;
            document.SecuritySettings.PermitPrint = true;

            byte[] fileContents = null;
            //MemoryStream stream = new MemoryStream();
            using (MemoryStream stream = new MemoryStream())
            {
                document.Save(stream);
                fileContents = stream.ToArray();
                return fileContents;
            }
        }

        private Byte[] firstForm()
        {
            
            // Get the blank form to fill out
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/media/documents/StudentEnrollmentChecklistApp.pdf");
            PdfDocument document = PdfReader.Open(filePath);

            // Set the flag so we can flatten once done.
            document.AcroForm.Elements.SetBoolean("/NeedAppearances", true);

            PdfTextField firstName = (PdfTextField)(document.AcroForm.Fields["StudentFirstName"]);
            firstName.Value = new PdfString(this.user.Name.FName);
            firstName.ReadOnly = true;
            PdfTextField lastName = (PdfTextField)(document.AcroForm.Fields["StudentLastName"]);
            lastName.Value = new PdfString(this.user.Name.LName);
            lastName.ReadOnly = true;
            PdfTextField middleInitial = (PdfTextField)(document.AcroForm.Fields["MI"]);
            middleInitial.Value = new PdfString(this.user.Name.MName);
            middleInitial.ReadOnly = true;
            PdfTextField birthday = (PdfTextField)(document.AcroForm.Fields["Birthday"]);
            birthday.Value = new PdfString(this.user.Birthday.ToString("MM-dd-yyyy"));
            birthday.ReadOnly = true;
            PdfTextField gender = (PdfTextField)(document.AcroForm.Fields["Gender"]);
            gender.Value = new PdfString(user.Gender);
            gender.ReadOnly = true;

            int age = DateTime.Now.Year - user.Birthday.Year;
            if (DateTime.Now < user.Birthday.AddYears(age))
            {
                age--;
            }
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
                return fileContents;
                //return File(fileContents, MediaTypeNames.Application.Octet, "FirstPage.pdf");
            }
        }

        private Byte[] ParentQuestionareForm()
        {
            // Get the blank form to fill out
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/media/documents/ParentQuestionnaireApp.pdf");
            PdfDocument document = PdfReader.Open(filePath);

            // Set the flag so we can flatten once done.
            document.AcroForm.Elements.SetBoolean("/NeedAppearances", true);

            // NAME INFO
            PdfTextField firstName = (PdfTextField)(document.AcroForm.Fields["FirstName"]);
            firstName.Value = new PdfString(this.user.Name.FName);
            firstName.ReadOnly = true;
            PdfTextField lastName = (PdfTextField)(document.AcroForm.Fields["LastName"]);
            lastName.Value = new PdfString(this.user.Name.LName);
            lastName.ReadOnly = true;
            PdfTextField middleInitial = (PdfTextField)(document.AcroForm.Fields["MiddleName"]);
            middleInitial.Value = new PdfString(this.user.Name.MName);
            middleInitial.ReadOnly = true;
            PdfTextField preferedName = (PdfTextField)(document.AcroForm.Fields["PreferredName"]);
            preferedName.Value = new PdfString(this.user.Name.NickName);
            preferedName.ReadOnly = true;
            // Birthday
            if (this.user.Birthday != null)
            {
                PdfTextField birthday = (PdfTextField)(document.AcroForm.Fields["BirthDate"]);
                birthday.Value = new PdfString(this.user.Birthday.ToString("MM-dd-yyyy"));
                birthday.ReadOnly = true;
            }

            // Show guardian list of names
            string _tempGuardList = "";
            foreach (Guardian guardian in this.user.Guardians)
            {
                _tempGuardList += guardian.Name.FName + " " + guardian.Name.LName + ";";
            }

            PdfTextField parentGuardians = (PdfTextField)(document.AcroForm.Fields["ParentGuardians"]);
            parentGuardians.Value = new PdfString(_tempGuardList);
            parentGuardians.ReadOnly = true;

            // Address
            if (this.user.ResidentAddress != null)
            {
                PdfTextField address = (PdfTextField)(document.AcroForm.Fields["Address"]);
                address.Value = new PdfString(this.user.ResidentAddress.PrintAddress());
                address.ReadOnly = true;
            }

            // Lives With
            PdfTextField livesWith = (PdfTextField)(document.AcroForm.Fields["LivesWith"]);
            livesWith.Value = new PdfString(_tempGuardList);
            livesWith.ReadOnly = true;

            #region SiblingInfo
            // Sibling Info:
            PdfTextField Sibling1Name = (PdfTextField)(document.AcroForm.Fields["Sibling1Name"]);
            PdfTextField Sibling2Name = (PdfTextField)(document.AcroForm.Fields["Sibling2Name"]);
            PdfTextField Sibling3Name = (PdfTextField)(document.AcroForm.Fields["Sibling3Name"]);
            PdfTextField Sibling1Age = (PdfTextField)(document.AcroForm.Fields["Sibling1Age"]);
            PdfTextField Sibling2Age = (PdfTextField)(document.AcroForm.Fields["Sibling2Age"]);
            PdfTextField Sibling3Age = (PdfTextField)(document.AcroForm.Fields["Sibling3Age"]);
            PdfTextField SiblingGrade1 = (PdfTextField)(document.AcroForm.Fields["SiblingGrade1"]);
            PdfTextField SiblingGrade2 = (PdfTextField)(document.AcroForm.Fields["SiblingGrade2"]);
            PdfTextField SiblingGrade3 = (PdfTextField)(document.AcroForm.Fields["SiblingGrade3"]);

            if(this.user.Siblings.Count >= 1)
            {
                Sibling1Name.Value = new PdfString(this.user.Siblings[0].FName + " " + this.user.Siblings[0].LName);
                Sibling1Age.Value = new PdfString(this.user.Siblings[0].Age.ToString());
                SiblingGrade1.Value = new PdfString(this.user.Siblings[0].Grade);
            }
            if(this.user.Siblings.Count >= 2)
            {
                Sibling2Name.Value = new PdfString(this.user.Siblings[1].FName + " " + this.user.Siblings[1].LName);
                Sibling2Age.Value = new PdfString(this.user.Siblings[1].Age.ToString());
                SiblingGrade2.Value = new PdfString(this.user.Siblings[1].Grade);
            }

            if(this.user.Siblings.Count >= 3)
            {
                Sibling3Name.Value = new PdfString(this.user.Siblings[2].FName + " " + this.user.Siblings[2].LName);
                Sibling3Age.Value = new PdfString(this.user.Siblings[2].Age.ToString());
                SiblingGrade3.Value = new PdfString(this.user.Siblings[2].Grade);
            }
            #endregion

            // Language At Home
            if (this.user.LanguageHisory != null)
            {
                PdfTextField language = (PdfTextField)(document.AcroForm.Fields["LanguageAtHome"]);
                language.Value = new PdfString(this.user.LanguageHisory.PrimaryLanguageSpokenAtHome);
                language.ReadOnly = true;
            }

            // Life Event
            if (this.user.LifeEvent != null)
            {
                PdfTextField lifeEvent = (PdfTextField)(document.AcroForm.Fields["LifeEvent"]);
                if (this.user.LifeEvent.hasLifeEvent == false)
                {
                    lifeEvent.Value = new PdfString("N/A");
                }
                else
                {
                    lifeEvent.Value = new PdfString(this.user.LifeEvent.LifeEventExplain);
                }
                lifeEvent.ReadOnly = true;
            }

            #region Celebrates
            // Celebrates
            if (this.user.Celebrate != null)
            {
                PdfCheckBoxField doesCelebrate = (PdfCheckBoxField)(document.AcroForm.Fields["BirthdayCheckYes"]);
                PdfCheckBoxField doesNotCelebrate = (PdfCheckBoxField)(document.AcroForm.Fields["BirthdayCheckNo"]);
                PdfTextField celebrateExplain = (PdfTextField)(document.AcroForm.Fields["DoesNotCelebrate"]);

                if (this.user.Celebrate.doesCelebrate)
                {
                    doesCelebrate.Checked = true;
                    doesNotCelebrate.Checked = false;
                    doesCelebrate.ReadOnly = true;
                    doesNotCelebrate.ReadOnly = true;
                }
                else
                {
                    doesCelebrate.Checked = false;
                    doesNotCelebrate.Checked = true;
                    doesCelebrate.ReadOnly = true;
                    doesNotCelebrate.ReadOnly = true;
                    celebrateExplain.Value = new PdfString(this.user.Celebrate.explainNotCelebrate);
                }
            }

            #endregion

            /*
             * 
             * 
             * 
             *   STOPPED AT SCHOOL INFO = NEED TO FINISSHHHHHHH! 
             * 
             * 
             * 
             */

            if (this.user.Guardians.Count >= 1)
            {
                PdfSignatureField parentSignature = (PdfSignatureField)(document.AcroForm.Fields["parentSignature"]);
                parentSignature.Value = new PdfString(this.user.Guardians[0].Name.FName + " " + this.user.Guardians[0].Name.LName);
                parentSignature.ReadOnly = true;

                PdfTextField signDate = (PdfTextField)(document.AcroForm.Fields["signedDate"]);
                signDate.Value = new PdfString(DateTime.Now.ToString("yyyy-MM-dd"));
                signDate.ReadOnly = true;
            }

            document.SecuritySettings.PermitFormsFill = false;
            document.SecuritySettings.PermitModifyDocument = false;
            document.SecuritySettings.PermitFullQualityPrint = true;
            document.SecuritySettings.PermitPrint = true;

            byte[] fileContents = null;
            using (MemoryStream stream = new MemoryStream())
            {
                document.Save(stream);
                fileContents = stream.ToArray();
                return fileContents;
            }
        }

    }
}