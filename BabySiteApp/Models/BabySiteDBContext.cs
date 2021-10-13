using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BabySiteServerBL.Models
{
    public partial class BabySiteDBContext : DbContext
    {
        public BabySiteDBContext()
        {
        }

        public BabySiteDBContext(DbContextOptions<BabySiteDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<BabySitter> BabySitters { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Massage> Massages { get; set; }
        public virtual DbSet<MassageType> MassageTypes { get; set; }
        public virtual DbSet<Parent> Parents { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<RequestStatus> RequestStatuses { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Database=BabySiteDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Hebrew_CI_AS");

            modelBuilder.Entity<Area>(entity =>
            {
                entity.ToTable("Area");

                entity.Property(e => e.AreaId).ValueGeneratedNever();
            });

            modelBuilder.Entity<BabySitter>(entity =>
            {
                entity.ToTable("BabySitter");

                entity.Property(e => e.BabySitterId).ValueGeneratedNever();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BabySitters)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("babysitter_userid_foreign");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.CityId).ValueGeneratedNever();

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.AreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("city_areaid_foreign");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.Property(e => e.LocationId).ValueGeneratedNever();

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("location_cityid_foreign");
            });

            modelBuilder.Entity<Massage>(entity =>
            {
                entity.ToTable("Massage");

                entity.Property(e => e.MassageId).ValueGeneratedNever();

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.HeadLine)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.MassageType)
                    .WithMany(p => p.Massages)
                    .HasForeignKey(d => d.MassageTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("massage_massagetypeid_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Massages)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("massage_userid_foreign");
            });

            modelBuilder.Entity<MassageType>(entity =>
            {
                entity.ToTable("MassageType");

                entity.Property(e => e.MassageTypeId).ValueGeneratedNever();

                entity.Property(e => e.MassageTypeName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.MassageTypes)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("massagetype_usertypeid_foreign");
            });

            modelBuilder.Entity<Parent>(entity =>
            {
                entity.Property(e => e.ParentId).ValueGeneratedNever();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Parents)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("parents_userid_foreign");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("Request");

                entity.Property(e => e.RequestId).ValueGeneratedNever();

                entity.HasOne(d => d.BabySitter)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.BabySitterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("request_babysitterid_foreign");

                entity.HasOne(d => d.Massage)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.MassageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("request_massageid_foreign");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.ParentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("request_parentid_foreign");

                entity.HasOne(d => d.RequestStatus)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.RequestStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("request_requeststatusid_foreign");
            });

            modelBuilder.Entity<RequestStatus>(entity =>
            {
                entity.ToTable("RequestStatus");

                entity.Property(e => e.RequestStatusId).ValueGeneratedNever();

                entity.Property(e => e.RequestStatusName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.Property(e => e.ReviewId).ValueGeneratedNever();

                entity.Property(e => e.Decription)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.BabySitter)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.BabySitterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reviews_babysitterid_foreign");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.ParentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reviews_parentid_foreign");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UserPswd)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_locationid_foreign");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_usertypeid_foreign");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.ToTable("UserType");

                entity.Property(e => e.UserTypeId).ValueGeneratedNever();

                entity.Property(e => e.UserTypeName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
