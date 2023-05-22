using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Hubtel.Wallets.Api.Models.Data.WalletDBContext
{
    public partial class Wallet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string AccountNumber { get; set; }
        public string AccountScheme { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Owner { get; set; }
    }
}
