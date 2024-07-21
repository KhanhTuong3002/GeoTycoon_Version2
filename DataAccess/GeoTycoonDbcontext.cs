using BusinessObject.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess
{
    public partial class GeoTycoonDbcontext : IdentityDbContext
    {
        public GeoTycoonDbcontext() { }

        public GeoTycoonDbcontext(DbContextOptions<GeoTycoonDbcontext> options) : base(options) { }

        public virtual DbSet<Question> Questions { get; init; } = default!;
        public virtual DbSet<Profile> Profiles { get; init; } = default!;
        public virtual DbSet<SetQuestion> SetQuestions { get; init; } = default!;
        public virtual DbSet<Tracking> Trackings { get; init; } = default!;
        public virtual DbSet<SetQuestionDetail> SetQuestionDetails { get; init; } = default!;
        public virtual DbSet<Game> Games { get; init; } = default!;
        public virtual DbSet<GameSession> GameSessions { get; init; } = default!;
        public virtual DbSet<Province> Provinces { get; init; } = default!;
        public override int SaveChanges()
        {
            AddTrackingEntries();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTrackingEntries();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void AddTrackingEntries()
        {
            var trackingEntries = new List<Tracking>();
            var entries = ChangeTracker.Entries<Question>().ToList();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    var tracking = new Tracking
                    {
                        Id = Guid.NewGuid().ToString(),
                        QuestionId = entry.Entity.Id,
                        UserId = entry.Entity.UserId,
                        Action = "Added",
                        OldValues = "No value",
                        NewValues = JsonConvert.SerializeObject(entry.CurrentValues.ToObject()),
                        CreatedAt = DateTime.UtcNow
                    };
                    trackingEntries.Add(tracking);
                }
                else if (entry.State == EntityState.Modified)
                {
                    var lastTracking = Trackings
                        .Where(t => t.QuestionId == entry.Entity.Id)
                        .OrderByDescending(t => t.CreatedAt)
                        .FirstOrDefault();

                    var tracking = new Tracking
                    {
                        Id = Guid.NewGuid().ToString(),
                        QuestionId = entry.Entity.Id,
                        UserId = entry.Entity.UserId,
                        Action = "Updated",
                        OldValues = lastTracking?.NewValues ?? "No previous data",
                        NewValues = JsonConvert.SerializeObject(entry.CurrentValues.ToObject()),
                        CreatedAt = DateTime.UtcNow
                    };
                    trackingEntries.Add(tracking);
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Unchanged; // Tạm thời đặt lại trạng thái để lấy giá trị hiện tại
                    var currentValues = entry.CurrentValues.ToObject();

                    var tracking = new Tracking
                    {
                        Id = Guid.NewGuid().ToString(),
                        QuestionId = entry.Entity.Id,
                        UserId = entry.Entity.UserId,
                        Action = "Deleted",
                        OldValues = JsonConvert.SerializeObject(currentValues),
                        NewValues = "No value",
                        CreatedAt = DateTime.UtcNow
                    };

                    trackingEntries.Add(tracking); // Thêm tracking vào danh sách trước khi xóa thực thể
                    entry.State = EntityState.Deleted; // Đặt lại trạng thái thành Deleted
                }
            }

            Trackings.AddRange(trackingEntries);
        }








        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                server => server.EnableRetryOnFailure());
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Province>().ToTable("Province").HasData
                (new Province { Id = 2, ProvinceName = "An Giang" },
                new Province { Id = 3, ProvinceName = "Bà Rịa - Vũng Tàu" },
                new Province { Id = 4, ProvinceName = "Bắc Giang" },
                new Province { Id = 5, ProvinceName = "Bắc Kạn" },
                new Province { Id = 6, ProvinceName = "Bạc Liêu" },
                new Province { Id = 7, ProvinceName = "Bắc Ninh" },
                new Province { Id = 8, ProvinceName = "Bến Tre" },
                new Province { Id = 9, ProvinceName = "Bình Định" },
                new Province { Id = 10, ProvinceName = "Bình Dương" },
                new Province { Id = 11, ProvinceName = "Bình Phước" },
                new Province { Id = 12, ProvinceName = "Bình Thuận" },
                new Province { Id = 13, ProvinceName = "Cà Mau" },
                new Province { Id = 14, ProvinceName = "Cao Bằng" },
                new Province { Id = 15, ProvinceName = "Đắk Lắk" },
                new Province { Id = 16, ProvinceName = "Đắk Nông" },
                new Province { Id = 17, ProvinceName = "Điện Biên" },
                new Province { Id = 18, ProvinceName = "Đồng Nai" },
                new Province { Id = 19, ProvinceName = "Đồng Tháp" },
                new Province { Id = 20, ProvinceName = "Gia Lai" },
                new Province { Id = 21, ProvinceName = "Hà Giang" },
                new Province { Id = 22, ProvinceName = "Hà Nam" },
                new Province { Id = 23, ProvinceName = "Hà Nội" },
                new Province { Id = 24, ProvinceName = "Hà Tĩnh" },
                new Province { Id = 25, ProvinceName = "Hải Dương" },
                new Province { Id = 26, ProvinceName = "Hải Phòng" },
                new Province { Id = 27, ProvinceName = "Hậu Giang" },
                new Province { Id = 28, ProvinceName = "Hòa Bình" },
                new Province { Id = 29, ProvinceName = "Hưng Yên" },
                new Province { Id = 30, ProvinceName = "Khánh Hòa" },
                new Province { Id = 31, ProvinceName = "Kiên Giang" },
                new Province { Id = 32, ProvinceName = "Kon Tum" },
                new Province { Id = 33, ProvinceName = "Lai Châu" },
                new Province { Id = 34, ProvinceName = "Lâm Đồng" },
                new Province { Id = 35, ProvinceName = "Lạng Sơn" },
                new Province { Id = 36, ProvinceName = "Lào Cai" },
                new Province { Id = 37, ProvinceName = "Long An" },
                new Province { Id = 38, ProvinceName = "Nam Định" },
                new Province { Id = 39, ProvinceName = "Nghệ An" },
                new Province { Id = 40, ProvinceName = "Ninh Bình" },
                new Province { Id = 41, ProvinceName = "Ninh Thuận" },
                new Province { Id = 42, ProvinceName = "Phú Thọ" },
                new Province { Id = 43, ProvinceName = "Phú Yên" },
                new Province { Id = 44, ProvinceName = "Quảng Bình" },
                new Province { Id = 45, ProvinceName = "Quảng Nam" },
                new Province { Id = 46, ProvinceName = "Quảng Ngãi" },
                new Province { Id = 47, ProvinceName = "Quảng Ninh" },
                new Province { Id = 48, ProvinceName = "Quảng Trị" },
                new Province { Id = 49, ProvinceName = "Sóc Trăng" },
                new Province { Id = 50, ProvinceName = "Sơn La" },
                new Province { Id = 51, ProvinceName = "Tây Ninh" },
                new Province { Id = 52, ProvinceName = "Thái Bình" },
                new Province { Id = 53, ProvinceName = "Thái Nguyên" },
                new Province { Id = 54, ProvinceName = "Thanh Hóa" },
                new Province { Id = 55, ProvinceName = "Thừa Thiên Huế" },
                new Province { Id = 56, ProvinceName = "Tiền Giang" },
                new Province { Id = 57, ProvinceName = "Trà Vinh" },
                new Province { Id = 58, ProvinceName = "Tuyên Quang" },
                new Province { Id = 59, ProvinceName = "Vĩnh Long" },
                new Province { Id = 60, ProvinceName = "Vĩnh Phúc" },
                new Province { Id = 61, ProvinceName = "Yên Bái" });

            builder.Entity<IdentityRole>().HasData(Default.Roles);
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.ApplyConfigurationsFromAssembly(typeof(GeoTycoonDbcontext).Assembly);
        }
    }
}
