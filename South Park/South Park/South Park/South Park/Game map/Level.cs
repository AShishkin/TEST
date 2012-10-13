using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.IO;


namespace South_Park
{
    class Level : DrawableGameComponent
    {
        private static System.Random RSpeed;
        private SpriteBatch SpriteBatch;
        private SpriteFont SpriteFont;

        public ObjectPool<ITawers> OPCameraTawers;
        private ObjectPool<Shell> Wave;
        private ObjectPool<Snowball> Snowballs;

      //  private ObjectPool<Aliens> Aliens;

        private ObjectPool<Homeless> Homeless;

        private Updater Updater;

        private Clouds Clouds;
        public Arrow Arrow;
        public Bubble Bubble;
        private Money Money;
        

        private CheckPointStart[] IndexStart;
        private CheckPointMove[] IndexMove;
        private CheckPointFinish IndexFinish;

        private SoundEffect[] SoundEffect;

        private delegate void MethodsDelegate();
        private delegate void CreateGameObjectDelegate(Vector2 Location);

        // Словарь команд 
        private Dictionary<int, MethodsDelegate> DrawInterface;
        private Dictionary<string, CreateGameObjectDelegate> CreateGameObject;

        private Dictionary<string, Texture2D> TextureBackground;

        public Cartman Cartman;


       

        private int _EnabledSnowballCount = 50; 
        private int _IndexStartCount = 0;
        private int _IndexMoveCount = 0;
        private string[] _LevelName;

        private Collision Collision;

       


        public Level(Game game)
            : base(game)
        {
            SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
            SpriteFont = Game.Content.Load<SpriteFont>("MySpriteFont");
            RSpeed = new System.Random(System.DateTime.Now.Millisecond);
            SoundEffect = new SoundEffect[10];
            this.Fon = new Texture2D[10];

            DrawInterface = new Dictionary<int, MethodsDelegate>
            {
                { 0, this.DrawGameInterface },
                { 1, this.DrawHeroIcon },
                { 2, this.DrawInterfaceString},
            };

            CreateGameObject = new Dictionary<string, CreateGameObjectDelegate>
            {
                { "Null", this.NullCommand },
                { "CameraTawer", this.CreateCamerTawer },
                
            };




            this.Interface = new Texture2D[10];
            this.SpriteSize = new Size(1170, 430);
            this.Location = new Vector2(150, 210);
            this.RectangleMask = new Microsoft.Xna.Framework.Rectangle((int)this.Location.X, (int)this.Location.Y,
                                                                       this.SpriteSize.Width, this.SpriteSize.Height);
            OPCameraTawers = new ObjectPool<ITawers>();
            Wave = new ObjectPool<Shell>();
            Snowballs = new ObjectPool<Snowball>();

          //  Aliens = new ObjectPool<Aliens>();
            Homeless = new ObjectPool<Homeless>();
            Money = new Money(100);
            

            Clouds = new Clouds(Game);

            Arrow = new Arrow(Game);
            Bubble = new Bubble(Game);

            IndexStart = new CheckPointStart[3];
            IndexMove = new CheckPointMove[20];
            IndexFinish = new CheckPointFinish();

            Cartman = new Cartman(Game, new Vector2(204, 308));

            Collision = new Collision();
            

            Updater = new Updater();

            for (int i = 0; i < 50; i++)
                Wave.Add(new Shell(Game, Vector2.Zero, "Wave"));



            for (int i = 0; i < 350; i++)
                OPCameraTawers.Add(new CameraTawer(Game));

            for (int i = 0; i < 50; i++)
                Snowballs.Add(new Snowball(Game));

            this.LoadContent();

            TextureBackground = new Dictionary<string, Texture2D>
            {
                { "CartmanHouse", this.Fon[0] },
            };

            this.Start("content/Levels/CartmanHouse/CartmanHouse.sp");

            SoundEffect[0].Play();

            //this.IndexConstructor
            this.EnemieConstructor();

        }

        private Texture2D[] Fon { get; set; }

        public Texture2D Background { get; set; }

