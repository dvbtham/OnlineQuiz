namespace OnlineQuiz.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameQuesionToQuestion : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ExamResultDetail", name: "QuesionID", newName: "QuestionID");
            
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.ExamResultDetail", name: "QuestionID", newName: "QuesionID");
        }
    }
}
