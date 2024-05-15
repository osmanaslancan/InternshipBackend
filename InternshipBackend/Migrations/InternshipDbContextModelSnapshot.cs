﻿// <auto-generated />
using System;
using InternshipBackend.Data;
using InternshipBackend.Data.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InternshipBackend.Migrations
{
    [DbContext(typeof(InternshipDbContext))]
    partial class InternshipDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("InternshipBackend.Data.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CountryId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities", "public");
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AdminUserId")
                        .HasColumnType("integer");

                    b.Property<string>("BackgroundPhotoUrl")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int?>("CityId")
                        .HasColumnType("integer");

                    b.Property<int?>("CountryId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("boolean");

                    b.Property<string>("LogoUrl")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("NumberOfWorkers")
                        .HasColumnType("integer");

                    b.Property<string>("Sector")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ShortDescription")
                        .HasMaxLength(75)
                        .HasColumnType("character varying(75)");

                    b.Property<string>("WebsiteUrl")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.HasIndex("AdminUserId")
                        .IsUnique();

                    b.HasIndex("CityId");

                    b.HasIndex("CountryId");

                    b.ToTable("Companies", "public");
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.CompanyEmployee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CompanyId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("UserId");

                    b.ToTable("CompanyEmployees", "public");
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Code3")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("PhoneCode")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("Countries", "public");
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.DbSeed", b =>
                {
                    b.Property<string>("SeederName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("AppliedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("SeederName");

                    b.ToTable("DbSeeds", "public");
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.ForeignLanguage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Degree")
                        .HasColumnType("integer");

                    b.Property<string>("LanguageCode")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ForeignLanguages", "public");
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.InternshipApplication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CvUrl")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("InternshipPostingId")
                        .HasColumnType("integer");

                    b.Property<string>("Message")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("InternshipPostingId");

                    b.HasIndex("UserId");

                    b.ToTable("InternshipApplication", "public");
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.InternshipPosting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("BackgroundPhotoUrl")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int?>("CityId")
                        .HasColumnType("integer");

                    b.Property<int>("CompanyId")
                        .HasColumnType("integer");

                    b.Property<int?>("CountryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DeadLine")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<int>("EmploymentType")
                        .HasColumnType("integer");

                    b.Property<bool>("HasSalary")
                        .HasColumnType("boolean");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Location")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Requirements")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Sector")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("WorkType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("CountryId");

                    b.ToTable("InternshipPostings", "public");
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.Supabase.StorageObject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BucketId")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("storage.objects", "public", t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.University", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("Universities", "public");
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.UniversityEducation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<int?>("EducationYear")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("Faculty")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<double>("GPA")
                        .HasColumnType("double precision");

                    b.Property<bool>("IsGraduated")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("date");

                    b.Property<int?>("UniversityId")
                        .HasColumnType("integer");

                    b.Property<string>("UniversityName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UniversityId");

                    b.HasIndex("UserId");

                    b.ToTable("UniversityEducations", "public");
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountType")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ProfilePhotoUrl")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid>("SupabaseId")
                        .HasColumnType("uuid");

                    b.Property<string>("Surname")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("SupabaseId")
                        .IsUnique();

                    b.ToTable("Users", "public");
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.UserCompanyFollow", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("CompanyId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("UserId", "CompanyId");

                    b.HasIndex("CompanyId");

                    b.ToTable("UserCompanyFollow", "public");
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.UserDetail", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int?>("CityId")
                        .HasColumnType("integer");

                    b.Property<int?>("CountryId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("District")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string[]>("DriverLicenses")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<UserDetailExtras>("Extras")
                        .HasColumnType("jsonb");

                    b.Property<int?>("Gender")
                        .HasColumnType("integer");

                    b.Property<int?>("MaritalStatus")
                        .HasColumnType("integer");

                    b.Property<int?>("MilitaryStatus")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("CountryId");

                    b.ToTable("UserDetails", "public");
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.UserPermission", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("Permission")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("UserId", "Permission");

                    b.ToTable("UserPermission", "public");
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.UserPostingFollow", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("PostingId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("UserId", "PostingId");

                    b.HasIndex("PostingId");

                    b.ToTable("UserPostingFollow", "public");
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.UserProject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("ProjectLink")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ProjectThumbnail")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserProjects", "public");
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.UserReference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Duty")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserReference", "public");
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.WorkHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Duties")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("date");

                    b.Property<bool>("IsWorkingNow")
                        .HasColumnType("boolean");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ReasonForLeave")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("date");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("WorkType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("WorkHistories", "public");
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.City", b =>
                {
                    b.HasOne("InternshipBackend.Data.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.Company", b =>
                {
                    b.HasOne("InternshipBackend.Data.Models.User", "AdminUser")
                        .WithOne()
                        .HasForeignKey("InternshipBackend.Data.Models.Company", "AdminUserId");

                    b.HasOne("InternshipBackend.Data.Models.City", null)
                        .WithMany()
                        .HasForeignKey("CityId");

                    b.HasOne("InternshipBackend.Data.Models.Country", null)
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.Navigation("AdminUser");
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.CompanyEmployee", b =>
                {
                    b.HasOne("InternshipBackend.Data.Models.Company", "Company")
                        .WithMany("Employees")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InternshipBackend.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("User");
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.ForeignLanguage", b =>
                {
                    b.HasOne("InternshipBackend.Data.Models.User", null)
                        .WithMany("ForeignLanguages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.InternshipApplication", b =>
                {
                    b.HasOne("InternshipBackend.Data.Models.InternshipPosting", null)
                        .WithMany("Applications")
                        .HasForeignKey("InternshipPostingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InternshipBackend.Data.Models.User", null)
                        .WithMany("Applications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.InternshipPosting", b =>
                {
                    b.HasOne("InternshipBackend.Data.Models.City", null)
                        .WithMany()
                        .HasForeignKey("CityId");

                    b.HasOne("InternshipBackend.Data.Models.Company", null)
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InternshipBackend.Data.Models.Country", null)
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.OwnsMany("InternshipBackend.Data.Models.ValueObjects.InternshipPostingComment", "Comments", b1 =>
                        {
                            b1.Property<int>("InternshipPostingId")
                                .HasColumnType("integer");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            b1.Property<string>("Comment")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("character varying(255)");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<int>("Points")
                                .HasColumnType("integer");

                            b1.Property<int>("UserId")
                                .HasColumnType("integer");

                            b1.HasKey("InternshipPostingId", "Id");

                            b1.ToTable("InternshipPostings", "public");

                            b1.ToJson("Comments");

                            b1.WithOwner()
                                .HasForeignKey("InternshipPostingId");
                        });

                    b.Navigation("Comments");
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.UniversityEducation", b =>
                {
                    b.HasOne("InternshipBackend.Data.Models.University", "University")
                        .WithMany()
                        .HasForeignKey("UniversityId");

                    b.HasOne("InternshipBackend.Data.Models.User", null)
                        .WithMany("UniversityEducations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("University");
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.UserCompanyFollow", b =>
                {
                    b.HasOne("InternshipBackend.Data.Models.Company", null)
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InternshipBackend.Data.Models.User", null)
                        .WithMany("FollowedCompanies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.UserDetail", b =>
                {
                    b.HasOne("InternshipBackend.Data.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId");

                    b.HasOne("InternshipBackend.Data.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.HasOne("InternshipBackend.Data.Models.User", "User")
                        .WithOne("Detail")
                        .HasForeignKey("InternshipBackend.Data.Models.UserDetail", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("InternshipBackend.Data.Models.ValueObjects.UserCv", "Cvs", b1 =>
                        {
                            b1.Property<int>("UserDetailId")
                                .HasColumnType("integer");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            b1.Property<string>("FileName")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("character varying(255)");

                            b1.Property<string>("FileUrl")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("character varying(255)");

                            b1.HasKey("UserDetailId", "Id");

                            b1.ToTable("UserDetails", "public");

                            b1.ToJson("Cvs");

                            b1.WithOwner()
                                .HasForeignKey("UserDetailId");
                        });

                    b.Navigation("City");

                    b.Navigation("Country");

                    b.Navigation("Cvs");

                    b.Navigation("User");
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.UserPermission", b =>
                {
                    b.HasOne("InternshipBackend.Data.Models.User", null)
                        .WithMany("UserPermissions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.UserPostingFollow", b =>
                {
                    b.HasOne("InternshipBackend.Data.Models.InternshipPosting", null)
                        .WithMany()
                        .HasForeignKey("PostingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InternshipBackend.Data.Models.User", null)
                        .WithMany("FollowedPostings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.UserProject", b =>
                {
                    b.HasOne("InternshipBackend.Data.Models.User", "User")
                        .WithMany("Projects")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.UserReference", b =>
                {
                    b.HasOne("InternshipBackend.Data.Models.User", null)
                        .WithMany("References")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InternshipBackend.Data.Models.UserDetail", null)
                        .WithMany("References")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.WorkHistory", b =>
                {
                    b.HasOne("InternshipBackend.Data.Models.User", null)
                        .WithMany("Works")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.Company", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.InternshipPosting", b =>
                {
                    b.Navigation("Applications");
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.User", b =>
                {
                    b.Navigation("Applications");

                    b.Navigation("Detail");

                    b.Navigation("FollowedCompanies");

                    b.Navigation("FollowedPostings");

                    b.Navigation("ForeignLanguages");

                    b.Navigation("Projects");

                    b.Navigation("References");

                    b.Navigation("UniversityEducations");

                    b.Navigation("UserPermissions");

                    b.Navigation("Works");
                });

            modelBuilder.Entity("InternshipBackend.Data.Models.UserDetail", b =>
                {
                    b.Navigation("References");
                });
#pragma warning restore 612, 618
        }
    }
}
