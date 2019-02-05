using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace TrueSync {

    /**
     *  @brief Truesync's {@link ICommunicator} implementation based on PUN. 
     **/
    public class PhotonTrueSyncCommunicator : ICommunicator {

        private LoadBalancingPeer loadBalancingPeer;

        public delegate void EventCallback(EventData eventData);

        private static EventCallback lastEventCallback;

        /**
         *  @brief Instantiates a new PhotonTrueSyncCommunicator based on a Photon's LoadbalancingPeer. 
         *  
         *  @param loadBalancingPeer Instance of a Photon's LoadbalancingPeer.
         **/
        internal PhotonTrueSyncCommunicator(LoadBalancingPeer loadBalancingPeer) {
            this.loadBalancingPeer = loadBalancingPeer;
        }

        public int RoundTripTime() {
            return loadBalancingPeer.RoundTripTime;
        }

        public void OpRaiseEvent(byte eventCode, object message, bool reliable, int[] toPlayers) {
            if (loadBalancingPeer.PeerState != PeerStateValue.Connected) {
                return;
            }

            RaiseEventOptions eventOptions = new RaiseEventOptions();
            eventOptions.TargetActors = toPlayers;

            SendOptions sendOptions = new SendOptions();
            sendOptions.Reliability = reliable;

            loadBalancingPeer.OpRaiseEvent(eventCode, message, eventOptions, sendOptions);
        }

        public void AddEventListener(OnEventReceived onEventReceived) {
            if (lastEventCallback != null)
            {
                PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
            }

            lastEventCallback = delegate (EventData eventData) { onEventReceived(eventData.Code, eventData.CustomData); };
            PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
        }

        private void NetworkingClient_EventReceived(EventData eventData)
        {
            lastEventCallback.Invoke(eventData);
        }
    }

}
