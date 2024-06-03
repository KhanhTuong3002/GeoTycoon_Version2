﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(GeoTycoonDbcontext))]
    partial class GeoTycoonDbcontextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BusinessObject.Entites.Game", b =>
                {
                    b.Property<string>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GameName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SetQuestionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("GameId");

                    b.HasIndex("SetQuestionId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("BusinessObject.Entites.GameSession", b =>
                {
                    b.Property<string>("SessionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccessCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("GameId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PhotonRoomId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SetQuestionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("SessionID");

                    b.HasIndex("GameId");

                    b.ToTable("GameSessions");
                });

            modelBuilder.Entity("BusinessObject.Entites.Profile", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<bool>("Isbanned")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("BusinessObject.Entites.Province", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("province_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ProvinceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("provinceName");

                    b.HasKey("Id");

                    b.ToTable("Province", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 2,
                            ProvinceName = "An Giang"
                        },
                        new
                        {
                            Id = 3,
                            ProvinceName = "Bà Rịa - Vũng Tàu"
                        },
                        new
                        {
                            Id = 4,
                            ProvinceName = "Bắc Giang"
                        },
                        new
                        {
                            Id = 5,
                            ProvinceName = "Bắc Kạn"
                        },
                        new
                        {
                            Id = 6,
                            ProvinceName = "Bạc Liêu"
                        },
                        new
                        {
                            Id = 7,
                            ProvinceName = "Bắc Ninh"
                        },
                        new
                        {
                            Id = 8,
                            ProvinceName = "Bến Tre"
                        },
                        new
                        {
                            Id = 9,
                            ProvinceName = "Bình Định"
                        },
                        new
                        {
                            Id = 10,
                            ProvinceName = "Bình Dương"
                        },
                        new
                        {
                            Id = 11,
                            ProvinceName = "Bình Phước"
                        },
                        new
                        {
                            Id = 12,
                            ProvinceName = "Bình Thuận"
                        },
                        new
                        {
                            Id = 13,
                            ProvinceName = "Cà Mau"
                        },
                        new
                        {
                            Id = 14,
                            ProvinceName = "Cao Bằng"
                        },
                        new
                        {
                            Id = 15,
                            ProvinceName = "Đắk Lắk"
                        },
                        new
                        {
                            Id = 16,
                            ProvinceName = "Đắk Nông"
                        },
                        new
                        {
                            Id = 17,
                            ProvinceName = "Điện Biên"
                        },
                        new
                        {
                            Id = 18,
                            ProvinceName = "Đồng Nai"
                        },
                        new
                        {
                            Id = 19,
                            ProvinceName = "Đồng Tháp"
                        },
                        new
                        {
                            Id = 20,
                            ProvinceName = "Gia Lai"
                        },
                        new
                        {
                            Id = 21,
                            ProvinceName = "Hà Giang"
                        },
                        new
                        {
                            Id = 22,
                            ProvinceName = "Hà Nam"
                        },
                        new
                        {
                            Id = 23,
                            ProvinceName = "Hà Nội"
                        },
                        new
                        {
                            Id = 24,
                            ProvinceName = "Hà Tĩnh"
                        },
                        new
                        {
                            Id = 25,
                            ProvinceName = "Hải Dương"
                        },
                        new
                        {
                            Id = 26,
                            ProvinceName = "Hải Phòng"
                        },
                        new
                        {
                            Id = 27,
                            ProvinceName = "Hậu Giang"
                        },
                        new
                        {
                            Id = 28,
                            ProvinceName = "Hòa Bình"
                        },
                        new
                        {
                            Id = 29,
                            ProvinceName = "Hưng Yên"
                        },
                        new
                        {
                            Id = 30,
                            ProvinceName = "Khánh Hòa"
                        },
                        new
                        {
                            Id = 31,
                            ProvinceName = "Kiên Giang"
                        },
                        new
                        {
                            Id = 32,
                            ProvinceName = "Kon Tum"
                        },
                        new
                        {
                            Id = 33,
                            ProvinceName = "Lai Châu"
                        },
                        new
                        {
                            Id = 34,
                            ProvinceName = "Lâm Đồng"
                        },
                        new
                        {
                            Id = 35,
                            ProvinceName = "Lạng Sơn"
                        },
                        new
                        {
                            Id = 36,
                            ProvinceName = "Lào Cai"
                        },
                        new
                        {
                            Id = 37,
                            ProvinceName = "Long An"
                        },
                        new
                        {
                            Id = 38,
                            ProvinceName = "Nam Định"
                        },
                        new
                        {
                            Id = 39,
                            ProvinceName = "Nghệ An"
                        },
                        new
                        {
                            Id = 40,
                            ProvinceName = "Ninh Bình"
                        },
                        new
                        {
                            Id = 41,
                            ProvinceName = "Ninh Thuận"
                        },
                        new
                        {
                            Id = 42,
                            ProvinceName = "Phú Thọ"
                        },
                        new
                        {
                            Id = 43,
                            ProvinceName = "Phú Yên"
                        },
                        new
                        {
                            Id = 44,
                            ProvinceName = "Quảng Bình"
                        },
                        new
                        {
                            Id = 45,
                            ProvinceName = "Quảng Nam"
                        },
                        new
                        {
                            Id = 46,
                            ProvinceName = "Quảng Ngãi"
                        },
                        new
                        {
                            Id = 47,
                            ProvinceName = "Quảng Ninh"
                        },
                        new
                        {
                            Id = 48,
                            ProvinceName = "Quảng Trị"
                        },
                        new
                        {
                            Id = 49,
                            ProvinceName = "Sóc Trăng"
                        },
                        new
                        {
                            Id = 50,
                            ProvinceName = "Sơn La"
                        },
                        new
                        {
                            Id = 51,
                            ProvinceName = "Tây Ninh"
                        },
                        new
                        {
                            Id = 52,
                            ProvinceName = "Thái Bình"
                        },
                        new
                        {
                            Id = 53,
                            ProvinceName = "Thái Nguyên"
                        },
                        new
                        {
                            Id = 54,
                            ProvinceName = "Thanh Hóa"
                        },
                        new
                        {
                            Id = 55,
                            ProvinceName = "Thừa Thiên Huế"
                        },
                        new
                        {
                            Id = 56,
                            ProvinceName = "Tiền Giang"
                        },
                        new
                        {
                            Id = 57,
                            ProvinceName = "Trà Vinh"
                        },
                        new
                        {
                            Id = 58,
                            ProvinceName = "Tuyên Quang"
                        },
                        new
                        {
                            Id = 59,
                            ProvinceName = "Vĩnh Long"
                        },
                        new
                        {
                            Id = 60,
                            ProvinceName = "Vĩnh Phúc"
                        },
                        new
                        {
                            Id = 61,
                            ProvinceName = "Yên Bái"
                        });
                });

            modelBuilder.Entity("BusinessObject.Entites.Question", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("question_id");

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(600)
                        .HasColumnType("nvarchar(600)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2500)
                        .HasColumnType("nvarchar(2500)");

                    b.Property<string>("Images")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Option1")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Option2")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Option3")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Option4")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("ProvinceId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Published")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ProvinceId");

                    b.HasIndex("UserId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("BusinessObject.Entites.SetQuestion", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("QuestionNumber")
                        .HasColumnType("int");

                    b.Property<string>("SetName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("SetQuestions");
                });

            modelBuilder.Entity("BusinessObject.Entites.SetQuestionDetail", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("QuestionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SetQuestionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("SetQuestionId");

                    b.ToTable("SetQuestionDetails");
                });

            modelBuilder.Entity("BusinessObject.Entites.Tracking", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("tracking_id");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NewValues")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OldValues")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Trackings");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "5db782a7-9aab-4cc4-8aa6-7befcd5360cd",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = "5b2b0df2-51c0-40ca-a510-85d33acb9a6e",
                            Name = "Teacher",
                            NormalizedName = "TEACHER"
                        },
                        new
                        {
                            Id = "189e5fac-9187-450c-92a8-56cf1068964a",
                            Name = "Pending",
                            NormalizedName = "PENDING"
                        },
                        new
                        {
                            Id = "0489f5a4-85dd-40c6-9b87-efad0c10db8d",
                            Name = "Student",
                            NormalizedName = "STUDENT"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("BusinessObject.Entites.Game", b =>
                {
                    b.HasOne("BusinessObject.Entites.SetQuestion", "SetQuestion")
                        .WithMany()
                        .HasForeignKey("SetQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SetQuestion");
                });

            modelBuilder.Entity("BusinessObject.Entites.GameSession", b =>
                {
                    b.HasOne("BusinessObject.Entites.Game", "Games")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Games");
                });

            modelBuilder.Entity("BusinessObject.Entites.Profile", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BusinessObject.Entites.Question", b =>
                {
                    b.HasOne("BusinessObject.Entites.Province", "Province")
                        .WithMany()
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Province");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BusinessObject.Entites.SetQuestionDetail", b =>
                {
                    b.HasOne("BusinessObject.Entites.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObject.Entites.SetQuestion", "SetQuestion")
                        .WithMany()
                        .HasForeignKey("SetQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("SetQuestion");
                });

            modelBuilder.Entity("BusinessObject.Entites.Tracking", b =>
                {
                    b.HasOne("BusinessObject.Entites.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
