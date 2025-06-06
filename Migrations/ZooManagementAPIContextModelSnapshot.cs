﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZooManagementAPI;

#nullable disable

namespace ZooManagementAPI.Migrations
{
    [DbContext(typeof(ZooManagementAPIContext))]
    partial class ZooManagementAPIContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.5");

            modelBuilder.Entity("ZooManagementAPI.Models.Animal", b =>
                {
                    b.Property<int>("AnimalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AnimalStatus")
                        .HasColumnType("TEXT");

                    b.Property<string>("Classification")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateAcquired")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateLeft")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<int>("EnclosureId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Species")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TransferredToZoo")
                        .HasColumnType("TEXT");

                    b.HasKey("AnimalId");

                    b.HasIndex("EnclosureId");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("ZooManagementAPI.Models.Enclosure", b =>
                {
                    b.Property<int>("EnclosureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaxCapacity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("EnclosureId");

                    b.ToTable("Enclosures");
                });

            modelBuilder.Entity("ZooManagementAPI.Models.Zookeeper", b =>
                {
                    b.Property<int>("ZookeeperId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ZookeeperId");

                    b.ToTable("Zookeepers");
                });

            modelBuilder.Entity("ZooManagementAPI.Models.ZookeeperAndAnimal", b =>
                {
                    b.Property<int>("ZookeeperAndAnimalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AnimalId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ZookeeperId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ZookeeperAndAnimalId");

                    b.HasIndex("AnimalId");

                    b.HasIndex("ZookeeperId");

                    b.ToTable("ZookeeperAndAnimals");
                });

            modelBuilder.Entity("ZooManagementAPI.Models.ZookeeperAndEnclosure", b =>
                {
                    b.Property<int>("ZookeeperAndEnclosureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EnclosureId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ZookeeperId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ZookeeperAndEnclosureId");

                    b.HasIndex("EnclosureId");

                    b.HasIndex("ZookeeperId");

                    b.ToTable("ZookeeperAndEnclosures");
                });

            modelBuilder.Entity("ZooManagementAPI.Models.Animal", b =>
                {
                    b.HasOne("ZooManagementAPI.Models.Enclosure", "Enclosure")
                        .WithMany()
                        .HasForeignKey("EnclosureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Enclosure");
                });

            modelBuilder.Entity("ZooManagementAPI.Models.ZookeeperAndAnimal", b =>
                {
                    b.HasOne("ZooManagementAPI.Models.Animal", "Animal")
                        .WithMany()
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZooManagementAPI.Models.Zookeeper", "Zookeeper")
                        .WithMany()
                        .HasForeignKey("ZookeeperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");

                    b.Navigation("Zookeeper");
                });

            modelBuilder.Entity("ZooManagementAPI.Models.ZookeeperAndEnclosure", b =>
                {
                    b.HasOne("ZooManagementAPI.Models.Enclosure", "Enclosure")
                        .WithMany()
                        .HasForeignKey("EnclosureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZooManagementAPI.Models.Zookeeper", "Zookeeper")
                        .WithMany()
                        .HasForeignKey("ZookeeperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Enclosure");

                    b.Navigation("Zookeeper");
                });
#pragma warning restore 612, 618
        }
    }
}
