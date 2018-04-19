using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WishlistManager.Models
{
    public class WishlistContext : DbContext
    {
        public WishlistContext(DbContextOptions<WishlistContext> options) : base(options)
        {

        }

        public DbSet<WishlistItem> Wishlists { get; set; }

    }
}
