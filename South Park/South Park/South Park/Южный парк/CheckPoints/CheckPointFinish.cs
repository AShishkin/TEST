using Microsoft.Xna.Framework;

namespace South_Park
{
    struct CheckPointFinish
    {
        public CheckPointFinish(Vector2 location)
        {
            this.Location = location;
        }
        /// <summary>
        /// Положение
        /// </summary>
        public Vector2 Location;
        /// <summary>
        /// Проверка столкновения масок
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public bool CheckCollision(Microsoft.Xna.Framework.Rectangle rect)
        {
            Rectangle spriterect = new Rectangle((int)Location.X, (int)Location.Y, 100, 100);
            return spriterect.Intersects(rect);
        }
        /// <summary>
        /// Возвращает прямоугольник границ 
        /// </summary>
        /// <returns></returns>
        public Rectangle GetBounds()
        {
            return new Rectangle((int)this.Location.X, (int)this.Location.Y, 100, 100);
        }
    }
}
