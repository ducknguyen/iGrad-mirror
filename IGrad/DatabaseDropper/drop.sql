USE [iGrad]
GO

-- DROP KEYS
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'User')
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.SchoolInfoes_SchoolInfo_fieldId]
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.Retaineds_Retainment_fieldId]
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.RaceEthnicities_ConsideredRaceAndEthnicity_fieldId]
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.QualifiedOrEnrolledInPrograms_QualifiedOrEnrolledInProgam_fieldId]
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.HomelessAssistancePreferences_HomelessAssistance_fieldId]
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.OptionalAssistances_OptionalOpportunities_fieldId]
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.Phones_PhoneInfo_fieldId]
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.ParentPlans_StudentsParentingPlan_fieldId]
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.Names_Name_fieldId]
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.LivesWiths_LivesWith_fieldId]
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.LifeEvents_LifeEvent_fieldId]
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.LanguageHistories_LanguageHisory_fieldId]
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.Healths_HealthInfo_fieldId]
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.ChildCares_StudentChildCare_fieldId]
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.Celebrates_Celebrate_fieldId]
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.BirthPlaceLocations_BirthPlace_fieldId]
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.Addresses_ResidentAddress_fieldId]
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.Addresses_MailingAddress_fieldId]
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.Addresses_SecondaryHouseholdAddress_fieldId]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Siblings')
ALTER TABLE [dbo].[Siblings] DROP CONSTRAINT [FK_dbo.Siblings_dbo.User_UserID]
GO


IF EXISTS (SELECT * FROM sys.tables WHERE name = 'SchoolInfoes')
ALTER TABLE [dbo].[SchoolInfoes] DROP CONSTRAINT [FK_dbo.SchoolInfoes_dbo.Violations_PreviousSchoolViolation_fieldId]
ALTER TABLE [dbo].[SchoolInfoes] DROP CONSTRAINT [FK_dbo.SchoolInfoes_dbo.PriorEducations_PriorEducation_fieldId]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'PreSchools')
ALTER TABLE [dbo].[PreSchools] DROP CONSTRAINT [FK_dbo.PreSchools_dbo.User_UserID]
ALTER TABLE [dbo].[PreSchools] DROP CONSTRAINT [FK_dbo.PreSchools_dbo.Addresses_Address_fieldId]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'IncomeTables')
ALTER TABLE [dbo].[IncomeTables] DROP CONSTRAINT [FK_dbo.IncomeTables_dbo.FamilyIncomes_FamilyIncome_id]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'HighSchoolInfoes')
ALTER TABLE [dbo].[HighSchoolInfoes] DROP CONSTRAINT [FK_dbo.HighSchoolInfoes_dbo.SchoolInfoes_SchoolInfo_fieldId]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Guardians')
ALTER TABLE [dbo].[Guardians] DROP CONSTRAINT [FK_dbo.Guardians_dbo.User_UserID]
ALTER TABLE [dbo].[Guardians] DROP CONSTRAINT [FK_dbo.Guardians_dbo.Phones_PhoneOne_fieldId]
ALTER TABLE [dbo].[Guardians] DROP CONSTRAINT [FK_dbo.Guardians_dbo.Phones_PhoneTwo_fieldId]
ALTER TABLE [dbo].[Guardians] DROP CONSTRAINT [FK_dbo.Guardians_dbo.Names_Name_fieldId]
ALTER TABLE [dbo].[Guardians] DROP CONSTRAINT [FK_dbo.Guardians_dbo.Addresses_Address_fieldId]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'EmergencyContacts')
ALTER TABLE [dbo].[EmergencyContacts] DROP CONSTRAINT [FK_dbo.EmergencyContacts_dbo.User_UserID]
ALTER TABLE [dbo].[EmergencyContacts] DROP CONSTRAINT [FK_dbo.EmergencyContacts_dbo.Phones_PhoneOne_fieldId]
ALTER TABLE [dbo].[EmergencyContacts] DROP CONSTRAINT [FK_dbo.EmergencyContacts_dbo.Phones_PhoneTwo_fieldId]
ALTER TABLE [dbo].[EmergencyContacts] DROP CONSTRAINT [FK_dbo.EmergencyContacts_dbo.Names_Name_fieldId]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'ChildCares')
ALTER TABLE [dbo].[ChildCares] DROP CONSTRAINT [FK_dbo.ChildCares_dbo.Addresses_ProviderAddress_fieldId]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetUserRoles')
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetUserLogins')
ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetUserClaims')
ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'MillitaryInfoes')
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.MillitaryInfoes_MillitaryInfo_fieldId]
GO


