using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Sender
{
    public partial class Form1 : Form
    {
        private Socket socket;
        private IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 15235);

        public Form1()
        {
            InitializeComponent();
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 15235);
            socket.Connect(endPoint);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var ms = new MemoryStream())
            {
                pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                var buffer = ms.GetBuffer();
                socket.Send(buffer);
            }
            //socket.Shutdown(SocketShutdown.Both);
            //socket.Close();
        }        
    }
}
