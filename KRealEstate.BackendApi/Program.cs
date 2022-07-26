using FluentValidation.AspNetCore;
using KRealEstate.Application.Catalog.Categories;
using KRealEstate.Application.Catalog.Products;
using KRealEstate.Application.Common;
using KRealEstate.Application.System.Email;
using KRealEstate.Data.DBContext;
using KRealEstate.Utilities.Constants;
using KRealEstate.ViewModels.System.Email;
using KRealEstate.ViewModels.System.Users;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
Microsoft.Extensions.Configuration.ConfigurationManager configuration = builder.Configuration;

//string issuer = configuration.GetValue<string>("Token:Issuer");
//string signingKey = configuration.GetValue<string>("Token:Key");
//byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);
//builder.Services.AddAuthentication(auth =>
//{
//    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//{
//    options.RequireHttpsMetadata = false;
//    options.SaveToken = true;
//    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidIssuer = issuer,
//        ValidateAudience = true,
//        ValidAudience = issuer,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ClockSkew = System.TimeSpan.Zero,
//        IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
//    };
//});

//// services.AddControllersWithViews();
////builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());

//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CollabClothing.BackendApi", Version = "v1" });
//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
//                                  Enter 'Bearer' [space] and then your token in the text input below.
//                                  \r\n\r\nExample: 'Bearer 12345abcdef'",
//        Name = "Authorization",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "Bearer"
//    });

//    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
//                  {
//                    {
//                      new OpenApiSecurityScheme
//                      {
//                        Reference = new OpenApiReference
//                          {
//                            Type = ReferenceType.SecurityScheme,
//                            Id = "Bearer"
//                          },
//                          Scheme = "oauth2",
//                          Name = "Bearer",
//                          In = ParameterLocation.Header,

//                        },
//                        new List<string>()
//                      }
//                    });
//});
builder.Services.AddCors(c => c.AddPolicy("AlloOrigin", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
    .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginValidation>());
builder.Services.AddControllers();
IWebHostEnvironment env = builder.Environment;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<RealEstateDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(SystemConstants.MAINCONNECTION)));
var emailConfig = configuration.GetSection("MailSettings").Get<MailSetting>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailSender, MailService>();

//builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IStorageService, FileStorageService>();

//builder.Services.AddTransient<UserManager<AspNetUser>, UserManager<AspNetUser>>();
builder.Services.AddMvc(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});
var app = builder.Build();
IConfiguration Configuration = app.Configuration;
IWebHostEnvironment environment = app.Environment;
// Configure the HTTP request pipeline.
app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
