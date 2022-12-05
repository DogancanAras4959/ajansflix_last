using ajansflix.DOMAIN.Mapping;
using ajansflix.DOMAIN.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.DOMAIN
{
    public class ajansflixdbcontext : DbContext
    {
        public ajansflixdbcontext(DbContextOptions<ajansflixdbcontext> options) : base(options)
        {

        }

        public virtual DbSet<Users> users { get; set; }
        public virtual DbSet<Roles> roles { get; set; }
        public virtual DbSet<Orders> orders { get; set; }
        public virtual DbSet<Products> products { get; set; }
        public virtual DbSet<ProductDetails> productDetails { get; set; }
        public virtual DbSet<FavoriteProducts> favoriteProducts { get; set; }
        public virtual DbSet<OrderProducts> orderProducts { get; set; }
        public virtual DbSet<Categories> categories { get; set; }
        public virtual DbSet<Customers> customers { get; set; }
        public virtual DbSet<Details> details { get; set; }
        public virtual DbSet<OrderStatus> orderStatus { get; set; }
        public virtual DbSet<DetailsData> detailsData { get; set; }
        public virtual DbSet<QuantityComponent> quantityComponent { get; set; }
        public virtual DbSet<ImageData> imagedatas { get; set; }
        public virtual DbSet<VideosData> videosData { get; set; }
        public virtual DbSet<ImageFile> imagefile { get; set; }
        public virtual DbSet<ProductImages> imageProductFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfiguration(new CategoryMapping());
            //modelBuilder.ApplyConfiguration(new ContactDataMapping());
            //modelBuilder.ApplyConfiguration(new CustomerMapping());
            //modelBuilder.ApplyConfiguration(new DetailMapping());
            //modelBuilder.ApplyConfiguration(new FavoriteProductMapping());
            //modelBuilder.ApplyConfiguration(new ImageDatasMapping());
            //modelBuilder.ApplyConfiguration(new OrderProductMapping());
            //modelBuilder.ApplyConfiguration(new ProductDetailsMapping());
            //modelBuilder.ApplyConfiguration(new ProductsMapping());
            //modelBuilder.ApplyConfiguration(new RoleMapping());
            //modelBuilder.ApplyConfiguration(new UserMapping());

        }

    }
}
