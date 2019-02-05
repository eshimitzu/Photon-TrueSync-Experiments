using Photon.Pun;
using TrueSync;
using UnityEngine;

public class SimulationPanelScript : MonoBehaviour {
    
    public void Update() {
        gameObject.SetActive(!PhotonNetwork.IsConnected || PhotonNetwork.IsMasterClient);
    }

    public void BtnRun() {
        TrueSyncManager.RunSimulation();
    }

    public void BtnPause() {
        TrueSyncManager.PauseSimulation();
    }

    public void BtnEnd() {
        TrueSyncManager.EndSimulation();
    }

}