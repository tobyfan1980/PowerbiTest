using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PowerbiTest
{
    class Program
    {
        static void Main(string[] args)
        {
            PowerbiEmbed pbe = new PowerbiEmbed("toby-test");

            pbe.GetWorkSpace("toby");
            pbe.GetImportsInWorkspace();
            pbe.GetReports();
            pbe.GetEmbedToken();

            Console.ReadLine();
        }
    }
}
