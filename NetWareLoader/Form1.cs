using System;
using System.Net;
using Microsoft.Win32;
using System.Windows.Forms;
using SharpMonoInjector;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Diagnostics.Eventing.Reader;

namespace NetWareLoader
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //Doesn't exist
        }

        private void siticoneButton1_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void siticoneButton1_Click_2(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void siticoneButton2_Click(object sender, EventArgs e)
        {
            string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string lcfp = Path.Combine(documents, "netwareDownloader");
            Directory.CreateDirectory(lcfp);

            string lcfpf = "https://github.com/waxnet/NetWare/raw/main/.build/NetWare.dll";
            string lcfpfpf = Path.Combine(lcfp, "NetWare.dll");

            using (var client = new WebClient())
            {
                client.DownloadFile(lcfpf, lcfpfpf);
            }
            byte[] assemblyBytes = File.ReadAllBytes(lcfpfpf);
            Injector gameInjector = new("1v1_LOL");
            IntPtr hasInjected = gameInjector.Inject(
                File.ReadAllBytes(lcfpfpf),
                "NetWare",
                "Loader",
                "Load"
            );
            gameInjector.Dispose();
        }

        //UNBAN
        private void siticoneButton3_Click(object sender, EventArgs e)
        {
            string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string setBypassed = Path.Combine(documents, "netwareDownloader");
            //string PlayKey = @"HKEY_USERS\S-1-5-21-4125990421-3961550520-2386719630-1000\Software\JustPlay.LOL";
            string PlayDir = @"Software\JustPlay.LOL\1v1.LOL";
            RegistryKey regKey = Registry.CurrentUser;
            RegistryKey subKey = regKey.OpenSubKey(PlayDir);
            if (subKey != null)
            {
                regKey.DeleteSubKeyTree(PlayDir);
            }
            else { Console.WriteLine("Already gone."); }

            string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string localPlay = Path.Combine(userPath, "AppData\\LocalLow\\JustPlay_LOL");

            if (Directory.Exists(localPlay))
            {
                Directory.Delete(localPlay, true);
            }
            else { Console.WriteLine("Already gone."); }

            MessageBox.Show("Finished!");
        }

        private void siticoneButton4_Click(object sender, EventArgs e)
        {
            //Get the steam path
            string steamPath = (string)Registry.GetValue(
                "HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\VALVE\\STEAM",
                "InstallPath",
                null);
            string loldll = Path.Combine(steamPath, "steamapps", "common", "1v1.LOL", "1v1_LOL_Data", "Managed", "1v1.dll");
            string ACTkDLL = Path.Combine(steamPath, "steamapps", "common", "1v1.LOL", "1v1_LOL_Data", "Managed", "ACTk.Runtime.dll");
            Console.WriteLine(loldll);

            //Check whether you have the patched version or not
            using (var sha256 = SHA256.Create())
            {
                using (var stream = File.OpenRead(loldll))
                {
                    byte[] hash = sha256.ComputeHash(stream);
                    string hashString = BitConverter.ToString(hash).Replace("-", string.Empty);
                    Console.WriteLine(hashString);
                    if (hashString != "EDBC43FA7BA5B74F3D83F7C9A556E39DB02F66B84FACC643F4D3176BE4CC2264")
                    {

                        //Documents folder
                        string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        string lolTempInDocs = Path.Combine(documents, "netwareLoader");
                        string lolPatched = "https://simplicated.cc/1v1.dll";

                        string ACTkRuntime = "https://simplicated.cc/ACTk.Runtime.dll";

                        Directory.CreateDirectory(lolTempInDocs);
                        string ARTD = Path.Combine(lolTempInDocs, "ACTk.Runtime.dll");
                        string lolPFIK = Path.Combine(lolTempInDocs, "1v1.dll");
                        using (var client = new WebClient())
                        {

                            client.DownloadFile(lolPatched, lolPFIK);
                            stream.Close();
                            client.DownloadFile(ACTkRuntime, ARTD);
                            stream.Close();

                            File.Copy(lolPFIK, loldll, true);
                            stream.Close();
                            File.Copy(ARTD, ACTkDLL, true);
                            stream.Close();
                            MessageBox.Show("Patched 1v1.lol");
                        }
                    }
                    //I think a practically useless failsafe but ok
                    else
                    {
                        MessageBox.Show("1v1.lol has already been patched");
                    }
                }
            }
        }

        private void siticoneButton5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://simplicated.cc/contact/alternative.html");
        }
        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            //Doesn't exist
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //Doesn't exist
        }
        private void label2_Click(object sender, EventArgs e)
        {
            //Doesn't exist
        }
    }
}