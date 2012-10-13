using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace South_Park
{
    public class CameraTawer : DrawableGameComponent, ITawers
    {
        static readonly Random _RandomDirection = new Random(DateTime.Now.Millisecond);

        private Animation Animation;
        private SpriteBatch SpriteBatch;
        private HealthIndicator HealthIndicator;

        public CameraTawer(Game game)
            : base(game)
        {
            this.Texture = new Texture2D[3];
            this.LoadContent();
            this.Location = Vector2.Zero;
            this.SpriteSize = new System.Drawing.Size(80, 80);

            SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
            Animation = new Animation(Game, this.Texture[0], this.Location, this.SpriteSize, 1, 8);
            HealthIndicator = new HealthIndicator(Game);

            this.FireZone = new Rectangle((int)this.Location.X - 100, (int)this.Location.Y - 100, 280, 280);
            this.Level = this.Experience = this.InactivityTime = 0;
            this.Health = 100;
            this.Direction = _RandomDirection.Next(0, 4);
            this.Enabled = true;
            this.Fire = false;
        }

        #region Fields

        /// <summary>
        /// Текстуры
        /// </summary>
        public Texture2D[] Texture { get; set; }
        /// <summary>
        /// Размер спрайта
        /// </summary>
        public System.Drawing.Size SpriteSize { get; set; }
        /// <summary>
        /// Позиция
        /// </summary>
        public Vector2 Location { get; set; }
        /// <summary>
        /// Направление
        /// </summary>
        public int Direction { get; set; }
        /// <summary>
        /// Очки опыта
        /// </summary>
        public int Experience { get; set; }
        /// <summary>
        /// Уровень
        /// </summary>
        public int Level { get; set; }

        public bool Fire { get; set; }
        /// <summary>
        /// Здоровье
        /// </summary>
        public int Health { get; set; }
        /// <summary>
        /// Время бездействия
        /// </summary>
        public int InactivityTime { get; set; }
        /// <summary>
        /// Маска коллизии
        /// </summary>
        public Rectangle RectangleMask { get; set; }
        /// <summary>
        /// Зона поражения
        /// </summary>
        public Rectangle FireZone { get; set; }

        #endregion


        /// <summary>
        /// Проверка столкновения масок
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public bool CheckCollision(Rectangle rect)
        {
            Rectangle spriterect = new Rectangle((int)Location.X + 20 , (int)Location.Y + 20,
                                                  40, 40);
            return spriterect.Intersects(rect);
        }

        /// <summary>
        /// Проверка столкновения с зоной обстрела
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public bool CheckCollisionWithFireZone(Rectangle rect)
        {
            Rectangle spriterect = new Rectangle((int)Location.X - 100, (int)Location.Y - 100, 280, 280);                         
            return spriterect.Intersects(rect);
        }

        /// <summary>
        /// Возвращает прямоугольник границ 
        /// </summary>
        /// <returns></returns>
        public Rectangle GetBounds()
        {
            return new Rectangle((int)this.Location.X, (int)this.Location.Y,
                 this.SpriteSize.Width, this.SpriteSize.Height);
        }

        private void Inactivity()
        {
            if (this.InactivityTime == 150)
            {
                this.InactivityTime = 0;
                if (!this.Enabled)
                    Animation.StartFrame = Animation.Frames = this.Direction = _RandomDirection.Next(1, 8);
            }
            else
                this.InactivityTime++; 
        }


        protected override void LoadContent()
        {
            this.Texture[0] = Game.Content.Load<Texture2D>("Анимация/Башни/Башня1/Tawer1Level1");
            this.Texture[1] = Game.Content.Load<Texture2D>("Анимация/Башни/Башня1/Tawer1Level2");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            HealthIndicator.Health = this.Health;
            HealthIndicator.Location = this.Location;
            HealthIndicator.Update(gameTime);

            if (Animation != null)
            {
                if (this.Level == 1)
                    Animation.Construct(this.Texture[0], 1);
                if (this.Level == 2)
                    Animation.Construct(this.Texture[1], 1);

                this.Inactivity();

                Animation.Location = this.Location;
                Animation.Update(gameTime);
            }

            


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (Animation != null)
                Animation.Draw(gameTime);

            HealthIndicator.Draw(gameTime);

            base.Draw(gameTime);
        }

    }
}
