using asp_core_3.auth.Filter;
using DataBase.DBcon;
using DepartmentsTbl_CRUD.Repos;
using DepartmentsTbl_CRUD.Service;
using DevetionStudetns.Interface;
using DevetionStudetns.Repositry.AcademicYearRepostry;
using DevetionStudetns.Repositry.AddDistributionRepositry;
using DevetionStudetns.Repositry.AppotmentsReposetry;
using DevetionStudetns.Repositry.AttendanceRepositry;
using DevetionStudetns.Repositry.CourseRepostry;
using DevetionStudetns.Repositry.DistributionsMainGroupRepositry;
using DevetionStudetns.Repositry.DivisionRepostry;
using DevetionStudetns.Repositry.DoctorReposotry;
using DevetionStudetns.Repositry.HospitalRepostry;
using DevetionStudetns.Repositry.MainGroupReosetry;
using DevetionStudetns.Repositry.RotationsRepositry;
using DevetionStudetns.Repositry.StudentsReposetry;
using DevetionStudetns.Repositry.SubGroupRepositry;
using DevetionStudetns.Repositry.TARepositry;
using DevetionStudetns.Repositry.UsersReosetry;
using DevetionStudetns.Service;
using FinalProject.Interface.IRepositry;
using FinalProject.Repositry;
using V1.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using rafeeqeq.auth.jwt.interfaces;
using rafeeqeq.auth.jwt.Service;
using System.Text;
using V1.abed;
using V1.Interface.IRepositry;
using V1.Interface.IService;
using V1.Repositry;
using V1.Service;
using FinalProject.Service;
using V1.ChatHub;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
///SignalR
builder.Services.AddSignalR();


builder.Services.AddDbContext<DBC>(Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IAcademicYear, AcademicYearRepo>();
builder.Services.AddScoped<AcademicYearService>();
builder.Services.AddScoped<IMainGroup, MainGourpRepositry>();
builder.Services.AddScoped<MainGroupService>();
builder.Services.AddScoped<ISubGroup, SubGroupRepo>();
builder.Services.AddScoped<SubGroupService>();
builder.Services.AddScoped<IUsers, UserRepo>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IDivision, DivisionRepo>();
builder.Services.AddScoped<DivisionService>();
builder.Services.AddScoped<IStudents, StudentsRepo>();
builder.Services.AddScoped<StudentsService>();
builder.Services.AddScoped<IAppointments, AppotmentsRepo>();
builder.Services.AddScoped<IDistribution, DistributionRepo>();
builder.Services.AddScoped<AppotmentsServices>();
builder.Services.AddScoped<DistriputionService>();
builder.Services.AddScoped<IRotations, RotationsRepo>();
builder.Services.AddScoped<RotationsService>();
builder.Services.AddScoped<IHospital, HospitalRepo>();
builder.Services.AddScoped<HospitalServes>();
builder.Services.AddScoped<ICourse, CourseRepo>();
builder.Services.AddScoped<CourseServes>();
builder.Services.AddScoped<IDepartmentRepo, DepartmentsRepo>();
builder.Services.AddScoped<IDepartmentsService, DepartmentsService>();
builder.Services.AddScoped<IDistributionsMainGroup, DistributionsMainGroupRepo>();
builder.Services.AddScoped<DistributionsMainGroupService>();
builder.Services.AddScoped<ITARepo, TARepo>(); 
builder.Services.AddScoped<ITAService, TAService>(); 
builder.Services.AddScoped<IDoctor, DoctorRepo>();
builder.Services.AddScoped<DoctorServes>();
builder.Services.AddScoped<IQuestionnaire, QuestionnaireRepo>();
builder.Services.AddScoped<QuestionnaireSerivce>();
builder.Services.AddScoped<MessageService>();
builder.Services.AddScoped<IWeeklyEvaluationRepo,WeeklyEvaluationRepo>(); 
builder.Services.AddScoped<IWeeklyEvaluationService,WeeklyEvaluationService>(); 
builder.Services.AddScoped<IMark, MarksRepo>();
builder.Services.AddScoped<MarkService>();
builder.Services.AddScoped<IEvaluationQuestionsRepo, EvaluationQuestionsReposoitry>();
builder.Services.AddScoped<EvaluationQuestionsServes>();
builder.Services.AddScoped<IEvaluationFormRepo, EvaluationFormRepositry>();
builder.Services.AddScoped<EvaluationFormServes>();
builder.Services.AddScoped<IEvaluationFormAndQuestionRepo, EvaluationFormAndEvaluationQuestionRepositry>();
builder.Services.AddScoped<EvaluationFormAndEvaluationQuestionServes>();
builder.Services.AddScoped<IAnswerTheEvaluationRepo, AnswerTheEvakluationReposoitry>();
builder.Services.AddScoped<AnswerTheEvaluationServes>();
builder.Services.AddScoped<IAttendanceRepositry, AttendanceRepositry>(); 
builder.Services.AddScoped<IAttendanceService, AttendanceService>();  
builder.Services.AddScoped<IAllAcademinYears, AllAcademicYearRepo>();
builder.Services.AddScoped<AllAcademinYearsSerivce>();
builder.Services.AddScoped<IApprovals, ApprovalsRepo>();
builder.Services.AddScoped<ApprovalsService>();
builder.Services.AddScoped<IResultAnswer, ResultAnswerRepo>();
builder.Services.AddScoped<ResultAnswerService>();
builder.Services.AddScoped<loginserves>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IDoctorCrouse, DoctorCrouseRepo>();
builder.Services.AddScoped<DoctorCourseService>();
builder.Services.AddScoped<IPolicie, PolicieRepo>();
builder.Services.AddScoped<PolicieService>();

builder.Services.AddControllers(options =>
{
    options.Filters.AddService<SSFilter>();  // Resolves SSFilter from DI
});
builder.Services.AddScoped<SSFilter>();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidIssuer = builder.Configuration["JWT:Issuer"],

//        ValidateAudience = true,
//        ValidAudience = builder.Configuration["JWT:Audience"],

//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(
//            Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])
//        ),

//        ValidateLifetime = true,
//        ClockSkew = TimeSpan.Zero
//    };
//    ///SignalR

//    options.Events = new JwtBearerEvents
//    {
//        OnMessageReceived = context =>
//        {
//            var accessToken =
//                context.Request.Query["access_token"];

//            var path = context.HttpContext.Request.Path;

//            if (!string.IsNullOrEmpty(accessToken)
//                && path.StartsWithSegments("/chatHub"))
//            {
//                context.Token = accessToken;
//            }

//            return Task.CompletedTask;
//        }
//    };
//});


///SignalR
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],

        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])
        ),

        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };

    // 🔴 (تعديل مهم جدًا لـ SignalR)
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;

            if (!string.IsNullOrEmpty(accessToken) &&
                path.StartsWithSegments("/chatHub"))
            {
                context.Token = accessToken;
            }

            return Task.CompletedTask;
        }
    };
});


// Enable CORS for the specific frontend URL
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowFrontend", policy =>
//    {
//        policy.WithOrigins("https://clinical-training-system.onrender.com") // رابط الواجهة من Render
//              .AllowAnyHeader()
//              .AllowAnyMethod();
//    });
//});

//var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
//builder.WebHost.UseUrls($"http://*:{port}");


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(builder =>
    builder.AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader());
}

// Use the CORS policy
//app.UseCors("AllowFrontend");
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();


app.MapControllers();
///SignalR
app.MapHub<V1.ChatHub.ChatHub>("/chatHub");
app.Run();
