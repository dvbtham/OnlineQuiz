namespace OnlineQuiz.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsCompletedColunm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExamResult", "IsCompleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExamResult", "IsCompleted");
        }
    }
}