        public Texture2D[] Interface { get; set; }

        public Vector2 Location { get; set; }

        public Microsoft.Xna.Framework.Rectangle RectangleMask { get; set; }

        public Size SpriteSize { get; set; }

        public Microsoft.Xna.Framework.Rectangle GetBounds()
        {
            return new Microsoft.Xna.Framework.Rectangle((int)this.Location.X, (int)this.Location.Y,
                 this.SpriteSize.Width, this.SpriteSize.Height);
        }






        public void Start(string LevelPatch)
        {
            int _X = 0;
            int _Y = 0;
            
            _LevelName = File.ReadAllLines(LevelPatch);

            this.Background = TextureBackground[_LevelName[1]];

            foreach (string str in _LevelName)
            {
                foreach (char ch in str)
                {
                    this.IndexConstructor(ch, new Vector2(_X, _Y));
                    _X += 20;
                }
                _X = 0;
                _Y += 20;
            }

            this.EnemieConstructor();
        }





        




       




        #region ## Null Command ##

        private void NullCommand(Vector2 location)
        {
            return;

        }

        #endregion


        #region ** CameraTawer **

        private void CreateCamerTawer(Vector2 Location)
        {
            for (int i = 0; i < OPCameraTawers.Count; i++)
            {
                if (!Cartman.IsCollision)
                {
                    if (((CameraTawer)OPCameraTawers[i]).Enabled)
                    {
                        Clouds.Create(Location, 0);
                        ((CameraTawer)OPCameraTawers[i]).Health = 100;
                        ((CameraTawer)OPCameraTawers[i]).Location = Location;
                        ((CameraTawer)OPCameraTawers[i]).Enabled = false;
                        break;
                    }
                }
            }
        }

        // Улучшить одинаковые сегменты кода
        private void DrawingCameraTawer(GameTime gameTime, IHeroes Hero)
        {

            if (Hero is Cartman)
            {
                Cartman.Draw(gameTime);
                for (int i = 0; i < OPCameraTawers.Count; i++)
                {
                    if (!((CameraTawer)OPCameraTawers[i]).Enabled)
                    {

                        if (OPCameraTawers[i].CheckCollision(Hero.GetBounds()))
                        {
                            if (Hero.Location.Y < OPCameraTawers[i].Location.Y - 12)
                            {

                                Cartman.Draw(gameTime);

                                ((CameraTawer)OPCameraTawers[i]).Draw(gameTime);
                            }
                            else
                            {

                                ((CameraTawer)OPCameraTawers[i]).Draw(gameTime);
                                Cartman.Draw(gameTime);
                            }
                        }
                        else ((CameraTawer)OPCameraTawers[i]).Draw(gameTime);
                    }



                    
                }
            }
        }
                        
             

                

                  
                    

                

        private void UpdateTawer1(GameTime gameTime)
        {
            for (int i = 0; i < OPCameraTawers.Count; i++)
                if (!((CameraTawer)OPCameraTawers[i]).Enabled)
                    ((CameraTawer)OPCameraTawers[i]).Update(gameTime);
        }

        #endregion


        #region ** Snowball **

        private void DrawSnowball(GameTime gameTime)
        {
            for (int i = 0; i < Snowballs.Count; i++)
                if (!Snowballs[i].Enabled)
                    Snowballs[i].Draw(gameTime);
        }

        private void CreateSnowball(Vector2 Location, int Direction)
        {
            for (int i = 0; i < Snowballs.Count; i++)
                if (Snowballs[i].Enabled)
                {
                    Snowballs[i].Step = RSpeed.Next(5, 8);
                    Snowballs[i].Location = new Vector2(Location.X + 64, Location.Y + 44);
                    Snowballs[i].Enabled = false;
                    Snowballs[i].Direction = Direction;
                    break;
                }
        }



        private void UpdateSnowball(GameTime gameTime)
        {
            for (int i = 0; i < Snowballs.Count; i++)
                if (!Snowballs[i].Enabled)
                    Snowballs[i].Update(gameTime);
        }

