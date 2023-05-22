using System.ComponentModel.DataAnnotations;

namespace Hubtel.Wallets.Api.Models.Data
{
    public class WalletModel
    {
        /// <summary>
        /// Enter the full name of the user
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Enter [momo or card] as value for Type field.
        /// </summary>
        [Required]
        [MaxLength(4)]
        public string Type { get; set; }
        /// <summary>
        /// Enter [momo number or card number] as value for AccountNumber field.
        /// </summary>
        [Required]
        [MaxLength(16)]
        public string AccountNumber { get; set; }
        /// <summary>
        /// Enter [MTN or Vodafone or AirtelTigo or Visa or Mastercard] as value for AccountScheme field.
        /// </summary>
        [Required]
        [MaxLength(10)]
        public string AccountScheme { get; set; }
        /// <summary>
        /// Enter [user phone number] as value for Owner field.
        /// </summary>
        [Required]
        [MaxLength(10)]
        [Phone]
        public string Owner { get; set; }
    }
}
