using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Soustitre
{
    class FichierSoustitre
    {
        public enum Type { Number, Time, Message, Empty }
        public List<String> Lines;
        public List<Srt> Messages;
        public FichierSoustitre()
        {

        }
        public void ReadFile()
        {
            string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            using (StreamReader sr = new StreamReader(mydocpath + @"\Mulan2020.srt"))
            {
                Lines = new List<string>();
                string Line;

                while ((Line = sr.ReadLine()) != null)
                {
                    Lines.Add(Line);
                }

                ParseFile();
            }
        }
        public void ParseFile()
        {
            int numbers = 0;
            string times = "";
            string messages = "";
            Type num = Type.Number;
            Messages = new List<Srt>();
            foreach (string line in Lines)
            {
                if (line == "")
                {
                    num = Type.Empty;
                }
                switch (num)
                {
                    case Type.Number:
                        numbers = Int32.Parse(line);
                        num++;
                        break;
                    case Type.Time:
                        times = line;
                        num++;
                        break;
                    case Type.Message:
                        messages += line + "\n";
                        break;
                    case Type.Empty:
                        Srt srt = new Srt(numbers, times, messages);
                        if (!Messages.Contains(srt))
                        {
                            Messages.Add(srt);
                        }
                        num = Type.Number;
                        messages = "";
                        break;

                }
            }
        }
        public async Task GetSrt(MainWindow main)
        {
            foreach (Srt line in Messages)
            {
                Console.WriteLine("test");
                Task r = line.AddMessage(main);
                await r;
            }
        }
    }
}