        #endregion

        #region ## Отрисовка интерфейса ##

        private void DrawHeroIcon()
        {
            if (Cartman.Health <= 30) SpriteBatch.Draw(this.Interface[2], new Microsoft.Xna.Framework.Rectangle(600, 640, 80, 80),
                                                       Microsoft.Xna.Framework.Color.White);
            if ((Cartman.Health >= 31) && (Cartman.Health <= 80)) 
                SpriteBatch.Draw(this.Interface[6], new Microsoft.Xna.Framework.Rectangle(600, 640, 80, 80),
                    Microsoft.Xna.Framework.Color.White);
            if (Cartman.Health >= 81) SpriteBatch.Draw(this.Interface[1], new Microsoft.Xna.Framework.Rectangle(610, 645, 80, 80),
                                                       Microsoft.Xna.Framework.Color.White);
        }

        private void DrawInterfaceString()
        {
           // SpriteBatch.DrawString(SpriteFont, "Snowball " + Snowballs.Count + "/" + _EnabledSnowballCount,
           //     new Vector2(300, 702), Microsoft.Xna.Framework.Color.Red);
           
            SpriteBatch.DrawString(SpriteFont, Money.Count.ToString(), new Vector2(450, 700), Microsoft.Xna.Framework.Color.White);

            SpriteBatch.DrawString(SpriteFont, "x " + Cartman.ContinuumCount, new Vector2(690, 700), Microsoft.Xna.Framework.Color.White);

            if (Cartman.Health <= 30)
            {
                SpriteBatch.DrawString(SpriteFont, Cartman.Health + "%", new Vector2(615, 702), Microsoft.Xna.Framework.Color.Red);
                SpriteBatch.DrawString(SpriteFont, "South Park", new Vector2(1040, 702), Microsoft.Xna.Framework.Color.Red);
            }
            if ((Cartman.Health >= 31) && (Cartman.Health <= 80))
            {
                SpriteBatch.DrawString(SpriteFont, Cartman.Health + "%", new Vector2(615, 702), Microsoft.Xna.Framework.Color.Yellow);
            }

            if (Cartman.Health >= 81)
            {
                SpriteBatch.DrawString(SpriteFont, Cartman.Health + "%", new Vector2(625, 700), Microsoft.Xna.Framework.Color.Lime);
                SpriteBatch.DrawString(SpriteFont, "South Park", new Vector2(1040, 702), Microsoft.Xna.Framework.Color.White);

            }


        }

        private void DrawGameInterface()
        {
            SpriteBatch.Draw(this.Background, new Microsoft.Xna.Framework.Rectangle(0, 0, 1320, 720),
                             Microsoft.Xna.Framework.Color.White);


            SpriteBatch.Draw(this.Interface[8], this.RectangleMask, Microsoft.Xna.Framework.Color.White);



            SpriteBatch.Draw(this.Interface[0], new Microsoft.Xna.Framework.Rectangle(0, 680, 1320, 40),
                Microsoft.Xna.Framework.Color.White);

            if (Cartman.Health <= 30)
                SpriteBatch.Draw(this.Interface[7], new Microsoft.Xna.Framework.Rectangle(0, 0, 1329, 720),
                    Microsoft.Xna.Framework.Color.White);

            SpriteBatch.Draw(this.Interface[3], new Microsoft.Xna.Framework.Rectangle(1050, 640, 150, 80),
                Microsoft.Xna.Framework.Color.White);
            SpriteBatch.Draw(this.Interface[4], new Microsoft.Xna.Framework.Rectangle(740, 640, 60, 60),
                Microsoft.Xna.Framework.Color.White);
            SpriteBatch.Draw(this.Interface[5], new Microsoft.Xna.Framework.Rectangle(370, 640, 80, 80),
                Microsoft.Xna.Framework.Color.White);

        }

        #endregion


