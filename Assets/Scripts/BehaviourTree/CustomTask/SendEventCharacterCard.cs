using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using ParadoxNotion.Services;

namespace TheWill
{
    [Category("✫ Utility")]
    [Description("Send a graph event. If global is true, all graph owners in scene will receive this event. Use along with the 'Check Event' Condition")]
    public class SendEventCharacterCard : ActionTask<GraphOwner>
    {
        [RequiredField]
        public BBParameter<string> eventName;
        public BBParameter<CharacterCard> card;
        public BBParameter<float> delay;
        public bool sendGlobal;

        protected override string info
        {
            get { return (sendGlobal ? "Global " : "") + "Send Event [" + eventName + "]" + (delay.value > 0 ? " after " + delay + " sec." : ""); }
        }

        protected override void OnUpdate()
        {
            if (elapsedTime >= delay.value)
            {
                var e = new EventData<CharacterCard>(eventName.value, card.value);
                if (sendGlobal)
                {
                    Graph.SendGlobalEvent(e);
                }
                else
                {
                    agent.SendEvent(e);
                }
                EndAction();
            }
        }
    }
}