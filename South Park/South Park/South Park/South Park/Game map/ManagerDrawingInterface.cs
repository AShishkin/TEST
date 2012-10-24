using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace South_Park
{
    class ManagerDrawingInterface : DrawableGameComponent
    {
        private Dictionary<int, Texture2D> Texture;
        private SpriteBatch SpriteBatch;
        private SpriteFont SpriteFont;

        public ManagerDrawingInterface(Game game, Texture2D background)
            : base(game)
        {
            Texture = new Dictionary<int, Texture2D>();
            SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
            SpriteFont = Game.Content.Load<SpriteFont>("MySpriteFont");
            this.Background = background;
            this.HelthIndicator = 0;
            this.LoadContent();
        }

        public Texture2D Background { get; set; }
        public int HelthIndicator { get; set; }
        public int MoneyIndicator { get; set; }
        public int CartmanContinnumIndicator { get; set; }
        public int TawerCountIndicator { get; set; }
        public int CityHelthIndicator { get; set; }
        public int ECountIndicator { get; set; }
        public string EName { get; set; }
  
       


        private void InterfaceStringDraw()
        {
            // Монеты
            SpriteBatch.DrawString(SpriteFont, "x " + this.MoneyIndicator, new Vector2(425, 700), Color.White);
            // Башни
            SpriteBatch.DrawString(SpriteFont, this.TawerCountIndicator + " x", new Vector2(842, 700), Color.White);
            // Жизни города
            SpriteBatch.DrawString(SpriteFont, this.CityHelthIndicator.ToString() , new Vector2(1010, 680), Color.White);

            SpriteBatch.DrawString(SpriteFont, this.ECountIndicator.ToString(), new Vector2(225, 680), Color.White);

            SpriteBatch.DrawString(SpriteFont, this.EName, new Vector2(205, 700), Color.White);

            //SpriteBatch.DrawString(SpriteFont, "x " + this.CartmanContinnumIndicator, new Vector2(690, 700), Microsoft.Xna.Framework.Color.White);

            if (this.HelthIndicator >= 100)
                SpriteBatch.DrawString(SpriteFont, this.HelthIndicator.ToString(), new Vector2(633, 680), Color.Lime);
            else if (this.HelthIndicator < 100 && this.HelthIndicator >= 10)
            {
                if(this.HelthIndicator >= 80)
                    SpriteBatch.DrawString(SpriteFont, this.HelthIndicator.ToString(), new Vector2(638, 680), Color.Lime);
                else if(this.HelthIndicator >= 40 && this.HelthIndicator < 80) 
                    SpriteBatch.DrawString(SpriteFont, this.HelthIndicator.ToString(), new Vector2(638, 680), Color.Orange);
                else if (this.HelthIndicator <= 40)
                {
                    SpriteBatch.DrawString(SpriteFont, this.HelthIndicator.ToString(), new Vector2(638, 680), Color.Red);
                    SpriteBatch.Draw(Texture[2], new Rectangle(0, 0, 1320, 720), Color.White);
                }
            }
            else if (this.HelthIndicator < 10)
                SpriteBatch.DrawString(SpriteFont, this.HelthIndicator.ToString(), new Vector2(644, 680), Color.Red);
        }

        // Отрисовка интерфейса
        private void InterfaceDraw()
        {
            // Фон
            SpriteBatch.Draw(this.Background, new Rectangle(0, 0, 1320, 720), Color.White);
            // Панель
            SpriteBatch.Draw(Texture[0], new Rectangle(150, 653, 1004, 69), Color.White);  
        }


        protected override void LoadContent()
        {

            Texture.Add(0, Game.Content.Load<Texture2D>("InterfaceContent/Panel"));
            Texture.Add(2, Game.Content.Load<Texture2D>("InterfaceContent/Alert/InterfaceAlert"));

            base.LoadContent();
        }


        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            this.InterfaceDraw();
            this.InterfaceStringDraw();
            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
