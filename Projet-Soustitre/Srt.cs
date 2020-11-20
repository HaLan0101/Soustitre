using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Text.RegularExpressions;

namespace Projet_Soustitre
{
    class Srt
    {
        public int Number;
        public TimeSpan TimeStart;
        public TimeSpan TimeEnd;
        public string Message;
        public Srt(int number, string time, string message)
        {
            Number = number;
            Message = message;
            ParseTime(time);
        }
        private void ParseTime(string time)
        {
            Regex r = new Regex(@"\d{2}:\d{2}:\d{1,2},\d{3} ?--> ?\d{2}:\d{2}:\d{1,2},\d{3}");
            if (r.IsMatch(time))
            {
                string[] split = { "-->", "-->" };
                string[] timeSplit = time.Split(split, StringSplitOptions.None);
                TimeStart = GetTime(timeSplit[0]);
                TimeEnd = GetTime(timeSplit[1]);

            }
        }
        public TimeSpan GetTime(string time)
        {
            char[] charSplit = { ':', ',' };
            string[] timeSplit = time.Split(charSplit);
            return new TimeSpan(0, Int32.Parse(timeSplit[0]), Int32.Parse(timeSplit[1]), Int32.Parse(timeSplit[2]), Int32.Parse(timeSplit[3]));
        }
        public async Task AddMessage(MainWindow main)
        {
 
            await AddTime();
            main.Update_Text(Message);
            await DisplayTime();
            main.Update_Text(null);
        }
        public async Task AddTime()
        {
            await Task.Delay(TimeStart);
        }
        public async Task DisplayTime()
        {
            await Task.Delay(TimeEnd - TimeStart);
        }
    }
}
