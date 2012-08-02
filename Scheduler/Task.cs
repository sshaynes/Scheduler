using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Scheduler
{
    public class Task
    {
        private string name { get; set; }
        private DateTime start { get; set; }
        private string schedule { get; set; }
        private bool active { get; set; }
        private string command { get; set; }
        private string user { get; set; }
        private string pass { get; set; }
        
        /// <summary>
        /// Creates a new Task
        /// </summary>
        /// <param name="Name">Task's desired name, must be unique</param>
        /// <param name="Command">Command to run when the task executes</param>
        /// <param name="Schedule">How often to run the task. Accepts: {list}</param>
        /// <param name="Start">Start time to run the command, in the time portion of a datetime for consistency (date portion is disregarded)</param>
        /// <param name="User">Desired username for the task to run under, if different from the default</param>
        /// <param name="Pass">Desired password for the username given, if different from the default</param>
        /// <param name="Enabled">Whether the task is currently active, defaults to false</param>
        public Task(string Name, string Command, string Schedule, DateTime Start, string User = "", string Pass = "", bool Enabled = false)
        {
            this.name = Name;
            this.command = Command;
            this.schedule = Schedule;
            this.start = Start;
            this.user = User == "" ? User : "Configuration Default";
            this.pass = Pass == "" ? Pass : "Configuration Default";

            if(Enabled){
                this.enable();
            }
        }

        /// <summary>
        /// Schedules the task in the Windows scheduled tasks list
        /// </summary>
        public void enable()
        {
            //Schtasks /Create /TN Notepad /TR notepad.exe /sc DAILY /ST 10:00 /RU administrator /RP password
            System.Diagnostics.Process.Start("Schtasks /Create " +
                "/TN " + name +
                " /TR " + command +
                " /sc " + schedule +
                " /ST " + start.TimeOfDay +
                " /RU " + (user != "Configuration Default" ? user : ConfigurationManager.AppSettings["user"]) +
                " /RP " + (pass != "Configuration Default" ? pass : ConfigurationManager.AppSettings["pass"])
            );

            active = true;
        }

        /// <summary>
        /// Deletes the task from the Windows scheduled tasks list
        /// </summary>
        public void disable()
        {
            //Schtasks /Create /TN Notepad /TR notepad.exe /sc DAILY /ST 10:00 /RU administrator /RP password
            System.Diagnostics.Process.Start("Schtasks /Delete " +
                "/TN " + name
            );

            active = false;
        }

        /// <summary>
        /// Runs the task as a one-off
        /// </summary>
        public void run()
        {
            System.Diagnostics.Process.Start(command);
        }
    }
}
