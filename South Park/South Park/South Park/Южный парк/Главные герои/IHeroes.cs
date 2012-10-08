using System.Drawing;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace South_Park
{
    /// <summary>
    /// интерфейс игровых героев
    /// </summary>
    interface IHeroes
    {
        /// <summary>
        /// Имя
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Позиция
        /// </summary>
        Vector2 Location { get; set; }

        /// <summary>
        /// Время бездействия
        /// </summary>
        int InactivityTime { get; set; }

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
        /// Шаг перемещения
        /// </summary>
        int Step { get; set; }

        /// <summary>
        /// Время перезарядки
        /// </summary>
        int RechargeTime { get; set; }

        /// <summary>
        /// Маска коллизии
        /// </summary>
        Microsoft.Xna.Framework.Rectangle RectangleMask { get; set; }

        /// <summary>
        /// Размер спрайта
        /// </summary>
        Size SpriteSize { get; set; }

        /// <summary>
        /// Столкновение с игровыми обьектами
        /// </summary>
        bool IsCollision { get; set; }

        /// <summary>
        /// Команда создания игрового обьекта
        /// </summary>
        string CommandsCreateGameObjects { get; set; }

        /// <summary>
        /// Возвращает прямоугольник границ
        /// </summary>
        /// <returns></returns>
        Microsoft.Xna.Framework.Rectangle GetBounds();
 
        /// <summary>
        /// Проверка столкновения масок
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        bool CheckCollision(Microsoft.Xna.Framework.Rectangle rect);

        /// <summary>
        /// Вызод из игровой зоны
        /// </summary>
        void ExitFromMap();
    }
}
