using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Photon.Pun;

public class SceneLoader : MonoBehaviour {

	public string scene;
	public bool disconnect;

	public void Load() {
		if (disconnect) {
			PhotonNetwork.Disconnect ();
		}
		SceneManager.LoadScene (scene);
	}
}
