using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Collections.Generic;

namespace South_Park___Coon_and_friends
{
    class Level : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Eric eric;

        public ObjectPool<ITowers> OPCameraTowers;

        private delegate void CreateGameObjectDelegate(Vector2 Location);
        private Dictionary<string, CreateGameObjectDelegate> CreateGameObject;
       // ManagerTower MTower;


        public Level(Game game)
            : base(game)
        {
            Camera2D.Zoom = 1f;
          //  MTower = new ManagerTower(game);
            this.Start("content/Levels/EricHouse/CartmanHouse.sou");

            CreateGameObject = new Dictionary<string, CreateGameObjectDelegate>
            {
                { "Null", this.NullCommand },
                { "CameraTower", this.CreateCamerTower },

            };

            OPCameraTowers = new ObjectPool<ITowers>();

            for (int i = 0; i < 95; i++)
               OPCameraTowers.Add(new CameraTower(Game));
            
            
            this.LoadContent();
            
            
        }

        private void NullCommand(Vector2 location)
        {
            return;

        }

        private void CreateCamerTower(Vector2 Location)
        {
            if (Map.GameMap[ManagerCollision.RowIndexCollision, ManagerCollision.CollumnIndexCollision].Enabled)
            {
                for (int i = 0; i < OPCameraTowers.Count; i++)
                {
                    if (((CameraTower)OPCameraTowers[i]).Enabled)
                    {
                        Map.GameMap[ManagerCollision.RowIndexCollision, ManagerCollision.CollumnIndexCollision].Enabled = false;
                        // tawerListEnabled.Add(i);
                        // Clouds.Create(Location, 0);

                        ((CameraTower)OPCameraTowers[i]).Health = 100;
                        ((CameraTower)OPCameraTowers[i]).Location = new Vector2((int)ManagerCollision.LocationCellCollistion.X, (int)ManagerCollision.LocationCellCollistion.Y);
                        ((CameraTower)OPCameraTowers[i]).Enabled = false;
                        break;
                    }
                }
            }
        }

        private void DrawingCameraTower(GameTime gameTime, IHero Hero)
        {


                for (int i = 0; i < OPCameraTowers.Count; i++)
                {
                    if (!((CameraTower)OPCameraTowers[i]).Enabled)
                    {


                        ((CameraTower)OPCameraTowers[i]).Draw(gameTime);
                    }
                        
                   
                }
            }

        private void UpdateTower1(GameTime gameTime)
        {
            for (int i = 0; i < OPCameraTowers.Count; i++)
                ((CameraTower)OPCameraTowers[i]).Update(gameTime);
        }




        public void Start(string patch)
        {
            string[] _LevelName = File.ReadAllLines(patch);
            int _X = 0;
            int _Y = 0;

            for (int i = 0; i < 8; i++)
            {
                string str = _LevelName[i];

                for (int j = 0; j < 16; j++)
                {
                    char ch = str[j];

                    if (ch == '+')
                    {
                        Map.GameMap[i, j].Enabled = true;
                        Map.GameMap[i, j].i = i;
                        Map.GameMap[i, j].j = j;
                        Map.GameMap[i, j].Location = new Vector2(_X, _Y);

                    }
                    _X += 80;
                }
                _X = 0;
                _Y += 80;
            }
            eric = new Eric(Game, Map.GameMap[3, 3].Location);
        }






        Texture2D Texture { get; set; }
        Texture2D Texture2 { get; set; }
        Texture2D Texture3 { get; set; }

        private Texture2D Background { get; set; }

        protected override void LoadContent()
        {
           
            
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            this.Texture = Game.Content.Load<Texture2D>("Map/Cell/Cell");
            this.Texture2 = Game.Content.Load<Texture2D>("Map/CellNow");
            this.Texture3 = Game.Content.Load<Texture2D>("Map/CellNowFalse");
            this.Background = ManagerBackground.GetBackground(Game, "EricHouse");
            base.LoadContent();
        }


        public override void Update(GameTime gameTime)
        {

            if (eric.Location.X <= 600)
            {
                if (eric.Location.Y <= 400)
                {
                    if (Camera2D.Zoom < 1.1f) Camera2D.Zoom += 0.0007f;
                }
                else
                {
                    if (Camera2D.Zoom > 0.95f) Camera2D.Zoom -= 0.0007f;

                }
                
                
                
                    

            }
            else
            {
                if (eric.Location.Y <= 400)
                {
                    if (Camera2D.Zoom < 1.02f) Camera2D.Zoom += 0.0007f;
                }
                else
                {
                    if (Camera2D.Zoom > 0.95f) Camera2D.Zoom -= 0.0007f;

                }
               


                if (Camera2D.Zoom > 1f) Camera2D.Zoom -= 0.0007f;

            }






            


            if (OPCameraTowers.Count > 0)
                this.UpdateTower1(gameTime);
            
            

            eric.Update(gameTime);
            CreateGameObject[eric.CommandsCreateGameObjects](eric.Location);
            ManagerCollision.HCollisionWithCell(eric);
            base.Update(gameTime);
        }




        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null,
                              Camera2D.get_transformation(Game.GraphicsDevice));

            spriteBatch.Draw(this.Background, new Rectangle(0, 0, 1350, 762), Color.White);



            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    spriteBatch.Draw(this.Texture, new Rectangle((int)Map.GameMap[i, j].Location.X, (int)Map.GameMap[i, j].Location.Y,
                                     Map.GameMap[i, j].Size.Width, Map.GameMap[i, j].Size.Height), Color.White);
                }
            }




            if (eric.IsCollision)
            {

                if (!Map.GameMap[ManagerCollision.RowIndexCollision, ManagerCollision.CollumnIndexCollision].Enabled)
                    spriteBatch.Draw(this.Texture3, new Rectangle((int)ManagerCollision.LocationCellCollistion.X,
                                                              (int)ManagerCollision.LocationCellCollistion.Y, 80, 80), Color.White);
                else spriteBatch.Draw(this.Texture2, new Rectangle((int)ManagerCollision.LocationCellCollistion.X,
                                                              (int)ManagerCollision.LocationCellCollistion.Y, 80, 80), Color.White);

            }

            


            spriteBatch.End();
            this.DrawingCameraTower(gameTime, eric);

            

            eric.Draw(gameTime);

            base.Draw(gameTime);
        }



    }
}
