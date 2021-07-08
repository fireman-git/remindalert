using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;

/*参照URL
* http://pineplanter.moo.jp/non-it-salaryman/2017/06/01/c-sharp-tasktray/
* https://code-examples-ja.hateblo.jp/entry/2016/12/11/C%23%E3%81%A7%E3%82%B7%E3%83%A3%E3%83%83%E3%83%88%E3%83%80%E3%82%A6%E3%83%B3%E3%81%AE%E3%82%A4%E3%83%99%E3%83%B3%E3%83%88%E3%82%92%E5%8F%97%E3%81%91%E5%8F%96%E3%81%A3%E3%81%A6%E5%87%A6%E7%90%86
* 
*/


class Remaindalert
{
    static void Main(string[] args)
    {
        ResidentAlarm rm = new ResidentAlarm();

        Application.Run();
    }

    //ログオフ、シャットダウンしようとしているとき
#pragma warning disable CA1416 // プラットフォームの互換性を検証
    private void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
    {
        if (e.Reason == SessionEndReasons.Logoff
            || e.Reason == SessionEndReasons.SystemShutdown)
        {
            if (MessageBox.Show("体調チェックシートの入力は終わりましたか？",
            "質問", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                //キャンセルする
                e.Cancel = true;
                //アンケートへアクセス
                Process.Start(ConfigurationManager.AppSettings.Get("sheetUrl"));
            }
            else
            {
                //終了
                System.Environment.Exit(0);
            }
        }
    }
#pragma warning restore CA1416 // プラットフォームの互換性を検証

    //指定時間到達
    public void RegularConfirmation()
    {
        switch (MessageBox.Show("体調チェックシートの入力は終わりましたか？",
            "質問", MessageBoxButtons.YesNoCancel))
        {
            case DialogResult.Yes:
                //終了
                System.Environment.Exit(0);
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

//常駐
class ResidentAlarm : Form
{
    public ResidentAlarm()
    {
        Remaindalert ra = new Remaindalert();
        DateTime dt = DateTime.Now;
        string[] times = ConfigurationManager.AppSettings.Get("time").Split(',');

        this.ShowInTaskbar = false;
        this.setComponents();

        while (true)
        {
            foreach (var item in times)
            {
                if (dt.Hour + ":" + dt.Minute == item)
                {
                    ra.RegularConfirmation();
                    //1分の間に複数ウィンドウが立ち上がるのを防止するため1分停止
                    System.Threading.Thread.Sleep(60000);
                }
            }
            dt = DateTime.Now;
        }
    }
    private void setComponents()
    {
        NotifyIcon icon = new NotifyIcon();
        icon.Icon = new Icon("Icon1.ico");
        icon.Visible = true;
        icon.Text = "体調チェックシート記入忘れ防止アラーム";
        ContextMenuStrip menu = new ContextMenuStrip();
        icon.ContextMenuStrip = menu;
    }
}