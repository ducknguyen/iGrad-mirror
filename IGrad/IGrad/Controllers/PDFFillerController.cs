using IGrad.Context;
using IGrad.Models.User;
using PdfSharp.Pdf;
using PdfSharp.Pdf.AcroForms;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Ionic.Zip;
using IGrad.Models.Income;
using System.Text.RegularExpressions;

namespace IGrad.Controllers
{
    public class PDFFillerController : Controller
    {
        private UserModel user;
        private FamilyIncome famIncome;

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
                       .Include(u => u.HealthInfo)
                       .Include(u => u.Guardians.Select(n => n.Name))
                       .Include(u => u.Siblings)
                       .Include(u => u.LanguageHisory)
                       .Include(u => u.LifeEvent)
                       .Include(u => u.SchoolInfo)
                       .Include(u => u.SchoolInfo.HighSchoolInformation)
                       .Include(u => u.SchoolInfo.PriorEducation)
                       .Include(u => u.QualifiedOrEnrolledInProgam)
                       .Where(u => u.UserID == userId)
                       .FirstOrDefault<UserModel>();
                PDFFillerController pdfControl = new PDFFillerController();
                return data;
            }
        }

        private FamilyIncome GetIncomeData()
        {
            using (IncomeContext ic = new IncomeContext())
            {
                var incomeData = ic.Income
                    .Include(m => m.incomeTable)
                    .FirstOrDefault();

                return incomeData;
            }
        }

        public ActionResult FillPdf(Guid userId)
        {
            // Get the user data to use in the PDF
            this.user = GetUserData(userId);
            this.famIncome = GetIncomeData();

            using (ZipFile zip = new ZipFile())
            {
                zip.AddEntry("StudentEnrollmentChecklist.pdf", StudentEnrollmentChecklistForm());
                zip.AddEntry("StudentEnrollmentInfo.pdf", StudentInfoAndEnrollmentForm());
                zip.AddEntry("EthnicityAndRace.pdf", EthnicityAndRaceDataForm());
                zip.AddEntry("HomeLanguageSurvey.pdf", HomeLanguageSurveyForm());
                zip.AddEntry("ParentQuestionaire.pdf", ParentQuestionareForm());
                zip.AddEntry("HealthHistory.pdf", HealthHistoryForm());
                zip.AddEntry("ImmunizationStatus.pdf", ImmunizationStatusForm());
                zip.AddEntry("FamilyIncomeSurvey.pdf", FamilyIncomeForm());
                zip.AddEntry("RequestForRecords.pdf", RequestForRecordsForm());
                zip.AddEntry("NativeAmericanEducationProgram.pdf", NativeAmericanEducationProgramForm());
                zip.AddEntry("HomelessAssistance.pdf", HomelessAssistanceForm());
                zip.AddEntry("KingCountyLibrarySystem.pdf", KingCountyLibrarySystemForm());


                MemoryStream output = new MemoryStream();
                output.Position = 0;
                zip.Save(output);
                output.Position = 0;
                return File(output, "application/zip", "StudentPacket.zip");
            }
        }

        private void SetPageSizeA4(PdfDocument document)
        {
            for (int page = 0; page < document.PageCount; page++)
            {
                document.Pages[page].Size = PdfSharp.PageSize.A4;
            }
        }

        private void SetFinishedSecuritySettings(PdfDocument document)
        {
            document.SecuritySettings.PermitFormsFill = false;
            document.SecuritySettings.PermitModifyDocument = false;
            document.SecuritySettings.PermitFullQualityPrint = true;
            document.SecuritySettings.PermitPrint = true;
        }
        
        private Byte[] StudentEnrollmentChecklistForm()
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

            //fill  Highschools
            if (user.SchoolInfo.HighSchoolInformation != null)
            {
                user.SchoolInfo.HighSchoolInformation = user.SchoolInfo.HighSchoolInformation.OrderByDescending(d => d.HighSchoolYear).ToList();
                for (int i = 0; i < user.SchoolInfo.HighSchoolInformation.Count; i++)
                {
                    if (i < 5)
                    {
                        PdfTextField grade = (PdfTextField)(document.AcroForm.Fields["Grade" + (i + 1)]);
                        PdfTextField year = (PdfTextField)(document.AcroForm.Fields["Year" + (i + 1)]);
                        PdfTextField school = (PdfTextField)(document.AcroForm.Fields["School" + (i + 1)]);
                        PdfTextField cityState = (PdfTextField)(document.AcroForm.Fields["CityState" + (i + 1)]);

                        grade.Value = new PdfString(user.SchoolInfo.HighSchoolInformation[i].HighSchoolGrade.ToString());
                        year.Value = new PdfString(user.SchoolInfo.HighSchoolInformation[i].HighSchoolYear.ToString());
                        school.Value = new PdfString(user.SchoolInfo.HighSchoolInformation[i].HighSchoolName.ToString());
                        cityState.Value = new PdfString(user.SchoolInfo.HighSchoolInformation[i].HighSchoolCity.ToString() +
                            "," + user.SchoolInfo.HighSchoolInformation[i].HighSchoolState.ToString());
                    }
                }
            }

            //fill additional programs (Special Ed, 504, English Language Learner)
            PdfCheckBoxField specialEducation = (PdfCheckBoxField)(document.AcroForm.Fields["SpecialEducation"]);
            PdfCheckBoxField plan504 = (PdfCheckBoxField)(document.AcroForm.Fields["504"]);
            PdfCheckBoxField englishLanguageLearner = (PdfCheckBoxField)(document.AcroForm.Fields["EnglishLanguageLearner"]);

            if (user.QualifiedOrEnrolledInProgam != null)
            {
                if (user.QualifiedOrEnrolledInProgam.SpecialEducation)
                {
                    specialEducation.Checked = true;
                    specialEducation.ReadOnly = true;
                }
                if (user.QualifiedOrEnrolledInProgam.plan504)
                {
                    plan504.Checked = true;
                    plan504.Checked = true;
                }
                if (user.QualifiedOrEnrolledInProgam.EngishAsSecondLanguage)
                {
                    englishLanguageLearner.Checked = true;
                    englishLanguageLearner.Checked = true;
                }
            }

            if (user.SchoolInfo.IsExpelledOrSuspended)
            {
                PdfCheckBoxField studentIsSuspendedOrExpelled = (PdfCheckBoxField)(document.AcroForm.Fields["StudentIsSuspendedOrExpelled"]);
                studentIsSuspendedOrExpelled.Checked = true;
                studentIsSuspendedOrExpelled.ReadOnly = true;
            }
            else
            {
                PdfCheckBoxField studentIsNotSuspendedOrExpelled = (PdfCheckBoxField)(document.AcroForm.Fields["StudentIsNotSuspendedOrExpelled"]);
                studentIsNotSuspendedOrExpelled.Checked = true;
                studentIsNotSuspendedOrExpelled.ReadOnly = true;
            }

            //flatten pdf and security settings
            SetFinishedSecuritySettings(document);

            return writeDocument(document);
        }

        //not complete
        private Byte[] StudentInfoAndEnrollmentForm()
        {
            // Get the blank form to fill out
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/media/documents/StudentInfoAndEnrollmentForm.pdf");
            PdfDocument document = PdfReader.Open(filePath);

            // Set the flag so we can flatten once done.
            document.AcroForm.Elements.SetBoolean("/NeedAppearances", true);

            //TODO Add logic to fill form.





            return writeDocument(document);
        }

        private Byte[] EthnicityAndRaceDataForm()
        {
            // Get the blank form to fill out
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/media/documents/EthnicityAndRaceData.pdf");
            PdfDocument document = PdfReader.Open(filePath);

            // Set the flag so we can flatten once done.
            document.AcroForm.Elements.SetBoolean("/NeedAppearances", true);

            PdfTextField name = (PdfTextField)(document.AcroForm.Fields["Name"]);
            name.Value = new PdfString(user.Name.FName + " " + user.Name.MName + " " + user.Name.LName);
            name.ReadOnly = true;

            Dictionary<string, string> racesDictionary = new Dictionary<string, string>();
            HashSet<string> isRaceSet = new HashSet<string>();
            foreach (var prop in user.ConsideredRaceAndEthnicity.GetType().GetProperties())
            {
                //determine if property is boolean or string
                if (prop.PropertyType.Name == "Boolean")
                {
                    string propValue = prop.GetValue(user.ConsideredRaceAndEthnicity).ToString();
                    if (propValue == "True")
                    {
                        racesDictionary.Add(prop.Name, prop.GetValue(user.ConsideredRaceAndEthnicity).ToString());
                        isRaceSet.Add(prop.Name);
                    }
                }
            }


            //Check all the booleanProperties 
            foreach (string isRace in isRaceSet)
            {
                try
                {
                    PdfCheckBoxField checkBox = (PdfCheckBoxField)(document.AcroForm.Fields[isRace]);
                    checkBox.Checked = true;
                    checkBox.ReadOnly = true;
                }
                catch(NullReferenceException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return writeDocument(document);
        }

        private Byte[] HomeLanguageSurveyForm()
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

            PdfTextField countryBornIn = (PdfTextField)(document.AcroForm.Fields["CountryBornIn"]);
            countryBornIn.Value = new PdfString(user.BirthPlace.Country.ToString());
            countryBornIn.ReadOnly = true;

            //this approach will check either the yes or no check box based off property of hasEducationOutsideUS
            string hadEducation = user.SchoolInfo.PriorEducation.hasEducationOutsideUS.ToString();
            PdfCheckBoxField hasEducationOutsideUS = (PdfCheckBoxField)(document.AcroForm.Fields["hasEducationOutsideUS" + hadEducation]);
            hasEducationOutsideUS.Checked = true;
            hasEducationOutsideUS.ReadOnly = true;

            PdfTextField monthsOfEd = (PdfTextField)(document.AcroForm.Fields["MonthsOfEducationOutsideUS"]);
            monthsOfEd.Value = new PdfString(user.SchoolInfo.PriorEducation.MonthsOfEducationOutsideUS.ToString());
            monthsOfEd.ReadOnly = true;

            PdfTextField languageOfEducation = (PdfTextField)(document.AcroForm.Fields["LanguageOfEducationOutsideUS"]);
            languageOfEducation.Value = new PdfString(user.SchoolInfo.PriorEducation.LanguageOfEducationOutsideUS.ToString());
            languageOfEducation.ReadOnly = true;

            PdfTextField firstAttendanceYear = (PdfTextField)(document.AcroForm.Fields["firstAttendanceOfUSEducationYear"]);
            firstAttendanceYear.Value = new PdfString(user.SchoolInfo.PriorEducation.firstAttendanceOfUSEducation.Year.ToString());
            firstAttendanceYear.ReadOnly = true;

            PdfTextField firstAttendanceMonth = (PdfTextField)(document.AcroForm.Fields["firstAttendanceOfUSEducationMonth"]);
            firstAttendanceMonth.Value = new PdfString(user.SchoolInfo.PriorEducation.firstAttendanceOfUSEducation.Month.ToString());
            firstAttendanceMonth.ReadOnly = true;

            PdfTextField firstAttendanceDay = (PdfTextField)(document.AcroForm.Fields["firstAttendanceOfUSEducationDay"]);
            firstAttendanceDay.Value = new PdfString(user.SchoolInfo.PriorEducation.firstAttendanceOfUSEducation.Day.ToString());
            firstAttendanceDay.ReadOnly = true;










            return writeDocument(document);
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

            if (this.user.Siblings.Count >= 1)
            {
                Sibling1Name.Value = new PdfString(this.user.Siblings[0].FName + " " + this.user.Siblings[0].LName);
                Sibling1Age.Value = new PdfString(this.user.Siblings[0].Age.ToString());
                SiblingGrade1.Value = new PdfString(this.user.Siblings[0].Grade);
            }
            if (this.user.Siblings.Count >= 2)
            {
                Sibling2Name.Value = new PdfString(this.user.Siblings[1].FName + " " + this.user.Siblings[1].LName);
                Sibling2Age.Value = new PdfString(this.user.Siblings[1].Age.ToString());
                SiblingGrade2.Value = new PdfString(this.user.Siblings[1].Grade);
            }

            if (this.user.Siblings.Count >= 3)
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

            #region SchoolInfo
            // get schools in last year
            try
            {
                int curYear = Convert.ToInt32(DateTime.Now.Year.ToString());
                int schoolsInLastYearCount = 0;

                if (user.SchoolInfo.HighSchoolInformation != null)
                {
                    user.SchoolInfo.HighSchoolInformation = user.SchoolInfo.HighSchoolInformation.OrderByDescending(d => d.HighSchoolYear).ToList();
                    for (int i = 0; i < user.SchoolInfo.HighSchoolInformation.Count; i++)
                    {

                        int year = Convert.ToInt32(user.SchoolInfo.HighSchoolInformation[i].HighSchoolYear.ToString());
                        if(year >= curYear -1 && year <= curYear)
                        {
                            schoolsInLastYearCount++;
                        }
                    }

                    PdfTextField numOfSchoolsLastYear = (PdfTextField)(document.AcroForm.Fields["HowManySchoolsLastYear"]);
                    numOfSchoolsLastYear.Value = new PdfString(schoolsInLastYearCount.ToString());
                    numOfSchoolsLastYear.ReadOnly = true;

                }
            }
            catch(Exception ex)
            {
                // Do nothing
            }

            // show previous hs info
            if (this.user.SchoolInfo.HighSchoolInformation != null)
            {
                string prevHighSchool = "";
                for (int i = 0; i < this.user.SchoolInfo.HighSchoolInformation.Count; i++)
                {
                    if (this.user.SchoolInfo.HighSchoolInformation[i].isLastHighSchoolAttended)
                    {
                        // LastYearSchoolInfo
                        prevHighSchool = this.user.SchoolInfo.HighSchoolInformation[i].HighSchoolName;

                        PdfTextField LastYearSchoolInfo = (PdfTextField)(document.AcroForm.Fields["LastYearSchoolInfo"]);
                        LastYearSchoolInfo.Value = new PdfString(this.user.SchoolInfo.HighSchoolInformation[i].HighSchoolName + ", " + this.user.SchoolInfo.HighSchoolInformation[i].HighSchoolState);
                        LastYearSchoolInfo.ReadOnly = true;
                    }
                }
            }
            #endregion

            #region School Fines
            if(user.SchoolInfo.PreviousSchoolViolation != null)
            {
                if (this.user.SchoolInfo.PreviousSchoolViolation.hasUnpaidFine)
                {
                    PdfCheckBoxField unpaidFine = (PdfCheckBoxField)(document.AcroForm.Fields["HasUnpaidFines"]);
                    unpaidFine.Checked = true;
                    unpaidFine.ReadOnly = true;

                    PdfTextField explainFine = (PdfTextField)(document.AcroForm.Fields["ExplainUnpaidFines"]);
                    explainFine.Value = new PdfString(this.user.SchoolInfo.PreviousSchoolViolation.ExplainUnpaidFine);
                    explainFine.ReadOnly = true;
                }
                else
                {
                    PdfCheckBoxField noUnpaidFine = (PdfCheckBoxField)(document.AcroForm.Fields["NoUnpaidFines"]);
                    noUnpaidFine.Checked = true;
                    noUnpaidFine.ReadOnly = true;
                }
            }
           
            #endregion

            #region Special Ed
            if(this.user.QualifiedOrEnrolledInProgam != null)
            {
                bool hasSpecialEducation = false;
                string specialEdText = "";
                if(this.user.QualifiedOrEnrolledInProgam.SpecialEducation)
                {
                    hasSpecialEducation = true;
                    specialEdText = specialEdText + " Special Education,";
                }
                if(this.user.QualifiedOrEnrolledInProgam.EngishAsSecondLanguage)
                {
                    hasSpecialEducation = true;
                    specialEdText = specialEdText + " English Second Language,";
                }
                if(this.user.QualifiedOrEnrolledInProgam.HighlyCapable)
                {
                    hasSpecialEducation = true;
                    specialEdText = specialEdText + " Highly Capable,";
                }
                if (this.user.QualifiedOrEnrolledInProgam.LAP)
                {
                    hasSpecialEducation = true;
                    specialEdText = specialEdText + " LAP,";
                }
                if (this.user.QualifiedOrEnrolledInProgam.plan504)
                {
                    hasSpecialEducation = true;
                    specialEdText = specialEdText + " Plan 504,";
                }
                if (this.user.QualifiedOrEnrolledInProgam.Title)
                {
                    hasSpecialEducation = true;
                    specialEdText = specialEdText + " Title,";
                }

                // set check box
                if (hasSpecialEducation)
                {
                    PdfCheckBoxField SpecialProgramsYes = (PdfCheckBoxField)(document.AcroForm.Fields["SpecialProgramsYes"]);
                    SpecialProgramsYes.Checked = true;
                    SpecialProgramsYes.ReadOnly = true;

                    // set explanation.
                    
                    PdfTextField ListSpecialPrograms = (PdfTextField)(document.AcroForm.Fields["ListSpecialPrograms"]);
                    ListSpecialPrograms.Value = new PdfString(specialEdText);
                    ListSpecialPrograms.ReadOnly = true;
                }
                else
                {
                    PdfCheckBoxField SpecialProgramsNo = (PdfCheckBoxField)(document.AcroForm.Fields["SpecialProgramsNo"]);
                    SpecialProgramsNo.Checked = true;
                    SpecialProgramsNo.ReadOnly = true;
                }
            }
            #endregion

            #region School Opinions
            // childs opinion on school
            PdfTextField HowDoesChildLikeSchool = (PdfTextField)(document.AcroForm.Fields["HowDoesChildLikeSchool"]);
            HowDoesChildLikeSchool.Value = new PdfString(this.user.SchoolInfo.SchoolOpinion);
            HowDoesChildLikeSchool.ReadOnly = true;

            // childs feedback in school
            PdfTextField SchoolFeedback = (PdfTextField)(document.AcroForm.Fields["SchoolFeedback"]);
            SchoolFeedback.Value = new PdfString(this.user.SchoolInfo.HowDoingInSchool);
            SchoolFeedback.ReadOnly = true;
            #endregion

            #region Explain Violation / Diciplanary
            if (user.SchoolInfo.PreviousSchoolViolation != null)
            {


                if (this.user.SchoolInfo.PreviousSchoolViolation.isSuspendedOrExpelled ||
                this.user.SchoolInfo.PreviousSchoolViolation.hasOtherViolation ||
                this.user.SchoolInfo.PreviousSchoolViolation.hasDiciplanaryStatus ||
                this.user.SchoolInfo.PreviousSchoolViolation.hadWeaponViolation ||
                this.user.SchoolInfo.IsExpelledOrSuspended)
                {
                    PdfCheckBoxField hasDisciplinary = (PdfCheckBoxField)(document.AcroForm.Fields["hasDisciplinary"]);
                    hasDisciplinary.Checked = true;
                    hasDisciplinary.ReadOnly = true;

                    string violationExplanation = "";
                    if (this.user.SchoolInfo.IsExpelledOrSuspended || this.user.SchoolInfo.PreviousSchoolViolation.isSuspendedOrExpelled)
                    {
                        violationExplanation = violationExplanation + " Expelled / Suspended, ";
                    }
                    if (this.user.SchoolInfo.PreviousSchoolViolation.hasOtherViolation)
                    {
                        if (!string.IsNullOrEmpty(this.user.SchoolInfo.PreviousSchoolViolation.ExplainOtherViolation))
                        {
                            violationExplanation = violationExplanation + string.Format(" {0},", this.user.SchoolInfo.PreviousSchoolViolation.ExplainOtherViolation);
                        }
                    }

                    if (this.user.SchoolInfo.PreviousSchoolViolation.hasDiciplanaryStatus)
                    {
                        if (!string.IsNullOrEmpty(this.user.SchoolInfo.PreviousSchoolViolation.ExplainDiciplanaryStatus))
                        {
                            violationExplanation = violationExplanation + string.Format(" {0},", this.user.SchoolInfo.PreviousSchoolViolation.ExplainDiciplanaryStatus);
                        }
                    }

                    if (this.user.SchoolInfo.PreviousSchoolViolation.hadWeaponViolation)
                    {
                        if (this.user.SchoolInfo.PreviousSchoolViolation.dateOfWeaponViolation != null)
                        {
                            violationExplanation = violationExplanation + string.Format(" Weapon Violation on {0},", this.user.SchoolInfo.PreviousSchoolViolation.dateOfWeaponViolation);
                        }
                    }
                }
                else
                {
                    PdfCheckBoxField hasDisciplinary = (PdfCheckBoxField)(document.AcroForm.Fields["noDisciplinary"]);
                    hasDisciplinary.Checked = true;
                    hasDisciplinary.ReadOnly = true;
                }
            }

            #endregion

            if (!string.IsNullOrEmpty(this.user.SchoolInfo.StrengthAndWeakness))
            {
                PdfTextField BriefChildStrengthWeak = (PdfTextField)(document.AcroForm.Fields["BriefChildStrengthWeak"]);
                BriefChildStrengthWeak.Value = new PdfString(this.user.SchoolInfo.StrengthAndWeakness);
                BriefChildStrengthWeak.ReadOnly = true;
            }
            

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

            return writeDocument(document);
        }

        //not complete
        private Byte[] HealthHistoryForm()
        {
            // Get the blank form to fill out
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/media/documents/HealthHistory.pdf");

            PdfDocument document = PdfReader.Open(filePath);

            //set page size A4
            SetPageSizeA4(document);

            // Set the flag so we can flatten once done.
            document.AcroForm.Elements.SetBoolean("/NeedAppearances", true);

            //TODO Add logic to fill form.

            //Name, Date, Birthday, gender, and School
            PdfTextField studentName = (PdfTextField)(document.AcroForm.Fields["StudentName"]);
            studentName.Value = new PdfString(user.Name.FName + " " + user.Name.MName + " " + user.Name.LName);
            studentName.ReadOnly = true;

            PdfTextField todayDate = (PdfTextField)(document.AcroForm.Fields["TodayDate"]);
            todayDate.Value = new PdfString(DateTime.Now.Date.ToShortDateString());
            todayDate.ReadOnly = true;

            //Birthday
            PdfTextField birthday = (PdfTextField)(document.AcroForm.Fields["Birthday"]);
            birthday.Value = new PdfString(user.Birthday.Date.ToShortDateString());
            birthday.ReadOnly = true;

            //gender
            if (user.Gender == "Male")
            {
                PdfCheckBoxField gender = (PdfCheckBoxField)(document.AcroForm.Fields["GenderMale"]);
                gender.Checked = true;
                gender.ReadOnly = true;
            }
            else
            {
                PdfCheckBoxField gender = (PdfCheckBoxField)(document.AcroForm.Fields["GenderFemale"]);
                gender.Checked = true;
                gender.ReadOnly = true;
            }

            //create lists of string and boolean properties
            List<string> booleanProperties = new List<string>();
            Dictionary<string, string> stringProperties = new Dictionary<string, string>();
            HashSet<string> stringPropertiesSet = new HashSet<string>();

            //Iterate through all types of medical history for booleans and string values, add to collection
            foreach (var prop in user.HealthInfo.GetType().GetProperties())
            {
                //get the type name
                string propType = prop.PropertyType.Name;

                //determine if property is boolean or string
                if (propType == "Boolean")
                {
                    string propStr = prop.Name + prop.GetValue(user.HealthInfo).ToString();
                    booleanProperties.Add(propStr);
                }
                else if (propType == "String")
                {
                    try
                    {
                        stringProperties.Add(prop.Name, prop.GetValue(user.HealthInfo).ToString());
                    }
                    catch (NullReferenceException e)
                    {
                        stringProperties.Add(prop.Name, "");
                    }
                    stringPropertiesSet.Add(prop.Name);
                }
            }


            //Check all the booleanProperties 
            foreach (string s in booleanProperties)
            {
                PdfCheckBoxField checkBox = (PdfCheckBoxField)(document.AcroForm.Fields[s]);
                checkBox.Checked = true;
                checkBox.ReadOnly = true;
            }

            //fill all the string properties with their description
            foreach (string s in stringPropertiesSet)
            {
                PdfTextField text = (PdfTextField)(document.AcroForm.Fields[s]);
                text.Value = new PdfString(stringProperties[s].ToString());
                text.ReadOnly = true;
            }

            SetFinishedSecuritySettings(document);
            return writeDocument(document);
        }

        private Byte[] ImmunizationStatusForm()
        {
            // Get the blank form to fill out
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/media/documents/ImmunizationStatus.pdf");
            PdfDocument document = PdfReader.Open(filePath);
            SetPageSizeA4(document);
            // Set the flag so we can flatten once done.
            document.AcroForm.Elements.SetBoolean("/NeedAppearances", true);

            //TODO Add logic to fill form.

            PdfTextField firstName = (PdfTextField)(document.AcroForm.Fields["FirstName"]);
            firstName.Value = new PdfString(user.Name.FName);
            firstName.ReadOnly = true;

            PdfTextField lastName = (PdfTextField)(document.AcroForm.Fields["LastName"]);
            lastName.Value = new PdfString(user.Name.LName);
            lastName.ReadOnly = true;

            PdfTextField middleInitial = (PdfTextField)(document.AcroForm.Fields["MiddleInitial"]);
            middleInitial.Value = new PdfString(user.Name.MName[0].ToString());
            middleInitial.ReadOnly = true;

            PdfTextField birthday = (PdfTextField)(document.AcroForm.Fields["Birthday"]);
            birthday.Value = new PdfString(user.Birthday.ToShortDateString());
            birthday.ReadOnly = true;

            PdfTextField gender = (PdfTextField)(document.AcroForm.Fields["Gender"]);
            gender.Value = new PdfString(user.Gender);
            gender.ReadOnly = true;

            return writeDocument(document);
        }

        public Byte[] FamilyIncomeForm()
        {
            // Get the blank form to fill out
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/media/documents/FamilyIncomeSurveyApp.pdf");
            PdfDocument document = PdfReader.Open(filePath);

            // Set the flag so we can flatten once done.
            document.AcroForm.Elements.SetBoolean("/NeedAppearances", true);

            #region DocumentRequiredData
            PdfTextField surveyYears1 = (PdfTextField)(document.AcroForm.Fields["SurveyYears1"]);
            surveyYears1.Value = new PdfString(this.famIncome.IncomeTableYears);
            surveyYears1.ReadOnly = true;

            PdfTextField surveyYears2 = (PdfTextField)(document.AcroForm.Fields["SurveyYears2"]);
            surveyYears2.Value = new PdfString(this.famIncome.IncomeTableYears);
            surveyYears2.ReadOnly = true;

            PdfTextField effectiveDates = (PdfTextField)(document.AcroForm.Fields["EffectiveDates"]);
            effectiveDates.Value = new PdfString(this.famIncome.EffectiveDates);
            effectiveDates.ReadOnly = true;
            #endregion

            #region income Pdf field arrays
            string[] monthlyFields = { "Monthly1", "Monthly2",
                "Monthly3", "Monthly4",
                "Monthly5", "Monthly6",
                "Monthly7", "Monthly8",
                "Monthly9", "Monthly10",
                "Monthly11", "Monthly12",
                "Monthly13", "Monthly14",
                "Monthly15"};
            string[] twiceMonthlyFields = { "TwiceMonthly1", "TwiceMonthly2",
                "TwiceMonthly3", "TwiceMonthly4",
                "TwiceMonthly5", "TwiceMonthly6",
                "TwiceMonthly7", "TwiceMonthly8",
                "TwiceMonthly9", "TwiceMonthly10",
                "TwiceMonthly11", "TwiceMonthly12",
                "TwiceMonthly13", "TwiceMonthly14",
                "TwiceMonthly15"};
            string[] twoWeeksFields = { "twoWeeks1", "twoWeeks2",
                "twoWeeks3", "twoWeeks4",
                "twoWeeks5", "twoWeeks6",
                "twoWeeks7", "twoWeeks8",
                "twoWeeks9", "twoWeeks10",
                "twoWeeks11", "twoWeeks12",
                "twoWeeks13", "twoWeeks14",
                "twoWeeks15"};
            string[] weeklyFields = { "Weekly1", "Weekly2",
                "Weekly3", "Weekly4",
                "Weekly5", "Weekly6",
                "Weekly7", "Weekly8",
                "Weekly9", "Weekly10",
                "Weekly11", "Weekly12",
                "Weekly12", "Weekly14",
                "Weekly15"};
            string[] annuallyFields = { "Annual1", "Annual2",
                "Annual3", "Annual5",
                "Annual5", "Annual6",
                "Annual7", "Annual8",
                "Annual9", "Annual10",
                "Annual11", "Annual12",
                "Annual12", "Annual14",
                "Annual15"};
            #endregion

            bool userMatchedIncome = false;

            for (int i = 0; i < this.famIncome.incomeTable.Count; i++)
            {
                //document.AcroForm.Fields["Monthly" + (i + 1)].Value = new PdfString(this.famIncome.incomeTable[i].Monthly);
                //document.AcroForm.Fields["TwiceMonthly" + (i + 1)].Value = new PdfString(this.famIncome.incomeTable[i].Monthly);
                //document.AcroForm.Fields["twoWeeks" + (i + 1)].Value = new PdfString(this.famIncome.incomeTable[i].Monthly);

                PdfTextField Monthly = (PdfTextField)(document.AcroForm.Fields["Monthly" + (i + 1)]);
                Monthly.Value = new PdfString(this.famIncome.incomeTable[i].Monthly);
                Monthly.ReadOnly = true;
                PdfTextField twiceMonthly = (PdfTextField)(document.AcroForm.Fields["TwiceMonthly" + (i + 1)]);
                twiceMonthly.Value = new PdfString(this.famIncome.incomeTable[i].TwiceMonthly);
                twiceMonthly.ReadOnly = true;
                PdfTextField twoWeeks = (PdfTextField)(document.AcroForm.Fields["twoWeeks" + (i + 1)]);
                twoWeeks.Value = new PdfString(this.famIncome.incomeTable[i].TwoWeeks);
                twoWeeks.ReadOnly = true;
                PdfTextField weekly = (PdfTextField)(document.AcroForm.Fields["Weekly" + (i + 1)]);
                weekly.Value = new PdfString(this.famIncome.incomeTable[i].Weekly);
                weekly.ReadOnly = true;
                PdfTextField annually = (PdfTextField)(document.AcroForm.Fields["Annual" + (i + 1)]);
                annually.Value = new PdfString(this.famIncome.incomeTable[i].Annually);
                annually.ReadOnly = true;

                // Compare user input anual income to table income chart
                #region annual income parser 
                if (!userMatchedIncome)
                {
                    try
                    {
                        int value1 = -1;
                        int value2 = -1;
                        string income = this.famIncome.incomeTable[i].Annually.ToString();

                        // remove comma's from numbers to keep regex simple and number conversion simple
                        income = income.Replace(",", "");

                        Match match = Regex.Match(income, @"[0-9]+", RegexOptions.IgnoreCase);
                        if (match.Success)
                        {
                            value1 = Convert.ToInt32(match.ToString());
                            income = income.Remove(match.Index, match.Length);
                            Match subMatch = Regex.Match(income, @"[0-9]+", RegexOptions.IgnoreCase);
                            if (subMatch.Success)
                            {
                                value2 = Convert.ToInt32(subMatch.ToString());
                            }
                        }

                        if(value1 == -1 || value2 == -1)
                        {
                            // do not continue doing anything, let it go back 
                        }

                        if(this.user.LivesWith.AnnualHouseHoldIncome >= value1 || this.user.LivesWith.AnnualHouseHoldIncome <= value2)
                        {
                            userMatchedIncome = true;
                            PdfCheckBoxField incomeBox = (PdfCheckBoxField)(document.AcroForm.Fields["IncomeCheck" + i]);
                            incomeBox.Checked = true;
                            incomeBox.ReadOnly = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        // bruh
                    }
                }

                #endregion
            }
            if(!userMatchedIncome)
            {
                PdfCheckBoxField incomeBox = (PdfCheckBoxField)(document.AcroForm.Fields["CustomIncomeCheck"]);
                PdfTextField annually = (PdfTextField)(document.AcroForm.Fields["fill_32"]); // do custom field in pdf
                incomeBox.Checked = true;
                incomeBox.ReadOnly = true;
                if(user.LivesWith != null)
                {
                    annually.Value = new PdfString(this.user.LivesWith.AnnualHouseHoldIncome.ToString());
                }
                else
                {
                    annually.Value = new PdfString("No value provided.");
                }
                annually.ReadOnly = true;
            }




            return writeDocument(document);
        }

        //not complete
        private Byte[] RequestForRecordsForm()
        {
            // Get the blank form to fill out
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/media/documents/RequestForRecords1-25-2018.pdf");
            PdfDocument document = PdfReader.Open(filePath);

            // Set the flag so we can flatten once done.
            document.AcroForm.Elements.SetBoolean("/NeedAppearances", true);

            PdfTextField firstrqst = (PdfTextField)(document.AcroForm.Fields["1st rqst"]);
            firstrqst.Value = new PdfString("X");
            firstrqst.ReadOnly = true;

            if (this.user.SchoolInfo.HighSchoolInformation != null)
            {
                string prevHighSchool = "";
                for (int i = 0; i < this.user.SchoolInfo.HighSchoolInformation.Count; i++)
                {
                    if (this.user.SchoolInfo.HighSchoolInformation[i].isLastHighSchoolAttended)
                    {
                        prevHighSchool = this.user.SchoolInfo.HighSchoolInformation[i].HighSchoolName;
                    }
                }

                PdfTextField lastSchool = (PdfTextField)(document.AcroForm.Fields["School Name Previous School"]);
                lastSchool.Value = new PdfString(prevHighSchool);
                lastSchool.ReadOnly = true;

                PdfTextField grade = (PdfTextField)(document.AcroForm.Fields["Grade"]);
                grade.Value = new PdfString(this.user.SchoolInfo.CurrentGrade.ToString());
                grade.ReadOnly = true;
            }

            if (this.user.ResidentAddress != null)
            {
                PdfTextField address = (PdfTextField)(document.AcroForm.Fields["Address"]);
                if (string.IsNullOrEmpty(this.user.ResidentAddress.POBox))
                {
                    address.Value = new PdfString(this.user.ResidentAddress.Street);
                }
                else
                {
                    address.Value = new PdfString(this.user.ResidentAddress.POBox);
                }
                address.ReadOnly = true;

                PdfTextField city = (PdfTextField)(document.AcroForm.Fields["City"]);
                city.Value = new PdfString(this.user.ResidentAddress.City);
                city.ReadOnly = true;

                PdfTextField state = (PdfTextField)(document.AcroForm.Fields["State"]);
                state.Value = new PdfString(this.user.ResidentAddress.City);
                state.ReadOnly = true;

                PdfTextField zipcode = (PdfTextField)(document.AcroForm.Fields["Zip Code"]);
                zipcode.Value = new PdfString(this.user.ResidentAddress.City);
                zipcode.ReadOnly = true;
            }

            PdfTextField phone = (PdfTextField)(document.AcroForm.Fields["Phone"]);
            phone.Value = new PdfString(this.user.PhoneInfo.PhoneNumber);
            phone.ReadOnly = true;

            PdfTextField lName = (PdfTextField)(document.AcroForm.Fields["Last Name"]);
            lName.Value = new PdfString(this.user.Name.LName);
            lName.ReadOnly = true;

            PdfTextField fName = (PdfTextField)(document.AcroForm.Fields["First"]);
            fName.Value = new PdfString(this.user.Name.FName);
            fName.ReadOnly = true;

            PdfTextField mi = (PdfTextField)(document.AcroForm.Fields["MI"]);
            mi.Value = new PdfString(this.user.Name.MName);
            mi.ReadOnly = true;

            PdfTextField dob = (PdfTextField)(document.AcroForm.Fields["DOB"]);
            dob.Value = new PdfString(this.user.Birthday.ToString("MM-dd-yyyy"));
            dob.ReadOnly = true;

            PdfSignatureField parentSignature = (PdfSignatureField)(document.AcroForm.Fields["ParentGuardian Signature"]);
            if (user.Guardians != null)
            {
                try
                {
                    if (user.Guardians[0] != null)
                    {
                        parentSignature.Value = new PdfString(this.user.Guardians[0].Name.FName + " " + this.user.Guardians[0].Name.LName);
                        parentSignature.ReadOnly = true;
                    }
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.Out.Write("No guardian in guardian ArrayList : " + e.Message + " : " + e.StackTrace);
                }
            }
            return writeDocument(document);
        }

        //not complete
        private Byte[] NativeAmericanEducationProgramForm()
        {
            // Get the blank form to fill out
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/media/documents/NativeAmericanEducationProgram.pdf");
            PdfDocument document = PdfReader.Open(filePath);

            // Set the flag so we can flatten once done.
            document.AcroForm.Elements.SetBoolean("/NeedAppearances", true);

            //TODO Add logic to fill form.


            return writeDocument(document);
        }

        //not complete
        private Byte[] HomelessAssistanceForm()
        {
            // Get the blank form to fill out
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/media/documents/HomelessAssistance.pdf");
            PdfDocument document = PdfReader.Open(filePath);

            // Set the flag so we can flatten once done.
            document.AcroForm.Elements.SetBoolean("/NeedAppearances", true);

            //TODO Add logic to fill form.


            return writeDocument(document);
        }

        //not complete
        private Byte[] KingCountyLibrarySystemForm()
        {
            // Get the blank form to fill out
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/media/documents/KingCountyLibrarySystem.pdf");
            PdfDocument document = PdfReader.Open(filePath);

            // Set the flag so we can flatten once done.
            document.AcroForm.Elements.SetBoolean("/NeedAppearances", true);

            //TODO Add logic to fill form.


            return writeDocument(document);

        }

        private static byte[] writeDocument(PdfDocument document)
        {
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
    }
}