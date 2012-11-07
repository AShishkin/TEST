using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

namespace South_Park___Coon_and_friends
{
    public class Eric : DrawableGameComponent, IHero
    {
        #region description

        /* В класс Картмана передаем массив текстру(всей анимации)
         * После создания экземпляра данного класса создаем обьект (класс Animation) 
         * у каждого обьекта у которого есть анимация будет свой экземпляр данного класса
         * в Animation передаем текстуру(НЕ МАССИВ ТЕКСТКУР) для проигрывания текущей анимации
         * 
         * Вызывая метод DRAW у класса CARTMAN в основном классе(LEVEL) мы вызываем и метод DRAW 
         * у класса Animation следовательно отрисовывем анимацию
         * 
         * Пример схемы последовательных вызовов методов
         * 
         * GAME1.DRAW() => LEVEL.DRAW() => CARTMAN.DRAW() => ANIMATION.DRAW()
         * GAME1.UPDATE => LEVEL.UPDATE() => CARTMAN.UPDATE() => ANIMATION.UPDATE()
         * 
         */

        #endregion

        private delegate void DMethods();

        private Dictionary<int, Texture2D> TextureMovement; // Словарь текстур передвижения
        private Dictionary<int, DMethods> CreateGameObject; // Словарь команд создания объектов
        private Animation Animation; // Анимация
        // Индикатор здоровья
       // private HealthIndicator HealthIndicator;
        // Генератор случайных чисел
        private static Random Random;




        public Eric(Game game, Vector2 location)
            : base(game)
        {
            this.Level = this.Experience = this.RechargeTime = 0;
            this.Health = 100;
            this.Step = 4;
            this.Direction = 6;
            this.IsCollision = false;
            this.Location = location;


            TextureMovement = new Dictionary<int, Texture2D>();
            this.LoadContent(); 
            CreateGameObject = new Dictionary<int, DMethods>
            {
                { 0, this.CreateCameraTawer },
                { 1, this.CreateSnowball },
                { 2, this.NullObject },
            };

            Animation = new Animation(Game, this.TextureMovement[0], this.Location, ManagerHero.GetSize(this.Type), 3, 8);
          //  HealthIndicator = new HealthIndicator(Game);
            Random = new Random(DateTime.Now.Millisecond);
        }




        /// <summary>
        /// Имя
        /// </summary>
        public string Type
        {
            get { return "Eric"; }
        }
        /// <summary>
        /// Положение
        /// </summary>
        public Vector2 Location { get; set; }
        /// <summary>
        /// Время бездействия
        /// </summary>
        public int InactivityTime { get; set; }
        /// <summary>
        /// Здоровье
        /// </summary>
        public int Health { get; set; }
        /// <summary>
        /// Уровень
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// Очки опыта
        /// </summary>
        public int Experience { get; set; }
        /// <summary>
        /// Направление
        /// </summary>
        public int Direction { get; set; }
        /// <summary>
        /// Шаг перемещения
        /// </summary>
        public int Step { get; set; }
        /// <summary>
        /// Время перезарядки
        /// </summary>
        public int RechargeTime { get; set; }
        /// <summary>
        /// Маска коллизии
        /// </summary>
        public Rectangle RectangleMask { get; set; }
        /// <summary>
        /// Столкновение
        /// </summary>
        public bool IsCollision { get; set; }
        /// <summary>
        /// Команда создания игрового обьекта
        /// </summary>
        public string CommandsCreateGameObjects { get; set; }
        /// <summary>
        /// Вызод из игровой зоны
        /// </summary>
        public void ExitFromMap()
        {
            if (this.Location.X < 152) this.Location = new Vector2(this.Location.X + 4, this.Location.Y);
            else if (this.Location.X > 1236) this.Location = new Vector2(this.Location.X - 4, this.Location.Y);

            if (this.Location.Y < 164) this.Location = new Vector2(this.Location.X, this.Location.Y + 4);
            else if (this.Location.Y > 564) this.Location = new Vector2(this.Location.X, this.Location.Y - 4);
        }
        /// <summary>
        /// Возвращает прямоугольник границ 
        /// </summary>
        /// <returns></returns>
        public Rectangle GetBounds()
        {
            return new Rectangle((int)this.Location.X + 30, (int)this.Location.Y + 60, 20, 20);
        }
        /// <summary>
        /// Проверка столкновения масок
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        public bool CheckCollision(Rectangle mask)
        {
            Rectangle collisionMask = new Rectangle((int)Location.X + 30, (int)Location.Y + 60, 20, 20);
            return collisionMask.Intersects(mask);
        }

 
        // Индексы создания игровых обьектов
        private int GameObject(KeyboardState KeyBord)
        {
            if (this.Health < 0) return 2;
            else
            {
                if (KeyBord.IsKeyDown(Keys.Space)) return 0;
                else if (KeyBord.IsKeyDown(Keys.Z)) return 1;
                else return 2;       
            }
        }
        // Создание камеры
        private void CreateCameraTawer()
        {
            this.CommandsCreateGameObjects = "CameraTawer";
        }

