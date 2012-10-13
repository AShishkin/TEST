using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Windows;
using System.Xml;
using System.Collections.Generic;

namespace South_Park
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        SpriteFont SGC;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        private static Random _Index = new Random(DateTime.Now.Millisecond);

        private List<String> Title;

        Level Map;
        FPS FramePerSeconds;

        public Game1()
        {
            Title = new List<string>();

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1320;
            graphics.PreferredBackBufferHeight = 720;


            this.IsMouseVisible = true;


            Title.Add(South_Park.Title.rTitle.Str0 );
                   
          



         

            Content.RootDirectory = "Content";

          //  BWriter = new BinaryWriter(new FileStream("C:/Users/Lamborgini/documents/visual studio 2010/Projects/South Park/South Park/South ParkContent/Title/Title.sp",FileMode.Create));
          //  BReader = new BinaryReader(new FileStream("C:/Users/Lamborgini/documents/visual studio 2010/Projects/South Park/South Park/South ParkContent/Title/Title.sp", FileMode.Open));

            this.HeadingName();

            
        }

        private void HeadingName()
        {
            
           
            try
            {


                Window.Title = "South Park : " + Title[_Index.Next(0, Title.Count)];
                
            }
            catch
            {
                Window.Title = "South Park : Error 666";
            }
        }

        protected override void Initialize()
        {
            Map = new Level(this);
            FramePerSeconds = new FPS(this);

            base.Initialize();
        }

        protected override void LoadContent()
        {


            SGC = Content.Load<SpriteFont>("MySpriteFont");

            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (this.IsActive)
            {
                Map.Update(gameTime);
                FramePerSeconds.Update(gameTime);
            }



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Gray);
            Map.Draw(gameTime);
            FramePerSeconds.Draw(gameTime);

            spriteBatch.Begin();



            spriteBatch.DrawString(SGC, "Garbage collector  " + GC.CollectionCount(0) + " - " + GC.CollectionCount(1) + " - " + GC.CollectionCount(2), new Vector2(0, 40), Color.Blue);
            spriteBatch.DrawString(SGC, "Direction                   " + Map.Cartman.Direction, new Vector2(0, 80), Color.Blue);
            spriteBatch.DrawString(SGC, "InactivityTime          " + Map.Cartman.InactivityTime, new Vector2(0, 100), Color.Blue);
            spriteBatch.DrawString(SGC, "IsCollision                 " + Map.Cartman.IsCollision, new Vector2(0, 120), Color.Blue);
            spriteBatch.DrawString(SGC, "Location                    " + Map.Cartman.Location, new Vector2(0, 140), Color.Blue);
            spriteBatch.DrawString(SGC, "ElapsedGameTime     " + gameTime.ElapsedGameTime.Milliseconds, new Vector2(0, 160), Color.Blue);
            spriteBatch.DrawString(SGC, "TotalMemory            " + GC.GetTotalMemory(false) / 1024 + " kb", new Vector2(0, 180), Color.Blue);
            spriteBatch.DrawString(SGC, "===           " + System.Windows.Forms.Border3DStyle.Bump , new Vector2(0, 200), Color.Blue);
            spriteBatch.End();


            base.Draw(gameTime);
        } 
    }
}
