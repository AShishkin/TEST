using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;

namespace South_Park
{
    class Shell : DrawableGameComponent
    {
        Animation _SpriteAnimation;
        SpriteBatch _SpriteBatch;

        public Shell(Game game, Vector2 Location, string Type)
            : base(game)
        {
            this.Type = Type;
            this.LoadContent();
            this.Location = Location;
            this.SpriteSize = new Size(20, 20);
            this.Availability = true;
            this.Diretcion = 0;


            _SpriteAnimation = new Animation(Game, this.Texture, this.Location, this.SpriteSize, 3, 8);
            _SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
        }

        private string Type { get; set; }

        public int Diretcion { get; set; }

        /// <summary>
        /// Размер спрайта
        /// </summary>
        public Size SpriteSize { get; set; }

        public Texture2D Texture { get; set; }

        public Vector2 Location { get; set; }

        public bool Availability { get; set; }


        #region ** Движение **

        private void MoveLeft()
        {
            _SpriteAnimation.Location = this.Location = new Vector2(this.Location.X - 4, this.Location.Y);
        }

        private void MoveLeftUp()
        {
            _SpriteAnimation.Location = this.Location = new Vector2(this.Location.X - 4, this.Location.Y - 4);
        }

        private void MoveLeftDown()
        {
            _SpriteAnimation.Location = this.Location = new Vector2(this.Location.X - 4, this.Location.Y + 4);
        }

        private void MoveRight()
        {
            _SpriteAnimation.Location = this.Location = new Vector2(this.Location.X + 4, this.Location.Y);
        }

        private void MoveRightUp()
        {
            _SpriteAnimation.Location = this.Location = new Vector2(this.Location.X + 4, this.Location.Y - 4);
        }

        private void MoveRightDown()
        {
            _SpriteAnimation.Location = this.Location = new Vector2(this.Location.X + 4, this.Location.Y + 4);
        }

        private void MoveUp()
        {
            _SpriteAnimation.Location = this.Location = new Vector2(this.Location.X, this.Location.Y - 4);
        }

        private void MoveDown()
        {
            _SpriteAnimation.Location = this.Location = new Vector2(this.Location.X, this.Location.Y + 4);
        }

        #endregion

        /// <summary>
        /// Маска коллизии
        /// </summary>
        public Microsoft.Xna.Framework.Rectangle RectangleMask { get; set; }

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

        /// <summary>
        /// Возвращает прямоугольник границ 
        /// </summary>
        /// <returns></returns>
        public Microsoft.Xna.Framework.Rectangle GetBounds()
        {
            return new Microsoft.Xna.Framework.Rectangle((int)this.Location.X, (int)this.Location.Y,
                 this.SpriteSize.Width, this.SpriteSize.Height);
        }

        protected override void LoadContent()
        {
            switch (this.Type)
            {
                case "Wave" :
                    if (this.Texture != null)
                        this.Texture.Dispose();
                    
                    this.Texture = Game.Content.Load<Texture2D>("Анимация/Башни/Башня1/Wave");
                    break;

                default:
                    if (this.Texture != null)
                        this.Texture.Dispose();
                    break;
            }

            base.LoadContent();
        }
        

        public override void Update(GameTime gameTime)
        {
            switch (this.Diretcion)
            {
                case 0:
                    this.MoveDown();
                    break;
                case 1:
                    this.MoveLeftDown();
                    break;
                case 2:
                    this.MoveLeft();
                    break;
                case 3:
                    this.MoveLeftUp();
                    break;
                case 4:
                    this.MoveUp();
                    break;
                case 5:
                    this.MoveRightUp();
                    break;
                case 6:
                    this.MoveRight();
                    break;
                case 7:
                    this.MoveRightDown();
                    break;
            }

            _SpriteAnimation.Update(gameTime); 
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _SpriteBatch.Begin();
            _SpriteAnimation.Draw(gameTime);
            _SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
