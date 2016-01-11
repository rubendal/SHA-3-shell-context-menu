using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHA_3
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length == 3)
            {
                //args[0] = path
                //args[1] = bits
                //args[2] = cal/ver
                int i = 0;
                if (args[2] == "cal")
                {
                    if (int.TryParse(args[1], out i))
                    {
                        Application.Run(new Calculate(args[0], i));
                    }
                }
                else
                {
                    if (args[2] == "ver")
                    {
                        if (int.TryParse(args[1], out i))
                        {
                            Application.Run(new Verify(args[0], i));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Unknown command");
                    }
                }
            }
            else
            {
                RegistryKey rkey = Registry.ClassesRoot.OpenSubKey("*\\shell\\SHA-3");
                if (rkey == null)
                {
                    RegistryKey key = Registry.ClassesRoot.OpenSubKey("*\\shell", true);
                    key.CreateSubKey("SHA-3");
                    key.Close();
                    RegistryKey skey = Registry.ClassesRoot.OpenSubKey("*\\shell\\SHA-3", true);
                    skey.SetValue("MUIVerb", "SHA-3", RegistryValueKind.String);
                    skey.SetValue("SubCommands", "", RegistryValueKind.String);
                    skey.CreateSubKey("shell");
                    skey.Close();
                    RegistryKey subkey = Registry.ClassesRoot.OpenSubKey("*\\shell\\SHA-3\\shell", true);
                    subkey.CreateSubKey("Calculate SHA-3 224 bits");
                    subkey.CreateSubKey("Calculate SHA-3 256 bits");
                    subkey.CreateSubKey("Calculate SHA-3 384 bits");
                    subkey.CreateSubKey("Calculate SHA-3 512 bits");
                    subkey.CreateSubKey("Verify SHA-3 224 bits");
                    subkey.CreateSubKey("Verify SHA-3 256 bits");
                    subkey.CreateSubKey("Verify SHA-3 384 bits");
                    subkey.CreateSubKey("Verify SHA-3 512 bits");
                    subkey.Close();
                    int[] bits = new int[] { 224, 256, 384, 512 };
                    foreach (int b in bits)
                    {
                        RegistryKey submenu = Registry.ClassesRoot.OpenSubKey("*\\shell\\SHA-3\\shell\\Calculate SHA-3 " + b + " bits", true);
                        submenu.SetValue("MUIVerb", "Calculate SHA-3 " + b + " bits");
                        submenu.CreateSubKey("command");
                        submenu.Close();
                        RegistryKey submenuc = Registry.ClassesRoot.OpenSubKey("*\\shell\\SHA-3\\shell\\Calculate SHA-3 " + b + " bits\\command", true);
                        submenuc.SetValue("", System.Reflection.Assembly.GetEntryAssembly().Location + " \"%1\" " + b + " cal");
                        submenuc.Close();
                    }
                    foreach (int b in bits)
                    {
                        RegistryKey submenu = Registry.ClassesRoot.OpenSubKey("*\\shell\\SHA-3\\shell\\Verify SHA-3 " + b + " bits", true);
                        submenu.SetValue("MUIVerb", "Verify SHA-3 " + b + " bits");
                        submenu.CreateSubKey("command");
                        submenu.Close();
                        RegistryKey submenuc = Registry.ClassesRoot.OpenSubKey("*\\shell\\SHA-3\\shell\\Verify SHA-3 " + b + " bits\\command", true);
                        submenuc.SetValue("", System.Reflection.Assembly.GetEntryAssembly().Location + " \"%1\" " + b + " ver");
                        submenuc.Close();
                    }
                    MessageBox.Show("Installed! Right click a file to calculate and verify SHA-3 hashes");
                }
                else
                {
                    rkey.Close();
                    MessageBox.Show("Right click a file to calculate and verify SHA-3 hashes");
                }
                
            }
        }
    }
}
