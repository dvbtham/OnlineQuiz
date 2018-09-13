namespace OnlineQuiz.Model.Entity
{
    using System.Data.Entity;

    public partial class OnlineQuizDbContext : DbContext
    {
        public OnlineQuizDbContext()
            : base("name=OnlineQuizDbContext")
        {
        }

        public virtual DbSet<AdvancedExamResult> AdvancedExamResults { get; set; }
        public virtual DbSet<AdvancedExamResultDetail> AdvancedExamResultDetails { get; set; }
        public virtual DbSet<AdvancedModuleRegistration> AdvancedModuleRegistrations { get; set; }
        public virtual DbSet<BasicExamResult> BasicExamResults { get; set; }
        public virtual DbSet<BasicExamResultDetail> BasicExamResultDetails { get; set; }
        public virtual DbSet<Examination> Examinations { get; set; }
        public virtual DbSet<ExaminationModule> ExaminationModules { get; set; }
        public virtual DbSet<ExaminationQuestion> ExaminationQuestions { get; set; }
        public virtual DbSet<ExaminationRoom> ExaminationRooms { get; set; }
        public virtual DbSet<Examinee> Examinees { get; set; }
        public virtual DbSet<ExamineeExamScheduleAdvanced> ExamineeExamScheduleAdvanceds { get; set; }
        public virtual DbSet<ExamineeExamScheduleBasic> ExamineeExamScheduleBasics { get; set; }
        public virtual DbSet<ExamineeInformationTechnologySkill> ExamineeInformationTechnologySkills { get; set; }
        public virtual DbSet<ExamPeriod> ExamPeriods { get; set; }
        public virtual DbSet<ExamScheduleAdvanced> ExamScheduleAdvanceds { get; set; }
        public virtual DbSet<ExamScheduleBasic> ExamScheduleBasics { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<IDExamineeRegistration> IDExamineeRegistrations { get; set; }
        public virtual DbSet<InformationTechnologySkill> InformationTechnologySkills { get; set; }
        public virtual DbSet<LocationExam> LocationExams { get; set; }
        public virtual DbSet<MajorClass> MajorClasses { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<PracticeExamQuestion> PracticeExamQuestions { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionClassification> QuestionClassifications { get; set; }
        public virtual DbSet<QuestionModule> QuestionModules { get; set; }
        public virtual DbSet<Registration> Registrations { get; set; }
        public virtual DbSet<StartEndTime> StartEndTimes { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdvancedExamResult>()
                .HasMany(e => e.AdvancedExamResultDetails)
                .WithOptional(e => e.AdvancedExamResult)
                .WillCascadeOnDelete();

            modelBuilder.Entity<AdvancedExamResultDetail>()
                .Property(e => e.Answer)
                .IsUnicode(false);

            modelBuilder.Entity<AdvancedModuleRegistration>()
                .Property(e => e.IDExaminee)
                .IsUnicode(false);

            modelBuilder.Entity<AdvancedModuleRegistration>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<AdvancedModuleRegistration>()
                .HasMany(e => e.AdvancedExamResults)
                .WithOptional(e => e.AdvancedModuleRegistration)
                .WillCascadeOnDelete();

            modelBuilder.Entity<AdvancedModuleRegistration>()
                .HasMany(e => e.ExamScheduleAdvanceds)
                .WithOptional(e => e.AdvancedModuleRegistration)
                .WillCascadeOnDelete();

            modelBuilder.Entity<BasicExamResult>()
                .HasMany(e => e.BasicExamResultDetails)
                .WithOptional(e => e.BasicExamResult)
                .WillCascadeOnDelete();

            modelBuilder.Entity<BasicExamResultDetail>()
                .Property(e => e.Answer)
                .IsUnicode(false);

            modelBuilder.Entity<Examination>()
                .HasMany(e => e.ExaminationQuestions)
                .WithOptional(e => e.Examination)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ExaminationQuestion>()
                .Property(e => e.Answer)
                .IsUnicode(false);

            modelBuilder.Entity<ExaminationRoom>()
                .HasMany(e => e.ExamScheduleBasics)
                .WithOptional(e => e.ExaminationRoom)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Examinee>()
                .Property(e => e.IdentityCard)
                .IsUnicode(false);

            modelBuilder.Entity<Examinee>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<Examinee>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Examinee>()
                .HasMany(e => e.ExamineeExamScheduleBasics)
                .WithOptional(e => e.Examinee)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Examinee>()
                .HasMany(e => e.Registrations)
                .WithOptional(e => e.Examinee)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Examinee>()
                .HasMany(e => e.Students)
                .WithOptional(e => e.Examinee)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ExamPeriod>()
                .HasMany(e => e.Examinations)
                .WithOptional(e => e.ExamPeriod)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ExamPeriod>()
                .HasMany(e => e.ExamScheduleBasics)
                .WithOptional(e => e.ExamPeriod)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ExamPeriod>()
                .HasMany(e => e.Registrations)
                .WithOptional(e => e.ExamPeriod)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ExamScheduleAdvanced>()
                .HasMany(e => e.ExamineeExamScheduleAdvanceds)
                .WithOptional(e => e.ExamScheduleAdvanced)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ExamScheduleBasic>()
                .HasMany(e => e.ExamineeExamScheduleBasics)
                .WithOptional(e => e.ExamScheduleBasic)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Faculty>()
                .HasMany(e => e.MajorClasses)
                .WithOptional(e => e.Faculty)
                .WillCascadeOnDelete();

            modelBuilder.Entity<IDExamineeRegistration>()
                .Property(e => e.IDExaminee)
                .IsUnicode(false);

            modelBuilder.Entity<IDExamineeRegistration>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<InformationTechnologySkill>()
                .HasMany(e => e.QuestionModules)
                .WithOptional(e => e.InformationTechnologySkill)
                .WillCascadeOnDelete();

            modelBuilder.Entity<InformationTechnologySkill>()
                .HasMany(e => e.Registrations)
                .WithOptional(e => e.InformationTechnologySkill)
                .WillCascadeOnDelete();

            modelBuilder.Entity<MajorClass>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<MajorClass>()
                .Property(e => e.Course)
                .IsUnicode(false);

            modelBuilder.Entity<MajorClass>()
                .HasMany(e => e.Students)
                .WithOptional(e => e.MajorClass)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Manager>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Manager>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Manager>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<Manager>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.Answer)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.BasicExamResultDetails)
                .WithOptional(e => e.Question)
                .HasForeignKey(e => e.QuesionID);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.ExaminationQuestions)
                .WithOptional(e => e.Question)
                .WillCascadeOnDelete();

            modelBuilder.Entity<QuestionClassification>()
                .HasMany(e => e.Questions)
                .WithOptional(e => e.QuestionClassification)
                .WillCascadeOnDelete();

            modelBuilder.Entity<QuestionModule>()
                .Property(e => e.IDQuestionModule)
                .IsUnicode(false);

            modelBuilder.Entity<QuestionModule>()
                .HasMany(e => e.Questions)
                .WithOptional(e => e.QuestionModule)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Registration>()
                .HasMany(e => e.AdvancedModuleRegistrations)
                .WithOptional(e => e.Registration)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Registration>()
                .HasMany(e => e.BasicExamResults)
                .WithOptional(e => e.Registration)
                .WillCascadeOnDelete();

            modelBuilder.Entity<StartEndTime>()
                .Property(e => e.TimePeriod)
                .IsUnicode(false);

            modelBuilder.Entity<StartEndTime>()
                .HasMany(e => e.ExamScheduleBasics)
                .WithOptional(e => e.StartEndTime)
                .WillCascadeOnDelete();
        }
    }
}
