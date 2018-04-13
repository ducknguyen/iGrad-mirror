USE [iGrad]
GO

ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.SchoolInfoes_SchoolInfo_fieldId]
GO

ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.Retaineds_Retainment_fieldId]
GO

ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.RaceEthnicities_ConsideredRaceAndEthnicity_fieldId]
GO

ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.QualifiedOrEnrolledInPrograms_QualifiedOrEnrolledInProgam_fieldId]
GO

ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.Phones_PhoneInfo_fieldId]
GO

ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.ParentPlans_StudentsParentingPlan_fieldId]
GO

ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.Names_Name_fieldId]
GO

ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.LivesWiths_LivesWith_fieldId]
GO

ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.LifeEvents_LifeEvent_fieldId]
GO

ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.LanguageHistories_LanguageHisory_fieldId]
GO

ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.Healths_HealthInfo_fieldId]
GO

ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.ChildCares_StudentChildCare_fieldId]
GO

ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.Celebrates_Celebrate_fieldId]
GO

ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.BirthPlaceLocations_BirthPlace_fieldId]
GO

ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.Addresses_ResidentAddress_fieldId]
GO

ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.Addresses_MailingAddress_fieldId]
GO

ALTER TABLE [dbo].[Siblings] DROP CONSTRAINT [FK_dbo.Siblings_dbo.User_UserID]
GO

ALTER TABLE [dbo].[SchoolInfoes] DROP CONSTRAINT [FK_dbo.SchoolInfoes_dbo.Violations_PreviousSchoolViolation_fieldId]
GO

ALTER TABLE [dbo].[SchoolInfoes] DROP CONSTRAINT [FK_dbo.SchoolInfoes_dbo.PriorEducations_PriorEducation_fieldId]
GO

ALTER TABLE [dbo].[PreSchools] DROP CONSTRAINT [FK_dbo.PreSchools_dbo.User_UserID]
GO

ALTER TABLE [dbo].[PreSchools] DROP CONSTRAINT [FK_dbo.PreSchools_dbo.Addresses_Address_fieldId]
GO

ALTER TABLE [dbo].[IncomeTables] DROP CONSTRAINT [FK_dbo.IncomeTables_dbo.FamilyIncomes_FamilyIncome_id]
GO

ALTER TABLE [dbo].[HighSchoolInfoes] DROP CONSTRAINT [FK_dbo.HighSchoolInfoes_dbo.SchoolInfoes_SchoolInfo_fieldId]
GO

ALTER TABLE [dbo].[Guardians] DROP CONSTRAINT [FK_dbo.Guardians_dbo.User_UserID]
GO

ALTER TABLE [dbo].[Guardians] DROP CONSTRAINT [FK_dbo.Guardians_dbo.Phones_Phone_fieldId]
GO

ALTER TABLE [dbo].[Guardians] DROP CONSTRAINT [FK_dbo.Guardians_dbo.Names_Name_fieldId]
GO

ALTER TABLE [dbo].[Guardians] DROP CONSTRAINT [FK_dbo.Guardians_dbo.Addresses_Address_fieldId]
GO

ALTER TABLE [dbo].[EmergencyContacts] DROP CONSTRAINT [FK_dbo.EmergencyContacts_dbo.User_UserID]
GO

ALTER TABLE [dbo].[EmergencyContacts] DROP CONSTRAINT [FK_dbo.EmergencyContacts_dbo.Phones_PhoneNumber_fieldId]
GO

ALTER TABLE [dbo].[EmergencyContacts] DROP CONSTRAINT [FK_dbo.EmergencyContacts_dbo.Names_Name_fieldId]
GO

ALTER TABLE [dbo].[ChildCares] DROP CONSTRAINT [FK_dbo.ChildCares_dbo.Addresses_ProviderAddress_fieldId]
GO

ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO

ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO

ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO

ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO

ALTER TABLE [dbo].[NativeAmericanEducations] DROP CONSTRAINT [FK_dbo.NativeAmericanEducations_dbo.Addresses_AddressOfTribeMaintainingEnrollment_fieldId]
GO

ALTER TABLE [dbo].[NativeAmericanEducations] DROP CONSTRAINT [PK_dbo.NativeAmericanEducations]
GO



/****** Object:  Table [dbo].[Violations]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[Violations]
GO

/****** Object:  Table [dbo].[User]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[User]
GO

/****** Object:  Table [dbo].[Siblings]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[Siblings]
GO

/****** Object:  Table [dbo].[SchoolInfoes]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[SchoolInfoes]
GO

/****** Object:  Table [dbo].[Retaineds]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[Retaineds]
GO

/****** Object:  Table [dbo].[RaceEthnicities]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[RaceEthnicities]
GO

/****** Object:  Table [dbo].[QualifiedOrEnrolledInPrograms]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[QualifiedOrEnrolledInPrograms]
GO

/****** Object:  Table [dbo].[PriorEducations]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[PriorEducations]
GO

/****** Object:  Table [dbo].[PreSchools]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[PreSchools]
GO

/****** Object:  Table [dbo].[Phones]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[Phones]
GO

/****** Object:  Table [dbo].[ParentPlans]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[ParentPlans]
GO

/****** Object:  Table [dbo].[Names]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[Names]
GO

/****** Object:  Table [dbo].[LivesWiths]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[LivesWiths]
GO

/****** Object:  Table [dbo].[LifeEvents]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[LifeEvents]
GO

/****** Object:  Table [dbo].[LanguageHistories]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[LanguageHistories]
GO

/****** Object:  Table [dbo].[IncomeTables]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[IncomeTables]
GO

/****** Object:  Table [dbo].[HighSchoolInfoes]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[HighSchoolInfoes]
GO

/****** Object:  Table [dbo].[Healths]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[Healths]
GO

/****** Object:  Table [dbo].[Guardians]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[Guardians]
GO

/****** Object:  Table [dbo].[FamilyIncomes]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[FamilyIncomes]
GO

/****** Object:  Table [dbo].[EmergencyContacts]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[EmergencyContacts]
GO

/****** Object:  Table [dbo].[ChildCares]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[ChildCares]
GO

/****** Object:  Table [dbo].[Celebrates]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[Celebrates]
GO

/****** Object:  Table [dbo].[BirthPlaceLocations]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[BirthPlaceLocations]
GO

/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[AspNetUsers]
GO

/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[AspNetUserRoles]
GO

/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[AspNetUserLogins]
GO

/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[AspNetUserClaims]
GO

/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[AspNetRoles]
GO

/****** Object:  Table [dbo].[Addresses]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[Addresses]
GO

/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 3/18/2018 6:30:53 PM ******/
DROP TABLE [dbo].[__MigrationHistory]
GO

DROP TABLE [dbo].[NativeAmericanEducations]
GO
