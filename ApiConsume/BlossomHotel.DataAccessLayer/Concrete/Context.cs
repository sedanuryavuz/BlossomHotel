using BlossomHotel.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomHotel.DataAccessLayer.Concrete
{
    public class Context:IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        optionsBuilder.UseSqlServer(@"Data Source=localhost; Initial Catalog=BlossomHotel; User ID=sa;Password=12345; Connect Timeout=30;Encrypt=True; Trust Server Certificate=True");
        }
        public DbSet<About>? Abouts { get; set; }
        public DbSet<Booking>? Bookings { get; set; }
        public DbSet<Hotel>? Hotels { get; set; }
        public DbSet<HotelImage>? HotelImages { get; set; }
        public DbSet<Room>? Rooms { get; set; }
        public DbSet<RoomImage>? RoomImages { get; set; }

        public DbSet<Staff>? Staffs { get; set; }
        public DbSet<Service>? Services { get; set; }
        public DbSet<Testimonial>? Testimonials { get; set; }
        public DbSet<Contact>? Contacts { get; set; }
        public DbSet<Favorite>? Favorites { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .Property(x => x.MessageCategory)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }

    }

}
