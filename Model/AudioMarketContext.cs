using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MuzicStore.Model
{
    public partial class AudioMarketContext : DbContext
    {
        public AudioMarketContext()
        {
        }

        public AudioMarketContext(DbContextOptions<AudioMarketContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Audio> Audios { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Mood> Moods { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Favorite> Favorites { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-V4TQUU3;Database=AudioMarket;User Id=sa;Password=12345;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Audio>(entity =>
            {
                entity.ToTable("Audio");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ArtistId).HasColumnName("artistId");

                entity.Property(e => e.Filename)
                    .HasMaxLength(255)
                    .HasColumnName("filename");

                entity.Property(e => e.GenreId).HasColumnName("genreId");

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .HasColumnName("image");

                entity.Property(e => e.MoodId).HasColumnName("moodId");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("price");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.Audios)
                    .HasForeignKey(d => d.ArtistId)
                    .HasConstraintName("FK__Audio__artistId__3C69FB99");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Audios)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("FK__Audio__genreId__3D5E1FD2");

                entity.HasOne(d => d.Mood)
                    .WithMany(p => p.Audios)
                    .HasForeignKey(d => d.MoodId)
                    .HasConstraintName("FK__Audio__moodId__3E52440B");
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.ToTable("Discount");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DiscountAmount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("discountAmount");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("Genre");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Genre1)
                    .HasMaxLength(255)
                    .HasColumnName("genre");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Mood>(entity =>
            {
                entity.ToTable("Mood");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MoodName)
                    .HasMaxLength(255)
                    .HasColumnName("moodName");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BuyerId).HasColumnName("buyerId");

                entity.Property(e => e.DiscountId).HasColumnName("discountId");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("price");

                entity.Property(e => e.PurchaseDate)
                    .HasColumnType("datetime")
                    .HasColumnName("purchaseDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.BuyerId)
                    .HasConstraintName("FK__Order__buyerId__412EB0B6");

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.DiscountId)
                    .HasConstraintName("FK__Order__discountI__4222D4EF");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.AudioId })
                    .HasName("PK__OrderDet__D9B8F28A924D8947");

                entity.ToTable("OrderDetail");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Audio)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.AudioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__Audio__440B1D61");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__Order__4316F928");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Review");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AudioId).HasColumnName("audioId");

                entity.Property(e => e.Comment).HasColumnName("comment");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Audio)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.AudioId)
                    .HasConstraintName("FK__Review__audioId__44FF419A");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Review__userId__45F365D3");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username");

                entity.HasMany(d => d.AudiosNavigation)
                    .WithMany(p => p.Users)
                    .UsingEntity<Favorite>(
                        j => j
                            .HasOne(f => f.Audio)
                            .WithMany()
                            .HasForeignKey(f => f.AudioId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__Favorite__audioId__3F466844"),
                        j => j
                            .HasOne(f => f.User)
                            .WithMany()
                            .HasForeignKey(f => f.UserId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__Favorite__userId__403A8C7D"),
                        j =>
                        {
                            j.HasKey("UserId", "AudioId").HasName("PK_Favorite");
                            j.ToTable("Favorite");
                            j.Property(f => f.UserId).HasColumnName("userId");
                            j.Property(f => f.AudioId).HasColumnName("audioId");
                        });
            });

            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.HasKey(f => new { f.UserId, f.AudioId }).HasName("PK_Favorite");

                entity.HasOne(f => f.User)
                    .WithMany(u => u.Favorites)
                    .HasForeignKey(f => f.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favorite__userId__403A8C7D");

                entity.HasOne(f => f.Audio)
                    .WithMany(a => a.Favorites)
                    .HasForeignKey(f => f.AudioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favorite__audioId__3F466844");

                entity.ToTable("Favorite");

                entity.Property(f => f.UserId).HasColumnName("userId");
                entity.Property(f => f.AudioId).HasColumnName("audioId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
