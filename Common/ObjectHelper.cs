using RankAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RankAPI.Common
{
    public static class ObjectHelper
    {
        static volatile public LM_Costomers[] _Customers = Enumerable.Range(0, 100).Select(t => new LM_Costomers
        {
            Score = new Random().Next(0, 100),
            CustromerID = new Random().NextLong(long.MinValue, long.MaxValue), //BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0)
        }).OrderBy(t=>t.Score).ThenBy(t=>t.CustromerID).ToArray();


        public static long NextLong(this Random random, long min, long max)
        {
            byte[] minArr = BitConverter.GetBytes(min);
            int hMin = BitConverter.ToInt32(minArr, 4);
            int lMin = BitConverter.ToInt32(new byte[] { minArr[0], minArr[1], minArr[2], minArr[3] }, 0);

            byte[] maxArr = BitConverter.GetBytes(max);
            int hMax = BitConverter.ToInt32(maxArr, 4);
            int lMax = BitConverter.ToInt32(new byte[] { maxArr[0], maxArr[1], maxArr[2], maxArr[3] }, 0);

            if (random == null)
                random = new Random();

            int h = random.Next(hMin, hMax);
            int l = 0;
            if (h == hMin)
                l = random.Next(Math.Min(lMin, lMax), Math.Max(lMin, lMax));
            else
                l = random.Next(0, Int32.MaxValue);


            byte[] lArr = BitConverter.GetBytes(l);
            byte[] hArr = BitConverter.GetBytes(h);
            byte[] result = new byte[8];

            for (int i = 0; i < lArr.Length; i++)
            {
                result[i] = lArr[i];
                result[i + 4] = hArr[i];
            }
            return BitConverter.ToInt64(result, 0);
        }
    }


   

}
