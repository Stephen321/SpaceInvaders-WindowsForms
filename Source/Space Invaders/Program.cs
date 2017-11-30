using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Space_Invaders
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DateTime currentUpdateTime;
            DateTime lastUpdateTime;
            TimeSpan frameTime;
            currentUpdateTime = DateTime.Now;
            lastUpdateTime = DateTime.Now;

            Form1 form = new Form1();
            form.Show();
            while (form.Created == true)
            {
                currentUpdateTime = DateTime.Now;
                frameTime = currentUpdateTime - lastUpdateTime;
                if (frameTime.TotalMilliseconds > 10)
                {
                    Application.DoEvents();
                    form.UpdateWorld();
                    form.Refresh();
                    lastUpdateTime = DateTime.Now;
                }
            }
        }
    }
}
