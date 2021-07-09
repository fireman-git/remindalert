using System.Configuration;
using System.Diagnostics;
using System.Windows.Forms;

namespace remindalert
{
    public class RegularConfirmation
    {
        //指定時間到達
        public void ShowQuestion(MessageBoxButtons buttonStyle)
        {
            switch (MessageBox.Show("体調チェックシートの入力は終わりましたか？",
                "質問", buttonStyle))
            {
                case DialogResult.Yes:
                    //終了
                    Application.Exit();
                    break;

                case DialogResult.No:
                    //アンケートへアクセス
                    Process.Start(ConfigurationManager.AppSettings.Get("sheetUrl"));
                    Application.Exit();
                    break;

                case DialogResult.Cancel:
                    break;
            }
        }
    }

}
