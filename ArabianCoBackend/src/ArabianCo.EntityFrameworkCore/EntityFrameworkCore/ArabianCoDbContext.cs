using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using ArabianCo.Authorization.Roles;
using ArabianCo.Authorization.Users;
using ArabianCo.MultiTenancy;
using ArabianCo.Domain.Countries;
using ArabianCo.Domain.Cities;
using ArabianCo.Domain.Areas;
using ArabianCo.Domain.Categories;
using ArabianCo.Domain.Attachments;
using ArabianCo.Domain.Brands;
using ArabianCo.Domain.FrequentlyQuestions;
using ArabianCo.Domain.Attributes;
using ArabianCo.Domain.AttributeValues;
using ArabianCo.Domain.Products;

namespace ArabianCo.EntityFrameworkCore
{
    public class ArabianCoDbContext : AbpZeroDbContext<Tenant, Role, User, ArabianCoDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public ArabianCoDbContext(DbContextOptions<ArabianCoDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AttributeValue>().HasOne(x => x.Product).WithMany(x => x.AttributeValues).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>().HasMany(x=>x.AttributeValues).WithOne(x=>x.Product).OnDelete(DeleteBehavior.NoAction);
            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<Country> Countries {  get; set; }
        public virtual DbSet<CountryTranslation> CountryTranslations {  get; set; }
        public virtual DbSet<City> Cities {  get; set; }
        public virtual DbSet<CityTranslation> CityTranslations {  get; set; }
        public virtual DbSet<Area> Areas {  get; set; }
        public virtual DbSet<AreaTranslation> AreaTranslations {  get; set; }
        public virtual DbSet<Attachment> Attachments {  get; set; }
        public virtual DbSet<Category> Categories {  get; set; }
        public virtual DbSet<CategoryTranslation> CategoryTranslations {  get; set; }
        public virtual DbSet<Brand> Brands {  get; set; }
        public virtual DbSet<BrandTranslation> BrandTranslations {  get; set; }
        public virtual DbSet<FrequentlyQuestion> FrequentlyQuestions {  get; set; }
        public virtual DbSet<FrequentlyQuestionTranslation> FrequentlyQuestionTranslations {  get; set; }
        public virtual DbSet<Attribute> Attributes {  get; set; }
        public virtual DbSet<AttributeTranslation> AttributeTranslations {  get; set; }
        public virtual DbSet<AttributeValue> AttributeValues {  get; set; }
        public virtual DbSet<AttributeValueTranslation> AttributeValueTranslations {  get; set; }
        public virtual DbSet<Product> Products {  get; set; }
    }
}
