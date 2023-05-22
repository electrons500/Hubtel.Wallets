using Hubtel.Wallets.Api.Models.BusinessRule;
using Hubtel.Wallets.Api.Models.Data;
using Hubtel.Wallets.Api.Models.Data.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hubtel.Wallets.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class WalletController : ControllerBase
    {
        private WalletService _WalletService;
        private WalletRules _WalletRules;
        public WalletController(WalletService walletService, WalletRules walletRules)
        {
            _WalletService = walletService;
            _WalletRules = walletRules;
        }

        /// <summary>
        /// Add a new wallet 
        /// </summary>
        /// <param name="model"></param>
        /// <remarks>
        /// This api should be used to add new wallet to hubtel app.
        /// 
        /// Sample data:
        /// 
        /// { \
        ///      "name": "Richard Opoku",\
        ///      "type": "momo",\
        ///      "accountNumber": "0244377253",\
        ///      "accountScheme": "MTN",\
        ///      "owner": "0244377253"\
        /// }
        /// 
        /// </remarks>
        /// <returns></returns>
        /// <response code="201">Wallet successfully created</response>
        /// <response code="400">Cannot create wallet</response>

        [HttpPost("AddWallet")]
        [ProducesResponseType(201)]
        
        public ActionResult AddWallet([FromBody] WalletModel model)
        {
            //check that user has entered all the required details.
            if(model.Name == "string" || model.Type =="string" || model.AccountNumber == "string" || model.AccountScheme == "string" || model.Owner == "string")
            {
                return StatusCode(StatusCodes.Status400BadRequest,new {message = "All field are required.",responseCode ="400"});
            }

            //If wallet already exist then throw badrequest to avoid duplicates

            if (_WalletRules.CheckIfWalletAlreadyExist(model.Type, model.AccountNumber))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = "Cannot create wallet since wallet already exist.", responseCode = "400"});
            }

            //If user has already created 5 wallets then throw bad request on the 6th wallet
            if (_WalletRules.CheckIfUserHasWalletEqualsFive(model.Owner))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = "Cannot create more than 5 wallets", responseCode = "400" });
            }

            //Create wallet if all the business rules has passed successfully
            bool IsWalletCreated = _WalletService.AddWallet(model);
            if(IsWalletCreated)
            {
                return StatusCode(StatusCodes.Status201Created, new { message = "Wallet successfully created", responseCode = "201" });
            }
            //if wallet was not successfully created then throw a bad request.
            return StatusCode(StatusCodes.Status400BadRequest, new { message = "Cannot create wallet", responseCode = "400" });
        }


        /// <summary>
        /// Get a single wallet information by wallet ID
        /// </summary>
        /// <param name="walletId"></param>
        /// <remarks>This api should be used to get a single wallet information</remarks>
        /// <returns></returns>
        /// <response code="200">Wallet successfully found</response>
        /// <response code="404">No wallet found</response>

        [HttpGet("GetWallet/{walletId}")]
        public ActionResult GetWallet(int walletId)
        {
            var model = _WalletService.GetWallet(walletId);
            if(model == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = "No wallet found", responseCode = "404" });
            }

            return StatusCode(StatusCodes.Status200OK, new { message = "Wallet successfully found", responseCode = "200",data= model });

        }

        /// <summary>
        /// Get list of wallets stored
        /// </summary>
        /// <remarks>This api should be used to get a list of  wallets and their information</remarks>
        /// <returns></returns>
        /// <response code="200">Wallets successfully found</response>
        /// <response code="404">No wallets found</response>
        [HttpGet("GetAllWallets")]
        public ActionResult GetAllWallets()  
        {
            var model = _WalletService.GetWallets();
            if (model == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = "No wallets found", responseCode = "404" });
            }

            return StatusCode(StatusCodes.Status200OK, new { message = "Wallets successfully found", responseCode = "200", data = model });

        }


        /// <summary>
        /// Remove wallet using account number.
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <remarks>This api should be used to remove wallet using account number</remarks>
        /// <returns></returns>
        /// <response code="200">Wallet successfully removed</response>
        /// <response code="400">Cannot remove wallet</response>
        [HttpDelete("RemoveWallet/{accountNumber}")]
        public ActionResult RemoveWallet(string accountNumber)
        {
            //Remove wallet using account number
            bool IsWalletRemoved = _WalletService.RemoveWallet(accountNumber);
            if (IsWalletRemoved)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = "Wallet successfully removed", responseCode = "200" });
            }
            //if wallet was not successfully removed then throw a bad request.
            return StatusCode(StatusCodes.Status400BadRequest, new { message = "Cannot remove wallet", responseCode = "400" });
        }

    }
}
