using Domain.Entities;
using Domain.Entities.Announcements;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ParkwayFunContext : DbContext
    {
        public ParkwayFunContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<AnnouncementType> AnnouncementTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(o => o.Id);
            modelBuilder.Entity<User>()
                .Property(o => o.Name)
                .HasMaxLength(25)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(o => o.LastName)
                .HasMaxLength(25)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(o => o.PasswordHash)
                .HasMaxLength(200)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(o => o.Email)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(o => o.PhoneNumber)
                .HasMaxLength(15)
                .IsRequired();
            modelBuilder.Entity<User>()
                .HasOne(x => x.Role)
                .WithMany(y => y.Users)
                .HasForeignKey(nd => nd.RoleId);


            #endregion

            #region Role

            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<Role>().HasKey(o => o.Id);
            modelBuilder.Entity<Role>()
                .Property(o => o.Name)
                .HasMaxLength(25)
                .IsRequired();

            #endregion

            #region Announcement

            modelBuilder.Entity<Announcement>().ToTable("Announcement");
            modelBuilder.Entity<Announcement>().HasKey(o => o.Id);
            modelBuilder.Entity<Announcement>()
                .Property(o => o.Title)
                .HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<Announcement>()
                .Property(o => o.Content)
                .HasMaxLength(4000);
            modelBuilder.Entity<Announcement>()
                .HasOne(x => x.Detail)
                .WithOne(y => y.Announcement)
                .HasForeignKey<AnnouncementDetail>(nd => nd.AnnouncementId);
            modelBuilder.Entity<Announcement>()
                .HasOne(x => x.Type)
                .WithMany(y => y.Announcements)
                .HasForeignKey(nd => nd.AnnouncementTypeId);
            modelBuilder.Entity<Announcement>()
                .HasOne(x => x.User)
                .WithMany(y => y.Announcements)
                .HasForeignKey(nd => nd.UserId);

            #endregion

            #region AnnouncementType

            modelBuilder.Entity<AnnouncementType>().ToTable("AnnouncementTypes");
            modelBuilder.Entity<AnnouncementType>().HasKey(o => o.Id);
            modelBuilder.Entity<AnnouncementType>().HasIndex(o => o.Name).IsUnique();
            modelBuilder.Entity<AnnouncementType>()
                .Property(o => o.Name)
                .HasMaxLength(100)
                .IsRequired();

            #endregion

            #region AnnouncementDetail

            modelBuilder.Entity<AnnouncementDetail>().ToTable("AnnouncementDetails");
            modelBuilder.Entity<AnnouncementDetail>().HasKey(o => o.Id);
            modelBuilder.Entity<AnnouncementDetail>()
                .Property(o => o.Created)
                .HasColumnType("datetime2").HasPrecision(0)
                .IsRequired();
            modelBuilder.Entity<AnnouncementDetail>()
                .Property(o => o.LastModified)
                .HasColumnType("datetime2").HasPrecision(0)
                .IsRequired();

            #endregion

        }


    }
}
