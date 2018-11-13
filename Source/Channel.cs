#define DEBUG
using System;
using CompAnimated;
using Verse;

namespace TVChannels
{
    public class Channel : ThingComp
    {
        private int _show = 0;
        private readonly ChannelDef _props;


        private CompProperties _childProp;
        private ThingComp _child;
        private ShowWorkerOrdinal _showWorker;
        public CompProp_TV TVProps;

        public Channel(ChannelDef props, CompProp_TV compPropTv)
        {
            _props = props;
            _showWorker = (ShowWorkerOrdinal) Activator.CreateInstance(_props.showWorker);
            _showWorker.def = _props;
            _showWorker.tv = parent;
            TVProps = compPropTv;
        }

        private ThingComp CurrentShow()
        {
            int cur = _show;

            _show = _showWorker.GetShow();

            if (cur == _show && _child != null && _childProp != null) return _child;

            _childProp = _props?.shows[_show];

            if (_childProp == null)
            {
                Log.ErrorOnce("No show", 1700709);
                return null;
            }


            _child = (ThingComp) Activator.CreateInstance(_childProp.compClass);
            if (_child is CompAnimatedOver over)
            {
                over.xOffset = TVProps.xOffset;
                over.yOffset = TVProps.yOffset;
                over.xScale = TVProps.xScale;
                over.yScale = TVProps.yScale;
            }

            _child.parent = parent;

            _child.Initialize(_childProp);
#if DEBUG
            Log.Message("Show Changed to : " + _show + " > " + _child);
#endif
            return _child;
        }

        public override void PostDraw()
        {
            base.PostDraw();
            //inject
            CurrentShow()?.PostDraw();
        }


        public override void CompTick()
        {
            CurrentShow()?.CompTick();
            
            _showWorker?.Tick();
            base.CompTick();
        }

        public override void CompTickRare()
        {
            CurrentShow()?.CompTickRare();
            _showWorker?.Tick();
            base.CompTickRare();
        }
    }
}