        private void CreateSnowball()
        {
            this.CommandsCreateGameObjects = "Snowball";
        }
        // Команда не создания обьекта
        private void NullObject()
        {
            this.CommandsCreateGameObjects = "Null";
        }



        // Обработка нажатия клавиши
        private int KeyIsPressed(KeyboardState KeyBord)
        {
            if (KeyBord.IsKeyDown(Keys.Left)) return 0;
            else if (KeyBord.IsKeyDown(Keys.Up)) return 1;
                else if (KeyBord.IsKeyDown(Keys.Right)) return 2;
                    else if (KeyBord.IsKeyDown(Keys.Down)) return 3;
                        else return 4;      
        }

    



        private void AnimationMove(int index, int direction, int frame, int startFrame, float TimeFrame)
        {
            if(Animation.Texture != this.TextureMovement[index])
                Animation.Texture = this.TextureMovement[index];

            if(Animation.Frames != frame)
                Animation.Frames = frame;

            if (Animation.StartFrame != startFrame)
                Animation.StartFrame = startFrame;

            if (Animation.TimeFrame != TimeFrame)
                Animation.TimeFrame = TimeFrame;

            this.Direction = direction;

            this.InactivityTime = 0;
        }

      


        

        private void KeyIs(int index, int direction, int frame, int startFrame, float timeFrame)
        {
            

            this.AnimationMove(index, direction, frame, startFrame, timeFrame);
            this.Location = ManagerHero.Movement(direction, this.Location, this.Step);
            Animation.Location = this.Location;


        }



        // +
        private void WithoutMovement()
        {
            this.InactivityTime++;

            if (this.Health > 0 && this.Health < 31) Animation.Construct(this.TextureMovement[6], 3, 0);
            else if (this.Health > 30 && this.Health < 66) Animation.Construct(this.TextureMovement[5], 3, 0);
            else if (this.Health > 66 && this.Health < 101) Animation.Construct(this.TextureMovement[4], 3, 0);
                
            if (this.Direction != 3)
                this.Direction = 3;
        }



        protected override void LoadContent()
        {
           ManagerEricContent.LoadContent(Game, ref TextureMovement);


            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (this.Enabled)
            {
                Animation.Update(gameTime);
             //   HealthIndicator.Update(gameTime);
                this.ExitFromMap(); // Проверка пересечения Картмана с границами игровой зоны
            }


            if (this.RechargeTime >= 50) this.RechargeTime = 0; else this.RechargeTime++;
            
            
            

            // Словарь команд создания игровых обьектов
            CreateGameObject[this.GameObject(Keyboard.GetState())]();


            if (this.Health > 0)
            {


                if (Keyboard.GetState().IsKeyDown(Keys.Left) && (Keyboard.GetState().IsKeyDown(Keys.Up))) 
                    this.KeyIs(2, this.Direction = 1, 3, 0, 0.06F);
                else if (Keyboard.GetState().IsKeyDown(Keys.Left) && (Keyboard.GetState().IsKeyDown(Keys.Down)))
                    this.KeyIs(2, this.Direction = 7, 3, 0, 0.06F);
                    else if (Keyboard.GetState().IsKeyDown(Keys.Right) && (Keyboard.GetState().IsKeyDown(Keys.Up)))
                        this.KeyIs(3, this.Direction = 3, 3, 0, 0.06F);               
                        else if (Keyboard.GetState().IsKeyDown(Keys.Right) && (Keyboard.GetState().IsKeyDown(Keys.Down)))
                            this.KeyIs(3, this.Direction = 5, 3, 0, 0.06F);
                            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                                this.KeyIs(2, this.Direction = 0, 3, 0, 0.06F);
                                else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                                    this.KeyIs(3, this.Direction = 4, 3, 0, 0.06F);
                                    else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                                        this.KeyIs(1, this.Direction = 2, 3, 0, 0.06F);
                                        else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                                            this.KeyIs(0, this.Direction = 6, 3, 1, 0.125F);
                                            else this.WithoutMovement();
    
             //   HealthIndicator.Location = this.Location;
            }
          //  else
           // {
              //  if ((_DeadTimer >= 0) && (_DeadTimer <= 501)) this.Dead();
              //  else _DeadTimer = 0;
           // }

         

            if ((this.InactivityTime >= 200) && (this.InactivityTime <= 400))
            {
         


            }
            else
                if (this.InactivityTime >= 400)
                    this.InactivityTime = 0;


           // if (Keyboard.GetState().IsKeyDown(Keys.F1))
           //     this.Health = HealthIndicator.Health--;

           // if (Keyboard.GetState().IsKeyDown(Keys.F2))
           //     this.Health = HealthIndicator.Health++;

            if (Keyboard.GetState().IsKeyDown(Keys.F3))
                this.Experience++;

            if (Keyboard.GetState().IsKeyDown(Keys.F4))
                this.Experience--;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (this.Enabled)
            {
                Animation.Draw(gameTime);
             //   HealthIndicator.Draw(gameTime);
            }
            base.Draw(gameTime);
        }
    }
}
