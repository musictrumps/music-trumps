using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TrumpEngine.Model;

namespace TrumpEngine.Data
{
    public class BandData
    {
        public void Insert(Band band)
        {
            try
            {
                using (var connection = new SqliteConnection("Data Source=C:\\Users\\thiago.DESKTOP-TT0ETA0\\source\\repos\\music-trumps\\TrumpEngine.Api\\trumpdata.db"))
                {
                    connection.Open();
                    SqliteCommand command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO band (Name, Picture, Summary, Begin) VALUES (@Name,@Picture,@Summary,@Begin)";
                    command.Parameters.AddWithValue("Name", band.Name);
                    command.Parameters.AddWithValue("Picture", band.Picture);
                    command.Parameters.AddWithValue("Summary", band.Summary);
                    command.Parameters.AddWithValue("Begin", band.Begin.ToShortDateString());
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Band> FindAll()
        {
            List<Band> bands = new List<Band>();

            try
            {
                using (var connection = new SqliteConnection("Data Source=C:\\Users\\thiago.DESKTOP-TT0ETA0\\source\\repos\\music-trumps\\TrumpEngine.Api\\trumpdata.db"))
                {
                    connection.Open();
                    SqliteCommand command = connection.CreateCommand();
                    command.CommandText = "SELECT Name, Picture, Summary, Begin FROM band where begin != '1/1/0001'";
                    IDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        Band band = new Band();
                        band.Name = Convert.ToString(dr["Name"]);
                        band.Picture = Convert.ToString(dr["Picture"]);
                        band.Summary = Convert.ToString(dr["Summary"]);
                        band.Begin = Convert.ToDateTime(dr["Begin"]);
                        bands.Add(band);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bands;
        }
    }
}
