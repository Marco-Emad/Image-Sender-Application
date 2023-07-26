using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace Receiver
{
    public partial class Form1 : Form
    {
        private Socket socket;
        private IPEndPoint endPoint;
        private Socket clientSocket;
        private bool isConnected = true;

        public Form1()
        {
            InitializeComponent();
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            endPoint = new IPEndPoint(IPAddress.Any, 15235);
        }

        private void Receive()
        {
            clientSocket = socket.Accept();
            while (true)
            {
                byte[] buffer = new byte[1000000];
                clientSocket.Receive(buffer);
                using (var memoryStream = new MemoryStream(buffer))
                {
                    pictureBox1.Image = System.Drawing.Image.FromStream(memoryStream);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            socket.Bind(endPoint);
            socket.Listen(-1);                       
            var thread = new Thread(Receive);
            thread.Start();
        }        
    }
}


//private void button1_Click(object sender, EventArgs e)
//{
//    if (!isConnected)
//    {
//        clientSocket = socket.Accept();
//        isConnected = true;
//        var thread = new Thread(Receive);
//        thread.Start();
//        button1.Text = "Disconnect";
//    }
//    else
//    {
//        isConnected = false;
//        button1.Text = "Connect";
//    }
//}