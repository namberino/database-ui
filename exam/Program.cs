using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace exam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            SimDAO simDAO = new SimDAO();
            List<Sim> sims = simDAO.duyenListSim("20/3/2025", "x");

            foreach (Sim sim in sims)
            {
                string outty = sim.simID + " " + sim.soSim + " " + sim.ngayKickHoat + " " + sim.ngayHetHan + " " + sim.khuyenMai;
                Console.WriteLine(outty);
            }

            int count = simDAO.DemSoSim();
            Console.WriteLine("Số sim hết hạn ngày 20/10/2023: " + count);

            Console.ReadLine();

            Application.Run(new Form1());
        }
    }
}
