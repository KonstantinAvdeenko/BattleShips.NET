﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BattleShips
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        { // запускает форму Form1 и вспомогательные функции
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
