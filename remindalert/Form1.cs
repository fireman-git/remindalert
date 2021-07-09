using Microsoft.Win32;
using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace remindalert
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //シャットダウン検知開始
            SystemEvents.SessionEnding += new SessionEndingEventHandler(SystemEvents_SessionEnding);

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
            timer1.Interval = 60000;
            timer1.Enabled = true;

        }

#pragma warning disable CA1416 // プラットフォームの互換性を検証
        //シャットダウン検知してポップアップ表示
        private void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            if (e.Reason == SessionEndReasons.Logoff
                || e.Reason == SessionEndReasons.SystemShutdown)
            {
                //シャットダウンをキャンセルする
                e.Cancel = true;
                RegularConfirmation rc = new RegularConfirmation();
                rc.ShowQuestion(MessageBoxButtons.YesNo);
            }
        }
#pragma warning restore CA1416 // プラットフォームの互換性を検証

        //毎分設定された時間を見てあってたらポップアップ表示
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            string[] times = ConfigurationManager.AppSettings.Get("time").Split(',');

            foreach (var item in times)
            {
                if (dt.Hour + ":" + dt.Minute == item)
                {
                    RegularConfirmation rc = new RegularConfirmation();
                    rc.ShowQuestion(MessageBoxButtons.YesNoCancel);
                }
            }
        }
    }
}
