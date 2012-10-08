using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace South_Park
{
    class Aliens : DrawableGameComponent, IEnemie
    {
        private delegate void DMethods();

        // Словарь команд передвижения
        private Dictionary<int, DMethods> Movement;

        Animation Animation;
        SpriteBatch SpriteBatch;

        public Aliens(Game game)
            : base(game)
        {
            this.Texture = new Texture2D[10];

            this.LoadContent();

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


            this.Location = Vector2.Zero;
            this.Health = 100;
            this.Level = 1;
            this.Step = 1;
            this.Direction = 4;
            this.SpriteSize = new System.Drawing.Size(80, 120);

            SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
            Animation = new Animation(Game, this.Texture[6], this.Location, this.SpriteSize, 2, 8);
        }

        public string Name
        {
            get { return "Alien"; }
        }

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

        #region ** Методы передвижения **

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




        protected override void LoadContent()
        {
            this.Texture[4] = Game.Content.Load<Texture2D>("Enemies/Aliens/MoveRight/MoveRight");

            this.Texture[6] = Game.Content.Load<Texture2D>("Enemies/Aliens/MoveDown/MoveDown");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            Movement[this.Direction]();

            Animation.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Animation.Texture = this.Texture[this.Direction];
            Animation.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
