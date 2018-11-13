using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;


namespace TVChannels
{
    public class ChannelDef : Def
    {
        public Type showWorker;
        public List<CompProperties> shows = new List<CompProperties>();
        public float secondsBetweenShows = Rand.RangeInclusive(300, 5000);
    }
}