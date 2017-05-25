using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace booksea.Models
{
    public partial class DBBookSeaProjectContext : DbContext
    {
       public DBBookSeaProjectContext(DbContextOptions<DBBookSeaProjectContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex");

                entity.Property(e => e.Id).HasMaxLength(450);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey })
                    .HasName("PK_AspNetUserLogins");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(450);

                entity.Property(e => e.ProviderKey).HasMaxLength(450);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PK_AspNetUserRoles");

                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_AspNetUserRoles_RoleId");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_AspNetUserRoles_UserId");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.Property(e => e.RoleId).HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name })
                    .HasName("PK_AspNetUserTokens");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.Property(e => e.LoginProvider).HasMaxLength(450);

                entity.Property(e => e.Name).HasMaxLength(450);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasMaxLength(450);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<PBook>(entity =>
            {
                entity.ToTable("P_Book");

                entity.Property(e => e.BookAutor).HasMaxLength(50);

                entity.Property(e => e.BookId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.BookName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.BookPrice).HasColumnType("money");

                entity.Property(e => e.BookPublish).HasMaxLength(50);

                entity.Property(e => e.BookTypeId).HasMaxLength(50);

                entity.Property(e => e.Bookover).HasColumnType("image");
            });

            modelBuilder.Entity<PBookType>(entity =>
            {
                entity.ToTable("P_BookType");

                entity.Property(e => e.BookTypeId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.BookTypeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PComment>(entity =>
            {
                entity.ToTable("P_Comment");

                entity.Property(e => e.BookId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CommentTime).HasColumnType("datetime");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PManager>(entity =>
            {
                entity.ToTable("P_Manager");

                entity.Property(e => e.ManagerId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ManagerName).HasMaxLength(50);

                entity.Property(e => e.Pwd)
                    .IsRequired()
                    .HasColumnName("PWD")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<POrder>(entity =>
            {
                entity.ToTable("P_Order");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OrderMessage).HasMaxLength(500);

                entity.Property(e => e.OrderPrice).HasColumnType("money");

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            modelBuilder.Entity<POrderDetail>(entity =>
            {
                entity.ToTable("P_OrderDetail");

                entity.Property(e => e.BookId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.BookPrice).HasColumnType("money");

                entity.Property(e => e.OrderId)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PUserInfo>(entity =>
            {
                entity.ToTable("P_UserInfo");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Postcodes).HasMaxLength(50);

                entity.Property(e => e.Pwd)
                    .IsRequired()
                    .HasColumnName("PWD")
                    .HasMaxLength(50);

                entity.Property(e => e.RegiterTime).HasColumnType("datetime");

                entity.Property(e => e.Sex).HasColumnType("varchar(2)");

                entity.Property(e => e.Tellphine).HasMaxLength(50);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<PBook> PBook { get; set; }
        public virtual DbSet<PBookType> PBookType { get; set; }
        public virtual DbSet<PComment> PComment { get; set; }
        public virtual DbSet<PManager> PManager { get; set; }
        public virtual DbSet<POrder> POrder { get; set; }
        public virtual DbSet<POrderDetail> POrderDetail { get; set; }
        public virtual DbSet<PUserInfo> PUserInfo { get; set; }
    }
}