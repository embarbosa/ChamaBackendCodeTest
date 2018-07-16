﻿// <auto-generated />
using Chama.WebApi.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Chama.WebApi.Migrations
{
    [DbContext(typeof(ChamaContext))]
    [Migration("20180716224532_CourseProcessedChanged")]
    partial class CourseProcessedChanged
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Chama.WebApi.Models.CourseModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MaxStudents");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("TeacherId");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Chama.WebApi.Models.ProcessedCourseModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("AverageAge");

                    b.Property<int>("CourseId");

                    b.Property<string>("CourseName");

                    b.Property<int>("CurrentNumberStudents");

                    b.Property<double>("MaxAge");

                    b.Property<int>("MaxStudents");

                    b.Property<double>("MinAge");

                    b.Property<int>("TeacherId");

                    b.Property<string>("TeacherName");

                    b.HasKey("Id");

                    b.ToTable("ProcessedCourses");
                });

            modelBuilder.Entity("Chama.WebApi.Models.StudentByCourseModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CourseId");

                    b.Property<int>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentsByCourse");
                });

            modelBuilder.Entity("Chama.WebApi.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Age");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Chama.WebApi.Models.CourseModel", b =>
                {
                    b.HasOne("Chama.WebApi.Models.UserModel", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Chama.WebApi.Models.StudentByCourseModel", b =>
                {
                    b.HasOne("Chama.WebApi.Models.CourseModel", "Course")
                        .WithMany("StudentsByCourse")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Chama.WebApi.Models.UserModel", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}