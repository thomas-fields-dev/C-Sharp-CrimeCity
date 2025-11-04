using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeCity.Classes
{
    public static class Billboard
    {
        public static void NiteCityBillboard()
        {
            Console.WriteLine("Welcome To Trapper Town!");
        }

        public static void EdgeRunnerBillboard(this EdgeRunner edgeRunner)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(edgeRunner);
            sb.Append("\n");
            sb.Append($"Hire {edgeRunner.FirstName} {edgeRunner.LastName} for {edgeRunner.Price}$$");
            sb.Append("\n");
            Console.WriteLine(sb.ToString());
        }

        public static void NewInTown()
        {
            Console.WriteLine("New in town huh?");
        }
    }

}
