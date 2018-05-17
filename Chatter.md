using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

// Весь отвечает за сетевой игровой чат, логи-подсказки игроку и констатацию событий за прошедший ход
namespace BattleShips
{
    // Считывает инфу о происходящем, чтоб показать ее игроку 
    public class Chatter
    {
        public static Chatter SelfRef { get; set; }

        public Chatter()
        {
            SelfRef = this;
        }

        private void GameWriteNotificationFunc(RichTextBox obj, string notification)
        {     // отправляет по ip сообщение в чат 
            if (ProgramDetect.Running == "server")
                Server.SelfRef.SendData("#GM" + notification);
            else if (ProgramDetect.Running == "client")
                Client.SelfRef.SendMessage("#GM" + notification);

            string text = " Игра: " + notification + "\n";
            obj.Text = obj.Text + text;
            obj.SelectionStart = obj.Text.Length -1;
            obj.ScrollToCaret();
        }
             // отправленное сопернику сообщение показывается в чате также у того, кто отправлял
             // если игрок играет с ИИ, то отправленное сообщение просто показывается в чате у игрока
        public void GameWriteNotification(RichTextBox obj, string notification)
        {
            if (obj.InvokeRequired)
            {
                obj.Invoke(new Action(delegate { GameWriteNotificationFunc(obj, notification); }));
            }
            else GameWriteNotificationFunc(obj, notification);
        }

        public void GameUpdateNotification(RichTextBox obj, string not)
        {
            if (obj.InvokeRequired)
                obj.Invoke(new Action(delegate { GameUpdateNotificationFunc(obj, not); }));
            else GameUpdateNotificationFunc(obj, not);
        }

        public void GameUpdateNotificationFunc(RichTextBox obj, string not)
        {
            string text = " Игра: " + not + "\n";
            obj.Text = obj.Text + text;
            obj.SelectionStart = obj.Text.Length;
            obj.ScrollToCaret();
        }
        // вывод в строку информации чей сейчас ход
        public void UpdateStatusLbl1(ToolStripStatusLabel obj, string not)
        {
            obj.Text = not + "...";
        }

        public void UpdateGrouBoxText(GroupBox obj, string not)
        {
            obj.Text = not;
        }
        // Если сделано что-то против правил, окно об ошибке
        public void ErrorMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
