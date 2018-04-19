using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WishlistManager.Models
{
    public class WishContext : DbContext
    {
        public WishContext(DbContextOptions<WishContext> options) : base(options)
        {

        }

        public DbSet<WishItem> Wishes { get; set; }

    }
}
