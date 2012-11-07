using Microsoft.Xna.Framework;

namespace South_Park___Coon_and_friends
{
    class ManagerCollision
    {
        public static Vector2 LocationCellCollistion { get; set; } // Координаты клетки с которой произошло столкновение
        public static int RowIndexCollision { get; set; } // Номер столбца клетки с которой произошло столкновение
        public static int CollumnIndexCollision { get; set; } // Номер строки клетки с которой произошло столкновение

        /// <summary>
        /// Проверяет столкновение героя с игровыми клетками
        /// </summary>
        /// <param name="hero">Герой</param>
        public static void HCollisionWithCell(Eric hero)
        {
            for(int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    if (hero.CheckCollision(Map.GameMap[i, j].Rectangle))
                    {
                        hero.IsCollision = true;
                        LocationCellCollistion = Map.GameMap[i, j].Location;
                        RowIndexCollision = i;
                        CollumnIndexCollision = j;
                        break;
                    }
                    else hero.IsCollision = false;  
                }
                if (hero.IsCollision) break;
            }

        
        }
    }
}
