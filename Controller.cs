using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace SimulateKeyPress
{
    class Form1 : Form
    {
        private Button button1 = new Button();

        protected virtual bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        [STAThread]

        

        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.Run(new Form1());
            // Get a handle to the Game application. The window class
            // and window name were obtained using the Spy++ tool.
            IntPtr gameHandle = FindWindow(null, "DeSmuME 0.9.11 x64");

            // Verify that Game is a running process.
            if (gameHandle == IntPtr.Zero)
            {
                MessageBox.Show("Game is not running.");
                return;
            }
            // Make Game the foreground application and send it 
            // a set of inputs.
            Console.Write("Test console message.");
            //while (true)
            //{
            //        try
            //        {
            //            string msg = File.ReadAllText(@"C:\Users\Adam\Desktop\Discord Bot\messages.txt");

            //            SetForegroundWindow(gameHandle);
            //            for (int i = 0; i < 10; i++)
            //            {
            //                //string f = msg[i].ToString();
            //                SendKeys.SendWait(msg);
            //            }
            //        }
            //        catch (IOException)
            //        {

            //        }
            //}

            string path = @"C:\Users\Adam\Desktop\Discord Bot";
            MonitorDirectory(path);
            Console.ReadKey();
        }

        // FileSystemWatcher functionality built using https://www.infoworld.com/article/3185447/application-development/how-to-work-with-filesystemwatcher-in-c.html

        private static void MonitorDirectory(string path)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = @"C:\Users\Adam\Desktop\Discord Bot";
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Changed += FileSystemWatcher_Changed;
            watcher.EnableRaisingEvents = true;
        }

        private static void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File changed: {0}", e.Name);
        }

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            Console.Write("File was modified.");
            IntPtr gameHandle = FindWindow(null, "DeSmuME 0.9.11 x64");
            try
            {
                string msg = File.ReadAllText(@"C:\Users\Adam\Desktop\Discord Bot\messages.txt");

                SetForegroundWindow(gameHandle);
                for (int i = 0; i < 10; i++)
                {
                    //string f = msg[i].ToString();
                    SendKeys.SendWait(msg);
                }
            }
            catch (IOException)
            {

            }
        }

        public Form1()
        {
            button1.Location = new Point(10, 10);
            button1.TabIndex = 0;
            button1.Text = "Send Command";
            button1.AutoSize = true;
            button1.Click += new EventHandler(button1_Click);

            this.DoubleClick += new EventHandler(Form1_DoubleClick);
            this.Controls.Add(button1);
        }

        // Get a handle to an application window.
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);



        // Send a series of key presses to the Emulator application.
        private void button1_Click(object sender, EventArgs e)
        {
            // Get a handle to the Emulator application. The window class
            // and window name were obtained using the Spy++ tool.


            //IntPtr emulatorHandle = FindWindow(null, "DeSmuME 0.9.11 x64");

            //// Verify that Emulator is a running process.
            //if (emulatorHandle == IntPtr.Zero)
            //{
            //    MessageBox.Show("Emulator is not running.");
            //    return;
            //}

            //// Make Emulator the foreground application and send it 
            //// a set of inputs.
            //string msg = System.IO.File.ReadAllText(@"C:\Users\Adam\Desktop\Discord Bot\messages.txt");
            //SetForegroundWindow(emulatorHandle);
            //for (int i = 0; i < 10; i++)
            //{
            //    //string f = msg[i].ToString();
            //    SendKeys.SendWait(msg);
            //}


            //SendKeys.SendWait("a");
            //SendKeys.SendWait("s");
            //SendKeys.SendWait("a");
            //SendKeys.SendWait("s");
            //SendKeys.SendWait("a");
            //SendKeys.SendWait("{DOWN}");
            //SendKeys.SendWait("{UP}");
            //SendKeys.SendWait("{LEFT}");
            //SendKeys.SendWait("{RIGHT}");
            //SendKeys.SendWait("a");
            //SendKeys.SendWait("s");
            //SendKeys.SendWait("a");
            //SendKeys.SendWait("s");
            //SendKeys.SendWait("a");
            //SendKeys.SendWait("{LEFT}");
        }

        // Send a key to the button when the user double-clicks anywhere 
        // on the form.
        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            // Send the enter key to the button, which raises the click 
            // event for the button. This works because the tab stop of 
            // the button is 0.
            SendKeys.Send("{ENTER}");
        }
    }
}