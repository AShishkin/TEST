using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace South_Park___Coon_and_friends
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Level level;
        private SpriteFont SGC;

        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 724;
            Content.RootDirectory = "Content";
            
        }


        protected override void Initialize()
        {
            level = new Level(this);

            base.Initialize();
        }


        protected override void LoadContent()
        {

            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            SGC = Content.Load<SpriteFont>("SpriteFont1");

          
        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            level.Update(gameTime);
          
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);




            level.Draw(gameTime);

            spriteBatch.Begin();
            spriteBatch.DrawString(SGC, "GarbageCollector  -  " + GC.CollectionCount(0) + " - " + GC.CollectionCount(1) + " - " + GC.CollectionCount(2), new Vector2(15, 10), Color.Violet);
            spriteBatch.DrawString(SGC, "ElapsedGameTime   -  " + gameTime.ElapsedGameTime.Milliseconds, new Vector2(15, 30), Color.Yellow);
            spriteBatch.DrawString(SGC, "TotalMemory          -  " + GC.GetTotalMemory(false) / 1024 + " kb", new Vector2(15, 50), Color.Aqua);
            spriteBatch.End(); 



            base.Draw(gameTime);
        }
    }
}
