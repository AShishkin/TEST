using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace South_Park
{
    sealed class Arrow : DrawableGameComponent
    {
        private SpriteBatch SpriteBatch;
        private Animation Animation;

        #region Designer

        public Arrow(Game game)
            : base(game)
        {
            SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
            Animation = new Animation(Game, this.Texture = Game.Content.Load<Texture2D>("Arrow/Arrow"), Vector2.Zero,
                                       this.Size = new System.Drawing.Size(60, 100), 8, 10, 0);
            this.Enabled = false;
        }

        #endregion

        #region Fields

        /// <summary>
        /// Текстура спрайта
        /// </summary>
        public Texture2D Texture { get; set; }
        /// <summary>
        /// Размер спрайта
        /// </summary>
        public System.Drawing.Size Size { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Создает анимацию в указанном месте
        /// </summary>
        /// <param name="location"></param>
        public void CreateArrow(Vector2 location)
        {
            Animation.Location = location;
            this.Enabled = true;
        }
        /// <summary>
        /// Лигика
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            Animation.Update(gameTime);

            base.Update(gameTime);
        }
        /// <summary>
        /// Графика
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            if (this.Enabled)
                Animation.Draw(gameTime);

            base.Draw(gameTime);
        }

        #endregion
    }
}
    

