using BuildDB_Team.entitys;
using database.models;
using DataBase.Entity;
using DataBase.entitys;
using DevetionStudetns.Entity;
using DevetionStudetns.entitys;
using DevetionStudetns.Interface;
using DevetionStudetns.NewFolder;
using Microsoft.EntityFrameworkCore;
using testDtoAndmapper.Entity;
using UploadReportsCode.Entity;
using V1.Entity;

namespace DataBase.DBcon
{
    public class DBC :DbContext
    {
        public DBC(DbContextOptions<DBC> options) : base(options) { }
        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Distribution> Distributions { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Doctor_Course> Doctor_Course { get; set; }
        public DbSet<Doctor> doctors { get; set; }
        public DbSet<Hospital> hospitals { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MainGrop> mainGrops { get; set; }
        public DbSet<Marks> Marks { get; set; }
        public DbSet<Questionnaire> Questionnaire { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Rotation> Rotations { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<SubGroup> subGroups { get; set; }
        public DbSet<TA> TA { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<WeeklyEvaluation> WeeklyEvaluation { get; set; }
        public DbSet<AnswerTheEvaluation> AnswerTheEvaluation { get; set; }
        public DbSet<EvaluationForm> EvaluationForm { get; set; }
        public DbSet<EvaluationForm_EvaluationQuestion> EvaluationForm_EvaluationQuestions { get; set; }
        public DbSet<EvaluationQuestion> EvaluationQuestions { get; set; }
        public DbSet<DistributionsMainGroup> DistributionsMainGroup { get; set; }
        public DbSet<AllAcademinYears> AllAcademinYears { get; set; }
        public DbSet<Policie> Policies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor_Course>().HasKey(sc => new { sc.DoctorId, sc.CourseId ,sc.CurrentAcademicYearName }); // المفتاح المركب
            modelBuilder.Entity<EvaluationForm_EvaluationQuestion>().HasKey(sc => new { sc.EvaluationFormId, sc.EvaluationQuestionId }); // المفتاح المركب

            modelBuilder.Entity<Policie>()
               .HasOne(w => w.User)
               .WithMany(f => f.CreatePolicie)
               .HasForeignKey(w => w.CreatorId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WeeklyEvaluation>()
                .HasOne(w => w.Appointment)
                .WithMany(a => a.WeeklyEvaluation)
                .HasForeignKey(w => w.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<WeeklyEvaluation>()
                .HasOne(w => w.EvaluationQuestion)
                .WithMany(e => e.WeeklyEvaluation)
                .HasForeignKey(w => w.EvaluationQuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WeeklyEvaluation>()
               .HasOne(w => w.EvaluationForm)
               .WithMany(f => f.weeklyEvaluations)
               .HasForeignKey(w => w.EvaluationFormId)
               .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<AcademicYear>()
                .HasIndex(u => u.AcademicYearLevel)
                .IsUnique();

            modelBuilder.Entity<Users>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Users>()
            .HasIndex(u => u.IdNumber)
            .IsUnique();

            modelBuilder.Entity<Users>()
            .HasIndex(u => u.PhoneNumber)
            .IsUnique();

            modelBuilder.Entity<DistributionsMainGroup>()
             .HasOne(s => s.MainGroup) 
             .WithMany(u => u.DistributionsMainGroup)  
             .HasForeignKey(r => r.MainGroupId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DistributionsMainGroup>()
            .HasOne(s => s.Rotations)  
            .WithMany(u => u.DistributionsMainGroup) 
            .HasForeignKey(r => r.RotationId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DistributionsMainGroup>()
            .HasOne(s => s.Department) 
            .WithMany(u => u.DistributionsMainGroup) 
            .HasForeignKey(r => r.DepartmentId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AnswerTheEvaluation>()
            .HasOne(s => s.Appointment)  
            .WithMany(u => u.answerTheEvaluation)  
            .HasForeignKey(r => r.Appointmentid).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AnswerTheEvaluation>()
            .HasOne(s => s.EvaluationQuestions)  
            .WithMany(u => u.AnswerTheEvaluation) 
            .HasForeignKey(r => r.QuestionId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AnswerTheEvaluation>()
            .HasOne(s => s.EvaluationForm)  
            .WithMany(u => u.AnswerTheEvaluation) 
            .HasForeignKey(r => r.EvaluationFormId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AnswerTheEvaluation>()
           .HasOne(s => s.EvaluatorPerson) 
           .WithMany(u => u.EvaluatedPersonUser)  
           .HasForeignKey(r => r.EvaluatedPersonId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AnswerTheEvaluation>()
           .HasOne(s => s.EvaluatedPerson) 
           .WithMany(u => u.EvaluatorPersonUser) 
           .HasForeignKey(r => r.EvaluatorPersonId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EvaluationForm_EvaluationQuestion>()
           .HasOne(s => s.EvaluationQuestions)  
           .WithMany(u => u.EvaluationForm_EvaluationQuestions) 
           .HasForeignKey(r => r.EvaluationQuestionId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EvaluationForm_EvaluationQuestion>()
           .HasOne(s => s.EvaluationForm)  
           .WithMany(u => u.EvaluationForm_EvaluationQuestions) 
           .HasForeignKey(r => r.EvaluationFormId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WeeklyEvaluation>()
            .HasOne(s => s.course)  
            .WithMany(u => u.WeeklyEvaluation) 
            .HasForeignKey(r => r.CourseId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WeeklyEvaluation>()
            .HasOne(s => s.Students) 
            .WithMany(u => u.WeeklyEvaluation) 
            .HasForeignKey(r => r.StudentId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WeeklyEvaluation>()
             .HasOne(s => s.Doctors)  
             .WithMany(u => u.WeeklyEvaluation) 
             .HasForeignKey(r => r.DoctorId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Marks>()
             .HasOne(s => s.Doctors) 
             .WithMany(u => u.Marks)  
             .HasForeignKey(r => r.DoctorId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Marks>()
             .HasOne(s => s.Students) 
             .WithMany(u => u.Marks)  
             .HasForeignKey(r => r.StudentId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Marks>()
             .HasOne(s => s.course) 
             .WithMany(u => u.Marks)  
             .HasForeignKey(r => r.CourseId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Report>()
            .HasOne(s => s.User)  
            .WithMany(u => u.Reports) 
            .HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Questionnaire>()
            .HasOne(s => s.User)  
            .WithMany(u => u.Questionnaire) 
            .HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Attendance>()
             .HasOne(s => s.course) 
             .WithMany(u => u.Attendance) 
             .HasForeignKey(r => r.CourseId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Attendance>()
              .HasOne(s => s.Doctors)  
              .WithMany(u => u.DoctorAttendances) 
              .HasForeignKey(r => r.DoctorId)
              .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Attendance>()
             .HasOne(a => a.Students)  
             .WithMany(u => u.StudentAttendances) 
             .HasForeignKey(r => r.StudentId)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
             .HasOne(s => s.Sender) 
             .WithMany(u => u.UserSender) 
             .HasForeignKey(r => r.SenderId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
             .HasOne(s => s.Resever) 
             .WithMany(u => u.UserResever)  
             .HasForeignKey(r => r.ReseverId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TA>()
                .HasOne(s => s.users) 
                .WithOne(u => u.TA) 
                .HasForeignKey<TA>(s => s.TAId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Doctor_Course>()
               .HasOne(s => s.course)  
               .WithMany(u => u.doctor_Courses) 
               .HasForeignKey(r => r.CourseId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Doctor_Course>()
              .HasOne(s => s.doctors)  
              .WithMany(u => u.doctor_Courses)  
              .HasForeignKey(r => r.DoctorId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.User) 
                .WithOne(u => u.students) 
                .HasForeignKey<Student>(s => s.UserId); 

            modelBuilder.Entity<Doctor>()
              .HasOne(s => s.users) 
              .WithOne(u => u.doctors)  
              .HasForeignKey<Doctor>(s => s.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Doctor>()
                   .HasOne(r => r.hospital) 
                   .WithMany(u => u.doctors) 
                   .HasForeignKey(r => r.HospitalId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Doctor>()
                 .HasOne(r => r.department) 
                 .WithMany(u => u.doctors) 
                 .HasForeignKey(r => r.DepartmentID).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Appointment>()
                     .HasOne(r => r.Rotations) 
                     .WithMany(u => u.Appointments) 
                     .HasForeignKey(r => r.RotationId).OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Course>()
                   .HasOne(r => r.Department) 
                   .WithMany(u => u.Courses) 
                   .HasForeignKey(r => r.DepartmentId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Distribution>()
                  .HasOne(r => r.SubGroup) 
                  .WithMany(u => u.Distributions) 
                  .HasForeignKey(r => r.SubGroupId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Distribution>()
                  .HasOne(r => r.User) 
                  .WithMany(u => u.Distributions)
                  .HasForeignKey(r => r.DoctorId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Distribution>()
                 .HasOne(r => r.Course)
                 .WithMany(u => u.Distributions) 
                 .HasForeignKey(r => r.CourseId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Distribution>()
                .HasOne(r => r.Appointments) 
                .WithMany(u => u.Distributions) 
                .HasForeignKey(r => r.AppointmentId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Distribution>()
                .HasOne(r => r.Rotations) 
                .WithMany(u => u.Distributions) 
                .HasForeignKey(r => r.RotationId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Division>()
                .HasOne(r => r.SubGroup) 
                .WithMany(u => u.divisions) 
                .HasForeignKey(r => r.SubGroupId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Division>()
               .HasOne(r => r.Student)
               .WithMany(u => u.divisions)
               .HasForeignKey(r => r.StudentId).
               OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SubGroup>()
               .HasOne(r => r.MainGrop) 
               .WithMany(u => u.SubGroups) 
               .HasForeignKey(r => r.MainGroupId).OnDelete(DeleteBehavior.Cascade);
        }
       
    }
}

    

