using Microsoft.Xna.Framework;

namespace South_Park
{
    class ManagerCheckPoints : Game
    {
        /// <summary>
        /// Конструктор контрольных точек
        /// </summary>
        /// <param name="game">Игра</param>
        /// <param name="checkPointChar">Символ соответствующий виду контрольной точки</param>
        /// <param name="location">Позиция</param>
        /// <returns>Возвращает объект класса контрольных точек соответствующий символу в виде object
        /// при использовании приводится к определенному типу контрольной точки
        /// </returns>
        public object CheckPointConstruct(Game game, char checkPointChar, Vector2 location)
        {
            switch (checkPointChar)
            {
                case '~': return new CheckPointStart(game, location, 0); break;
                case '!': return new CheckPointStart(game, location, 1); break;
                case '@': return new CheckPointStart(game, location, 2); break;
                case '#': return new CheckPointStart(game, location, 3); break;
                case '$': return new CheckPointStart(game, location, 4); break;
                case '%': return new CheckPointStart(game, location, 5); break;
                case '^': return new CheckPointStart(game, location, 6); break;
                case '&': return new CheckPointStart(game, location, 7); break;

                case '0': return new CheckPointMove(game, location, 0); break;
                case '1': return new CheckPointMove(game, location, 1); break;
                case '2': return new CheckPointMove(game, location, 2); break;
                case '3': return new CheckPointMove(game, location, 3); break;
                case '4': return new CheckPointMove(game, location, 4); break;
                case '5': return new CheckPointMove(game, location, 5); break;
                case '6': return new CheckPointMove(game, location, 6); break;
                case '7': return new CheckPointMove(game, location, 7); break;

                case '+': return new CheckPointFinish(location);
                default: return null; break;

            }
        }
    }
}
