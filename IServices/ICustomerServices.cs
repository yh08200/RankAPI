using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RankAPI.IServices
{
    public interface ICustomerServices
    {
        /// <summary>
        /// 更新分数
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        Task<string> Score(Int64 customerID, decimal score);
        /// <summary>
        /// 获取区间信息
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        Task<string> leaderboard(int start, int end);
        /// <summary>
        /// 获取区间信息 重载
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        Task<string> leaderboard(Int64 customerid, int high, int low);
    }
}
