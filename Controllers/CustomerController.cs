using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RankAPI.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RankAPI.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices _customerServices;
        public CustomerController(ICustomerServices customerServices)
        {
            _customerServices = customerServices;
        }

        [HttpPost]
        [Route("{customerId:Long}/score/{socre:decimal}")]
        public ActionResult score(Int64 customerId, decimal socre)
        {
            var res = _customerServices.Score(customerId, socre);
            return Ok(res.Result);
        }

        [HttpGet]
        [Route("leaderboard")]
        public ActionResult leaderboard(int start, int end)
        {
            var res = _customerServices.leaderboard(start, end);
            return Ok(res.Result);
        }

        [HttpGet]
        [Route("leaderboard/{customerid:Long}")]
        public ActionResult leaderboard(Int64 customerid,int high, int low)
        {
            var res = _customerServices.leaderboard(customerid, high, low);
            return Ok(res.Result);
        }
    }
}
