using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;

namespace Fivem_Launcher
{
    public partial class Dashboard : Form
    {
        public string ipSRV = "localhost:30120";

        public Dashboard()
        {
            InitializeComponent();
        }

        public void Alert(string msg, Form_Alert.enmType type)
        {
            Form_Alert frm = new Form_Alert();
            frm.showAlert(msg, type);
        }

        private string responseFromServer;
        private string status;
        private string svCon;
        bool ipServ = false;
        public void ServerUpdate()
        {

            svCon = "http://" + ipSRV + "/players.json";
            WebRequest request = WebRequest.Create(svCon);
            request.Credentials = CredentialCache.DefaultCredentials;
            ipServ = isExist(svCon);
            if (ipServ)
            {
                WebResponse response = request.GetResponse();
                status = ((HttpWebResponse)response).StatusDescription;
                Console.WriteLine(status);

                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    responseFromServer = reader.ReadToEnd();
                    Console.WriteLine(responseFromServer);


                }


                response.Close();
                label18.Text = "Online";
                label18.ForeColor = System.Drawing.Color.Green;
                Player[] item = JsonConvert.DeserializeObject<Player[]>(responseFromServer);
                var countPlayers = item.Count();
                label26.Text = countPlayers.ToString() + " Connected Players";
                label26.Size = new Size(496, 25);

            }
            else
            {
                label18.Text = "Offline";
                label18.ForeColor = System.Drawing.Color.Red;
                label26.Text = "Server is Offline";
                label4.Visible = true;
            }

        }

        public class Player
        {
            public string endpoint { get; set; }
            public int id { get; set; }
            public string[] identifiers { get; set; }
            public string name { get; set; }
            public int ping { get; set; }
        }

        private bool isExist(string ipSRV)
        {
            WebRequest webRequest = HttpWebRequest.Create(svCon);
            webRequest.Method = "HEAD";
            try
            {
                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    return true;
                }
            }
            catch //(WebException ex)
            {
                return false;
            }
        }
        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ServerUpdate();

            PanelChangelog.Visible = false;
            PanelCache.Visible = false;
            PanelHome.Visible = true;
        }

        private void guna2CircleButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            Process[] steam = Process.GetProcessesByName("steam");
            if (steam.Length == 0)
            {
                Thread.Sleep(500);
                this.Alert("Steam is not running or you are not Logged in.", Form_Alert.enmType.Error); return;
            }
            svCon = "http://" + ipSRV + "/players.json";
            WebRequest request = WebRequest.Create(svCon);
            request.Credentials = CredentialCache.DefaultCredentials;
            ipServ = isExist(svCon);
            if (ipServ)
            {
                WebResponse response = request.GetResponse();
                status = ((HttpWebResponse)response).StatusDescription;
                Console.WriteLine(status);

                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    responseFromServer = reader.ReadToEnd();
                    Console.WriteLine(responseFromServer);


                }


                response.Close();
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C Start fivem://connect/" + ipSRV;
                this.Alert("Launching Fivem Now", Form_Alert.enmType.Success);
                process.StartInfo = startInfo;
                process.Start();
            }
            else
            {
                this.Alert("Sorry, our server is currently offline", Form_Alert.enmType.Error); return;
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void lblWarningSteam_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            PanelChangelog.Visible = true;
            PanelHome.Visible = false;
            PanelCache.Visible = false;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            PanelHome.Visible = true;
            PanelChangelog.Visible = false;
            PanelCache.Visible = false;
        }

        private void PanelHome_Paint(object sender, PaintEventArgs e)
        {

        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = LabelLocation.Text;
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                LabelLocation.Text = dialog.FileName;
            }
            Properties.Settings.Default["fivemdir"] = LabelLocation.Text;
            Properties.Settings.Default.Save();
            this.Alert("You have set your fivem directory", Form_Alert.enmType.Success);
        }

        private void guna2Button15_Click(object sender, EventArgs e)
        {

            string path1 = Properties.Settings.Default["fivemdir"].ToString() + "\\cache\\authbrowser";
            string path2 = Properties.Settings.Default["fivemdir"].ToString() + "\\cache\\browser";
            string path3 = Properties.Settings.Default["fivemdir"].ToString() + "\\cache\\browser-fxdk";
            string path4 = Properties.Settings.Default["fivemdir"].ToString() + "\\cache\\db";
            string path5 = Properties.Settings.Default["fivemdir"].ToString() + "\\cache\\priv";
            string path6 = Properties.Settings.Default["fivemdir"].ToString() + "\\cache\\servers";
            string path7 = Properties.Settings.Default["fivemdir"].ToString() + "\\cache\\subprocess";
            string path8 = Properties.Settings.Default["fivemdir"].ToString() + "\\cache\\unconfirmed";
            string path9 = Properties.Settings.Default["fivemdir"].ToString() + "\\cache\\launcher_skip_mtl2";
            string path10 = Properties.Settings.Default["fivemdir"].ToString() + "\\cache\\session";

            if (Directory.Exists(path1))
            {
                Directory.Delete(path1, true);
            }
            else if (Directory.Exists(path2))
            {
                Directory.Delete(path2, true);
            }
            else if (Directory.Exists(path3))
            {
                Directory.Delete(path3, true);
            }
            else if (Directory.Exists(path4))
            {
                Directory.Delete(path4, true);
            }
            else if (Directory.Exists(path5))
            {
                Directory.Delete(path5, true);
            }
            else if (Directory.Exists(path6))
            {
                Directory.Delete(path6, true);
            }
            else if (Directory.Exists(path7))
            {
                Directory.Delete(path7, true);
            }
            else if (Directory.Exists(path8))
            {
                Directory.Delete(path8, true);
            }
            else if (Directory.Exists(path9))
            {
                File.Delete(path9);
            }
            else if (Directory.Exists(path10))
            {
                File.Delete(path10);
            }
            else
            {
                this.Alert("Failed to clear your Fivem Cache", Form_Alert.enmType.Error); return;
            }
            this.Alert("Cleared your Fivem Cache", Form_Alert.enmType.Success);

        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            PanelCache.Visible = true;
            PanelHome.Visible = false;
            PanelChangelog.Visible = false;
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {

        }

        private void SuccessCache_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label46_Click(object sender, EventArgs e)
        {

        }

        private void lblSuccessOnline_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button16_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
            this.Alert("Reloaded Changelog", Form_Alert.enmType.Success);
        }

        private void guna2Button13_Click(object sender, EventArgs e)
        {
            Process.Start("http://google.com");
        }
    }
}
