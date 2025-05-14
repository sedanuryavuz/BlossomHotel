using BlossomHotel.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomHotel.DataAccessLayer.Concrete
{
    public class Context:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost; Initial Catalog=BlossomHotel; User ID=sa;Password=12345; Connect Timeout=30;Encrypt=True; Trust Server Certificate=True");
        }
        public DbSet<Room>? Rooms { get; set; }
        public DbSet<Booking>? Bookings { get; set; }
    }
}
