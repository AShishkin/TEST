using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace South_Park
{
    class ManagerEnemies : Game
    {
        private Random REnemieType = new Random(DateTime.Now.Millisecond);
        // Словарь команд передвижения
        private Dictionary<int, string> RGingerType;





        public ManagerEnemies()
        {
            RGingerType = new Dictionary<int, string>()
            {
                { 0, "Ginger01" },
                { 1, "Ginger02" },

            };
        }


        public object Construct(Game game, string name)
        {
            switch (name)
            {
               // case "Homeless": return new Homeless(game); break;

                case "Gingers" : return new Gingers(game, RGingerType[REnemieType.Next(0, 2)]); break;
                default: return null; 

            }      
        }
    }
}
