using System;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Timers;
using System.Text.RegularExpressions;
using System.Drawing;

namespace BattleShips
{
    public class Client
    {
        public static Client SelfRef { get; set; }
        private bool _clientOk;
        public bool ClientOk
        {    
            //Пользуется свойством CurrentUICulture текущего потока для всех обращений к ресурсу, модуль Resources.Designer.cs
            get { return _clientOk; }  
            set { _clientOk = value; }
        }

        private string _messageReceived;
        private IPAddress _ip;
        private static System.Timers.Timer aTimer;
        private IPEndPoint _serverIpEndPoint;
        private TcpClient _tcpClient;
        private Thread _threadTcpClient;
        private StreamWriter _swSender;
        private StreamReader _swReader;
        private const Int32 Port = 80;
        
        private bool _goOn = false;
        
        // Время ожидания для аутентификации с сервером
        private void RunMe(int ms)
        {
            aTimer = new System.Timers.Timer(ms);
            aTimer.Enabled = true;
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            try
            { // Тестирование соединения с сервером
                _tcpClient = new TcpClient();
                _serverIpEndPoint = new IPEndPoint(_ip, Port);
                _tcpClient.Connect(_serverIpEndPoint);
                _threadTcpClient = new Thread(new ThreadStart(ReceiveMessage));
                _threadTcpClient.Start();
                _threadTcpClient.IsBackground = true;
                if (_tcpClient.Connected)
                    StopMe();
                _swSender = new StreamWriter(_tcpClient.GetStream());
                SendMessage("~t~e~s~t~");
            }
            catch (Exception)
            {

            }
        }

        private void StopMe()
        {
            try
            {
                if (aTimer.Enabled)
                {
                    aTimer.Enabled = false;
                }
                else if (!aTimer.Enabled)
                {
                    aTimer.Enabled = true;
                }
            }
            catch (Exception)
            {
                
            }
        }

        public Client()
        {
            SelfRef = this;
        }
        
        public void StartClient(IPAddress ipAddress)
        {
            _ip = ipAddress;
            RunMe(1000);
        }

        // получение и запись данных о выстрелах за ход 
        public void SendMessage(string message)
        {
            try
            {
                _swSender.WriteLine(message);
                _swSender.Flush();
            }
            catch (Exception e)
            {
                CloseConnection();
                MessageBox.Show(e.ToString());
            }
        }

        private void Checker(string text)
        // отклики клиента при сетевой игре на события связанные с формами Connection и Form1
        {
            if (_messageReceived == "~o~k~")
            {
                string msg = "#N" + Players.ClientName;
                SendMessage(msg);
            }
            else if (_messageReceived[0] == '#' && _messageReceived[1] == 'N')
            {
                _messageReceived = _messageReceived.Remove(0, 1).Remove(0, 1);
                Players.ServerName = _messageReceived;
                OkChek.ClientOk = true;
                SendMessage("#ClientOK");
            }
            else if (_messageReceived == "#ServerOK")
            {
                OkChek.ServerOk = true;
            }
            else if (_messageReceived[0] == '#' && _messageReceived[1] == 'M')
            {
                _messageReceived = _messageReceived.Remove(0, 1).Remove(0, 1);
                Form1.SelfRef.RichTextBoxUpdateText("client", _messageReceived);
            }
            else if (_messageReceived == "#Cserver done")
            {
                Form1.SelfRef.LanForm("server done");
                ProgramDetect.ServerReady = true;
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
                int def = Convert.ToInt32(_messageReceived.Remove(0,2));
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
        {   // передача значений клеток выстрелов по сети в течение хода
            int[] coords = Regex.Replace(message, @"\D", " ", RegexOptions.Compiled).Split(default(Char[]), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            Point newpoint = new Point(coords[0], coords[1]);
            return newpoint;
        }

        public void ReceiveMessage()
        {    // проверка и поддержание соединения, в случае ошибки уведомление
            try
            {
                _swReader = new StreamReader(_tcpClient.GetStream());
                _goOn = true;
                while (_goOn)
                {
                    _messageReceived = _swReader.ReadLine();
                    if (_messageReceived == "")
                    return;

                    Checker(_messageReceived);
                    //MessageBox.Show(_messageReceived); //Для отладки
                }
            }
            catch (Exception e)
            {
                Form1.SelfRef.DisableAll(true);
                CloseConnection();
                _goOn = false;
                MessageBox.Show("Удаленный сервер разорвал соединение. Перезапустите игру чтобы начать сначала." /*+ e.ToString()*/, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CloseConnection()
        {   // при потере соединения, или намеренном разрыве соединения c сервером игра прекращается
            try
            {
                if (_tcpClient.Connected)
                    _tcpClient.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
