using Microsoft.Win32;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace remindalert
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // 常駐セットアップ
            NotifyIcon icon = new NotifyIcon();
            icon.Icon = new Icon("Icon1.ico");
            icon.Visible = true;
            icon.Text = "体調チェックシート記入忘れ防止アラーム";
            ContextMenuStrip menu = new ContextMenuStrip();
            icon.ContextMenuStrip = menu;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // タイマーで1分ごとにtimer1_Tickを実行
            Text = Application.ProductName;
            timer1.Interval = 60000;
            timer1.Enabled = true;
        }


#pragma warning disable CA1416 // プラットフォームの互換性を検証
        private void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            if (e.Reason == SessionEndReasons.Logoff
                || e.Reason == SessionEndReasons.SystemShutdown)
            {
                if (MessageBox.Show("体調チェックシートの入力は終わりましたか？",
                "質問", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    //シャットダウンをキャンセルする
                    e.Cancel = true;
                    //アンケートへアクセス後、終了
                    Process.Start(ConfigurationManager.AppSettings.Get("sheetUrl"));
                    Application.Exit();
                }
                else
                {
                    //終了
                    Application.Exit();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            DateTime dt = DateTime.Now;
            string[] times = ConfigurationManager.AppSettings.Get("time").Split(',');

            foreach (var item in times)
            {
                if (dt.Hour + ":" + dt.Minute == item)
                {
                    RegularConfirmation rc = new RegularConfirmation();
                    rc.showQuestion();
                }
            }
        }
#pragma warning restore CA1416 // プラットフォームの互換性を検証
    }

    public class RegularConfirmation
    {
        //指定時間到達
        public void showQuestion()
        {
            switch (MessageBox.Show("体調チェックシートの入力は終わりましたか？",
                "質問", MessageBoxButtons.YesNoCancel))
            {
                case DialogResult.Yes:
                    //終了
                    Application.Exit();
                    break;

                case DialogResult.No:
                    //アンケートへアクセス
                    Process.Start(ConfigurationManager.AppSettings.Get("sheetUrl"));
                    break;

                case DialogResult.Cancel:
                    break;
            }
        }
    }

}
