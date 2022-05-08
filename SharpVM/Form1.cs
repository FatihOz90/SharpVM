using System;
using System.Windows.Forms;

namespace SharpVM
{
    public partial class Form1 : Form
    {
        public static Form1 instance;

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

            //Form1 Instance
            instance = this;

            //Class Instance
            new Server();
            new GuestOS();
            new HttpService();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            HttpService.instance.HttpServiceStart();
            btnStart.Enabled = false;
            btnStart.Text = "Starting";
        }

        public void LogsWrite(string Type, string _logs)
        {
            bt_Logs.Text += Type + " " + _logs + Environment.NewLine;
        }
    }
}