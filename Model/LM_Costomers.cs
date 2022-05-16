using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace RankAPI.Model
{
    /// <summary>
    /// 客户信息model
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    public struct LM_Costomers
    {
        /// <summary>
        /// 客户ID
        /// </summary>
        public Int64 CustromerID { get; set; }
        /// <summary>
        /// 分数
        /// </summary>
        public decimal Score { get; set; } 
        /// <summary>
        /// 客户ID
        /// </summary>
        public int Rank { get; set; }
    }
}
