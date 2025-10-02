using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RatPet.Helpers;
using RatPet.VisualControllers;
using RatPet.VisualControllers.Rat;
using System.Collections.Generic;
using System.Diagnostics;

// TODO: hacer un fondo bastado en tonos amarillos, como tierra arenosa
// TODO: mejorar los layers de marcador etc, que los quesos lluevan por encima. Tal vez la clave sea reservar de 0 a 0,5 para la zona superior y de 0,5 a 1 para la ratzone
// TODO: escalar ratspeed
// TODO: pasar los métodos get a propiedades públicas?
// TODO: debería ver que propiedades publicas uso de state para delegar esa lógica a la clase state
// TODO: strings que uso para acceder al appsettings pasar a constantes? clase singleton para pasar los valores?
// TODO: Objeto pantalla que conste de box, botonera y background, todo lo inamovible en pantalla
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
                parameters);

            var box = new Box(GraphicsDevice.Viewport,
                Content.Load<Texture2D>("blackpixel"),
                parameters);

            var rat = new Rat(Content,
                box,
                parameters);

            var cheesePool = new CheesePool(Content.Load<Texture2D>("cheese2"),
                box,
                rat,
                parameters,
                scoreBoard);

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
            _spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp, blendState: BlendState.AlphaBlend);

            visuals.ForEach(visual => visual.Draw(_spriteBatch));

            _spriteBatch.End();
            base.Draw(gameTime);
        } 
    }
}
