using System;
using System.Drawing;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace South_Park
{
    public class Cartman : DrawableGameComponent, IHeroes
    {
        #region  Описание класса

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

        #region Обьявления классов, словарей, локальные поля

        // Делегат методов
        private delegate void DMethods();
        // Словарь текстур
        private Dictionary<int, Texture2D> Texture;
        // Словарь команд бросания снежка
        private Dictionary<int, DMethods> ThrowingOfSnowballs; 
        // Словарь команд передвижения
        private Dictionary<int, DMethods> Movement;
        // Словарь команд создания игровых обьектов
        private Dictionary<int, DMethods> CreateGameObject;
        // Словарь диалогов и фраз
        private Dictionary<int, DMethods> Dialogues;
        // Звуки
        private SoundEffect[] SoundEffect;
        // Анимация
        private Animation Animation;
        // Индикатор здоровья
        private HealthIndicator HealthIndicator;
        // Генератор случайных чисел
        private static Random Random;
        // Индех случайного диалога
        private int _DialogIndex = 0;

        private int _DeadTimer = 0;

        private Continuum Continuum;


        #endregion

        #region Designer

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="game">Игра</param>
        /// <param name="location">Позиция</param>
        public Cartman(Game game, Vector2 location)
            : base(game)
        {
            Texture = new Dictionary<int, Texture2D>();
            SoundEffect = new SoundEffect[30];

            this.LoadContent(); 

            ThrowingOfSnowballs = new Dictionary<int, DMethods>
            {
                { 0, this.ThrowToTheLeft },
                //{ 1, delegate(double x, double y) { return x - y; } },
                { 2, this.ThrowToTheRight },
                { 3, this.ThrowToTheDown },
            };


            #region ## Словарь методов передвижения ##

            Movement = new Dictionary<int, DMethods>
            {
                { 0, this.KeyIsPressedLeft },
                { 1, this.KeyIsPressedUp },
                { 2, this.KeyIsPressedRight },
                { 3, this.KeyIsPressedDown },
                { 4, this.WithoutMovement },
            };

            #endregion

            #region ## Словарь методов создания игровых обьектов ##

            CreateGameObject = new Dictionary<int, DMethods>
            {
                { 0, this.CreateCameraTawer },
                { 1, this.CreateSnowball },
                { 2, this.NullObject },
            };

            #endregion

            #region ## Словарь методов диалогов и фраз ##

            Dialogues = new Dictionary<int, DMethods>
            {
                { 1, this.TurnToTheLeft },
                { 2, this.Inactivity },
                { 3, this.TurnToTheRight },
                { 4, this.WithoutMovement },
                { 5, this.Noooo },
                { 6, this.SurpriseDialog },
                { 7, this.Curse },
                { 8, this.Question },
                { 9, this.ShockDialog },
                { 10, this.BoredomThePhrase },
                { 11, this.ApprovalTheUser },
                { 12, this.MegaSurprise },
                { 13, this.ApprovalTheUser },
                { 14, this.No },
            };

            #endregion

            

            this.Level = this.Experience = this.RechargeTime = 0;

            this.Health = 100;
            this.Step = 10;
            this.Direction = 3;

            this.IsCollision = false;
            this.Location = location;

            this.SpriteSize = new Size(80, 80);
            
            Animation = new Animation(Game, this.Texture[0], this.Location, this.SpriteSize, 3, 8);
            HealthIndicator = new HealthIndicator(Game);
            Random = new Random(DateTime.Now.Millisecond);
            Continuum = new Continuum(5);
     
        }

        #endregion

        #region Fields

        /// <summary>
        /// Имя
        /// </summary>
        public string Name
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
        public Microsoft.Xna.Framework.Rectangle RectangleMask { get; set; }
        /// <summary>
        /// Размер спрайта
        /// </summary>
        public Size SpriteSize { get; set; }
        /// <summary>
        /// Столкновение
        /// </summary>
        public bool IsCollision { get; set; }
        /// <summary>
        /// Команда создания игрового обьекта
        /// </summary>
        public string CommandsCreateGameObjects { get; set; }

        public int ContinuumCount
        {
            get { return Continuum.Count; }
            set { Continuum.Count = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Вызод из игровой зоны
        /// </summary>
        public void ExitFromMap()
        {
            if (this.Location.X < 152) this.Location = new Vector2(this.Location.X + 4, this.Location.Y);
            else if (this.Location.X > 1236) this.Location = new Vector2(this.Location.X - 4, this.Location.Y);

            if (this.Location.Y < 180) this.Location = new Vector2(this.Location.X, this.Location.Y + 4);
            else if (this.Location.Y > 564) this.Location = new Vector2(this.Location.X, this.Location.Y - 4);
        }
        /// <summary>
        /// Возвращает прямоугольник границ 
        /// </summary>
        /// <returns></returns>
        public Microsoft.Xna.Framework.Rectangle GetBounds()
        {
            return new Microsoft.Xna.Framework.Rectangle((int)this.Location.X, (int)this.Location.Y,
                                                        this.SpriteSize.Width, this.SpriteSize.Height);
        }
        /// <summary>
        /// Проверка столкновения масок
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public bool CheckCollision(Microsoft.Xna.Framework.Rectangle rect)
        {
            Microsoft.Xna.Framework.Rectangle spriterect =
                new Microsoft.Xna.Framework.Rectangle((int)Location.X, (int)Location.Y,
                    SpriteSize.Width, SpriteSize.Height);

            return spriterect.Intersects(rect);
        }
        public void Dialog(int index)
        {
            BoredomThePhrase();
           // Dialogues[index]();
        }

        #region Команды созданя игровых обьектов

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

        #endregion

        #region Методы передвижения

        // Обработка нажатия клавиши
        private int KeyIsPressed(KeyboardState KeyBord)
        {
            if (KeyBord.IsKeyDown(Keys.Left)) return 0;
            else if (KeyBord.IsKeyDown(Keys.Up)) return 1;
                else if (KeyBord.IsKeyDown(Keys.Right)) return 2;
                    else if (KeyBord.IsKeyDown(Keys.Down)) return 3;
                        else return 4;      
        }
        // Движение влево
        private void MoveLeft()
        {
            Animation.Location = this.Location = new Vector2(this.Location.X - 4, this.Location.Y);
        }
        // Движение влево и вверх
        private void MoveLeftUp()
        {
            Animation.Location = this.Location = new Vector2(this.Location.X - 4, this.Location.Y - 4);  
        }
        // Движение влево и вниз
        private void MoveLeftDown()
        {
            Animation.Location = this.Location = new Vector2(this.Location.X - 4, this.Location.Y + 4); 
        }
        // Движение вправо
        private void MoveRight()
        {
            Animation.Location = this.Location = new Vector2(this.Location.X + 4, this.Location.Y);
        }
        // Движение вправо и вверх
        private void MoveRightUp()
        {
            Animation.Location = this.Location = new Vector2(this.Location.X + 4, this.Location.Y - 4);
        }
        // Движение вправо и вниз
        private void MoveRightDown()
        {
            Animation.Location = this.Location = new Vector2(this.Location.X + 4, this.Location.Y + 4);
        }
        // Движение вверх
        private void MoveUp()
        {
            Animation.Location = this.Location = new Vector2(this.Location.X, this.Location.Y - 4);
        }
        // Движение вниз
        private void MoveDown()
        {
            Animation.Location = this.Location = new Vector2(this.Location.X, this.Location.Y + 4);
        }

        #endregion

        #region Нажатие клавиши движения вверх/вниз и вправо/влево

        private void AnimationMove(int index, int direction, int frame, int startFrame, float TimeFrame)
        {
            if(Animation.Texture != this.Texture[index])
                Animation.Texture = this.Texture[index];

            if(Animation.Frames != frame)
                Animation.Frames = frame;

            if (Animation.StartFrame != startFrame)
                Animation.StartFrame = startFrame;

            if (Animation.TimeFrame != TimeFrame)
                Animation.TimeFrame = TimeFrame;

            this.Direction = direction;

            this.InactivityTime = 0;
        }

        private void KeyIsPressedDown()
        {
            this.AnimationMove(0, 3, 3, 1, 0.125F);
            this.MoveDown();
        }

        private void KeyIsPressedUp()
        {
            this.AnimationMove(1, 1, 3, 1, 0.125F);
            this.MoveUp();
        }
           
        private void KeyIsPressedLeft()
        {

            this.AnimationMove(2, 0, 3, 0, 0.06F);
            this.MoveLeft();
        }

        private void KeyIsPressedLeftUp()
        {
            this.AnimationMove(2, 0, 3, 0, 0.06F); 
            this.MoveLeftUp();
        }

        private void KeyIsPressedLeftDown()
        {
            this.AnimationMove(2, 0, 3, 0, 0.06F);
            this.MoveLeftDown();
        }

        private void KeyIsPressedRight()
        {
            this.AnimationMove(3, 2, 3, 0, 0.06F);
            this.MoveRight();
        }
            
        private void KeyIsPressedRightUp()
        {
            this.AnimationMove(3, 2, 3, 0, 0.06F);
            this.MoveRightUp();
        }

        private void KeyIsPressedRightDown()
        {
            this.AnimationMove(3, 2, 3, 0, 0.06F);
            this.MoveRightDown();
        } 

        private void LeftUpDownMove(KeyboardState KeyBord)
        {
            if (KeyBord.IsKeyDown(Keys.Left) && KeyBord.IsKeyDown(Keys.Up))
                this.KeyIsPressedLeftUp();
            else
            {
                if (KeyBord.IsKeyDown(Keys.Left) && KeyBord.IsKeyDown(Keys.Down))
                    this.KeyIsPressedLeftDown();
                else
                    this.KeyIsPressedLeft();
            }   
        }

        private void RightUpDownMove(KeyboardState KeyBord)
        {
            if (KeyBord.IsKeyDown(Keys.Right) && KeyBord.IsKeyDown(Keys.Up))
                this.KeyIsPressedRightUp();
            else
            {
                if (KeyBord.IsKeyDown(Keys.Right) && KeyBord.IsKeyDown(Keys.Down))
                    this.KeyIsPressedRightDown();
                else
                    this.KeyIsPressedRight();
            }
        }

        #endregion

        #region ## Диалоги ##

        // +
        private void Question()
        {
            if (this.InactivityTime == 205)
                SoundEffect[2].Play();

            Animation.Construct(this.Texture[9], 3, 0, 0.125F);

            if (Animation.CurrentFrame == Animation.Frames - 1)
                if ((this.InactivityTime >= 340) && (this.InactivityTime <= 350))
                    Animation.Construct(this.Texture[9], 2, 2, 0.125F);
                else Animation.Construct(this.Texture[8], 2, 2, 0.125F);                
        }

        // +
        private void Noooo()
        {
            if (this.InactivityTime == 205)
                SoundEffect[3].Play();

            Animation.Construct(this.Texture[8], 3, 0, 0.125F);

            if (Animation.CurrentFrame == Animation.Frames - 1)
                this.Animation.Construct(this.Texture[8], 2, 2, 0.125F); 
        }

        // +
        private void Inactivity()
        {
            if (this.Health <= 30)
            {
                if (this.InactivityTime == 205)
                    SoundEffect[6].Play();
                if (this.InactivityTime <= 315)
                    Animation.Construct(this.Texture[7], 3, 0, 0.125F);
            }
            else
            {
                if (this.InactivityTime == 205)
                    SoundEffect[4].Play();
                if (this.InactivityTime <= 295)
                    Animation.Construct(this.Texture[6], 3, 0, 0.125F);
            }
        }

        // +
        private void SurpriseDialog()
        {
            Animation.Construct(this.Texture[30], 3, 0, 0.125F);

            if (Animation.CurrentFrame == Animation.Frames - 1)
                if (this.InactivityTime >= 380)
                    Animation.Construct(this.Texture[30], 2, 0);
                else Animation.Construct(this.Texture[30], 2, 2);
                    
        }

        // +
        private void WithoutMovement()
        {
            this.InactivityTime++;

            Animation.Construct(this.Texture[4], 3, 0);

            if (this.Direction != 3)
                this.Direction = 3;
        }

        




        // +-
        private void Curse()
        {
            if(this.InactivityTime == 205)
                SoundEffect[0].Play();

            Animation.Construct(this.Texture[10], 3, 0, 0.125F);

            if (Animation.CurrentFrame == Animation.Frames - 1)
                if ((this.InactivityTime >= 208) && (this.InactivityTime <= 232))
                    Animation.Construct(this.Texture[22], 2, 2, 0.125F);
                else Animation.Construct(this.Texture[10], 2, 2, 0.125F);
        }

        
        


        // Править анимацию и звук +-
        private void ShockDialog()
        {
            Animation.Construct(this.Texture[11], 3, 0, 0.125F);

            if (Animation.CurrentFrame == Animation.Frames - 1)
                if(this.InactivityTime >= 230)
                    Animation.Construct(this.Texture[7], 2, 2, 0.125F);

            if (this.InactivityTime == 205)
                SoundEffect[3].Play();

        }

        /// <summary>
        /// Скука
        /// </summary>
        private void BoredomThePhrase()
        {
            if (this.InactivityTime == 205)
                SoundEffect[9].Play();

            if (this.InactivityTime <= 280)
                Animation.Construct(this.Texture[12], 3, 0, 0.125F);
            else this.InactivityTime = 0;       
        }

        /// <summary>
        /// Хвальба пользователя
        /// </summary>
        private void ApprovalTheUser()
        {
            if (this.InactivityTime == 205)
                SoundEffect[5].Play();

            if (this.InactivityTime <= 230)
                Animation.Construct(this.Texture[18], 3, 0, 0.125F);
            else
                if(this.InactivityTime <= 280)
                    Animation.Construct(this.Texture[12], 3, 0, 0.125F);
                else this.InactivityTime = 0;
        }

        /// <summary>
        /// Сильное удивление
        /// </summary>
        private void MegaSurprise()
        {
            if (this.InactivityTime == 205)
                SoundEffect[11].Play();

            if (this.InactivityTime <= 230)
                Animation.Construct(this.Texture[14], 3, 0, 0.125F);
            else
                if (this.InactivityTime <= 284)
                    Animation.Construct(this.Texture[12], 3, 0, 0.125F);
                else this.InactivityTime = 0;
        }

        /// <summary>
        /// Досада
        /// </summary>
        private void Disappointment()
        {
            if (this.InactivityTime == 205)
                SoundEffect[12].Play();

            if (this.InactivityTime <= 290)
                Animation.Construct(this.Texture[4], 3, 0, 0.125F);
            else this.InactivityTime = 0;
        }

        private void No()
        {
            if (this.InactivityTime == 205)
                SoundEffect[14].Play();

            if (this.InactivityTime <= 230)
                Animation.Construct(this.Texture[20], 3, 0, 0.125F);
            else this.InactivityTime = 0;
        }

        private void Freedom()
        {
            if (this.InactivityTime == 205)
                SoundEffect[15].Play();

            if (this.InactivityTime <= 300)
                Animation.Construct(this.Texture[16], 3, 0, 0.125F);
            else
                if((this.InactivityTime > 300) && (this.InactivityTime < 365))
                    Animation.Construct(this.Texture[17], 3, 0, 0.125F);
        }

        #endregion

        #region ## Действия ##

        // +
        private void TurnToTheLeft()
        {
            Animation.Construct(this.Texture[31], 3, 0, 0.125F);
        }

        // +
        private void TurnToTheRight()
        {
            Animation.Construct(this.Texture[32], 3, 0, 0.125F);
        }

        /// <summary>
        /// Анимация смерти
        /// </summary>
        private void Dead()
        {
            _DeadTimer++;

            Animation.Frames = 5;
            Animation.Texture = this.Texture[33];

            if (_DeadTimer == 500)
            {
                this.Health = HealthIndicator.Health = 100;
                this.ContinuumCount--;
            }
        }

        private void Sleep()
        {
            if ((this.InactivityTime >= 200) && (this.InactivityTime <= 230))
                Animation.Construct(this.Texture[23], 3, 0, 0.125F);
            
        }

        #endregion


        #region ** Бросок снежка **

        private void ToThrowSnowball(int Direction)
        {
            if(this.InactivityTime != 0)
                this.InactivityTime = 0;

             ThrowingOfSnowballs[Direction]();   
        }



        private void ThrowToTheLeft()
        {
            if (Animation.Texture != this.Texture[8])
                Animation.Texture = this.Texture[8];

            if (Animation.Frames != 3)
                Animation.Frames = 3;

            if (Animation.TimeFrame != 0.125F)
                Animation.TimeFrame = 0.125F;
        }

        private void ThrowToTheRight()
        {
            if (Animation.Texture != this.Texture[9])
                Animation.Texture = this.Texture[9];

            if (Animation.Frames != 3)
                Animation.Frames = 3;

            if (Animation.TimeFrame != 0.125F)
                Animation.TimeFrame = 0.125F;
        }

        private void ThrowToTheDown()
        {
            if (Animation.Texture != this.Texture[10])
                Animation.Texture = this.Texture[10];

            if (Animation.Frames != 3)
                Animation.Frames = 3;

            if (Animation.TimeFrame != 0.125F)
                Animation.TimeFrame = 0.125F;
        }


        


        #endregion



        /// <summary>
        /// Загрузка игрового контента
        /// </summary>
        protected override void LoadContent()
        {
            Texture.Add(0, Game.Content.Load<Texture2D>("Картман/Movement/Down"));
            Texture.Add(1, Game.Content.Load<Texture2D>("Картман/Movement/Up"));
            Texture.Add(2, Game.Content.Load<Texture2D>("Картман/Movement/Left"));
            Texture.Add(3, Game.Content.Load<Texture2D>("Картман/Movement/Right"));
            Texture.Add(4, Game.Content.Load<Texture2D>("Картман/Stopped/Stopped"));

            Texture.Add(6, Game.Content.Load<Texture2D>("Картман/Разговоры/Dialog1"));
            Texture.Add(7, Game.Content.Load<Texture2D>("Картман/Разговоры/Dialog2"));
            Texture.Add(8, Game.Content.Load<Texture2D>("Картман/Разговоры/Dialog3"));
            Texture.Add(9, Game.Content.Load<Texture2D>("Картман/Разговоры/Dialog4"));
            Texture.Add(10, Game.Content.Load<Texture2D>("Картман/Разговоры/Dialog5"));
            Texture.Add(11, Game.Content.Load<Texture2D>("Картман/Разговоры/Dialog6"));
            Texture.Add(12, Game.Content.Load<Texture2D>("Картман/Разговоры/Dialog7"));
            Texture.Add(13, Game.Content.Load<Texture2D>("Картман/Разговоры/Dialog8"));
            Texture.Add(14, Game.Content.Load<Texture2D>("Картман/Разговоры/Dialog9"));
            Texture.Add(15, Game.Content.Load<Texture2D>("Картман/Разговоры/Dialog10"));
            Texture.Add(16, Game.Content.Load<Texture2D>("Картман/Разговоры/Dialog11_1"));
            Texture.Add(17, Game.Content.Load<Texture2D>("Картман/Разговоры/Dialog11_2"));



            Texture.Add(30, Game.Content.Load<Texture2D>("Картман/Действия/Surprise"));
            Texture.Add(31, Game.Content.Load<Texture2D>("Картман/Действия/Turns/Left"));
            Texture.Add(32, Game.Content.Load<Texture2D>("Картман/Действия/Turns/Right"));
            Texture.Add(33, Game.Content.Load<Texture2D>("Картман/Действия/Dead"));


          //  this.Texture[8] = Game.Content.Load<Texture2D>("Картман/Действия/Throws/ThrowToTheLeft");
          //  this.Texture[9] = Game.Content.Load<Texture2D>("Картман/Действия/Throws/ThrowToTheRight");
         //   this.Texture[10] = Game.Content.Load<Texture2D>("Картман/Действия/Throws/ThrowToTheDown");

          //  this.Texture[11] = Game.Content.Load<Texture2D>("Картман/Действия/Turns/TurnToTheLeft");
           // this.Texture[12] = Game.Content.Load<Texture2D>("Картман/Действия/Turns/TurnToTheRight");

            this.Texture[23] = Game.Content.Load<Texture2D>("Картман/Действия/Inactivity");


            SoundEffect[0] = Game.Content.Load<SoundEffect>("Звуки/Картман/CartmanSound01");
            SoundEffect[1] = Game.Content.Load<SoundEffect>("Звуки/Картман/CartmanSound02");
            SoundEffect[2] = Game.Content.Load<SoundEffect>("Звуки/Картман/CartmanSound03");
            SoundEffect[3] = Game.Content.Load<SoundEffect>("Звуки/Картман/CartmanSound04");
            SoundEffect[4] = Game.Content.Load<SoundEffect>("Звуки/Картман/CartmanSound05");
            SoundEffect[5] = Game.Content.Load<SoundEffect>("Звуки/Картман/CartmanSound06");
            SoundEffect[6] = Game.Content.Load<SoundEffect>("Звуки/Картман/CartmanSound07");

            

            SoundEffect[9] = Game.Content.Load<SoundEffect>("Звуки/Картман/CartmanSound09");

            SoundEffect[11] = Game.Content.Load<SoundEffect>("Звуки/Картман/CartmanSound11");
            SoundEffect[12] = Game.Content.Load<SoundEffect>("Звуки/Картман/CartmanSound12");
            SoundEffect[13] = Game.Content.Load<SoundEffect>("Звуки/Картман/CartmanSound13");
            SoundEffect[14] = Game.Content.Load<SoundEffect>("Звуки/Картман/CartmanSound14");
            SoundEffect[15] = Game.Content.Load<SoundEffect>("Звуки/Картман/CartmanSound15");

            base.LoadContent();
        }







        
        public override void Update(GameTime gameTime)
        {
            Animation.Update(gameTime);

            HealthIndicator.Update(gameTime);
            
            // Проверка пересечения Картмана с границами игровой зоны
            this.ExitFromMap();

            // Словарь команд создания игровых обьектов
            CreateGameObject[this.GameObject(Keyboard.GetState())]();

            #region ## Обработчик команд движения персонажа ##

            if (this.Health > 0)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left) && (Keyboard.GetState().IsKeyDown(Keys.Up)
                    || Keyboard.GetState().IsKeyDown(Keys.Down)))
                    this.LeftUpDownMove(Keyboard.GetState());
                else
                    if (Keyboard.GetState().IsKeyDown(Keys.Right) && (Keyboard.GetState().IsKeyDown(Keys.Up)
                        || Keyboard.GetState().IsKeyDown(Keys.Down)))
                        this.RightUpDownMove(Keyboard.GetState());
                    else
                        Movement[this.KeyIsPressed(Keyboard.GetState())]();

                HealthIndicator.Location = this.Location;
            }
            else
            {
                if ((_DeadTimer >= 0) && (_DeadTimer <= 501)) this.Dead();
                else _DeadTimer = 0;
            }

            #endregion

            #region ## Время простоя и случайные диалоги ##

            if ((this.InactivityTime >= 200) && (this.InactivityTime <= 400))
            {
                if (this.InactivityTime == 200)
                    _DialogIndex = Random.Next(1, 14);

               // Dialogues[_DialogIndex]();
               // Inactivity();
              //  Question();
              //  Noooo();
              //  SurpriseDialog();
              //  WithoutMovement();
              //  TurnToTheLeft();
               // TurnToTheRight();
              //  ShockDialog();
              //  Freedom();


            }
            else
                if (this.InactivityTime >= 400)
                    this.InactivityTime = 0;

            #endregion







            if (this.RechargeTime >= 50)
                this.RechargeTime = 0;
                else this.RechargeTime++;

                
                

            if (Keyboard.GetState().IsKeyDown(Keys.F1))
                this.Health = HealthIndicator.Health--;

            if (Keyboard.GetState().IsKeyDown(Keys.F2))
                this.Health = HealthIndicator.Health++;

            if (Keyboard.GetState().IsKeyDown(Keys.F3))
                this.Experience++;

            if (Keyboard.GetState().IsKeyDown(Keys.F4))
                this.Experience--;
































            

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            
            Animation.Draw(gameTime);
            HealthIndicator.Draw(gameTime);

            base.Draw(gameTime);
        }

        #endregion
    }
}
