using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace signal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private int port;//记录当前扫描的端口号
        private string Address;//记录扫描的系统地址
        private bool[] done = new bool[65536];//记录端口的开放状态
        private int start;//记录扫描的起始端口
        private int end;//记录扫描的结束端口
        private bool OK;

     
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            label2.Text = textBox2.Text;
            label3.Text = textBox3.Text;
            progressBar1.Minimum = Int32.Parse(textBox2.Text);
            progressBar1.Maximum = Int32.Parse(textBox3.Text);
            listBox1.Items.Clear();
            listBox1.Items.Add("端口扫描器v1.0.");
            listBox1.Items.Add("");
            PortScan();

        }
        private void PortScan()
        {
            start = Int32.Parse(textBox2.Text);
            end = Int32.Parse(textBox3.Text);
            //判断输入端口是否合法
            if ((start >= 0 && start <= 65536) && (end >= 0 && end <= 65536) && (start <= end))
            {
                listBox1.Items.Add("开始扫描：这个过程可能需要等待几分钟！");
                Address = textBox1.Text;
                for (int i = start; i <= end; i++)
                {
                    port = i;
                    Scan();
                    progressBar1.Value = i;
                    label1.Text = i.ToString();
                }
                while (!OK)
                {
                    OK = true;
                    for (int i = start; i <= end; i++)
                    {
                        if (!done[i])
                        {
                            OK = false;
                            break;
                        }
                    }
                }
                listBox1.Items.Add("扫描结束！");
            }
            else
            {
                MessageBox.Show("输入错误，端口范围为[0,65536]");
            }
        }
        //连接端口
        private void Scan()
        {
            int portnow = port;
            done[portnow] = true;
            TcpClient objTCP = null;
            try
            {
                objTCP = new TcpClient(Address, portnow);
                listBox1.Items.Add("端口" + portnow.ToString() + "开放");
            }
            catch
            {

            }

        }

    }
}
