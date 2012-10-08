using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace South_Park
{
    interface ITawers
    {
        /// <summary>
        /// Текстура
        /// </summary>
        Texture2D[] Texture { get; set; }

        /// <summary>
        /// Позиция
        /// </summary>
        Vector2 Location { get; set; }

        /// <summary>
        /// Здоровье
        /// </summary>
        int Health { get; set; }

        /// <summary>
        /// Уровень
        /// </summary>
        int Level { get; set; }

        /// <summary>
        /// Очки опыта
        /// </summary>
        int Experience { get; set; }

        /// <summary>
        /// Направление
        /// </summary>
        int Direction { get; set; }

        /// <summary>
        /// Маска коллизии
        /// </summary>
        Microsoft.Xna.Framework.Rectangle RectangleMask { get; set; }

        /// <summary>
        /// Размер спрайта
        /// </summary>
        Size SpriteSize { get; set; }

        /// <summary>
        /// Возвращает прямоугольник границ танка
        /// </summary>
        /// <returns></returns>
        Microsoft.Xna.Framework.Rectangle GetBounds();
    
        /// <summary>
        /// Проверка столкновения масок
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        bool CheckCollision(Microsoft.Xna.Framework.Rectangle rect);
    }
}
        
    

