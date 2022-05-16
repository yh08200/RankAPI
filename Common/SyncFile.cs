using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RankAPI.Common
{
    public class SyncFile
    {
        private static ReaderWriterLockSlim readerWriterLock = new ReaderWriterLockSlim();

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="action"></param>
        public static void WriteFile(Action action)
        {
            readerWriterLock.EnterWriteLock();
            try
            {
                action.Invoke();
            }
            finally
            {
                readerWriterLock.ExitWriteLock();
            }
        }
    }
}
