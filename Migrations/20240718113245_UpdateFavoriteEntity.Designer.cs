﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MuzicStore.Model;

#nullable disable

namespace MuzicStore.Migrations
{
    [DbContext(typeof(AudioMarketContext))]
    [Migration("20240718113245_UpdateFavoriteEntity")]
    partial class UpdateFavoriteEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MuzicStore.Model.Audio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ArtistId")
                        .HasColumnType("int")
                        .HasColumnName("artistId");

                    b.Property<string>("Filename")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("filename");

                    b.Property<int?>("GenreId")
                        .HasColumnType("int")
                        .HasColumnName("genreId");

                    b.Property<string>("Image")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("image");

                    b.Property<int?>("MoodId")
                        .HasColumnType("int")
                        .HasColumnName("moodId");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,0)")
                        .HasColumnName("price");

                    b.Property<bool?>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("status")
                        .HasDefaultValueSql("((1))");

                    b.Property<string>("Title")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("GenreId");

                    b.HasIndex("MoodId");

                    b.ToTable("Audio", (string)null);
                });

            modelBuilder.Entity("MuzicStore.Model.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal?>("DiscountAmount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("discountAmount");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<bool?>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("status")
                        .HasDefaultValueSql("((1))");

                    b.HasKey("Id");

                    b.ToTable("Discount", (string)null);
                });

            modelBuilder.Entity("MuzicStore.Model.Favorite", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("userId");

                    b.Property<int>("AudioId")
                        .HasColumnType("int")
                        .HasColumnName("audioId");

                    b.HasKey("UserId", "AudioId")
                        .HasName("PK_Favorite");

                    b.HasIndex("AudioId");

                    b.ToTable("Favorite", (string)null);
                });

            modelBuilder.Entity("MuzicStore.Model.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Genre1")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("genre");

                    b.Property<bool?>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("status")
                        .HasDefaultValueSql("((1))");

                    b.HasKey("Id");

                    b.ToTable("Genre", (string)null);
                });

            modelBuilder.Entity("MuzicStore.Model.Mood", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("MoodName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("moodName");

                    b.Property<bool?>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("status")
                        .HasDefaultValueSql("((1))");

                    b.HasKey("Id");

                    b.ToTable("Mood", (string)null);
                });

            modelBuilder.Entity("MuzicStore.Model.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BuyerId")
                        .HasColumnType("int")
                        .HasColumnName("buyerId");

                    b.Property<int?>("DiscountId")
                        .HasColumnType("int")
                        .HasColumnName("discountId");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,0)")
                        .HasColumnName("price");

                    b.Property<DateTime?>("PurchaseDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("purchaseDate")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int?>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("status")
                        .HasDefaultValueSql("((0))");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("DiscountId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("MuzicStore.Model.OrderDetail", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("AudioId")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderId", "AudioId")
                        .HasName("PK__OrderDet__D9B8F28A924D8947");

                    b.HasIndex("AudioId");

                    b.ToTable("OrderDetail", (string)null);
                });

            modelBuilder.Entity("MuzicStore.Model.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AudioId")
                        .HasColumnType("int")
                        .HasColumnName("audioId");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("comment");

                    b.Property<int?>("Rating")
                        .HasColumnType("int")
                        .HasColumnName("rating");

                    b.Property<int?>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("status")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("userId");

                    b.HasKey("Id");

                    b.HasIndex("AudioId");

                    b.HasIndex("UserId");

                    b.ToTable("Review", (string)null);
                });

            modelBuilder.Entity("MuzicStore.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("password");

                    b.Property<int?>("Role")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("role")
                        .HasDefaultValueSql("((0))");

                    b.Property<bool?>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("status")
                        .HasDefaultValueSql("((1))");

                    b.Property<string>("Username")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("MuzicStore.Model.Audio", b =>
                {
                    b.HasOne("MuzicStore.Model.User", "Artist")
                        .WithMany("Audios")
                        .HasForeignKey("ArtistId")
                        .HasConstraintName("FK__Audio__artistId__3C69FB99");

                    b.HasOne("MuzicStore.Model.Genre", "Genre")
                        .WithMany("Audios")
                        .HasForeignKey("GenreId")
                        .HasConstraintName("FK__Audio__genreId__3D5E1FD2");

                    b.HasOne("MuzicStore.Model.Mood", "Mood")
                        .WithMany("Audios")
                        .HasForeignKey("MoodId")
                        .HasConstraintName("FK__Audio__moodId__3E52440B");

                    b.Navigation("Artist");

                    b.Navigation("Genre");

                    b.Navigation("Mood");
                });

            modelBuilder.Entity("MuzicStore.Model.Favorite", b =>
                {
                    b.HasOne("MuzicStore.Model.Audio", "Audio")
                        .WithMany("Favorites")
                        .HasForeignKey("AudioId")
                        .IsRequired()
                        .HasConstraintName("FK__Favorite__audioId__3F466844");

                    b.HasOne("MuzicStore.Model.User", "User")
                        .WithMany("Favorites")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK__Favorite__userId__403A8C7D");

                    b.Navigation("Audio");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MuzicStore.Model.Order", b =>
                {
                    b.HasOne("MuzicStore.Model.User", "Buyer")
                        .WithMany("Orders")
                        .HasForeignKey("BuyerId")
                        .HasConstraintName("FK__Order__buyerId__412EB0B6");

                    b.HasOne("MuzicStore.Model.Discount", "Discount")
                        .WithMany("Orders")
                        .HasForeignKey("DiscountId")
                        .HasConstraintName("FK__Order__discountI__4222D4EF");

                    b.Navigation("Buyer");

                    b.Navigation("Discount");
                });

            modelBuilder.Entity("MuzicStore.Model.OrderDetail", b =>
                {
                    b.HasOne("MuzicStore.Model.Audio", "Audio")
                        .WithMany("OrderDetails")
                        .HasForeignKey("AudioId")
                        .IsRequired()
                        .HasConstraintName("FK__OrderDeta__Audio__440B1D61");

                    b.HasOne("MuzicStore.Model.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("FK__OrderDeta__Order__4316F928");

                    b.Navigation("Audio");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("MuzicStore.Model.Review", b =>
                {
                    b.HasOne("MuzicStore.Model.Audio", "Audio")
                        .WithMany("Reviews")
                        .HasForeignKey("AudioId")
                        .HasConstraintName("FK__Review__audioId__44FF419A");

                    b.HasOne("MuzicStore.Model.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Review__userId__45F365D3");

                    b.Navigation("Audio");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MuzicStore.Model.Audio", b =>
                {
                    b.Navigation("Favorites");

                    b.Navigation("OrderDetails");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("MuzicStore.Model.Discount", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("MuzicStore.Model.Genre", b =>
                {
                    b.Navigation("Audios");
                });

            modelBuilder.Entity("MuzicStore.Model.Mood", b =>
                {
                    b.Navigation("Audios");
                });

            modelBuilder.Entity("MuzicStore.Model.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("MuzicStore.Model.User", b =>
                {
                    b.Navigation("Audios");

                    b.Navigation("Favorites");

                    b.Navigation("Orders");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
