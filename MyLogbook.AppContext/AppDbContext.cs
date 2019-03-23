﻿using Microsoft.EntityFrameworkCore;
using MyLogbook.Entities;
using System;
using System.Collections.Generic;

namespace MyLogbook.AppContext
{
    public class AppDbContext: DbContext
    {
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<ProfessorGroupLink> ProfessorGroupLinks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base (options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Guid faculty_une = Guid.NewGuid();
            Guid faculty_deux = Guid.NewGuid();
            Guid faculty_trois = Guid.NewGuid();
            Guid faculty_quatre = Guid.NewGuid();

            Guid group_une = Guid.NewGuid();
            Guid group_deux = Guid.NewGuid();
            Guid group_trois = Guid.NewGuid();
            Guid group_quatre = Guid.NewGuid();

            Guid department_une = Guid.NewGuid();
            Guid department_deux = Guid.NewGuid();
            Guid department_trois = Guid.NewGuid();
            Guid department_quatre = Guid.NewGuid();

            Guid professor_une = Guid.NewGuid();
            Guid professor_deux = Guid.NewGuid();
            Guid professor_trois = Guid.NewGuid();
            Guid professor_quatre = Guid.NewGuid();

            Guid student_une = Guid.NewGuid();
            Guid student_deux = Guid.NewGuid();
            Guid student_trois = Guid.NewGuid();
            Guid student_quatre = Guid.NewGuid();
            Guid student_cinq = Guid.NewGuid();
            Guid student_six = Guid.NewGuid();
            Guid student_sept = Guid.NewGuid();
            Guid student_huit = Guid.NewGuid();

            builder.Entity<Faculty>()
                .HasMany(f => f.Groups)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Faculty>()
                .HasMany(d => d.Departments)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Group>()
                .HasOne(g => g.Faculty)
                .WithMany(item => item.Groups)
                .HasForeignKey(g => g.FacultyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Department>()
                .HasOne(g => g.Faculty)
                .WithMany(item => item.Departments)
                .HasForeignKey(g => g.FacultyId);
            builder.Entity<Department>()
                .HasMany(p => p.Professors)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Professor>()
                .HasOne(p => p.Department)
                .WithMany(item => item.Professors)
                .HasForeignKey(g => g.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ProfessorGroupLink>()
                .HasKey(pgl => new { pgl.GroupId, pgl.ProfessorId });
            builder.Entity<ProfessorGroupLink>()
                .HasOne(pgl => pgl.Group)
                .WithMany(pgl => pgl.ProfessorGroupLinks)
                .HasForeignKey(pgl => pgl.GroupId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<ProfessorGroupLink>()
                .HasOne(pgl => pgl.Professor)
                .WithMany(pgl => pgl.ProfessorGroupLinks)
                .HasForeignKey(pgl => pgl.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Student>()
                .HasOne(p => p.Group)
                .WithMany(item => item.Students)
                .HasForeignKey(g => g.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            /*
             * First data for faculties 
             */
            builder.Entity<Faculty>().HasData(
                new Faculty { Id = faculty_une, Title = "Programming" },
                new Faculty { Id = faculty_deux, Title = "System administration and security" },
                new Faculty { Id = faculty_trois, Title = "Disign" },
                new Faculty { Id = faculty_quatre, Title = "Base" }
                );

            /*
            * First data for groups 
            */
            builder.Entity<Group>().HasData(
                new Group {Id = group_une, Title = "PR-01-1", FacultyId = faculty_une },
                new Group {Id = group_deux, Title = "PR-01-2", FacultyId = faculty_une },
                new Group {Id = group_trois, Title = "SAS-01-1", FacultyId = faculty_deux },
                new Group {Id = group_quatre, Title = "DN-01-1", FacultyId = faculty_trois }
                );

            /*
            * First data for departments 
            */
            builder.Entity<Department>().HasData(
                new Department { Id = department_une, Title = "Department NET", FacultyId = faculty_une },
                new Department { Id = department_deux, Title = "Department WEB", FacultyId = faculty_une },
                new Department { Id = department_trois, Title = "Department Draw", FacultyId = faculty_trois },
                new Department { Id = department_quatre, Title = "Department Network", FacultyId = faculty_deux }
                );

            /*
            * First data for professors 
            */
            builder.Entity<Professor>().HasData(
                new Professor { Id = professor_une, FirstName = "Jhon", LastName = "Conar", MiddleName = "Alexey", DepartmentId = department_une },
                new Professor { Id = professor_deux, FirstName = "Josef", LastName = "Millar", MiddleName = "Nguyen", DepartmentId = department_une },
                new Professor { Id = professor_trois, FirstName = "Elleonora", LastName = "Bo", MiddleName = "Olga", DepartmentId = department_deux },
                new Professor { Id = professor_quatre, FirstName = "Philip", LastName = "Chan", MiddleName = "Djordje", DepartmentId = department_trois }
                );

            /*
            * First data for ProfessorGroupLink 
            */
            builder.Entity<ProfessorGroupLink>().HasData(
                new ProfessorGroupLink { GroupId = group_une, ProfessorId = professor_une },
                new ProfessorGroupLink { GroupId = group_une, ProfessorId = professor_deux },
                new ProfessorGroupLink { GroupId = group_deux, ProfessorId = professor_trois },
                new ProfessorGroupLink { GroupId = group_deux, ProfessorId = professor_quatre }
                );

            /*
            * First data for students 
             */
            builder.Entity<Student>().HasData(
                new Student { Id = student_une, FirstName = "Ivanov", LastName = "Ivan", MiddleName = "Ivanovich", GroupId = group_une },
                new Student { Id = student_deux, FirstName = "Ivanova", LastName = "Ivanna", MiddleName = "Ivanovna", GroupId = group_une },
                new Student { Id = student_trois, FirstName = "Petrov", LastName = "Petr", MiddleName = "Petrovich", GroupId = group_deux },
                new Student { Id = student_quatre, FirstName = "Petrova", LastName = "Petra", MiddleName = "Petrovna", GroupId = group_une },
                new Student { Id = student_cinq, FirstName = "Sidorov", LastName = "Sidor", MiddleName = "Sidorovich", GroupId = group_deux },
                new Student { Id = student_six, FirstName = "Sidorova", LastName = "Sidora", MiddleName = "Sidorovna", GroupId = group_une },
                new Student { Id = student_sept, FirstName = "Malina", LastName = "Malin", MiddleName = "Milanovich", GroupId = group_une },
                new Student { Id = student_huit, FirstName = "Malina", LastName = "Mila", MiddleName = "Milanovna", GroupId = group_deux }
                );
        }
    }
}
