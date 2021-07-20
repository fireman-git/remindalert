using Microsoft.Win32;
using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace remindalert
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();

            //シャットダウン検知開始
            SystemEvents.SessionEnding += new SessionEndingEventHandler(SystemEvents_SessionEnding);

            // 常駐セットアップ
            NotifyIcon icon = new NotifyIcon
            {
                Icon = new Icon("Icon1.ico"),
                Visible = true,
                Text = "体調チェックシート記入忘れ防止アラーム"
            };
            ContextMenuStrip menu = new ContextMenuStrip();
            icon.ContextMenuStrip = menu;

            //タイマーセットアップ
            System.Timers.Timer timer = new System.Timers.Timer(60000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        //毎分設定された時間を見てあってたらポップアップ表示
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            DateTime dt = DateTime.Now;
            string[] times = ConfigurationManager.AppSettings.Get("time").Split(',');

            foreach (var item in times)
            {
                if (dt.Hour + ":" + dt.Minute == item)
                {
                    RegularConfirmation rc = new RegularConfirmation();
                    rc.ShowQuestion();
                }
            }
        }

#pragma warning disable CA1416 // プラットフォームの互換性を検証
        //シャットダウン検知してポップアップ表示
        private void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            if (e.Reason == SessionEndReasons.Logoff
                || e.Reason == SessionEndReasons.SystemShutdown)
            {
                //シャットダウンを邪魔する
                e.Cancel = true;
                RegularConfirmation rc = new RegularConfirmation();
                rc.ShowQuestion();
            }
        }
#pragma warning restore CA1416 // プラットフォームの互換性を検証
    }
}
