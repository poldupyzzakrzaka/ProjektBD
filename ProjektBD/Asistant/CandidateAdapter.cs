using System.Collections.Generic;
using ProjektBD.Database;
using MySql.Data.MySqlClient;

namespace ProjektBD.Asistant
{
    class CandidateAdapter
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public decimal Pesel { get; set; }
        public string Sex { get; set; }
        private int ID { get; set; }

        private string searchCommand;

        private List<CandidateAdapter> list = null;

        public int GetID()
        {
            return ID;
        }

        public void SearchCommand()
        {
            searchCommand = "SELECT id, name, surname, city, pesel, sex FROM candidates";
        }

        public void SearchCommand(string name, string surname, string city, string sex, string pesel)
        {
            searchCommand = "SELECT id, name, surname, city, pesel, sex FROM candidates WHERE";
            if (name.Length != 0)
                searchCommand += " name LIKE '%" + name + "%' AND";
            if (surname.Length != 0)
                searchCommand += " surname LIKE '%" + surname + "%' AND";
            if (city.Length != 0)
                searchCommand += " city LIKE '%" + city + "%' AND";
            if (sex.Length != 0)
                searchCommand += " sex LIKE '%" + sex + "%' AND";
            if (pesel.Length != 0)
                searchCommand += " pesel LIKE '%" + pesel + "%' AND";
            if (searchCommand.EndsWith("AND"))
                searchCommand = searchCommand.Substring(0, searchCommand.Length - 4);
            else
                searchCommand = searchCommand.Substring(0, searchCommand.Length - 6);
        }

        public void CreateList()
        {
            list = new List<CandidateAdapter>();
            MySqlCommand command = DBConnection.Instance.Conn.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = searchCommand;
            DBConnection.Instance.Conn.Open();
            Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                CandidateAdapter can = new CandidateAdapter();
                can.ID = Reader.GetInt32(0);
                can.Name = Reader.GetString(1);
                can.Surname = Reader.GetString(2);
                can.City = Reader.GetString(3);
                can.Pesel = Reader.GetDecimal(4);
                can.Sex = Reader.GetString(5);
                list.Add(can);
            }
            DBConnection.Instance.Conn.Close();
        }

        public List<CandidateAdapter> GetList()
        {
            return list;
        }

    }
}
