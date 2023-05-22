using Hubtel.Wallets.Api.Models.Data.WalletDBContext;
using System.Linq;

namespace Hubtel.Wallets.Api.Models.BusinessRule
{
    public class WalletRules
    {
        private WalletDBContext _WalletDBContext;
        public WalletRules(WalletDBContext walletDBContext)
        {
            _WalletDBContext = walletDBContext;
        }
        public bool CheckIfWalletAlreadyExist(string Type, string accountNumber) 
        {
            if (Type.ToLower() == "card")
            {
                //Get the number of wallet created with the card(Visa or mastercard) number
                var NumberOfWalletWithSameCardNumber = _WalletDBContext.Wallet.Where(x => x.AccountNumber == accountNumber.Substring(0, 6)).Count();
                if (NumberOfWalletWithSameCardNumber == 1)
                {
                    return true; //If true then wallet registered with this card number exist.
                }
            }
            else
            {
                //Get the number of wallet created with the momo number
                var NumberOfWalletWithSameMomoNumber = _WalletDBContext.Wallet.Where(x => x.AccountNumber == accountNumber).Count();
                if (NumberOfWalletWithSameMomoNumber == 1)
                {
                    return true; //If true then wallet registered with this momo number exist.
                }
            } 

            return false; //If false then,No wallet exist with the card or momo number.
        }

        //This method checks that a user cannot create more than 5 wallets.
        public bool CheckIfUserHasWalletEqualsFive(string OwnerPhoneNumber)
        {
            var NumberOfWalletsCreatedbyUser = _WalletDBContext.Wallet.Where(x => x.Owner == OwnerPhoneNumber).Count();
            if(NumberOfWalletsCreatedbyUser == 5)
            {
                return true; //Return true,if user wallet created by user is 5,then user cannot created the 6th wallet
            }
            
            return false; //Return false,if user wallet created by user is below 5,then user can still create a wallet.
        }

        public string GetSixDigitsIfCard(string type,string accountNumber)
        {
            if (!string.IsNullOrEmpty(type)) // check if the parameter type is not null
            {
                // Return first 6 digits of the account number, if type is card.
                if (type == "card")
                {
                    return accountNumber.Substring(0, 6);  
                } 
            }
            return accountNumber; //If type is not card,then it is a momo number.hence return the same number
        }

       

    }
}
