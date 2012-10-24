using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace South_Park
{
    // Переписать в структуру
    class CheckPointMove : DrawableGameComponent
    {
        private SpriteBatch SpriteBatch;

        public CheckPointMove(Game game, Vector2 location, int direction)
            : base(game)
        {

            this.LoadContent();

            this.Location = location;
            this.Direction = direction;

            this.RectangleMask = new Rectangle((int)this.Location.X, (int)this.Location.Y, 20, 20);

            SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
        }

        public int Direction { get; set; }

        public Texture2D Texture { get; set; }

        public Vector2 Location { get; set; }

        public Rectangle RectangleMask { get; set; }

        /// <summary>
        /// Проверка столкновения масок
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public bool CheckCollision(Rectangle rect)
        {
            Rectangle spriterect = new Rectangle((int)Location.X, (int)Location.Y, 20, 20);
            return spriterect.Intersects(rect);
        }

        /// <summary>
        /// Возвращает прямоугольник границ 
        /// </summary>
        /// <returns></returns>
        public Rectangle GetBounds()
        {
            return new Rectangle((int)this.Location.X, (int)this.Location.Y, 20, 20);
        }

        protected override void LoadContent()
        {
            this.Texture = Game.Content.Load<Texture2D>("Levels/Index/1");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(this.Texture, this.RectangleMask, Color.White);
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

