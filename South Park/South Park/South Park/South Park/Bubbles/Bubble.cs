﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace South_Park
{
    sealed class Bubble : DrawableGameComponent
    {
        private SpriteBatch SpriteBatch;
        private Animation Animation;
        private Dictionary<int, Texture2D> Textures;


        #region Designer

        public Bubble(Game game)
            : base(game)
        {
            SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
            Textures = new Dictionary<int, Texture2D>();

            this.LoadContent();
            this.Enabled = false;

            Animation = new Animation(Game, Textures[0], Vector2.Zero,
                                       this.SpriteSize = new System.Drawing.Size(80, 40), 8, 10, 0);   
        }

        #endregion

        #region Fields

        /// <summary>
        /// Размер спрайта
        /// </summary>
        public System.Drawing.Size SpriteSize { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Создает анимацию в указанном месте
        /// </summary>
        /// <param name="location"></param>
        public void Create(Vector2 location, int index)
        {
            Animation.Location = location;

            this.Enabled = true;

            Animation.Texture = Textures[index];
        }
        // Контент
        protected override void LoadContent()
        {
            Textures.Add(0, Game.Content.Load<Texture2D>("Круги/Bubble1"));

            base.LoadContent();
        }
        /// <summary>
        /// Логика
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if(this.Enabled)
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