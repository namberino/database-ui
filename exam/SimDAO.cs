using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace exam
{
    internal class SimDAO
    {
        string conString = "Data Source=DESKTOP-J7AVQIK\\SQLEXPRESS;Initial Catalog=SimThe;Integrated Security=True;TrustServerCertificate=True";

        public SqlConnection connectToDB()
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            return con;
        }

        public string SafeGetString(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return "NULL";
        }

        public List<Sim> DocListSim()
        {
            List<Sim> sims = new List<Sim>();
            SqlConnection con = connectToDB();

            SqlCommand cmd = new SqlCommand("select * from Sim", con);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Sim sim = new Sim()
                {
                    simID = this.SafeGetString(reader, 0),
                    soSim = this.SafeGetString(reader, 1),
                    ngayKickHoat = this.SafeGetString(reader, 2),
                    ngayHetHan = this.SafeGetString(reader, 3),
                    khuyenMai = this.SafeGetString(reader, 4)
                };

                sims.Add(sim);
            }

            con.Close();

            return sims;
        }

        public List<Sim> duyenListSim(string ngayHetHan, string khuyenMai)
        {
            List<Sim> sims = new List<Sim>();
            SqlConnection con = connectToDB();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Sim where NgayHetHan = @expire and KhuyenMai = @coup";
            cmd.Parameters.AddWithValue("@expire", ngayHetHan);
            cmd.Parameters.AddWithValue("@coup", khuyenMai);
            cmd.Connection = con;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Sim sim = new Sim()
                {
                    simID = this.SafeGetString(reader, 0),
                    soSim = this.SafeGetString(reader, 1),
                    ngayKickHoat = this.SafeGetString(reader, 2),
                    ngayHetHan = this.SafeGetString(reader, 3),
                    khuyenMai = this.SafeGetString(reader, 4)
                };

                sims.Add(sim);
            }

            con.Close();

            return sims;
        }

        public void Delete(string soSim)
        {
            SqlConnection con = connectToDB();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "delete from Sim where SoSim = @so";
            cmd.Parameters.AddWithValue("@so", soSim);
            cmd.Connection = con;

            cmd.ExecuteNonQuery();
            con.Close();
        }

        public List<Sim> TimKiem(string soSim) 
        {
            List<Sim> sims = new List<Sim>();
            SqlConnection con = connectToDB();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Sim where SoSim = @so";
            cmd.Parameters.AddWithValue("@so", soSim);
            cmd.Connection = con;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Sim sim = new Sim()
                {
                    simID = this.SafeGetString(reader, 0),
                    soSim = this.SafeGetString(reader, 1),
                    ngayKickHoat = this.SafeGetString(reader, 2),
                    ngayHetHan = this.SafeGetString(reader, 3),
                    khuyenMai = this.SafeGetString(reader, 4)
                };

                sims.Add(sim);
            }

            con.Close();

            return sims;
        }

        public void ChuyenCSV()
        {
            List<Sim> sims = this.DocListSim();

            using (StreamWriter sw = new StreamWriter("C:\\Users\\namnb\\Projects\\exam\\exam\\sim.csv"))
            {
                sw.WriteLine("SimID,SoSim,NgayKichHoat,NgayHetHan,KhuyenMai");

                foreach (Sim sim in sims)
                {
                    string outty = string.Format("{0},{1},{2},{3},{4}", sim.simID, sim.soSim, sim.ngayKickHoat, sim.ngayHetHan, sim.khuyenMai);
                    sw.WriteLine(outty);
                }
            }
        }

        public int DemSoSim()
        {
            Dictionary<string, string[]> dict = new Dictionary<string, string[]>();
            int count = 0;
            SqlConnection con = connectToDB();

            SqlCommand cmd = new SqlCommand("select * from Sim", con);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                
                Sim sim = new Sim()
                {
                    simID = this.SafeGetString(reader, 0),
                    soSim = this.SafeGetString(reader, 1),
                    ngayKickHoat = this.SafeGetString(reader, 2),
                    ngayHetHan = this.SafeGetString(reader, 3),
                    khuyenMai = this.SafeGetString(reader, 4)
                };

                string[] values = {sim.soSim, sim.ngayKickHoat, sim.ngayHetHan, sim.khuyenMai};
                dict.Add(sim.simID, values);
            }

            con.Close();

            foreach (var item in dict.Values)
            {
                if (item[2].ToString() == "20/10/2023")
                {
                    count++;
                }
            }

            return count;
        }
    }
}
