using Microsoft.Xna.Framework;

namespace South_Park___Coon_and_friends
{
    #region Opp
    // Структура игровой клетки
    // Содержит координ положения, индексы расположения в матрице
    #endregion
    struct Cell
    {
        /// <summary>
        /// Игровая клетка
        /// </summary>
        /// <param name="location">Положение</param>
        /// <param name="i">Индекс столбца</param>
        /// <param name="j">Индекс строки</param>
        public Cell(Vector2 location, int i, int j, bool enabled)
        {
            this.Location = location;
            this.i = i;
            this.j = j;
            this.Enabled = enabled;
        }
        /// <summary>
        /// Положение
        /// </summary>
        public Vector2 Location;
        /// <summary>
        /// Индекс столбца
        /// </summary>
        public int i;
        /// <summary>
        /// Индекс строки
        /// </summary>
        public int j;

        public bool Enabled;

        /// <summary>
        /// Возвращает размер клетки
        /// </summary>
        public System.Drawing.Size Size
        {
            get { return new System.Drawing.Size(80, 80); }
        }
        /// <summary>
        /// Возвращает прямоугольник клетки
        /// </summary>
        public Rectangle Rectangle
        {
            get { return new Rectangle((int)this.Location.X, (int)this.Location.Y, 80, 80); }
        }
    }
}
