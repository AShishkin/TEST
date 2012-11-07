using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace South_Park___Coon_and_friends
{
    struct ManagerEricContent
    {
        static public void LoadContent(Game game, ref Dictionary<int, Texture2D> texture)
        {
            texture.Add(0, game.Content.Load<Texture2D>("Eric/Movement/Down"));
            texture.Add(1, game.Content.Load<Texture2D>("Eric/Movement/Up"));
            texture.Add(2, game.Content.Load<Texture2D>("Eric/Movement/Left"));
            texture.Add(3, game.Content.Load<Texture2D>("Eric/Movement/Right"));
            texture.Add(4, game.Content.Load<Texture2D>("Eric/Stopped/StoppedGood"));
            texture.Add(5, game.Content.Load<Texture2D>("Eric/Stopped/StoppedNormal"));
            texture.Add(6, game.Content.Load<Texture2D>("Eric/Stopped/StoppedBad"));
        }
    }
}  