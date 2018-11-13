using Verse;

namespace TVChannels
{
    
    public class ShowWorkerOrdinal
    {
        public ChannelDef def { get; set; }
        public ThingWithComps tv { get; set; }

        protected int cur = 0;
        public virtual int GetShow()
        {
            return cur;
        }
        
        public virtual void NextShow()
        {
            var showsCount = def.shows.Count;
            cur++;
            cur %= showsCount;
        }
        
        public int ticksToCycle = -1;
        
        public virtual void Tick()
        {
            if (Find.TickManager.TicksGame <= ticksToCycle) return;
            
            ticksToCycle = Find.TickManager.TicksGame + def.secondsBetweenShows.SecondsToTicks();
                
            NextShow();
        }
    }

    public class ShowWorkerRandom : ShowWorkerOrdinal
    {
        public override void NextShow()
        {
            cur = Rand.Range(0, def.shows.Count);
            
        }
    }
    
    
    public class ShowWorkerWeather : ShowWorkerRandom
    {
        public override void NextShow()
        {
            if(Rand.Bool)
                base.NextShow();
        }
    }
}