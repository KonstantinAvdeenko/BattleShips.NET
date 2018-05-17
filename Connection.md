using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;

namespace BattleShips
{
    public partial class Connection : Form
    {   // запись ip адреса пк в роли сервера или соединение с сервером по записанному адресу
        private Regex _regIp = new Regex(@"\b(([01]?\d?\d|2[0-4]\d|25[0-5])\.){3}([01]?\d?\d|2[0-4]\d|25[0-5])\b");
        private IPAddress _ip;
        public delegate void LabelUpdateDelegate(string text);
        public void LabelUpdate(string text)
        {
            if (labelConnectionStatus.InvokeRequired)
                labelConnectionStatus.Invoke(new LabelUpdateDelegate(s => labelConnectionStatus.Text = s), text);
            else labelConnectionStatus.Text = text;
        }

        #region PROGRAM
        public Connection()
        // определение ПК в роли клиента или сервера по информации в форме
        {
            InitializeComponent();
        }

        private void PrBarStartAnimation()
        {
            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 30;
            panel_Progress.Visible = true;
        }

        private void PrBarStopAnimation()
        {
            panel_Progress.Visible = false;
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.MarqueeAnimationSpeed = 0;
            progressBar1.Visible = false;
        }

        private void radioButton_Server_CheckedChanged(object sender, EventArgs e)
        {
            textBox_ServerIP.Enabled =! radioButton_Server.Checked;
            ProgramDetect.Running = "server";
        }

        private void radioButton_Client_CheckedChanged(object sender, EventArgs e)
        {
            textBox_ServerIP.Enabled = radioButton_Client.Checked;
            ProgramDetect.Running = "client";
        }
        #endregion

        private void textBox_name_TextChanged(object sender, EventArgs e)
        {
            button_Connect.Enabled = (textBox_name.Text.Length > 2);
        }

        private void PrepareForm(bool state)
        { // проверка всего записанного в форму на соответствие минимальным требованиям заполнения
            textBox_name.Enabled = state;
            button_Connect.Enabled = state;
            button_Cancel.Enabled = !state;
            if (radioButton_Client.Checked)
                textBox_ServerIP.Enabled = state;
        }

        private void button_Connect_Click(object sender, EventArgs e)
        {
            if ( _regIp.IsMatch(textBox_ServerIP.Text))
            {
                PrBarStartAnimation();
                timer1.Start();
                //настройка формы
                PrepareForm(false);

                if (radioButton_Server.Checked)
                {  // пк в роли сервера
                    Players.ServerName = textBox_name.Text;
                    Server server = new Server();
                    server.ServerStart();

                }
                else if (radioButton_Client.Checked)
                {   // пк в роли клиета и подключение по ip к серверу
                    Players.ClientName = textBox_name.Text;
                    try
                    {
                        Client client = new Client();
                        _ip = IPAddress.Parse(textBox_ServerIP.Text);
                        client.StartClient(_ip);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        throw;
                    }
                } 
            }
            else
            {
                MessageBox.Show("Введен некорректный IP-адрес. Пожалуйста, попробуйте еще раз.");
            }
            
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        { // при отмене соединение закрывается вместе с сетевым режимом игры
            switch (ProgramDetect.Running)
            {
                case "server":
                    Server.SelfRef.CloseConnection(1);
                    break;
                case "client":
                    Client.SelfRef.CloseConnection();
                    break;
            }
            PrBarStopAnimation();
            textBox_name.Enabled = true;
            ProgramDetect.Running = null;
            PrepareForm(true);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (OkChek.ServerOk && OkChek.ClientOk)
            { // таймер на каждый ход игроков при сетевой игре, ходы делаются в форме Form1 как и при игре с ИИ
                timer1.Stop();
                if (Form1.SelfRef != null)
                {
                    Form1.SelfRef.DisableNewLanGame(true);
                    Form1.SelfRef.HideSender("show");
                    Form1.SelfRef.LanGame = true;
                    Form1.SelfRef.LeftPanel("grbox");
                    Form1.SelfRef.ServerShoots = (ProgramDetect.Running == "server");
                    Form1.SelfRef.ClientShoots = false;
                    Form1.SelfRef.LanForm("wait");
                    Form1.SelfRef.GamePreStart = true;
                }
                Close();
            }
        }

        private void textBox_name_KeyDown(object sender, KeyEventArgs e)

        { // поддерживается режим копипасты при помощи горячих клавиш
            if (e.KeyData == Keys.Enter && textBox_name.Text.Length >= 3)
                button_Connect_Click(sender, e);
            
        }

        private void textBox_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (Char)Keys.V) && (ModifierKeys == Keys.Control))
            {
                textBox_name.Paste();
                e.Handled = true;
            }
            else if ((e.KeyChar == (Char)Keys.C) && (ModifierKeys == Keys.Control))
            {
                textBox_name.Copy();
                e.Handled = true;
            }
            else if ((e.KeyChar == (Char)Keys.X) && (ModifierKeys == Keys.Control))
            {
                textBox_name.Cut();
                e.Handled = true;
            }
            else if (!Char.IsLetter(e.KeyChar))
            {
                if (e.KeyChar != (Char)Keys.Back)
                    e.Handled = true;
            }
            
        }

        private void textBox_ServerIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (Char)Keys.V) && (ModifierKeys == Keys.Control))
            {
                textBox_ServerIP.Paste();
                e.Handled = true;
            }
            else if ((e.KeyChar == (Char)Keys.C) && (ModifierKeys == Keys.Control) )
            {
                textBox_ServerIP.Copy();
                e.Handled = true;
            }
            else if ((e.KeyChar == (Char)Keys.X) && (ModifierKeys == Keys.Control))
            {
                textBox_ServerIP.Cut();
                e.Handled = true;
            }
            else if ( (!(Char.IsDigit(e.KeyChar)) && !((e.KeyChar == '.')  && (textBox_ServerIP.Text.Length != 0))) )
            {
                if (e.KeyChar != (char)Keys.Back)
                    e.Handled = true;
            }
        }
    }

    static class Players
    { // вывод введенных имен игроков играющих в текущее время до конца игры
        public static string ClientName { get; set; }
        public static string ServerName { get; set; }
    }
    static class OkChek
    { // подключается к модулям Client и Server соответственно
      // ради свойства CurrentUICulture текущего потока для всех обращений к ресурсу в модуле Resources.Designer.cs
        public static bool ClientOk { get; set; }
        public static bool ServerOk { get; set; }
    }
}
