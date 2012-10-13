using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace South_Park
{
    class FPS : DrawableGameComponent
    {
        private SpriteBatch SpriteBatch;
        private SpriteFont SpriteFont;

        // Колличество кадров
        private int _Frames = 0; 
        // Время между вызовами метода Draw()
        private double _Seconds = 0;

        public FPS(Game game)
            : base(game)
        {
            SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
            SpriteFont = Game.Content.Load<SpriteFont>("MySpriteFont");
            this.Count = 0;
        } 

        /// <summary>
        /// Колличество кадров в секунду
        /// </summary>
        public int Count { get; set; }

        public override void Update(GameTime gameTime)
        {
            // Время прошедшее с последнего вызова
            _Seconds += gameTime.ElapsedGameTime.TotalSeconds;

            if (_Seconds >= 1)
            {
                this.Count = _Frames;
                _Seconds = _Frames = 0;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _Frames++;
             
            SpriteBatch.Begin();
            SpriteBatch.DrawString(SpriteFont, "FramePereSeconds  " + this.Count, new Vector2(0, 60), Color.Blue);
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
