using System;
using System.Collections.Generic;
using System.Collections;
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
    public class StackOfColors
    {
        private List<Color> colorpatterns;
        private Boolean isshowingpatterns;
        private int currentloc;
        private double timeelapsed;
        private Color[] arr;
        private Random random;
        private GameStateManager gsm;
        private Color greencolor;
        public StackOfColors(GameStateManager gsm) {
            colorpatterns = new List<Color>();
            isshowingpatterns = true;
            timeelapsed = 0f;
            greencolor = new Color(0, 255, 0);
            arr = new Color[] { Color.Red, Color.Yellow, greencolor, Color.Blue };
            random = new Random();
            this.gsm = gsm;
            colorpatterns.Add(arr[random.Next(0, arr.Length)]);
        }
        public Boolean playColor(Color clr)
        {
            
            if (colorpatterns[currentloc] == clr)
            {
                currentloc++;
                if (currentloc == colorpatterns.Count)
                {
                    isshowingpatterns = true;
                    colorpatterns.Add(arr[random.Next(0, arr.Length)]);
                    currentloc = 0;
                    return true;
                }
                return true;
            }
            return false;
        }
        public void update(GameTime gametime)
        {
            if (isshowingpatterns)
            {
                timeelapsed += gametime.ElapsedGameTime.TotalMilliseconds;
                if (timeelapsed > 1000)
                {
                    if(colorpatterns[currentloc] == Color.Red){
                        gsm.addEffect(new FadeEffect(true, Consts.DELAYTIME, 0, Sprite.CreateRectangleTexture(300, 300, Color.DarkRed), new Vector2(0, 300)));
                        gsm.playSound("red");
                    }
                    if(colorpatterns[currentloc] == Color.Yellow){
                        gsm.addEffect(new FadeEffect(true, Consts.DELAYTIME, 0, Sprite.CreateRectangleTexture(300, 300, new Color(204, 204, 0)), new Vector2(300, 300)));
                        gsm.playSound("yellow");
                    }
                    if(colorpatterns[currentloc] == Color.Blue){
                        gsm.addEffect(new FadeEffect(true, Consts.DELAYTIME, 0, Sprite.CreateRectangleTexture(300, 300, new Color(0, 0, 128)), new Vector2(300, 0)));
                        gsm.playSound("blue");
                    }
                    if(colorpatterns[currentloc] == greencolor){
                        gsm.addEffect(new FadeEffect(true, Consts.DELAYTIME, 0, Sprite.CreateRectangleTexture(300, 300, new Color(0, 100, 0)), new Vector2(0, 0)));
                        gsm.playSound("green");
                    }

                    timeelapsed = 0;
                    currentloc++;
                    if (currentloc == colorpatterns.Count)
                    {
                        currentloc = 0;
                        isshowingpatterns = false;
                        timeelapsed = 0;
                    }
                }
            }
        }
        public Boolean isPlayingAnimations()
        {
            return isshowingpatterns;
        }
        public String GetStringOfColors()
        {
            String bla = "";
            for (int i = 0; i < colorpatterns.Count; i++)
            {
                bla += colorpatterns[i] + " ";
            }
            return bla;
        }
        public void restart()
        {
            currentloc = 0;
            isshowingpatterns = true;
            timeelapsed = 0f;
            colorpatterns = new List<Color>();
            colorpatterns.Add(arr[random.Next(0, arr.Length)]);
        }
        public Color getCurrentColor()
        {
            return colorpatterns[currentloc];
        }
        public int getLength()
        {
            return colorpatterns.Count;
        }
        public Color getCorrectColor()
        {
            return colorpatterns[currentloc];
        }
    }
}
