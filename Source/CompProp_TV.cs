using System.Collections.Generic;
using System.Linq;
using Verse;

namespace TVChannels
{
    public class CompProp_TV : CompProperties
    {
        public float yOffset = 0f, xOffset = 0f, xScale = 1f, yScale = 1f;
        public List<ChannelDef> channels = new List<ChannelDef>();
        public List<Channel> _channels => channels.Select(x => new Channel(x, this)).ToList();

        public CompProp_TV()
        {
            Log.Message("New TV");
            compClass = typeof(CompTVFlick);
            channels.Shuffle();
        }

        public Channel Channel(int channel, ThingWithComps parent)
        {
            int count = _channels?.Count ?? 1;
            channel = (count + channel) % count; //loose abs when back step
            var res =   _channels?[channel];
            res.parent = parent;
            return res;
        }
    }
}