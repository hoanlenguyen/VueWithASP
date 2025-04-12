using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using webapi.Extensions;
using webapi.Model.BaseEntities;
using webapi.Model.Products;

namespace webapi.Data
{
    public class StoreDbContext : DbContext
    {
        private readonly bool _disposed;
        private readonly ILoggerFactory? _loggerFactory;
        private string _contextUser;
        private readonly int? _contextUserId;

        public int? ContextUserId => _contextUserId;

        public ExtensionHost Extensions { get; set; } = new();
        public ILogger<StoreDbContext>? Logger { get; }

        public StoreDbContext(
            DbContextOptions<StoreDbContext> options,
            //QueryableVisitor<IQueryableVisitor<IModel>, IModel>? queryableVisitor,
            //ModelObserver? observer,
            IHttpContextAccessor httpContextAccessor,
            ILoggerFactory loggerFactory,
            ILogger<StoreDbContext> logger
            ) : base(options)
        {
            _loggerFactory = loggerFactory;
            _contextUser = httpContextAccessor.HttpContext?.User.Identity?.Name ?? "StoreDbContext";
            Logger = logger;
            //if (httpContextAccessor?.HttpContext?.User?.Identity?.GetIdentityUserId(out var userIdentity) ?? false)
            //{
            //    _contextUserId = userIdentity;
            //}

            if (this.Database.IsRelational())
            {
                var connection = this.Database.GetDbConnection();
                connection.StateChange += OnConnectionStateChange;
            }

            SavingChanges += OnSavingChanges;
            //if (queryableVisitor is not null)
            //{
            //    Extensions.TrySetExtension<IQueryableVisitor<IQueryableVisitor<IModel>, IModel>>(queryableVisitor);
            //}
            //if (observer is not null)
            //{
            //    Extensions.TrySetExtension<IPluginModelObserver>(observer);
            //}
            //if (htmlSanitisation is not null)
            //{
            //    Extensions.TrySetExtension<IHtmlSanitisation>(htmlSanitisation);
            //}
        }
        #region Product

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }

        #endregion Product

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
#if DEBUG
            optionsBuilder.LogTo((s) => Debug.WriteLine(s), LogLevel.Debug);
            optionsBuilder.EnableSensitiveDataLogging().LogTo((s) => Debug.WriteLine(s), LogLevel.Debug);
#endif
            if (_loggerFactory != null)
            {
                optionsBuilder.UseLoggerFactory(_loggerFactory).EnableDetailedErrors();
            }

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.EnableSensitiveDataLogging(true);
                optionsBuilder.UseSqlServer("Data Source=localsql;Initial Catalog=DemoDB;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True;",
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangedNotifications);

            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.BuildProductModels();
        }

        private void OnSavingChanges(object? sender, SavingChangesEventArgs? e)
        {
            foreach (var entry in ChangeTracker.Entries().ToList())
            {
                if (entry.Entity is IAuditModel auditModel && (entry.State == EntityState.Added || entry.State == EntityState.Modified))
                {
                    auditModel.ChangedByUser = _contextUser;
                }
            }
        }

        private void OnConnectionStateChange(object sender, StateChangeEventArgs e)
        {
            SetContextUser(_contextUser);
        }

        private void SetContextUser(string contextUser)
        {
            if (_contextUser != contextUser)
            {
                if (string.IsNullOrWhiteSpace(contextUser))
                {
                    contextUser = "StoreDbContext";
                }
                _contextUser = contextUser;
            }

            var dbConnection = Database.GetDbConnection();
            if (dbConnection.State == ConnectionState.Open)
            {
                using DbCommand cmd = dbConnection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"exec sp_set_session_context 'ContextUser', N'{_contextUser}'";
                cmd.ExecuteNonQuery();
            }
        }
    }
}