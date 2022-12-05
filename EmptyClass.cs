using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace francyzskayaPracticheskaya
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string nashaPapka = "\\";
            Kursor kursor = new Kursor();
            List<Failik> papkaList = new List<Failik>();
            while (true)
            {
                Console.SetCursorPosition(0, 0);
                kursor.max = 0;
                Console.WriteLine("Проводник.ехе. Текущий путь - " + nashaPapka);
                if (nashaPapka == "\\")
                {
                    papkaList = new List<Failik>();
                    DriveInfo[] allDrives = DriveInfo.GetDrives();
                    foreach (DriveInfo drive in allDrives)
                    {
                        double totalSpace = drive.TotalSize;
                        double availableSpace = drive.AvailableFreeSpace;
                        Failik elem = new Failik($"Диск {drive.Name} | Размер {totalSpace} гб | Доступно {availableSpace} гб", drive.Name);
                        papkaList.Add(elem);
                    }
                }
                else
                {
                    try
                    {
                        papkaList = GetFiles.Get(nashaPapka);
                    }
                    catch
                    {

                    }

                }
                ShowInformation.ShowDirs(papkaList);
                kursor.max = papkaList.Count;
                Console.SetCursorPosition(0, kursor.pos);
                Console.Write(">>");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Enter:
                        if (Directory.Exists(papkaList[kursor.pos - kursor.min].path))
                        {
                            nashaPapka = papkaList[kursor.pos - kursor.min].path;
                        }
                        else
                        {
                            Process.Start(papkaList[kursor.pos - kursor.min].path);
                        }
                        Console.Clear();
                        break;
                    case ConsoleKey.DownArrow:
                        if (kursor.pos == kursor.max)
                            kursor.pos = kursor.min;
                        else
                            kursor.pos++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (kursor.pos == kursor.min)
                            kursor.pos = kursor.max;
                        else
                            kursor.pos--;
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}