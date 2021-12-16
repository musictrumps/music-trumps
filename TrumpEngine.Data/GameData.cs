using Microsoft.Data.Sqlite;
using System;
using System.Data;
using TrumpEngine.Model;

namespace TrumpEngine.Data
{
    public class GameData
    {
        public void Insert(Game game)
        {
            try
            {
                using (var connection = new SqliteConnection("Data Source=C:\\Users\\thiago.DESKTOP-TT0ETA0\\source\\repos\\music-trumps\\TrumpEngine.Api\\trumpdata.db"))
                {
                    connection.Open();
                    SqliteCommand command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO game (UUID,Player1,Player2,Player1_Cards,Player2_Cards,Player1_Points,Player2_Points,Player1_Turn,Player2_Turn,Player1_CurrentBand,Player2_CurrentBand) VALUES (@UUID,@Player1,@Player2,@Player1_Cards,@Player2_Cards,@Player1_Points,@Player2_Points,@Player1_Turn,@Player2_Turn,@Player1_CurrentBand,@Player2_CurrentBand)";
                    command.Parameters.AddWithValue("UUID", game.UUID);
                    command.Parameters.AddWithValue("Player1", game.Player1);
                    command.Parameters.AddWithValue("Player2", game.Player2);
                    command.Parameters.AddWithValue("Player1_Cards", game.Player1_Cards);
                    command.Parameters.AddWithValue("Player2_Cards", game.Player2_Cards);
                    command.Parameters.AddWithValue("Player1_Points", game.Player1_Points);
                    command.Parameters.AddWithValue("Player2_Points", game.Player2_Points);
                    command.Parameters.AddWithValue("Player1_Turn", game.Player1_Turn);
                    command.Parameters.AddWithValue("Player2_Turn", game.Player2_Turn);
                    command.Parameters.AddWithValue("Player1_CurrentBand", game.Player1_CurrentBand);
                    command.Parameters.AddWithValue("Player2_CurrentBand", game.Player2_CurrentBand);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Game FindByUUID(string UUID)
        {
            Game game = null;

            try
            {
                using (var connection = new SqliteConnection("Data Source=C:\\Users\\thiago.DESKTOP-TT0ETA0\\source\\repos\\music-trumps\\TrumpEngine.Api\\trumpdata.db"))
                {
                    connection.Open();
                    SqliteCommand command = connection.CreateCommand();
                    command.CommandText = "SELECT UUID, Player1, Player2, Player1_Cards, Player2_Cards, Player1_Points, Player2_Points, Player1_Turn, Player2_Turn, Player1_CurrentBand, Player2_CurrentBand FROM game WHERE UUID=@UUID";
                    command.Parameters.AddWithValue("UUID", UUID);
                    IDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        game = new Game
                        {
                            UUID = Convert.ToString(dr["UUID"]),
                            Player1 = Convert.ToString(dr["Player1"]),
                            Player2 = Convert.ToString(dr["Player2"]),
                            Player1_Cards = Convert.ToString(dr["Player1_Cards"]),
                            Player2_Cards = Convert.ToString(dr["Player2_Cards"]),
                            Player1_Points = dr["Player1_Points"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Player1_Points"]),
                            Player2_Points = dr["Player2_Points"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Player2_Points"]),
                            Player1_Turn = dr["Player1_Turn"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Player1_Turn"]),
                            Player2_Turn = dr["Player2_Turn"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Player2_Turn"]),
                            Player1_CurrentBand = Convert.ToString(dr["Player1_CurrentBand"]),
                            Player2_CurrentBand = Convert.ToString(dr["Player2_CurrentBand"])
                        };
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return game;
        }

        public void Update(Game game)
        {
            try
            {
                using (var connection = new SqliteConnection("Data Source=C:\\Users\\thiago.DESKTOP-TT0ETA0\\source\\repos\\music-trumps\\TrumpEngine.Api\\trumpdata.db"))
                {
                    connection.Open();
                    SqliteCommand command = connection.CreateCommand();
                    command.CommandText = "UPDATE game SET Player1=@Player1,Player2=@Player2,Player1_Cards=@Player1_Cards,Player2_Cards=@Player2_Cards,Player1_Points=@Player1_Points,Player2_Points=@Player2_Points,Player1_Turn=@Player1_Turn,Player2_Turn=@Player2_Turn,Player1_CurrentBand=@Player1_CurrentBand,Player2_CurrentBand=@Player2_CurrentBand WHERE UUID = @UUID";
                    command.Parameters.AddWithValue("UUID", game.UUID);
                    command.Parameters.AddWithValue("Player1", game.Player1);
                    command.Parameters.AddWithValue("Player2", game.Player2);
                    command.Parameters.AddWithValue("Player1_Cards", game.Player1_Cards);
                    command.Parameters.AddWithValue("Player2_Cards", game.Player2_Cards);
                    command.Parameters.AddWithValue("Player1_Points", game.Player1_Points);
                    command.Parameters.AddWithValue("Player2_Points", game.Player2_Points);
                    command.Parameters.AddWithValue("Player1_Turn", game.Player1_Turn);
                    command.Parameters.AddWithValue("Player2_Turn", game.Player2_Turn);
                    command.Parameters.AddWithValue("Player1_CurrentBand", game.Player1_CurrentBand);
                    command.Parameters.AddWithValue("Player2_CurrentBand", game.Player2_CurrentBand);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