IF EXISTS (SELECT * FROM sys.tables WHERE name = 'NativeAmericanEducations')
ALTER TABLE [dbo].[NativeAmericanEducations] DROP CONSTRAINT [FK_dbo.NativeAmericanEducations_dbo.Addresses_AddressOfTribeMaintainingEnrollment_fieldId]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Violations')
DROP TABLE [dbo].[Violations]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Retaineds')
DROP TABLE [dbo].[Retaineds]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'RaceEthnicities')
DROP TABLE [dbo].[RaceEthnicities]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'QualifiedOrEnrolledInPrograms')
DROP TABLE [dbo].[QualifiedOrEnrolledInPrograms]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'PriorEducations')
DROP TABLE [dbo].[PriorEducations]
GO


IF EXISTS (SELECT * FROM sys.tables WHERE name = 'PreSchools')
DROP TABLE [dbo].[PreSchools]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Phones')
DROP TABLE [dbo].[Phones]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'ParentPlans')
DROP TABLE [dbo].[ParentPlans]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Names')
DROP TABLE [dbo].[Names]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'LivesWiths')
DROP TABLE [dbo].[LivesWiths]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'LifeEvents')
DROP TABLE [dbo].[LifeEvents]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'LanguageHistories')
DROP TABLE [dbo].[LanguageHistories]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Healths')
DROP TABLE [dbo].[Healths]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'FamilyIncomes')
DROP TABLE [dbo].[FamilyIncomes]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'EmergencyContacts')
DROP TABLE [dbo].[EmergencyContacts]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Celebrates')
DROP TABLE [dbo].[Celebrates]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'BirthPlaceLocations')
DROP TABLE [dbo].[BirthPlaceLocations]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetUsers')
DROP TABLE [dbo].[AspNetUsers]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetRoles')
DROP TABLE [dbo].[AspNetRoles]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Addresses')
DROP TABLE [dbo].[Addresses]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = '__MigrationHistory')
DROP TABLE [dbo].[__MigrationHistory]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'HomelessAssistancePreferences')
DROP TABLE [dbo].[HomelessAssistancePreferences]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'OptionalAssistances')
DROP TABLE [dbo].[OptionalAssistances]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'User')
DROP TABLE [dbo].[User]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Siblings')
DROP TABLE [dbo].[Siblings]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'SchoolInfoes')
DROP TABLE [dbo].[SchoolInfoes]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'PreSchools')
DROP TABLE [dbo].[PreSchools]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'IncomeTables')
DROP TABLE IncomeTables
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'HighSchoolInfoes')
DROP TABLE [dbo].[HighSchoolInfoes]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Guardians')
DROP TABLE [dbo].[Guardians]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'EmergencyContacts')
DROP TABLE [dbo].[EmergencyContacts]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'ChildCares')
DROP TABLE [dbo].[ChildCares]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetUserRoles')
DROP TABLE [dbo].[AspNetUserRoles]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetUserClaims')
DROP TABLE [dbo].[AspNetUserClaims]

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetUserLogins')
DROP TABLE [dbo].[AspNetUserLogins]
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'NativeAmericanEducations')
DROP TABLE [dbo].[NativeAmericanEducations]
GO