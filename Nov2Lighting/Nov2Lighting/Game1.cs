using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;

namespace Nov2Lighting
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager device;

        private Effect myShader;
        private Texture smileyTex;
        private Model teapot;

        private Matrix world = Matrix.Identity;
        private Matrix view = Matrix.Identity;
        private Matrix projection = Matrix.Identity;

        private Vector3 diffuseColor = new Vector3(1, 1, 1);

        public Game1()
        {
            device = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            world = Matrix.CreateTranslation(new Vector3(0, 0, 0));
            view = Matrix.CreateLookAt(new Vector3(0, 0, 3), new Vector3(0, 0, 0), Vector3.Up);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), device.GraphicsDevice.Viewport.AspectRatio, 0.1f, 100f);

            world *= Matrix.CreateRotationX(1.5f * MathHelper.Pi);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            myShader = Content.Load<Effect>("MyShader");
            smileyTex = Content.Load<Texture2D>("Smiley2");
            teapot = Content.Load<Model>("Teapot");
        }

        protected override void Update(GameTime gameTime)
        {
            myShader.Parameters["WorldViewProjection"].SetValue(world * view * projection);
            myShader.Parameters["Texture"].SetValue(smileyTex);
            myShader.Parameters["DiffuseColor"].SetValue(diffuseColor);



            base.Draw(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            foreach (ModelMesh mesh in teapot.Meshes)
            {
                foreach (ModelMeshPart meshpart in mesh.MeshParts)
                {
                    meshpart.Effect = myShader;
                }
                mesh.Draw();
            }
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}