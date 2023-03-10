using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TaskManagement.Models
{
    public partial class TaskManagementContext : DbContext
    {
        public TaskManagementContext()
        {
        }

        public TaskManagementContext(DbContextOptions<TaskManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Section> Sections { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<Task> Tasks { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserSectionRole> UserSectionRoles { get; set; } = null!;
        public virtual DbSet<UserTaskRole> UserTaskRoles { get; set; } = null!;
        public virtual DbSet<UserWorkSpaceRole> UserWorkSpaceRoles { get; set; } = null!;
        public virtual DbSet<WorkSpace> WorkSpaces { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-GBO8FJS;Initial Catalog=TaskManagement;User ID=sa;Password=123;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_Comment_Task");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_User");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.Property(e => e.UserActiveId).HasColumnName("UserActiveID");

                entity.Property(e => e.UserPassiveId).HasColumnName("UserPassiveID");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_Notification_Task");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.ToTable("Section");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.WorkSpaceId).HasColumnName("WorkSpaceID");

                entity.HasOne(d => d.WorkSpace)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.WorkSpaceId)
                    .HasConstraintName("FK_Section_WorkSpace1");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("Tag");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("Task");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.SectionId).HasColumnName("SectionID");

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.TaskFrom).HasColumnType("datetime");

                entity.Property(e => e.TaskTo).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.SectionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Task_Section1");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.TagId)
                    .HasConstraintName("FK_Task_Tag");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(16);

                entity.Property(e => e.Phone).HasMaxLength(10);

                entity.Property(e => e.UserName).HasMaxLength(16);

                entity.Property(e => e.Work).HasMaxLength(50);
            });

            modelBuilder.Entity<UserSectionRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.SectionId, e.RoleId });

                entity.ToTable("UserSectionRole");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.SectionId).HasColumnName("SectionID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserSectionRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserSectionRole_Role");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.UserSectionRoles)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_UserSectionRole_Section1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSectionRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserSectionRole_User1");
            });

            modelBuilder.Entity<UserTaskRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.TaskId, e.RoleId });

                entity.ToTable("UserTaskRole");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserTaskRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserTaskRole_Role");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.UserTaskRoles)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_UserTaskRole_Task1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTaskRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserTaskRole_User1");
            });

            modelBuilder.Entity<UserWorkSpaceRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.WorkSpaceId, e.RoleId });

                entity.ToTable("UserWorkSpaceRole");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.WorkSpaceId).HasColumnName("WorkSpaceID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserWorkSpaceRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserWorkSpaceRole_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserWorkSpaceRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserWorkSpaceRole_User1");

                entity.HasOne(d => d.WorkSpace)
                    .WithMany(p => p.UserWorkSpaceRoles)
                    .HasForeignKey(d => d.WorkSpaceId)
                    .HasConstraintName("FK_UserWorkSpaceRole_WorkSpace1");
            });

            modelBuilder.Entity<WorkSpace>(entity =>
            {
                entity.ToTable("WorkSpace");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Describe).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
