using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace WebSocketServer
{
    public partial class frmMain : Form
    {
        private WebSocketSharp.Server.WebSocketServer server;

        public class Lancher : WebSocketBehavior
        {
            protected override void OnMessage(MessageEventArgs e)
            {
                Send($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} サーバ：{e.Data}を受信");
                
                string receiveData = e.Data.ToUpper();
                if (receiveData == "SAKURA")
                {
                    Process.Start(@"C:\Program Files (x86)\sakura\sakura.exe");
                    Send($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} サーバ：サクラエディタを起動");
                }
            }
        }
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            server.Start();
            OutLog("サーバ待ち受け開始");
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            server = new WebSocketSharp.Server.WebSocketServer(8080);
            server.AddWebSocketService<Lancher>("/");

            OutLog("サーバ初期化完了");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            server.Stop();
            OutLog("サーバ停止");
        }

        public void OutLog(string msg)
        {
            txtLog.Text += $"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} {msg}{Environment.NewLine}";
        }
    }
}
