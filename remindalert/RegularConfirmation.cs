using System.Configuration;
using System.Diagnostics;
using System.Windows.Forms;

namespace remindalert
{
    public class RegularConfirmation
    {
        //指定時間到達
        public void ShowQuestion()
        {
            // 親フォームを作成
            using (Form f = new Form())
            {
                f.TopMost = true; // 親フォームを常に最前面に表示する
                switch (MessageBox.Show("体調チェックシートの入力は終わりましたか？",
                "体調チェックシート記入忘れ防止アラーム", MessageBoxButtons.YesNo))
                {
                    case DialogResult.Yes:
                        //終了
                        Application.Exit();
                        break;

                    case DialogResult.No:
                        //アンケートへアクセス後終了
                        Process.Start(ConfigurationManager.AppSettings.Get("sheetUrl"));
                        break;
                }
                f.TopMost = false;
            }
        }
    }

}
