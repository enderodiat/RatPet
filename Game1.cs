using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Configuration;
using System.Diagnostics;

// TODO: animación queso cayendo
// TODO: pasar los métodos get a propiedades públicas?
// TODO: lavar codigo game
// TODO: renombrar clase game1
// TODO: debería ver que propiedades publicas uso de state para delegar esa lógica a la clase state
// TODO: strings que uso para acceder al appsettings pasar a constantes? clase singleton para pasar los valores?
// TODO: crear cheese pool y contador
// TODO: crear brainRat

namespace Project1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Rat rat;
        Box box;
        Cheese cheese;

        public Game1() 
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            int padding = int.Parse(ConfigurationManager.AppSettings["areaRatPadding"]);
            int topFramesAnimation = int.Parse(ConfigurationManager.AppSettings["topFramesPerSpriteAnimation"]);
            int defaultSpeed = int.Parse(ConfigurationManager.AppSettings["defaultSpeed"]);
            int fallingSpeed = int.Parse(ConfigurationManager.AppSettings["fallingSpeed"]);
            float scale = float.Parse(ConfigurationManager.AppSettings["defaultScale"]);
            
            float reduceCollisionY = float.Parse(ConfigurationManager.AppSettings["collisionCheeseReduceFactorY"]);
            float reduceCollisionX = float.Parse(ConfigurationManager.AppSettings["collisionCheeseReduceFactorX"]);

            box = new Box(Content, padding, GraphicsDevice.Viewport);

            rat = new Rat(Content,
                new Vector2(GraphicsDevice.Viewport.Width/2, GraphicsDevice.Viewport.Height/2),
                GraphicsDevice.Viewport,
                scale,
                defaultSpeed);

            cheese = new Cheese(Content, new Vector2(0,0), GraphicsDevice.Viewport, padding, reduceCollisionX, reduceCollisionY, fallingSpeed, scale);
            cheese.SetNewPosition();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Debug.WriteLine("Mouse: (" + Mouse.GetState().X + ", " + Mouse.GetState().Y + ")");
                Debug.WriteLine(rat.scale);
                cheese.SetNewPosition();
            }

            rat.Update(box);
            cheese.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(
                SpriteSortMode.BackToFront,
                samplerState: SamplerState.PointClamp);

            if (!cheese.Collision(rat))
            {
                cheese.Draw(_spriteBatch);
            }
            else
            {
                cheese.SetNewPosition();
            }

            rat.Draw(_spriteBatch);
            //box.Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        } 
    }
}
