using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Data;

public partial class BooKstoreDbContext : IdentityDbContext<ApiUser>
{
    public BooKstoreDbContext()
    {
    }

    public BooKstoreDbContext(DbContextOptions<BooKstoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }
  

  

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Authors__3214EC07A79FD088");

            entity.Property(e => e.Bio).HasMaxLength(250);
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Books__3214EC07615CAC1F");

            entity.HasIndex(e => e.Isbn, "UQ__Books__447D36EA84101264").IsUnique();

            entity.Property(e => e.Image).HasMaxLength(50);
            entity.Property(e => e.Isbn)
                .HasMaxLength(50)
                .HasColumnName("ISBN");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Summary).HasMaxLength(250);
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.Year).HasColumnName("year");

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_Books_ToTable");
        });
      
        
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Name = "User",
                NormalizedName= "USER",
                //from //https://guidgenerator.com/online-guid-generator.aspx
                Id = "03a18a35-bfc1-4ed0-97ad-2e6e35d1dae4"

            },
             new IdentityRole
             {
                 Name = "Administrator",
                 NormalizedName = "ADMINISTRATOR",
                 //from https://guidgenerator.com/online-guid-generator.aspx
                 Id = "1ce7e2e7-e88d-4178-abc6-17cfd5422dcc"

             }
            ) ;

       
        
        
        
        var hasher = new PasswordHasher<ApiUser>();
        modelBuilder.Entity<ApiUser>().HasData(
          new ApiUser
          {
            
              //from https://guidgenerator.com/online-guid-generator.aspx
              Id = "f6214ff4-b714-4f90-bc99-15885f55aedb",
              Email="admin@bookstore.com",
              NormalizedEmail="ADMIN@BOOKSTORE.COM",
              UserName= "admin@bookstore.com",
              NormalizedUserName = "ADMIN@BOOKSTORE.COM",
              FirstName="System",
              LastName="Admin",
              PasswordHash=hasher.HashPassword(null,"P@ssword1")

          },
           new ApiUser
           {
               
            
               //from https://guidgenerator.com/online-guid-generator.aspx
               Id = "6a934c83-d9d5-4d85-9267-d53e7fb62f8f",
                Email = "user@bookstore.com",
               NormalizedEmail = "USER@BOOKSTORE.COM",
               UserName = "user@bookstore.com",
               NormalizedUserName = "USER@BOOKSTORE.COM",
               FirstName = "System",
               LastName = "User",
               PasswordHash = hasher.HashPassword(null, "P@ssword1")

           }
          );
      
        
        
        
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId= "03a18a35-bfc1-4ed0-97ad-2e6e35d1dae4",
                UserId= "6a934c83-d9d5-4d85-9267-d53e7fb62f8f"
            },
             new IdentityUserRole<string>
             {
                 RoleId = "1ce7e2e7-e88d-4178-abc6-17cfd5422dcc",
                 UserId = "f6214ff4-b714-4f90-bc99-15885f55aedb"
             }
            );


        OnModelCreatingPartial(modelBuilder);  
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
