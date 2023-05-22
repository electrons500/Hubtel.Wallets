using System;
using System.ComponentModel.DataAnnotations;

namespace Hubtel.Wallets.Api.Models.Data.ApiModel
{
    public class WalletApiModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string AccountNumber { get; set; }
        public string AccountScheme { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }
        public string Owner { get; set; } 

    }
}
