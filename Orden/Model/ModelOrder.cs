using System;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Orden.Model
{
    public partial class ModelOrder : DbContext
    {
        public ModelOrder()
            : base(DecryptString())
        {
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.ValidateOnSaveEnabled = false;
            Database.CommandTimeout = 120;
        }
        public virtual DbSet<Assistant> Assistants { get; set; }
        public virtual DbSet<BankCheck> BankChecks { get; set; }
        public virtual DbSet<BankCreditAccount> BankCreditAccounts { get; set; }
        public virtual DbSet<BankDebitAccount> BankDebitAccounts { get; set; }
        public virtual DbSet<Disbursement> Disbursements { get; set; }
        public virtual DbSet<DiscountConcept> DiscountConcepts { get; set; }
        public virtual DbSet<FinacleData> FinacleDatas { get; set; }
        public virtual DbSet<GeneralInformation> GeneralInformations { get; set; }
        public virtual DbSet<Insurance> Insurances { get; set; }
        public virtual DbSet<Participant> Participants { get; set; }
        public virtual DbSet<PropertyInformation> PropertyInformations { get; set; }
        public virtual DbSet<Quality> Qualities { get; set; }
        public virtual DbSet<Seller> Sellers { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Subrogation> Subrogations { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }

        public virtual DbSet<TicketsUsers> TicketsUsers { get; set; }
        public virtual DbSet<TypeUser> TypeUsers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UsersAssistant> UsersAssistants { get; set; }
        public virtual DbSet<ValidationFrech> ValidationFreches { get; set; }
        public virtual DbSet<CausesStranding> CausesStrandings { get; set; }
        public virtual DbSet<AuditProcess> AuditProcesses { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrderStates> OrderStates { get; set; }
        public virtual DbSet<OrderStatesGet> OrderStatesGets { get; set; }
        public virtual DbSet<OrderUsers> OrderUsers { get; set; }
        public virtual DbSet<AuditTicket> AuditTickets { get; set; }
        public virtual DbSet<DistributeTicketsUsers> DistributeTicketsUsers { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assistant>()
                .HasMany(e => e.UsersAssistants)
                .WithRequired(e => e.Assistant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<State>()
                .HasMany(e => e.Tickets)
                .WithRequired(e => e.State)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UsersAssistants)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TypeUser>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.TypeUser)
                .WillCascadeOnDelete(false);
        }
        public static string DecryptString()
        {
            HashAlgorithm hash = MD5.Create();
            string cipherText = ConfigurationManager.ConnectionStrings["ModelOrder"].ToString();
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = hash.ComputeHash(Encoding.UTF8.GetBytes("CedexGenerico"));
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
