using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RatPet.Helpers;
using RatPet.VisualControllers;
using System.Collections.Generic;
using System.Diagnostics;

// TODO: ajustar la position y el rectangle de rat según el estado para que siempre gire con respecto al centro del cuerpo

// TODO: escalar velocidad
// TODO: retocar sprite de subida
// TODO: pasar los métodos get a propiedades públicas?
// TODO: debería ver que propiedades publicas uso de state para delegar esa lógica a la clase state
// TODO: strings que uso para acceder al appsettings pasar a constantes? clase singleton para pasar los valores?
// TODO: contador de cheese
// TODO: Objeto pantalla que conste de box, botonera y background, todo lo inamovible en pantalla
// TODO: Función go to the point, para clickar en pantalla y que la rata camine
// TODO: crear brainRat, tal vez utilizar ratTasks? algo paralelero a ratStates? una task puede cotener varias tareas. Además harían faltamás estados, como durmiendo. La velocidad también debería cambiar.

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
            //Original resolution 800x480
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Parameters parameters = new Parameters();

            var scoreBoard = new CheeseScore(GraphicsDevice.Viewport,
                Content.Load<Texture2D>("cheeseScore"),
                Content.Load<Texture2D>("tinyCheese"),
                Content.Load<SpriteFont>("DefaultFont"),
                parameters.scorePaddingTop,
                parameters.scorePaddingRight,
                parameters.uiScale);

            var box = new Box(GraphicsDevice.Viewport,
                Content.Load<Texture2D>("blackpixel"),
                parameters.boxBorders,
                parameters.ratAreaPaddingTop,
                parameters.ratAreaPaddingRight,
                parameters.ratAreaPaddingLeft,
                parameters.ratAreaPaddingBottom);

            var rat = new Rat(Content,
                box,
                parameters.topFramesAnimation,
                parameters.statesFileName,
                parameters.scale,
                parameters.defaultSpeed);

            var cheesePool = new CheesePool(Content.Load<Texture2D>("cheese2"),
                box,
                rat,
                Content.Load<SpriteFont>("DefaultFont"),
                parameters.reduceCollisionX,
                parameters.reduceCollisionY,
                parameters.fallingSpeed,
                parameters.mediaFramesPerCheese,
                parameters.scale);

            visuals = new List<Visual>() { scoreBoard, box, rat, cheesePool };
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
