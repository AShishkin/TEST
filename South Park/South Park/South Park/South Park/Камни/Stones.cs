using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace South_Park
{
    class Stones : DrawableGameComponent
    {
        SpriteBatch _SpriteBatch;

        public Stones(Game game, Vector2 location)
            : base(game)
        {
            _SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
            this.Texture = Game.Content.Load<Texture2D>("Камни/Stones"); 
            this.Location = location;
            this.SpriteSize = new System.Drawing.Size(60, 60);
            this.RectangleMask = new Rectangle((int)this.Location.X, (int)this.Location.Y,
                                               this.SpriteSize.Width, this.SpriteSize.Height);

            this.Enabled = false;
        }

        public Texture2D Texture { get; set; }

        public Vector2 Location { get; set; }

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

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _SpriteBatch.Begin();
            _SpriteBatch.Draw(this.Texture, this.RectangleMask, Color.Wheat);
            _SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
        
    

