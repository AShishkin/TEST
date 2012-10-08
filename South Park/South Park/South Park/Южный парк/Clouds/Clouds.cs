using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace South_Park
{
    sealed class Clouds : DrawableGameComponent
    {
        private SpriteBatch SpriteBatch;
        private Animation Animation;

        private Dictionary<int, Texture2D> Textures;

        public Clouds(Game game) 
            : base(game) 
        {
            SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
            Textures = new Dictionary<int, Texture2D>();

            this.LoadContent();
            this.Enabled = false;

            Animation = new Animation(Game, Textures[0], Vector2.Zero, 
                                      this.SpriteSize = new System.Drawing.Size(100, 100), 8, 10, 0);
        }

        /// <summary>
        /// Размер спрайта
        /// </summary>
        public System.Drawing.Size SpriteSize { get; set; }

        /// <summary>
        /// Проигрывает один цикл анимации
        /// </summary>
        /// <param name="location">Положение</param>
        public void Create(Vector2 location, int index)
        {
            Animation.Location = location;

            this.Enabled = true;

            Animation.Texture = Textures[index];
        }

        protected override void LoadContent()
        {
            Textures.Add(0, Game.Content.Load<Texture2D>("Облака/Clouds")); 

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (Animation.CurrentFrame == Animation.Frames - 1)
            {
                this.Enabled = false;
                Animation.CurrentFrame = 0;
            }
            else 
                if(this.Enabled)
                    Animation.Update(gameTime);
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if(this.Enabled)
                Animation.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
