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
    public class FadeEffect : EffectInstance
    {
        private Sprite spr;
        private double time2bdrawn;
        private double time2fade;
        private double currenttime;
        private Boolean finishedFirst;
        private Color clr;
        public Boolean isdrawn;
        public FadeEffect(Boolean isdrawn,double time2bedrawn,double time2fade,Texture2D tex,Vector2 pos)
            : base(isdrawn)
        {
            this.time2bdrawn = time2bedrawn;
            this.time2fade = time2fade;
            this.currenttime = 0;
            this.finishedFirst = false;
            this.clr = Color.White;

            spr = new Sprite(tex);
            spr.setPosition(pos);
        }
        public override void draw()
        {
            if (!finishedFirst)
            {
                spr.draw();
            }
            else
            {
                clr *= 0.95f;
                spr.draw(clr);
            }
        }
        public override Boolean update(GameTime gametime)
        {
            currenttime += gametime.ElapsedGameTime.TotalMilliseconds;
            
            if (currenttime > time2bdrawn)
            {
                finishedFirst = true;
                currenttime=0;
            }
            if (finishedFirst)
            {
                return currenttime > time2fade;
            }

            return false;
        }
    }
}
