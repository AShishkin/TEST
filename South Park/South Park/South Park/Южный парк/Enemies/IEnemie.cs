using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace South_Park
{
    interface IEnemie
    {
        /// <summary>
        /// Имя
        /// </summary>
        string Name { get; }

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
        /// Направление
        /// </summary>
        int Direction { get; set; }

        /// <summary>
        /// Шаг перемещения
        /// </summary>
        int Step { get; set; }

        /// <summary>
        /// Маска коллизии
        /// </summary>
        Rectangle RectangleMask { get; set; }

        /// <summary>
        /// Размер спрайта
        /// </summary>
        System.Drawing.Size SpriteSize { get; set; }

        /// <summary>
        /// Столкновение с игровыми обьектами
        /// </summary>
        bool IsCollision { get; set; }

        /// <summary>
        /// Возвращает прямоугольник границ
        /// </summary>
        /// <returns></returns>
        Rectangle GetBounds();

        /// <summary>
        /// Проверка столкновения масок
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        bool CheckCollision(Microsoft.Xna.Framework.Rectangle rect);
    }
}
