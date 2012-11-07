namespace South_Park___Coon_and_friends
{
    #region Opp
    // Структура содержит матрицу игровых клеток
    // GameMap - позволяет получить доступ к элементу матрицы
    #endregion
    struct Map 
    {
        private static Cell[,] GMap = new Cell[8, 17]; // Матрица игровых клеток(игровая карта)

        // Возвращает весь массив игровых клеток
        public static Cell[,] GameMap
        {
            get { return GMap; }
        }
    }
}
