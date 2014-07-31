using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using abstractor;

namespace bikini
{
    class Program
    {
        static void Main(string[] args)
        {
            Wlan_Abstractor myAbstractor = new Wlan_Abstractor();
            Console.WriteLine("\n**have myAbstractor**");
            myAbstractor.configureHardware("RIO0", 1, 0.001, 20000000.0, false, true, true, false, true, 2412000000.0, -20.00);
            Console.WriteLine("\n**have hardware configuration**");
            myAbstractor.initiate();
            Console.WriteLine("\n**hardware initiated**\n");
            myAbstractor.takeMeasurements();
            Console.WriteLine("\n**Measurements have been taken**\n");
            myAbstractor.getmeasurement("all");
            Console.WriteLine("\n**Measurements have been returned**\n");
            myAbstractor.closeReferences();
            Console.WriteLine("\n**Sessions have been closed**\n");
        }
    }
}
