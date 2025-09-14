using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RatPet.Helpers;
using RatPet.VisualControllers;
using System.Collections.Generic;
using System.Diagnostics;


// TODO: escalar velocidad
// TODO: pasar los métodos get a propiedades públicas?
// TODO: debería ver que propiedades publicas uso de state para delegar esa lógica a la clase state
// TODO: strings que uso para acceder al appsettings pasar a constantes? clase singleton para pasar los valores?
// TODO: contador de cheese
// TODO: crear brainRat

namespace RatPet
{
    public class RatPetGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        List<Visual> visuals;

        public RatPetGame() 
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

            Parameters parameters = new Parameters();

            var box = new Box(new Vector2(parameters.padding, parameters.padding), 
                GraphicsDevice.Viewport,
                Content.Load<Texture2D>("blackpixel"),
                parameters.boxBorders);

            var rat = new Rat(new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2),
                Content,
                box,
                parameters.topFramesAnimation,
                parameters.statesFileName,
                parameters.scale,
                parameters.defaultSpeed);

            var cheesePool = new CheesePool(box.Position,
                Content.Load<Texture2D>("cheese2"),
                box.Rectangle,
                rat,
                parameters.reduceCollisionX,
                parameters.reduceCollisionY,
                parameters.fallingSpeed,
                parameters.mediaFramesPerCheese,
                parameters.scale);

            visuals = new List<Visual>() { box, rat, cheesePool };
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Debug.WriteLine("Mouse: (" + Mouse.GetState().X + ", " + Mouse.GetState().Y + ")");
            }

            visuals.ForEach(visual => visual.Update());

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp);

            visuals.ForEach(visual => visual.Draw(_spriteBatch));

            _spriteBatch.End();
            base.Draw(gameTime);
        } 
    }
}
