namespace OnlineQuiz.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdvancedExamResultDetail",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        AdvancedExamResultID = c.Guid(),
                        QuestionID = c.Guid(),
                        Answer = c.String(maxLength: 5, unicode: false),
                        ResultAswer = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AdvancedExamResult", t => t.AdvancedExamResultID, cascadeDelete: true)
                .ForeignKey("dbo.Question", t => t.QuestionID)
                .Index(t => t.AdvancedExamResultID)
                .Index(t => t.QuestionID);
            
            CreateTable(
                "dbo.AdvancedExamResult",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ExamPeriodID = c.Guid(),
                        AdvancedModuleRegistrationID = c.Guid(),
                        ExaminationQuestionID = c.Guid(),
                        StartEndTimeID = c.Guid(),
                        ExamineeID = c.Guid(),
                        IDxaminee = c.Guid(),
                        ExamCode = c.Int(),
                        Duration = c.Int(),
                        TrueQuestion = c.Int(),
                        Point = c.Double(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AdvancedModuleRegistration", t => t.AdvancedModuleRegistrationID, cascadeDelete: true)
                .ForeignKey("dbo.Examinee", t => t.ExamineeID)
                .ForeignKey("dbo.ExaminationQuestion", t => t.ExaminationQuestionID)
                .ForeignKey("dbo.ExamPeriod", t => t.ExamPeriodID)
                .ForeignKey("dbo.StartEndTime", t => t.StartEndTimeID)
                .Index(t => t.ExamPeriodID)
                .Index(t => t.AdvancedModuleRegistrationID)
                .Index(t => t.ExaminationQuestionID)
                .Index(t => t.StartEndTimeID)
                .Index(t => t.ExamineeID);
            
            CreateTable(
                "dbo.AdvancedModuleRegistration",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        RegistrationID = c.Guid(),
                        QuestionModuleID = c.Guid(),
                        IDExaminee = c.String(maxLength: 50, unicode: false),
                        Password = c.String(maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.QuestionModule", t => t.QuestionModuleID)
                .ForeignKey("dbo.Registration", t => t.RegistrationID, cascadeDelete: true)
                .Index(t => t.RegistrationID)
                .Index(t => t.QuestionModuleID);
            
            CreateTable(
                "dbo.ExamScheduleAdvanced",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        AdvancedModuleRegistrationID = c.Guid(),
                        ExamPeriodID = c.Guid(),
                        ExaminationDate = c.DateTime(storeType: "date"),
                        StartEndTimeID = c.Guid(),
                        ExaminationRoomID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ExaminationRoom", t => t.ExaminationRoomID)
                .ForeignKey("dbo.ExamPeriod", t => t.ExamPeriodID)
                .ForeignKey("dbo.StartEndTime", t => t.StartEndTimeID)
                .ForeignKey("dbo.AdvancedModuleRegistration", t => t.AdvancedModuleRegistrationID, cascadeDelete: true)
                .Index(t => t.AdvancedModuleRegistrationID)
                .Index(t => t.ExamPeriodID)
                .Index(t => t.StartEndTimeID)
                .Index(t => t.ExaminationRoomID);
            
            CreateTable(
                "dbo.ExaminationRoom",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(maxLength: 255),
                        Quantity = c.Int(),
                        Remark = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ExamScheduleBasic",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ExamPeriodID = c.Guid(),
                        ExaminationDate = c.DateTime(storeType: "date"),
                        StartEndTimeID = c.Guid(),
                        ExaminationRoomID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ExamPeriod", t => t.ExamPeriodID, cascadeDelete: true)
                .ForeignKey("dbo.StartEndTime", t => t.StartEndTimeID, cascadeDelete: true)
                .ForeignKey("dbo.ExaminationRoom", t => t.ExaminationRoomID, cascadeDelete: true)
                .Index(t => t.ExamPeriodID)
                .Index(t => t.StartEndTimeID)
                .Index(t => t.ExaminationRoomID);
            
            CreateTable(
                "dbo.ExamineeExamScheduleBasic",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ExamScheduleBasicID = c.Guid(),
                        ExamineeID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Examinee", t => t.ExamineeID, cascadeDelete: true)
                .ForeignKey("dbo.ExamScheduleBasic", t => t.ExamScheduleBasicID, cascadeDelete: true)
                .Index(t => t.ExamScheduleBasicID)
                .Index(t => t.ExamineeID);
            
            CreateTable(
                "dbo.Examinee",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        LastName = c.String(maxLength: 50),
                        FirstName = c.String(maxLength: 50),
                        FullName = c.String(maxLength: 100),
                        DateOfBirth = c.DateTime(storeType: "date"),
                        Gender = c.Boolean(),
                        IdentityCard = c.String(maxLength: 15, unicode: false),
                        Mobile = c.String(maxLength: 15, unicode: false),
                        Email = c.String(maxLength: 255, unicode: false),
                        Remark = c.String(maxLength: 255),
                        Status = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BasicExamResult",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ExamPeriodID = c.Guid(),
                        RegistrationID = c.Guid(),
                        ExaminationQuestionID = c.Guid(),
                        StartEndTimeID = c.Guid(),
                        ExamineeID = c.Guid(),
                        IDxaminee = c.Guid(),
                        ExamCode = c.Int(),
                        Duration = c.Int(),
                        TrueQuestion = c.Int(),
                        Point = c.Double(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ExaminationQuestion", t => t.ExaminationQuestionID)
                .ForeignKey("dbo.Registration", t => t.RegistrationID, cascadeDelete: true)
                .ForeignKey("dbo.ExamPeriod", t => t.ExamPeriodID)
                .ForeignKey("dbo.Examinee", t => t.ExamineeID)
                .ForeignKey("dbo.StartEndTime", t => t.StartEndTimeID)
                .Index(t => t.ExamPeriodID)
                .Index(t => t.RegistrationID)
                .Index(t => t.ExaminationQuestionID)
                .Index(t => t.StartEndTimeID)
                .Index(t => t.ExamineeID);
            
            CreateTable(
                "dbo.BasicExamResultDetail",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        BasicExamResultID = c.Guid(),
                        QuesionID = c.Guid(),
                        Answer = c.String(maxLength: 5, unicode: false),
                        ResultAswer = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Question", t => t.QuesionID)
                .ForeignKey("dbo.BasicExamResult", t => t.BasicExamResultID, cascadeDelete: true)
                .Index(t => t.BasicExamResultID)
                .Index(t => t.QuesionID);
            
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        QuestionModuleID = c.Guid(),
                        QuestionClassificationID = c.Guid(),
                        QuestionContent = c.String(maxLength: 450),
                        AAnswer = c.String(maxLength: 255),
                        BAnswer = c.String(maxLength: 255),
                        CAnswer = c.String(maxLength: 255),
                        DAnswer = c.String(maxLength: 255),
                        Answer = c.String(maxLength: 1, unicode: false),
                        ResultAnswer = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.QuestionModule", t => t.QuestionModuleID, cascadeDelete: true)
                .ForeignKey("dbo.QuestionClassification", t => t.QuestionClassificationID, cascadeDelete: true)
                .Index(t => t.QuestionModuleID)
                .Index(t => t.QuestionClassificationID);
            
            CreateTable(
                "dbo.ExaminationQuestion",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ExaminationID = c.Guid(),
                        ExamCode = c.Int(),
                        QuestionID = c.Guid(),
                        QuestionContent = c.String(maxLength: 450),
                        AAnswer = c.String(maxLength: 255),
                        BAnswer = c.String(maxLength: 255),
                        CAnswer = c.String(maxLength: 255),
                        DAnswer = c.String(maxLength: 255),
                        Answer = c.String(maxLength: 1, unicode: false),
                        ResultAnswer = c.String(maxLength: 255),
                        Status = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Examination", t => t.ExaminationID, cascadeDelete: true)
                .ForeignKey("dbo.Question", t => t.QuestionID, cascadeDelete: true)
                .Index(t => t.ExaminationID)
                .Index(t => t.QuestionID);
            
            CreateTable(
                "dbo.Examination",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ExamPeriodID = c.Guid(),
                        Duration = c.Int(),
                        QuestionNumber = c.Int(),
                        TestNumber = c.Int(),
                        ExellentNumber = c.Int(),
                        VeryGoodNumber = c.Int(),
                        GoodNumber = c.Int(),
                        AverageNumber = c.Int(),
                        Remark = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ExamPeriod", t => t.ExamPeriodID, cascadeDelete: true)
                .Index(t => t.ExamPeriodID);
            
            CreateTable(
                "dbo.ExaminationModule",
                c => new
                    {
                        ExaminationID = c.Guid(nullable: false),
                        QuestionModuleID = c.Guid(nullable: false),
                        ExellentNumber = c.Int(),
                        VeryGoodNumber = c.Int(),
                        GoodNumber = c.Int(),
                        AverageNumber = c.Int(),
                    })
                .PrimaryKey(t => new { t.ExaminationID, t.QuestionModuleID })
                .ForeignKey("dbo.Examination", t => t.ExaminationID, cascadeDelete: true)
                .ForeignKey("dbo.QuestionModule", t => t.QuestionModuleID, cascadeDelete: true)
                .Index(t => t.ExaminationID)
                .Index(t => t.QuestionModuleID);
            
            CreateTable(
                "dbo.QuestionModule",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        InformationTechnologySkillID = c.Guid(),
                        IDQuestionModule = c.String(maxLength: 50, unicode: false),
                        Name = c.String(maxLength: 255),
                        Quantity = c.Int(),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.InformationTechnologySkill", t => t.InformationTechnologySkillID, cascadeDelete: true)
                .Index(t => t.InformationTechnologySkillID);
            
            CreateTable(
                "dbo.InformationTechnologySkill",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ExamineeInformationTechnologySkill",
                c => new
                    {
                        ExamineeID = c.Guid(nullable: false),
                        InformationTechnologySkillID = c.Guid(nullable: false),
                        DateOfCertification = c.DateTime(storeType: "date"),
                        Remark = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => new { t.ExamineeID, t.InformationTechnologySkillID })
                .ForeignKey("dbo.Examinee", t => t.ExamineeID, cascadeDelete: true)
                .ForeignKey("dbo.InformationTechnologySkill", t => t.InformationTechnologySkillID, cascadeDelete: true)
                .Index(t => t.ExamineeID)
                .Index(t => t.InformationTechnologySkillID);
            
            CreateTable(
                "dbo.Registration",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ExamineeID = c.Guid(),
                        InformationTechnologySkillID = c.Guid(),
                        ExamPeriodID = c.Guid(),
                        RegistrationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ExamPeriod", t => t.ExamPeriodID, cascadeDelete: true)
                .ForeignKey("dbo.InformationTechnologySkill", t => t.InformationTechnologySkillID, cascadeDelete: true)
                .ForeignKey("dbo.Examinee", t => t.ExamineeID, cascadeDelete: true)
                .Index(t => t.ExamineeID)
                .Index(t => t.InformationTechnologySkillID)
                .Index(t => t.ExamPeriodID);
            
            CreateTable(
                "dbo.ExamPeriod",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(maxLength: 255),
                        StartDate = c.DateTime(storeType: "date"),
                        EndDate = c.DateTime(storeType: "date"),
                        Remark = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.IDExamineeRegistration",
                c => new
                    {
                        RegistrationID = c.Guid(nullable: false),
                        IDExaminee = c.String(nullable: false, maxLength: 50, unicode: false),
                        Password = c.String(maxLength: 255, unicode: false),
                        Status = c.Boolean(),
                    })
                .PrimaryKey(t => new { t.RegistrationID, t.IDExaminee })
                .ForeignKey("dbo.Registration", t => t.RegistrationID, cascadeDelete: true)
                .Index(t => t.RegistrationID);
            
            CreateTable(
                "dbo.QuestionClassification",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StartEndTime",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        TimePeriod = c.String(maxLength: 100, unicode: false),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ExamineeExamScheduleAdvanced",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ExamScheduleAdvancedID = c.Guid(),
                        ExamineeID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Examinee", t => t.ExamineeID)
                .ForeignKey("dbo.ExamScheduleAdvanced", t => t.ExamScheduleAdvancedID, cascadeDelete: true)
                .Index(t => t.ExamScheduleAdvancedID)
                .Index(t => t.ExamineeID);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        IDStudent = c.Int(),
                        ExamineeID = c.Guid(),
                        MajorClassID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MajorClass", t => t.MajorClassID, cascadeDelete: true)
                .ForeignKey("dbo.Examinee", t => t.ExamineeID, cascadeDelete: true)
                .Index(t => t.ExamineeID)
                .Index(t => t.MajorClassID);
            
            CreateTable(
                "dbo.MajorClass",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(maxLength: 255, unicode: false),
                        Course = c.String(maxLength: 100, unicode: false),
                        Quantity = c.Int(),
                        FacultyID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Faculty", t => t.FacultyID, cascadeDelete: true)
                .Index(t => t.FacultyID);
            
            CreateTable(
                "dbo.Faculty",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LocationExam",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(maxLength: 255),
                        State = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Manager",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        UserName = c.String(maxLength: 100, unicode: false),
                        Password = c.String(maxLength: 255, unicode: false),
                        LastName = c.String(maxLength: 100),
                        FirstName = c.String(maxLength: 100),
                        Mobile = c.String(maxLength: 20, unicode: false),
                        Email = c.String(maxLength: 100, unicode: false),
                        Role = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Note",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(maxLength: 255),
                        State = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PracticeExamQuestions",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(maxLength: 100),
                        Status = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExamScheduleAdvanced", "AdvancedModuleRegistrationID", "dbo.AdvancedModuleRegistration");
            DropForeignKey("dbo.ExamineeExamScheduleAdvanced", "ExamScheduleAdvancedID", "dbo.ExamScheduleAdvanced");
            DropForeignKey("dbo.ExamScheduleBasic", "ExaminationRoomID", "dbo.ExaminationRoom");
            DropForeignKey("dbo.ExamineeExamScheduleBasic", "ExamScheduleBasicID", "dbo.ExamScheduleBasic");
            DropForeignKey("dbo.Student", "ExamineeID", "dbo.Examinee");
            DropForeignKey("dbo.Student", "MajorClassID", "dbo.MajorClass");
            DropForeignKey("dbo.MajorClass", "FacultyID", "dbo.Faculty");
            DropForeignKey("dbo.Registration", "ExamineeID", "dbo.Examinee");
            DropForeignKey("dbo.ExamineeExamScheduleBasic", "ExamineeID", "dbo.Examinee");
            DropForeignKey("dbo.ExamineeExamScheduleAdvanced", "ExamineeID", "dbo.Examinee");
            DropForeignKey("dbo.ExamScheduleBasic", "StartEndTimeID", "dbo.StartEndTime");
            DropForeignKey("dbo.ExamScheduleAdvanced", "StartEndTimeID", "dbo.StartEndTime");
            DropForeignKey("dbo.BasicExamResult", "StartEndTimeID", "dbo.StartEndTime");
            DropForeignKey("dbo.AdvancedExamResult", "StartEndTimeID", "dbo.StartEndTime");
            DropForeignKey("dbo.BasicExamResult", "ExamineeID", "dbo.Examinee");
            DropForeignKey("dbo.BasicExamResultDetail", "BasicExamResultID", "dbo.BasicExamResult");
            DropForeignKey("dbo.Question", "QuestionClassificationID", "dbo.QuestionClassification");
            DropForeignKey("dbo.ExaminationQuestion", "QuestionID", "dbo.Question");
            DropForeignKey("dbo.ExaminationQuestion", "ExaminationID", "dbo.Examination");
            DropForeignKey("dbo.Question", "QuestionModuleID", "dbo.QuestionModule");
            DropForeignKey("dbo.Registration", "InformationTechnologySkillID", "dbo.InformationTechnologySkill");
            DropForeignKey("dbo.IDExamineeRegistration", "RegistrationID", "dbo.Registration");
            DropForeignKey("dbo.Registration", "ExamPeriodID", "dbo.ExamPeriod");
            DropForeignKey("dbo.ExamScheduleBasic", "ExamPeriodID", "dbo.ExamPeriod");
            DropForeignKey("dbo.ExamScheduleAdvanced", "ExamPeriodID", "dbo.ExamPeriod");
            DropForeignKey("dbo.Examination", "ExamPeriodID", "dbo.ExamPeriod");
            DropForeignKey("dbo.BasicExamResult", "ExamPeriodID", "dbo.ExamPeriod");
            DropForeignKey("dbo.AdvancedExamResult", "ExamPeriodID", "dbo.ExamPeriod");
            DropForeignKey("dbo.BasicExamResult", "RegistrationID", "dbo.Registration");
            DropForeignKey("dbo.AdvancedModuleRegistration", "RegistrationID", "dbo.Registration");
            DropForeignKey("dbo.QuestionModule", "InformationTechnologySkillID", "dbo.InformationTechnologySkill");
            DropForeignKey("dbo.ExamineeInformationTechnologySkill", "InformationTechnologySkillID", "dbo.InformationTechnologySkill");
            DropForeignKey("dbo.ExamineeInformationTechnologySkill", "ExamineeID", "dbo.Examinee");
            DropForeignKey("dbo.ExaminationModule", "QuestionModuleID", "dbo.QuestionModule");
            DropForeignKey("dbo.AdvancedModuleRegistration", "QuestionModuleID", "dbo.QuestionModule");
            DropForeignKey("dbo.ExaminationModule", "ExaminationID", "dbo.Examination");
            DropForeignKey("dbo.BasicExamResult", "ExaminationQuestionID", "dbo.ExaminationQuestion");
            DropForeignKey("dbo.AdvancedExamResult", "ExaminationQuestionID", "dbo.ExaminationQuestion");
            DropForeignKey("dbo.BasicExamResultDetail", "QuesionID", "dbo.Question");
            DropForeignKey("dbo.AdvancedExamResultDetail", "QuestionID", "dbo.Question");
            DropForeignKey("dbo.AdvancedExamResult", "ExamineeID", "dbo.Examinee");
            DropForeignKey("dbo.ExamScheduleAdvanced", "ExaminationRoomID", "dbo.ExaminationRoom");
            DropForeignKey("dbo.AdvancedExamResult", "AdvancedModuleRegistrationID", "dbo.AdvancedModuleRegistration");
            DropForeignKey("dbo.AdvancedExamResultDetail", "AdvancedExamResultID", "dbo.AdvancedExamResult");
            DropIndex("dbo.MajorClass", new[] { "FacultyID" });
            DropIndex("dbo.Student", new[] { "MajorClassID" });
            DropIndex("dbo.Student", new[] { "ExamineeID" });
            DropIndex("dbo.ExamineeExamScheduleAdvanced", new[] { "ExamineeID" });
            DropIndex("dbo.ExamineeExamScheduleAdvanced", new[] { "ExamScheduleAdvancedID" });
            DropIndex("dbo.IDExamineeRegistration", new[] { "RegistrationID" });
            DropIndex("dbo.Registration", new[] { "ExamPeriodID" });
            DropIndex("dbo.Registration", new[] { "InformationTechnologySkillID" });
            DropIndex("dbo.Registration", new[] { "ExamineeID" });
            DropIndex("dbo.ExamineeInformationTechnologySkill", new[] { "InformationTechnologySkillID" });
            DropIndex("dbo.ExamineeInformationTechnologySkill", new[] { "ExamineeID" });
            DropIndex("dbo.QuestionModule", new[] { "InformationTechnologySkillID" });
            DropIndex("dbo.ExaminationModule", new[] { "QuestionModuleID" });
            DropIndex("dbo.ExaminationModule", new[] { "ExaminationID" });
            DropIndex("dbo.Examination", new[] { "ExamPeriodID" });
            DropIndex("dbo.ExaminationQuestion", new[] { "QuestionID" });
            DropIndex("dbo.ExaminationQuestion", new[] { "ExaminationID" });
            DropIndex("dbo.Question", new[] { "QuestionClassificationID" });
            DropIndex("dbo.Question", new[] { "QuestionModuleID" });
            DropIndex("dbo.BasicExamResultDetail", new[] { "QuesionID" });
            DropIndex("dbo.BasicExamResultDetail", new[] { "BasicExamResultID" });
            DropIndex("dbo.BasicExamResult", new[] { "ExamineeID" });
            DropIndex("dbo.BasicExamResult", new[] { "StartEndTimeID" });
            DropIndex("dbo.BasicExamResult", new[] { "ExaminationQuestionID" });
            DropIndex("dbo.BasicExamResult", new[] { "RegistrationID" });
            DropIndex("dbo.BasicExamResult", new[] { "ExamPeriodID" });
            DropIndex("dbo.ExamineeExamScheduleBasic", new[] { "ExamineeID" });
            DropIndex("dbo.ExamineeExamScheduleBasic", new[] { "ExamScheduleBasicID" });
            DropIndex("dbo.ExamScheduleBasic", new[] { "ExaminationRoomID" });
            DropIndex("dbo.ExamScheduleBasic", new[] { "StartEndTimeID" });
            DropIndex("dbo.ExamScheduleBasic", new[] { "ExamPeriodID" });
            DropIndex("dbo.ExamScheduleAdvanced", new[] { "ExaminationRoomID" });
            DropIndex("dbo.ExamScheduleAdvanced", new[] { "StartEndTimeID" });
            DropIndex("dbo.ExamScheduleAdvanced", new[] { "ExamPeriodID" });
            DropIndex("dbo.ExamScheduleAdvanced", new[] { "AdvancedModuleRegistrationID" });
            DropIndex("dbo.AdvancedModuleRegistration", new[] { "QuestionModuleID" });
            DropIndex("dbo.AdvancedModuleRegistration", new[] { "RegistrationID" });
            DropIndex("dbo.AdvancedExamResult", new[] { "ExamineeID" });
            DropIndex("dbo.AdvancedExamResult", new[] { "StartEndTimeID" });
            DropIndex("dbo.AdvancedExamResult", new[] { "ExaminationQuestionID" });
            DropIndex("dbo.AdvancedExamResult", new[] { "AdvancedModuleRegistrationID" });
            DropIndex("dbo.AdvancedExamResult", new[] { "ExamPeriodID" });
            DropIndex("dbo.AdvancedExamResultDetail", new[] { "QuestionID" });
            DropIndex("dbo.AdvancedExamResultDetail", new[] { "AdvancedExamResultID" });
            DropTable("dbo.PracticeExamQuestions");
            DropTable("dbo.Note");
            DropTable("dbo.Manager");
            DropTable("dbo.LocationExam");
            DropTable("dbo.Faculty");
            DropTable("dbo.MajorClass");
            DropTable("dbo.Student");
            DropTable("dbo.ExamineeExamScheduleAdvanced");
            DropTable("dbo.StartEndTime");
            DropTable("dbo.QuestionClassification");
            DropTable("dbo.IDExamineeRegistration");
            DropTable("dbo.ExamPeriod");
            DropTable("dbo.Registration");
            DropTable("dbo.ExamineeInformationTechnologySkill");
            DropTable("dbo.InformationTechnologySkill");
            DropTable("dbo.QuestionModule");
            DropTable("dbo.ExaminationModule");
            DropTable("dbo.Examination");
            DropTable("dbo.ExaminationQuestion");
            DropTable("dbo.Question");
            DropTable("dbo.BasicExamResultDetail");
            DropTable("dbo.BasicExamResult");
            DropTable("dbo.Examinee");
            DropTable("dbo.ExamineeExamScheduleBasic");
            DropTable("dbo.ExamScheduleBasic");
            DropTable("dbo.ExaminationRoom");
            DropTable("dbo.ExamScheduleAdvanced");
            DropTable("dbo.AdvancedModuleRegistration");
            DropTable("dbo.AdvancedExamResult");
            DropTable("dbo.AdvancedExamResultDetail");
        }
    }
}
