using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace South_Park
{
    sealed class CheckPointStart : DrawableGameComponent
    {
        private SpriteBatch SpriteBatch;
        private SpriteFont SpriteFont;
        private Dictionary<int, Texture2D> Textures;

        public CheckPointStart(Game game, Vector2 location, int direction)
            : base(game)
        {
            SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
            Textures = new Dictionary<int, Texture2D>();

            this.LoadContent();
            this.Location = location;
            this.Direction = direction;
            this.Time = 100;
        }

        /// <summary>
        /// Направление
        /// </summary>
        public int Direction { get; set; }
        /// <summary>
        /// Положение
        /// </summary>
        public Vector2 Location { get; set; }
        /// <summary>
        /// Время
        /// </summary>
        public int Time { get; set; }
        protected override void LoadContent()
        {
            SpriteFont = Game.Content.Load<SpriteFont>("MySpriteFont");

            Textures.Add(0, Game.Content.Load<Texture2D>("Levels/Index/IndexStart/IndexStartLeft"));
            Textures.Add(1, Game.Content.Load<Texture2D>("Levels/Index/IndexStart/IndexStartLeftUp"));
            Textures.Add(2, Game.Content.Load<Texture2D>("Levels/Index/IndexStart/IndexStartUp"));
            Textures.Add(3, Game.Content.Load<Texture2D>("Levels/Index/IndexStart/IndexStartRightUp"));
            Textures.Add(4, Game.Content.Load<Texture2D>("Levels/Index/IndexStart/IndexStartRight"));
            Textures.Add(5, Game.Content.Load<Texture2D>("Levels/Index/IndexStart/IndexStartRightDown"));
            Textures.Add(6, Game.Content.Load<Texture2D>("Levels/Index/IndexStart/IndexStartDown"));
            Textures.Add(7, Game.Content.Load<Texture2D>("Levels/Index/IndexStart/IndexStartLeftDown"));

            base.LoadContent();
        }
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(Textures[this.Direction], new Rectangle((int)this.Location.X, (int)this.Location.Y, 90, 90), Color.White);
            SpriteBatch.DrawString(SpriteFont, this.Time.ToString(), new Vector2(this.Location.X + 15, this.Location.Y + 37), Color.White);
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
   

    

