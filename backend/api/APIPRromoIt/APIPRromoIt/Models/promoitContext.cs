using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace APIPRromoIt.Models
{
    public partial class promoitContext : DbContext
    {
        public promoitContext()
        {
        }

        public promoitContext(DbContextOptions<promoitContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BalanceTransaction> BalanceTransactions { get; set; } = null!;
        public virtual DbSet<Campaign> Campaigns { get; set; } = null!;
        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<DonatedProduct> DonatedProducts { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductToOrder> ProductToOrders { get; set; } = null!;
        public virtual DbSet<Retweet> Retweets { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Tweet> Tweets { get; set; } = null!;
        public virtual DbSet<TwitterAccount> TwitterAccounts { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserBalance> UserBalances { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-V0M7V3Q0;Database=promoit;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BalanceTransaction>(entity =>
            {
                entity.HasKey(e => e.TransactionId);

                entity.ToTable("balance_transactions");

                entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.CampaignId).HasColumnName("campaign_id");

                entity.Property(e => e.CreateByUser)
                    .HasMaxLength(50)
                    .HasColumnName("create_by_user");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("create_date");

                entity.Property(e => e.Reason)
                    .HasMaxLength(50)
                    .HasColumnName("reason");

                entity.Property(e => e.RetweetId).HasColumnName("retweet_id");

                entity.Property(e => e.UpdateByUser)
                    .HasMaxLength(50)
                    .HasColumnName("update_by_user");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("update_date");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.BalanceTransactions)
                    .HasForeignKey(d => d.CampaignId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_balance_transactions_campaign");
            });

            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.ToTable("campaign");

                entity.Property(e => e.CampaignId).HasColumnName("campaign_id");

                entity.Property(e => e.CampaignName)
                    .HasMaxLength(50)
                    .HasColumnName("campaign_name");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("create_date");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Hashtag)
                    .HasMaxLength(50)
                    .HasColumnName("hashtag");

                entity.Property(e => e.UserId)
                    .HasMaxLength(100)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Campaigns)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_campaign_company");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Campaigns)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_campaign_user");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("company");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(50)
                    .HasColumnName("company_name");

                entity.Property(e => e.CompanyType)
                    .HasMaxLength(20)
                    .HasColumnName("company_type");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Site)
                    .HasMaxLength(50)
                    .HasColumnName("site");
            });

            modelBuilder.Entity<DonatedProduct>(entity =>
            {
                entity.ToTable("donated_product");

                entity.Property(e => e.DonatedProductId).HasColumnName("donated_product_id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.CampaignId).HasColumnName("campaign_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.UserId)
                    .HasMaxLength(100)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.DonatedProducts)
                    .HasForeignKey(d => d.CampaignId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_donated_product_campaign");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.DonatedProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_donated_product_product");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DonatedProducts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_donated_product_user");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.CampaignId).HasColumnName("campaign_id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UserId)
                    .HasMaxLength(100)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CampaignId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_order_campaign");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_order_company1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_order_user");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .HasColumnName("product_name");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_product_company");
            });

            modelBuilder.Entity<ProductToOrder>(entity =>
            {
                entity.HasKey(e => e.PoId);

                entity.ToTable("product_to_order");

                entity.Property(e => e.PoId).HasColumnName("po_id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ProductToOrders)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_product_to_order_order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductToOrders)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_product_to_order_product");
            });

            modelBuilder.Entity<Retweet>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("retweet");

                entity.Property(e => e.Campaign)
                    .HasMaxLength(50)
                    .HasColumnName("campaign");

                entity.Property(e => e.CreateByUser)
                    .HasMaxLength(50)
                    .HasColumnName("create_by_user");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_date");

                entity.Property(e => e.ParsingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("parsing_date");

                entity.Property(e => e.RetweetId).HasColumnName("retweet_id");

                entity.Property(e => e.Retweets).HasColumnName("retweets");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("status")
                    .IsFixedLength();

                entity.Property(e => e.TwittId)
                    .HasMaxLength(50)
                    .HasColumnName("twitt_id");

                entity.Property(e => e.TwitterUserId)
                    .HasMaxLength(50)
                    .HasColumnName("twitter_user_id");

                entity.Property(e => e.UpdateByUser)
                    .HasMaxLength(50)
                    .HasColumnName("update_by_user");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("update_date");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(30)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<Tweet>(entity =>
            {
                entity.ToTable("tweet");

                entity.Property(e => e.TweetId).HasColumnName("tweet_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<TwitterAccount>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("twitter_accounts");

                entity.Property(e => e.TwitterUserId)
                    .HasMaxLength(50)
                    .HasColumnName("twitter_user_id");

                entity.Property(e => e.TwitterUsername)
                    .HasMaxLength(50)
                    .HasColumnName("twitter_username");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.UserId)
                    .HasMaxLength(100)
                    .HasColumnName("user_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasColumnName("address");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .HasColumnName("role");

                entity.Property(e => e.TelNumber)
                    .HasMaxLength(50)
                    .HasColumnName("tel_number");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasColumnName("user_name");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_user_company");
            });

            modelBuilder.Entity<UserBalance>(entity =>
            {
                entity.HasKey(e => e.BalanceId);

                entity.ToTable("user_balance");

                entity.Property(e => e.BalanceId).HasColumnName("balance_id");

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.UserId)
                    .HasMaxLength(100)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserBalances)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_balance_user");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
