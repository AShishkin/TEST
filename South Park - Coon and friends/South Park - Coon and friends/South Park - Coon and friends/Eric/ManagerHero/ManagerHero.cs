using Microsoft.Xna.Framework;

namespace South_Park___Coon_and_friends
{
    struct ManagerHero
    {
        /// <summary>
        /// Возвращает позицию
        /// </summary>
        /// <param name="direction">Направление</param>
        /// <param name="location">Текущая позиция</param>
        /// <param name="step">Шиг</param>
        /// <returns></returns>
        static public Vector2 Movement(int direction, Vector2 location, float step)
        {
            switch (direction)
            {
                case 0: location.X -= step; return location;
                case 1: location.X -= step; location.Y -= step; return location;
                case 2: location.Y -= step; return location;
                case 3: location.X += step; location.Y -= step; return location;
                case 4: location.X += step; return location;
                case 5: location.X += step; location.Y += step; return location;
                case 6: location.Y += step; return location;
                case 7: location.X -= step; location.Y += step; return location;
                default: return Vector2.Zero;
            }
        }
        /// <summary>
        /// Возвращает размер спрайта
        /// </summary>
        /// <param name="type">Тип</param>
        /// <returns></returns>
        static public System.Drawing.Size GetSize(string type)
        {
            switch (type)
            {
                case "Eric": return new System.Drawing.Size(80, 80);
                default: return System.Drawing.Size.Empty;
            }
        }
    }
}
    