        public void IndexConstructor(char ch, Vector2 location)
        {
            switch (ch)
            {
                case '~': IndexStart[_IndexStartCount++] = new CheckPointStart(Game, location, 0); break;
                case '!': IndexStart[_IndexStartCount++] = new CheckPointStart(Game, location, 1); break;
                case '@': IndexStart[_IndexStartCount++] = new CheckPointStart(Game, location, 2); break;
                case '#': IndexStart[_IndexStartCount++] = new CheckPointStart(Game, location, 3); break;
                case '$': IndexStart[_IndexStartCount++] = new CheckPointStart(Game, location, 4); break;
                case '%': IndexStart[_IndexStartCount++] = new CheckPointStart(Game, location, 5); break;
                case '^': IndexStart[_IndexStartCount++] = new CheckPointStart(Game, location, 6); break;
                case '&': IndexStart[_IndexStartCount++] = new CheckPointStart(Game, location, 7); break;

                case '0': IndexMove[_IndexMoveCount++] = new CheckPointMove(Game, location, 0); break;
                case '1': IndexMove[_IndexMoveCount++] = new CheckPointMove(Game, location, 1); break;
                case '2': IndexMove[_IndexMoveCount++] = new CheckPointMove(Game, location, 2); break;
                case '3': IndexMove[_IndexMoveCount++] = new CheckPointMove(Game, location, 3); break;
                case '4': IndexMove[_IndexMoveCount++] = new CheckPointMove(Game, location, 4); break;
                case '5': IndexMove[_IndexMoveCount++] = new CheckPointMove(Game, location, 5); break;
                case '6': IndexMove[_IndexMoveCount++] = new CheckPointMove(Game, location, 6); break;
                case '7': IndexMove[_IndexMoveCount++] = new CheckPointMove(Game, location, 7); break;

                case '+': IndexFinish = new CheckPointFinish(location); break;
            }  
        }


        public void EnemieConstructor()
        {
            for (int i = 0; i < 1; i++)
                Homeless.Add(new Homeless(Game));



            Homeless[0].Location = new Vector2(IndexStart[0].Location.X - 100, IndexStart[0].Location.Y - 50);
           // Aliens[1].Location = new Vector2(IndexStart[1].Location.X - 100, IndexStart[1].Location.Y);
        }







        protected override void LoadContent()
        {
            this.Fon[0] = Game.Content.Load<Texture2D>("Levels/CartmanHouse/CartmanHouse");
            this.Interface[0] = Game.Content.Load<Texture2D>("Интерфейс/Panel");
            this.Interface[3] = Game.Content.Load<Texture2D>("Интерфейс/Interface02");
            this.Interface[4] = Game.Content.Load<Texture2D>("Интерфейс/Interface03");
            this.Interface[5] = Game.Content.Load<Texture2D>("Интерфейс/Money");
            this.Interface[7] = Game.Content.Load<Texture2D>("Интерфейс/Alert");

            this.Interface[1] = Game.Content.Load<Texture2D>("Интерфейс/Картман/InterfaceCartman01");
            this.Interface[6] = Game.Content.Load<Texture2D>("Интерфейс/Картман/InterfaceCartman02");
            this.Interface[2] = Game.Content.Load<Texture2D>("Интерфейс/Картман/InterfaceCartman03");
            this.Interface[8] = Game.Content.Load<Texture2D>("Анимация/TestBackground");

            SoundEffect[0] = Game.Content.Load<SoundEffect>("Звуки/Уровни/StartLevel");


            base.LoadContent();
        }


