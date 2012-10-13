using Microsoft.Xna.Framework;

namespace South_Park
{
    class ManagerEnemies : Game
    {
        public object Construct(Game game, string name)
        {
            switch (name)
            {
                case "Homeless": return new Homeless(game); break;
                default: return null; 

            }      
        }
    }
}
