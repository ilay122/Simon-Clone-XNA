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
    class PlayState : GameState
    {
        private Sprite red;
        private Sprite yellow;
        private Sprite blue;
        private Sprite green;
        private MouseState prevmouse;
        private KeyboardState prevkeyb;
        private StackOfColors stackofcolors;
        private int highscore;
        private Boolean showcorrectcolor;
        private Color[] arr;
        private Color greencolor;
        private String[] colornames;
        public PlayState(GameStateManager gsm, ContentManager content)
            : base(gsm, content)
        {
            greencolor = new Color(0, 255, 0);
            red = new Sprite(Sprite.CreateRectangleTexture(300, 300, Color.Red));
            yellow = new Sprite(Sprite.CreateRectangleTexture(300, 300, Color.Yellow));
            blue = new Sprite(Sprite.CreateRectangleTexture(300, 300, Color.Blue));
            green = new Sprite(Sprite.CreateRectangleTexture(300, 300, greencolor));

            prevmouse = Mouse.GetState();
            prevkeyb = Keyboard.GetState();
            stackofcolors = new StackOfColors(gsm);
            
            arr = new Color[] { Color.Red, Color.Yellow, greencolor, Color.Blue };
            colornames = new String[] { "Red", "Yellow", "Green", "Blue" };
            this.highscore = 0;
            this.showcorrectcolor = false;

            red.setPosition(new Vector2(0,300));
            yellow.setPosition(new Vector2(300,300));
            blue.setPosition(new Vector2(300,0));
            green.setPosition(new Vector2(0,0));

        }
        public override void draw()
        {
            red.draw();
            yellow.draw();
            blue.draw();
            green.draw();
            
            Game1.spriteBatch.DrawString(Game1.font, "Highscore : " + highscore.ToString(), new Vector2(600, 0), Color.White);
            if (showcorrectcolor)
            {
                Color currectcolortopress = stackofcolors.getCurrentColor();
                int correct = 0;
                for (int i = 1; i < arr.Length; i++)
                {
                    if (arr[i] == currectcolortopress)
                    {
                        correct = i;
                    }
                }
                    Game1.spriteBatch.DrawString(Game1.font, "Correct Color : \n" + colornames[correct]+"\n:^)", new Vector2(600, 300), Color.White);
            }
        }
        public override void update(GameTime gametime)
        {

            MouseState cms = Mouse.GetState();
            if (!stackofcolors.isPlayingAnimations())
            {
                if (cms.LeftButton == ButtonState.Pressed && prevmouse.LeftButton == ButtonState.Released)
                {
                    Color colorplayed = Color.White;
                    Boolean clickedsomething=false;
                    String colorplayedd = "";
                    if (red.getCollisionRectangle().Contains(cms.X, cms.Y))
                    {
                        gsm.addEffect(new FadeEffect(true, Consts.DELAYTIME, 0, Sprite.CreateRectangleTexture(300, 300, Color.DarkRed), new Vector2(0, 300)));
                        clickedsomething=true;
                        colorplayedd = "red";
                        colorplayed = Color.Red;
                        
                    }
                    if (yellow.getCollisionRectangle().Contains(cms.X, cms.Y))
                    {
                        gsm.addEffect(new FadeEffect(true, Consts.DELAYTIME, 0, Sprite.CreateRectangleTexture(300, 300, new Color(204, 204, 0)), new Vector2(300, 300)));
                        clickedsomething=true;
                        colorplayedd = "yellow";
                        colorplayed = Color.Yellow;
                    }
                    if (blue.getCollisionRectangle().Contains(cms.X, cms.Y))
                    {
                        gsm.addEffect(new FadeEffect(true, Consts.DELAYTIME, 0, Sprite.CreateRectangleTexture(300, 300, new Color(0, 0, 128)), new Vector2(300, 0)));
                        clickedsomething=true;
                        colorplayedd="blue";
                        colorplayed = Color.Blue;
                    }
                    if (green.getCollisionRectangle().Contains(cms.X, cms.Y))
                    {
                        gsm.addEffect(new FadeEffect(true, Consts.DELAYTIME, 0, Sprite.CreateRectangleTexture(300, 300, new Color(0, 100, 0)), new Vector2(0, 0)));
                        clickedsomething=true;
                        colorplayedd = "green";
                        colorplayed = new Color(0, 255, 0);
                    }
                    if (clickedsomething)
                    {
                        Boolean correct = stackofcolors.playColor(colorplayed);
                        if (!correct)
                        {
                            gsm.playSound("boom");
                            highscore = Math.Max(highscore, stackofcolors.getLength()-1);
                            stackofcolors.restart();
                            
                        }
                        else
                        {
                            gsm.playSound(colorplayedd);
                        }
                        
                    }
                }
                prevmouse = cms;

            }
            else
            {
                stackofcolors.update(gametime);
            }

            KeyboardState keyb = Keyboard.GetState();
            if (keyb.IsKeyDown(Keys.H) && prevkeyb.IsKeyUp(Keys.H))
            {
                showcorrectcolor = !showcorrectcolor;
            }
            prevkeyb = keyb;
        }
        
    }
}