        public override void Update(GameTime gameTime)
        {

            Cartman.Update(gameTime);

            Homeless[0].Update(gameTime);


            for (int i = 0; i < IndexMove.Length; i++)
                if (IndexMove[i] != null)
                    Collision.CEnemieWithIndexMove(Homeless[0], IndexMove[i]);


            Collision.CEnemieWithIndexFinish(Homeless[0], IndexFinish, IndexStart[0]);

            Collision.CHeroWithTawer(Cartman, OPCameraTawers, Arrow, Bubble);

            Collision.CEnemiesWithTawer(Homeless[0], OPCameraTawers);

            CreateGameObject[Cartman.CommandsCreateGameObjects](Cartman.Location);

            Clouds.Update(gameTime);
  
            

                
                
            






            if (Arrow.Enabled)
                Arrow.Update(gameTime);

            if (Bubble.Enabled)
                Bubble.Update(gameTime);

            if (OPCameraTawers.Count > 0)
                this.UpdateTawer1(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();

            for (int i = 0; i < 3; i++)
                DrawInterface[i]();

            SpriteBatch.End();


            if (Bubble.Enabled)
                Bubble.Draw(gameTime);

            this.DrawingCameraTawer(gameTime, Cartman);


            Clouds.Draw(gameTime);

            if (Arrow.Enabled)
                Arrow.Draw(gameTime);

            for (int i = 0; i < 3; i++)
                if(IndexStart[i] != null)
                    IndexStart[i].Draw(gameTime);

            for (int i = 0; i < 20; i++)
                if (IndexMove[i] != null)
                    IndexMove[i].Draw(gameTime);


          //  for (int i = 0; i < Homeless.Count - 1; i++)
                Homeless[0].Draw(gameTime);

                

            base.Draw(gameTime);
        }









    }
}



namespace South_Park
{
    class Collision
    {
        /// <summary>
        /// Проверка столкновений
        /// </summary>
        /// <param name="hero">Герой</param>
        /// <param name="tawer">Пул объектов</param>
        /// <param name="arrow">Стрелка</param>
        /// <param name="bubble">Круги</param>
        public void CHeroWithTawer(IHeroes hero, ObjectPool<ITawers> tawer, Arrow arrow, Bubble bubble)
        {
            for (int i = 0; i < tawer.Count; i++)
            {

                if (hero.CheckCollision(tawer[i].GetBounds()))
                {
                    arrow.CreateArrow(new Vector2(tawer[i].Location.X + 10, tawer[i].Location.Y - 80));
                    bubble.Create(new Vector2(tawer[i].Location.X, tawer[i].Location.Y + 35), 0);
                    hero.IsCollision = true;
                    break;
                }
                else
                {
                    bubble.Enabled = arrow.Enabled = false;
                    hero.IsCollision = false;
                }
            }
        }
        /// <summary>
        /// Проверка столкновений
        /// </summary>
        /// <param name="enemie">Враг</param>
        /// <param name="tawer">Пул объектов</param>
        public void CEnemiesWithTawer(IEnemie enemie, ObjectPool<ITawers> tawer)
        {
            for (int i = 0; i < tawer.Count; i++)
            {
                if (enemie.CheckCollision(tawer[i].GetBounds()))
                {
                    enemie.Stop(enemie.Direction);
                    tawer[i].Health--;
                    //!!
                    if (tawer[i].Health < 1)
                    {
                        ((CameraTawer)tawer[i]).Enabled = true;
                        tawer[i].Location = Vector2.Zero;

                    }
                }     
            }
        }
        /// <summary>
        /// Провекра столкновений
        /// </summary>
        /// <param name="enemie">Враг</param>
        /// <param name="checkPointMove">Контрольная точка</param>
        public void CEnemieWithIndexMove(IEnemie enemie, CheckPointMove checkPointMove)
        {
            if (enemie.CheckCollision(checkPointMove.GetBounds()))
                enemie.Direction = checkPointMove.Direction;
        }
        /// <summary>
        /// Проверка столкновений
        /// </summary>
        /// <param name="enemie">Враг</param>
        /// <param name="chechPointFinish">Точка фииша</param>
        /// <param name="checkPointStart">Точка старта</param>
        public void CEnemieWithIndexFinish(IEnemie enemie, CheckPointFinish chechPointFinish,
                                           CheckPointStart checkPointStart)
        {
            if (enemie.CheckCollision(chechPointFinish.GetBounds()))
            {
                enemie.Location = new Vector2(checkPointStart.Location.X - 100, checkPointStart.Location.Y);
                if (enemie.Direction != checkPointStart.Direction)
                    enemie.Direction = checkPointStart.Direction;
            }
        }
    }
}