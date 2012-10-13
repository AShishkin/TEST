using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace South_Park
{
    class Homeless : DrawableGameComponent, IEnemie
    {
        #region Обьявления классов, словарей, локальные поля

        // Делегата методов
        private delegate void DMethods();
        // Словарь команд передвижения
        private Dictionary<int, DMethods> Movement;
        // Словарь команд передвижения
        private Dictionary<int, DMethods> Stopped;
        // Словарь менеджера загрузки игрового контента
        private Dictionary<string, DMethods> HomelessManeger;
        // Индекатор здоровья
        private HealthIndicator HealthIndicator;
        // Анимация
        private Animation Animation;
        // Менеджер отрисовки
        private SpriteBatch SpriteBatch;

        #endregion

        #region Designer

        public Homeless(Game game)
            : base(game)
        {
            HomelessManeger = new Dictionary<string, DMethods>
            {
                { "Homeless01", this.LoadContentHomeless01 },
                { "Homeless02", this.LoadContentHomeless02 },
            }; 

            this.Texture = new Texture2D[10];
            this.Type = "Homeless01";
            this.Location = Vector2.Zero;
            this.Health = 100;
            this.Level = 1;
            this.Step = 1;
            this.Direction = 4;
            this.SpriteSize = new System.Drawing.Size(80, 160);
            this.LoadContent();

            SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
            Animation = new Animation(Game, this.Texture[4], this.Location, this.SpriteSize, 2, 9);
            HealthIndicator = new HealthIndicator(Game);

            Movement = new Dictionary<int, DMethods>
            {
                { 0, this.MoveLeft },
                { 1, this.MoveLeftUp },
                { 2, this.MoveUp },
                { 3, this.MoveRightUp },
                { 4, this.MoveRight },
                { 5, this.MoveRightDown },
                { 6, this.MoveDown },
                { 7, this.MoveLeftDown },
            };

            Stopped = new Dictionary<int, DMethods>
            {
                { 0, this.StoppedLeft },
                { 1, this.StoppedLeftUp },
                { 2, this.StoppedUp },
                { 3, this.StoppedRightUp },
                { 4, this.StoppedRight},
                { 5, this.StoppedRightDown },
                { 6, this.StoppedDown },
                { 7, this.StoppedLeftDown },
            };           
        }

        #endregion

        #region Fields

        public string Name
        {
            get { return "Homeless"; }
        }
        /// <summary>
        /// Тип бездомного
        /// </summary>
        public string Type { get; set; }
        public Texture2D[] Texture { get; set; }
        public Vector2 Location { get; set; }
        public int Health { get; set; } 
        public int Level { get; set; }
        public int Direction { get; set; }
        public int Step { get; set; }
        public Rectangle RectangleMask { get; set; }
        /// <summary>
        /// Размер спрайта
        /// </summary>
        public System.Drawing.Size SpriteSize { get; set; }
        /// <summary>
        /// Столкновение с игровыми обьектами
        /// </summary>
        public bool IsCollision { get; set; }

        #endregion

        #region Methods

        #region Maneger loading

        private void LoadContentHomeless01()
        {
            this.Texture[0] = this.Texture[1] = this.Texture[7] = Game.Content.Load<Texture2D>("Enemies/Homeless/MoveLeft/MoveLeft");
            this.Texture[2] = Game.Content.Load<Texture2D>("Enemies/Homeless/MoveUp/MoveUp");
            this.Texture[3] = this.Texture[4] = this.Texture[5] = Game.Content.Load<Texture2D>("Enemies/Homeless/MoveRight/MoveRight");
            this.Texture[6] = Game.Content.Load<Texture2D>("Enemies/Homeless/MoveDown/MoveDown");
        }
        private void LoadContentHomeless02()
        {
        }

        #endregion

        #region Movement methods

        private void MoveLeft()
        {
            Animation.Location = this.Location = new Vector2(this.Location.X - this.Step, this.Location.Y);
        }
        private void MoveLeftUp()
        {
            Animation.Location = this.Location = new Vector2(this.Location.X - this.Step, this.Location.Y - this.Step);
        }
        private void MoveLeftDown()
        {
            Animation.Location = this.Location = new Vector2(this.Location.X - this.Step, this.Location.Y + this.Step);
        }
        private void MoveRight()
        {
            Animation.Location = this.Location = new Vector2(this.Location.X + this.Step, this.Location.Y);
        }
        private void MoveRightUp()
        {
            Animation.Location = this.Location = new Vector2(this.Location.X + this.Step, this.Location.Y - this.Step);
        }
        private void MoveRightDown()
        {
            Animation.Location = this.Location = new Vector2(this.Location.X + this.Step, this.Location.Y + this.Step);
        }
        private void MoveUp()
        {
            Animation.Location = this.Location = new Vector2(this.Location.X, this.Location.Y - this.Step);
        }
        private void MoveDown()
        {
            Animation.Location = this.Location = new Vector2(this.Location.X, this.Location.Y + this.Step);
        }

        #endregion

        #region Stopped methods

        private void StoppedLeft()
        {
            this.Location = new Vector2(this.Location.X + this.Step, this.Location.Y);
        }
        private void StoppedLeftUp()
        {
            this.Location = new Vector2(this.Location.X + this.Step, this.Location.Y + this.Step);
        }
        private void StoppedLeftDown()
        {
            this.Location = new Vector2(this.Location.X + this.Step, this.Location.Y - this.Step);
        }
        private void StoppedRight()
        {
            this.Location = new Vector2(this.Location.X - this.Step, this.Location.Y);
        }
        private void StoppedRightUp()
        {
            this.Location = new Vector2(this.Location.X - this.Step, this.Location.Y + this.Step);
        }
        private void StoppedRightDown()
        {
            this.Location = new Vector2(this.Location.X - this.Step, this.Location.Y - this.Step);
        }
        private void StoppedUp()
        {
            this.Location = new Vector2(this.Location.X, this.Location.Y - this.Step);
        }
        private void StoppedDown()
        {
            this.Location = new Vector2(this.Location.X, this.Location.Y - this.Step);
        }

        #endregion


        /// <summary>
        /// Возвращает прямоугольник границ
        /// </summary>
        /// <returns></returns>
        public Rectangle GetBounds()
        {
            return new Rectangle((int)this.Location.X, (int)this.Location.Y,
                 this.SpriteSize.Width, this.SpriteSize.Height);
        }
        /// <summary>
        /// Проверка столкновения масок
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public bool CheckCollision(Rectangle rect)
        {
            Rectangle spriterect = new Rectangle((int)Location.X + 10, (int)Location.Y + 40,
                                                  SpriteSize.Width - 30, SpriteSize.Height - 70);
            return spriterect.Intersects(rect);
        }

        public void Stop(int direction)
        {
            Stopped[direction]();
        }

        /// <summary>
        /// Загрузка игрового контента
        /// </summary>
        protected override void LoadContent()
        {
            HomelessManeger[this.Type]();
            base.LoadContent();

        }
        /// <summary>
        /// Обновление игровой логики
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            HealthIndicator.Health = this.Health;
            HealthIndicator.Location = this.Location;
            Animation.Texture = this.Texture[this.Direction];
            Movement[this.Direction]();
            Animation.Update(gameTime);
            
             
            HealthIndicator.Update(gameTime);
            base.Update(gameTime);
        }
        /// <summary>
        /// Отрисовка
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            Animation.Draw(gameTime);
            HealthIndicator.Draw(gameTime);
            base.Draw(gameTime);
        }

        #endregion
    }
}




