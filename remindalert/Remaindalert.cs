using System;
using System.Windows.Forms;

/*参照URL
* http://pineplanter.moo.jp/non-it-salaryman/2017/06/01/c-sharp-tasktray/
* https://code-examples-ja.hateblo.jp/entry/2016/12/11/C%23%E3%81%A7%E3%82%B7%E3%83%A3%E3%83%83%E3%83%88%E3%83%80%E3%82%A6%E3%83%B3%E3%81%AE%E3%82%A4%E3%83%99%E3%83%B3%E3%83%88%E3%82%92%E5%8F%97%E3%81%91%E5%8F%96%E3%81%A3%E3%81%A6%E5%87%A6%E7%90%86
* 
*/

namespace remindalert
{    class Remaindalert
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            _ = new FrmMain();
            Application.Run();
        }
    }
}


