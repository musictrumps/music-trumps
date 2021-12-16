using System;
using TrumpEngine.Data;
using TrumpEngine.Model;

namespace TrumpEngine.Business
{
    public class GameBusiness
    {
        private readonly GameData _data;

        public GameBusiness()
        {
            _data = new GameData();
        }

        public void Insert(Game game)
        {
            try
            {
                _data.Insert(game);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Game FindByUUID(string UUID)
        {
            try
            {
                return _data.FindByUUID(UUID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Game game)
        {
            try
            {
                _data.Update(game);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
