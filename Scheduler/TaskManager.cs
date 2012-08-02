using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Scheduler
{
    static class TaskManager
    {
        public List<Task> tasks = new List<Task>();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public void addTask(string name, string command)
        {
            tasks.Add(new Task(name, command, "One time only", DateTime.Now));
        }

        public List<Task> getTasks()
        {
            return tasks;
        }

        // should provide a logging component, to log task runs in the database when there is one
        // maybe wrap windows tasks in a wrapper program so that this functionality can exist?
    }
}
