namespace OnlineQuiz.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusColunm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExamResultDetail", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.ExamResult", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExamResult", "Status");
            DropColumn("dbo.ExamResultDetail", "Status");
        }
    }
}
