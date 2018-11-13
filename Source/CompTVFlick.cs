
using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace TVChannels
{
    public class CompTVFlick : ThingComp
    {
        private int channel = 0;

        private Channel curChannel;
        

        private ThingComp CurrentChannel()
        {
            if(curChannel != null) return curChannel;
            
            curChannel = Props?.Channel(channel, parent);

            if (curChannel == null)
            {
                Log.ErrorOnce("No channel", 1700707);
                return null;
            }
            else return curChannel;


        }

        public override void PostDraw()
        {

            base.PostDraw();
            
            if(parent.TryGetComp<CompPowerTrader>().PowerOn)
                CurrentChannel()?.PostDraw();
        }


        public override void CompTick()
        {
            CurrentChannel()?.CompTick();

            base.CompTick();
        }

        public override void CompTickRare()
        {
            CurrentChannel()?.CompTickRare();

            base.CompTickRare();
        }


        public CompProp_TV Props => (CompProp_TV) props;

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            foreach (Gizmo c in base.CompGetGizmosExtra())
            {
                yield return c;
            }


            yield return new Command_Action
            {
                hotKey = KeyBindingDefOf.Misc6,
                defaultLabel = "Next Channel",
                defaultDesc = "Click to the next channel!",
                action = ChangeChannel
            };

            yield break;
        }



        private void ChangeChannel()
        {
            var count = Props?.channels?.Count ?? 1;

            channel = (channel + 1) % count;
            curChannel = null;
        }
    }
}