using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace BattleShips
{

    public partial class Form1 : Form
    {
        #region VARIABLES
        //Глобальные переменные:
        public bool ServerShoots;
        public bool ClientShoots;
        public bool LanGame;
        public Chatter Chatter = new Chatter();
        public bool GamePreStart = false;
        public bool UserReadyToGame;
        public bool UserShoots = false;
        public bool ComputerShoots = false;
        public string Raspolojenie = "horizontal";
        public bool CanPut = true;
        public int Ships4Left = 1;
        public int Ships3Left = 2;
        public int Ships2Left = 3;
        public int Ships1Left = 4;
        public int UserCellsLeft = 20;
        public int CompCellsLeft = 20;

        //Частные переменные:
        private const string Viraz = ":;() ?!-=+/*";
        private bool _ship4Painting = false;
        private bool _ship3Painting = false;
        private bool _ship2Painting = false;
        private bool _ship1Painting = false;
        private string ShipCell = Environment.CurrentDirectory + @"\images\ship_cell.jpg";
        private string ShipClean = Environment.CurrentDirectory + @"\images\clean.jpg";
        private string Dead = Environment.CurrentDirectory + @"\images\dead.jpg";
        private string Miss = Environment.CurrentDirectory + @"\images\miss.jpg";
        #endregion

        #region SHIPS
        private List<Point> Ships = new List<Point>();
        private List<Point> ShipsDead = new List<Point>();
        private List<Point> UserMisses = new List<Point>();
        private List<Point> CompMisses = new List<Point>();
        private List<Point> CompShips = new List<Point>();
        private List<Point> CompShipsDead = new List<Point>();
        private List<Point> Queue = new List<Point>();
        private List<Point> QueueWrong = new List<Point>();
        private List<Point> UserShip4_1 = new List<Point>();
        private List<Point> UserShip4_1Obvodka = new List<Point>();
        private List<Point> UserShip3_1 = new List<Point>();
        private List<Point> UserShip3_2 = new List<Point>();
        private List<Point> UserShip3_1Obvodka = new List<Point>();
        private List<Point> UserShip3_2Obvodka = new List<Point>();
        private List<Point> UserShip2_1 = new List<Point>();
        private List<Point> UserShip2_2 = new List<Point>();
        private List<Point> UserShip2_3 = new List<Point>();
        private List<Point> UserShip2_1Obvodka = new List<Point>();
        private List<Point> UserShip2_2Obvodka = new List<Point>();
        private List<Point> UserShip2_3Obvodka = new List<Point>();
        private List<Point> UserShip1_1 = new List<Point>();
        private List<Point> UserShip1_2 = new List<Point>();
        private List<Point> UserShip1_3 = new List<Point>();
        private List<Point> UserShip1_4 = new List<Point>();
        private List<Point> UserShip1_1Obvodka = new List<Point>();
        private List<Point> UserShip1_2Obvodka = new List<Point>();
        private List<Point> UserShip1_3Obvodka = new List<Point>();
        private List<Point> UserShip1_4Obvodka = new List<Point>();
        private List<Point> CompShip4_1 = new List<Point>();
        private List<Point> CompShip4_1Obvodka = new List<Point>();
        private List<Point> CompShip3_1 = new List<Point>();
        private List<Point> CompShip3_2 = new List<Point>();
        private List<Point> CompShip3_1Obvodka = new List<Point>();
        private List<Point> CompShip3_2Obvodka = new List<Point>();
        private List<Point> CompShip2_1 = new List<Point>();
        private List<Point> CompShip2_2 = new List<Point>();
        private List<Point> CompShip2_3 = new List<Point>();
        private List<Point> CompShip2_1Obvodka = new List<Point>();
        private List<Point> CompShip2_2Obvodka = new List<Point>();
        private List<Point> CompShip2_3Obvodka = new List<Point>();
        private List<Point> CompShip1_1 = new List<Point>();
        private List<Point> CompShip1_2 = new List<Point>();
        private List<Point> CompShip1_3 = new List<Point>();
        private List<Point> CompShip1_4 = new List<Point>();
        private List<Point> CompShip1_1Obvodka = new List<Point>();
        private List<Point> CompShip1_2Obvodka = new List<Point>();
        private List<Point> CompShip1_3Obvodka = new List<Point>();
        private List<Point> CompShip1_4Obvodka = new List<Point>();
        List<List<Point>> AllUserShips = new List<List<Point>>();
        List<List<Point>> AllUserShipsObvodka = new List<List<Point>>();
        #endregion

        public Form1()
        {  // возможность для пользователя дополнительных настроек формы для игры в Морской бой 
            InitializeComponent();            
            SelfRef = this;
            звукToolStripMenuItem.Checked = Properties.Settings.Default.Sound;
        }

        private void MakeAllUserShipsList()
        {  // фиксация добавления и уничтожения кораблей, что сопровождается обводкой корабля
            AllUserShips.Clear();
            AllUserShips.Add(UserShip4_1);
            AllUserShips.Add(UserShip3_1);
            AllUserShips.Add(UserShip3_2);
            AllUserShips.Add(UserShip2_1);
            AllUserShips.Add(UserShip2_2);
            AllUserShips.Add(UserShip2_3);
            AllUserShips.Add(UserShip1_1);
            AllUserShips.Add(UserShip1_2);
            AllUserShips.Add(UserShip1_3);
            AllUserShips.Add(UserShip1_4);
            AllUserShipsObvodka.Clear();
            AllUserShipsObvodka.Add(UserShip4_1Obvodka);
            AllUserShipsObvodka.Add(UserShip3_1Obvodka);
            AllUserShipsObvodka.Add(UserShip3_2Obvodka);
            AllUserShipsObvodka.Add(UserShip2_1Obvodka);
            AllUserShipsObvodka.Add(UserShip2_2Obvodka);
            AllUserShipsObvodka.Add(UserShip2_3Obvodka);
            AllUserShipsObvodka.Add(UserShip1_1Obvodka);
            AllUserShipsObvodka.Add(UserShip1_2Obvodka);
            AllUserShipsObvodka.Add(UserShip1_3Obvodka);
            AllUserShipsObvodka.Add(UserShip1_4Obvodka);            
        }

        #region ONE PLAYER

        public static Form1 SelfRef { get; set; }

        private void ControlEnabler1(bool state)

        {  // отклики при одиночной игре на события связанные с формой Form1
            if (groupBox1.InvokeRequired)
                groupBox1.Invoke(new Action(delegate { groupBox1.Visible = true; }));
            else groupBox1.Visible = true;

            if (chkBoxUser.InvokeRequired)
                chkBoxUser.Invoke(new Action(delegate { chkBoxUser.Enabled = state; }));
            else chkBoxUser.Enabled = state;

            if (chkBoxRandom.InvokeRequired)
                chkBoxRandom.Invoke(new Action(delegate { chkBoxRandom.Enabled = state; }));
            else chkBoxRandom.Enabled = state;

            if (button1.InvokeRequired)
                button1.Invoke(new Action(delegate { button1.Enabled = state; }));
            else button1.Enabled = state;

            if (dataGridView1.InvokeRequired)
                dataGridView1.Invoke(new Action(delegate { dataGridView1.Enabled = state; }));
            else dataGridView1.Enabled = state;
        }

        private void ControlEnablerUser(bool state)

        {   // проверка все ли корабли раставлены
            radioButton4.Enabled = state;
            radioButton3.Enabled = state;
            radioButton2.Enabled = state;
            radioButton1.Enabled = state;
            listBox1.Enabled = state;
        }

        private void ControlEnablerRandom(bool state)
        {  // проверка корабли будут раставляться случайно или вручную
            button3.Enabled = state;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HideSender("hide");
            for (int i = 1; i < 11; i++)
            { // проверка все ли корабли раставлены по правилам вертикально или горизонтально
                string[] myRow = { i.ToString() };
                dataGridView2.Rows.Add(myRow);
                dataGridView2.Rows[i - 1].Height = 30;
                dataGridView1.Rows.Add(myRow);
                dataGridView1.Rows[i - 1].Height = 30;
            } 
            // вывод советов в лог формы
            Chatter.GameWriteNotification(richTextBox2, "Добро пожаловать в игру Морской Бой");
            Chatter.GameWriteNotification(richTextBox2, "Начните новую игру с помощью меню");
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {  
            const string message = "Вы действительно хотите выйти из игры?";
            const string caption = "Выход из игры";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void ShipAdd(DataGridView obj, int columnIndex, int rowIndex)
        {  // проверка по правилам кораблей по вертикали считая количество и палубы кораблей
            CanPut = true;
            if (Raspolojenie == "vertical")
            {
                if (_ship4Painting && Ships4Left != 0)
                {
                    if (rowIndex >= 7)
                    {
                        CanPut = false;
                    }
                    else if (columnIndex > 1 && columnIndex < 10 && rowIndex > 0 && rowIndex < 6)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 5; j++)
                            {
                                UserShip4_1Obvodka.Add(new Point(i, j));
                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (columnIndex > 1 && columnIndex < 10 && rowIndex == 0)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                        {
                            for (int j = rowIndex; j < rowIndex + 5; j++)
                            {
                                UserShip4_1Obvodka.Add(new Point(i, j));
                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (columnIndex > 1 && columnIndex < 10 && rowIndex == 6)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 4; j++)
                            {
                                UserShip4_1Obvodka.Add(new Point(i, j));
                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (columnIndex == 1 && rowIndex > 0 && rowIndex < 6)
                    {
                        for (int i = columnIndex; i < columnIndex + 2; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 5; j++)
                            {
                                UserShip4_1Obvodka.Add(new Point(i, j));
                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (columnIndex == 1 && rowIndex == 6)
                    {
                        for (int i = columnIndex; i < columnIndex + 2; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 4; j++)
                            {
                                UserShip4_1Obvodka.Add(new Point(i, j));
                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (columnIndex == 10 && rowIndex > 0 && rowIndex < 6)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 1; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 5; j++)
                            {
                                UserShip4_1Obvodka.Add(new Point(i, j));
                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (columnIndex == 10 && rowIndex == 6)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 1; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 4; j++)
                            {
                                UserShip4_1Obvodka.Add(new Point(i, j));
                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (columnIndex == 1 && rowIndex == 0)
                    {
                        for (int i = columnIndex; i < columnIndex + 2; i++)
                        {
                            for (int j = rowIndex; j < rowIndex + 5; j++)
                            {
                                UserShip4_1Obvodka.Add(new Point(i, j));
                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (columnIndex == 10 && rowIndex == 0)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 1; i++)
                        {
                            for (int j = rowIndex; j < rowIndex + 5; j++)
                            {
                                UserShip4_1Obvodka.Add(new Point(i, j));
                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }

                    if (CanPut)
                    {
                        for (int i = rowIndex; i < rowIndex + 4; i++)
                        {
                            Ships.Add(new Point(columnIndex, i));
                            UserShip4_1.Add(new Point(columnIndex, i));
                            obj[columnIndex, i].Value = new Bitmap(ShipCell);
                            UserShip4_1Obvodka.Remove(new Point(columnIndex, i));
                        }
                        Ships4Left--;
                    }
                    else if (!CanPut)
                    {
                        UserShip4_1Obvodka.Clear();
                        Chatter.ErrorMessage("Невозможно расположить корабль!");
                    }
                }
                else if (_ship4Painting && Ships4Left == 0)
                {
                    Chatter.ErrorMessage("Все четырехпалубные корабли уже расположены!");
                }
                else if (_ship3Painting && Ships3Left != 0)
                {
                    if (rowIndex >= 8)
                    {

                        CanPut = false;
                    }
                    else if (columnIndex > 1 && columnIndex < 10 && rowIndex > 0 && rowIndex < 7)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 4; j++)
                            {
                                int ship3_1 = UserShip3_1.ToArray().Length;
                                if (ship3_1 < 3)
                                {
                                    UserShip3_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship3_1 == 3)
                                {
                                    UserShip3_2Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (columnIndex > 1 && columnIndex < 10 && rowIndex == 0)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                        {
                            for (int j = rowIndex; j < rowIndex + 4; j++)
                            {
                                int ship3_1 = UserShip3_1.ToArray().Length;
                                if (ship3_1 < 3)
                                {
                                    UserShip3_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship3_1 == 3)
                                {
                                    UserShip3_2Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (columnIndex > 1 && columnIndex < 10 && rowIndex == 7)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 3; j++)
                            {
                                int ship3_1 = UserShip3_1.ToArray().Length;
                                if (ship3_1 < 3)
                                {
                                    UserShip3_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship3_1 == 3)
                                {
                                    UserShip3_2Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (columnIndex == 1 && rowIndex > 0 && rowIndex < 7)
                    {
                        for (int i = columnIndex; i < columnIndex + 2; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 4; j++)
                            {
                                int ship3_1 = UserShip3_1.ToArray().Length;
                                if (ship3_1 < 3)
                                {
                                    UserShip3_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship3_1 == 3)
                                {
                                    UserShip3_2Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (columnIndex == 1 && rowIndex == 7)
                    {
                        for (int i = columnIndex; i < columnIndex + 2; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 3; j++)
                            {
                                int ship3_1 = UserShip3_1.ToArray().Length;
                                if (ship3_1 < 3)
                                {
                                    UserShip3_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship3_1 == 3)
                                {
                                    UserShip3_2Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (columnIndex == 10 && rowIndex > 0 && rowIndex < 7)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 1; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 4; j++)
                            {
                                int ship3_1 = UserShip3_1.ToArray().Length;
                                if (ship3_1 < 3)
                                {
                                    UserShip3_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship3_1 == 3)
                                {
                                    UserShip3_2Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (columnIndex == 10 && rowIndex == 7)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 1; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 3; j++)
                            {
                                int ship3_1 = UserShip3_1.ToArray().Length;
                                if (ship3_1 < 3)
                                {
                                    UserShip3_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship3_1 == 3)
                                {
                                    UserShip3_2Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (columnIndex == 1 && rowIndex == 0)
                    {
                        for (int i = columnIndex; i < columnIndex + 2; i++)
                        {
                            for (int j = rowIndex; j < rowIndex + 4; j++)
                            {
                                int ship3_1 = UserShip3_1.ToArray().Length;
                                if (ship3_1 < 3)
                                {
                                    UserShip3_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship3_1 == 3)
                                {
                                    UserShip3_2Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (columnIndex == 10 && rowIndex == 0)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 1; i++)
                        {
                            for (int j = rowIndex; j < rowIndex + 4; j++)
                            {
                                int ship3_1 = UserShip3_1.ToArray().Length;
                                if (ship3_1 < 3)
                                {
                                    UserShip3_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship3_1 == 3)
                                {
                                    UserShip3_2Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }


                    if (CanPut)
                    {
                        for (int i = rowIndex; i < rowIndex + 3; i++)
                        {
                            Ships.Add(new Point(columnIndex, i));

                            int ship3_1 = UserShip3_1.ToArray().Length;
                            if (ship3_1 < 3)
                            {
                                UserShip3_1.Add(new Point(columnIndex, i));
                                UserShip3_1Obvodka.Remove(new Point(columnIndex, i));
                            }
                            else if (ship3_1 == 3)
                            {
                                UserShip3_2.Add(new Point(columnIndex, i));
                                UserShip3_2Obvodka.Remove(new Point(columnIndex, i));
                            }

                            obj[columnIndex, i].Value = new Bitmap(ShipCell);
                        }
                        Ships3Left--;
                    }
                    else if (!CanPut)
                    {
                        int ship3_1 = UserShip3_1.ToArray().Length;
                        if (ship3_1 < 3)
                        {
                            UserShip3_1Obvodka.Clear();
                        }
                        else if (ship3_1 == 3)
                        {
                            UserShip3_2Obvodka.Clear();
                        }

                        Chatter.ErrorMessage("Невозможно расположить корабль!");
                    }
                }
                else if (_ship3Painting && Ships3Left == 0)
                {
                    Chatter.ErrorMessage("Все трехпалубные корабли уже расположены!");
                }
                else if (_ship2Painting && Ships2Left != 0)
                {
                    if (rowIndex == 9)
                    {
                        CanPut = false;
                    }
                    else if (columnIndex > 1 && columnIndex < 10 && rowIndex > 0 && rowIndex < 8)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 3; j++)
                            {
                                int ship2_1 = UserShip2_1.ToArray().Length;
                                int ship2_2 = UserShip2_2.ToArray().Length;

                                if (ship2_1 < 2)
                                {
                                    UserShip2_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 < 2)
                                {
                                    UserShip2_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 == 2)
                                {
                                    UserShip2_3Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (columnIndex > 1 && columnIndex < 10 && rowIndex == 0)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                        {
                            for (int j = rowIndex; j < rowIndex + 3; j++)
                            {
                                int ship2_1 = UserShip2_1.ToArray().Length;
                                int ship2_2 = UserShip2_2.ToArray().Length;

                                if (ship2_1 < 2)
                                {
                                    UserShip2_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 < 2)
                                {
                                    UserShip2_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 == 2)
                                {
                                    UserShip2_3Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (columnIndex > 1 && columnIndex < 10 && rowIndex == 8)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 2; j++)
                            {
                                int ship2_1 = UserShip2_1.ToArray().Length;
                                int ship2_2 = UserShip2_2.ToArray().Length;

                                if (ship2_1 < 2)
                                {
                                    UserShip2_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 < 2)
                                {
                                    UserShip2_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 == 2)
                                {
                                    UserShip2_3Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (columnIndex == 1 && rowIndex > 0 && rowIndex < 8)
                    {
                        for (int i = columnIndex; i < columnIndex + 2; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 3; j++)
                            {
                                int ship2_1 = UserShip2_1.ToArray().Length;
                                int ship2_2 = UserShip2_2.ToArray().Length;

                                if (ship2_1 < 2)
                                {
                                    UserShip2_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 < 2)
                                {
                                    UserShip2_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 == 2)
                                {
                                    UserShip2_3Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (columnIndex == 1 && rowIndex == 8)
                    {
                        for (int i = columnIndex; i < columnIndex + 2; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 2; j++)
                            {
                                int ship2_1 = UserShip2_1.ToArray().Length;
                                int ship2_2 = UserShip2_2.ToArray().Length;

                                if (ship2_1 < 2)
                                {
                                    UserShip2_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 < 2)
                                {
                                    UserShip2_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 == 2)
                                {
                                    UserShip2_3Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (columnIndex == 10 && rowIndex > 0 && rowIndex < 8)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 1; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 3; j++)
                            {
                                int ship2_1 = UserShip2_1.ToArray().Length;
                                int ship2_2 = UserShip2_2.ToArray().Length;

                                if (ship2_1 < 2)
                                {
                                    UserShip2_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 < 2)
                                {
                                    UserShip2_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 == 2)
                                {
                                    UserShip2_3Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (columnIndex == 10 && rowIndex == 8)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 1; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 2; j++)
                            {
                                int ship2_1 = UserShip2_1.ToArray().Length;
                                int ship2_2 = UserShip2_2.ToArray().Length;

                                if (ship2_1 < 2)
                                {
                                    UserShip2_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 < 2)
                                {
                                    UserShip2_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 == 2)
                                {
                                    UserShip2_3Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (columnIndex == 1 && rowIndex == 0)
                    {
                        for (int i = columnIndex; i < columnIndex + 2; i++)
                        {
                            for (int j = rowIndex; j < rowIndex + 3; j++)
                            {
                                int ship2_1 = UserShip2_1.ToArray().Length;
                                int ship2_2 = UserShip2_2.ToArray().Length;

                                if (ship2_1 < 2)
                                {
                                    UserShip2_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 < 2)
                                {
                                    UserShip2_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 == 2)
                                {
                                    UserShip2_3Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (columnIndex == 10 && rowIndex == 0)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 1; i++)
                        {
                            for (int j = rowIndex; j < rowIndex + 3; j++)
                            {
                                int ship2_1 = UserShip2_1.ToArray().Length;
                                int ship2_2 = UserShip2_2.ToArray().Length;

                                if (ship2_1 < 2)
                                {
                                    UserShip2_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 < 2)
                                {
                                    UserShip2_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 == 2)
                                {
                                    UserShip2_3Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }


                    if (CanPut)
                    {
                        for (int i = rowIndex; i < rowIndex + 2; i++)
                        {
                            Ships.Add(new Point(columnIndex, i));

                            int ship2_1 = UserShip2_1.ToArray().Length;
                            int ship2_2 = UserShip2_2.ToArray().Length;

                            if (ship2_1 < 2)
                            {
                                UserShip2_1.Add(new Point(columnIndex, i));
                                UserShip2_1Obvodka.Remove(new Point(columnIndex, i));
                            }
                            else if (ship2_1 == 2 && ship2_2 < 2)
                            {
                                UserShip2_2.Add(new Point(columnIndex, i));
                                UserShip2_2Obvodka.Remove(new Point(columnIndex, i));
                            }
                            else if (ship2_1 == 2 && ship2_2 == 2)
                            {
                                UserShip2_3.Add(new Point(columnIndex, i));
                                UserShip2_3Obvodka.Remove(new Point(columnIndex, i));
                            }

                            obj[columnIndex, i].Value = new Bitmap(ShipCell);
                        }
                        Ships2Left--;
                    }
                    else if (!CanPut)
                    {
                        int ship2_1 = UserShip2_1.ToArray().Length;
                        int ship2_2 = UserShip2_2.ToArray().Length;

                        if (ship2_1 < 2)
                        {
                            UserShip2_1Obvodka.Clear();
                        }
                        else if (ship2_1 == 2 && ship2_2 < 2)
                        {
                            UserShip2_2Obvodka.Clear();
                        }
                        else if (ship2_1 == 2 && ship2_2 == 2)
                        {
                            UserShip2_3Obvodka.Clear();
                        }

                        Chatter.ErrorMessage("Невозможно расположить корабль!");
                    }
                }
                else if (_ship2Painting && Ships2Left == 0)
                {
                    Chatter.ErrorMessage("Все двопалубные корабли уже расположены!");
                }
            }
            else if (Raspolojenie == "horizontal")
            { // проверка по правилам кораблей по горизонтали считая количество и палубы кораблей
                if (_ship4Painting && Ships4Left != 0)
                {
                    if (columnIndex >= 8)
                    {
                        CanPut = false;
                    }
                    else if (rowIndex > 0 && rowIndex < 9 && columnIndex > 1 && columnIndex < 7)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 5; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 2; j++)
                            {
                                UserShip4_1Obvodka.Add(new Point(i, j));
                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (rowIndex > 0 && rowIndex < 9 && columnIndex == 1)
                    {
                        for (int i = columnIndex; i < columnIndex + 5; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 2; j++)
                            {
                                UserShip4_1Obvodka.Add(new Point(i, j));
                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (rowIndex > 0 && rowIndex < 9 && columnIndex == 7)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 4; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 2; j++)
                            {
                                UserShip4_1Obvodka.Add(new Point(i, j));
                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (rowIndex == 9 && columnIndex > 1 && columnIndex < 7)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 5; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 1; j++)
                            {
                                UserShip4_1Obvodka.Add(new Point(i, j));
                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (rowIndex == 9 && columnIndex == 7)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 4; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 1; j++)
                            {
                                UserShip4_1Obvodka.Add(new Point(i, j));
                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (rowIndex == 0 && columnIndex > 1 && columnIndex < 7)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 5; i++)
                        {
                            for (int j = rowIndex; j < rowIndex + 2; j++)
                            {
                                UserShip4_1Obvodka.Add(new Point(i, j));
                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (rowIndex == 0 && columnIndex == 7)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 4; i++)
                        {
                            for (int j = rowIndex; j < rowIndex + 2; j++)
                            {
                                UserShip4_1Obvodka.Add(new Point(i, j));
                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (rowIndex == 9 && columnIndex == 1)
                    {
                        for (int i = columnIndex; i < columnIndex + 5; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 1; j++)
                            {
                                UserShip4_1Obvodka.Add(new Point(i, j));
                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (rowIndex == 0 && columnIndex == 1)
                    {
                        for (int i = columnIndex; i < columnIndex + 5; i++)
                        {
                            for (int j = rowIndex; j < rowIndex + 2; j++)
                            {
                                UserShip4_1Obvodka.Add(new Point(i, j));
                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }

                    if (CanPut)
                    {
                        for (int i = columnIndex; i < columnIndex + 4; i++)
                        {
                            Ships.Add(new Point(i, rowIndex));
                            UserShip4_1.Add(new Point(i, rowIndex));
                            UserShip4_1Obvodka.Remove(new Point(i, rowIndex));
                            obj[i, rowIndex].Value = new Bitmap(ShipCell);
                        }
                        Ships4Left--;
                    }
                    else if (!CanPut)
                    {
                        UserShip4_1.Clear();
                        Chatter.ErrorMessage("Невозможно расположить корабль!");
                    }

                }
                else if (_ship4Painting && Ships4Left == 0)
                {
                    Chatter.ErrorMessage("Все четырехпалубные корабли уже расположены!");
                }
                else if (_ship3Painting && Ships3Left != 0)
                {
                    if (columnIndex >= 9)
                    {
                        CanPut = false;
                    }
                    else if (rowIndex > 0 && rowIndex < 9 && columnIndex > 1 && columnIndex < 8)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 4; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 2; j++)
                            {
                                int ship3_1 = UserShip3_1.ToArray().Length;

                                if (ship3_1 < 3)
                                {
                                    UserShip3_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship3_1 == 3)
                                {
                                    UserShip3_2Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (rowIndex > 0 && rowIndex < 9 && columnIndex == 1)
                    {
                        for (int i = columnIndex; i < columnIndex + 4; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 2; j++)
                            {
                                int ship3_1 = UserShip3_1.ToArray().Length;

                                if (ship3_1 < 3)
                                {
                                    UserShip3_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship3_1 == 3)
                                {
                                    UserShip3_2Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (rowIndex > 0 && rowIndex < 9 && columnIndex == 8)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 3; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 2; j++)
                            {
                                int ship3_1 = UserShip3_1.ToArray().Length;

                                if (ship3_1 < 3)
                                {
                                    UserShip3_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship3_1 == 3)
                                {
                                    UserShip3_2Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (rowIndex == 9 && columnIndex > 1 && columnIndex < 8)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 4; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 1; j++)
                            {
                                int ship3_1 = UserShip3_1.ToArray().Length;

                                if (ship3_1 < 3)
                                {
                                    UserShip3_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship3_1 == 3)
                                {
                                    UserShip3_2Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (rowIndex == 9 && columnIndex == 8)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 3; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 1; j++)
                            {
                                int ship3_1 = UserShip3_1.ToArray().Length;

                                if (ship3_1 < 3)
                                {
                                    UserShip3_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship3_1 == 3)
                                {
                                    UserShip3_2Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (rowIndex == 0 && columnIndex > 1 && columnIndex < 8)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 4; i++)
                        {
                            for (int j = rowIndex; j < rowIndex + 2; j++)
                            {
                                int ship3_1 = UserShip3_1.ToArray().Length;

                                if (ship3_1 < 3)
                                {
                                    UserShip3_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship3_1 == 3)
                                {
                                    UserShip3_2Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (rowIndex == 0 && columnIndex == 8)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 3; i++)
                        {
                            for (int j = rowIndex; j < rowIndex + 2; j++)
                            {
                                int ship3_1 = UserShip3_1.ToArray().Length;

                                if (ship3_1 < 3)
                                {
                                    UserShip3_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship3_1 == 3)
                                {
                                    UserShip3_2Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (rowIndex == 9 && columnIndex == 1)
                    {
                        for (int i = columnIndex; i < columnIndex + 4; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 1; j++)
                            {
                                int ship3_1 = UserShip3_1.ToArray().Length;

                                if (ship3_1 < 3)
                                {
                                    UserShip3_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship3_1 == 3)
                                {
                                    UserShip3_2Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (rowIndex == 0 && columnIndex == 1)
                    {
                        for (int i = columnIndex; i < columnIndex + 4; i++)
                        {
                            for (int j = rowIndex; j < rowIndex + 2; j++)
                            {
                                int ship3_1 = UserShip3_1.ToArray().Length;

                                if (ship3_1 < 3)
                                {
                                    UserShip3_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship3_1 == 3)
                                {
                                    UserShip3_2Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }

                    if (CanPut)
                    {
                        for (int i = columnIndex; i < columnIndex + 3; i++)
                        {
                            Ships.Add(new Point(i, rowIndex));

                            int ship3_1 = UserShip3_1.ToArray().Length;

                            if (ship3_1 < 3)
                            {
                                UserShip3_1.Add(new Point(i, rowIndex));
                                UserShip3_1Obvodka.Remove(new Point(i, rowIndex));
                            }
                            else if (ship3_1 == 3)
                            {
                                UserShip3_2.Add(new Point(i, rowIndex));
                                UserShip3_2Obvodka.Remove(new Point(i, rowIndex));
                            }

                            obj[i, rowIndex].Value = new Bitmap(ShipCell);
                        }
                        Ships3Left--;
                    }
                    else if (!CanPut)
                    {
                        int ship3_1 = UserShip3_1.ToArray().Length;

                        if (ship3_1 < 3)
                        {
                            UserShip3_1Obvodka.Clear();
                        }
                        else if (ship3_1 == 3)
                        {
                            UserShip3_2Obvodka.Clear();
                        }

                        Chatter.ErrorMessage("Невозможно расположить корабль!");
                    }
                }
                else if (_ship3Painting && Ships3Left == 0)
                {
                    Chatter.ErrorMessage("Все трехпалубные корабли уже расположены!");
                }
                else if (_ship2Painting && Ships2Left != 0)
                {
                    if (columnIndex == 10)
                    {
                        CanPut = false;
                    }
                    else if (rowIndex > 0 && rowIndex < 9 && columnIndex > 1 && columnIndex < 9)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 3; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 2; j++)
                            {
                                int ship2_1 = UserShip2_1.ToArray().Length;
                                int ship2_2 = UserShip2_2.ToArray().Length;

                                if (ship2_1 < 2)
                                {
                                    UserShip2_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 < 2)
                                {
                                    UserShip2_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 == 2)
                                {
                                    UserShip2_3Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (rowIndex > 0 && rowIndex < 9 && columnIndex == 1)
                    {
                        for (int i = columnIndex; i < columnIndex + 3; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 2; j++)
                            {
                                int ship2_1 = UserShip2_1.ToArray().Length;
                                int ship2_2 = UserShip2_2.ToArray().Length;

                                if (ship2_1 < 2)
                                {
                                    UserShip2_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 < 2)
                                {
                                    UserShip2_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 == 2)
                                {
                                    UserShip2_3Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (rowIndex > 0 && rowIndex < 9 && columnIndex == 9)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 2; j++)
                            {
                                int ship2_1 = UserShip2_1.ToArray().Length;
                                int ship2_2 = UserShip2_2.ToArray().Length;

                                if (ship2_1 < 2)
                                {
                                    UserShip2_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 < 2)
                                {
                                    UserShip2_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 == 2)
                                {
                                    UserShip2_3Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (rowIndex == 9 && columnIndex > 1 && columnIndex < 9)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 3; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 1; j++)
                            {
                                int ship2_1 = UserShip2_1.ToArray().Length;
                                int ship2_2 = UserShip2_2.ToArray().Length;

                                if (ship2_1 < 2)
                                {
                                    UserShip2_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 < 2)
                                {
                                    UserShip2_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 == 2)
                                {
                                    UserShip2_3Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (rowIndex == 9 && columnIndex == 9)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 1; j++)
                            {
                                int ship2_1 = UserShip2_1.ToArray().Length;
                                int ship2_2 = UserShip2_2.ToArray().Length;

                                if (ship2_1 < 2)
                                {
                                    UserShip2_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 < 2)
                                {
                                    UserShip2_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 == 2)
                                {
                                    UserShip2_3Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (rowIndex == 0 && columnIndex > 1 && columnIndex < 9)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 3; i++)
                        {
                            for (int j = rowIndex; j < rowIndex + 2; j++)
                            {
                                int ship2_1 = UserShip2_1.ToArray().Length;
                                int ship2_2 = UserShip2_2.ToArray().Length;

                                if (ship2_1 < 2)
                                {
                                    UserShip2_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 < 2)
                                {
                                    UserShip2_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 == 2)
                                {
                                    UserShip2_3Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (rowIndex == 0 && columnIndex == 9)
                    {
                        for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                        {
                            for (int j = rowIndex; j < rowIndex + 2; j++)
                            {
                                int ship2_1 = UserShip2_1.ToArray().Length;
                                int ship2_2 = UserShip2_2.ToArray().Length;

                                if (ship2_1 < 2)
                                {
                                    UserShip2_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 < 2)
                                {
                                    UserShip2_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 == 2)
                                {
                                    UserShip2_3Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (rowIndex == 9 && columnIndex == 1)
                    {
                        for (int i = columnIndex; i < columnIndex + 3; i++)
                        {
                            for (int j = rowIndex - 1; j < rowIndex + 1; j++)
                            {
                                int ship2_1 = UserShip2_1.ToArray().Length;
                                int ship2_2 = UserShip2_2.ToArray().Length;

                                if (ship2_1 < 2)
                                {
                                    UserShip2_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 < 2)
                                {
                                    UserShip2_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 == 2)
                                {
                                    UserShip2_3Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }
                    else if (rowIndex == 0 && columnIndex == 1)
                    {
                        for (int i = columnIndex; i < columnIndex + 3; i++)
                        {
                            for (int j = rowIndex; j < rowIndex + 2; j++)
                            {
                                int ship2_1 = UserShip2_1.ToArray().Length;
                                int ship2_2 = UserShip2_2.ToArray().Length;

                                if (ship2_1 < 2)
                                {
                                    UserShip2_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 < 2)
                                {
                                    UserShip2_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship2_1 == 2 && ship2_2 == 2)
                                {
                                    UserShip2_3Obvodka.Add(new Point(i, j));
                                }

                                if (Ships.Contains(new Point(i, j)))
                                {
                                    CanPut = false;
                                }
                            }
                        }
                    }

                    if (CanPut)
                    {
                        for (int i = columnIndex; i < columnIndex + 2; i++)
                        {
                            Ships.Add(new Point(i, rowIndex));

                            int ship2_1 = UserShip2_1.ToArray().Length;
                            int ship2_2 = UserShip2_2.ToArray().Length;

                            if (ship2_1 < 2)
                            {
                                UserShip2_1.Add(new Point(i, rowIndex));
                                UserShip2_1Obvodka.Remove(new Point(i, rowIndex));
                            }
                            else if (ship2_1 == 2 && ship2_2 < 2)
                            {
                                UserShip2_2.Add(new Point(i, rowIndex));
                                UserShip2_2Obvodka.Remove(new Point(i, rowIndex));
                            }
                            else if (ship2_1 == 2 && ship2_2 == 2)
                            {
                                UserShip2_3.Add(new Point(i, rowIndex));
                                UserShip2_3Obvodka.Remove(new Point(i, rowIndex));
                            }

                            obj[i, rowIndex].Value = new Bitmap(ShipCell);
                        }
                        Ships2Left--;
                    }
                    else if (!CanPut)
                    {
                        int ship2_1 = UserShip2_1.ToArray().Length;
                        int ship2_2 = UserShip2_2.ToArray().Length;

                        if (ship2_1 < 2)
                        {
                            UserShip2_1Obvodka.Clear();
                        }
                        else if (ship2_1 == 2 && ship2_2 < 2)
                        {
                            UserShip2_2Obvodka.Clear();
                        }
                        else if (ship2_1 == 2 && ship2_2 == 2)
                        {
                            UserShip2_3Obvodka.Clear();
                        }

                        Chatter.ErrorMessage("Невозможно расположить корабль!");
                    }
                }
                else if (_ship2Painting && Ships2Left == 0)
                {
                    Chatter.ErrorMessage("Все двопалубные корабли уже расположены!");
                }
            }

            if (_ship1Painting && Ships1Left != 0)
            { // расположение по правилам однопалубных кораблей
                if (columnIndex > 1 && columnIndex < 10 && rowIndex > 0 && rowIndex < 9)
                {
                    for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                    {
                        for (int j = rowIndex - 1; j < rowIndex + 2; j++)
                        {
                            int ship1_1 = UserShip1_1.ToArray().Length;
                            int ship1_2 = UserShip1_2.ToArray().Length;
                            int ship1_3 = UserShip1_3.ToArray().Length;

                            if (ship1_1 == 0)
                            {
                                UserShip1_1Obvodka.Add(new Point(i, j));
                            }
                            else if (ship1_1 == 1 && ship1_2 == 0)
                            {
                                UserShip1_2Obvodka.Add(new Point(i, j));
                            }
                            else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                            {
                                UserShip1_3Obvodka.Add(new Point(i, j));
                            }
                            else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 1)
                            {
                                UserShip1_4Obvodka.Add(new Point(i, j));
                            }

                            if (Ships.Contains(new Point(i, j)))
                            {
                                CanPut = false;
                            }
                        }
                    }
                }
                else if (columnIndex == 1 && rowIndex > 0 && rowIndex < 9)
                {
                    for (int i = columnIndex; i < columnIndex + 2; i++)
                    {
                        for (int j = rowIndex - 1; j < rowIndex + 2; j++)
                        {
                            int ship1_1 = UserShip1_1.ToArray().Length;
                            int ship1_2 = UserShip1_2.ToArray().Length;
                            int ship1_3 = UserShip1_3.ToArray().Length;

                            if (ship1_1 == 0)
                            {
                                UserShip1_1Obvodka.Add(new Point(i, j));
                            }
                            else if (ship1_1 == 1 && ship1_2 == 0)
                            {
                                UserShip1_2Obvodka.Add(new Point(i, j));
                            }
                            else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                            {
                                UserShip1_3Obvodka.Add(new Point(i, j));
                            }
                            else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 1)
                            {
                                UserShip1_4Obvodka.Add(new Point(i, j));
                            }

                            if (Ships.Contains(new Point(i, j)))
                            {
                                CanPut = false;
                            }
                        }
                    }
                }
                else if (columnIndex == 10 && rowIndex > 0 && rowIndex < 9)
                {
                    for (int i = columnIndex - 1; i < columnIndex + 1; i++)
                    {
                        for (int j = rowIndex - 1; j < rowIndex + 2; j++)
                        {
                            int ship1_1 = UserShip1_1.ToArray().Length;
                            int ship1_2 = UserShip1_2.ToArray().Length;
                            int ship1_3 = UserShip1_3.ToArray().Length;

                            if (ship1_1 == 0)
                            {
                                UserShip1_1Obvodka.Add(new Point(i, j));
                            }
                            else if (ship1_1 == 1 && ship1_2 == 0)
                            {
                                UserShip1_2Obvodka.Add(new Point(i, j));
                            }
                            else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                            {
                                UserShip1_3Obvodka.Add(new Point(i, j));
                            }
                            else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 1)
                            {
                                UserShip1_4Obvodka.Add(new Point(i, j));
                            }

                            if (Ships.Contains(new Point(i, j)))
                            {
                                CanPut = false;
                            }
                        }
                    }
                }
                else if (rowIndex == 0 && columnIndex > 1 && columnIndex < 10)
                {
                    for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                    {
                        for (int j = rowIndex; j < rowIndex + 2; j++)
                        {
                            int ship1_1 = UserShip1_1.ToArray().Length;
                            int ship1_2 = UserShip1_2.ToArray().Length;
                            int ship1_3 = UserShip1_3.ToArray().Length;

                            if (ship1_1 == 0)
                            {
                                UserShip1_1Obvodka.Add(new Point(i, j));
                            }
                            else if (ship1_1 == 1 && ship1_2 == 0)
                            {
                                UserShip1_2Obvodka.Add(new Point(i, j));
                            }
                            else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                            {
                                UserShip1_3Obvodka.Add(new Point(i, j));
                            }
                            else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 1)
                            {
                                UserShip1_4Obvodka.Add(new Point(i, j));
                            }

                            if (Ships.Contains(new Point(i, j)))
                            {
                                CanPut = false;
                            }
                        }
                    }
                }
                else if (rowIndex == 9 && columnIndex > 1 && columnIndex < 10)
                {
                    for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                    {
                        for (int j = rowIndex - 1; j < rowIndex + 1; j++)
                        {
                            int ship1_1 = UserShip1_1.ToArray().Length;
                            int ship1_2 = UserShip1_2.ToArray().Length;
                            int ship1_3 = UserShip1_3.ToArray().Length;

                            if (ship1_1 == 0)
                            {
                                UserShip1_1Obvodka.Add(new Point(i, j));
                            }
                            else if (ship1_1 == 1 && ship1_2 == 0)
                            {
                                UserShip1_2Obvodka.Add(new Point(i, j));
                            }
                            else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                            {
                                UserShip1_3Obvodka.Add(new Point(i, j));
                            }
                            else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 1)
                            {
                                UserShip1_4Obvodka.Add(new Point(i, j));
                            }

                            if (Ships.Contains(new Point(i, j)))
                            {
                                CanPut = false;
                            }
                        }
                    }
                }
                else if (rowIndex == 0 && columnIndex == 1)
                {
                    for (int i = columnIndex; i < columnIndex + 2; i++)
                    {
                        for (int j = rowIndex; j < rowIndex + 2; j++)
                        {
                            int ship1_1 = UserShip1_1.ToArray().Length;
                            int ship1_2 = UserShip1_2.ToArray().Length;
                            int ship1_3 = UserShip1_3.ToArray().Length;

                            if (ship1_1 == 0)
                            {
                                UserShip1_1Obvodka.Add(new Point(i, j));
                            }
                            else if (ship1_1 == 1 && ship1_2 == 0)
                            {
                                UserShip1_2Obvodka.Add(new Point(i, j));
                            }
                            else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                            {
                                UserShip1_3Obvodka.Add(new Point(i, j));
                            }
                            else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 1)
                            {
                                UserShip1_4Obvodka.Add(new Point(i, j));
                            }

                            if (Ships.Contains(new Point(i, j)))
                            {
                                CanPut = false;
                            }
                        }
                    }
                }
                else if (rowIndex == 0 && columnIndex == 10)
                {
                    for (int i = columnIndex - 1; i < columnIndex + 1; i++)
                    {
                        for (int j = rowIndex; j < rowIndex + 2; j++)
                        {
                            int ship1_1 = UserShip1_1.ToArray().Length;
                            int ship1_2 = UserShip1_2.ToArray().Length;
                            int ship1_3 = UserShip1_3.ToArray().Length;

                            if (ship1_1 == 0)
                            {
                                UserShip1_1Obvodka.Add(new Point(i, j));
                            }
                            else if (ship1_1 == 1 && ship1_2 == 0)
                            {
                                UserShip1_2Obvodka.Add(new Point(i, j));
                            }
                            else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                            {
                                UserShip1_3Obvodka.Add(new Point(i, j));
                            }
                            else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 1)
                            {
                                UserShip1_4Obvodka.Add(new Point(i, j));
                            }

                            if (Ships.Contains(new Point(i, j)))
                            {
                                CanPut = false;
                            }
                        }
                    }
                }
                else if (rowIndex == 9 && columnIndex == 1)
                {
                    for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                    {
                        for (int j = rowIndex - 1; j < rowIndex + 1; j++)
                        {
                            int ship1_1 = UserShip1_1.ToArray().Length;
                            int ship1_2 = UserShip1_2.ToArray().Length;
                            int ship1_3 = UserShip1_3.ToArray().Length;

                            if (ship1_1 == 0)
                            {
                                UserShip1_1Obvodka.Add(new Point(i, j));
                            }
                            else if (ship1_1 == 1 && ship1_2 == 0)
                            {
                                UserShip1_2Obvodka.Add(new Point(i, j));
                            }
                            else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                            {
                                UserShip1_3Obvodka.Add(new Point(i, j));
                            }
                            else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 1)
                            {
                                UserShip1_4Obvodka.Add(new Point(i, j));
                            }

                            if (Ships.Contains(new Point(i, j)))
                            {
                                CanPut = false;
                            }
                        }
                    }
                }
                else if (rowIndex == 9 && columnIndex == 10)
                {
                    for (int i = columnIndex - 1; i < columnIndex + 1; i++)
                    {
                        for (int j = rowIndex - 1; j < rowIndex + 1; j++)
                        {
                            int ship1_1 = UserShip1_1.ToArray().Length;
                            int ship1_2 = UserShip1_2.ToArray().Length;
                            int ship1_3 = UserShip1_3.ToArray().Length;

                            if (ship1_1 == 0)
                            {
                                UserShip1_1Obvodka.Add(new Point(i, j));
                            }
                            else if (ship1_1 == 1 && ship1_2 == 0)
                            {
                                UserShip1_2Obvodka.Add(new Point(i, j));
                            }
                            else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                            {
                                UserShip1_3Obvodka.Add(new Point(i, j));
                            }
                            else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 1)
                            {
                                UserShip1_4Obvodka.Add(new Point(i, j));
                            }

                            if (Ships.Contains(new Point(i, j)))
                            {
                                CanPut = false;
                            }
                        }
                    }
                }

                if (CanPut)
                {
                    Ships.Add(new Point(columnIndex, rowIndex));

                    int ship1_1 = UserShip1_1.ToArray().Length;
                    int ship1_2 = UserShip1_2.ToArray().Length;
                    int ship1_3 = UserShip1_3.ToArray().Length;

                    if (ship1_1 == 0)
                    {
                        UserShip1_1.Add(new Point(columnIndex, rowIndex));
                        UserShip1_1Obvodka.Remove(new Point(columnIndex, rowIndex));
                    }
                    else if (ship1_1 == 1 && ship1_2 == 0)
                    {
                        UserShip1_2.Add(new Point(columnIndex, rowIndex));
                        UserShip1_2Obvodka.Remove(new Point(columnIndex, rowIndex));
                    }
                    else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                    {
                        UserShip1_3.Add(new Point(columnIndex, rowIndex));
                        UserShip1_3Obvodka.Remove(new Point(columnIndex, rowIndex));
                    }
                    else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 1)
                    {
                        UserShip1_4.Add(new Point(columnIndex, rowIndex));
                        UserShip1_4Obvodka.Remove(new Point(columnIndex, rowIndex));
                    }

                    obj[columnIndex, rowIndex].Value = new Bitmap(ShipCell);
                    Ships1Left--;
                }
                else if (!CanPut)
                {
                    int ship1_1 = UserShip1_1.ToArray().Length;
                    int ship1_2 = UserShip1_2.ToArray().Length;
                    int ship1_3 = UserShip1_3.ToArray().Length;

                    if (ship1_1 == 0)
                    {
                        UserShip1_1Obvodka.Clear();
                    }
                    else if (ship1_1 == 1 && ship1_2 == 0)
                    {
                        UserShip1_2Obvodka.Clear();
                    }
                    else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                    {
                        UserShip1_3Obvodka.Clear();
                    }
                    else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 1)
                    {
                        UserShip1_4Obvodka.Clear();
                    }
                    Chatter.ErrorMessage("Невозможно расположить корабль!");
                }
            }
            else if (_ship1Painting && Ships1Left == 0)
            {
                Chatter.ErrorMessage("Все однопалубные корабли уже расположены!");
            }
            updateStatusLabels();
            ReadyToGame(); // после расположения активация кнопки для возможности начать игру
        }

        public void ReadyToGame()
        {
            int result = Ships4Left + Ships3Left + Ships2Left + Ships1Left;
            button2.Enabled = (result == 0);
        }

        private void ShipsLeftSbros()
        { // счет общего количества палуб на всех кораблях
            Ships4Left = 1;
            Ships3Left = 2;
            Ships2Left = 3;
            Ships1Left = 4;
            UserCellsLeft = 20;
            CompCellsLeft = 20;
        }

        private void Clear()
        {
            updateStatusLabels();
        }

        private void updateStatusLabels()
        {// запоминание и выделение цветом всех уничтоженных кораблей
            ship4StatusLbl.Text = Ships4Left.ToString();
            ship3StatusLbl.Text = Ships3Left.ToString();
            ship2StatusLbl.Text = Ships2Left.ToString();
            ship1StatusLbl.Text = Ships1Left.ToString();
        }

        private void ClearAllShips()
        { // очистка игрового поля при начале новой игры
            Ships.Clear();
            ShipsDead.Clear();
            UserMisses.Clear();
            CompMisses.Clear();
            CompShips.Clear();
            CompShipsDead.Clear();
            UserShip4_1.Clear();
            UserShip3_1.Clear();
            UserShip3_2.Clear();
            UserShip2_1.Clear();
            UserShip2_2.Clear();
            UserShip2_3.Clear();
            UserShip1_1.Clear();
            UserShip1_2.Clear();
            UserShip1_3.Clear();
            UserShip1_4.Clear();
            CompShip4_1.Clear();
            CompShip3_1.Clear();
            CompShip3_2.Clear();
            CompShip2_1.Clear();
            CompShip2_2.Clear();
            CompShip1_1.Clear();
            CompShip1_2.Clear();
            CompShip1_3.Clear();
            CompShip1_4.Clear();
            UserShip4_1Obvodka.Clear();
            UserShip3_1Obvodka.Clear();
            UserShip3_2Obvodka.Clear();
            UserShip2_1Obvodka.Clear();
            UserShip2_2Obvodka.Clear();
            UserShip2_3Obvodka.Clear();
            UserShip1_1Obvodka.Clear();
            UserShip1_2Obvodka.Clear();
            UserShip1_3Obvodka.Clear();
            UserShip1_4Obvodka.Clear();
            CompShip4_1Obvodka.Clear();
            CompShip3_1Obvodka.Clear();
            CompShip3_2Obvodka.Clear();
            CompShip2_1Obvodka.Clear();
            CompShip2_2Obvodka.Clear();
            CompShip1_1Obvodka.Clear();
            CompShip1_2Obvodka.Clear();
            CompShip1_3Obvodka.Clear();
            CompShip1_4Obvodka.Clear();
            Queue.Clear();
            QueueWrong.Clear();
        }

        private void ClearRtb()
        { // очистка всех кораблей как элементов единой матрицы 
            for (int i = 1; i < 11; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Point cell = new Point(i, j);
                    CellUpdate(dataGridView1, cell, ShipClean);
                    CellUpdate(dataGridView2, cell, ShipClean);
                }
            }
        }

        private void ClearForBtnClear()
        {
            CanPut = true;
            ShipPaintingSbros();
            ShipsLeftSbros();
            ClearAllShips();
            ClearRtb();
        }

        private void ShipPaintingSbros()
        { // при очистке игрового поля все корабли переходят в статус не расставленных
            _ship4Painting = false;
            _ship3Painting = false;
            _ship2Painting = false;
            _ship1Painting = false;
        }

        private void TotalClearForNewGame()
        { // начальные настройки формы и активация выше перечисленных методов при начале игры с ИИ
            if (!LanGame)
                GamePreStart = false;
            UserShoots = false;
            ComputerShoots = false;
            Raspolojenie = "horizontal";
            CanPut = true;
            ShipsLeftSbros();
            ShipPaintingSbros();
            ClearAllShips();
            ClearRtb();

        }

        public void TotalClearForNewLan()
        {  // начальные настройки формы при начале игры по сети с другим игроком
            if (button2.InvokeRequired)
                button2.Invoke(new Action(delegate { button2.Enabled = false; }));
            else button2.Enabled = false;

            TotalClearForNewGame();
            if (richTextBox2.InvokeRequired)
                richTextBox2.Invoke(new Action(delegate { richTextBox2.Clear(); }));
            else richTextBox2.Clear();

            if (richTextBox1.InvokeRequired)
                richTextBox1.Invoke(new Action(delegate { richTextBox1.Clear(); }));
            else richTextBox1.Clear();
            LeftPanel("grbox");
            LanForm("wait");
            ProgramDetect.ServerReady = false;
            ProgramDetect.ClientReady = false;
            timer2.Enabled = true;
        }

        private void новаяИграСКомпьютеромToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            // диалоговые окна и вывод подсказки игроку по расстановке кораблей при начале игры с ИИ 
            DialogResult dialogResult = MessageBox.Show("Вы действительно хотите начать новую игру с компьютером?", "Новая игра с компьютером", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                TotalClearForNewGame();
                Chatter.UpdateGrouBoxText(groupBox1, "Осталось расположить кораблей:");
                Chatter.UpdateStatusLbl1(toolStripStatusLabel1, "Выберите способ расстановки кораблей");
                Chatter.GameWriteNotification(richTextBox2, "Вы начали новую игру с компьютером");
                Chatter.GameUpdateNotification(richTextBox2, "Расставьте корабли на вашем поле.\n" +
                                                            "Для этого нажмите на соответствующий " +
                                                            "корабль, что вы видите слева от этого окна, " +
                                                            "выберите его расположение - " +
                                                            "вертикально или горизонтально, после чего " +
                                                            "выберите клетку на поле с условием что эта клетка " +
                                                            "будет самая верхняя или же самая левая для выбранного корабля " +
                                                            "соответственно. Между кораблями должно оставаться расстояние в 1 клетку");
                updateStatusLabels();
                GamePreStart = true;
                ControlEnabler1(true);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (listBox1.SelectedIndex)
            {
                case 0:
                    Raspolojenie = "horizontal";
                    break;
                case 1:
                    Raspolojenie = "vertical";
                    break;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (GamePreStart && radioButton4.Checked == true)
            {
                _ship4Painting = true;
                _ship3Painting = false;
                _ship2Painting = false;
                _ship1Painting = false;
                radioButton3.Checked = false;
                radioButton2.Checked = false;
                radioButton1.Checked = false;
                Chatter.UpdateStatusLbl1(toolStripStatusLabel1,
                                         "Расположение четырехпалубного корабля: осталось кораблей: " + Ships4Left.ToString());
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (GamePreStart && radioButton3.Checked == true)
            {
                _ship4Painting = false;
                _ship3Painting = true;
                _ship2Painting = false;
                _ship1Painting = false;
                radioButton4.Checked = false;
                radioButton2.Checked = false;
                radioButton1.Checked = false;
                Chatter.UpdateStatusLbl1(toolStripStatusLabel1,
                                         "Расположение трехпалубного корабля: осталось кораблей: " + Ships3Left.ToString());
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (GamePreStart && radioButton2.Checked == true)
            {
                _ship4Painting = false;
                _ship3Painting = false;
                _ship2Painting = true;
                _ship1Painting = false;
                radioButton4.Checked = false;
                radioButton3.Checked = false;
                radioButton1.Checked = false;
                Chatter.UpdateStatusLbl1(toolStripStatusLabel1,
                                         "Расположение двопалубного корабля: осталось кораблей: " + Ships2Left.ToString());
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (GamePreStart && radioButton1.Checked)
            {
                _ship4Painting = false;
                _ship3Painting = false;
                _ship2Painting = false;
                _ship1Painting = true;
                radioButton4.Checked = false;
                radioButton3.Checked = false;
                radioButton2.Checked = false;
                Chatter.UpdateStatusLbl1(toolStripStatusLabel1,
                                         "Расположение однопалубного корабля: осталось кораблей: " + Ships1Left.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        { // кнопка начала игры
            GamePreStart = true;
            button2.Enabled = false;
            ClearForBtnClear();
        }

        private void chkBoxUser_CheckedChanged(object sender, EventArgs e)
        {   // выбор метода расположения кораблей вручную или рандомно
            if (chkBoxUser.Checked)
            {
                button1_Click(sender, e);
                ControlEnablerUser(true);
                ControlEnablerRandom(false);
                chkBoxRandom.Checked = false;
                Chatter.UpdateStatusLbl1(toolStripStatusLabel1, "Расположите корабли вручную");
            }
            else if (!chkBoxUser.Checked)
            {
                ControlEnablerUser(false);
                ControlEnablerRandom(true);
                chkBoxRandom.Checked = true;
                Chatter.UpdateStatusLbl1(toolStripStatusLabel1, "Случайное расположение кораблей");
            }
        }

        private void chkBoxRandom_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxRandom.Checked)
            {
                ControlEnablerRandom(true);
                ControlEnablerUser(false);
                chkBoxUser.Checked = false;
                Chatter.UpdateStatusLbl1(toolStripStatusLabel1, "Случайное расположение кораблей");
            }
            else if (!chkBoxRandom.Checked)
            {
                ControlEnablerRandom(false);
                chkBoxUser.Checked = true;
                Chatter.UpdateStatusLbl1(toolStripStatusLabel1, "Расположите корабли вручную");
            }
        }

        private void RandomShipsAdd(List<Point> listObj, string who)
        { // случайное расположение кораблей
            const int colIndMin = 1;
            const int colIndMax = 11;

            const int rowIndMin = 0;
            const int rowIndMax = 10;

            const int raspMin = 1;
            const int raspMax = 3;

            while (Ships4Left != 0)
            { // случайное расположение 4-палубного корабля
                CanPut = true;
                Random colInd = new Random(DateTime.Now.Millisecond - 136);
                Random rowInd = new Random(DateTime.Now.Millisecond - 2);
                Random rasp = new Random(DateTime.Now.Millisecond + 18);
                int columnIndex = colInd.Next(colIndMin, colIndMax);
                int rowIndex = rowInd.Next(rowIndMin, rowIndMax);
                int raspolojenie = rasp.Next(raspMin, raspMax);

                switch (raspolojenie)
                {  // вертикальное расположение
                    case 1: 
                        if (rowIndex >= 7)
                        {
                            CanPut = false;
                        }
                        else if (columnIndex > 1 && columnIndex < 10 && rowIndex > 0 && rowIndex < 6)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 5; j++)
                                {
                                    if (who == "computer")
                                    {
                                        CompShip4_1Obvodka.Add(new Point(i, j));
                                    }
                                    else if (who == "user")
                                    {
                                        UserShip4_1Obvodka.Add(new Point(i, j));
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (columnIndex > 1 && columnIndex < 10 && rowIndex == 0)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                            {
                                for (int j = rowIndex; j < rowIndex + 5; j++)
                                {
                                    if (who == "computer")
                                    {
                                        CompShip4_1Obvodka.Add(new Point(i, j));
                                    }
                                    else if (who == "user")
                                    {
                                        UserShip4_1Obvodka.Add(new Point(i, j));
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (columnIndex > 1 && columnIndex < 10 && rowIndex == 6)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 4; j++)
                                {
                                    if (who == "computer")
                                    {
                                        CompShip4_1Obvodka.Add(new Point(i, j));
                                    }
                                    else if (who == "user")
                                    {
                                        UserShip4_1Obvodka.Add(new Point(i, j));
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (columnIndex == 1 && rowIndex > 0 && rowIndex < 6)
                        {
                            for (int i = columnIndex; i < columnIndex + 2; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 5; j++)
                                {
                                    if (who == "computer")
                                    {
                                        CompShip4_1Obvodka.Add(new Point(i, j));
                                    }
                                    else if (who == "user")
                                    {
                                        UserShip4_1Obvodka.Add(new Point(i, j));
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (columnIndex == 1 && rowIndex == 6)
                        {
                            for (int i = columnIndex; i < columnIndex + 2; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 4; j++)
                                {
                                    if (who == "computer")
                                    {
                                        CompShip4_1Obvodka.Add(new Point(i, j));
                                    }
                                    else if (who == "user")
                                    {
                                        UserShip4_1Obvodka.Add(new Point(i, j));
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (columnIndex == 10 && rowIndex > 0 && rowIndex < 6)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 1; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 5; j++)
                                {
                                    if (who == "computer")
                                    {
                                        CompShip4_1Obvodka.Add(new Point(i, j));
                                    }
                                    else if (who == "user")
                                    {
                                        UserShip4_1Obvodka.Add(new Point(i, j));
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (columnIndex == 10 && rowIndex == 6)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 1; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 4; j++)
                                {
                                    if (who == "computer")
                                    {
                                        CompShip4_1Obvodka.Add(new Point(i, j));
                                    }
                                    else if (who == "user")
                                    {
                                        UserShip4_1Obvodka.Add(new Point(i, j));
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (columnIndex == 1 && rowIndex == 0)
                        {
                            for (int i = columnIndex; i < columnIndex + 2; i++)
                            {
                                for (int j = rowIndex; j < rowIndex + 5; j++)
                                {
                                    if (who == "computer")
                                    {
                                        CompShip4_1Obvodka.Add(new Point(i, j));
                                    }
                                    else if (who == "user")
                                    {
                                        UserShip4_1Obvodka.Add(new Point(i, j));
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (columnIndex == 10 && rowIndex == 0)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 1; i++)
                            {
                                for (int j = rowIndex; j < rowIndex + 5; j++)
                                {
                                    if (who == "computer")
                                    {
                                        CompShip4_1Obvodka.Add(new Point(i, j));
                                    }
                                    else if (who == "user")
                                    {
                                        UserShip4_1Obvodka.Add(new Point(i, j));
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        if (CanPut)
                        {
                            for (int i = rowIndex; i < rowIndex + 4; i++)
                            {
                                listObj.Add(new Point(columnIndex, i));

                                if (who == "computer")
                                {
                                    CompShip4_1.Add(new Point(columnIndex, i));
                                    CompShip4_1Obvodka.Remove(new Point(columnIndex, i));
                                }
                                else if (who == "user")
                                {
                                    UserShip4_1.Add(new Point(columnIndex, i));
                                    UserShip4_1Obvodka.Remove(new Point(columnIndex, i));
                                }

                            }
                            Ships4Left--;
                        }
                        else if (!CanPut)
                        {
                            if (who == "computer")
                            {
                                CompShip4_1Obvodka.Clear();
                            }
                            else if (who == "user")
                            {
                                UserShip4_1Obvodka.Clear();
                            }
                        }
                        break;

                    case 2: // горизонтальное расположение
                        if (columnIndex >= 8)
                        {
                            CanPut = false;
                        }
                        else if (rowIndex > 0 && rowIndex < 9 && columnIndex > 1 && columnIndex < 7)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 5; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 2; j++)
                                {
                                    if (who == "computer")
                                    {
                                        CompShip4_1Obvodka.Add(new Point(i, j));
                                    }
                                    else if (who == "user")
                                    {
                                        UserShip4_1Obvodka.Add(new Point(i, j));
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (rowIndex > 0 && rowIndex < 9 && columnIndex == 1)
                        {
                            for (int i = columnIndex; i < columnIndex + 5; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 2; j++)
                                {
                                    if (who == "computer")
                                    {
                                        CompShip4_1Obvodka.Add(new Point(i, j));
                                    }
                                    else if (who == "user")
                                    {
                                        UserShip4_1Obvodka.Add(new Point(i, j));
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (rowIndex > 0 && rowIndex < 9 && columnIndex == 7)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 4; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 2; j++)
                                {
                                    if (who == "computer")
                                    {
                                        CompShip4_1Obvodka.Add(new Point(i, j));
                                    }
                                    else if (who == "user")
                                    {
                                        UserShip4_1Obvodka.Add(new Point(i, j));
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (rowIndex == 9 && columnIndex > 1 && columnIndex < 7)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 5; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 1; j++)
                                {
                                    if (who == "computer")
                                    {
                                        CompShip4_1Obvodka.Add(new Point(i, j));
                                    }
                                    else if (who == "user")
                                    {
                                        UserShip4_1Obvodka.Add(new Point(i, j));
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (rowIndex == 9 && columnIndex == 7)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 4; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 1; j++)
                                {
                                    if (who == "computer")
                                    {
                                        CompShip4_1Obvodka.Add(new Point(i, j));
                                    }
                                    else if (who == "user")
                                    {
                                        UserShip4_1Obvodka.Add(new Point(i, j));
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (rowIndex == 0 && columnIndex > 1 && columnIndex < 7)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 5; i++)
                            {
                                for (int j = rowIndex; j < rowIndex + 2; j++)
                                {
                                    if (who == "computer")
                                    {
                                        CompShip4_1Obvodka.Add(new Point(i, j));
                                    }
                                    else if (who == "user")
                                    {
                                        UserShip4_1Obvodka.Add(new Point(i, j));
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (rowIndex == 0 && columnIndex == 7)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 4; i++)
                            {
                                for (int j = rowIndex; j < rowIndex + 2; j++)
                                {
                                    if (who == "computer")
                                    {
                                        CompShip4_1Obvodka.Add(new Point(i, j));
                                    }
                                    else if (who == "user")
                                    {
                                        UserShip4_1Obvodka.Add(new Point(i, j));
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (rowIndex == 9 && columnIndex == 1)
                        {
                            for (int i = columnIndex; i < columnIndex + 5; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 1; j++)
                                {
                                    if (who == "computer")
                                    {
                                        CompShip4_1Obvodka.Add(new Point(i, j));
                                    }
                                    else if (who == "user")
                                    {
                                        UserShip4_1Obvodka.Add(new Point(i, j));
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (rowIndex == 0 && columnIndex == 1)
                        {
                            for (int i = columnIndex; i < columnIndex + 5; i++)
                            {
                                for (int j = rowIndex; j < rowIndex + 2; j++)
                                {
                                    if (who == "computer")
                                    {
                                        CompShip4_1Obvodka.Add(new Point(i, j));
                                    }
                                    else if (who == "user")
                                    {
                                        UserShip4_1Obvodka.Add(new Point(i, j));
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        if (CanPut)
                        {
                            for (int i = columnIndex; i < columnIndex + 4; i++)
                            {
                                listObj.Add(new Point(i, rowIndex));
                                if (who == "computer")
                                {
                                    CompShip4_1.Add(new Point(i, rowIndex));
                                    CompShip4_1Obvodka.Remove(new Point(i, rowIndex));
                                }
                                else if (who == "user")
                                {
                                    UserShip4_1.Add(new Point(i, rowIndex));
                                    UserShip4_1Obvodka.Remove(new Point(i, rowIndex));
                                }
                            }
                            Ships4Left--;
                        }
                        else if (!CanPut)
                        {
                            if (who == "computer")
                            {
                                CompShip4_1Obvodka.Clear();
                            }
                            else if (who == "user")
                            {
                                UserShip4_1Obvodka.Clear();
                            }
                        }
                        break;
                }

            }

            while (Ships3Left != 0)
            { // случайное расположение 3-палубного корабля
                CanPut = true;
                Random colInd = new Random(DateTime.Now.Millisecond - 25);
                Random rowInd = new Random(DateTime.Now.Millisecond);
                Random rasp = new Random(DateTime.Now.Millisecond + 14);
                int columnIndex = colInd.Next(colIndMin, colIndMax);
                int rowIndex = rowInd.Next(rowIndMin, rowIndMax);
                int raspolojenie = rasp.Next(raspMin, raspMax);

                switch (raspolojenie)
                {
                    case 1:// вертикальное расположение
                        if (rowIndex >= 8)
                        {

                            CanPut = false;
                        }
                        else if (columnIndex > 1 && columnIndex < 10 && rowIndex > 0 && rowIndex < 7)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 4; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship3_1 = CompShip3_1.ToArray().Length;
                                        if (ship3_1 < 3)
                                        {
                                            CompShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            CompShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship3_1 = UserShip3_1.ToArray().Length;
                                        if (ship3_1 < 3)
                                        {
                                            UserShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            UserShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (columnIndex > 1 && columnIndex < 10 && rowIndex == 0)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                            {
                                for (int j = rowIndex; j < rowIndex + 4; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship3_1 = CompShip3_1.ToArray().Length;
                                        if (ship3_1 < 3)
                                        {
                                            CompShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            CompShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship3_1 = UserShip3_1.ToArray().Length;
                                        if (ship3_1 < 3)
                                        {
                                            UserShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            UserShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (columnIndex > 1 && columnIndex < 10 && rowIndex == 7)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 3; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship3_1 = CompShip3_1.ToArray().Length;
                                        if (ship3_1 < 3)
                                        {
                                            CompShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            CompShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship3_1 = UserShip3_1.ToArray().Length;
                                        if (ship3_1 < 3)
                                        {
                                            UserShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            UserShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (columnIndex == 1 && rowIndex > 0 && rowIndex < 7)
                        {
                            for (int i = columnIndex; i < columnIndex + 2; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 4; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship3_1 = CompShip3_1.ToArray().Length;
                                        if (ship3_1 < 3)
                                        {
                                            CompShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            CompShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship3_1 = UserShip3_1.ToArray().Length;
                                        if (ship3_1 < 3)
                                        {
                                            UserShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            UserShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (columnIndex == 1 && rowIndex == 7)
                        {
                            for (int i = columnIndex; i < columnIndex + 2; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 3; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship3_1 = CompShip3_1.ToArray().Length;
                                        if (ship3_1 < 3)
                                        {
                                            CompShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            CompShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship3_1 = UserShip3_1.ToArray().Length;
                                        if (ship3_1 < 3)
                                        {
                                            UserShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            UserShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (columnIndex == 10 && rowIndex > 0 && rowIndex < 7)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 1; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 4; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship3_1 = CompShip3_1.ToArray().Length;
                                        if (ship3_1 < 3)
                                        {
                                            CompShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            CompShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship3_1 = UserShip3_1.ToArray().Length;
                                        if (ship3_1 < 3)
                                        {
                                            UserShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            UserShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (columnIndex == 10 && rowIndex == 7)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 1; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 3; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship3_1 = CompShip3_1.ToArray().Length;
                                        if (ship3_1 < 3)
                                        {
                                            CompShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            CompShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship3_1 = UserShip3_1.ToArray().Length;
                                        if (ship3_1 < 3)
                                        {
                                            UserShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            UserShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (columnIndex == 1 && rowIndex == 0)
                        {
                            for (int i = columnIndex; i < columnIndex + 2; i++)
                            {
                                for (int j = rowIndex; j < rowIndex + 4; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship3_1 = CompShip3_1.ToArray().Length;
                                        if (ship3_1 < 3)
                                        {
                                            CompShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            CompShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship3_1 = UserShip3_1.ToArray().Length;
                                        if (ship3_1 < 3)
                                        {
                                            UserShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            UserShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (columnIndex == 10 && rowIndex == 0)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 1; i++)
                            {
                                for (int j = rowIndex; j < rowIndex + 4; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship3_1 = CompShip3_1.ToArray().Length;
                                        if (ship3_1 < 3)
                                        {
                                            CompShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            CompShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship3_1 = UserShip3_1.ToArray().Length;
                                        if (ship3_1 < 3)
                                        {
                                            UserShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            UserShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        if (CanPut)
                        {
                            for (int i = rowIndex; i < rowIndex + 3; i++)
                            {
                                listObj.Add(new Point(columnIndex, i));

                                if (who == "computer")
                                {
                                    int ship3_1 = CompShip3_1.ToArray().Length;
                                    if (ship3_1 < 3)
                                    {
                                        CompShip3_1.Add(new Point(columnIndex, i));
                                        CompShip3_1Obvodka.Remove(new Point(columnIndex, i));
                                    }
                                    else if (ship3_1 == 3)
                                    {
                                        CompShip3_2.Add(new Point(columnIndex, i));
                                        CompShip3_2Obvodka.Remove(new Point(columnIndex, i));
                                    }
                                }
                                else if (who == "user")
                                {
                                    int ship3_1 = UserShip3_1.ToArray().Length;
                                    if (ship3_1 < 3)
                                    {
                                        UserShip3_1.Add(new Point(columnIndex, i));
                                        UserShip3_1Obvodka.Remove(new Point(columnIndex, i));
                                    }
                                    else if (ship3_1 == 3)
                                    {
                                        UserShip3_2.Add(new Point(columnIndex, i));
                                        UserShip3_2Obvodka.Remove(new Point(columnIndex, i));
                                    }
                                }

                            }
                            Ships3Left--;
                        }
                        else if (!CanPut)
                        {
                            if (who == "computer")
                            {
                                int ship3_1 = CompShip3_1.ToArray().Length;
                                if (ship3_1 < 3)
                                {
                                    CompShip3_1Obvodka.Clear();
                                }
                                else if (ship3_1 == 3)
                                {
                                    CompShip3_2Obvodka.Clear();
                                }
                            }
                            else if (who == "user")
                            {
                                int ship3_1 = UserShip3_1.ToArray().Length;
                                if (ship3_1 < 3)
                                {
                                    UserShip3_1Obvodka.Clear();
                                }
                                else if (ship3_1 == 3)
                                {
                                    UserShip3_2Obvodka.Clear();
                                }
                            }
                        }
                        break;

                    case 2: // горизонтальное расположение
                        if (columnIndex >= 9)
                        {
                            CanPut = false;
                        }
                        else if (rowIndex > 0 && rowIndex < 9 && columnIndex > 1 && columnIndex < 8)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 4; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 2; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship3_1 = CompShip3_1.ToArray().Length;

                                        if (ship3_1 < 3)
                                        {
                                            CompShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            CompShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship3_1 = UserShip3_1.ToArray().Length;

                                        if (ship3_1 < 3)
                                        {
                                            UserShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            UserShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (rowIndex > 0 && rowIndex < 9 && columnIndex == 1)
                        {
                            for (int i = columnIndex; i < columnIndex + 4; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 2; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship3_1 = CompShip3_1.ToArray().Length;

                                        if (ship3_1 < 3)
                                        {
                                            CompShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            CompShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship3_1 = UserShip3_1.ToArray().Length;

                                        if (ship3_1 < 3)
                                        {
                                            UserShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            UserShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (rowIndex > 0 && rowIndex < 9 && columnIndex == 8)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 3; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 2; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship3_1 = CompShip3_1.ToArray().Length;

                                        if (ship3_1 < 3)
                                        {
                                            CompShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            CompShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship3_1 = UserShip3_1.ToArray().Length;

                                        if (ship3_1 < 3)
                                        {
                                            UserShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            UserShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (rowIndex == 9 && columnIndex > 1 && columnIndex < 8)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 4; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 1; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship3_1 = CompShip3_1.ToArray().Length;

                                        if (ship3_1 < 3)
                                        {
                                            CompShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            CompShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship3_1 = UserShip3_1.ToArray().Length;

                                        if (ship3_1 < 3)
                                        {
                                            UserShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            UserShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (rowIndex == 9 && columnIndex == 8)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 3; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 1; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship3_1 = CompShip3_1.ToArray().Length;

                                        if (ship3_1 < 3)
                                        {
                                            CompShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            CompShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship3_1 = UserShip3_1.ToArray().Length;

                                        if (ship3_1 < 3)
                                        {
                                            UserShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            UserShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (rowIndex == 0 && columnIndex > 1 && columnIndex < 8)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 4; i++)
                            {
                                for (int j = rowIndex; j < rowIndex + 2; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship3_1 = CompShip3_1.ToArray().Length;

                                        if (ship3_1 < 3)
                                        {
                                            CompShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            CompShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship3_1 = UserShip3_1.ToArray().Length;

                                        if (ship3_1 < 3)
                                        {
                                            UserShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            UserShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (rowIndex == 0 && columnIndex == 8)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 3; i++)
                            {
                                for (int j = rowIndex; j < rowIndex + 2; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship3_1 = CompShip3_1.ToArray().Length;

                                        if (ship3_1 < 3)
                                        {
                                            CompShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            CompShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship3_1 = UserShip3_1.ToArray().Length;

                                        if (ship3_1 < 3)
                                        {
                                            UserShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            UserShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (rowIndex == 9 && columnIndex == 1)
                        {
                            for (int i = columnIndex; i < columnIndex + 4; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 1; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship3_1 = CompShip3_1.ToArray().Length;

                                        if (ship3_1 < 3)
                                        {
                                            CompShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            CompShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship3_1 = UserShip3_1.ToArray().Length;

                                        if (ship3_1 < 3)
                                        {
                                            UserShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            UserShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (rowIndex == 0 && columnIndex == 1)
                        {
                            for (int i = columnIndex; i < columnIndex + 4; i++)
                            {
                                for (int j = rowIndex; j < rowIndex + 2; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship3_1 = CompShip3_1.ToArray().Length;

                                        if (ship3_1 < 3)
                                        {
                                            CompShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            CompShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship3_1 = UserShip3_1.ToArray().Length;

                                        if (ship3_1 < 3)
                                        {
                                            UserShip3_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship3_1 == 3)
                                        {
                                            UserShip3_2Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        if (CanPut)
                        {
                            for (int i = columnIndex; i < columnIndex + 3; i++)
                            {
                                listObj.Add(new Point(i, rowIndex));
                                if (who == "computer")
                                {
                                    int ship3_1 = CompShip3_1.ToArray().Length;

                                    if (ship3_1 < 3)
                                    {
                                        CompShip3_1.Add(new Point(i, rowIndex));
                                        CompShip3_1Obvodka.Remove(new Point(i, rowIndex));
                                    }
                                    else if (ship3_1 == 3)
                                    {
                                        CompShip3_2.Add(new Point(i, rowIndex));
                                        CompShip3_2Obvodka.Remove(new Point(i, rowIndex));
                                    }
                                }
                                else if (who == "user")
                                {
                                    int ship3_1 = UserShip3_1.ToArray().Length;

                                    if (ship3_1 < 3)
                                    {
                                        UserShip3_1.Add(new Point(i, rowIndex));
                                        UserShip3_1Obvodka.Remove(new Point(i, rowIndex));
                                    }
                                    else if (ship3_1 == 3)
                                    {
                                        UserShip3_2.Add(new Point(i, rowIndex));
                                        UserShip3_2Obvodka.Remove(new Point(i, rowIndex));
                                    }
                                }

                            }
                            Ships3Left--;
                        }
                        else if (!CanPut)
                        {
                            if (who == "computer")
                            {
                                int ship3_1 = CompShip3_1.ToArray().Length;

                                if (ship3_1 < 3)
                                {
                                    CompShip3_1Obvodka.Clear();
                                }
                                else if (ship3_1 == 3)
                                {
                                    CompShip3_2Obvodka.Clear();
                                }
                            }
                            else if (who == "user")
                            {
                                int ship3_1 = UserShip3_1.ToArray().Length;

                                if (ship3_1 < 3)
                                {
                                    UserShip3_1Obvodka.Clear();
                                }
                                else if (ship3_1 == 3)
                                {
                                    UserShip3_2Obvodka.Clear();
                                }
                            }
                        }
                        break;
                }
            }

            while (Ships2Left != 0)
            { // случайное расположение 2-палубного корабля
                CanPut = true;
                Random colInd = new Random(DateTime.Now.Millisecond - 60);
                Random rowInd = new Random(DateTime.Now.Millisecond + 2);
                Random rasp = new Random(DateTime.Now.Millisecond + 128);
                int columnIndex = colInd.Next(colIndMin, colIndMax);
                int rowIndex = rowInd.Next(rowIndMin, rowIndMax);
                int raspolojenie = rasp.Next(raspMin, raspMax);

                switch (raspolojenie)
                {
                    case 1: // вертикальное расположение
                        if (rowIndex == 9)
                        {
                            CanPut = false;
                        }
                        else if (columnIndex > 1 && columnIndex < 10 && rowIndex > 0 && rowIndex < 8)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 3; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship2_1 = CompShip2_1.ToArray().Length;
                                        int ship2_2 = CompShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            CompShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            CompShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            CompShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship2_1 = UserShip2_1.ToArray().Length;
                                        int ship2_2 = UserShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            UserShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            UserShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            UserShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (columnIndex > 1 && columnIndex < 10 && rowIndex == 0)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                            {
                                for (int j = rowIndex; j < rowIndex + 3; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship2_1 = CompShip2_1.ToArray().Length;
                                        int ship2_2 = CompShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            CompShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            CompShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            CompShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship2_1 = UserShip2_1.ToArray().Length;
                                        int ship2_2 = UserShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            UserShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            UserShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            UserShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (columnIndex > 1 && columnIndex < 10 && rowIndex == 8)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 2; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship2_1 = CompShip2_1.ToArray().Length;
                                        int ship2_2 = CompShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            CompShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            CompShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            CompShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship2_1 = UserShip2_1.ToArray().Length;
                                        int ship2_2 = UserShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            UserShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            UserShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            UserShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (columnIndex == 1 && rowIndex > 0 && rowIndex < 8)
                        {
                            for (int i = columnIndex; i < columnIndex + 2; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 3; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship2_1 = CompShip2_1.ToArray().Length;
                                        int ship2_2 = CompShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            CompShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            CompShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            CompShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship2_1 = UserShip2_1.ToArray().Length;
                                        int ship2_2 = UserShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            UserShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            UserShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            UserShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (columnIndex == 1 && rowIndex == 8)
                        {
                            for (int i = columnIndex; i < columnIndex + 2; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 2; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship2_1 = CompShip2_1.ToArray().Length;
                                        int ship2_2 = CompShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            CompShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            CompShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            CompShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship2_1 = UserShip2_1.ToArray().Length;
                                        int ship2_2 = UserShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            UserShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            UserShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            UserShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (columnIndex == 10 && rowIndex > 0 && rowIndex < 8)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 1; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 3; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship2_1 = CompShip2_1.ToArray().Length;
                                        int ship2_2 = CompShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            CompShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            CompShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            CompShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship2_1 = UserShip2_1.ToArray().Length;
                                        int ship2_2 = UserShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            UserShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            UserShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            UserShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (columnIndex == 10 && rowIndex == 8)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 1; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 2; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship2_1 = CompShip2_1.ToArray().Length;
                                        int ship2_2 = CompShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            CompShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            CompShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            CompShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship2_1 = UserShip2_1.ToArray().Length;
                                        int ship2_2 = UserShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            UserShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            UserShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            UserShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (columnIndex == 1 && rowIndex == 0)
                        {
                            for (int i = columnIndex; i < columnIndex + 2; i++)
                            {
                                for (int j = rowIndex; j < rowIndex + 3; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship2_1 = CompShip2_1.ToArray().Length;
                                        int ship2_2 = CompShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            CompShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            CompShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            CompShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship2_1 = UserShip2_1.ToArray().Length;
                                        int ship2_2 = UserShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            UserShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            UserShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            UserShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (columnIndex == 10 && rowIndex == 0)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 1; i++)
                            {
                                for (int j = rowIndex; j < rowIndex + 3; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship2_1 = CompShip2_1.ToArray().Length;
                                        int ship2_2 = CompShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            CompShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            CompShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            CompShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship2_1 = UserShip2_1.ToArray().Length;
                                        int ship2_2 = UserShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            UserShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            UserShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            UserShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        if (CanPut)
                        {
                            for (int i = rowIndex; i < rowIndex + 2; i++)
                            {
                                listObj.Add(new Point(columnIndex, i));
                                if (who == "computer")
                                {
                                    int ship2_1 = CompShip2_1.ToArray().Length;
                                    int ship2_2 = CompShip2_2.ToArray().Length;

                                    if (ship2_1 < 2)
                                    {
                                        CompShip2_1.Add(new Point(columnIndex, i));
                                        CompShip2_1Obvodka.Remove(new Point(columnIndex, i));
                                    }
                                    else if (ship2_1 == 2 && ship2_2 < 2)
                                    {
                                        CompShip2_2.Add(new Point(columnIndex, i));
                                        CompShip2_2Obvodka.Remove(new Point(columnIndex, i));
                                    }
                                    else if (ship2_1 == 2 && ship2_2 == 2)
                                    {
                                        CompShip2_3.Add(new Point(columnIndex, i));
                                        CompShip2_3Obvodka.Remove(new Point(columnIndex, i));
                                    }
                                }
                                else if (who == "user")
                                {
                                    int ship2_1 = UserShip2_1.ToArray().Length;
                                    int ship2_2 = UserShip2_2.ToArray().Length;

                                    if (ship2_1 < 2)
                                    {
                                        UserShip2_1.Add(new Point(columnIndex, i));
                                        UserShip2_1Obvodka.Remove(new Point(columnIndex, i));
                                    }
                                    else if (ship2_1 == 2 && ship2_2 < 2)
                                    {
                                        UserShip2_2.Add(new Point(columnIndex, i));
                                        UserShip2_2Obvodka.Remove(new Point(columnIndex, i));
                                    }
                                    else if (ship2_1 == 2 && ship2_2 == 2)
                                    {
                                        UserShip2_3.Add(new Point(columnIndex, i));
                                        UserShip2_3Obvodka.Remove(new Point(columnIndex, i));
                                    }
                                }

                            }
                            Ships2Left--;
                        }
                        else if (!CanPut)
                        {
                            if (who == "computer")
                            {
                                int ship2_1 = CompShip2_1.ToArray().Length;
                                int ship2_2 = CompShip2_2.ToArray().Length;

                                if (ship2_1 < 2)
                                {
                                    CompShip2_1Obvodka.Clear();
                                }
                                else if (ship2_1 == 2 && ship2_2 < 2)
                                {
                                    CompShip2_2Obvodka.Clear();
                                }
                                else if (ship2_1 == 2 && ship2_2 == 2)
                                {
                                    CompShip2_3Obvodka.Clear();
                                }
                            }
                            else if (who == "user")
                            {
                                int ship2_1 = UserShip2_1.ToArray().Length;
                                int ship2_2 = UserShip2_2.ToArray().Length;

                                if (ship2_1 < 2)
                                {
                                    UserShip2_1Obvodka.Clear();
                                }
                                else if (ship2_1 == 2 && ship2_2 < 2)
                                {
                                    UserShip2_2Obvodka.Clear();
                                }
                                else if (ship2_1 == 2 && ship2_2 == 2)
                                {
                                    UserShip2_3Obvodka.Clear();
                                }
                            }
                        }
                        break;

                    case 2: // горизонтальное расположение
                        if (columnIndex == 10)
                        {
                            CanPut = false;
                        }
                        else if (rowIndex > 0 && rowIndex < 9 && columnIndex > 1 && columnIndex < 9)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 3; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 2; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship2_1 = CompShip2_1.ToArray().Length;
                                        int ship2_2 = CompShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            CompShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            CompShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            CompShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship2_1 = UserShip2_1.ToArray().Length;
                                        int ship2_2 = UserShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            UserShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            UserShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            UserShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (rowIndex > 0 && rowIndex < 9 && columnIndex == 1)
                        {
                            for (int i = columnIndex; i < columnIndex + 3; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 2; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship2_1 = CompShip2_1.ToArray().Length;
                                        int ship2_2 = CompShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            CompShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            CompShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            CompShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship2_1 = UserShip2_1.ToArray().Length;
                                        int ship2_2 = UserShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            UserShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            UserShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            UserShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }

                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (rowIndex > 0 && rowIndex < 9 && columnIndex == 9)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 2; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship2_1 = CompShip2_1.ToArray().Length;
                                        int ship2_2 = CompShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            CompShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            CompShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            CompShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship2_1 = UserShip2_1.ToArray().Length;
                                        int ship2_2 = UserShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            UserShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            UserShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            UserShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (rowIndex == 9 && columnIndex > 1 && columnIndex < 9)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 3; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 1; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship2_1 = CompShip2_1.ToArray().Length;
                                        int ship2_2 = CompShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            CompShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            CompShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            CompShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship2_1 = UserShip2_1.ToArray().Length;
                                        int ship2_2 = UserShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            UserShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            UserShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            UserShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (rowIndex == 9 && columnIndex == 9)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 1; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship2_1 = CompShip2_1.ToArray().Length;
                                        int ship2_2 = CompShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            CompShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            CompShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            CompShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship2_1 = UserShip2_1.ToArray().Length;
                                        int ship2_2 = UserShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            UserShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            UserShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            UserShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (rowIndex == 0 && columnIndex > 1 && columnIndex < 9)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 3; i++)
                            {
                                for (int j = rowIndex; j < rowIndex + 2; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship2_1 = CompShip2_1.ToArray().Length;
                                        int ship2_2 = CompShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            CompShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            CompShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            CompShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship2_1 = UserShip2_1.ToArray().Length;
                                        int ship2_2 = UserShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            UserShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            UserShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            UserShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (rowIndex == 0 && columnIndex == 9)
                        {
                            for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                            {
                                for (int j = rowIndex; j < rowIndex + 2; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship2_1 = CompShip2_1.ToArray().Length;
                                        int ship2_2 = CompShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            CompShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            CompShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            CompShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship2_1 = UserShip2_1.ToArray().Length;
                                        int ship2_2 = UserShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            UserShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            UserShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            UserShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (rowIndex == 9 && columnIndex == 1)
                        {
                            for (int i = columnIndex; i < columnIndex + 3; i++)
                            {
                                for (int j = rowIndex - 1; j < rowIndex + 1; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship2_1 = CompShip2_1.ToArray().Length;
                                        int ship2_2 = CompShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            CompShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            CompShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            CompShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship2_1 = UserShip2_1.ToArray().Length;
                                        int ship2_2 = UserShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            UserShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            UserShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            UserShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        else if (rowIndex == 0 && columnIndex == 1)
                        {
                            for (int i = columnIndex; i < columnIndex + 3; i++)
                            {
                                for (int j = rowIndex; j < rowIndex + 2; j++)
                                {
                                    if (who == "computer")
                                    {
                                        int ship2_1 = CompShip2_1.ToArray().Length;
                                        int ship2_2 = CompShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            CompShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            CompShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            CompShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    else if (who == "user")
                                    {
                                        int ship2_1 = UserShip2_1.ToArray().Length;
                                        int ship2_2 = UserShip2_2.ToArray().Length;

                                        if (ship2_1 < 2)
                                        {
                                            UserShip2_1Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 < 2)
                                        {
                                            UserShip2_2Obvodka.Add(new Point(i, j));
                                        }
                                        else if (ship2_1 == 2 && ship2_2 == 2)
                                        {
                                            UserShip2_3Obvodka.Add(new Point(i, j));
                                        }
                                    }
                                    if (listObj.Contains(new Point(i, j)))
                                    {
                                        CanPut = false;
                                    }
                                }
                            }
                        }
                        if (CanPut)
                        {
                            for (int i = columnIndex; i < columnIndex + 2; i++)
                            {
                                listObj.Add(new Point(i, rowIndex));
                                if (who == "computer")
                                {
                                    int ship2_1 = CompShip2_1.ToArray().Length;
                                    int ship2_2 = CompShip2_2.ToArray().Length;

                                    if (ship2_1 < 2)
                                    {
                                        CompShip2_1.Add(new Point(i, rowIndex));
                                        CompShip2_1Obvodka.Remove(new Point(i, rowIndex));
                                    }
                                    else if (ship2_1 == 2 && ship2_2 < 2)
                                    {
                                        CompShip2_2.Add(new Point(i, rowIndex));
                                        CompShip2_2Obvodka.Remove(new Point(i, rowIndex));
                                    }
                                    else if (ship2_1 == 2 && ship2_2 == 2)
                                    {
                                        CompShip2_3.Add(new Point(i, rowIndex));
                                        CompShip2_3Obvodka.Remove(new Point(i, rowIndex));
                                    }
                                }
                                else if (who == "user")
                                {
                                    int ship2_1 = UserShip2_1.ToArray().Length;
                                    int ship2_2 = UserShip2_2.ToArray().Length;

                                    if (ship2_1 < 2)
                                    {
                                        UserShip2_1.Add(new Point(i, rowIndex));
                                        UserShip2_1Obvodka.Remove(new Point(i, rowIndex));
                                    }
                                    else if (ship2_1 == 2 && ship2_2 < 2)
                                    {
                                        UserShip2_2.Add(new Point(i, rowIndex));
                                        UserShip2_2Obvodka.Remove(new Point(i, rowIndex));
                                    }
                                    else if (ship2_1 == 2 && ship2_2 == 2)
                                    {
                                        UserShip2_3.Add(new Point(i, rowIndex));
                                        UserShip2_3Obvodka.Remove(new Point(i, rowIndex));
                                    }
                                }

                            }
                            Ships2Left--;
                        }
                        else if (!CanPut)
                        {
                            if (who == "computer")
                            {
                                int ship2_1 = CompShip2_1.ToArray().Length;
                                int ship2_2 = CompShip2_2.ToArray().Length;

                                if (ship2_1 < 2)
                                {
                                    CompShip2_1Obvodka.Clear();
                                }
                                else if (ship2_1 == 2 && ship2_2 < 2)
                                {
                                    CompShip2_2Obvodka.Clear();
                                }
                                else if (ship2_1 == 2 && ship2_2 == 2)
                                {
                                    CompShip2_3Obvodka.Clear();
                                }
                            }
                            else if (who == "user")
                            {
                                int ship2_1 = UserShip2_1.ToArray().Length;
                                int ship2_2 = UserShip2_2.ToArray().Length;

                                if (ship2_1 < 2)
                                {
                                    UserShip2_1Obvodka.Clear();
                                }
                                else if (ship2_1 == 2 && ship2_2 < 2)
                                {
                                    UserShip2_2Obvodka.Clear();
                                }
                                else if (ship2_1 == 2 && ship2_2 == 2)
                                {
                                    UserShip2_3Obvodka.Clear();
                                }
                            }
                        }
                        break;
                }
            }

            while (Ships1Left != 0)
            { // случайное расположение 1-палубного корабля
                CanPut = true;
                Random colInd = new Random(DateTime.Now.Millisecond - 15);
                Random rowInd = new Random(DateTime.Now.Millisecond + 20);

                int columnIndex = colInd.Next(colIndMin, colIndMax);
                int rowIndex = rowInd.Next(rowIndMin, rowIndMax);

                if (columnIndex > 1 && columnIndex < 10 && rowIndex > 0 && rowIndex < 9)
                {
                    for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                    {
                        for (int j = rowIndex - 1; j < rowIndex + 2; j++)
                        {
                            if (who == "computer")
                            {
                                int ship1_1 = CompShip1_1.ToArray().Length;
                                int ship1_2 = CompShip1_2.ToArray().Length;
                                int ship1_3 = CompShip1_3.ToArray().Length;

                                if (ship1_1 == 0)
                                {
                                    CompShip1_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 0)
                                {
                                    CompShip1_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                                {
                                    CompShip1_3Obvodka.Add(new Point(i, j));
                                }
                                else
                                {
                                    CompShip1_4Obvodka.Add(new Point(i, j));
                                }
                            }
                            else if (who == "user")
                            {
                                int ship1_1 = UserShip1_1.ToArray().Length;
                                int ship1_2 = UserShip1_2.ToArray().Length;
                                int ship1_3 = UserShip1_3.ToArray().Length;

                                if (ship1_1 == 0)
                                {
                                    UserShip1_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 0)
                                {
                                    UserShip1_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                                {
                                    UserShip1_3Obvodka.Add(new Point(i, j));
                                }
                                else
                                {
                                    UserShip1_4Obvodka.Add(new Point(i, j));
                                }
                            }

                            if (listObj.Contains(new Point(i, j)))
                            {
                                CanPut = false;
                            }
                        }
                    }
                }
                else if (columnIndex == 1 && rowIndex > 0 && rowIndex < 9)
                {
                    for (int i = columnIndex; i < columnIndex + 2; i++)
                    {
                        for (int j = rowIndex - 1; j < rowIndex + 2; j++)
                        {
                            if (who == "computer")
                            {
                                int ship1_1 = CompShip1_1.ToArray().Length;
                                int ship1_2 = CompShip1_2.ToArray().Length;
                                int ship1_3 = CompShip1_3.ToArray().Length;

                                if (ship1_1 == 0)
                                {
                                    CompShip1_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 0)
                                {
                                    CompShip1_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                                {
                                    CompShip1_3Obvodka.Add(new Point(i, j));
                                }
                                else
                                {
                                    CompShip1_4Obvodka.Add(new Point(i, j));
                                }
                            }
                            else if (who == "user")
                            {
                                int ship1_1 = UserShip1_1.ToArray().Length;
                                int ship1_2 = UserShip1_2.ToArray().Length;
                                int ship1_3 = UserShip1_3.ToArray().Length;

                                if (ship1_1 == 0)
                                {
                                    UserShip1_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 0)
                                {
                                    UserShip1_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                                {
                                    UserShip1_3Obvodka.Add(new Point(i, j));
                                }
                                else
                                {
                                    UserShip1_4Obvodka.Add(new Point(i, j));
                                }
                            }

                            if (listObj.Contains(new Point(i, j)))
                            {
                                CanPut = false;
                            }
                        }
                    }
                }
                else if (columnIndex == 10 && rowIndex > 0 && rowIndex < 9)
                {
                    for (int i = columnIndex - 1; i < columnIndex + 1; i++)
                    {
                        for (int j = rowIndex - 1; j < rowIndex + 2; j++)
                        {
                            if (who == "computer")
                            {
                                int ship1_1 = CompShip1_1.ToArray().Length;
                                int ship1_2 = CompShip1_2.ToArray().Length;
                                int ship1_3 = CompShip1_3.ToArray().Length;

                                if (ship1_1 == 0)
                                {
                                    CompShip1_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 0)
                                {
                                    CompShip1_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                                {
                                    CompShip1_3Obvodka.Add(new Point(i, j));
                                }
                                else
                                {
                                    CompShip1_4Obvodka.Add(new Point(i, j));
                                }
                            }
                            else if (who == "user")
                            {
                                int ship1_1 = UserShip1_1.ToArray().Length;
                                int ship1_2 = UserShip1_2.ToArray().Length;
                                int ship1_3 = UserShip1_3.ToArray().Length;

                                if (ship1_1 == 0)
                                {
                                    UserShip1_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 0)
                                {
                                    UserShip1_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                                {
                                    UserShip1_3Obvodka.Add(new Point(i, j));
                                }
                                else
                                {
                                    UserShip1_4Obvodka.Add(new Point(i, j));
                                }
                            }

                            if (listObj.Contains(new Point(i, j)))
                            {
                                CanPut = false;
                            }
                        }
                    }
                }
                else if (rowIndex == 0 && columnIndex > 1 && columnIndex < 10)
                {
                    for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                    {
                        for (int j = rowIndex; j < rowIndex + 2; j++)
                        {
                            if (who == "computer")
                            {
                                int ship1_1 = CompShip1_1.ToArray().Length;
                                int ship1_2 = CompShip1_2.ToArray().Length;
                                int ship1_3 = CompShip1_3.ToArray().Length;

                                if (ship1_1 == 0)
                                {
                                    CompShip1_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 0)
                                {
                                    CompShip1_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                                {
                                    CompShip1_3Obvodka.Add(new Point(i, j));
                                }
                                else
                                {
                                    CompShip1_4Obvodka.Add(new Point(i, j));
                                }
                            }
                            else if (who == "user")
                            {
                                int ship1_1 = UserShip1_1.ToArray().Length;
                                int ship1_2 = UserShip1_2.ToArray().Length;
                                int ship1_3 = UserShip1_3.ToArray().Length;

                                if (ship1_1 == 0)
                                {
                                    UserShip1_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 0)
                                {
                                    UserShip1_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                                {
                                    UserShip1_3Obvodka.Add(new Point(i, j));
                                }
                                else
                                {
                                    UserShip1_4Obvodka.Add(new Point(i, j));
                                }
                            }

                            if (listObj.Contains(new Point(i, j)))
                            {
                                CanPut = false;
                            }
                        }
                    }
                }
                else if (rowIndex == 9 && columnIndex > 1 && columnIndex < 10)
                {
                    for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                    {
                        for (int j = rowIndex - 1; j < rowIndex + 1; j++)
                        {
                            if (who == "computer")
                            {
                                int ship1_1 = CompShip1_1.ToArray().Length;
                                int ship1_2 = CompShip1_2.ToArray().Length;
                                int ship1_3 = CompShip1_3.ToArray().Length;

                                if (ship1_1 == 0)
                                {
                                    CompShip1_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 0)
                                {
                                    CompShip1_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                                {
                                    CompShip1_3Obvodka.Add(new Point(i, j));
                                }
                                else
                                {
                                    CompShip1_4Obvodka.Add(new Point(i, j));
                                }
                            }
                            else if (who == "user")
                            {
                                int ship1_1 = UserShip1_1.ToArray().Length;
                                int ship1_2 = UserShip1_2.ToArray().Length;
                                int ship1_3 = UserShip1_3.ToArray().Length;

                                if (ship1_1 == 0)
                                {
                                    UserShip1_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 0)
                                {
                                    UserShip1_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                                {
                                    UserShip1_3Obvodka.Add(new Point(i, j));
                                }
                                else
                                {
                                    UserShip1_4Obvodka.Add(new Point(i, j));
                                }
                            }

                            if (listObj.Contains(new Point(i, j)))
                            {
                                CanPut = false;
                            }
                        }
                    }
                }
                else if (rowIndex == 0 && columnIndex == 1)
                {
                    for (int i = columnIndex; i < columnIndex + 2; i++)
                    {
                        for (int j = rowIndex; j < rowIndex + 2; j++)
                        {
                            if (who == "computer")
                            {
                                int ship1_1 = CompShip1_1.ToArray().Length;
                                int ship1_2 = CompShip1_2.ToArray().Length;
                                int ship1_3 = CompShip1_3.ToArray().Length;

                                if (ship1_1 == 0)
                                {
                                    CompShip1_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 0)
                                {
                                    CompShip1_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                                {
                                    CompShip1_3Obvodka.Add(new Point(i, j));
                                }
                                else
                                {
                                    CompShip1_4Obvodka.Add(new Point(i, j));
                                }
                            }
                            else if (who == "user")
                            {
                                int ship1_1 = UserShip1_1.ToArray().Length;
                                int ship1_2 = UserShip1_2.ToArray().Length;
                                int ship1_3 = UserShip1_3.ToArray().Length;

                                if (ship1_1 == 0)
                                {
                                    UserShip1_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 0)
                                {
                                    UserShip1_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                                {
                                    UserShip1_3Obvodka.Add(new Point(i, j));
                                }
                                else
                                {
                                    UserShip1_4Obvodka.Add(new Point(i, j));
                                }
                            }

                            if (listObj.Contains(new Point(i, j)))
                            {
                                CanPut = false;
                            }
                        }
                    }
                }
                else if (rowIndex == 0 && columnIndex == 10)
                {
                    for (int i = columnIndex - 1; i < columnIndex + 1; i++)
                    {
                        for (int j = rowIndex; j < rowIndex + 2; j++)
                        {
                            if (who == "computer")
                            {
                                int ship1_1 = CompShip1_1.ToArray().Length;
                                int ship1_2 = CompShip1_2.ToArray().Length;
                                int ship1_3 = CompShip1_3.ToArray().Length;

                                if (ship1_1 == 0)
                                {
                                    CompShip1_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 0)
                                {
                                    CompShip1_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                                {
                                    CompShip1_3Obvodka.Add(new Point(i, j));
                                }
                                else
                                {
                                    CompShip1_4Obvodka.Add(new Point(i, j));
                                }
                            }
                            else if (who == "user")
                            {
                                int ship1_1 = UserShip1_1.ToArray().Length;
                                int ship1_2 = UserShip1_2.ToArray().Length;
                                int ship1_3 = UserShip1_3.ToArray().Length;

                                if (ship1_1 == 0)
                                {
                                    UserShip1_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 0)
                                {
                                    UserShip1_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                                {
                                    UserShip1_3Obvodka.Add(new Point(i, j));
                                }
                                else
                                {
                                    UserShip1_4Obvodka.Add(new Point(i, j));
                                }
                            }

                            if (listObj.Contains(new Point(i, j)))
                            {
                                CanPut = false;
                            }
                        }
                    }
                }
                else if (rowIndex == 9 && columnIndex == 1)
                {
                    for (int i = columnIndex - 1; i < columnIndex + 2; i++)
                    {
                        for (int j = rowIndex - 1; j < rowIndex + 1; j++)
                        {
                            if (who == "computer")
                            {
                                int ship1_1 = CompShip1_1.ToArray().Length;
                                int ship1_2 = CompShip1_2.ToArray().Length;
                                int ship1_3 = CompShip1_3.ToArray().Length;

                                if (ship1_1 == 0)
                                {
                                    CompShip1_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 0)
                                {
                                    CompShip1_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                                {
                                    CompShip1_3Obvodka.Add(new Point(i, j));
                                }
                                else
                                {
                                    CompShip1_4Obvodka.Add(new Point(i, j));
                                }
                            }
                            else if (who == "user")
                            {
                                int ship1_1 = UserShip1_1.ToArray().Length;
                                int ship1_2 = UserShip1_2.ToArray().Length;
                                int ship1_3 = UserShip1_3.ToArray().Length;

                                if (ship1_1 == 0)
                                {
                                    UserShip1_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 0)
                                {
                                    UserShip1_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                                {
                                    UserShip1_3Obvodka.Add(new Point(i, j));
                                }
                                else
                                {
                                    UserShip1_4Obvodka.Add(new Point(i, j));
                                }
                            }

                            if (listObj.Contains(new Point(i, j)))
                            {
                                CanPut = false;
                            }
                        }
                    }
                }
                else if (rowIndex == 9 && columnIndex == 10)
                {
                    for (int i = columnIndex - 1; i < columnIndex + 1; i++)
                    {
                        for (int j = rowIndex - 1; j < rowIndex + 1; j++)
                        {
                            if (who == "computer")
                            {
                                int ship1_1 = CompShip1_1.ToArray().Length;
                                int ship1_2 = CompShip1_2.ToArray().Length;
                                int ship1_3 = CompShip1_3.ToArray().Length;

                                if (ship1_1 == 0)
                                {
                                    CompShip1_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 0)
                                {
                                    CompShip1_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                                {
                                    CompShip1_3Obvodka.Add(new Point(i, j));
                                }
                                else
                                {
                                    CompShip1_4Obvodka.Add(new Point(i, j));
                                }
                            }
                            else if (who == "user")
                            {
                                int ship1_1 = UserShip1_1.ToArray().Length;
                                int ship1_2 = UserShip1_2.ToArray().Length;
                                int ship1_3 = UserShip1_3.ToArray().Length;

                                if (ship1_1 == 0)
                                {
                                    UserShip1_1Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 0)
                                {
                                    UserShip1_2Obvodka.Add(new Point(i, j));
                                }
                                else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                                {
                                    UserShip1_3Obvodka.Add(new Point(i, j));
                                }
                                else
                                {
                                    UserShip1_4Obvodka.Add(new Point(i, j));
                                }
                            }

                            if (listObj.Contains(new Point(i, j)))
                            {
                                CanPut = false;
                            }
                        }
                    }
                }
                if (CanPut)
                {
                    listObj.Add(new Point(columnIndex, rowIndex));
                    if (who == "computer")
                    {
                        int ship1_1 = CompShip1_1.ToArray().Length;
                        int ship1_2 = CompShip1_2.ToArray().Length;
                        int ship1_3 = CompShip1_3.ToArray().Length;

                        if (ship1_1 == 0)
                        {
                            CompShip1_1.Add(new Point(columnIndex, rowIndex));
                            CompShip1_1Obvodka.Remove(new Point(columnIndex, rowIndex));
                        }
                        else if (ship1_1 == 1 && ship1_2 == 0)
                        {
                            CompShip1_2.Add(new Point(columnIndex, rowIndex));
                            CompShip1_2Obvodka.Remove(new Point(columnIndex, rowIndex));
                        }
                        else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                        {
                            CompShip1_3.Add(new Point(columnIndex, rowIndex));
                            CompShip1_3Obvodka.Remove(new Point(columnIndex, rowIndex));
                        }
                        else
                        {
                            CompShip1_4.Add(new Point(columnIndex, rowIndex));
                            CompShip1_4Obvodka.Remove(new Point(columnIndex, rowIndex));
                        }
                    }
                    else if (who == "user")
                    {
                        int ship1_1 = UserShip1_1.ToArray().Length;
                        int ship1_2 = UserShip1_2.ToArray().Length;
                        int ship1_3 = UserShip1_3.ToArray().Length;

                        if (ship1_1 == 0)
                        {
                            UserShip1_1.Add(new Point(columnIndex, rowIndex));
                            UserShip1_1Obvodka.Remove(new Point(columnIndex, rowIndex));
                        }
                        else if (ship1_1 == 1 && ship1_2 == 0)
                        {
                            UserShip1_2.Add(new Point(columnIndex, rowIndex));
                            UserShip1_2Obvodka.Remove(new Point(columnIndex, rowIndex));
                        }
                        else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                        {
                            UserShip1_3.Add(new Point(columnIndex, rowIndex));
                            UserShip1_3Obvodka.Remove(new Point(columnIndex, rowIndex));
                        }
                        else
                        {
                            UserShip1_4.Add(new Point(columnIndex, rowIndex));
                            UserShip1_4Obvodka.Remove(new Point(columnIndex, rowIndex));
                        }
                    }

                    Ships1Left--;
                }
                else if (!CanPut)
                {
                    if (who == "computer")
                    {
                        int ship1_1 = CompShip1_1.ToArray().Length;
                        int ship1_2 = CompShip1_2.ToArray().Length;
                        int ship1_3 = CompShip1_3.ToArray().Length;

                        if (ship1_1 == 0)
                        {
                            CompShip1_1Obvodka.Clear();
                        }
                        else if (ship1_1 == 1 && ship1_2 == 0)
                        {
                            CompShip1_2Obvodka.Clear();
                        }
                        else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                        {
                            CompShip1_3Obvodka.Clear();
                        }
                        else
                        {
                            CompShip1_4Obvodka.Clear();
                        }
                    }
                    else if (who == "user")
                    {
                        int ship1_1 = UserShip1_1.ToArray().Length;
                        int ship1_2 = UserShip1_2.ToArray().Length;
                        int ship1_3 = UserShip1_3.ToArray().Length;

                        if (ship1_1 == 0)
                        {
                            UserShip1_1Obvodka.Clear();
                        }
                        else if (ship1_1 == 1 && ship1_2 == 0)
                        {
                            UserShip1_2Obvodka.Clear();
                        }
                        else if (ship1_1 == 1 && ship1_2 == 1 && ship1_3 == 0)
                        {
                            UserShip1_3Obvodka.Clear();
                        }
                        else
                        {
                            UserShip1_4Obvodka.Clear();
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        { // случайное расположение кораблей для игрока при нажатии на кнопку
            this.Cursor = Cursors.WaitCursor;
            TotalClearForNewGame();
            button3.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            Clear();
            Ships.Clear();
            Chatter.UpdateStatusLbl1(toolStripStatusLabel1, "Генерация случайного расположения");
            RandomShipsAdd(Ships, "user");

            foreach (var point in Ships)
            { // подтверждение игроком расстановки кораблей при ручной и рандомной расстановке 
                dataGridView1[point.X, point.Y].Value = new Bitmap(ShipCell);
            }
            button3.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = true;
            Chatter.UpdateStatusLbl1(toolStripStatusLabel1, "Корабли расставлены");
            this.Cursor = Cursors.Default;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!LanGame)
            { // при одиночной игре игрок ходит первым
                Cursor = Cursors.WaitCursor;
                ShipsLeftSbros();
                RandomShipsAdd(CompShips, "computer");

                dataGridView2.Enabled = true;
                UserShoots = true;
                groupBox1.Enabled = false;
                Chatter.GameUpdateNotification(richTextBox1, "Игра началась, игрок стреляет первым");
                Chatter.UpdateStatusLbl1(toolStripStatusLabel1, "Ход игрока");
                Cursor = Cursors.Default;
            }
            else if (LanGame)
            { // при сетевой игре первым ходит игрок в роли сервера
                LeftPanel("panel");
                if (ProgramDetect.Running == "server")
                {
                    ProgramDetect.ServerReady = true;
                    LanForm("server done");
                    Server.SelfRef.SendData("#Cserver done");
                }
                else if (ProgramDetect.Running == "client")
                {
                    ProgramDetect.ClientReady = true;
                    LanForm("client done");
                    Client.SelfRef.SendMessage("#Cclient done");
                }
                MakeAllUserShipsList();
                timer2.Enabled = true;
            }

        }

        private void LabelInvoke(Label label, string text)
        { // константация фактов о ходе
            if (label.InvokeRequired)
            {
                label.Invoke(new Action(delegate
                {
                    label.Text = text;
                }));
            }
            else
            {
                label.Text = text;
            }
        }

        private void PrBarAnimation(bool enabled)
        { // сопровождение выстрелов характерными звуками
            if (enabled && !progressBar1.Visible)
            {
                if (progressBar1.InvokeRequired)
                {
                    progressBar1.Invoke(new Action(delegate
                    {
                        progressBar1.Visible = true;
                        progressBar1.Style = ProgressBarStyle.Marquee;
                        progressBar1.MarqueeAnimationSpeed = 30;
                    }));
                }
                else
                {
                    progressBar1.Visible = true;
                    progressBar1.Style = ProgressBarStyle.Marquee;
                    progressBar1.MarqueeAnimationSpeed = 30;
                }

                if (label2.InvokeRequired)
                {
                    label2.Invoke(new Action(delegate
                    {
                        label2.Visible = true;
                    }));
                }
                else label2.Visible = true;
            }
            else if (!enabled)
            {
                if (progressBar1.InvokeRequired)
                {
                    progressBar1.Invoke(new Action(delegate
                    {
                        progressBar1.Style = ProgressBarStyle.Continuous;
                        progressBar1.MarqueeAnimationSpeed = 0;
                        progressBar1.Visible = false;
                    }));
                }
                else
                {
                    progressBar1.Style = ProgressBarStyle.Continuous;
                    progressBar1.MarqueeAnimationSpeed = 0;
                    progressBar1.Visible = false;
                }
                if (label2.InvokeRequired)
                {
                    label2.Invoke(new Action(delegate
                    {
                        label2.Visible = false;
                    }));
                }
                else label2.Visible = false;
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        { // осуществление выстрела при нажатии на ячейку игрового поля
            Point shoot = new Point();
            shoot.X = dataGridView2.CurrentCell.ColumnIndex;
            shoot.Y = dataGridView2.CurrentCell.RowIndex;
            if (!LanGame)
                PlayerShoot(shoot);
            else if (LanGame)
                PlayerShootLan(shoot);
        }

        public void CellUpdate(DataGridView dataObj, Point point, string bitmap)
        { // обновление данных об игровом поле
            if (dataObj.InvokeRequired)
            {
                dataObj.Invoke(new Action(delegate { dataObj[point.X, point.Y].Value = new Bitmap(bitmap); }));
            }
            else dataObj[point.X, point.Y].Value = new Bitmap(bitmap);
        }

        private Point SmartComputer()
        { // вывод был ли корабль подбит, уничтожен или нет
            Point shoot = new Point();
            Random rnd = new Random(DateTime.Now.Millisecond);
            int xxl = rnd.Next(2);
            if (QueueWrong.Count != 0)
            {
                switch (xxl)
                {
                    case 0:
                        shoot = QueueWrong.First();
                        QueueWrong.Remove(shoot);
                        break;
                    case 1:
                        shoot = Queue.First();
                        QueueWrong.Clear();
                        break;
                }
            }
            else if (QueueWrong.Count == 0)
            {
                shoot = Queue.First();
            }
            return shoot;
        }

        private void AddQueue(Point point)
        { // стрельба по игроку злым восставшим ИИ старается найти самый большой корабль
            if (!UserShip1_1.Contains(point) || !UserShip1_2.Contains(point) || !UserShip1_3.Contains(point) || !UserShip1_4.Contains(point))
            {
                if (point.X == 1 && point.Y == 0)
                {
                    QueueWrong.Add(new Point(2, 0));
                    QueueWrong.Add(new Point(1, 1));
                }
                else if (point.X == 10 && point.Y == 0)
                {
                    QueueWrong.Add(new Point(9, 0));
                    QueueWrong.Add(new Point(10, 1));
                }
                else if (point.X == 1 && point.Y == 9)
                {
                    QueueWrong.Add(new Point(1, 8));
                    QueueWrong.Add(new Point(2, 9));
                }
                else if (point.X == 10 && point.Y == 9)
                {
                    QueueWrong.Add(new Point(10, 8));
                    QueueWrong.Add(new Point(9, 9));
                }
                else if (point.X == 1 && point.Y > 0 && point.Y < 9)
                {
                    QueueWrong.Add(new Point(point.X, point.Y + 1));
                    QueueWrong.Add(new Point(point.X, point.Y - 1));
                    QueueWrong.Add(new Point(point.X + 1, point.Y));
                }
                else if (point.Y == 0 && point.X > 1 && point.X < 10)
                {
                    QueueWrong.Add(new Point(point.X - 1, point.Y));
                    QueueWrong.Add(new Point(point.X + 1, point.Y));
                    QueueWrong.Add(new Point(point.X, point.Y + 1));
                }
                else if (point.X == 10 && point.Y > 0 && point.Y < 9)
                {
                    QueueWrong.Add(new Point(point.X, point.Y + 1));
                    QueueWrong.Add(new Point(point.X, point.Y - 1));
                    QueueWrong.Add(new Point(point.X - 1, point.Y));
                }
                else if (point.Y == 9 && point.X > 1 && point.X < 10)
                {
                    QueueWrong.Add(new Point(point.X + 1, point.Y));
                    QueueWrong.Add(new Point(point.X - 1, point.Y));
                    QueueWrong.Add(new Point(point.X, point.Y - 1));
                }
                else if (point.X > 1 && point.X < 10 && point.Y > 0 && point.Y < 9)
                {
                    QueueWrong.Add(new Point(point.X + 1, point.Y));
                    QueueWrong.Add(new Point(point.X - 1, point.Y));
                    QueueWrong.Add(new Point(point.X, point.Y + 1));
                    QueueWrong.Add(new Point(point.X, point.Y - 1));
                }
            }


            if (UserShip4_1.Contains(point))
            { // счет сколько живых кораблей осталось у игрока, есть ли раненные
                UserShip4_1.Remove(point);
                foreach (var point1 in UserShip4_1)
                {
                    Queue.Add(point1);
                }
            }
            else if (UserShip3_1.Contains(point))
            {
                UserShip3_1.Remove(point);
                foreach (var point1 in UserShip3_1)
                {
                    Queue.Add(point1);
                }
            }
            else if (UserShip3_2.Contains(point))
            {
                UserShip3_2.Remove(point);
                foreach (var point1 in UserShip3_2)
                {
                    Queue.Add(point1);
                }
            }
            else if (UserShip2_1.Contains(point))
            {
                UserShip2_1.Remove(point);
                foreach (var point1 in UserShip2_1)
                {
                    Queue.Add(point1);
                }
            }
            else if (UserShip2_2.Contains(point))
            {
                UserShip2_2.Remove(point);
                foreach (var point1 in UserShip2_2)
                {
                    Queue.Add(point1);
                }
            }
            else if (UserShip2_3.Contains(point))
            {
                UserShip2_3.Remove(point);
                foreach (var point1 in UserShip2_3)
                {
                    Queue.Add(point1);
                }
            }

            List<Point> tempList = new List<Point>();

            foreach (var point1 in QueueWrong)
            {
                if (Queue.Contains(point1))
                {
                    tempList.Add(point1);
                }
            }

            foreach (var point2 in tempList)
            {
                QueueWrong.Remove(point2);
            }

            tempList.Clear();
            QueueWrong.Clear();
        }

        private void ShipsObvodkaAndChek(string whoShoots, Point shootPoint)
        {
            if (whoShoots == "computer")
            {
                if (UserShip4_1.Contains(shootPoint))
                {  // взбесившийся ИИ не успокоится пока не добьет раненный корабль игрока
                    UserShip4_1.Remove(shootPoint);
                    Chatter.GameWriteNotification(richTextBox1, "Компьютер подбивает четырехпалубный корабль игрока");
                    if (UserShip4_1.Count == 0)
                    {
                        foreach (var point in UserShip4_1Obvodka)
                        { // после уничтожения любого корабля он обводится вокруг радиусом в 1 клетку 
                            CellUpdate(dataGridView1, point, Miss);
                            CompMisses.Add(point);
                            Chatter.GameWriteNotification(richTextBox1, "Компьютер потопил четырехпалубный корабль игрока");
                        }
                    }
                }
                else if (UserShip3_1.Contains(shootPoint))
                {
                    UserShip3_1.Remove(shootPoint);
                    Chatter.GameWriteNotification(richTextBox1, "Компьютер подбивает трехпалубный корабль игрока");
                    if (UserShip3_1.Count == 0)
                    {
                        foreach (var point in UserShip3_1Obvodka)
                        {
                            CellUpdate(dataGridView1, point, Miss);
                            CompMisses.Add(point);
                            Chatter.GameWriteNotification(richTextBox1, "Компьютер потопил трехпалубный корабль игрока");
                        }
                    }
                }
                else if (UserShip3_2.Contains(shootPoint))
                {
                    UserShip3_2.Remove(shootPoint);
                    Chatter.GameWriteNotification(richTextBox1, "Компьютер подбивает трехпалубный корабль игрока");
                    if (UserShip3_2.Count == 0)
                    {
                        foreach (var point in UserShip3_2Obvodka)
                        {
                            CellUpdate(dataGridView1, point, Miss);
                            CompMisses.Add(point);
                            Chatter.GameWriteNotification(richTextBox1, "Компьютер потопил трехпалубный корабль игрока");
                        }
                    }
                }
                else if (UserShip2_1.Contains(shootPoint))
                {
                    UserShip2_1.Remove(shootPoint);
                    Chatter.GameWriteNotification(richTextBox1, "Компьютер подбивает двопалубный корабль игрока");
                    if (UserShip2_1.Count == 0)
                    {
                        foreach (var point in UserShip2_1Obvodka)
                        {
                            CellUpdate(dataGridView1, point, Miss);
                            CompMisses.Add(point);
                            Chatter.GameWriteNotification(richTextBox1, "Компьютер потопил двопалубный корабль игрока");
                        }
                    }
                }
                else if (UserShip2_2.Contains(shootPoint))
                {
                    UserShip2_2.Remove(shootPoint);
                    Chatter.GameWriteNotification(richTextBox1, "Компьютер подбивает двопалубный корабль игрока");
                    if (UserShip2_2.Count == 0)
                    {
                        foreach (var point in UserShip2_2Obvodka)
                        {
                            CellUpdate(dataGridView1, point, Miss);
                            CompMisses.Add(point);
                            Chatter.GameWriteNotification(richTextBox1, "Компьютер потопил двопалубный корабль игрока");
                        }
                    }
                }
                else if (UserShip2_3.Contains(shootPoint))
                {
                    UserShip2_3.Remove(shootPoint);
                    Chatter.GameWriteNotification(richTextBox1, "Компьютер подбивает двопалубный корабль игрока");
                    if (UserShip2_3.Count == 0)
                    {
                        foreach (var point in UserShip2_3Obvodka)
                        {
                            CellUpdate(dataGridView1, point, Miss);
                            CompMisses.Add(point);
                            Chatter.GameWriteNotification(richTextBox1, "Компьютер потопил двопалубный корабль игрока");
                        }
                    }
                }
                else if (UserShip1_1.Contains(shootPoint))
                {
                    UserShip1_1.Remove(shootPoint);
                    if (UserShip1_1.Count == 0)
                    {
                        foreach (var point in UserShip1_1Obvodka)
                        {
                            CellUpdate(dataGridView1, point, Miss);
                            CompMisses.Add(point);
                            Chatter.GameWriteNotification(richTextBox1, "Компьютер потопил однопалубный корабль игрока");
                        }
                    }
                }
                else if (UserShip1_2.Contains(shootPoint))
                {
                    UserShip1_2.Remove(shootPoint);
                    if (UserShip1_2.Count == 0)
                    {
                        foreach (var point in UserShip1_2Obvodka)
                        {
                            CellUpdate(dataGridView1, point, Miss);
                            CompMisses.Add(point);
                            Chatter.GameWriteNotification(richTextBox1, "Компьютер потопил однопалубный корабль игрока");
                        }
                    }
                }
                else if (UserShip1_3.Contains(shootPoint))
                {
                    UserShip1_3.Remove(shootPoint);
                    if (UserShip1_3.Count == 0)
                    {
                        foreach (var point in UserShip1_3Obvodka)
                        {
                            CellUpdate(dataGridView1, point, Miss);
                            CompMisses.Add(point);
                            Chatter.GameWriteNotification(richTextBox1, "Компьютер потопил однопалубный корабль игрока");
                        }
                    }
                }
                else if (UserShip1_4.Contains(shootPoint))
                {
                    UserShip1_4.Remove(shootPoint);
                    if (UserShip1_4.Count == 0)
                    {
                        foreach (var point in UserShip1_4Obvodka)
                        {
                            CellUpdate(dataGridView1, point, Miss);
                            CompMisses.Add(point);
                            Chatter.GameWriteNotification(richTextBox1, "Компьютер потопил однопалубный корабль игрока");
                        }
                    }
                }

                ShipsDead.Add(shootPoint);
                Ships.Remove(shootPoint);
                Queue.Remove(shootPoint);
                QueueWrong.Clear();
                CellUpdate(dataGridView1, shootPoint, Dead);
                UserCellsLeft--;
            }
            else if (whoShoots == "player")
            {
                if (CompShip4_1.Contains(shootPoint))
                {
                    CompShip4_1.Remove(shootPoint);
                    Chatter.GameWriteNotification(richTextBox1, "Игрок подбивает четырехпалубный корабль компьютера");
                    if (CompShip4_1.Count == 0)
                    {
                        foreach (var point in CompShip4_1Obvodka)
                        {
                            CellUpdate(dataGridView2, point, Miss);
                            UserMisses.Add(point);
                            Chatter.GameWriteNotification(richTextBox1, "Игрок потопил четырехпалубный корабль компьютера");
                        }
                    }
                }
                else if (CompShip3_1.Contains(shootPoint))
                {
                    CompShip3_1.Remove(shootPoint);
                    Chatter.GameWriteNotification(richTextBox1, "Игрок подбивает трехпалубный корабль компьютера");
                    if (CompShip3_1.Count == 0)
                    {
                        foreach (var point in CompShip3_1Obvodka)
                        {
                            CellUpdate(dataGridView2, point, Miss);
                            UserMisses.Add(point);
                            Chatter.GameWriteNotification(richTextBox1, "Игрок потопил трехпалубный корабль компьютера");
                        }
                    }
                }
                else if (CompShip3_2.Contains(shootPoint))
                {
                    CompShip3_2.Remove(shootPoint);
                    Chatter.GameWriteNotification(richTextBox1, "Игрок подбивает трехпалубный корабль компьютера");
                    if (CompShip3_2.Count == 0)
                    {
                        foreach (var point in CompShip3_2Obvodka)
                        {
                            CellUpdate(dataGridView2, point, Miss);
                            UserMisses.Add(point);
                            Chatter.GameWriteNotification(richTextBox1, "Игрок потопил трехпалубный корабль компьютера");
                        }
                    }
                }
                else if (CompShip2_1.Contains(shootPoint))
                {
                    CompShip2_1.Remove(shootPoint);
                    Chatter.GameWriteNotification(richTextBox1, "Игрок подбивает двопалубный корабль компьютера");
                    if (CompShip2_1.Count == 0)
                    {
                        foreach (var point in CompShip2_1Obvodka)
                        {
                            CellUpdate(dataGridView2, point, Miss);
                            UserMisses.Add(point);
                            Chatter.GameWriteNotification(richTextBox1, "Игрок потопил двопалубный корабль компьютера");
                        }
                    }
                }
                else if (CompShip2_2.Contains(shootPoint))
                {
                    CompShip2_2.Remove(shootPoint);
                    Chatter.GameWriteNotification(richTextBox1, "Игрок подбивает двопалубный корабль компьютера");
                    if (CompShip2_2.Count == 0)
                    {
                        foreach (var point in CompShip2_2Obvodka)
                        {
                            CellUpdate(dataGridView2, point, Miss);
                            UserMisses.Add(point);
                            Chatter.GameWriteNotification(richTextBox1, "Игрок потопил двопалубный корабль компьютера");
                        }
                    }
                }
                else if (CompShip2_3.Contains(shootPoint))
                {
                    CompShip2_3.Remove(shootPoint);
                    Chatter.GameWriteNotification(richTextBox1, "Игрок подбивает двопалубный корабль компьютера");
                    if (CompShip2_3.Count == 0)
                    {
                        foreach (var point in CompShip2_3Obvodka)
                        {
                            CellUpdate(dataGridView2, point, Miss);
                            UserMisses.Add(point);
                            Chatter.GameWriteNotification(richTextBox1, "Игрок потопил двопалубный корабль компьютера");
                        }
                    }
                }
                else if (CompShip1_1.Contains(shootPoint))
                {
                    CompShip1_1.Remove(shootPoint);
                    if (CompShip1_1.Count == 0)
                    {
                        foreach (var point in CompShip1_1Obvodka)
                        {
                            CellUpdate(dataGridView2, point, Miss);
                            UserMisses.Add(point);
                            Chatter.GameWriteNotification(richTextBox1, "Игрок потопил однопалубный корабль компьютера");
                        }
                    }
                }
                else if (CompShip1_2.Contains(shootPoint))
                {
                    CompShip1_2.Remove(shootPoint);
                    if (CompShip1_2.Count == 0)
                    {
                        foreach (var point in CompShip1_2Obvodka)
                        {
                            CellUpdate(dataGridView2, point, Miss);
                            UserMisses.Add(point);
                            Chatter.GameWriteNotification(richTextBox1, "Игрок потопил однопалубный корабль компьютера");
                        }
                    }
                }
                else if (CompShip1_3.Contains(shootPoint))
                {
                    CompShip1_3.Remove(shootPoint);
                    if (CompShip1_3.Count == 0)
                    {
                        foreach (var point in CompShip1_3Obvodka)
                        {
                            CellUpdate(dataGridView2, point, Miss);
                            UserMisses.Add(point);
                            Chatter.GameWriteNotification(richTextBox1, "Игрок потопил однопалубный корабль компьютера");
                        }
                    }
                }
                else if (CompShip1_4.Contains(shootPoint))
                {
                    CompShip1_4.Remove(shootPoint);
                    if (CompShip1_4.Count == 0)
                    {
                        foreach (var point in CompShip1_4Obvodka)
                        {
                            CellUpdate(dataGridView2, point, Miss);
                            UserMisses.Add(point);
                            Chatter.GameWriteNotification(richTextBox1, "Игрок потопил однопалубный корабль компьютера");
                        }
                    }
                }
                CompShipsDead.Add(shootPoint);
                CompShips.Remove(shootPoint);
                CellUpdate(dataGridView2, shootPoint, Dead);
                CompCellsLeft--;
            }
        }

        private void ComputerShootsEvent()
        { // следит чей ход и константирует факты ходившего в этот ход
            this.Cursor = Cursors.WaitCursor;
            Point compShoot = new Point();

            if (Queue.Count == 0)
            {
                compShoot = RandomPoint();
            }
            else if (Queue.Count != 0)
            {
                compShoot = SmartComputer();
            }

            if (Ships.Contains(compShoot))
            {
                if (Queue.Count == 0)
                {
                    AddQueue(compShoot);
                }

                ShipsObvodkaAndChek("computer", compShoot);
                CellUpdate(dataGridView1, compShoot, Dead);
                ToWin();
            }
            else
            {
                CompMisses.Add(compShoot);
                QueueWrong.Remove(compShoot);
                CellUpdate(dataGridView1, compShoot, Miss);
                Chatter.GameWriteNotification(richTextBox1, ProgramDetect.PlayerName + " промахивается");
                Chatter.GameWriteNotification(richTextBox1, "Стреляет игрок");
                dataGridView2.Enabled = true;
                ComputerShoots = false;
                UserShoots = true;
                timer1.Enabled = false;
                this.Cursor = Cursors.Default;
            }
        }

        private void PlayerShoot(Point shoot)
        { // помощь для игрока, если он в игре допускает ошибку 
          // проверка попадания или промаха и констатация факта
            if (!LanGame)
            {
                if (UserShoots && dataGridView2.CurrentCell.ColumnIndex > 0)
                {
                    if (UserMisses.Contains(shoot) || CompShipsDead.Contains(shoot))
                    {
                        Chatter.GameWriteNotification(richTextBox1, "Вы уже стреляли в это место");
                    }
                    else
                    { 
                        if (CompShips.Contains(shoot))
                        {
                            ShipsObvodkaAndChek("player", shoot);
                            ToWin();
                        }
                        else
                        {
                            UserMisses.Add(shoot);
                            Chatter.GameWriteNotification(richTextBox1, "Игрок промахивается");
                            Chatter.GameWriteNotification(richTextBox1, "Стреляет компьютер");
                            CellUpdate(dataGridView2, shoot, Miss);
                            UserShoots = false;
                            ComputerShoots = true;
                            dataGridView2.Enabled = false;
                            timer1.Enabled = true;
                        }
                    }
                }
                else if (UserShoots && dataGridView2.CurrentCell.ColumnIndex == 0)
                {
                    Chatter.GameWriteNotification(richTextBox1, "Вы выбрали не правильную клетку");
                }
            }
        }

        private Point RandomPoint()
        { // рандомный выстрел для ИИ, если нет раненых кораблей игрока
            Random x = new Random(DateTime.Now.Millisecond - 100);
            Random y = new Random(DateTime.Now.Millisecond);
            Point compShoot = new Point();

            compShoot = new Point(x.Next(1, 11), y.Next(0, 10));

            while (ShipsDead.Contains(compShoot) || CompMisses.Contains(compShoot))
            {
                compShoot = new Point(x.Next(1, 11), y.Next(0, 10));
            }

            return compShoot;
        }

        private void ToWin()
        { // константация факта победы игрока или ИИ при уничтожении всех кораблей противника
          // при ранении корабля противника игроку или ИИ дается еще ход
            if (UserCellsLeft == 0)
            {
                ComputerShoots = false;
                UserShoots = false;
                foreach (var point in CompShips)
                {
                    CellUpdate(dataGridView2, point, ShipCell);
                }
                dataGridView1.Enabled = false;
                dataGridView2.Enabled = false;
                Chatter.GameWriteNotification(richTextBox1, "Компьютер выиграл!");
                Chatter.ErrorMessage("Компьютер выиграл!");
            }
            else if (CompCellsLeft == 0)
            {
                ComputerShoots = false;
                UserShoots = false;
                dataGridView1.Enabled = false;
                dataGridView2.Enabled = false;
                Chatter.GameWriteNotification(richTextBox1, "Вы выиграли!");
                Chatter.ErrorMessage("Вы выиграли!");
            }
            else if (UserShoots)
            {
                Chatter.GameWriteNotification(richTextBox1, "Игрок стреляет еще раз");
            }
            else if (ComputerShoots)
            {
                Chatter.GameWriteNotification(richTextBox1, "Компьютер стреляет еще раз");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        { // таймер для каждого хода, по умолчанию время не ограничено
            ComputerShootsEvent();
        }

        private void выходToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void новаяИграПоСетиToolStripMenuItem_Click(object sender, EventArgs e)
        { // вызов формы Connection при начале сетевой игры
            const string message = "Вы действительно хотите начать новую игру по сети?";
            const string caption = "Новая игра по сети";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }
            else if (result == DialogResult.Yes)
            {
                Form connection = new Connection();
                connection.ShowDialog();
            }
        }

        public void SelectText(string text, Color color)
        { // поддержка чата 
            if (richTextBox1.InvokeRequired)
                richTextBox1.Invoke(new Action(delegate { SelectTextFunc(text, color); }));
            else
                SelectTextFunc(text, color);
        }

        private void SelectTextFunc(string text, Color color)
        { // для выделения цветом в чате сообщений разных игроков
            /*int n = richTextBox1.Find(text);
            while (n > -1)
            {
                richTextBox1.Select(n, text.Length);
                richTextBox1.SelectionColor = color;
                n = richTextBox1.Find(text, n + text.Length, RichTextBoxFinds.MatchCase);
            }*/
        }

        private void ChatWriteMessage()
        { // чат при сетевой игре в котором можно активировать чит на победу
            if (textBox1.Text.Length != 0)
            {
                if (textBox1.Text == ProgramDetect.Cheat)
                {
                    ChatSendMessage(textBox1.Text);
                    textBox1.Text = null;
                }
                else
                {
                    string s = textBox1.Text;
                    ChatSendMessage(s);
                    if (ProgramDetect.Running == "server")
                        richTextBox1.Text += Players.ServerName + ": " + s + "\n";
                    else if (ProgramDetect.Running == "client")
                        richTextBox1.Text += Players.ClientName + ": " + s + "\n";
                    SelectText(Players.ServerName + ":", Color.DarkRed);
                    SelectText(Players.ClientName + ":", Color.DarkGreen);
                    /*if (Chatter.SelfRef != null)
                        Chatter.GameNameColor();*/
                    textBox1.Text = null;
                    richTextBox1.SelectionStart = richTextBox1.Text.Length;
                    richTextBox1.ScrollToCaret();
                }

            }
        }

        public void RichTextBoxUpdateText(string who, string text)
        { // звуковое сопровождение при приеме сообщения от другого игрока
            PlaySound("message.wav");
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new Action(delegate
                {
                    UpdatedFunction(who, text);
                }))
                ;
            }
            else
            {
                UpdatedFunction(who, text);
            }
        }

        public void UpdatedFunction(string who, string text)
        { // для выделения сообщений разным цветом
            string s = text;

            if (who == "server")
                richTextBox1.Text += Players.ClientName + ": " + s + "\n";
            else if (who == "client")
                richTextBox1.Text += Players.ServerName + ": " + s + "\n";

            SelectText(Players.ServerName + ":", Color.DarkRed);
            SelectText(Players.ClientName + ":", Color.DarkGreen);
            /*if (Chatter.SelfRef != null)
                Chatter.GameNameColor();*/
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void ChatSendMessage(string message)
        { // передача сообщений в чате
            if (message == ProgramDetect.Cheat)
            {
                if (ProgramDetect.Running == "server")
                {
                    Server.SelfRef.SendData(message);
                }
                else if (ProgramDetect.Running == "client")
                {
                    Client.SelfRef.SendMessage(message);
                }
            }
            else
            {
                if (ProgramDetect.Running == "server")
                {
                    Server.SelfRef.SendData("#M" + message);
                }
                else if (ProgramDetect.Running == "client")
                {
                    Client.SelfRef.SendMessage("#M" + message);
                }
            }

        }

        private void ChatGameWriteFunc(string message)
        { // активация кнопки для отправки сообщений
            richTextBox1.Text += message;
            SelectText(Players.ServerName + ":", Color.DarkRed);
            SelectText(Players.ClientName + ":", Color.DarkGreen);
            /*if (Chatter.SelfRef != null)
                Chatter.GameNameColor();*/
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        public void ChatGameWrite(string text)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new Action(delegate
                {
                    ChatGameWriteFunc(text);
                }));
            }
            else
            {
                ChatGameWriteFunc(text);
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            ChatWriteMessage();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        { // Игроку можно ввести свой ник для игры по сети
            if (!Char.IsLetterOrDigit(e.KeyChar) && !Viraz.Contains(e.KeyChar))
            {
                if (e.KeyChar != (Char)Keys.Back)
                    e.Handled = true;
                if (e.KeyChar == (char)13)
                {
                    button4_Click_1(sender, e);
                    e.Handled = true;
                }
            }
        }

        public void HideSender(string state)
        { // действия при открытии и закрытии форм
            switch (state)
            {
                case "hide":
                    textBox1.Enabled = false;
                    button4.Enabled = false;
                    textBox1.Visible = false;
                    button4.Visible = false;
                    tabControl1.Height = 207;
                    break;
                case "show":
                    textBox1.Enabled = true;
                    button4.Enabled = true;
                    textBox1.Visible = true;
                    button4.Visible = true;
                    tabControl1.Height = 160;
                    break;
            }
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        { // для добавления кораблей при расстановке
            ShipAdd(dataGridView1, dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex);
        }

        #endregion

        #region LAN

        public void LeftPanel(string text)
        { // работа с панелью сетевой игры, которую нельзя запустить в 2 диалоговых окнах и более
            if (text == "grbox")
            {
                if (panel1.InvokeRequired)
                {
                    panel1.Invoke(new Action(delegate
                                                 {
                                                     panel1.Visible = false;
                                                     panel1.Hide(); 
                                                 }));
                } 
                else
                {
                    panel1.Visible = false;
                    panel1.Hide();
                }

                if (groupBox1.InvokeRequired)
                {
                    groupBox1.Invoke(new Action(delegate
                                                    {
                                                        groupBox1.Visible = true;
                                                        groupBox1.Show();
                                                    }));
                }
                else
                {
                    groupBox1.Visible = true;
                    groupBox1.Show();
                }
                ControlEnabler1(true);
            }
            else if (text == "panel")
            {
                if (groupBox1.InvokeRequired)
                {
                    groupBox1.Invoke(new Action(delegate
                                                    {
                                                        groupBox1.Visible = false;
                                                        groupBox1.Hide();
                                                    }));
                }
                else
                {
                    groupBox1.Visible = false;
                    groupBox1.Hide();
                }

                if (panel1.InvokeRequired)
                {
                    panel1.Invoke(new Action(delegate
                                                 {
                                                     panel1.Visible = true;
                                                     panel1.Show();
                                                 }));
                }
                else
                {
                    panel1.Visible = true;
                    panel1.Show();
                }
            }
        }

        private void PlayerShootLan(Point shoot)
        {
            if (dataGridView2.CurrentCell.ColumnIndex > 0)
            {
                if (UserMisses.Contains(shoot) || CompShipsDead.Contains(shoot))
                {
                    Chatter.GameWriteNotification(richTextBox1, "Вы уже стреляли в это место");
                }
                else
                {
                    //Добавление точки в буфер
                    ProgramDetect.BufferedPoint = shoot;
                    //Отсылка точки
                    SendShoot(shoot);
                    //Запрет стрельбы до получения ответа
                    LanAllowShoot(false);
                }
            }
            else if (dataGridView2.CurrentCell.ColumnIndex == 0)
            {
                Chatter.GameWriteNotification(richTextBox1, "Вы выбрали не правильную клетку");
            }
        }

        public void LanForm(string action)
        { // 3 последовательно сменющиеся состояния сетевой игры
            const string waitingStr = " - ожидание...";
            const string doneStr = " - готово!";
            const string turnStr = " стреляет...";
            switch (action)
            {
                case "wait":
                    if (ProgramDetect.Running == "server")
                    {
                        LabelInvoke(label42, Players.ServerName + waitingStr);
                        LabelInvoke(label1, Players.ClientName + waitingStr);
                    }
                    else if (ProgramDetect.Running == "client")
                    {
                        LabelInvoke(label42, Players.ClientName + waitingStr);
                        LabelInvoke(label1, Players.ServerName + waitingStr);
                    }
                    break;
                case "server done":
                    if (ProgramDetect.Running == "server")
                    {
                        LabelInvoke(label42, Players.ServerName + doneStr);
                        LabelInvoke(label1, Players.ClientName + waitingStr);
                    }
                    else if (ProgramDetect.Running == "client")
                    {
                        LabelInvoke(label42, Players.ClientName + waitingStr);
                        LabelInvoke(label1, Players.ServerName + doneStr);
                    }
                    break;
                case "client done":
                    if (ProgramDetect.Running == "server")
                    {
                        LabelInvoke(label42, Players.ServerName + waitingStr);
                        LabelInvoke(label1, Players.ClientName + doneStr);
                    }
                    else if (ProgramDetect.Running == "client")
                    {
                        LabelInvoke(label42, Players.ClientName + doneStr);
                        LabelInvoke(label1, Players.ServerName + waitingStr);
                    }
                    break;
                case "server turn":
                    if (ProgramDetect.Running == "server")
                    {
                        LabelInvoke(label42, Players.ServerName + turnStr);
                        LabelInvoke(label1, Players.ClientName);
                    }
                    else if (ProgramDetect.Running == "client")
                    {
                        LabelInvoke(label42, Players.ClientName);
                        LabelInvoke(label1, Players.ServerName + turnStr);
                    }
                    break;
                case "client turn":
                    if (ProgramDetect.Running == "server")
                    {
                        LabelInvoke(label42, Players.ServerName);
                        LabelInvoke(label1, Players.ClientName + turnStr);                        
                    }
                    else if (ProgramDetect.Running == "client")
                    {
                        LabelInvoke(label42, Players.ClientName + turnStr);
                        LabelInvoke(label1, Players.ServerName);
                    }
                    break;
            }
        }

        public void SendShoot(Point shoot)
        {
            switch (ProgramDetect.Running)
            {  // контроль чей ход при сетевой игре 
                case "server":
                    Server.SelfRef.SendData("#Y" + shoot);
                break;
                case "client":
                    Client.SelfRef.SendMessage("#Y" + shoot);
                break;
            }
        }

        public void SendShootAnswer(bool answer)
        { // константация факта попадания в сетевой игре 
            switch (ProgramDetect.Running)
            {
                case "server":
                    if (answer)
                        Server.SelfRef.SendData("#U1");
                    else
                        Server.SelfRef.SendData("#U0");
                    break;

                case "client":
                    if (answer)
                        Client.SelfRef.SendMessage("#U1");
                    else
                        Client.SelfRef.SendMessage("#U0");
                    break;
            }
        }

        public void SendObvodkaPoint(Point point)
        {  // обводка на 1 клетку вокруг потопленного корабля
            switch (ProgramDetect.Running)
            {
                case "server":
                    Server.SelfRef.SendData("#O" + point);
                    break;

                case "client":
                    Client.SelfRef.SendMessage("#O" + point);
                    break;
            }
        }

        public void ReceiveObvodkaPoint(Point point)
        {
            CellUpdate(dataGridView2, point, Miss);
            UserMisses.Add(point);
        }

        public void SendLeftShipPoint()
        {  // стрельба по кораблям в сетевой игре 
            foreach (var shipPoint in Ships)
            {
                switch (ProgramDetect.Running)
                {
                    case "server":
                        Server.SelfRef.SendData("#J" + shipPoint);
                        break;
                    case "client":
                        Client.SelfRef.SendMessage("#J" + shipPoint);
                        break;
                }
            }
        }

        public void ReceiveLeftShipPoint(Point point)
        {
            CellUpdate(dataGridView2, point, ShipCell);
        }

        public void ReceiveShoot(Point shoot)
        {
            //если попали в корабль
            if (Ships.Contains(shoot))
            {
                //Включаем онимацию ожидания
                PrBarAnimation(true);
                //Отсылаем положительный ответ о попадании в корабль
                SendShootAnswer(true);
                //Обновляем фон клетки в которую попали
                CellUpdate(dataGridView1, shoot, Dead);
                //ищем корабль из списка кораблей который содержит клетку, в которую попали
                var ship = AllUserShips.Where(x => x.Contains(shoot)).First();  
                //проверяем подбит или убит корабль
                if (ship.Count == 1)
                {
                    //в случае если корабль убит воспроизводим звук окончательного взрыва
                    PlaySound("explosion_finish.wav");
                    //ищем индекс списка в который входит выстрел
                    var indx = AllUserShips.FindIndex(s => s.Contains(shoot));
                    //выбираем список с клетками обводки корабля по найденному индексу
                    var shipObvodka = AllUserShipsObvodka[indx];
                    foreach (var point in shipObvodka)
                    {
                        //отсылаем клетку второму игроку, чтобы он закрасил ее как обводку, и добавил в промахи
                        SendObvodkaPoint(point);
                        //закрашиваем у себя эту клетку
                        CellUpdate(dataGridView1, point, Miss);
                    }
                }
                //если корабль только подбит то воспроиводим звук взрыва
                else PlaySound("explosion.wav");
                //удаляем клетку из списка клеток корабля, в который попали
                ship.Remove(shoot);
                //удаляем клетку из списка оставшихся клеток
                Ships.Remove(shoot);
                //Проверяем условия победы
                LanChekWin();
            }
            // если не попали в корабль
            else if (!Ships.Contains(shoot))
            {
                //проигрываем звук промаха
                PlaySound("miss.wav");
                //отправляем ответ игроку о промахе
                SendShootAnswer(false);
                //Обновляем фон клетки в которую попали
                CellUpdate(dataGridView1, shoot, Miss);
                //разрешаем стрельбу локальному игроку
                LanAllowShoot(true);
                //убираем анимацию прогресса о ожидании
                PrBarAnimation(false);
                //обновляем заголовки полей
                LanForm((ProgramDetect.Running == "server") ? "server turn" : "client turn");
            }
        }

        public void ReceiveShootAnswer(int ans)
        {  // соответствующие реакции на попадание и промах и вывод чат инфы о ходе
            if (ans == 1)
            {
                PlaySound("explosion_other.wav");
                PrBarAnimation(false);
                CompShipsDead.Add(ProgramDetect.BufferedPoint);
                CellUpdate(dataGridView2, ProgramDetect.BufferedPoint, Dead);
                LanAllowShoot(true);
                WriteGM("попадает.");
                WriteGM("стреляет еще раз.");                
            }
            else if (ans == 0)
            {
                PlaySound("miss.wav");
                PrBarAnimation(true);
                UserMisses.Add(ProgramDetect.BufferedPoint);
                CellUpdate(dataGridView2, ProgramDetect.BufferedPoint, Miss);
                WriteGM("промахивается.");
                LanForm((ProgramDetect.Running == "server") ? "client turn" : "server turn");

                switch (ProgramDetect.Running)
                {
                    case "server":
                        Chatter.GameWriteNotification(richTextBox2, Players.ClientName + " стреляет.");
                        break;

                    case "client":
                        Chatter.GameWriteNotification(richTextBox2, Players.ServerName + " стреляет.");
                        break;
                }
            }
        }

        public void LanAllowShoot(bool allow)
        { // разрешение стрельбы локальному игроку после синхронизации с сервером 
            if (dataGridView2.InvokeRequired)
            {
                dataGridView2.Invoke(new Action(delegate { dataGridView2.Enabled = allow; }));
            }
            else dataGridView2.Enabled = allow;
        }

        public void WriteGM(string message)
        {  // подтверждение готовности игры от игроков
            switch (ProgramDetect.Running)
            {
                case "server":
                    Chatter.GameWriteNotification(richTextBox2, Players.ServerName + " " + message);
                    break;

                case "client":
                    Chatter.GameWriteNotification(richTextBox2, Players.ClientName + " " + message);
                    break;
            }
        }

        public void ReceiveGM(string message)
        {  // начало игры после подтверждения готовности 
            Chatter.SelfRef.GameUpdateNotification(richTextBox2, message);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {  // допустимое время ожидания для синхронизации во время сетевой игры
            if (ProgramDetect.ServerReady && ProgramDetect.ClientReady)
            {
                LeftPanel("panel");
                if (ProgramDetect.Running == "server")
                {
                    PrBarAnimation(false);
                    ProgramDetect.PlayerName = Players.ServerName;
                    LanForm("server turn");
                    LanAllowShoot(true);
                    WriteGM("стреляет.");
                }
                else if (ProgramDetect.Running == "client")
                {
                    ProgramDetect.PlayerName = Players.ClientName;
                    LanForm("server turn");
                    LanAllowShoot(false);
                    PrBarAnimation(true);
                }                
                timer2.Enabled = false;
            }
        }

        private void LanChekWin()
        {  // сообщение проигравшему игроку
            if (Ships.Count == 0)
            {
                PlaySound("loose.wav");
                MessageBox.Show("Все ваши корабли затоплены противником. Вы проиграли!", "Игра окончена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (ProgramDetect.Running == "server")
                    Server.SelfRef.SendData("~#EndGame#~");
                else
                    Client.SelfRef.SendMessage("~#EndGame#~");
                TotalClearForNewLan();
            }
        }
        public void Loose()
        {  // сообщение победившему игроку 
            PlaySound("win.wav");
            MessageBox.Show("Вы потопили все корабли противника. Вы выиграли!", "Игра окончена", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TotalClearForNewLan();
        }

        public void DisableAll(bool state)
        {  // в случае добровольного выхода из игры одного из игроков игра завершается
           // если время ожидания для синхронизации превышено, игра завершается 
            if (state)
            {
                if (dataGridView2.InvokeRequired)
                {
                    dataGridView2.Invoke(new Action(delegate { dataGridView2.Enabled = false; }));
                }
                else dataGridView2.Enabled = false;
                
                if (textBox1.InvokeRequired)
                {
                    textBox1.Invoke(new Action(delegate { textBox1.Enabled = false; }));
                }
                else textBox1.Enabled = false;

                if (button4.InvokeRequired)
                {
                    button4.Invoke(new Action(delegate { button4.Enabled = false; }));
                }
                else button4.Enabled = false;
                switch (ProgramDetect.Running)
                {
                    case "server":
                        Server.SelfRef.CloseConnection(2);
                        break;
                    case "client":
                        Client.SelfRef.CloseConnection();
                        break;
                }
            }
        }

        public void DisableNewLanGame(bool state)
        {  // выбор варианта о начале сетевой игры
            if (state)
                новаяИграПоСетиToolStripMenuItem.Enabled = false;
            else if (!state)
                новаяИграПоСетиToolStripMenuItem.Enabled = true;
        }
        #endregion      

        private void звукToolStripMenuItem_Click(object sender, EventArgs e)
        {  // включение или выключение звука в настройках
            Properties.Settings.Default.Sound = !Properties.Settings.Default.Sound;
            Properties.Settings.Default.Save();
            звукToolStripMenuItem.Checked = Properties.Settings.Default.Sound;
        }

        private void PlaySound(string sound)
        {  // если в настройках звук вкл, то воспроизведение звука при событиях
            if (Properties.Settings.Default.Sound)
            {
                System.Media.SoundPlayer player =
                new System.Media.SoundPlayer();
                string path = Environment.CurrentDirectory + @"\sounds\" + sound;
                player.SoundLocation = path;
                player.Load();
                player.Play();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {  // кнопка сдаться, судьба любит непоколебимых
            MessageBox.Show("На данный момент эта функция отключена :(", "Судьба любит непоколебимых", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void разработчикToolStripMenuItem_Click(object sender, EventArgs e)
        {  // раздел справка
            const string text = "Разработчиком и правообладателем данной игры является Авдеенко Константин Сергеевич. Все " +
                                "предложения или замечания присылать на E-mail: AvdeenkoKonstantin1995@gmail.com";
            const string header = "О разработчике";
            MessageBox.Show(text, header, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void оИгреToolStripMenuItem_Click(object sender, EventArgs e)
        {  // раздел справка
            const string text = "Игра Морской Бой разработана в России, г. Краснодар. Для сетевой игры необходимо два игрока, " +
                                "у одного из них должен быть белый IP если игра будет вестить через сеть интернет. Этот игрок с " +
                                "белым IP должен выступать в качестве сервера для второго игрока. Так же стоит " +
                                "помнить, что игра не идеальная и в случае обнаружения какой-либо ошибки в игре, прошу присылать " +
                                "мне информацию на мой E-mail: AvdeenkoKonstantin1995@gmail.com Я постараюсь в кратчайшие сроки исправить ошибку и " +
                                "вышлю вам исправленный установщик в обратном письме.";
            const string header = "Информация";
            MessageBox.Show(text, header, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {  // параметры для строк матрицы поля 

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {  // параметры для столбцов матрицы поля 

        }
    }

    static class ProgramDetect
    {  // основные кнопки на формах запускающие другие модули
        public static Point BufferedPoint;
        public static string PlayerName;
        public static string Running;
        public static bool ServerReady;
        public static bool ClientReady;
        public static string Cheat = "c2he34at47";
    }
}