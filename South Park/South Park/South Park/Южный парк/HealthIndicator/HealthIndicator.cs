using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace South_Park
{
    class HealthIndicator : DrawableGameComponent
    {
        private Dictionary<int, Texture2D> Texture;
        private SpriteBatch SpriteBatch;

        private int _Index = 0;

        public HealthIndicator(Game game) 
            : base(game)
        {
            Texture = new Dictionary<int, Texture2D>();
            SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
 
            this.Health = 100;

            this.LoadContent();
        }

        public int Health { get; set; }

        public Vector2 Location { get; set; }

        private void HealthBarGood()
        {
            if ((this.Health < 100) && (this.Health >= 95)) _Index = 1;
            else
                if ((this.Health < 95) && (this.Health >= 90)) _Index = 2;
                else
                    if ((this.Health < 90) && (this.Health >= 85)) _Index = 3;
                    else
                        if ((this.Health < 85) && (this.Health >= 80)) _Index = 4;
                        else
                            if ((this.Health < 80) && (this.Health >= 75)) _Index = 5;
                            else
                                if ((this.Health < 75) && (this.Health >= 70)) _Index = 6;
        }

        private void HealthBarMiddle()
        {
            if ((this.Health < 70) && (this.Health >= 65)) _Index = 7;
            else
                if ((this.Health < 65) && (this.Health >= 60)) _Index = 8;
                else
                    if ((this.Health < 60) && (this.Health >= 55)) _Index = 9;
                    else
                        if ((this.Health < 55) && (this.Health >= 50)) _Index = 10;
                        else
                            if ((this.Health < 50) && (this.Health >= 45)) _Index = 11;
                            else
                                if ((this.Health < 45) && (this.Health >= 40)) _Index = 12;
                                else
                                    if ((this.Health < 40) && (this.Health >= 35)) _Index = 13;
                                    else
                                        if ((this.Health < 35) && (this.Health >= 30)) _Index = 14;
        }

        private void HealthBarSmall()
        {
            if ((this.Health < 30) && (this.Health >= 25)) _Index = 15;
            else
                if ((this.Health < 25) && (this.Health >= 20)) _Index = 16;
                else
                    if ((this.Health < 20) && (this.Health >= 15)) _Index = 17;
                    else
                        if ((this.Health < 15) && (this.Health >= 10)) _Index = 18;
                        else
                            if ((this.Health < 10) && (this.Health >= 5)) _Index = 19;
                            else
                                if ((this.Health < 5) && (this.Health >= 0)) _Index = 20;
        }

        protected override void LoadContent()
        {
            Texture.Add(1, Game.Content.Load<Texture2D>("HealthIndicator/BarHealth01"));
            Texture.Add(2, Game.Content.Load<Texture2D>("HealthIndicator/BarHealth02"));
            Texture.Add(3, Game.Content.Load<Texture2D>("HealthIndicator/BarHealth03"));
            Texture.Add(4, Game.Content.Load<Texture2D>("HealthIndicator/BarHealth04"));
            Texture.Add(5, Game.Content.Load<Texture2D>("HealthIndicator/BarHealth05"));
            Texture.Add(6, Game.Content.Load<Texture2D>("HealthIndicator/BarHealth06"));
            Texture.Add(7, Game.Content.Load<Texture2D>("HealthIndicator/BarHealth07"));
            Texture.Add(8, Game.Content.Load<Texture2D>("HealthIndicator/BarHealth08"));
            Texture.Add(9, Game.Content.Load<Texture2D>("HealthIndicator/BarHealth09"));
            Texture.Add(10, Game.Content.Load<Texture2D>("HealthIndicator/BarHealth10"));
            Texture.Add(11, Game.Content.Load<Texture2D>("HealthIndicator/BarHealth11"));
            Texture.Add(12, Game.Content.Load<Texture2D>("HealthIndicator/BarHealth12"));
            Texture.Add(13, Game.Content.Load<Texture2D>("HealthIndicator/BarHealth13"));
            Texture.Add(14, Game.Content.Load<Texture2D>("HealthIndicator/BarHealth14"));
            Texture.Add(15, Game.Content.Load<Texture2D>("HealthIndicator/BarHealth15"));
            Texture.Add(16, Game.Content.Load<Texture2D>("HealthIndicator/BarHealth16"));
            Texture.Add(17, Game.Content.Load<Texture2D>("HealthIndicator/BarHealth17"));
            Texture.Add(18, Game.Content.Load<Texture2D>("HealthIndicator/BarHealth18"));
            Texture.Add(19, Game.Content.Load<Texture2D>("HealthIndicator/BarHealth19"));
            Texture.Add(20, Game.Content.Load<Texture2D>("HealthIndicator/BarHealth20"));

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if ((this.Health < 100) && (this.Health >= 70))
                this.HealthBarGood();
            else
                if ((this.Health < 70) && (this.Health >= 30))
                    this.HealthBarMiddle();
                else
                    if ((this.Health < 30) && (this.Health > 0))
                        this.HealthBarSmall();
                    else
                        if (this.Health <= 0) _Index = 0;
                        else 
                            if (this.Health >= 100) _Index = -1;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            if ((_Index != 0) && (_Index != -1))
                SpriteBatch.Draw(Texture[_Index], new Rectangle((int)this.Location.X, (int)this.Location.Y -  8, 80, 7), Color.White);
            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
