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
using System.Text;

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
                       .Include(u => u.Guardians)
                       .Include(u => u.Guardians.Select(n => n.Name))
                       .Include(u => u.Guardians.Select(p => p.PhoneOne))
                       .Include(u => u.Guardians.Select(p => p.PhoneTwo))
                       .Include(u => u.Siblings)
                       .Include(u => u.LivesWith)
                       .Include(u => u.LanguageHisory)
                       .Include(u => u.LifeEvent)
                       .Include(u => u.SchoolInfo)
                       .Include(u => u.SchoolInfo.HighSchoolInformation)
                       .Include(u => u.SchoolInfo.PriorEducation)
                       .Include(u => u.SchoolInfo.PreviousSchoolViolation)
                       .Include(u => u.QualifiedOrEnrolledInProgam)
                       .Include(u => u.NativeAmericanEducation)
                       .Include(u => u.NativeAmericanEducation.AddressOfTribeMaintainingEnrollment)
                       .Include(u => u.HomelessAssistance)
                       .Include(u => u.ResidentAddress)
                       .Include(u => u.MailingAddress)
                       .Include(u => u.SecondaryHouseholdAddress)
                       .Include(u => u.OptionalOpportunities)
                       .Include(u => u.StudentsParentingPlan)
                       .Include(u => u.MillitaryInfo)
                       .Include(u => u.Guardians.Select(k=>k.Address))
                       .Include(u => u.StudentChildCare)
                       .Include(u => u.EmergencyContacts)
                       .Include(u => u.EmergencyContacts.Select(n => n.Name))
                       .Include(u => u.EmergencyContacts.Select(p => p.PhoneOne))
                       .Include(u => u.EmergencyContacts.Select(p => p.PhoneTwo))
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
                zip.AddEntry("MilitaryInfo.pdf", MilitaryInfoForm());
                zip.AddEntry("KingCountyLibrarySystem.pdf", KingCountyLibrarySystemForm());

                //optional forms
                if (UserRequiresNativeAmericanForm(user.ConsideredRaceAndEthnicity))
                {
                    zip.AddEntry("NativeAmericanEducationProgram.pdf", NativeAmericanEducationProgramForm());
                }

                if (UserRequiresHomelessAssistanceForm())
                {
                    zip.AddEntry("HomelessAssistance.pdf", HomelessAssistanceForm());
                }

                if (UserRequiresOptionalAssistanceForm())
                {
                    zip.AddEntry("IGradOptionalAssistance.pdf", IGradOptionalAssistance());
                }


                MemoryStream output = new MemoryStream();
                output.Position = 0;
                zip.Save(output);
                output.Position = 0;
                return File(output, "application/zip", "StudentPacket.zip");
            }
        }

        private static void SetPageSizeA4(PdfDocument document)
        {
            for (int page = 0; page < document.PageCount; page++)
            {
                document.Pages[page].Size = PdfSharp.PageSize.A4;
            }
        }

        private bool UserRequiresNativeAmericanForm(RaceEthnicity race)
        {
            if (race.isAlaskaNative || race.isChehalis || race.isColville ||
                  race.isCowlitz || race.isHoh || race.isHames ||
                  race.isKalispel || race.isLowerElwha || race.isLummi ||
                  race.isMakah || race.isMuckleshoot || race.isNisqually ||
                  race.isNooksack || race.isPortGambleClallam || race.isPuyallup ||
                  race.isQuileute || race.isSamish || race.isSauk_Suiattle ||
                  race.isShoalwater || race.isSkokomish || race.isSnoqualmie ||
                  race.isSpokane || race.isSquaxinIsland || race.isStillaguamish ||
                  race.isSwinomish || race.isTulalip || race.isUpperSkagit ||
                  race.isYakama || race.isOtherWashingtonIndian ||
                  race.isOtherNorthCentralOrSouthAmericanIndian)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool UserRequiresHomelessAssistanceForm()
        {
            if (user.HomelessAssistance != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool UserRequiresOptionalAssistanceForm()
        {
            if (user.OptionalOpportunities != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void SetFinishedSecuritySettings(PdfDocument document)
        {
            document.SecuritySettings.PermitFormsFill = false;
            document.SecuritySettings.PermitModifyDocument = false;
            document.SecuritySettings.PermitFullQualityPrint = true;
            document.SecuritySettings.PermitPrint = true;
        }

        private static void SetAllDocumentFieldsReadOnly(PdfDocument document)
        {
            if (document.AcroForm.Fields.Count() > 0)
            {
                for (int i = 0; i < document.AcroForm.Fields.Count(); i++)
                {
                    document.AcroForm.Fields[i].ReadOnly = true;
                }
            }
        }

        private Byte[] MilitaryInfoForm()
        {
            // Get Blank Form
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/media/documents/MilitaryForm.pdf");
            PdfDocument document = PdfReader.Open(filePath);

            // Set the flag so we can flatten once done.
            document.AcroForm.Elements.SetBoolean("/NeedAppearances", true);

            PdfTextField studentName = (PdfTextField)(document.AcroForm.Fields["StudentName"]);
            studentName.Value = new PdfString(this.user.Name.FName + " " + this.user.Name.LName);

            PdfTextField newSchool = (PdfTextField)(document.AcroForm.Fields["School"]);
            newSchool.Value = new PdfString("iGrad");

            PdfCheckBoxField activeduty = (PdfCheckBoxField)(document.AcroForm.Fields["ActiveDuty"]);
            PdfCheckBoxField nationalguard = (PdfCheckBoxField)(document.AcroForm.Fields["NationalGuard"]);
            PdfCheckBoxField morethanone = (PdfCheckBoxField)(document.AcroForm.Fields["MoreThanOne"]);
            PdfCheckBoxField none = (PdfCheckBoxField)(document.AcroForm.Fields["None"]);
            PdfCheckBoxField reserves = (PdfCheckBoxField)(document.AcroForm.Fields["Reserves"]);
            PdfCheckBoxField refused = (PdfCheckBoxField)(document.AcroForm.Fields["Refused"]);

            if (user.MillitaryInfo != null)
            {
                if (user.MillitaryInfo.ArmedForcesActiveDuty)
                {
                    activeduty.Checked = true;
                }
                if (user.MillitaryInfo.NationalGuard)
                {
                    nationalguard.Checked = true;
                }
                if (user.MillitaryInfo.MoreThanOne)
                {
                    morethanone.Checked = true;
                }
                if (user.MillitaryInfo.None)
                {
                    none.Checked = true;
                }
                if (user.MillitaryInfo.ArmedForcesReserved)
                {
                    reserves.Checked = true;
                }
                if (user.MillitaryInfo.PreferNotToAnswer)
                {
                    refused.Checked = true;
                }
            }

            return writeDocument(document);
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

            PdfTextField lastName = (PdfTextField)(document.AcroForm.Fields["StudentLastName"]);
            lastName.Value = new PdfString(this.user.Name.LName);

            PdfTextField middleInitial = (PdfTextField)(document.AcroForm.Fields["MI"]);
            middleInitial.Value = new PdfString(this.user.Name.MName);

            PdfTextField birthday = (PdfTextField)(document.AcroForm.Fields["Birthday"]);
            birthday.Value = new PdfString(this.user.Birthday.ToString("MM-dd-yyyy"));

            PdfTextField gender = (PdfTextField)(document.AcroForm.Fields["Gender"]);
            gender.Value = new PdfString(user.Gender);

            int age = DateTime.Now.Year - user.Birthday.Year;
            if (DateTime.Now < user.Birthday.AddYears(age))
            {
                age--;
            }

            PdfTextField formAge = (PdfTextField)(document.AcroForm.Fields["Age"]);
            formAge.Value = new PdfString(age.ToString());

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
                }
                if (user.QualifiedOrEnrolledInProgam.plan504)
                {
                    plan504.Checked = true;
                }
                if (user.QualifiedOrEnrolledInProgam.EnglishAsSecondLanguage)
                {
                    englishLanguageLearner.Checked = true;
                }
            }

            if (user.SchoolInfo.IsExpelledOrSuspended)
            {
                PdfCheckBoxField studentIsSuspendedOrExpelled = (PdfCheckBoxField)(document.AcroForm.Fields["StudentIsSuspendedOrExpelled"]);
                studentIsSuspendedOrExpelled.Checked = true;
            }
            else
            {
                PdfCheckBoxField studentIsNotSuspendedOrExpelled = (PdfCheckBoxField)(document.AcroForm.Fields["StudentIsNotSuspendedOrExpelled"]);
                studentIsNotSuspendedOrExpelled.Checked = true;
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

            //Student Name
            PdfTextField studentLastName = (PdfTextField)(document.AcroForm.Fields["StudentLastName"]);
            studentLastName.Value = new PdfString(user.Name.LName);

            PdfTextField studentFirstName = (PdfTextField)(document.AcroForm.Fields["StudentFirstName"]);
            studentFirstName.Value = new PdfString(user.Name.FName);

            PdfTextField studentMiddleName = (PdfTextField)(document.AcroForm.Fields["StudentMiddleName"]);
            studentMiddleName.Value = new PdfString(user.Name.MName);

            PdfTextField studentPrevName = (PdfTextField)(document.AcroForm.Fields["StudentPreviousName"]);
            studentPrevName.Value = new PdfString(user.Name.PreviousName);

            //Birthday Gender and grade level
            PdfTextField studentBirthday = (PdfTextField)(document.AcroForm.Fields["StudentBirthday"]);
            studentBirthday.Value = new PdfString(user.Birthday.ToShortDateString());

            if (user.Gender == "Male")
            {
                PdfCheckBoxField studentIsMale = (PdfCheckBoxField)(document.AcroForm.Fields["StudentIsMale"]);
                studentIsMale.Checked = true;
            }
            else
            {
                PdfCheckBoxField studentIsFemale = (PdfCheckBoxField)(document.AcroForm.Fields["StudentIsFemale"]);
                studentIsFemale.Checked = true;
            }

            PdfTextField studentGrade = (PdfTextField)(document.AcroForm.Fields["StudentGrade"]);
            studentGrade.Value = new PdfString(user.SchoolInfo.CurrentGrade.ToString());

            //birthplace, student lives with
            PdfTextField studentBirthplaceCity = (PdfTextField)(document.AcroForm.Fields["BirthplaceCity"]);
            studentBirthplaceCity.Value = new PdfString(user.BirthPlace.City);

            PdfTextField studentBirthplaceState = (PdfTextField)(document.AcroForm.Fields["BirthplaceState"]);
            studentBirthplaceState.Value = new PdfString(user.BirthPlace.State);

            PdfTextField studentBirthplaceCountry = (PdfTextField)(document.AcroForm.Fields["BirthplaceCountry"]);
            studentBirthplaceCountry.Value = new PdfString(user.BirthPlace.Country);

            //Lives with checkboxes
            PdfCheckBoxField livesWithBothParents = (PdfCheckBoxField)(document.AcroForm.Fields["LivesWithBothParents"]);
            livesWithBothParents.Checked = user.LivesWith.LivesWithBothParents;

            PdfCheckBoxField livesWithMotherOnly = (PdfCheckBoxField)(document.AcroForm.Fields["LivesWithMotherOnly"]);
            livesWithMotherOnly.Checked = user.LivesWith.LivesWithMotherOnly;

            PdfCheckBoxField livesWithFatherAndStepMom = (PdfCheckBoxField)(document.AcroForm.Fields["LivesWithFatherAndStepMom"]);
            livesWithFatherAndStepMom.Checked = user.LivesWith.LivesWithFatherAndStepMom;

            PdfCheckBoxField livesWithGuardian = (PdfCheckBoxField)(document.AcroForm.Fields["LivesWithGuardian"]);
            livesWithGuardian.Checked = user.LivesWith.LivesWithGuardian;

            PdfCheckBoxField livesWithSelf = (PdfCheckBoxField)(document.AcroForm.Fields["LivesWithSelf"]);
            livesWithSelf.Checked = user.LivesWith.LivesWithSelf;

            PdfCheckBoxField livesWithGrandparents = (PdfCheckBoxField)(document.AcroForm.Fields["LivesWithGrandparents"]);
            livesWithGrandparents.Checked = user.LivesWith.LivesWithGrandparents;

            PdfCheckBoxField livesWithFatherOnly = (PdfCheckBoxField)(document.AcroForm.Fields["LivesWithFatherOnly"]);
            livesWithFatherOnly.Checked = user.LivesWith.LivesWithFatherOnly;

            PdfCheckBoxField livesWithMotherAndStepDad = (PdfCheckBoxField)(document.AcroForm.Fields["LivesWithMotherAndStepDad"]);
            livesWithMotherAndStepDad.Checked = user.LivesWith.LivesWithMotherAndStepDad;

            PdfCheckBoxField livesWithFosterParents = (PdfCheckBoxField)(document.AcroForm.Fields["LivesWithFosterParents"]);
            livesWithFosterParents.Checked = user.LivesWith.LivesWithFosterParents;

            PdfCheckBoxField livesWithAgency = (PdfCheckBoxField)(document.AcroForm.Fields["LivesWithAgency"]);
            livesWithAgency.Checked = user.LivesWith.LivesWithAgency;

            //check if they live with other, fill out description and check the checkbox for other as we're not capturing the other checkbox.
            if (user.LivesWith.Other != "")
            {
                PdfTextField livesWithOtherDesc = (PdfTextField)(document.AcroForm.Fields["LivesWithOtherDesc"]);
                livesWithOtherDesc.Value = new PdfString(user.LivesWith.Other);

                PdfCheckBoxField livesWithOther = (PdfCheckBoxField)(document.AcroForm.Fields["LivesWithOther"]);
                livesWithOther.Checked = true;
            }

            #region primary guardians
            //Primary guardian information
            List<Guardian> primaryGuardians = user.GetPrimaryGuardians();
            if (primaryGuardians.Count > 0)
            {
                Guardian guardian1 = primaryGuardians[0];
                //fill name
                PdfTextField guardian1Name = (PdfTextField)document.AcroForm.Fields["PrimaryGuardian1Name"];
                if(guardian1.Name.MName != null)
                {
                    guardian1Name.Value = new PdfString(guardian1.Name.LName + " " + guardian1.Name.FName + guardian1.Name.MName.Substring(0, 1));
                }else
                {
                    guardian1Name.Value = new PdfString(guardian1.Name.LName + " " + guardian1.Name.FName );
                }

                string guardianRelationshipCheckBoxName = "PrimaryGuardian1" + guardian1.Relationship;

                PdfCheckBoxField relationshipCheckbox = (PdfCheckBoxField)document.AcroForm.Fields[guardianRelationshipCheckBoxName];
                relationshipCheckbox.Checked = true;

                //phone 1
                PdfTextField phoneOne = (PdfTextField)document.AcroForm.Fields["PrimaryGuardian1PhoneOne"];
                phoneOne.Value = new PdfString(guardian1.PhoneOne.PhoneNumber.ToString());
                //phone 1 type
                PdfTextField phoneOneType = (PdfTextField)document.AcroForm.Fields["PrimaryGuardian1PhoneOneType"];
                phoneOneType.Value = new PdfString(guardian1.PhoneOne.PhoneType.ToString());

                //phone 2
                if(guardian1.PhoneTwo.PhoneNumber != null)
                {
                    PdfTextField phoneTwo = (PdfTextField)document.AcroForm.Fields["PrimaryGuardian1PhoneTwo"];
                    phoneTwo.Value = new PdfString(guardian1.PhoneTwo.PhoneNumber.ToString());
                    //phone 2 type
                    PdfTextField phoneTwoType = (PdfTextField)document.AcroForm.Fields["PrimaryGuardian1PhoneTwoType"];
                    phoneTwoType.Value = new PdfString(guardian1.PhoneTwo.PhoneType.ToString());
                }
                

                //email
                PdfTextField email = (PdfTextField)document.AcroForm.Fields["PrimaryGuardian1Email"];
                email.Value = new PdfString(guardian1.Email.ToString());

                //active military?
                if (guardian1.IsActiveMilitary)
                {
                    PdfCheckBoxField activeMilitary = (PdfCheckBoxField)document.AcroForm.Fields["PrimaryGuardian1IsActiveMilitaryTrue"];
                    activeMilitary.Checked = true;
                }
                else
                {
                    PdfCheckBoxField activeMilitary = (PdfCheckBoxField)document.AcroForm.Fields["PrimaryGuardian1IsActiveMilitaryFalse"];
                    activeMilitary.Checked = true;
                }
            }

            // primary guardian 2 information
            if (primaryGuardians.Count > 1)
            {
                Guardian guardian2 = primaryGuardians[1];
                //fill name
                PdfTextField guardian2Name = (PdfTextField)document.AcroForm.Fields["PrimaryGuardian2Name"];
                guardian2Name.Value = new PdfString(guardian2.Name.LName + " " + guardian2.Name.FName + guardian2.Name.MName.Substring(0, 1));

                string guardianRelationshipCheckBoxName = "PrimaryGuardian2" + guardian2.Relationship;

                PdfCheckBoxField relationshipCheckbox = (PdfCheckBoxField)document.AcroForm.Fields[guardianRelationshipCheckBoxName];
                relationshipCheckbox.Checked = true;

                //phone 1
                PdfTextField phoneOne = (PdfTextField)document.AcroForm.Fields["PrimaryGuardian2PhoneOne"];
                phoneOne.Value = new PdfString(guardian2.PhoneOne.PhoneNumber.ToString());
                //phone 1 type
                PdfTextField phoneOneType = (PdfTextField)document.AcroForm.Fields["PrimaryGuardian2PhoneOneType"];
                phoneOneType.Value = new PdfString(guardian2.PhoneOne.PhoneType.ToString());

                //phone 2
                PdfTextField phoneTwo = (PdfTextField)document.AcroForm.Fields["PrimaryGuardian2PhoneTwo"];
                phoneTwo.Value = new PdfString(guardian2.PhoneTwo.PhoneNumber.ToString());
                //phone 2 type
                PdfTextField phoneTwoType = (PdfTextField)document.AcroForm.Fields["PrimaryGuardian2PhoneTwoType"];
                phoneTwoType.Value = new PdfString(guardian2.PhoneTwo.PhoneType.ToString());

                //email
                PdfTextField email = (PdfTextField)document.AcroForm.Fields["PrimaryGuardian2Email"];
                email.Value = new PdfString(guardian2.Email.ToString());

                //active military?
                if (guardian2.IsActiveMilitary)
                {
                    PdfCheckBoxField activeMilitary = (PdfCheckBoxField)document.AcroForm.Fields["PrimaryGuardian2IsActiveMilitaryTrue"];
                    activeMilitary.Checked = true;
                }
                else
                {
                    PdfCheckBoxField activeMilitary = (PdfCheckBoxField)document.AcroForm.Fields["PrimaryGuardian2IsActiveMilitaryFalse"];
                    activeMilitary.Checked = true;
                }
            }
            #endregion


            #region SecondaryHouseholdAndGuardians
            if (user.SecondaryHouseholdAddress != null)
            {
                List<Guardian> secondaryGuardians = user.GetSecondaryGuardians();
                if (secondaryGuardians.Count > 0)
                {
                    Guardian guardian1 = secondaryGuardians[0];
                    //fill name
                    PdfTextField guardian1Name = (PdfTextField)document.AcroForm.Fields["SecondaryGuardian1Name"];
                    guardian1Name.Value = new PdfString(guardian1.Name.LName + " " + guardian1.Name.FName + guardian1.Name.MName.Substring(0, 1));

                    string guardianRelationshipCheckBoxName = "SecondaryGuardian1" + guardian1.Relationship;
                    if (guardian1.Relationship != "Guardian")
                    {
                        PdfCheckBoxField relationshipCheckbox = (PdfCheckBoxField)document.AcroForm.Fields[guardianRelationshipCheckBoxName];
                        relationshipCheckbox.Checked = true;
                    }


                    //phone 1
                    PdfTextField phoneOne = (PdfTextField)document.AcroForm.Fields["SecondaryGuardian1PhoneOne"];
                    phoneOne.Value = new PdfString(guardian1.PhoneOne.PhoneNumber.ToString());
                    //phone 1 type
                    PdfTextField phoneOneType = (PdfTextField)document.AcroForm.Fields["SecondaryGuardian1PhoneOneType"];
                    phoneOneType.Value = new PdfString(guardian1.PhoneOne.PhoneType.ToString());

                    //phone 2
                    PdfTextField phoneTwo = (PdfTextField)document.AcroForm.Fields["SecondaryGuardian1PhoneTwo"];
                    phoneTwo.Value = new PdfString(guardian1.PhoneTwo.PhoneNumber.ToString());
                    //phone 2 type
                    PdfTextField phoneTwoType = (PdfTextField)document.AcroForm.Fields["SecondaryGuardian1PhoneTwoType"];
                    phoneTwoType.Value = new PdfString(guardian1.PhoneTwo.PhoneType.ToString());

                    //email
                    PdfTextField email = (PdfTextField)document.AcroForm.Fields["SecondaryGuardian1Email"];
                    email.Value = new PdfString(guardian1.Email.ToString());

                }

                // secondary guardian 2 information
                if (secondaryGuardians.Count > 1)
                {
                    Guardian guardian2 = secondaryGuardians[0];
                    //fill name
                    PdfTextField guardian1Name = (PdfTextField)document.AcroForm.Fields["SecondaryGuardian2Name"];
                    guardian1Name.Value = new PdfString(guardian2.Name.LName + " " + guardian2.Name.FName + guardian2.Name.MName.Substring(0, 1));

                    string guardianRelationshipCheckBoxName = "SecondaryGuardian2" + guardian2.Relationship;
                    if (guardian2.Relationship != "Guardian")
                    {
                        PdfCheckBoxField relationshipCheckbox = (PdfCheckBoxField)document.AcroForm.Fields[guardianRelationshipCheckBoxName];
                        relationshipCheckbox.Checked = true;
                    }


                    //phone 1
                    PdfTextField phoneOne = (PdfTextField)document.AcroForm.Fields["SecondaryGuardian2PhoneOne"];
                    phoneOne.Value = new PdfString(guardian2.PhoneOne.PhoneNumber.ToString());
                    //phone 1 type
                    PdfTextField phoneOneType = (PdfTextField)document.AcroForm.Fields["SecondaryGuardian2PhoneOneType"];
                    phoneOneType.Value = new PdfString(guardian2.PhoneOne.PhoneType.ToString());

                    //phone 2
                    PdfTextField phoneTwo = (PdfTextField)document.AcroForm.Fields["SecondaryGuardian2PhoneTwo"];
                    phoneTwo.Value = new PdfString(guardian2.PhoneTwo.PhoneNumber.ToString());
                    //phone 2 type
                    PdfTextField phoneTwoType = (PdfTextField)document.AcroForm.Fields["SecondaryGuardian2PhoneTwoType"];
                    phoneTwoType.Value = new PdfString(guardian2.PhoneTwo.PhoneType.ToString());

                    //email
                    PdfTextField email = (PdfTextField)document.AcroForm.Fields["SecondaryGuardian2Email"];
                    email.Value = new PdfString(guardian2.Email.ToString());

                    //check active military
                    if (secondaryGuardians[0].IsActiveMilitary || secondaryGuardians[1].IsActiveMilitary)
                    {
                        PdfCheckBoxField activeMilitary = (PdfCheckBoxField)document.AcroForm.Fields["SecondaryGuardianIsActiveMilitaryTrue"];
                        activeMilitary.Checked = true;
                    }
                    else
                    {
                        PdfCheckBoxField activeMilitary = (PdfCheckBoxField)document.AcroForm.Fields["SecondaryGuardianIsActiveMilitaryFalse"];
                        activeMilitary.Checked = true;
                    }
                }

                

                //secondary household address
                Address secAddr = user.SecondaryHouseholdAddress;

                PdfTextField address = (PdfTextField)document.AcroForm.Fields["SecondaryHouseholdAddress"];
                address.Value = new PdfString(secAddr.Street + " " + secAddr.City + ", " + secAddr.State + " " + secAddr.Zip + " " + secAddr.POBox);

            }
            #endregion


            //Resident address
            PdfTextField residentAddressStreet = (PdfTextField)(document.AcroForm.Fields["ResidentAddressStreet"]);
            residentAddressStreet.Value = new PdfString(user.ResidentAddress.Street);

            PdfTextField residentAddressApt = (PdfTextField)(document.AcroForm.Fields["ResidentAddressApt"]);
            residentAddressApt.Value = new PdfString(user.ResidentAddress.AptNum);

            PdfTextField residentAddressCity = (PdfTextField)(document.AcroForm.Fields["ResidentAddressCity"]);
            residentAddressCity.Value = new PdfString(user.ResidentAddress.City);

            PdfTextField residentAddressState = (PdfTextField)(document.AcroForm.Fields["ResidentAddressState"]);
            residentAddressState.Value = new PdfString(user.ResidentAddress.State);

            PdfTextField residentAddressZip = (PdfTextField)(document.AcroForm.Fields["ResidentAddressZip"]);
            residentAddressZip.Value = new PdfString(user.ResidentAddress.Zip.ToString());

            //Mailing address

            PdfTextField mailingAddressStreet = (PdfTextField)(document.AcroForm.Fields["MailingAddressStreet"]);
            mailingAddressStreet.Value = new PdfString(user.MailingAddress.Street);

            PdfTextField mailingAddressApt = (PdfTextField)(document.AcroForm.Fields["MailingAddressApt"]);
            mailingAddressApt.Value = new PdfString(user.MailingAddress.AptNum);

            PdfTextField mailingAddressCity = (PdfTextField)(document.AcroForm.Fields["MailingAddressCity"]);
            mailingAddressCity.Value = new PdfString(user.MailingAddress.City);

            PdfTextField mailingAddressState = (PdfTextField)(document.AcroForm.Fields["MailingAddressState"]);
            mailingAddressState.Value = new PdfString(user.MailingAddress.State);

            PdfTextField mailingAddressZip = (PdfTextField)(document.AcroForm.Fields["MailingAddressZip"]);
            mailingAddressZip.Value = new PdfString(user.MailingAddress.Zip.ToString());

            //Resident Home
            PdfTextField residentPhone = (PdfTextField)(document.AcroForm.Fields["ResidentHomePhone"]);
            residentPhone.Value = new PdfString(user.PhoneInfo.PhoneNumber + "    " + user.PhoneInfo.PhoneType);

            //for (int i = 0; i < user.Guardians.Count; i++)
            //{
            //    if (i < 2)
            //    {
            //        PdfTextField guardianWorkPhone = (PdfTextField)(document.AcroForm.Fields["GuardianWorkPhone" + (i + 1)]);
            //        guardianWorkPhone.Value = new PdfString(user.Guardians[i].PhoneOne.PhoneNumber);

            //        PdfTextField guardianEmail = (PdfTextField)(document.AcroForm.Fields["GuardianEmail" + (i + 1)]);
            //        guardianEmail.Value = new PdfString(user.Guardians[i].Email);
            //    }
            //}

            //Parenting Plan
            if (user.StudentsParentingPlan.inEffect)
            {
                PdfCheckBoxField planInEffect = (PdfCheckBoxField)(document.AcroForm.Fields["InEffectTrue"]);
                planInEffect.Checked = true;
            }
            else
            {
                PdfCheckBoxField planNotInEffect = (PdfCheckBoxField)(document.AcroForm.Fields["InEffectFalse"]);
                planNotInEffect.Checked = true;
            }

            //court order on education decisions
            if (user.StudentsParentingPlan.CourtOrderOnEducationDecisions)
            {
                PdfCheckBoxField courtOrder = (PdfCheckBoxField)document.AcroForm.Fields["CourtOrderOnEducationDecisionsTrue"];
                courtOrder.Checked = true;
            }
            else
            {
                PdfCheckBoxField courtOrder = (PdfCheckBoxField)document.AcroForm.Fields["CourtOrderOnEducationDecisionsFalse"];
                courtOrder.Checked = true;
            }

            PdfCheckBoxField motherHasCourtOrder = (PdfCheckBoxField)(document.AcroForm.Fields["MotherHasOrder"]);
            motherHasCourtOrder.Checked = user.StudentsParentingPlan.MotherHasOrder;

            PdfCheckBoxField fatherHasCourtOrder = (PdfCheckBoxField)(document.AcroForm.Fields["FatherHasOrder"]);
            fatherHasCourtOrder.Checked = user.StudentsParentingPlan.FatherHasOrder;

            if (!(String.IsNullOrEmpty(user.StudentsParentingPlan.Other)))
            {
                //check other box
                PdfCheckBoxField otherCourtOrder = (PdfCheckBoxField)(document.AcroForm.Fields["OtherCourtOrderCheckbox"]);
                otherCourtOrder.Checked = true;

                PdfTextField otherCourtOrderDesc = (PdfTextField)(document.AcroForm.Fields["OtherCourtOrderDesc"]);
                otherCourtOrderDesc.Value = new PdfString(user.StudentsParentingPlan.Other);
            }


            //list siblings
            for (int i = 0; i < user.Siblings.Count; i++)
            {
                PdfTextField siblingLastName = (PdfTextField)(document.AcroForm.Fields["SiblingLastName" + (i + 1)]);
                siblingLastName.Value = new PdfString(user.Siblings[i].LName);

                PdfTextField siblingFirstName = (PdfTextField)(document.AcroForm.Fields["SiblingFirstName" + (i + 1)]);
                siblingFirstName.Value = new PdfString(user.Siblings[i].FName);

                PdfTextField siblingSchool = (PdfTextField)(document.AcroForm.Fields["SiblingSchool" + (i + 1)]);
                siblingSchool.Value = new PdfString(user.Siblings[i].School);

                PdfTextField siblingGrade = (PdfTextField)(document.AcroForm.Fields["SiblingGrade" + (i + 1)]);
                siblingGrade.Value = new PdfString(user.Siblings[i].Grade.ToString());
            }

            //child care section
            if (user.StudentChildCare != null)
            {
                PdfCheckBoxField childCareBeforeSchool = (PdfCheckBoxField)(document.AcroForm.Fields["ChildCareIsBeforeSchool"]);
                childCareBeforeSchool.Checked = user.StudentChildCare.IsBeforeSchool;

                PdfCheckBoxField childCareIsAfterSchool = (PdfCheckBoxField)(document.AcroForm.Fields["ChildCareIsAfterSchool"]);
                childCareIsAfterSchool.Checked = user.StudentChildCare.IsAfterSchool;

                PdfCheckBoxField childCareIsBeforeAndAfterSchool = (PdfCheckBoxField)(document.AcroForm.Fields["ChildCareIsBeforeAndAfterSchool"]);
                childCareIsBeforeAndAfterSchool.Checked = user.StudentChildCare.IsBeforeAndAfterSchool;

                if (user.StudentChildCare.ProviderName != null)
                {
                    PdfTextField childCareProviderName = (PdfTextField)(document.AcroForm.Fields["ChildCareProviderName"]);
                    childCareProviderName.Value = new PdfString(user.StudentChildCare.ProviderName);
                }

                if (user.StudentChildCare.ProviderAddress != null)
                {
                    PdfTextField childCareProviderAddress = (PdfTextField)(document.AcroForm.Fields["ChildCareProviderAddress"]);
                    childCareProviderAddress.Value = new PdfString(user.StudentChildCare.ProviderAddress.Street + " , "
                       + user.StudentChildCare.ProviderAddress.City + "," + user.StudentChildCare.ProviderAddress.State + " "
                       + user.StudentChildCare.ProviderAddress.Zip.ToString());
                }

                if (user.StudentChildCare.ProviderPhoneNumber != null)
                {
                    PdfTextField childCareProviderPhoneNumber = (PdfTextField)(document.AcroForm.Fields["ChildCareProviderPhoneNumber"]);
                    childCareProviderPhoneNumber.Value = new PdfString(user.StudentChildCare.ProviderPhoneNumber);
                }
            }

            if (user.PreSchool != null)
            {
                if (user.PreSchool.Count > 0)
                {
                    PdfCheckBoxField attendedPreschool = (PdfCheckBoxField)(document.AcroForm.Fields["ChildAttendedPreschoolTrue"]);
                    attendedPreschool.Checked = true;

                    for (int i = 0; i < user.PreSchool.Count; i++)
                    {
                        PdfTextField preschoolName = (PdfTextField)(document.AcroForm.Fields["PreschoolName" + (i + 1)]);
                        preschoolName.Value = new PdfString(user.PreSchool[i].Name);

                        PdfTextField preschoolAddress = (PdfTextField)(document.AcroForm.Fields["PreschoolAddress" + (i + 1)]);
                        Address address = user.PreSchool[i].Address;
                        string fullAddress;
                        if (address.POBox != "")
                        {
                            fullAddress = address.Street + " POBOX: " + address.POBox + " " + address.City + ", " + address.State + " "
                            + address.Zip.ToString();
                        }
                        else
                        {
                            fullAddress = address.Street + " " + address.City + ", " + address.State + " "
                            + address.Zip.ToString();
                        }
                        preschoolAddress.Value = new PdfString(fullAddress);
                    }
                }
            }


            //Special Programs
            //special ed
            if (user.QualifiedOrEnrolledInProgam != null)
            {
                if (user.QualifiedOrEnrolledInProgam.SpecialEducation)
                {
                    PdfCheckBoxField specEd = (PdfCheckBoxField)(document.AcroForm.Fields["SpecialEducationTrue"]);
                    specEd.Checked = true;
                }
                else
                {
                    PdfCheckBoxField specEd = (PdfCheckBoxField)(document.AcroForm.Fields["SpecialEducationFalse"]);
                    specEd.Checked = true;
                }
                //plan504
                if (user.QualifiedOrEnrolledInProgam.plan504)
                {
                    PdfCheckBoxField plan504 = (PdfCheckBoxField)(document.AcroForm.Fields["Plan504True"]);
                    plan504.Checked = true;
                }
                else
                {
                    PdfCheckBoxField plan504 = (PdfCheckBoxField)(document.AcroForm.Fields["Plan504False"]);
                    plan504.Checked = true;
                }
                //Title program
                if (user.QualifiedOrEnrolledInProgam.Title)
                {
                    PdfCheckBoxField title = (PdfCheckBoxField)(document.AcroForm.Fields["TitleTrue"]);
                    title.Checked = true;
                }
                else
                {
                    PdfCheckBoxField title = (PdfCheckBoxField)(document.AcroForm.Fields["TitleFalse"]);
                    title.Checked = true;
                }

                //LAP
                if (user.QualifiedOrEnrolledInProgam.LAP)
                {
                    PdfCheckBoxField lap = (PdfCheckBoxField)(document.AcroForm.Fields["LAPTrue"]);
                    lap.Checked = true;
                }
                else
                {
                    PdfCheckBoxField lap = (PdfCheckBoxField)(document.AcroForm.Fields["LAPFalse"]);
                    lap.Checked = true;
                }

                //highly capable

                if (user.QualifiedOrEnrolledInProgam.HighlyCapable)
                {
                    PdfCheckBoxField highlyCapable = (PdfCheckBoxField)(document.AcroForm.Fields["HighlyCapableTrue"]);
                    highlyCapable.Checked = true;
                }
                else
                {
                    PdfCheckBoxField highlyCapable = (PdfCheckBoxField)(document.AcroForm.Fields["HighlyCapableFalse"]);
                    highlyCapable.Checked = true;
                }

                //English as second language (ESL)

                if (user.QualifiedOrEnrolledInProgam.EnglishAsSecondLanguage)
                {
                    PdfCheckBoxField esl = (PdfCheckBoxField)(document.AcroForm.Fields["EnglishAsSecondLanguageTrue"]);
                    esl.Checked = true;
                }
                else
                {
                    PdfCheckBoxField esl = (PdfCheckBoxField)(document.AcroForm.Fields["EnglishAsSecondLanguageFalse"]);
                    esl.Checked = true;
                }
            }


            if (user.Retainment != null)
            {
                if (user.Retainment.hasBeenRetained)
                {
                    PdfCheckBoxField hasBeenRetained = (PdfCheckBoxField)(document.AcroForm.Fields["HasBeenRetainedTrue"]);
                    hasBeenRetained.Checked = true;

                    PdfTextField retainedGradeLevel = (PdfTextField)(document.AcroForm.Fields["RetainmentGradeLevel"]);
                    retainedGradeLevel.Value = new PdfString(user.Retainment.GradeLevelsRetained.ToString());
                }
            }
            else
            {
                PdfCheckBoxField hasBeenRetained = (PdfCheckBoxField)(document.AcroForm.Fields["HasBeenRetainedFalse"]);
                hasBeenRetained.Checked = true;
            }


            //last school attended info
            for (int i = 0; i < user.SchoolInfo.HighSchoolInformation.Count; i++)
            {
                HighSchoolInfo highSchool = user.SchoolInfo.HighSchoolInformation[i];
                if (highSchool.isLastHighSchoolAttended)
                {
                    PdfTextField LastHighSchoolAttended = (PdfTextField)(document.AcroForm.Fields["LastHighSchoolAttended"]);
                    LastHighSchoolAttended.Value = new PdfString(highSchool.HighSchoolName);

                    PdfTextField lastHighSchoolLocationInfo = (PdfTextField)(document.AcroForm.Fields["LastHighSchoolInformation"]);
                    lastHighSchoolLocationInfo.Value = new PdfString(highSchool.HighSchoolCity + ", " + highSchool.HighSchoolState);

                    PdfTextField lastHighSchoolDistrict = (PdfTextField)(document.AcroForm.Fields["LastHighSchoolDistrict"]);
                    lastHighSchoolDistrict.Value = new PdfString(highSchool.SchoolDistrict);
                }
            }

            //WashingtonState  school
            StringBuilder schoolsInWAStringBuilder = new StringBuilder();
            StringBuilder schoolsInKentSchoolDistrictStringBuilder = new StringBuilder();
            int lastWashingtonAttendDate = 0;
            int lastKentSDAttendDate = 0;
            if (user.SchoolInfo.HighSchoolInformation.Count > 0)
            {
                for (int i = 0; i < user.SchoolInfo.HighSchoolInformation.Count; i++)
                {
                    HighSchoolInfo highSchool = user.SchoolInfo.HighSchoolInformation[i];
                    if (Regex.IsMatch(highSchool.HighSchoolState, "WA", RegexOptions.IgnoreCase) ||
                        Regex.IsMatch(highSchool.HighSchoolState, "Washington", RegexOptions.IgnoreCase))
                    {
                        if (schoolsInWAStringBuilder.Length > 0)
                        {
                            schoolsInWAStringBuilder.Append(", " + highSchool.HighSchoolName);
                        }
                        else
                        {
                            schoolsInWAStringBuilder.Append(highSchool.HighSchoolName);
                        }


                        //check year if greater (only keeping greatest)
                        if (Int16.Parse(highSchool.HighSchoolYear) > lastWashingtonAttendDate)
                        {
                            lastWashingtonAttendDate = Int16.Parse(highSchool.HighSchoolYear);
                        }
                    }
                    if(highSchool.SchoolDistrict != null)
                    {
                        if (Regex.IsMatch(highSchool.SchoolDistrict, "kent.*", RegexOptions.IgnoreCase))
                        {

                            if (schoolsInKentSchoolDistrictStringBuilder.Length > 0)
                            {
                                schoolsInKentSchoolDistrictStringBuilder.Append(", " + highSchool.HighSchoolName);
                            }
                            else
                            {
                                schoolsInKentSchoolDistrictStringBuilder.Append(highSchool.HighSchoolName);
                            }

                            if (Int16.Parse(highSchool.HighSchoolYear) > lastKentSDAttendDate)
                            {
                                lastKentSDAttendDate = Int16.Parse(highSchool.HighSchoolYear);
                            }
                        }
                    }
                }
            }
            PdfTextField schoolsAttendedInWashington = (PdfTextField)document.AcroForm.Fields["SchoolsAttendedInWashington"];
            schoolsAttendedInWashington.Value = new PdfString(schoolsInWAStringBuilder.ToString());

            PdfTextField schoolsAttendedInWashingtonDate = (PdfTextField)document.AcroForm.Fields["SchoolsAttendedInWashingtonDate"];
            schoolsAttendedInWashingtonDate.Value = new PdfString(lastWashingtonAttendDate.ToString());

            //Attended school in kent school district

            PdfTextField schoolsAttendedInKentSD = (PdfTextField)document.AcroForm.Fields["SchoolsAttendedInKentSchoolDistrict"];
            schoolsAttendedInKentSD.Value = new PdfString(schoolsInKentSchoolDistrictStringBuilder.ToString());

            PdfTextField schoolsAttendedInKentSDDate = (PdfTextField)document.AcroForm.Fields["SchoolsAttendedInKentSchoolDistrictDate"];
            schoolsAttendedInKentSDDate.Value = new PdfString(lastKentSDAttendDate.ToString());




            //student suspended for weapons violation
            if (user.SchoolInfo.PreviousSchoolViolation != null)
            {
                //if they were suspended
                if (user.SchoolInfo.PreviousSchoolViolation.isSuspendedOrExpelled)
                {
                    //check the suspended box
                    PdfCheckBoxField suspended = (PdfCheckBoxField)(document.AcroForm.Fields["StudentSuspendedOrExpelledTrue"]);
                    suspended.Checked = true;
                    //fill the suspension date
                    PdfTextField suspensionDate = (PdfTextField)(document.AcroForm.Fields["StudentSuspendedOrExpelledDate"]);
                    suspensionDate.Value = new PdfString(user.SchoolInfo.PreviousSchoolViolation.dateOfWeaponViolation.ToShortDateString());
                }
                //otherwise, check the not suspended box
                else
                {
                    PdfCheckBoxField suspended = (PdfCheckBoxField)(document.AcroForm.Fields["StudentSuspendedOrExpelledFalse"]);
                    suspended.Checked = true;
                }
            }
            //if the weapons violation is null, we still need to check the NO checkbox for was suspended or expelled.
            else
            {
                PdfCheckBoxField suspended = (PdfCheckBoxField)(document.AcroForm.Fields["StudentSuspendedOrExpelledFalse"]);
                suspended.Checked = true;
            }

            //EMERGENCY CONTACTS

            for (int i = 0; i < user.EmergencyContacts.Count; i++)
            {
                //only want the first 3 Emergency Contact (EC)
                if (i < 3)
                {
                    EmergencyContact ec = user.EmergencyContacts[i];

                    PdfTextField ecLastName = (PdfTextField)(document.AcroForm.Fields["EmergencyContactLastName" + (i + 1)]);
                    ecLastName.Value = new PdfString(ec.Name.LName);

                    PdfTextField ecFirstName = (PdfTextField)(document.AcroForm.Fields["EmergencyContactFirstName" + (i + 1)]);
                    ecFirstName.Value = new PdfString(ec.Name.FName);

                    if(ec.Name.MName != null)
                    {
                        PdfTextField ecMiddleInitial = (PdfTextField)(document.AcroForm.Fields["EmergencyContactMiddleName" + (i + 1)]);
                        ecMiddleInitial.Value = new PdfString(ec.Name.MName.Substring(0));
                    }
                    

                    PdfTextField ecRelationToStudent = (PdfTextField)(document.AcroForm.Fields["EmergencyContactRelationshipToStudent" + (i + 1)]);
                    ecRelationToStudent.Value = new PdfString(ec.Relationship);

                    PdfTextField ecPhoneOne = (PdfTextField)(document.AcroForm.Fields["EmergencyContact" + (i + 1) + "Phone1"]);
                    ecPhoneOne.Value = new PdfString(ec.PhoneOne.PhoneNumber);

                    PdfTextField ecPhoneOneType = (PdfTextField)(document.AcroForm.Fields["EmergencyContact" + (i + 1) + "Phone1Type"]);
                    ecPhoneOneType.Value = new PdfString(ec.PhoneOne.PhoneType);

                    PdfTextField ecPhoneTwo = (PdfTextField)(document.AcroForm.Fields["EmergencyContact" + (i + 1) + "Phone2"]);
                    ecPhoneTwo.Value = new PdfString(ec.PhoneTwo.PhoneNumber);

                    PdfTextField ecPhoneTwoType = (PdfTextField)(document.AcroForm.Fields["EmergencyContact" + (i + 1) + "Phone2Type"]);
                    ecPhoneTwoType.Value = new PdfString(ec.PhoneTwo.PhoneType);

                }
            }

            return writeDocument(document);
        }

        private Byte[] EthnicityAndRaceDataForm()
        {
            // Get the blank form to fill out
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/media/documents/EthnicityAndRaceData.pdf");
            PdfDocument document = PdfReader.Open(filePath);
            // Set the flag so we can flatten once done.
            document.AcroForm.Elements.SetBoolean("/NeedAppearances", true);

            if (user.ConsideredRaceAndEthnicity == null)
            {
                return writeDocument(document);
            }
            else
            {
                PdfTextField name = (PdfTextField)(document.AcroForm.Fields["Name"]);
                name.Value = new PdfString(user.Name.FName + " " + user.Name.MName + " " + user.Name.LName);

                HashSet<string> isRaceSet = new HashSet<string>();
                foreach (var prop in user.ConsideredRaceAndEthnicity.GetType().GetProperties())
                {
                    //determine if property is boolean for race
                    if (prop.PropertyType.Name == "Boolean")
                    {
                        string propValue = prop.GetValue(user.ConsideredRaceAndEthnicity).ToString();
                        if (propValue == "True")
                        {
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
                    }
                    catch (NullReferenceException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                return writeDocument(document);
            }
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

            PdfTextField studentGrade = (PdfTextField)(document.AcroForm.Fields["Grade"]);
            studentGrade.Value = new PdfString(Convert.ToString(this.user.SchoolInfo.CurrentGrade));

            PdfTextField todayDate = (PdfTextField)(document.AcroForm.Fields["Date"]);
            todayDate.Value = new PdfString(DateTime.Now.ToString("yyyy-MM-dd"));

            //Do not need unless we're populating guardian name and signature
            //if (this.user.Guardians.Count != 0)
            //{
            //    PdfTextField parentName = (PdfTextField)(document.AcroForm.Fields["ParentGuardianName"]);
            //    parentName.Value = new PdfString(user.Guardians[0].Name.FName + " " + user.Guardians[0].Name.LName);

            //    PdfSignatureField parentSignature = (PdfSignatureField)(document.AcroForm.Fields["ParentGuardian Signature"]);
            //    parentSignature.Value = new PdfString(this.user.Guardians[0].Name.FName + " " + this.user.Guardians[0].Name.LName);
            //}
            #region LanguageInfo
            if (this.user.LanguageHisory != null)
            {
                PdfTextField prefSchoolLanguage = (PdfTextField)(document.AcroForm.Fields["preferedSchoolLanguage"]);
                prefSchoolLanguage.Value = new PdfString(this.user.LanguageHisory.PreferredLanguage);

                PdfTextField firstLearnedLanguage = (PdfTextField)(document.AcroForm.Fields["firstLanguage"]);
                firstLearnedLanguage.Value = new PdfString(this.user.LanguageHisory.UserFirstLanguageLearned);

                PdfTextField studenthomeLanguage = (PdfTextField)(document.AcroForm.Fields["homeLanguage"]);
                studenthomeLanguage.Value = new PdfString(this.user.LanguageHisory.StudentPrimaryLanguageAtHome);

                PdfTextField homeLanguage = (PdfTextField)(document.AcroForm.Fields["childsPrimaryLanguage"]);
                homeLanguage.Value = new PdfString(this.user.LanguageHisory.PrimaryLanguageSpokenAtHome);


                // English Support
                PdfCheckBoxField hadSupport = (PdfCheckBoxField)(document.AcroForm.Fields["hadEnglishSupport"]);
                PdfCheckBoxField noSupport = (PdfCheckBoxField)(document.AcroForm.Fields["noEnglishSupport"]);
                PdfCheckBoxField dunno = (PdfCheckBoxField)(document.AcroForm.Fields["noIdeaOfEnglishSupport"]);

                if (this.user.LanguageHisory.StudentReceievedEnglishDevelopmentSupport)
                {
                    hadSupport.Checked = true;
                }
                else if (!this.user.LanguageHisory.StudentReceievedEnglishDevelopmentSupport)
                {
                    noSupport.Checked = true;
                }

                else if (this.user.LanguageHisory.unsureOfEnglishSupport)
                {
                    dunno.Checked = true;
                }
            }
            #endregion

            if (user.BirthPlace.Country != null)
            {
                PdfTextField countryBornIn = (PdfTextField)(document.AcroForm.Fields["CountryBornIn"]);
                countryBornIn.Value = new PdfString(user.BirthPlace.Country.ToString());
            }


            if (user.SchoolInfo.PriorEducation != null)
            {
                //this approach will check either the yes or no check box based off property of hasEducationOutsideUS
                string hadEducation = user.SchoolInfo.PriorEducation.hasEducationOutsideUS.ToString();
                PdfCheckBoxField hasEducationOutsideUS = (PdfCheckBoxField)(document.AcroForm.Fields["hasEducationOutsideUS" + hadEducation]);
                hasEducationOutsideUS.Checked = true;

                PdfTextField monthsOfEd = (PdfTextField)(document.AcroForm.Fields["MonthsOfEducationOutsideUS"]);
                monthsOfEd.Value = new PdfString(user.SchoolInfo.PriorEducation.MonthsOfEducationOutsideUS.ToString());

                PdfTextField languageOfEducation = (PdfTextField)(document.AcroForm.Fields["LanguageOfEducationOutsideUS"]);
                languageOfEducation.Value = new PdfString(user.SchoolInfo.PriorEducation.LanguageOfEducationOutsideUS.ToString());

                PdfTextField firstAttendanceYear = (PdfTextField)(document.AcroForm.Fields["firstAttendanceOfUSEducationYear"]);
                firstAttendanceYear.Value = new PdfString(user.SchoolInfo.PriorEducation.firstAttendanceOfUSEducation.Year.ToString());

                PdfTextField firstAttendanceMonth = (PdfTextField)(document.AcroForm.Fields["firstAttendanceOfUSEducationMonth"]);
                firstAttendanceMonth.Value = new PdfString(user.SchoolInfo.PriorEducation.firstAttendanceOfUSEducation.Month.ToString());

                PdfTextField firstAttendanceDay = (PdfTextField)(document.AcroForm.Fields["firstAttendanceOfUSEducationDay"]);
                firstAttendanceDay.Value = new PdfString(user.SchoolInfo.PriorEducation.firstAttendanceOfUSEducation.Day.ToString());

            }
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

            PdfTextField lastName = (PdfTextField)(document.AcroForm.Fields["LastName"]);
            lastName.Value = new PdfString(this.user.Name.LName);

            PdfTextField middleInitial = (PdfTextField)(document.AcroForm.Fields["MiddleName"]);
            middleInitial.Value = new PdfString(this.user.Name.MName);

            PdfTextField preferedName = (PdfTextField)(document.AcroForm.Fields["PreferredName"]);
            preferedName.Value = new PdfString(this.user.Name.NickName);

            // Birthday
            if (this.user.Birthday != null)
            {
                PdfTextField birthday = (PdfTextField)(document.AcroForm.Fields["BirthDate"]);
                birthday.Value = new PdfString(this.user.Birthday.ToString("MM-dd-yyyy"));
            }

            // Show guardian list of names
            string _tempGuardList = "";
            foreach (Guardian guardian in this.user.Guardians)
            {
                _tempGuardList += guardian.Name.FName + " " + guardian.Name.LName + ";";
            }

            PdfTextField parentGuardians = (PdfTextField)(document.AcroForm.Fields["ParentGuardians"]);
            parentGuardians.Value = new PdfString(_tempGuardList);

            // Address
            if (this.user.ResidentAddress != null)
            {
                PdfTextField address = (PdfTextField)(document.AcroForm.Fields["Address"]);
                address.Value = new PdfString(this.user.ResidentAddress.PrintAddress());
            }

            // Lives With
            PdfTextField livesWith = (PdfTextField)(document.AcroForm.Fields["LivesWith"]);
            livesWith.Value = new PdfString(_tempGuardList);

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
                }
                else
                {
                    doesCelebrate.Checked = false;
                    doesNotCelebrate.Checked = true;
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
                        if (year >= curYear - 1 && year <= curYear)
                        {
                            schoolsInLastYearCount++;
                        }
                    }

                    PdfTextField numOfSchoolsLastYear = (PdfTextField)(document.AcroForm.Fields["HowManySchoolsLastYear"]);
                    numOfSchoolsLastYear.Value = new PdfString(schoolsInLastYearCount.ToString());
                }
            }
            catch (Exception ex)
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
                    }
                }
            }
            #endregion

            #region School Fines
            if (user.SchoolInfo.PreviousSchoolViolation != null)
            {
                if (this.user.SchoolInfo.PreviousSchoolViolation.hasUnpaidFine)
                {
                    PdfCheckBoxField unpaidFine = (PdfCheckBoxField)(document.AcroForm.Fields["HasUnpaidFines"]);
                    unpaidFine.Checked = true;

                    PdfTextField explainFine = (PdfTextField)(document.AcroForm.Fields["ExplainUnpaidFines"]);
                    explainFine.Value = new PdfString(this.user.SchoolInfo.PreviousSchoolViolation.ExplainUnpaidFine);
                }
                else
                {
                    PdfCheckBoxField noUnpaidFine = (PdfCheckBoxField)(document.AcroForm.Fields["NoUnpaidFines"]);
                    noUnpaidFine.Checked = true;
                }
            }

            #endregion

            #region Special Ed
            if (this.user.QualifiedOrEnrolledInProgam != null)
            {
                bool hasSpecialEducation = false;
                string specialEdText = "";
                if (this.user.QualifiedOrEnrolledInProgam.SpecialEducation)
                {
                    hasSpecialEducation = true;
                    specialEdText = specialEdText + " Special Education,";
                }
                if (this.user.QualifiedOrEnrolledInProgam.EnglishAsSecondLanguage)
                {
                    hasSpecialEducation = true;
                    specialEdText = specialEdText + " English Second Language,";
                }
                if (this.user.QualifiedOrEnrolledInProgam.HighlyCapable)
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

                    // set explanation.
                    PdfTextField ListSpecialPrograms = (PdfTextField)(document.AcroForm.Fields["ListSpecialPrograms"]);
                    ListSpecialPrograms.Value = new PdfString(specialEdText);
                }
                else
                {
                    PdfCheckBoxField SpecialProgramsNo = (PdfCheckBoxField)(document.AcroForm.Fields["SpecialProgramsNo"]);
                    SpecialProgramsNo.Checked = true;
                }
            }
            #endregion

            #region School Opinions
            // childs opinion on school
            PdfTextField HowDoesChildLikeSchool = (PdfTextField)(document.AcroForm.Fields["HowDoesChildLikeSchool"]);
            HowDoesChildLikeSchool.Value = new PdfString(this.user.SchoolInfo.SchoolOpinion);

            // childs feedback in school
            PdfTextField SchoolFeedback = (PdfTextField)(document.AcroForm.Fields["SchoolFeedback"]);
            SchoolFeedback.Value = new PdfString(this.user.SchoolInfo.HowDoingInSchool);
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

                    PdfTextField ExplainDisciplinary = (PdfTextField)(document.AcroForm.Fields["ExplainDisciplinary"]);
                    ExplainDisciplinary.Value = new PdfString(violationExplanation);

                }
                else
                {
                    PdfCheckBoxField hasDisciplinary = (PdfCheckBoxField)(document.AcroForm.Fields["noDisciplinary"]);
                    hasDisciplinary.Checked = true;
                }
            }

            #endregion
            if (user.SchoolInfo.PreviousSchoolViolation != null)
            {
                if (user.SchoolInfo.PreviousSchoolViolation.hasSexViolation ||
               user.SchoolInfo.PreviousSchoolViolation.hasCriminalViolation ||
               user.SchoolInfo.PreviousSchoolViolation.hasViolentTendicies)
                {
                    PdfCheckBoxField hasViolentDrugSexBehavior = (PdfCheckBoxField)(document.AcroForm.Fields["hasViolentDrugSexBehavior"]);
                    hasViolentDrugSexBehavior.Checked = true;

                    string explainHistoryOfViolentDrugSexBehavior = "";
                    PdfTextField HistoryOfViolentDrugSexBehavior = (PdfTextField)(document.AcroForm.Fields["HistoryOfViolentDrugSexBehavior"]);
                    if (user.SchoolInfo.PreviousSchoolViolation.hasSexViolation)
                    {
                        if (!string.IsNullOrEmpty(user.SchoolInfo.PreviousSchoolViolation.explainSexViolation))
                        {
                            explainHistoryOfViolentDrugSexBehavior = user.SchoolInfo.PreviousSchoolViolation.explainSexViolation + ". ";
                        }
                    }
                    if (user.SchoolInfo.PreviousSchoolViolation.hasCriminalViolation)
                    {
                        if (!string.IsNullOrEmpty(user.SchoolInfo.PreviousSchoolViolation.explainCriminalViolation))
                        {
                            explainHistoryOfViolentDrugSexBehavior = user.SchoolInfo.PreviousSchoolViolation.explainCriminalViolation + ". ";
                        }
                    }
                    if (user.SchoolInfo.PreviousSchoolViolation.hasViolentTendicies)
                    {
                        if (!string.IsNullOrEmpty(user.SchoolInfo.PreviousSchoolViolation.explainViolence))
                        {
                            explainHistoryOfViolentDrugSexBehavior = user.SchoolInfo.PreviousSchoolViolation.explainViolence + ". ";
                        }
                    }
                    HistoryOfViolentDrugSexBehavior.Value = new PdfString(explainHistoryOfViolentDrugSexBehavior);
                }
                else
                {
                    PdfCheckBoxField noViolentDrugSexBehavior = (PdfCheckBoxField)(document.AcroForm.Fields["noViolentDrugSexBehavior"]);
                    noViolentDrugSexBehavior.Checked = true;
                }
            }


            if (!string.IsNullOrEmpty(this.user.SchoolInfo.StrengthAndWeakness))
            {
                PdfTextField BriefChildStrengthWeak = (PdfTextField)(document.AcroForm.Fields["BriefChildStrengthWeak"]);
                BriefChildStrengthWeak.Value = new PdfString(this.user.SchoolInfo.StrengthAndWeakness);
            }

            if (!string.IsNullOrEmpty(user.SchoolInfo.ParentAdditionalFeedbackInfo))
            {
                PdfTextField parentAdditionalInfo = (PdfTextField)(document.AcroForm.Fields["AdditionalInfo"]);
                parentAdditionalInfo.Value = new PdfString(user.SchoolInfo.ParentAdditionalFeedbackInfo);
            }


            //if (this.user.Guardians.Count >= 1)
            //{
            //    PdfSignatureField parentSignature = (PdfSignatureField)(document.AcroForm.Fields["parentSignature"]);
            //    parentSignature.Value = new PdfString(this.user.Guardians[0].Name.FName + " " + this.user.Guardians[0].Name.LName);

            //    PdfTextField signDate = (PdfTextField)(document.AcroForm.Fields["signedDate"]);
            //    signDate.Value = new PdfString(DateTime.Now.ToString("yyyy-MM-dd"));
            //}

            return writeDocument(document);
        }

        private Byte[] HealthHistoryForm()
        {
            // Get the blank form to fill out
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/media/documents/HealthHistory.pdf");

            PdfDocument document = PdfReader.Open(filePath);

            //set page size A4
            SetPageSizeA4(document);

            // Set the flag so we can flatten once done.
            document.AcroForm.Elements.SetBoolean("/NeedAppearances", true);

            //Name, Date, Birthday, gender, and School
            PdfTextField studentName = (PdfTextField)(document.AcroForm.Fields["StudentName"]);
            studentName.Value = new PdfString(user.Name.FName + " " + user.Name.MName + " " + user.Name.LName);

            PdfTextField todayDate = (PdfTextField)(document.AcroForm.Fields["TodayDate"]);
            todayDate.Value = new PdfString(DateTime.Now.Date.ToShortDateString());

            //Birthday
            PdfTextField birthday = (PdfTextField)(document.AcroForm.Fields["Birthday"]);
            birthday.Value = new PdfString(user.Birthday.Date.ToShortDateString());

            //gender
            if (user.Gender == "Male")
            {
                PdfCheckBoxField gender = (PdfCheckBoxField)(document.AcroForm.Fields["GenderMale"]);
                gender.Checked = true;
            }
            else
            {
                PdfCheckBoxField gender = (PdfCheckBoxField)(document.AcroForm.Fields["GenderFemale"]);
                gender.Checked = true;
            }

            //create list of strings and boolean properties
            List<string> healthInfoList = new List<string>();
            Dictionary<string, string> healthInfoDescriptions = new Dictionary<string, string>();
            HashSet<string> stringPropertiesSet = new HashSet<string>();

            //Iterate through all types of medical history for booleans and string values, add to collection
            foreach (var prop in user.HealthInfo.GetType().GetProperties())
            {
                //get the type name
                string propType = prop.PropertyType.Name;

                //determine if property is boolean or string
                if (propType == "Boolean")
                {
                    string propStrWithValue = prop.Name + prop.GetValue(user.HealthInfo).ToString();
                    healthInfoList.Add(propStrWithValue);
                }
                else if (propType == "String")
                {
                    try
                    {
                        healthInfoDescriptions.Add(prop.Name, prop.GetValue(user.HealthInfo).ToString());
                    }
                    catch (NullReferenceException e)
                    {
                        healthInfoDescriptions.Add(prop.Name, "");
                    }
                    stringPropertiesSet.Add(prop.Name);
                }
            }

            //Check all the booleanProperties 
            foreach (string s in healthInfoList)
            {
                try
                {
                    PdfCheckBoxField checkBox = (PdfCheckBoxField)(document.AcroForm.Fields[s]);
                    checkBox.Checked = true;
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine("field does not exist: " + e.Message);
                }
            }



            //fill all the string properties with their description
            foreach (string s in stringPropertiesSet)
            {
                PdfTextField text = (PdfTextField)(document.AcroForm.Fields[s]);
                text.Value = new PdfString(healthInfoDescriptions[s].ToString());
            }

            if (user.HealthInfo.SeriousInjuryOrSurgeryDate != null)
            {
                PdfTextField injuryDate = (PdfTextField)(document.AcroForm.Fields["SeriousInjuryOrSurgeryDate"]);
                injuryDate.Value = new PdfString(user.HealthInfo.SeriousInjuryOrSurgeryDate.ToString());
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

            PdfTextField lastName = (PdfTextField)(document.AcroForm.Fields["LastName"]);
            lastName.Value = new PdfString(user.Name.LName);

            if (user.Name.MName != null)
            {
                PdfTextField middleInitial = (PdfTextField)(document.AcroForm.Fields["MiddleInitial"]);
                middleInitial.Value = new PdfString(user.Name.MName[0].ToString());
            }

            PdfTextField birthday = (PdfTextField)(document.AcroForm.Fields["Birthday"]);
            birthday.Value = new PdfString(user.Birthday.ToShortDateString());

            PdfTextField gender = (PdfTextField)(document.AcroForm.Fields["Gender"]);
            gender.Value = new PdfString(user.Gender);

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

            PdfTextField surveyYears2 = (PdfTextField)(document.AcroForm.Fields["SurveyYears2"]);
            surveyYears2.Value = new PdfString(this.famIncome.IncomeTableYears);

            PdfTextField effectiveDates = (PdfTextField)(document.AcroForm.Fields["EffectiveDates"]);
            effectiveDates.Value = new PdfString(this.famIncome.EffectiveDates);
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

                PdfTextField twiceMonthly = (PdfTextField)(document.AcroForm.Fields["TwiceMonthly" + (i + 1)]);
                twiceMonthly.Value = new PdfString(this.famIncome.incomeTable[i].TwiceMonthly);

                PdfTextField twoWeeks = (PdfTextField)(document.AcroForm.Fields["twoWeeks" + (i + 1)]);
                twoWeeks.Value = new PdfString(this.famIncome.incomeTable[i].TwoWeeks);

                PdfTextField weekly = (PdfTextField)(document.AcroForm.Fields["Weekly" + (i + 1)]);
                weekly.Value = new PdfString(this.famIncome.incomeTable[i].Weekly);

                PdfTextField annually = (PdfTextField)(document.AcroForm.Fields["Annual" + (i + 1)]);
                annually.Value = new PdfString(this.famIncome.incomeTable[i].Annually);

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

                        if (value1 == -1 || value2 == -1)
                        {
                            // do not continue doing anything, let it go back 
                        }

                        if (this.user.LivesWith.AnnualHouseHoldIncome >= value1 || this.user.LivesWith.AnnualHouseHoldIncome <= value2)
                        {
                            userMatchedIncome = true;
                            PdfCheckBoxField incomeBox = (PdfCheckBoxField)(document.AcroForm.Fields["IncomeCheck" + i]);
                            incomeBox.Checked = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        // bruh
                    }
                }

                #endregion
            }
            if (!userMatchedIncome)
            {
                PdfCheckBoxField incomeBox = (PdfCheckBoxField)(document.AcroForm.Fields["CustomIncomeCheck"]);
                PdfTextField annually = (PdfTextField)(document.AcroForm.Fields["fill_32"]); // do custom field in pdf
                incomeBox.Checked = true;
                if (user.LivesWith != null)
                {
                    annually.Value = new PdfString(this.user.LivesWith.AnnualHouseHoldIncome.ToString());
                }
                else
                {
                    annually.Value = new PdfString("No value provided.");
                }
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

            if (this.user.SchoolInfo.HighSchoolInformation != null)
            {
                HighSchoolInfo prevHighSchool;
                for (int i = 0; i < this.user.SchoolInfo.HighSchoolInformation.Count; i++)
                {
                    if (this.user.SchoolInfo.HighSchoolInformation[i].isLastHighSchoolAttended)
                    {
                        prevHighSchool = user.SchoolInfo.HighSchoolInformation[i];

                        PdfTextField lastSchool = (PdfTextField)(document.AcroForm.Fields["School Name Previous School"]);
                        lastSchool.Value = new PdfString(prevHighSchool.HighSchoolName);

                        PdfTextField lastSchoolCity = (PdfTextField)(document.AcroForm.Fields["City"]);
                        lastSchoolCity.Value = new PdfString(prevHighSchool.HighSchoolCity);

                        PdfTextField lastSchoolState = (PdfTextField)(document.AcroForm.Fields["State"]);
                        lastSchoolState.Value = new PdfString(prevHighSchool.HighSchoolState);

                        PdfTextField grade = (PdfTextField)(document.AcroForm.Fields["Grade"]);
                        grade.Value = new PdfString(prevHighSchool.HighSchoolGrade.ToString());

                    }
                }
            }

            PdfTextField phone = (PdfTextField)(document.AcroForm.Fields["Phone"]);
            phone.Value = new PdfString(this.user.PhoneInfo.PhoneNumber);

            PdfTextField lName = (PdfTextField)(document.AcroForm.Fields["Last Name"]);
            lName.Value = new PdfString(this.user.Name.LName);

            PdfTextField fName = (PdfTextField)(document.AcroForm.Fields["First"]);
            fName.Value = new PdfString(this.user.Name.FName);

            PdfTextField mi = (PdfTextField)(document.AcroForm.Fields["MI"]);
            mi.Value = new PdfString(this.user.Name.MName);

            PdfTextField dob = (PdfTextField)(document.AcroForm.Fields["DOB"]);
            dob.Value = new PdfString(this.user.Birthday.ToString("MM-dd-yyyy"));

            PdfSignatureField parentSignature = (PdfSignatureField)(document.AcroForm.Fields["ParentGuardian Signature"]);
            if (user.Guardians != null)
            {
                try
                {
                    if (user.Guardians[0] != null)
                    {
                        parentSignature.Value = new PdfString(this.user.Guardians[0].Name.FName + " " + this.user.Guardians[0].Name.LName);
                    }
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.Out.Write("No guardian in guardian ArrayList : " + e.Message + " : " + e.StackTrace);
                }
            }
            return writeDocument(document);
        }

        private Byte[] NativeAmericanEducationProgramForm()
        {
            //name of school should never change as all are applying to Igrad. Not captured in web form.
            const string NAME_OF_SCHOOL = "iGRAD";

            // Get the blank form to fill out
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/media/documents/NativeAmericanEducationProgram.pdf");
            PdfDocument document = PdfReader.Open(filePath);

            //set page size
            SetPageSizeA4(document);

            // Set the flag so we can flatten once done.
            document.AcroForm.Elements.SetBoolean("/NeedAppearances", true);

            //check for null object
            if (user.NativeAmericanEducation != null)
            {
                PdfTextField name = (PdfTextField)(document.AcroForm.Fields["Name"]);
                name.Value = new PdfString(user.Name.FName + " " + user.Name.MName + " " + user.Name.LName);

                PdfTextField birthday = (PdfTextField)(document.AcroForm.Fields["Birthday"]);
                birthday.Value = new PdfString(user.Birthday.ToShortDateString());

                PdfTextField grade = (PdfTextField)(document.AcroForm.Fields["Grade"]);
                grade.Value = new PdfString(user.SchoolInfo.CurrentGrade.ToString());

                PdfTextField nameOfSchool = (PdfTextField)(document.AcroForm.Fields["NameOfSchool"]);
                nameOfSchool.Value = new PdfString(NAME_OF_SCHOOL);

                PdfTextField nameOfIndividualWithTribalEnrollment = (PdfTextField)(document.AcroForm.Fields["NameOfIndividualWithTribalEnrollment"]);
                nameOfIndividualWithTribalEnrollment.Value = new PdfString(user.NativeAmericanEducation.NameOfIndividualWithTribalEnrollment);

                PdfCheckBoxField tribalMembershipIsChilds = (PdfCheckBoxField)(document.AcroForm.Fields["TribalMembershipIsChilds"]);
                tribalMembershipIsChilds.Checked = user.NativeAmericanEducation.TribalMembershipIsChilds;

                PdfCheckBoxField tribalMembershipIsChildsParent = (PdfCheckBoxField)(document.AcroForm.Fields["TribalMembershipIsChildsParent"]);
                tribalMembershipIsChildsParent.Checked = user.NativeAmericanEducation.TribalMembershipIsChildsParent;

                PdfCheckBoxField tribalMembershipIsChildsGrandparent = (PdfCheckBoxField)(document.AcroForm.Fields["TribalMembershipIsChildsGrandparent"]);
                tribalMembershipIsChildsGrandparent.Checked = user.NativeAmericanEducation.TribalMembershipIsChildsGrandparent;

                PdfTextField nameOfTribeOrBandOfMembership = (PdfTextField)(document.AcroForm.Fields["NameOfTribeOrBandOfMembership"]);
                nameOfTribeOrBandOfMembership.Value = new PdfString(user.NativeAmericanEducation.NameOfTribeOrBandOfMembership);

                PdfCheckBoxField tribeOrBandIsFederallyRecognized = (PdfCheckBoxField)(document.AcroForm.Fields["TribeOrBandIsFederallyRecognized"]);
                tribeOrBandIsFederallyRecognized.Checked = user.NativeAmericanEducation.TribeOrBandIsFederallyRecognized;

                PdfCheckBoxField tribeOrBandIsStateRecognized = (PdfCheckBoxField)(document.AcroForm.Fields["TribeOrBandIsStateRecognized"]);
                tribeOrBandIsStateRecognized.Checked = user.NativeAmericanEducation.TribeOrBandIsStateRecognized;

                PdfCheckBoxField tribeOrBandIsTerminatedTribe = (PdfCheckBoxField)(document.AcroForm.Fields["TribeOrBandIsTerminatedTribe"]);
                tribeOrBandIsTerminatedTribe.Checked = user.NativeAmericanEducation.TribeOrBandIsTerminatedTribe;

                PdfCheckBoxField tribeOrBandIsOfIndianGroupEducationGrant = (PdfCheckBoxField)(document.AcroForm.Fields["TribeOrBandIsOfIndianGroupEducationGrant"]);
                tribeOrBandIsOfIndianGroupEducationGrant.Checked = user.NativeAmericanEducation.TribeOrBandIsOfIndianGroupEducationGrant;

                PdfTextField membershipOrEnrollmentNumber = (PdfTextField)(document.AcroForm.Fields["MembershipOrEnrollmentNumber"]);
                membershipOrEnrollmentNumber.Value = new PdfString(user.NativeAmericanEducation.MembershipOrEnrollmentNumber);

                PdfTextField descriptionOfEvidenceOfEnrollment = (PdfTextField)(document.AcroForm.Fields["DescriptionOfEvidenceOfEnrollment"]);
                descriptionOfEvidenceOfEnrollment.Value = new PdfString(user.NativeAmericanEducation.DescriptionOfEvidenceOfEnrollment);

                PdfTextField nameOfTribeMaintaningEnrollment = (PdfTextField)(document.AcroForm.Fields["NameOfTribeMaintaningEnrollment"]);
                nameOfTribeMaintaningEnrollment.Value = new PdfString(user.NativeAmericanEducation.NameOfTribeMaintaningEnrollment);

                //check for null address
                if (user.NativeAmericanEducation.AddressOfTribeMaintainingEnrollment != null)
                {
                    PdfTextField addressOfTribeMaintainingEnrollmentStreet = (PdfTextField)(document.AcroForm.Fields["AddressOfTribeMaintainingEnrollmentStreet"]);
                    addressOfTribeMaintainingEnrollmentStreet.Value = new PdfString(user.NativeAmericanEducation.AddressOfTribeMaintainingEnrollment.Street);

                    PdfTextField addressOfTribeMaintainingEnrollmentCity = (PdfTextField)(document.AcroForm.Fields["AddressOfTribeMaintainingEnrollmentCity"]);
                    addressOfTribeMaintainingEnrollmentCity.Value = new PdfString(user.NativeAmericanEducation.AddressOfTribeMaintainingEnrollment.City);

                    PdfTextField addressOfTribeMaintainingEnrollmentState = (PdfTextField)(document.AcroForm.Fields["AddressOfTribeMaintainingEnrollmentState"]);
                    addressOfTribeMaintainingEnrollmentState.Value = new PdfString(user.NativeAmericanEducation.AddressOfTribeMaintainingEnrollment.State);

                    PdfTextField addressOfTribeMaintainingEnrollmentZip = (PdfTextField)(document.AcroForm.Fields["AddressOfTribeMaintainingEnrollmentZip"]);
                    addressOfTribeMaintainingEnrollmentZip.Value = new PdfString(user.NativeAmericanEducation.AddressOfTribeMaintainingEnrollment.Zip.ToString());
                }
            }

            return writeDocument(document);
        }

        private Byte[] HomelessAssistanceForm()
        {
            // Get the blank form to fill out
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/media/documents/HomelessAssistance.pdf");
            PdfDocument document = PdfReader.Open(filePath);

            SetPageSizeA4(document);
            // Set the flag so we can flatten once done.
            document.AcroForm.Elements.SetBoolean("/NeedAppearances", true);

            //TODO Add logic to fill form.

            //FIRST PAGE OF THE HOMELESS ASSISTANCE

            PdfCheckBoxField contactByPhone = (PdfCheckBoxField)(document.AcroForm.Fields["ContactByPhone"]);
            contactByPhone.Checked = user.HomelessAssistance.ContactByPhone;

            if (user.HomelessAssistance.ContactByPhone)
            {
                PdfTextField phoneNumber = (PdfTextField)(document.AcroForm.Fields["PhoneNumber"]);
                phoneNumber.Value = new PdfString(user.PhoneInfo.PhoneNumber.ToString());
            }

            PdfCheckBoxField contactByStudentNote = (PdfCheckBoxField)(document.AcroForm.Fields["ContactByStudentNote"]);
            contactByStudentNote.Checked = user.HomelessAssistance.ContactByStudentNote;

            PdfCheckBoxField contactByEmail = (PdfCheckBoxField)(document.AcroForm.Fields["ContactByEmail"]);
            contactByEmail.Checked = user.HomelessAssistance.ContactByEmail;

            if (user.HomelessAssistance.ContactByEmail)
            {
                PdfTextField email = (PdfTextField)(document.AcroForm.Fields["Email"]);
                email.Value = new PdfString(user.Email.ToString());
            }



            //populate student/sibling fields

            PdfTextField studentName = (PdfTextField)(document.AcroForm.Fields["StudentName"]);
            studentName.Value = new PdfString(user.Name.FName + " " + user.Name.LName);

            PdfTextField studentGrade = (PdfTextField)(document.AcroForm.Fields["StudentGrade"]);
            studentGrade.Value = new PdfString(user.SchoolInfo.CurrentGrade.ToString());


            //FIND the last school attended for student and set value
            for (int i = 0; i < user.SchoolInfo.HighSchoolInformation.Count; i++)
            {
                //if school is last attended for student
                if (user.SchoolInfo.HighSchoolInformation[i].isLastHighSchoolAttended)
                {
                    PdfTextField studentLastSchoolAttended = (PdfTextField)(document.AcroForm.Fields["StudentLastSchoolAttended"]);
                    studentLastSchoolAttended.Value = new PdfString(user.SchoolInfo.HighSchoolInformation[i].HighSchoolName.ToString());
                }
            }


            //Set sibling1 info
            if (user.Siblings.Count > 0)
            {
                if (user.Siblings[0] != null)
                {
                    Sibling sibling = user.Siblings[0];

                    PdfTextField firstSiblingName = (PdfTextField)(document.AcroForm.Fields["SiblingName1"]);
                    firstSiblingName.Value = new PdfString(sibling.FName + " " + sibling.LName);

                    PdfTextField firstSiblingGrade = (PdfTextField)(document.AcroForm.Fields["SiblingGrade1"]);
                    firstSiblingGrade.Value = new PdfString(sibling.Grade.ToString());

                    PdfTextField firstSiblingSchool = (PdfTextField)(document.AcroForm.Fields["SiblingSchool1"]);
                    firstSiblingSchool.Value = new PdfString(sibling.School.ToString());

                }
                if (user.Siblings.Count > 1)
                {
                    Sibling secondSibling = user.Siblings[1];

                    PdfTextField secondSiblingName = (PdfTextField)(document.AcroForm.Fields["SiblingName2"]);
                    secondSiblingName.Value = new PdfString(secondSibling.FName + " " + secondSibling.LName);

                    PdfTextField secondSiblingGrade = (PdfTextField)(document.AcroForm.Fields["SiblingGrade2"]);
                    secondSiblingGrade.Value = new PdfString(secondSibling.Grade.ToString());

                    PdfTextField secondSiblingSchool = (PdfTextField)(document.AcroForm.Fields["SiblingSchool2"]);
                    secondSiblingSchool.Value = new PdfString(secondSibling.School.ToString());
                }
            }

            SetPageSizeA4(document);
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

        private Byte[] IGradOptionalAssistance()
        {
            // Get the blank form to fill out
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/media/documents/IGradOptionalAssistance.pdf");
            PdfDocument document = PdfReader.Open(filePath);

            // Set the flag so we can flatten once done.
            document.AcroForm.Elements.SetBoolean("/NeedAppearances", true);


            PdfTextField name = (PdfTextField)(document.AcroForm.Fields["Name"]);
            name.Value = new PdfString(user.Name.FName + " " + user.Name.MName + " " + user.Name.LName);

            PdfTextField birthday = (PdfTextField)(document.AcroForm.Fields["Birthday"]);
            birthday.Value = new PdfString(user.Birthday.ToShortDateString());

            PdfTextField date = (PdfTextField)(document.AcroForm.Fields["Date"]);
            date.Value = new PdfString(DateTime.Now.ToShortDateString());

            //create a list of Field Names with the boolean value appended so we know what checkboxes to check
            List<string> booleansToCheck = new List<string>();

            //get the properties of the OptionalOpportunities object
            foreach (var prop in user.OptionalOpportunities.GetType().GetProperties())
            {
                //get the type name
                string propType = prop.PropertyType.Name;

                //determine if property is boolean (we want booleans)
                if (propType == "Boolean")
                {
                    //append the property name with the value. i.e, for isHomeless, we would have "isHomelessTrue" as the string to add.
                    string propStrWithValue = prop.Name + prop.GetValue(user.OptionalOpportunities).ToString();
                    booleansToCheck.Add(propStrWithValue);
                }
            }

            //check all the boxes in the form for boolean fields that we found in the OptionalOpportunities object
            foreach (string s in booleansToCheck)
            {
                try
                {
                    PdfCheckBoxField checkBox = (PdfCheckBoxField)(document.AcroForm.Fields[s]);
                    checkBox.Checked = true;
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine("field does not exist: " + e.Message);
                }
            }

            if (user.OptionalOpportunities.StudentIsParenting)
            {
                //TODO FIX TYPO and refactor AGESOFCHILREN on OptionalOpportunities model.
                PdfTextField agesOfChildren = (PdfTextField)(document.AcroForm.Fields["AgesOfChilren"]);
                agesOfChildren.Value = new PdfString(user.OptionalOpportunities.AgesOfChilren);
            }

            if (user.OptionalOpportunities.StudentIsPregnant)
            {
                PdfTextField pregnantDueDate = (PdfTextField)(document.AcroForm.Fields["PregnantStudentDueDate"]);
                pregnantDueDate.Value = new PdfString(user.OptionalOpportunities.PregnantStudentDueDate.Value.ToShortDateString());
            }

            PdfTextField phoneNumber = (PdfTextField)(document.AcroForm.Fields["PhoneNumber"]);
            phoneNumber.Value = new PdfString(user.PhoneInfo.PhoneNumber.ToString());

            PdfTextField email = (PdfTextField)(document.AcroForm.Fields["Email"]);
            email.Value = new PdfString(user.Email);

            PdfTextField howStudentHeardAboutIGrade = (PdfTextField)(document.AcroForm.Fields["HowStudentHeardAboutIGrad"]);
            howStudentHeardAboutIGrade.Value = new PdfString(user.OptionalOpportunities.HowStudentHeardAboutIGrad);

            SetPageSizeA4(document);
            return writeDocument(document);
        }

        private static byte[] writeDocument(PdfDocument document)
        {
            SetFinishedSecuritySettings(document);
            SetAllDocumentFieldsReadOnly(document);
            SetPageSizeA4(document);

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