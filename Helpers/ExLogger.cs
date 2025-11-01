using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeCity.Helpers
{
    public static class ExLogger
    {
        private static List<string> Message { get; set; } = new List<string>();
        public static void Log(string value = "", bool newLine = false)
        {
            if (newLine)
                Message.Add(value + "\n");
            else
                Message.Add(value + " ");
        }

        public static void Display()
        {
            string message_ = "";
            foreach (string m in Message)
            {
                message_ += m;
            }
            Console.WriteLine(message_);
        }
        public static void Clear()
        {
            Message.Clear();
        }
    }

}
