using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace simonCloneThing
{
    public class GameStateManager
    {
        private SpriteBatch batch;
        private GraphicsDeviceManager graphics;
        private int currentState;
        private List<GameState> gamestates;
        private ContentManager content;
        private Vector2 defaultcampos;

        private EffectManager fxm;
        private Dictionary<int,object> publicsObjs;
        private Dictionary<String, SoundEffect> soundEffects;
        
        public GameStateManager(ContentManager content)
        {
            
            this.batch = Game1.spriteBatch;
            this.graphics = Game1.graphics;
            gamestates = new List<GameState>();
            this.content = content;
            defaultcampos = Vector2.Zero;
            
            currentState = Consts.PLAYSTATE;
            fxm = new EffectManager();
            publicsObjs = new Dictionary<int, object>();
            soundEffects = new Dictionary<string, SoundEffect>();

            addSound("data/1", "green");
            addSound("data/2", "red");
            addSound("data/3", "blue");
            addSound("data/4", "yellow");
            addSound("data/5", "boom");
            
        }
        private void addState(GameState state)
        {
            gamestates.Add(state);
        }
        public void loadContent()
        {
            addState(new PlayState(this, content));

        }
        public void setState(int state)
        {
            this.currentState = state;
        }
        public void update(GameTime gametime)
        {
            gamestates[currentState].update(gametime);
            fxm.update(gametime);
            
        }
        public void draw()
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            batch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Game1.cam.getTransformation());
            gamestates[currentState].draw();
            fxm.draw();
            batch.End();
        }
        public void Exit()
        {
            Environment.Exit(0);
        }

        public void addPublicObj(int num,Object obj)
        {
            publicsObjs.Add(num,obj);
        }
        public void addEffect(EffectInstance efct)
        {
            fxm.addEffect(efct);
        }
        public void playSound(String name,float volume=0.02f,float pitch=0,float pan=0)
        {
            soundEffects[name].Play(volume,pitch,pan);
        }
        public void addSound(String filename,String name)
        {
            SoundEffect soundefect = content.Load<SoundEffect>(filename);
            soundEffects.Add(name, soundefect);
            
        }
    }
}