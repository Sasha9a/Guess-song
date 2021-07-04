using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;

namespace Угадайка
{
    static class Victorina
    {
        static public List<string> list = new List<string>();
        static public int gameDuration = 60;
        static public int musicDuration = 10;
        static public bool randomStart = false;
        static public string lastFolder = "";
        static public bool AllDirectories = false;
        static public string answer = "";
        static public void ReadMusic()
        {
            try
            {
                string[] music_files = Directory.GetFiles(lastFolder, "*.mp3", AllDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
                list.Clear();
                list.AddRange(music_files);
            }
            catch
            {
            }
        }
        static string  regKeyName = "Software\\MyProgect\\GuessMelody";
        public static void WriteParam()
        {
            RegistryKey rk = null;
            try
            {
                rk = Registry.CurrentUser.CreateSubKey(regKeyName);
                if (rk == null) return;
                rk.SetValue("lastFolder", lastFolder);
                rk.SetValue("randomStart", randomStart);
                rk.SetValue("gameDuration", gameDuration);
                rk.SetValue("musicDuration", musicDuration);
                rk.SetValue("AllDirectories", AllDirectories);
            }
            finally
            {
                if (rk != null) rk.Close();
            }
        }
        public static void ReadParam()
        {
            RegistryKey rk = null;
            try
            {
                rk = Registry.CurrentUser.OpenSubKey(regKeyName);
                if(rk != null)
                {
                    lastFolder = (string)rk.GetValue("lastFolder");
                    randomStart = Convert.ToBoolean(rk.GetValue("randomStart",false));
                    gameDuration = (int)rk.GetValue("gameDuration");
                    musicDuration = (int)rk.GetValue("musicDuration");
                    AllDirectories = Convert.ToBoolean(rk.GetValue("AllDirectories", false));
                }
            }
            finally
            {
                if (rk != null) rk.Close();
            }
        }
    }
}
