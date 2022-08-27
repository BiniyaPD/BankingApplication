using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BankingApp.EFLayer.Models
{
    public partial class db_bankContext : DbContext
    {
        public db_bankContext()
        {
        }

        public db_bankContext(DbContextOptions<db_bankContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<CorporateAccount> CorporateAccounts { get; set; }
        public virtual DbSet<CurrentAccount> CurrentAccounts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Customerlogin> Customerlogins { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<SavingsAccount> SavingsAccounts { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=BINIYA-PD;Initial Catalog=db_bank;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccountNumber)
                    .HasName("PK__account__17D0878B096851F7");

                entity.ToTable("account");

                entity.Property(e => e.AccountNumber).HasColumnName("accountNumber");

                entity.Property(e => e.AccountType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("accountType");

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("customerId");

                entity.Property(e => e.Doc)
                    .HasColumnType("date")
                    .HasColumnName("DOC");

                entity.Property(e => e.Ifsc)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("IFSC");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.Tin)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TIN");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__account__custome__52593CB8");
            });

            modelBuilder.Entity<CorporateAccount>(entity =>
            {
                entity.HasKey(e => e.CorporateAccountNo)
                    .HasName("PK_CorporateTable");

                entity.ToTable("corporateAccount");

                entity.Property(e => e.CorporateAccountNo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("corporateAccountNo")
                    .HasDefaultValueSql("(format(NEXT VALUE FOR [CorporateSequence],'CO-000000#'))");

                entity.Property(e => e.AccountNumber).HasColumnName("accountNumber");

                entity.HasOne(d => d.AccountNumberNavigation)
                    .WithMany(p => p.CorporateAccounts)
                    .HasForeignKey(d => d.AccountNumber)
                    .HasConstraintName("FK__corporate__accou__60A75C0F");
            });

            modelBuilder.Entity<CurrentAccount>(entity =>
            {
                entity.HasKey(e => e.CurrentAccountNo)
                    .HasName("PK_CurrentTable");

                entity.ToTable("currentAccount");

                entity.Property(e => e.CurrentAccountNo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("currentAccountNo")
                    .HasDefaultValueSql("(format(NEXT VALUE FOR [CurrentSequence],'CA-000000#'))");

                entity.Property(e => e.AccountNumber).HasColumnName("accountNumber");

                entity.Property(e => e.TinNumber).HasMaxLength(20);

                entity.HasOne(d => d.AccountNumberNavigation)
                    .WithMany(p => p.CurrentAccounts)
                    .HasForeignKey(d => d.AccountNumber)
                    .HasConstraintName("FK__currentAc__accou__5BE2A6F2");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("customerId")
                    .HasDefaultValueSql("(format(NEXT VALUE FOR [CustomerSequence],'C-000000#'))");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("emailId");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("firstName");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("gender");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("lastName");

                entity.Property(e => e.ManagerId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("managerId");

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("mobileNumber");

                entity.Property(e => e.Pincode)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("pincode");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("state");
            });

            modelBuilder.Entity<Customerlogin>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK__customer__B611CB7D8E39AF1A");

                entity.ToTable("customerlogin");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("customerId");

                entity.Property(e => e.Customerpassword)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("customerpassword");
            });

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.ToTable("manager");

                entity.Property(e => e.ManagerId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("managerId")
                    .HasDefaultValueSql("(format(NEXT VALUE FOR [ManagerSequence],'M-000000#'))");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(50)
                    .HasColumnName("emailId");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(30)
                    .HasColumnName("firstName");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("gender");

                entity.Property(e => e.LastName)
                    .HasMaxLength(30)
                    .HasColumnName("lastName");

                entity.Property(e => e.ManagerPassword)
                    .HasMaxLength(60)
                    .HasColumnName("managerPassword");

                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(10)
                    .HasColumnName("mobileNumber");
            });

            modelBuilder.Entity<SavingsAccount>(entity =>
            {
                entity.HasKey(e => e.SavingsAccountNo)
                    .HasName("PK_SavingsTable");

                entity.ToTable("savingsAccount");

                entity.Property(e => e.SavingsAccountNo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("savingsAccountNo")
                    .HasDefaultValueSql("(format(NEXT VALUE FOR [SavingsSequence],'SA-000000#'))");

                entity.Property(e => e.AccountNumber).HasColumnName("accountNumber");

                entity.HasOne(d => d.AccountNumberNavigation)
                    .WithMany(p => p.SavingsAccounts)
                    .HasForeignKey(d => d.AccountNumber)
                    .HasConstraintName("FK__savingsAc__accou__571DF1D5");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("transaction");

                entity.Property(e => e.TransactionId).HasColumnName("transactionId");

                entity.Property(e => e.DestinationAccountNo).HasColumnName("destinationAccountNo");

                entity.Property(e => e.SourceAccountNo).HasColumnName("sourceAccountNo");

                entity.Property(e => e.TransactionAmount).HasColumnName("transactionAmount");

                entity.Property(e => e.TransactionDate)
                    .HasColumnType("date")
                    .HasColumnName("transactionDate");

                entity.Property(e => e.TransactionDescription)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("transactionDescription");

                entity.Property(e => e.TransactionType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("transactionType");

                entity.HasOne(d => d.SourceAccountNoNavigation)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.SourceAccountNo)
                    .HasConstraintName("FK__transacti__sourc__6383C8BA");
            });

            modelBuilder.HasSequence<int>("CorporateSequence");

            modelBuilder.HasSequence<int>("CurrentSequence");

            modelBuilder.HasSequence<int>("CustomerSequence");

            modelBuilder.HasSequence<int>("ManagerSequence");

            modelBuilder.HasSequence<int>("SavingsSequence");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
