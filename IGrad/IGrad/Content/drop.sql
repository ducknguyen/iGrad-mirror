ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.SchoolInfoes_SchoolInfo_fieldId]


ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.Retaineds_Retainment_fieldId]


ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.RaceEthnicities_ConsideredRaceAndEthnicity_fieldId]


ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.QualifiedOrEnrolledInPrograms_QualifiedOrEnrolledInProgam_fieldId]


ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.Phones_PhoneInfo_fieldId]


ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.ParentPlans_StudentsParentingPlan_fieldId]


ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.Names_Name_fieldId]


ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.LivesWiths_LivesWith_fieldId]


ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.LanguageHistories_LanguageHisory_fieldId]


ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.Healths_HealthInfo_fieldId]


ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.EmergencyContacts_EmergencyContact_fieldId]


ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.ChildCares_StudentChildCare_fieldId]


ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.BirthPlaceLocations_BirthPlace_fieldId]


ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.Addresses_ResidentAddress_fieldId]


ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.Addresses_MailingAddress_fieldId]


ALTER TABLE [dbo].[Siblings] DROP CONSTRAINT [FK_dbo.Siblings_dbo.User_UserID]


ALTER TABLE [dbo].[SchoolInfoes] DROP CONSTRAINT [FK_dbo.SchoolInfoes_dbo.Violations_PreviousSchoolViolation_fieldId]


ALTER TABLE [dbo].[SchoolInfoes] DROP CONSTRAINT [FK_dbo.SchoolInfoes_dbo.PriorEducations_PriorEducation_fieldId]


ALTER TABLE [dbo].[PreSchools] DROP CONSTRAINT [FK_dbo.PreSchools_dbo.User_UserID]


ALTER TABLE [dbo].[PreSchools] DROP CONSTRAINT [FK_dbo.PreSchools_dbo.Addresses_Address_fieldId]


ALTER TABLE [dbo].[HighSchoolInfoes] DROP CONSTRAINT [FK_dbo.HighSchoolInfoes_dbo.SchoolInfoes_SchoolInfo_fieldId]


ALTER TABLE [dbo].[Guardians] DROP CONSTRAINT [FK_dbo.Guardians_dbo.User_UserID]


ALTER TABLE [dbo].[Guardians] DROP CONSTRAINT [FK_dbo.Guardians_dbo.Phones_Phone_fieldId]


ALTER TABLE [dbo].[Guardians] DROP CONSTRAINT [FK_dbo.Guardians_dbo.Names_Name_fieldId]


ALTER TABLE [dbo].[Guardians] DROP CONSTRAINT [FK_dbo.Guardians_dbo.Addresses_Address_fieldId]


ALTER TABLE [dbo].[EmergencyContacts] DROP CONSTRAINT [FK_dbo.EmergencyContacts_dbo.Phones_PhoneNumber_fieldId]


ALTER TABLE [dbo].[EmergencyContacts] DROP CONSTRAINT [FK_dbo.EmergencyContacts_dbo.Names_Name_fieldId]


ALTER TABLE [dbo].[ChildCares] DROP CONSTRAINT [FK_dbo.ChildCares_dbo.Addresses_ProviderAddress_fieldId]


ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]


ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]


ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]


ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]


/****** Object:  Table [dbo].[Violations]    Script Date: 3/4/2018 2:06:08 PM ******/
DROP TABLE [dbo].[Violations]


/****** Object:  Table [dbo].[User]    Script Date: 3/4/2018 2:06:08 PM ******/
DROP TABLE [dbo].[User]


/****** Object:  Table [dbo].[Siblings]    Script Date: 3/4/2018 2:06:08 PM ******/
DROP TABLE [dbo].[Siblings]


/****** Object:  Table [dbo].[SchoolInfoes]    Script Date: 3/4/2018 2:06:08 PM ******/
DROP TABLE [dbo].[SchoolInfoes]


/****** Object:  Table [dbo].[Retaineds]    Script Date: 3/4/2018 2:06:08 PM ******/
DROP TABLE [dbo].[Retaineds]


/****** Object:  Table [dbo].[RaceEthnicities]    Script Date: 3/4/2018 2:06:08 PM ******/
DROP TABLE [dbo].[RaceEthnicities]


/****** Object:  Table [dbo].[QualifiedOrEnrolledInPrograms]    Script Date: 3/4/2018 2:06:08 PM ******/
DROP TABLE [dbo].[QualifiedOrEnrolledInPrograms]


/****** Object:  Table [dbo].[PriorEducations]    Script Date: 3/4/2018 2:06:08 PM ******/
DROP TABLE [dbo].[PriorEducations]


/****** Object:  Table [dbo].[PreSchools]    Script Date: 3/4/2018 2:06:08 PM ******/
DROP TABLE [dbo].[PreSchools]


/****** Object:  Table [dbo].[Phones]    Script Date: 3/4/2018 2:06:08 PM ******/
DROP TABLE [dbo].[Phones]


/****** Object:  Table [dbo].[ParentPlans]    Script Date: 3/4/2018 2:06:08 PM ******/
DROP TABLE [dbo].[ParentPlans]


/****** Object:  Table [dbo].[Names]    Script Date: 3/4/2018 2:06:08 PM ******/
DROP TABLE [dbo].[Names]


/****** Object:  Table [dbo].[LivesWiths]    Script Date: 3/4/2018 2:06:08 PM ******/
DROP TABLE [dbo].[LivesWiths]


/****** Object:  Table [dbo].[LanguageHistories]    Script Date: 3/4/2018 2:06:08 PM ******/
DROP TABLE [dbo].[LanguageHistories]


/****** Object:  Table [dbo].[HighSchoolInfoes]    Script Date: 3/4/2018 2:06:08 PM ******/
DROP TABLE [dbo].[HighSchoolInfoes]


/****** Object:  Table [dbo].[Healths]    Script Date: 3/4/2018 2:06:08 PM ******/
DROP TABLE [dbo].[Healths]


/****** Object:  Table [dbo].[Guardians]    Script Date: 3/4/2018 2:06:08 PM ******/
DROP TABLE [dbo].[Guardians]


/****** Object:  Table [dbo].[EmergencyContacts]    Script Date: 3/4/2018 2:06:08 PM ******/
DROP TABLE [dbo].[EmergencyContacts]


/****** Object:  Table [dbo].[ChildCares]    Script Date: 3/4/2018 2:06:08 PM ******/
DROP TABLE [dbo].[ChildCares]


/****** Object:  Table [dbo].[BirthPlaceLocations]    Script Date: 3/4/2018 2:06:08 PM ******/
DROP TABLE [dbo].[BirthPlaceLocations]


/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 3/4/2018 2:06:08 PM ******/
DROP TABLE [dbo].[AspNetUsers]


/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 3/4/2018 2:06:08 PM ******/
DROP TABLE [dbo].[AspNetUserRoles]


/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 3/4/2018 2:06:08 PM ******/
DROP TABLE [dbo].[AspNetUserLogins]


/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 3/4/2018 2:06:08 PM ******/
DROP TABLE [dbo].[AspNetUserClaims]


/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 3/4/2018 2:06:08 PM ******/
DROP TABLE [dbo].[AspNetRoles]


/****** Object:  Table [dbo].[Addresses]    Script Date: 3/4/2018 2:06:08 PM ******/
DROP TABLE [dbo].[Addresses]


/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 3/4/2018 2:06:08 PM ******/
DROP TABLE [dbo].[__MigrationHistory]


