using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace South_Park 
{
    class Animation : DrawableGameComponent
    {
        private SpriteBatch SpriteBatch;

        #region Designer

        /// <summary>
        /// Конструктор анимации 
        /// </summary>
        /// <param name="game">Текущая игра</param>
        /// <param name="texture2D">Текстура анимации</param>
        public Animation(Game game, Texture2D texture2D)
            : base(game)
        {
            SpriteBatch = new SpriteBatch(game.GraphicsDevice);

            this.Texture = texture2D;
            this.Location = Vector2.Zero;
            this.Size = new System.Drawing.Size(80, 80);
            this.Frames = 1;
            this.Frequency = 10;
            this.CurrentFrame = this.StartFrame = 0;
            this.TimeFrame = (float)1 / this.Frequency;   
        }
        /// <summary>
        /// Конструктор анимации 
        /// </summary>
        /// <param name="game">Текущая игра</param>
        /// <param name="texture2D">Текстура анимации</param>
        /// <param name="location">Положение анимации</param>
        public Animation(Game game, Texture2D texture2D, Vector2 location)
            : base(game)
        {
            SpriteBatch = new SpriteBatch(game.GraphicsDevice);

            this.Texture = texture2D;
            this.Location = location;
            this.Size = new System.Drawing.Size(80, 80);
            this.Frames = 1;
            this.Frequency = 10;
            this.CurrentFrame = this.StartFrame = 0;
            this.TimeFrame = (float)1 / this.Frequency;    
        }
        /// <summary>
        /// Конструктор анимации 
        /// </summary>
        /// <param name="game">Текущая игра</param>
        /// <param name="texture2D">Текстура анимации</param>
        /// <param name="location">Положение анимации</param>
        /// <param name="size">Размер кадра анимации</param>
        public Animation(Game game, Texture2D texture2D, Vector2 location, System.Drawing.Size size)
            : base(game)
        {
            SpriteBatch = new SpriteBatch(game.GraphicsDevice);

            this.Texture = texture2D;
            this.Location = location;
            this.Size = size;
            this.Frames = 1;
            this.Frequency = 10;
            this.CurrentFrame = this.StartFrame = 0;
            this.TimeFrame = (float)1 / this.Frequency; 
        }
        /// <summary>
        /// Конструктор анимации 
        /// </summary>
        /// <param name="game">Текущая игра</param>
        /// <param name="texture2D">Текстура анимации</param>
        /// <param name="location">Положение анимации</param>
        /// <param name="size">Размер кадра анимации</param>
        /// <param name="frames">Колличество кадров анимации</param>
        public Animation(Game game, Texture2D texture2D, Vector2 location, System.Drawing.Size size, int frames)
            : base(game)
        {
            SpriteBatch = new SpriteBatch(game.GraphicsDevice);

            this.Texture = texture2D;
            this.Location = location;
            this.Size = size;
            this.Frames = frames;
            this.Frequency = 10;
            this.CurrentFrame = this.StartFrame = 0;
            this.TimeFrame = (float)1 / this.Frequency;
        }
        /// <summary>
        /// Конструктор анимации
        /// </summary>
        /// <param name="game">Текущая игра</param>
        /// <param name="texture2D">Текущая анимация</param>
        /// <param name="location">Положение анимации</param>
        /// <param name="size">Размер кадра анимации</param>
        /// <param name="frames">Колличество кадров анимации</param>
        /// <param name="frequency">Частота кадров</param>
        public Animation(Game game, Texture2D texture2D, Vector2 location, System.Drawing.Size size, int frames,
                               int frequency)
            : base(game)
        {
            SpriteBatch = new SpriteBatch(game.GraphicsDevice);

            this.Texture = texture2D;
            this.Location = location;
            this.Size = size;
            this.Frames = frames;
            this.Frequency = frequency;
            this.CurrentFrame = this.StartFrame = 0;
            this.TimeFrame = (float)1 / this.Frequency;
        }
        /// <summary>
        /// Конструктор анимации
        /// </summary>
        /// <param name="game">Текущая игра</param>
        /// <param name="texture2D">Текущая анимация</param>
        /// <param name="location">Положение анимации</param>
        /// <param name="size">Размер кадра анимации</param>
        /// <param name="frames">Колличество кадров анимации</param>
        /// <param name="frequency">Частота кадров</param>
        /// <param name="startFrame">Начальный кадр</param>
        public Animation(Game game, Texture2D texture2D, Vector2 location, System.Drawing.Size size, int frames,
                               int frequency, int startFrame)
            : base(game)
        {
            SpriteBatch = new SpriteBatch(game.GraphicsDevice);

            this.Texture = texture2D;
            this.Location = location;
            this.Size = size;
            this.Frames = frames;
            this.Frequency = frequency;
            this.CurrentFrame = 0;
            this.StartFrame = startFrame;
            this.TimeFrame = (float)1 / this.Frequency;
        }
        /// <summary>
        /// Конструктор анимации
        /// </summary>
        /// <param name="game">Текущая игра</param>
        /// <param name="texture2D">Текущая анимация</param>
        /// <param name="location">Положение анимации</param>
        /// <param name="size">Размер кадра анимации</param>
        /// <param name="frames">Колличество кадров анимации</param>
        /// <param name="frequency">Частота кадров</param>
        /// <param name="startFrame">Начальный кадр</param>
        /// <param name="timeFrame">Время между кадрами анимации</param>
        public Animation(Game game, Texture2D texture2D, Vector2 location, System.Drawing.Size size, int frames,
                               int frequency, int startFrame, int timeFrame)
            : base(game)
        {
            SpriteBatch = new SpriteBatch(game.GraphicsDevice);

            this.Texture = texture2D;
            this.Location = location;
            this.Size = size;
            this.Frames = frames;
            this.Frequency = frequency;
            this.CurrentFrame = 0;
            this.StartFrame = startFrame;
            this.TimeFrame = timeFrame;
        }

        #endregion

        #region Fields

        /// <summary>
        /// Текстура
        /// </summary>
        public Texture2D Texture { get; set; }
        /// <summary>
        /// Размеря прямоугольника кадра анимации
        /// </summary>
        public System.Drawing.Size Size { get; set; }
        /// <summary>
        /// Колличество кадров анимации
        /// </summary>
        public int Frames { get; set; }
        /// <summary>
        /// Частота кадров анимации
        /// </summary>
        public int Frequency { get; set; }
        /// <summary>
        /// Текущий кадр
        /// </summary>
        public int CurrentFrame { get; set; }
        /// <summary>
        /// Начальный кадр
        /// </summary>
        public int StartFrame { get; set; }
        /// <summary>
        /// Время отображения одного кадра 
        /// </summary>
        public float TimeFrame { get; set; }
        /// <summary>
        /// Время, которое прошло с начала отображения текущего кадра
        /// </summary>
        public float TotalElapsed { get; set; }
        /// <summary>
        /// Положение анимации
        /// </summary>
        public Vector2 Location { get; set; }
        /// <summary>
        /// Прямоугольник для задания позиции кадра в изображении
        /// </summary>
        public Rectangle RectangleSprite { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Параметры отображения анимации
        /// </summary>
        /// <param name="Texture">Текстура</param>
        public void Construct(Texture2D Texture)
        {
            if (this.Texture != Texture)
                this.Texture = Texture;
        }
        /// <summary>
        /// Параметры отображения анимации
        /// </summary>
        /// <param name="Texture">Текстура</param>
        /// <param name="FrameCount">Колличество кадров анимации</param>
        public void Construct(Texture2D Texture, int FrameCount)
        {
            if (this.Texture != Texture)
                this.Texture = Texture;

            if (this.Frames != FrameCount)
                this.Frames = FrameCount;
        }
        /// <summary>
        /// Параметры отображения анимации
        /// </summary>
        /// <param name="Texture">Текстура</param>
        /// <param name="FrameCount">Колличество кадров анимации</param>
        /// <param name="StartFtame">Начальный кадр</param>
        public void Construct(Texture2D Texture, int FrameCount, int StartFtame)
        {
            if (this.Texture != Texture)
                this.Texture = Texture;

            if (this.Frames != FrameCount)
                this.Frames = FrameCount;

            if (this.StartFrame != StartFtame)
                this.StartFrame = StartFtame;
        }
        /// <summary>
        /// Параметры отображения анимации
        /// </summary>
        /// <param name="Texture">Текстура</param>
        /// <param name="FrameCount">Колличество кадров анимации</param>
        /// <param name="StartFtame">Начальный кадр</param>
        /// <param name="TimeFrame">Время отображение кадра</param>
        public void Construct(Texture2D Texture, int FrameCount, int StartFtame, float TimeFrame)
        {
            if(this.Texture != Texture)
                this.Texture = Texture;

            if(this.Frames != FrameCount)
                this.Frames = FrameCount;

            if(this.StartFrame != StartFtame)
                this.StartFrame = StartFtame;

            if(this.TimeFrame != TimeFrame)
                this.TimeFrame = TimeFrame;
        }
        /// <summary>
        /// Параметры отображения анимации
        /// </summary>
        /// <param name="Texture">Текстура</param>
        /// <param name="FrameCount">Колличество кадров анимации</param>
        /// <param name="StartFtame">Начальный кадр</param>
        /// <param name="TimeFrame">Время отображения одного кадра</param>
        /// <param name="CurrentFrame">Текущий кадр</param>
        public void Construct(Texture2D Texture, int FrameCount, int StartFtame, float TimeFrame, int CurrentFrame)
        {
            if (this.Texture != Texture)
                this.Texture = Texture;

            if (this.Frames != FrameCount)
                this.Frames = FrameCount;

            if (this.StartFrame != StartFtame)
                this.StartFrame = StartFtame;

            if (this.TimeFrame != TimeFrame)
                this.TimeFrame = TimeFrame;

            if (this.CurrentFrame != CurrentFrame)
                this.CurrentFrame = CurrentFrame;
        }
        /// <summary>
        /// Параметры отображения анимации
        /// </summary>
        /// <param name="Texture">Текстура</param>
        /// <param name="FrameCount">Колличество кадров анимации</param>
        /// <param name="StartFtame">Начальный кадр</param>
        /// <param name="TimeFrame">Время отображения одного кадра</param>
        /// <param name="CurrentFrame">Текущий кадр</param>
        /// <param name="Frequency">Частота кадров анимации</param>
        public void Construct(Texture2D Texture, int FrameCount, int StartFtame, float TimeFrame, int CurrentFrame, int Frequency)
        {
            if (this.Texture != Texture)
                this.Texture = Texture;

            if (this.Frames != FrameCount)
                this.Frames = FrameCount;

            if (this.StartFrame != StartFtame)
                this.StartFrame = StartFtame;

            if (this.TimeFrame != TimeFrame)
                this.TimeFrame = TimeFrame;

            if (this.CurrentFrame != CurrentFrame)
                this.CurrentFrame = CurrentFrame;

            if (this.Frequency != Frequency)
                this.Frequency = Frequency;
        }
        /// <summary>
        /// Процедура анимации спрайта
        /// </summary>
        /// <param name="LastTimeFrame">Колличество секунд прошедшее с последнего вызова</param>
        public void ChangeFrame(float LastTimeFrame)
        {
            this.TotalElapsed += LastTimeFrame;

            if (this.TotalElapsed > this.TimeFrame)
            {
                if (this.CurrentFrame >= this.Frames - 1)
                    this.CurrentFrame = this.StartFrame;
                else this.CurrentFrame++;

                this.RectangleSprite = new Rectangle((int)this.Size.Width * this.CurrentFrame, 0,
                                                      this.Size.Width, this.Size.Height);
                this.TotalElapsed = 0;
            }
        }
        /// <summary>
        /// Логика
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            //Вызов процедуры анимации спрайта
            //в качестве параметра передается время,прошедшее после
            //последнего вызова Update
            ChangeFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
            
            base.Update(gameTime);
        }
        /// <summary>
        /// Отрисовка
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin(SpriteSortMode.Texture, BlendState.NonPremultiplied);
            SpriteBatch.Draw(this.Texture, this.Location, this.RectangleSprite, Microsoft.Xna.Framework.Color.White);
            SpriteBatch.End();
            base.Draw(gameTime);
        }

        #endregion
    }
}
