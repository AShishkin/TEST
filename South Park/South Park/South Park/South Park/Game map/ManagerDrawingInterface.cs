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

        // Отрисовка иконок Картмана на панели
        private void CartmanIconsDraw()
        {
            if (this.HelthIndicator <= 30) SpriteBatch.Draw(Texture[2], new Rectangle(605, 640, 89, 89), Color.White);
            else if (this.HelthIndicator >= 31 && this.HelthIndicator <= 61) SpriteBatch.Draw(Texture[1], new Rectangle(605, 640, 89, 89), Color.White);
            else if (this.HelthIndicator >= 61) SpriteBatch.Draw(Texture[0], new Rectangle(605, 640, 89, 89), Color.White);
        }


        private void InterfaceStringDraw()
        {
            // Монеты
            SpriteBatch.DrawString(SpriteFont, "x " + this.MoneyIndicator, new Vector2(415, 700), Color.White);

            SpriteBatch.DrawString(SpriteFont, "x " + this.TawerCountIndicator, new Vector2(615, 700), Color.White);


            //SpriteBatch.DrawString(SpriteFont, "x " + this.CartmanContinnumIndicator, new Vector2(690, 700), Microsoft.Xna.Framework.Color.White);

            //if (this.HelthIndicator <= 30)
            //{
            //    SpriteBatch.DrawString(SpriteFont, this.HelthIndicator + "%", new Vector2(625, 700), Microsoft.Xna.Framework.Color.Black);
               
            //}
            //if ((this.HelthIndicator >= 31) && (this.HelthIndicator <= 80))
            //{
            //    SpriteBatch.DrawString(SpriteFont, this.HelthIndicator + "%", new Vector2(625, 700), Microsoft.Xna.Framework.Color.Yellow);
            //}

            //if (this.HelthIndicator >= 81)
            //{
            //    SpriteBatch.DrawString(SpriteFont, this.HelthIndicator + "%", new Vector2(625, 700), Microsoft.Xna.Framework.Color.Lime);
                

            //}
        }

        // Отрисовка интерфейса
        private void InterfaceDraw()
        {
            // Фон
            SpriteBatch.Draw(this.Background, new Rectangle(0, 0, 1320, 720), Color.White);
            // Панель
            SpriteBatch.Draw(Texture[3], new Rectangle(150, 633, 1020, 100), Color.White);
            // Картинка города
   //         SpriteBatch.Draw(Texture[4], new Rectangle(1050, 640, 150, 80), Color.White);
            // Монеты
          //  SpriteBatch.Draw(Texture[5], new Rectangle(370, 640, 80, 80), Color.White);
            // Башня
           // SpriteBatch.Draw(Texture[7], new Rectangle(100, 653, 60, 80), Color.White);
            // Красная обводка при малом кол-ве здоровья
            if (this.HelthIndicator <= 30) SpriteBatch.Draw(Texture[6], new Rectangle(0, 0, 1320, 720), Color.White);              
        }


        protected override void LoadContent()
        {
            Texture.Add(0, Game.Content.Load<Texture2D>("InterfaceContent/CartmanInterfaceContent/Icons/IconGood"));
            Texture.Add(1, Game.Content.Load<Texture2D>("InterfaceContent/CartmanInterfaceContent/Icons/IconNormal"));
            Texture.Add(2, Game.Content.Load<Texture2D>("InterfaceContent/CartmanInterfaceContent/Icons/IconBad"));

            Texture.Add(3, Game.Content.Load<Texture2D>("InterfaceContent/Panel"));
            Texture.Add(4, Game.Content.Load<Texture2D>("InterfaceContent/City"));
            Texture.Add(5, Game.Content.Load<Texture2D>("InterfaceContent/Money"));
            Texture.Add(6, Game.Content.Load<Texture2D>("InterfaceContent/Alert/InterfaceAlert"));
            Texture.Add(7, Game.Content.Load<Texture2D>("InterfaceContent/Tawer"));



           // this.Interface[8] = Game.Content.Load<Texture2D>("Анимация/TestBackground");



            base.LoadContent();
        }


        


        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            this.InterfaceDraw();
            this.CartmanIconsDraw();
            this.InterfaceStringDraw();
            SpriteBatch.End();
            base.Draw(gameTime);
        }



    }
}
