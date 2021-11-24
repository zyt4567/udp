using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kehuduan1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Socket socketSend;
        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string str;
                /*
                 * 做好连接准备
                 */
                int port = 2000;
                string host = "192.168.43.236";//我室友的IP地址
                IPAddress ip = IPAddress.Parse(host);
                IPEndPoint ipe = new IPEndPoint(ip, port);//把ip和端口转化为IPEndPoint实例
                Socket c = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//创建一个Socket
                /*
                 * 开始连接
                 */
                str = "Connect to server...";
                textBox1.AppendText(str + Environment.NewLine);
                c.Connect(ipe);//连接到服务器
                /*
                 *发送消息 
                 */
                string sendStr = textBox2.Text;
                str = "The message content: " + sendStr;
                textBox1.AppendText(str + Environment.NewLine);
                byte[] bs = Encoding.UTF8.GetBytes(sendStr);
                str = "Send the message to the server...";
                textBox1.AppendText(str + Environment.NewLine);
                c.Send(bs, bs.Length, 0);//发送信息
                /*
                 * 接收服务器端的反馈信息
                 */
                string recvStr = "";
                byte[] recvBytes = new byte[1024];
                int bytes;
                bytes = c.Receive(recvBytes, recvBytes.Length, 0);//从服务器端接受返回信息
                recvStr += Encoding.UTF8.GetString(recvBytes, 0, bytes);
                str = "The server feedback: " + recvStr;//显示服务器返回信息
                textBox1.AppendText(str + Environment.NewLine);
                /*
                 * 关闭socket
                 */
                c.Close();
            }
            catch (ArgumentNullException f)
            {
                string str = "ArgumentNullException: " + f.ToString();
                textBox1.AppendText(str + Environment.NewLine);
            }
            catch (SocketException f)
            {
                string str = "ArgumentNullException: " + f.ToString();
                textBox1.AppendText(str + Environment.NewLine);
            }
            textBox1.AppendText("" + Environment.NewLine);
            textBox2.Text = "";
        }
    }
}
