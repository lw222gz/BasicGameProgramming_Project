using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NinjaBox.Model;
using NinjaBox.View.GameObjectsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.View
{
    class MainView
    {
        private Camera camera;
        private PlatformView platformView;
        private PlayerView playerView;
        private SpriteBatch spriteBatch;
        public MainView(GraphicsDevice device, ContentManager content)
        {
            camera = new Camera(device);
            spriteBatch = new SpriteBatch(device);
            platformView = new PlatformView(camera, content, spriteBatch);
            playerView = new PlayerView(camera, content, spriteBatch);
        }

        public void DrawGame(Level level)
        {
            camera.UpdateCameraOffset(level.Player);

            platformView.DrawPlatforms(level.Levelplatforms);

            playerView.DrawPlayer(level.Player);
        }
    }
}
