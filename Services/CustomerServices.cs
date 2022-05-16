using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.Logging;
using RankAPI.Common;
using RankAPI.IServices;
using RankAPI.Model;
using StructLinq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RankAPI.Services
{
    public class CustomerServices : ICustomerServices
    {
        //private readonly string _path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "lst.json";

        private readonly ILogger<CustomerServices> _logger;
        public CustomerServices(ILogger<CustomerServices> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 更新得分
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        [Benchmark]
        public async Task<string> Score(Int64 customerID, decimal score)
        {
            try
            {
                var res = await Task.Run<string>(() =>
                    {
                        var temp = ObjectHelper._Customers.ToStructEnumerable().Where(t => t.CustromerID == customerID, t => t).ToArray();
                        var _count = temp.Length;
                        if (_count <= 0)
                            return $"No Match customerid is {customerID}";
                        for (int i = 0; i < _count; i++)
                            temp[i].Score += score;
                        return temp.ToJson();
                    });
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError($"【Score Error：{ex.Message}】");
                return ex.Message;
            }
        }

        /// <summary>
        /// 获取区间信息
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [Benchmark]
        public async Task<string> leaderboard(int start, int end)
        {
            try
            {
                var res = await Task.Run<string>(() =>
                 {
                     LM_Costomers[] lM_Costomers = { };
                     if (start >= ObjectHelper._Customers.Length)
                         return $"Out of index,The source max length is { ObjectHelper._Customers.Length}";
                     var temp = ObjectHelper._Customers.ToStructEnumerable().Skip(start - 1).Take(end - start + 1).ToArray();
                     if (temp.Length <= 0)
                         return $"No Match customerid";
                     for (int j = 0; j < temp.Length; j++)
                     {
                         var _index = -1;
                         if (-1 != _index)
                             for (int i = 0; i < ObjectHelper._Customers.Length; i++)
                             {
                                 if (temp[j].CustromerID == ObjectHelper._Customers[i].CustromerID)
                                 {
                                     _index = i + 1;//找到索引
                                     break;
                                 }
                             }
                         temp[j].Rank = ++_index;
                         lM_Costomers.Append(temp[j]);
                     }
                     return lM_Costomers.ToJson();
                 });
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError($"【leaderboard Error：{ex.Message}】");
                return ex.Message;
            }
        }

        /// <summary>
        /// 获取区间信息 重载
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [Benchmark]
        public async Task<string> leaderboard(Int64 customerid, int high, int low)
        {
            try
            {
                var res = await Task.Run<string>(() =>
                 {
                     LM_Costomers[] lM_Costomers = { };
                     var temp = ObjectHelper._Customers.ToStructEnumerable().Where(t => t.CustromerID == customerid).ToArray();
                     if (temp.Length <= 0)
                         return $"No Match customerid is {customerid}";
                     if (high >= ObjectHelper._Customers.Length)
                         return $"Out of index,The source max length is { ObjectHelper._Customers.Length}";

                     for (int j = 0; j < temp.Length; j++)
                     {
                         for (int i = 0; i < ObjectHelper._Customers.Length; i++)
                         {
                             if (temp[j].CustromerID == ObjectHelper._Customers[i].CustromerID)
                             {
                                 var _index = i + 1;//找到索引
                                 var start = _index - high;
                                 var result = ObjectHelper._Customers.Skip(_index - high - 1).Take(low + high + 1).ToArray();
                                 for (int k = 0; k < result.Length; k++)
                                 {
                                     result[k].Rank = ++start;
                                     lM_Costomers.Append(result[k]);
                                 }
                             }
                         }
                     }
                     return lM_Costomers.ToJson();
                 });
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError($"【leaderboard overwrite Error：{ex.Message}】");
                return ex.Message;
            }
        }
    }
}
