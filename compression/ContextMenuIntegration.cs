using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace compression
{
    public static class ContextMenuIntegration
    {
        public static void AddContextMenu(string exePath)
        {
            try
            {
                using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(@"*\shell\khdضغط بـ"))
                {
                    key.SetValue("", "khdضغط بـ");
                    using (RegistryKey command = key.CreateSubKey("command"))
                    {
                        command.SetValue("", $"\"{exePath}\" \"%1\"");
                    }
                }

                using (RegistryKey dirKey = Registry.ClassesRoot.CreateSubKey(@"Directory\shell\khdضغط بـ"))
                {
                    dirKey.SetValue("", "khdضغط بـ");
                    using (RegistryKey command = dirKey.CreateSubKey("command"))
                    {
                        command.SetValue("", $"\"{exePath}\" \"%1\"");
                    }
                }

                using (RegistryKey cmpFileKey = Registry.ClassesRoot.CreateSubKey(@"SystemFileAssociations\.cmp\shell\khdفك الضغط بـ"))
                {
                    cmpFileKey.SetValue("", "khdفك الضغط بـ");
                    using (RegistryKey command = cmpFileKey.CreateSubKey("command"))
                    {
                        command.SetValue("", $"\"{exePath}\" --extract \"%1\"");
                    }
                }


                MessageBox.Show("تمت إضافة الأوامر إلى كليك يمين للملفات والمجلدات.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في إضافة كليك يمين: " + ex.Message);
            }
        }

        public static void RemoveContextMenu()
        {
            try
            {
                Registry.ClassesRoot.DeleteSubKeyTree(@"*\shell\khdضغط بـ", false);
                Registry.ClassesRoot.DeleteSubKeyTree(@"Directory\shell\khdضغط بـ", false);

                Registry.ClassesRoot.DeleteSubKeyTree(@".cmp\shell\khdفك الضغط بـ", false);
                MessageBox.Show("تمت إزالة الأوامر من كليك يمين للملفات والمجلدات.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في إزالة كليك يمين: " + ex.Message);
            }
        }
    }
}
