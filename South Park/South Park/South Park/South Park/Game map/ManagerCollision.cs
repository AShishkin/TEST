using Microsoft.Xna.Framework;

namespace South_Park
{
    class ManagerCollision
    {
        /// <summary>
        /// Проверка столкновений
        /// </summary>
        /// <param name="hero">Герой</param>
        /// <param name="tawer">Пул объектов</param>
        /// <param name="arrow">Стрелка</param>
        /// <param name="bubble">Круги</param>
        public void CHeroWithTawer(IHeroes hero, ObjectPool<ITawers> tawer, Arrow arrow, Bubble bubble)
        {
            for (int i = 0; i < tawer.Count; i++)
            {
                if (hero.CheckCollision(tawer[i].GetBounds()))
                {
                    arrow.CreateArrow(new Vector2(tawer[i].Location.X + 10, tawer[i].Location.Y - 80));
                    bubble.Create(new Vector2(tawer[i].Location.X, tawer[i].Location.Y + 35), 0);
                    hero.IsCollision = true;
                    break;
                }
                else
                {
                    bubble.Enabled = arrow.Enabled = false;
                    hero.IsCollision = false;
                }
            }
        }
        /// <summary>
        /// Проверка столкновений
        /// </summary>
        /// <param name="enemie">Враг</param>
        /// <param name="tawer">Пул объектов</param>
        public void CEnemiesWithTawer(IEnemie enemie, ObjectPool<ITawers> tawer, Clouds clouds, int count)
        {
            for (int i = 0; i < tawer.Count; i++)
            {
                if (enemie.CheckCollision(tawer[i].GetBounds()))
                {
                    ((CameraTawer)tawer[i]).InactivityTime = 0;
                    this.СReDirection(enemie.Direction, tawer[i]);
                    

                    enemie.Stop(enemie.Direction);
                    tawer[i].Health--;
                    //!!
                    if (tawer[i].Health < 1)
                    {
                        count--;
                        ((CameraTawer)tawer[i]).Enabled = true;
                        clouds.Enabled = true;
                        clouds.Create(tawer[i].Location, 0);
                        tawer[i].Location = Vector2.Zero;
                    }
                }
            }
        }

        public void CEnemiesWithTawerFireZone(IEnemie enemie, ObjectPool<ITawers> tawer)
        {
            for (int i = 0; i < tawer.Count; i++)
            {

                if (tawer[i].CheckCollisionWithFireZone(enemie.GetBounds()))
                {
                    ((CameraTawer)tawer[i]).InactivityTime = 0;
                    if (!((CameraTawer)tawer[i]).Enabled)
                    {
                        if (enemie.Location.X < tawer[i].Location.X && enemie.Location.Y < tawer[i].Location.Y) this.СReDirection(5, tawer[i]);
                        else if (enemie.Location.X > tawer[i].Location.X && enemie.Location.Y < tawer[i].Location.Y) this.СReDirection(7, tawer[i]);
                        else if (enemie.Location.X < tawer[i].Location.X && enemie.Location.Y > tawer[i].Location.Y) this.СReDirection(3, tawer[i]);
                        else if (enemie.Location.X > tawer[i].Location.X && enemie.Location.Y > tawer[i].Location.Y) this.СReDirection(1, tawer[i]); 
                    } 
                }
            }
        }

        /// <summary>
        /// Провекра столкновений
        /// </summary>
        /// <param name="enemie">Враг</param>
        /// <param name="checkPointMove">Контрольная точка</param>
        public void CEnemieWithIndexMove(IEnemie enemie, CheckPointMove checkPointMove)
        {
            if (enemie.CheckCollision(checkPointMove.GetBounds()))
                enemie.Direction = checkPointMove.Direction;
        }
        /// <summary>
        /// Проверка столкновений
        /// </summary>
        /// <param name="enemie">Враг</param>
        /// <param name="chechPointFinish">Точка фииша</param>
        /// <param name="checkPointStart">Точка старта</param>
        public void CEnemieWithIndexFinish(IEnemie enemie, CheckPointFinish chechPointFinish,
                                           CheckPointStart checkPointStart)
        {
            if (enemie.CheckCollision(chechPointFinish.GetBounds()))
            {
                enemie.Location = new Vector2(checkPointStart.Location.X - 100, checkPointStart.Location.Y);
                if (enemie.Direction != checkPointStart.Direction)
                    enemie.Direction = checkPointStart.Direction;
            }
        }
        // Поворачивает башню в сторону противника при столкновениия
        private void СReDirection(int direction, ITawers tawer)
        {
            switch (direction)
            {
                case 0: tawer.Direction = 6; break;
                case 1: tawer.Direction = 7; break;
                case 2: tawer.Direction = 0; break;
                case 3: tawer.Direction = 1; break;
                case 4: tawer.Direction = 2; break;
                case 5: tawer.Direction = 3; break;
                case 6: tawer.Direction = 4; break;
                case 7: tawer.Direction = 5; break;
            }           
        }
    }  
}
