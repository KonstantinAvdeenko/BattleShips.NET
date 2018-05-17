using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;

namespace BattleShips
{
    public class Server
    {
        public static Server SelfRef { get; set; }

        private TcpListener _tcpListenerServer;
        private TcpClient _connectedClient;
        private Thread _threadTcpListenerServer;
        private Thread _threadConnectedClient;
        private StreamReader _swReader;
        private StreamWriter _swWriter;
        private bool _clientConnected = false;
        private const Int32 Port = 80;
        private string _messageReceived;
        private bool _goOn = false;
        private bool _serverOk;
       
        public bool ServerOk

            // пользуется свойством CurrentUICulture текущего потока 
            // для создания возможности всех обращений к ресурсу, модуль Resources.Designer.cs
        {
            get { return _serverOk; }
            set { _serverOk = value; }
        }

        public string MessageReceived
             
            // возврат сообщения от сервера
        {
            get { return _messageReceived; }
            set { _messageReceived = value; }
        }

        public Server()
        {
            SelfRef = this;
        }

        // создание сервера для подсоединения к нему клиента по ip через сокет порт или вывод сообщения об ошибке
        public void ServerStart()
        {
            try
            {    
                _tcpListenerServer = new TcpListener(IPAddress.Any, Port);
                _tcpListenerServer.Start();
                _threadTcpListenerServer = new Thread(new ThreadStart(Listen));
                _threadTcpListenerServer.Start();
                _threadTcpListenerServer.IsBackground = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void Listen()
        {
            try
            { // подключение стороннего клиента по ip к созданному серверу
                while (_clientConnected == false)
                {
                    _connectedClient = _tcpListenerServer.AcceptTcpClient();
                    _threadConnectedClient = new Thread(new ParameterizedThreadStart(ReceiveData));
                    _threadConnectedClient.Start(_connectedClient);
                    _threadConnectedClient.IsBackground = true;
                    _clientConnected = true;
                    _swWriter = new StreamWriter(_connectedClient.GetStream());
                }
            }
            catch (Exception)
            {
                
            }
            
        }

        private void Checker(string text)
        // отклики сервера при сетевой игре на события связанные с формами Connection и Form1
        {
            if (_messageReceived == "~t~e~s~t~")
            {
                SendData("~o~k~");
                _serverOk = true;
            }
            else if (_messageReceived[0] == '#' && _messageReceived[1] == 'N')
            {
                _messageReceived = _messageReceived.Remove(0, 1).Remove(0, 1);
                Players.ClientName = _messageReceived;
                SendData("#N" + Players.ServerName);
            }
            else if (_messageReceived == "#ClientOK")
            {
                OkChek.ClientOk = true;
                OkChek.ServerOk = true;
                SendData("#ServerOK");
            }
            else if (_messageReceived[0] == '#' && _messageReceived[1] == 'M')
            {
                _messageReceived = _messageReceived.Remove(0, 1).Remove(0, 1);
                Form1.SelfRef.RichTextBoxUpdateText("server", _messageReceived);
            }
            else if (_messageReceived == "#Cclient done")
            {
                Form1.SelfRef.LanForm("client done");
                ProgramDetect.ClientReady = true;
            }
            else if (_messageReceived[0] == '#' && _messageReceived[1] == 'G' && _messageReceived[2] == 'M')
            {
                Form1.SelfRef.ReceiveGM(_messageReceived.Remove(0, 3));
            }
            else if (_messageReceived[0] == '#' && _messageReceived[1] == 'Y')
            {
                _messageReceived = _messageReceived.Remove(0, 2);
                Form1.SelfRef.ReceiveShoot(GetPoint(_messageReceived));
            }
            else if (_messageReceived[0] == '#' && _messageReceived[1] == 'U')
            {
                int def = Convert.ToInt32(_messageReceived.Remove(0, 2));
                Form1.SelfRef.ReceiveShootAnswer(def);
            }
            else if (_messageReceived[0] == '#' && _messageReceived[1] == 'O')
            {
                Form1.SelfRef.ReceiveObvodkaPoint(GetPoint(_messageReceived.Remove(0, 2)));
            }
            else if (_messageReceived == "~#EndGame#~")
            {
                Form1.SelfRef.Loose();
            }
            else if (_messageReceived[0] == '#' && _messageReceived[1] == 'J')
            {
                Form1.SelfRef.ReceiveLeftShipPoint(GetPoint(_messageReceived.Remove(0, 2)));
            }
            else if (_messageReceived == ProgramDetect.Cheat)
            {
                Form1.SelfRef.SendLeftShipPoint();
            }
        }

        private Point GetPoint(string message)

        { // передача значений клеток выстрелов по сети в течение хода
            int[] coords = Regex.Replace(message, @"\D", " ", RegexOptions.Compiled).Split(default(Char[]), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            Point newpoint = new Point(coords[0], coords[1]);
            return newpoint;
        }

        public void ReceiveData(object client)

        { // проверка и поддержание соединения, в случае ошибки уведомление
            TcpClient connectedClient = (TcpClient) client;
            _swReader = new StreamReader(connectedClient.GetStream());
            _goOn = true;
            while (_goOn)
            {
                try
                {
                    _messageReceived = _swReader.ReadLine();
                    if (_messageReceived == "")
                    return;

                    Checker(_messageReceived);
                    //MessageBox.Show(_messageReceived); //для отладки
                }
                catch (Exception e)
                {
                    Form1.SelfRef.DisableAll(true);
                    CloseConnection(2);
                    _goOn = false;
                    MessageBox.Show("Удаленный клиент разорвал соединение. Перезапустите игру чтобы начать сначала."/* + e.ToString()*/, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
        }

        public void SendData(string message)
        
        {  //передача и запись данных о выстрелах за ход 
            try
            {
                _swWriter.WriteLine(message);
                _swWriter.Flush();
            }
            catch (IOException e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void CloseConnection(int selector)

        // при потере соединения, или намеренном разрыве соединения сервером или клиентом игра прекращается
        {
            try
            {
                if (selector == 1)
                {
                    _tcpListenerServer.Stop();
                }
                else if (selector == 2)
                {
                    _tcpListenerServer.Stop();

                    if (_connectedClient.Connected)
                    _connectedClient.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
