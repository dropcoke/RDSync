using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RDSync
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //ミューテックス作成
            Mutex mutex = new Mutex(false, "RDSync");

            //ミューテックスの所有権を要求する
            if (mutex.WaitOne(0, false) == false)
            {
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ConfigData.Init();
            TaskTray task = new TaskTray();
            Application.Run();
            task.Dispose();
            mutex.Dispose();
        }
    }
}
