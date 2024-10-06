using Microsoft.EntityFrameworkCore;
using SOPGraphQL;
using SOPGraphQL.MappingProfiles;
using SOPGraphQL.Mutations;
using SOPGraphQL.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<UserQuery>();
builder.Services.AddScoped<PromotionQuery>();
builder.Services.AddScoped<MenuItemQuery>();
builder.Services.AddScoped<OrderQuery>();
builder.Services.AddScoped<OrderItemQuery>();
builder.Services.AddScoped<UserMutation>();
builder.Services.AddScoped<PromotionMutation>();
builder.Services.AddScoped<MenuItemMutation>();
builder.Services.AddScoped<OrderMutation>();
builder.Services.AddScoped<OrderItemMutation>();
builder.Services
    .AddGraphQLServer()
    .AddQueryType(d => d.Name("Query"))
    .AddTypeExtension<UserQuery>()
    .AddTypeExtension<PromotionQuery>()
    .AddTypeExtension<MenuItemQuery>()
    .AddTypeExtension<OrderQuery>()
    .AddTypeExtension<OrderItemQuery>()
    .AddMutationType<UserMutation>()
    .AddMutationType<PromotionMutation>()
    .AddMutationType<MenuItemMutation>()
    .AddMutationType<OrderMutation>()
    .AddMutationType<OrderItemMutation>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapGraphQL();

app.Run();

