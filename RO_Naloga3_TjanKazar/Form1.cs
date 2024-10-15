using System.ComponentModel;
using System.Drawing.Text;
using System.Net;
using System.Net.Sockets;

namespace RO_Naloga3_TjanKazar
{
    public partial class Form1 : Form
    {
        public Form1(bool jeServer)
        {
            // zacetek igre
            InitializeComponent();
            Recver.DoWork += Recver_DoWork;
            CheckForIllegalCrossThreadCalls = false;
            if (jeServer)
            {
                // ce uporabnik klikne na host game je streznik v tej instanci programa
                Igralec = 'X';
                nadsprotnik = 'O';
                Server = new TcpListener(IPAddress.Any, 54321);
                Server.Start();
                // blokirna metoda AcceptSocket() ustavi nadaljno izvajanje kode do
                // takrat, ko se povezava ne vspostavi
                socket = Server.AcceptSocket();
            }
            else
            {
                // v nadsprotnem primeru je odjemalec
                Igralec = 'O';
                nadsprotnik = 'X';
                try
                {
                // socketa doloèimo kot odjemalca v tej instanci programa
                    Client = new TcpClient("127.0.0.1", 54321);
                    socket = Client.Client;
                    Recver.RunWorkerAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }
            }
        }
        private void Recver_DoWork(object? sender, DoWorkEventArgs e)
        {
            // ce je bool CheckPosition true je igra koncana z zmago porazom ali remijem
            if (CheckPosition())
                return;
            // ugasnemo gumbe igralcu, javimo mu da je na vrsti nadsprotnik
            prepreciPotezo();
            label1.Text = "Nadsprotnikova poteza:";
            recv();
            // po prejeti nadsportnikovi potezi mogoèimo igralcu gumbe kjer velja .Text == ""
            label1.Text = "vaša poteza";
            if (!CheckPosition())
                omogociPozeto();
        }
        public Form1 form1;
        char Igralec;
        char nadsprotnik;
        // socket se neve, ali bo server ali client
        Socket socket;
        BackgroundWorker Recver = new BackgroundWorker();
        TcpListener Server = null;
        TcpClient Client = null;

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] st = { 1 };
            socket.Send(st);
            button1.Text = Igralec.ToString();
            Recver.RunWorkerAsync();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] st = { 2 };
            socket.Send(st);
            button2.Text = Igralec.ToString();
            Recver.RunWorkerAsync();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            byte[] st = { 3 };
            socket.Send(st);
            button3.Text = Igralec.ToString();
            Recver.RunWorkerAsync();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            byte[] st = { 4 };
            socket.Send(st);
            button4.Text = Igralec.ToString();
            Recver.RunWorkerAsync();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            byte[] st = { 5 };
            socket.Send(st);
            button5.Text = Igralec.ToString();
            Recver.RunWorkerAsync();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            byte[] st = { 6 };
            socket.Send(st);
            button6.Text = Igralec.ToString();
            Recver.RunWorkerAsync();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            byte[] st = { 7 };
            socket.Send(st);
            button7.Text = Igralec.ToString();
            Recver.RunWorkerAsync();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            byte[] st = { 8 };
            socket.Send(st);
            button8.Text = Igralec.ToString();
            Recver.RunWorkerAsync();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            byte[] st = { 9 };
            socket.Send(st);
            button9.Text = Igralec.ToString();
            Recver.RunWorkerAsync();
        }
        private bool CheckPosition()
        {
            if (button1.Text == button2.Text && button2.Text == button3.Text && button3.Text != "")
            {
                if (button1.Text[0] == Igralec)
                {
                    MessageBox.Show("Zmagali ste! " + Igralec);
                }
                else
                    MessageBox.Show("Zmagali ste! " + nadsprotnik);
                return true;
            }
            else if (button4.Text == button5.Text && button5.Text == button6.Text && button6.Text != "")
            {
                if (button4.Text[0] == Igralec)
                {
                    MessageBox.Show("Zmagali ste! " + Igralec);
                }
                else
                    MessageBox.Show("Zmagali ste! " + nadsprotnik);
                return true;
            }
            else if (button7.Text == button8.Text && button8.Text == button9.Text && button9.Text != "")
            {
                if (button7.Text[0] == Igralec)
                {
                    MessageBox.Show("Zmagali ste! " + Igralec);
                }
                else
                    MessageBox.Show("Zmagali ste! " + nadsprotnik);
                return true;
            }
            else if (button4.Text == button5.Text && button5.Text == button6.Text && button7.Text != "")
            {
                if (button4.Text[0] == Igralec)
                {
                    MessageBox.Show("Zmagali ste! " + Igralec);
                }
                else
                    MessageBox.Show("Zmagali ste! " + nadsprotnik);
                return true;
            }
            else if (button1.Text == button4.Text && button4.Text == button7.Text && button7.Text != "")
            {
                if (button1.Text[0] == Igralec)
                {
                    MessageBox.Show("Zmagali ste! " + Igralec);
                }
                else
                    MessageBox.Show("Zmagali ste! " + nadsprotnik);
                return true;
            }
            else if (button2.Text == button5.Text && button5.Text == button8.Text && button8.Text != "")
            {
                if (button2.Text[0] == Igralec)
                {
                    MessageBox.Show("Zmagali ste! " + Igralec);
                }
                else
                    MessageBox.Show("Zmagali ste! " + nadsprotnik);
                return true;
            }
            else if (button3.Text == button6.Text && button6.Text == button9.Text && button9.Text != "")
            {
                if (button3.Text[0] == Igralec)
                {
                    MessageBox.Show("Zmagali ste! " + Igralec);
                }
                else
                    MessageBox.Show("Zmagali ste! " + nadsprotnik);
                return true;
            }
            else if (button1.Text == button5.Text && button5.Text == button9.Text && button9.Text != "")
            {
                if (button1.Text[0] == Igralec)
                {
                    MessageBox.Show("Zmagali ste! " + Igralec);
                }
                else
                    MessageBox.Show("Zmagali ste! " + nadsprotnik);
                return true;
            }
            else if (button3.Text == button5.Text && button5.Text == button7.Text && button7.Text != "")
            {
                if (button3.Text[0] == Igralec)
                {
                    MessageBox.Show("Zmagali ste! " + Igralec);
                }
                else
                    MessageBox.Show("Zmagali ste! " + nadsprotnik);
                return true;
            }
            else if (button1.Text != "" && button2.Text != "" && button3.Text != "" && button4.Text != "" && button5.Text != "" && button6.Text != "" && button7.Text != "" && button8.Text != "" && button9.Text != "")
            {
                MessageBox.Show("Remi!");
                return true;
            }
            return false;
        }
        private void recv()
        {
            byte[] data = new byte[1];
            socket.Receive(data);
            try
            {
                switch (data[0])
                {
                    case 1:
                        button1.Text = nadsprotnik.ToString();
                        break;
                    case 2:
                        button2.Text = nadsprotnik.ToString();
                        break;
                    case 3:
                        button3.Text = nadsprotnik.ToString();
                        break;
                    case 4:
                        button4.Text = nadsprotnik.ToString();
                        break;
                    case 5:
                        button5.Text = nadsprotnik.ToString();
                        break;
                    case 6:
                        button6.Text = nadsprotnik.ToString();
                        break;
                    case 7:
                        button7.Text = nadsprotnik.ToString();
                        break;
                    case 8:
                        button8.Text = nadsprotnik.ToString();
                        break;
                    case 9:
                        button9.Text = nadsprotnik.ToString();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void omogociPozeto()
        {
            Button[] buttons = { button1, button2, button3, button4, button5, button6, button7, button8, button9 };
            foreach (var button in buttons)
            {
                if (button.Text == "")
                    button.Enabled = true;
            }
        }
        private void prepreciPotezo()
        {
            Button[] buttons = { button1, button2, button3, button4, button5, button6, button7, button8, button9 };
            foreach (var button in buttons)
            {
                button.Enabled = false;
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Recver.WorkerSupportsCancellation = true;
            Recver.CancelAsync();
            if (Server != null)
            {
                Server.Stop();
            }
        }
    }
}