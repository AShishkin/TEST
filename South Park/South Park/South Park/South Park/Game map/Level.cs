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
        private ObjectPool<Laser> Wave;
        private ObjectPool<Snowball> Snowballs;

      //  private ObjectPool<Aliens> Aliens;

        private ObjectPool<Homeless> OPHomeless;

        private Updater Updater;

        private Clouds Clouds;
        public Arrow Arrow;
        public Bubble Bubble;
        private Money Money;

        

        private CheckPointStart[] CheckPointsStart;
        private CheckPointMove[] CheckPointsMove;
        private CheckPointFinish[] CheckPointsFinish;

        private SoundEffect[] SoundEffect;

        private delegate void MethodsDelegate();
        private delegate void CreateGameObjectDelegate(Vector2 Location);


        private Dictionary<string, CreateGameObjectDelegate> CreateGameObject;



        public Cartman Cartman;


       

        private int _EnabledSnowballCount = 50;
 
        private int _CheckPointsStartCount = 0;
        private int _CheckPointsMoveCount = 0;
        private int _CheckPointsFinishCount = 0;

        private string[] _LevelName;

        private ManagerCollision MCollision;
        private ManagerCheckPoints MCheckPoints;
        private ManagerEnemies MEnemies;
        private ManagerDrawingInterface MDInterface;


        private TawerCounter TCounter;


        public Level(Game game)
            : base(game)
        {
            SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
            SpriteFont = Game.Content.Load<SpriteFont>("MySpriteFont");
            RSpeed = new System.Random(System.DateTime.Now.Millisecond);
            SoundEffect = new SoundEffect[10];
            this.Fon = new Texture2D[10];

         

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
            Wave = new ObjectPool<Laser>();
            Snowballs = new ObjectPool<Snowball>();

          //  Aliens = new ObjectPool<Aliens>();
            OPHomeless = new ObjectPool<Homeless>();
            Money = new Money(100);
            

            Clouds = new Clouds(Game);

            Arrow = new Arrow(Game);
            Bubble = new Bubble(Game);

            


            CheckPointsStart = new CheckPointStart[4];
            CheckPointsMove = new CheckPointMove[20];
            CheckPointsFinish = new CheckPointFinish[4];

            Cartman = new Cartman(Game, new Vector2(204, 308));

            MCollision = new ManagerCollision();
            MCheckPoints = new ManagerCheckPoints();
            MEnemies = new ManagerEnemies();

            TCounter = new TawerCounter();

            Updater = new Updater();

            for (int i = 0; i < 50; i++)
                Wave.Add(new Laser(Game, Vector2.Zero, "Wave"));



            for (int i = 0; i < 350; i++)
                OPCameraTawers.Add(new CameraTawer(Game));

            for (int i = 0; i < 50; i++)
                Snowballs.Add(new Snowball(Game));

            this.LoadContent();



            this.Start("content/Levels/CartmanHouse/CartmanHouse.sp");

            SoundEffect[0].Play();

    

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

            MDInterface = new ManagerDrawingInterface(Game, Game.Content.Load<Texture2D>("Levels/CartmanHouse/CartmanHouse"));

            foreach (string str in _LevelName)
            {
                foreach (char ch in str)
                {
                    if (MCheckPoints.CheckPointConstruct(Game, ch, new Vector2(_X, _Y)) is CheckPointStart)
                        CheckPointsStart[_CheckPointsStartCount++] = (CheckPointStart)MCheckPoints.CheckPointConstruct(Game, ch, new Vector2(_X, _Y));
                    else if(MCheckPoints.CheckPointConstruct(Game, ch, new Vector2(_X, _Y)) is CheckPointMove)
                        CheckPointsMove[_CheckPointsMoveCount++] = (CheckPointMove)MCheckPoints.CheckPointConstruct(Game, ch, new Vector2(_X, _Y));
                    else if (MCheckPoints.CheckPointConstruct(Game, ch, new Vector2(_X, _Y)) is CheckPointFinish)
                        CheckPointsFinish[_CheckPointsFinishCount++] = (CheckPointFinish)MCheckPoints.CheckPointConstruct(Game, ch, new Vector2(_X, _Y)); 
                    _X += 20;
                }
                _X = 0;
                _Y += 20;
            }

           // for (int i = 0; i < 1; i++)
                OPHomeless.Add((Homeless)MEnemies.Construct(Game, "Homeless"));
                OPHomeless[0].Location = new Vector2(CheckPointsStart[0].Location.X - 100, CheckPointsStart[0].Location.Y - 50);

           
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
                        MDInterface.TawerCountIndicator = ++TCounter.Count;
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





        protected override void LoadContent()
        {

            

            SoundEffect[0] = Game.Content.Load<SoundEffect>("Звуки/Уровни/StartLevel");


            base.LoadContent();
        }


        public override void Update(GameTime gameTime)
        {

            Cartman.Update(gameTime);



            OPHomeless[0].Update(gameTime);


            for (int i = 0; i < CheckPointsMove.Length; i++)
                if (CheckPointsMove[i] != null)
                    MCollision.CEnemieWithIndexMove(OPHomeless[0], CheckPointsMove[i]);


            MCollision.CEnemieWithIndexFinish(OPHomeless[0], CheckPointsFinish[0], CheckPointsStart[0]);
            MCollision.CEnemiesWithTawerFireZone(OPHomeless[0], OPCameraTawers);
            MCollision.CHeroWithTawer(Cartman, OPCameraTawers, Arrow, Bubble);
            MCollision.CEnemiesWithTawer(OPHomeless[0], OPCameraTawers, Clouds, TCounter.Count);
            


            MDInterface.HelthIndicator = Cartman.Health;
            MDInterface.MoneyIndicator = Money.Count;
            MDInterface.CartmanContinnumIndicator = Cartman.ContinuumCount;



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


            

            
            MDInterface.Draw(gameTime);

            if (Bubble.Enabled)
                Bubble.Draw(gameTime);

            this.DrawingCameraTawer(gameTime, Cartman);

            Cartman.Draw(gameTime);

            Clouds.Draw(gameTime);

            if (Arrow.Enabled)
                Arrow.Draw(gameTime);

            for (int i = 0; i < 3; i++)
                if(CheckPointsStart[i] != null)
                    CheckPointsStart[i].Draw(gameTime);

            for (int i = 0; i < 20; i++)
                if (CheckPointsMove[i] != null)
                    CheckPointsMove[i].Draw(gameTime);


          //  for (int i = 0; i < Homeless.Count - 1; i++)
                OPHomeless[0].Draw(gameTime);

    

            base.Draw(gameTime);
        }









    }
}



