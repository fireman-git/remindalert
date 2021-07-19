﻿using System.Configuration;
using System.Diagnostics;
using System.Windows.Forms;

namespace remindalert
{
    public class RegularConfirmation
    {
        //指定時間到達
        public void ShowQuestion(MessageBoxButtons buttonStyle)
        {
            // 親フォームを作成
            using (Form f = new Form())
            {
                f.TopMost = true; // 親フォームを常に最前面に表示する
                switch (MessageBox.Show("体調チェックシートの入力は終わりましたか？",
                "体調チェックシート記入忘れ防止アラーム", buttonStyle))
                {
                    case DialogResult.Yes:
                        //終了
                        Application.Exit();
                        break;

                    case DialogResult.No:
                        //アンケートへアクセス後終了
                        Process.Start(ConfigurationManager.AppSettings.Get("sheetUrl"));
                        break;

                    case DialogResult.Cancel:
                        break;
                }
                f.TopMost = false;
            }
        }
    }

}
