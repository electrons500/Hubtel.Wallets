using Hubtel.Wallets.Api.Models.BusinessRule;
using Hubtel.Wallets.Api.Models.Data.ApiModel;
using Hubtel.Wallets.Api.Models.Data.WalletDBContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hubtel.Wallets.Api.Models.Data.Service
{
    public class WalletService
    {
        private WalletDBContext.WalletDBContext _WalletDBContext;
        private WalletRules _WalletRules;
        public WalletService(WalletDBContext.WalletDBContext walletDBContext, WalletRules walletRules)
        {
            _WalletDBContext = walletDBContext;
            _WalletRules = walletRules;
        }

        //Add  a wallet
        public bool AddWallet(WalletModel model)
        {
            //if type is card then save first 6 digits of card number else return then momo number
            string GetSixDigitsIfCard = _WalletRules.GetSixDigitsIfCard(model.Type,model.AccountNumber);

            //Create an object of the wallet and pass the user data to it.
            Wallet wallet = new Wallet()
            {
                Name = model.Name,
                Type = model.Type,
                AccountNumber = GetSixDigitsIfCard,
                AccountScheme = model.AccountScheme,
                CreatedAt = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
                Owner = model.Owner
            };

            //Insert wallet data to database
            _WalletDBContext.Wallet.Add(wallet);
            int AffectRow = _WalletDBContext.SaveChanges();
            if(AffectRow > 0)
            {
                return true;
            }

            return false;
        }


        //Remove  a wallet
        public bool RemoveWallet(string accountNumber)
        {
            //Get a wallet from Database using wallet Id
            Wallet wallet = GetWalletDetails(accountNumber);
            if (wallet != null)
            {
                //remove wallet from database
                _WalletDBContext.Wallet.Remove(wallet);
                int AffectRow = _WalletDBContext.SaveChanges();
                if (AffectRow > 0)
                {
                    return true;
                }
            }

            return false;
        }

        //Get wallet details by account number
        public Wallet GetWalletDetails(string accountNumber)
        {
            var GetMomoAccount = _WalletDBContext.Wallet.Where(x => x.AccountNumber == accountNumber).FirstOrDefault();
            if(GetMomoAccount != null)
            {
                return GetMomoAccount;
            }

            var GetCardAccount = _WalletDBContext.Wallet.Where(x => x.AccountNumber == accountNumber.Substring(0, 6)).FirstOrDefault();
            if (GetCardAccount != null)
            {
                return GetCardAccount;
            }

            return null;
        }



        //Get a single wallet by Id
        public WalletApiModel GetWallet(int walletId)
        {
            
            //Get a wallet from Database using wallet Id
            Wallet wallet = _WalletDBContext.Wallet.Where(x => x.Id == walletId).FirstOrDefault();
            if (wallet != null)
            {
               //Pass wallet data to the object of WalletApiModel
                WalletApiModel model = new WalletApiModel
                {
                    Id = wallet.Id,
                    Name = wallet.Name,
                    Type = wallet.Type,
                    AccountNumber = wallet.AccountNumber,
                    AccountScheme = wallet.AccountScheme,
                    CreatedAt = wallet.CreatedAt,
                    Owner = wallet.Owner
                    
                };

                return model;
            }
            else 
            {
                //Return empty wallet
                WalletApiModel emptyModel = new WalletApiModel();
                return emptyModel;
            }

        }

        //Get all wallets
        public List<WalletApiModel> GetWallets()
        {
           //Get list of wallet from database
            List<Wallet> wallet = _WalletDBContext.Wallet.ToList();

            //Pass the list from database to WalletApiModel List
            List<WalletApiModel> model = wallet.Select(x => new WalletApiModel
            {
                Id = x.Id,
                Name = x.Name,
                Type = x.Type,
                AccountNumber = x.AccountNumber,
                AccountScheme = x.AccountScheme,
                CreatedAt = x.CreatedAt,
                Owner = x.Owner
            }).ToList();

            //return the list of wallets
            return model;   
        }



    }
}

