using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace South_Park___Coon_and_friends
{
    class Level : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Eric eric;


        public Level(Game game)
            : base(game)
        {
            

            int _X = 0;
            int _Y = 0;


            for (int i = 0; i < 8; i++ )
            {
                for (int j = 0; j < 17; j++)
                {
                    Map.GameMap[i, j].Enabled = true;
                    Map.GameMap[i, j].i = i;
                    Map.GameMap[i, j].j = j;
                    Map.GameMap[i, j].Location = new Vector2(_X, _Y);
                    _X += 80;
                }
                _X = 0;
                _Y += 80;
            }
            this.LoadContent();
            eric = new Eric(game, Map.GameMap[3, 3].Location);
        }

        Texture2D Texture { get; set; }
        Texture2D Texture2 { get; set; }
        Texture2D Texture3 { get; set; }


        protected override void LoadContent()
        {
           
            
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            this.Texture = Game.Content.Load<Texture2D>("Map/Cell/Cell");
            this.Texture2 = Game.Content.Load<Texture2D>("Map/CellNow");
            this.Texture3 = Game.Content.Load<Texture2D>("Map/CellNowFalse");
            base.LoadContent();
        }


        public override void Update(GameTime gameTime)
        {
            eric.Update(gameTime);
            ManagerCollision.HCollisionWithCell(eric);
            base.Update(gameTime);
        }




        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    spriteBatch.Draw(this.Texture, new Rectangle((int)Map.GameMap[i, j].Location.X, (int)Map.GameMap[i, j].Location.Y,
                                     Map.GameMap[i, j].Size.Width, Map.GameMap[i, j].Size.Height), Color.White);
                }
            }

            if (eric.IsCollision)
            {
                
                if(!Map.GameMap[ManagerCollision.RowIndexCollision, ManagerCollision.CollumnIndexCollision].Enabled)
                    spriteBatch.Draw(this.Texture3, new Rectangle((int)ManagerCollision.LocationCellCollistion.X,
                                                              (int)ManagerCollision.LocationCellCollistion.Y, 80, 80), Color.White);
                else spriteBatch.Draw(this.Texture2, new Rectangle((int)ManagerCollision.LocationCellCollistion.X,
                                                              (int)ManagerCollision.LocationCellCollistion.Y, 80, 80), Color.White);

            }




            spriteBatch.End();
            eric.Draw(gameTime);

            base.Draw(gameTime);
        }



    }
}
