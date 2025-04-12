using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using webapi.Model.BaseEntities;
using webapi.Model.Identity;

namespace webapi.Data
{
    public class IdentityDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        private readonly ILoggerFactory? _loggerFactory;
        private string _contextUser;
        private readonly int? _contextUserId;
        public readonly ILogger<IdentityDbContext>? Logger;

        public IdentityDbContext(
            DbContextOptions<IdentityDbContext> options,
            IHttpContextAccessor httpContextAccessor,
            ILoggerFactory loggerFactory,
            ILogger<IdentityDbContext> logger
            ) : base(options)
        {
            _loggerFactory = loggerFactory;
            _contextUser = httpContextAccessor.HttpContext?.User.Identity?.Name ?? "IdentityDbContext";
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
        }

        #region Identity

        public override DbSet<User> Users { get; set; }
        public override DbSet<Role> Roles { get; set; }
        public override DbSet<UserRole> UserRoles { get; set; }
        public override DbSet<RoleClaim> RoleClaims { get; set; }
        public override DbSet<UserClaim> UserClaims { get; set; }
        public override DbSet<UserLogin> UserLogins { get; set; }
        public override DbSet<UserToken> UserTokens { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }

        #endregion Identity

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
                optionsBuilder.UseSqlServer("Data Source=localsql;Initial Catalog=DemoDB;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True;", o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangedNotifications);

            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.BuildIdentityModels();
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
                    contextUser = "IdentityDbContext";
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