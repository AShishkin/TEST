using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace South_Park
{
    class Snowball : DrawableGameComponent
    {
        SpriteBatch _SpriteBatch;

        public Snowball(Game game)
            : base(game)
        {
            _SpriteBatch = new SpriteBatch(Game.GraphicsDevice);

            this.Location = Vector2.Zero;
            this.Loss = 15;
            this.Direction = 3;
            this.Enabled = true;
            this.SpriteSize = new System.Drawing.Size(15, 15);
            this.Texture = Game.Content.Load<Texture2D>("Анимация/Snowball");

            this.RectangleMask = new Rectangle((int)this.Location.X, (int)this.Location.Y, 
                                                this.SpriteSize.Width, this.SpriteSize.Height);
            this.Step = 6; 
        }


        public Texture2D Texture { get; set; }

        public Vector2 Location { get; set; }

        public int Direction { get; set; }

        public int Step { get; set; }

        /// <summary>
        /// Урон
        /// </summary>
        public int Loss { get; set; }

        /// <summary>
        /// Размер спрайта
        /// </summary>
        public System.Drawing.Size SpriteSize { get; set; }

        /// <summary>
        /// Маска коллизии
        /// </summary>
        public Rectangle RectangleMask { get; set; }

        /// <summary>
        /// Возвращает прямоугольник границ 
        /// </summary>
        /// <returns></returns>
        public Rectangle GetBounds()
        {
            return new Microsoft.Xna.Framework.Rectangle((int)this.Location.X, (int)this.Location.Y,
                 this.SpriteSize.Width, this.SpriteSize.Height);
        }

        /// <summary>
        /// Проверка столкновения масок
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public bool CheckCollision(Rectangle rect)
        {
            Microsoft.Xna.Framework.Rectangle spriterect =
                new Microsoft.Xna.Framework.Rectangle((int)Location.X, (int)Location.Y,
                    SpriteSize.Width, SpriteSize.Height);

            return spriterect.Intersects(rect);
        }

        #region ** Движение **

        private void MoveLeft()
        {
            this.Location = new Vector2(this.Location.X - this.Step, this.Location.Y);
        }

        private void MoveLeftUp()
        {
            this.Location = new Vector2(this.Location.X - this.Step, this.Location.Y - this.Step);
        }

        private void MoveLeftDown()
        {
            this.Location = new Vector2(this.Location.X - this.Step, this.Location.Y + this.Step);
        }

        private void MoveRight()
        {
            this.Location = new Vector2(this.Location.X + this.Step, this.Location.Y);
        }

        private void MoveRightUp()
        {
            this.Location = new Vector2(this.Location.X + this.Step, this.Location.Y - this.Step);
        }

        private void MoveRightDown()
        {
            this.Location = new Vector2(this.Location.X + this.Step, this.Location.Y + this.Step);
        }

        private void MoveUp()
        {
            this.Location = new Vector2(this.Location.X, this.Location.Y - this.Step);
        }

        private void MoveDown()
        {
            this.Location = new Vector2(this.Location.X, this.Location.Y + this.Step);
        }

        #endregion

        public override void Update(GameTime gameTime)
        {
            switch (this.Direction)
            {
                case 0:
                    this.MoveLeft();
                    break;
                case 1:
                    this.MoveUp();
                    break;
                case 2:
                    this.MoveRight();
                    break;
                case 3:
                    this.MoveDown();
                    break;
            }


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _SpriteBatch.Begin();
            _SpriteBatch.Draw(this.Texture, new Rectangle((int)this.Location.X, (int)this.Location.Y, 15, 15) , Color.White);
            _SpriteBatch.End();
            base.Draw(gameTime);
        }


    }
}
