namespace OnlineQuiz.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstRun : DbMigration
    {
        public override void Up()
        {
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
                "dbo.InformationTechnologySkill",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Examination",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        InformationTechnologySkillID = c.Guid(),
                        Duration = c.Int(),
                        QuestionNumber = c.Int(),
                        TestNumber = c.Int(),
                        Remark = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.InformationTechnologySkill", t => t.InformationTechnologySkillID)
                .Index(t => t.InformationTechnologySkillID);
            
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
                .ForeignKey("dbo.QuestionClassification", t => t.QuestionClassificationID, cascadeDelete: true)
                .ForeignKey("dbo.QuestionModule", t => t.QuestionModuleID, cascadeDelete: true)
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
                .ForeignKey("dbo.Question", t => t.QuestionID, cascadeDelete: true)
                .ForeignKey("dbo.Examination", t => t.ExaminationID, cascadeDelete: true)
                .Index(t => t.ExaminationID)
                .Index(t => t.QuestionID);
            
            CreateTable(
                "dbo.ExamResultDetail",
                c => new
                    {
                        ExamResultID = c.Guid(nullable: false),
                        QuestionID = c.Guid(nullable: false),
                        Answer = c.String(maxLength: 1, unicode: false),
                        ResultAnswer = c.String(maxLength: 250),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.ExamResultID, t.QuestionID })
                .ForeignKey("dbo.ExamResult", t => t.ExamResultID, cascadeDelete: true)
                .ForeignKey("dbo.Question", t => t.QuestionID)
                .Index(t => t.ExamResultID)
                .Index(t => t.QuestionID);
            
            CreateTable(
                "dbo.ExamResult",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ExamineeID = c.Guid(),
                        IDExaminee = c.String(maxLength: 50, unicode: false),
                        ExaminationID = c.Guid(),
                        ExamCode = c.Int(),
                        DateOfTest = c.DateTime(),
                        Duration = c.Int(),
                        TrueQuestion = c.Int(),
                        Point = c.Double(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Examination", t => t.ExaminationID)
                .ForeignKey("dbo.Examinee", t => t.ExamineeID)
                .Index(t => t.ExamineeID)
                .Index(t => t.ExaminationID);
            
            CreateTable(
                "dbo.QuestionClassification",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
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
                "dbo.StartEndTime",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        TimePeriod = c.String(maxLength: 100, unicode: false),
                        Remark = c.String(),
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
            
            CreateTable(
                "dbo.ExaminationOfExamPeriod",
                c => new
                    {
                        ExaminationID = c.Guid(nullable: false),
                        ExamPeriodID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ExaminationID, t.ExamPeriodID })
                .ForeignKey("dbo.Examination", t => t.ExaminationID, cascadeDelete: true)
                .ForeignKey("dbo.ExamPeriod", t => t.ExamPeriodID, cascadeDelete: true)
                .Index(t => t.ExaminationID)
                .Index(t => t.ExamPeriodID);
            
            CreateTable(
                "dbo.ExamineeExamScheduleAdvanced",
                c => new
                    {
                        ExamineeID = c.Guid(nullable: false),
                        ExamScheduleAdvancedID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ExamineeID, t.ExamScheduleAdvancedID })
                .ForeignKey("dbo.Examinee", t => t.ExamineeID, cascadeDelete: false)
                .ForeignKey("dbo.ExamScheduleAdvanced", t => t.ExamScheduleAdvancedID, cascadeDelete: true)
                .Index(t => t.ExamineeID)
                .Index(t => t.ExamScheduleAdvancedID);
            
            CreateTable(
                "dbo.ExamineeExamScheduleBasic",
                c => new
                    {
                        ExamineeID = c.Guid(nullable: false),
                        ExamScheduleBasicID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ExamineeID, t.ExamScheduleBasicID })
                .ForeignKey("dbo.Examinee", t => t.ExamineeID, cascadeDelete: true)
                .ForeignKey("dbo.ExamScheduleBasic", t => t.ExamScheduleBasicID, cascadeDelete: true)
                .Index(t => t.ExamineeID)
                .Index(t => t.ExamScheduleBasicID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExamScheduleAdvanced", "AdvancedModuleRegistrationID", "dbo.AdvancedModuleRegistration");
            DropForeignKey("dbo.ExamScheduleBasic", "ExaminationRoomID", "dbo.ExaminationRoom");
            DropForeignKey("dbo.ExamScheduleBasic", "StartEndTimeID", "dbo.StartEndTime");
            DropForeignKey("dbo.ExamScheduleAdvanced", "StartEndTimeID", "dbo.StartEndTime");
            DropForeignKey("dbo.Student", "ExamineeID", "dbo.Examinee");
            DropForeignKey("dbo.Student", "MajorClassID", "dbo.MajorClass");
            DropForeignKey("dbo.MajorClass", "FacultyID", "dbo.Faculty");
            DropForeignKey("dbo.Registration", "ExamineeID", "dbo.Examinee");
            DropForeignKey("dbo.ExamineeExamScheduleBasic", "ExamScheduleBasicID", "dbo.ExamScheduleBasic");
            DropForeignKey("dbo.ExamineeExamScheduleBasic", "ExamineeID", "dbo.Examinee");
            DropForeignKey("dbo.ExamineeExamScheduleAdvanced", "ExamScheduleAdvancedID", "dbo.ExamScheduleAdvanced");
            DropForeignKey("dbo.ExamineeExamScheduleAdvanced", "ExamineeID", "dbo.Examinee");
            DropForeignKey("dbo.Registration", "InformationTechnologySkillID", "dbo.InformationTechnologySkill");
            DropForeignKey("dbo.QuestionModule", "InformationTechnologySkillID", "dbo.InformationTechnologySkill");
            DropForeignKey("dbo.ExamineeInformationTechnologySkill", "InformationTechnologySkillID", "dbo.InformationTechnologySkill");
            DropForeignKey("dbo.Examination", "InformationTechnologySkillID", "dbo.InformationTechnologySkill");
            DropForeignKey("dbo.ExaminationOfExamPeriod", "ExamPeriodID", "dbo.ExamPeriod");
            DropForeignKey("dbo.ExaminationOfExamPeriod", "ExaminationID", "dbo.Examination");
            DropForeignKey("dbo.Registration", "ExamPeriodID", "dbo.ExamPeriod");
            DropForeignKey("dbo.IDExamineeRegistration", "RegistrationID", "dbo.Registration");
            DropForeignKey("dbo.AdvancedModuleRegistration", "RegistrationID", "dbo.Registration");
            DropForeignKey("dbo.ExamScheduleBasic", "ExamPeriodID", "dbo.ExamPeriod");
            DropForeignKey("dbo.ExamScheduleAdvanced", "ExamPeriodID", "dbo.ExamPeriod");
            DropForeignKey("dbo.ExaminationQuestion", "ExaminationID", "dbo.Examination");
            DropForeignKey("dbo.Question", "QuestionModuleID", "dbo.QuestionModule");
            DropForeignKey("dbo.Question", "QuestionClassificationID", "dbo.QuestionClassification");
            DropForeignKey("dbo.ExamResultDetail", "QuestionID", "dbo.Question");
            DropForeignKey("dbo.ExamResultDetail", "ExamResultID", "dbo.ExamResult");
            DropForeignKey("dbo.ExamResult", "ExamineeID", "dbo.Examinee");
            DropForeignKey("dbo.ExamResult", "ExaminationID", "dbo.Examination");
            DropForeignKey("dbo.ExaminationQuestion", "QuestionID", "dbo.Question");
            DropForeignKey("dbo.ExaminationModule", "QuestionModuleID", "dbo.QuestionModule");
            DropForeignKey("dbo.AdvancedModuleRegistration", "QuestionModuleID", "dbo.QuestionModule");
            DropForeignKey("dbo.ExaminationModule", "ExaminationID", "dbo.Examination");
            DropForeignKey("dbo.ExamineeInformationTechnologySkill", "ExamineeID", "dbo.Examinee");
            DropForeignKey("dbo.ExamScheduleAdvanced", "ExaminationRoomID", "dbo.ExaminationRoom");
            DropIndex("dbo.ExamineeExamScheduleBasic", new[] { "ExamScheduleBasicID" });
            DropIndex("dbo.ExamineeExamScheduleBasic", new[] { "ExamineeID" });
            DropIndex("dbo.ExamineeExamScheduleAdvanced", new[] { "ExamScheduleAdvancedID" });
            DropIndex("dbo.ExamineeExamScheduleAdvanced", new[] { "ExamineeID" });
            DropIndex("dbo.ExaminationOfExamPeriod", new[] { "ExamPeriodID" });
            DropIndex("dbo.ExaminationOfExamPeriod", new[] { "ExaminationID" });
            DropIndex("dbo.MajorClass", new[] { "FacultyID" });
            DropIndex("dbo.Student", new[] { "MajorClassID" });
            DropIndex("dbo.Student", new[] { "ExamineeID" });
            DropIndex("dbo.IDExamineeRegistration", new[] { "RegistrationID" });
            DropIndex("dbo.Registration", new[] { "ExamPeriodID" });
            DropIndex("dbo.Registration", new[] { "InformationTechnologySkillID" });
            DropIndex("dbo.Registration", new[] { "ExamineeID" });
            DropIndex("dbo.ExamResult", new[] { "ExaminationID" });
            DropIndex("dbo.ExamResult", new[] { "ExamineeID" });
            DropIndex("dbo.ExamResultDetail", new[] { "QuestionID" });
            DropIndex("dbo.ExamResultDetail", new[] { "ExamResultID" });
            DropIndex("dbo.ExaminationQuestion", new[] { "QuestionID" });
            DropIndex("dbo.ExaminationQuestion", new[] { "ExaminationID" });
            DropIndex("dbo.Question", new[] { "QuestionClassificationID" });
            DropIndex("dbo.Question", new[] { "QuestionModuleID" });
            DropIndex("dbo.QuestionModule", new[] { "InformationTechnologySkillID" });
            DropIndex("dbo.ExaminationModule", new[] { "QuestionModuleID" });
            DropIndex("dbo.ExaminationModule", new[] { "ExaminationID" });
            DropIndex("dbo.Examination", new[] { "InformationTechnologySkillID" });
            DropIndex("dbo.ExamineeInformationTechnologySkill", new[] { "InformationTechnologySkillID" });
            DropIndex("dbo.ExamineeInformationTechnologySkill", new[] { "ExamineeID" });
            DropIndex("dbo.ExamScheduleBasic", new[] { "ExaminationRoomID" });
            DropIndex("dbo.ExamScheduleBasic", new[] { "StartEndTimeID" });
            DropIndex("dbo.ExamScheduleBasic", new[] { "ExamPeriodID" });
            DropIndex("dbo.ExamScheduleAdvanced", new[] { "ExaminationRoomID" });
            DropIndex("dbo.ExamScheduleAdvanced", new[] { "StartEndTimeID" });
            DropIndex("dbo.ExamScheduleAdvanced", new[] { "ExamPeriodID" });
            DropIndex("dbo.ExamScheduleAdvanced", new[] { "AdvancedModuleRegistrationID" });
            DropIndex("dbo.AdvancedModuleRegistration", new[] { "QuestionModuleID" });
            DropIndex("dbo.AdvancedModuleRegistration", new[] { "RegistrationID" });
            DropTable("dbo.ExamineeExamScheduleBasic");
            DropTable("dbo.ExamineeExamScheduleAdvanced");
            DropTable("dbo.ExaminationOfExamPeriod");
            DropTable("dbo.PracticeExamQuestions");
            DropTable("dbo.Note");
            DropTable("dbo.Manager");
            DropTable("dbo.LocationExam");
            DropTable("dbo.StartEndTime");
            DropTable("dbo.Faculty");
            DropTable("dbo.MajorClass");
            DropTable("dbo.Student");
            DropTable("dbo.IDExamineeRegistration");
            DropTable("dbo.Registration");
            DropTable("dbo.ExamPeriod");
            DropTable("dbo.QuestionClassification");
            DropTable("dbo.ExamResult");
            DropTable("dbo.ExamResultDetail");
            DropTable("dbo.ExaminationQuestion");
            DropTable("dbo.Question");
            DropTable("dbo.QuestionModule");
            DropTable("dbo.ExaminationModule");
            DropTable("dbo.Examination");
            DropTable("dbo.InformationTechnologySkill");
            DropTable("dbo.ExamineeInformationTechnologySkill");
            DropTable("dbo.Examinee");
            DropTable("dbo.ExamScheduleBasic");
            DropTable("dbo.ExaminationRoom");
            DropTable("dbo.ExamScheduleAdvanced");
            DropTable("dbo.AdvancedModuleRegistration");
        }
    }
}
