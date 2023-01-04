﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Notifications.Infrastructure.Persistence;

#nullable disable

namespace Notifications.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220821162407_notificaiton initial db")]
    partial class notificaitoninitialdb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Notifications.Domain.Entity.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmailRetries")
                        .HasColumnType("int");

                    b.Property<bool>("EmailSent")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SMSRetries")
                        .HasColumnType("int");

                    b.Property<bool>("SMSSent")
                        .HasColumnType("bit");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("TradeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TradeNotificationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TradeNotificationId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Notifications.Domain.Entity.TradeNotification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CommoditiesIdentifier")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CounterpartiesIdentifier")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocationIdentifier")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Side")
                        .HasColumnType("int");

                    b.Property<DateTime>("TradeDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TradeStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TradeNotifications");
                });

            modelBuilder.Entity("Notifications.Domain.Entity.Notification", b =>
                {
                    b.HasOne("Notifications.Domain.Entity.TradeNotification", "TradeNotification")
                        .WithMany()
                        .HasForeignKey("TradeNotificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TradeNotification");
                });
#pragma warning restore 612, 618
        }
    }
}
