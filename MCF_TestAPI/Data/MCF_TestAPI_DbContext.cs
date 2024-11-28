using MCF_TestAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace MCF_TestAPI.Data
{
    public class MCF_TestAPI_DbContext : DbContext
    {

        public MCF_TestAPI_DbContext(DbContextOptions<MCF_TestAPI_DbContext> op) : base(op)
        {

        }

        public DbSet<ms_user> ms_users { get; set; }
        public DbSet<ms_storage_location> ms_storage_locations { get; set; }
        public DbSet<tr_bpkb> tr_bpkbs { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<tr_bpkb>()
                .HasOne(x => x.Location)
                .WithMany(x => x.tr_bpkbs)
                .HasForeignKey(x => x.location_id)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(mb);

            mb.Entity<ms_storage_location>().HasData(new ms_storage_location()
            {
                location_id = "TEST1",
                location_name = "TEST 1"
            });

            mb.Entity<ms_storage_location>().HasData(new ms_storage_location()
            {
                location_id = "TEST2",
                location_name = "TEST 2"
            });

            mb.Entity<ms_user>().HasData(new ms_user()
            {
                user_id = 1,
                user_name = "rhommie",
                password = "123",
                isActive = true
            });
        }
    }
}
