using UnityEngine;

namespace Assets.Scripts
{
    public class NetworkManager : MonoBehaviour
    {

        private SpawnSpot[] spawnSpots;

        // Use this for initialization
        void Start ()
        {
            spawnSpots = FindObjectsOfType<SpawnSpot>();

	        Connect();
        }

        private void Connect()
        {
            PhotonNetwork.ConnectUsingSettings("1.0.0");
            
        }

        void OnGUI()
        {
            GUILayout.Label( PhotonNetwork.connectionStateDetailed.ToString());
        }

        void OnJoinedLobby()
        {
            Debug.Log("OnJoinedLobby");
            PhotonNetwork.JoinRandomRoom();
        }

        void OnPhotonRandomJoinFailed()
        {
            Debug.Log("OnPhotonRandomJoinFailed");
            PhotonNetwork.CreateRoom(null);
        }

        private void OnJoinedRoom()
        {
            Debug.Log("OnJoinedRoom");
            SpawnMyPLayer();
        }

        private void SpawnMyPLayer()
        {
            var mySpawnSpot = spawnSpots[Random.Range(0, spawnSpots.Length)];
            var myPlayerGO = (GameObject)PhotonNetwork.Instantiate("PlayerController", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);
            //myPlayerGO.GetComponent<FPSInputController>().enabled = true;
            myPlayerGO.GetComponent<MouseLook>().enabled = true;
            myPlayerGO.transform.FindChild("Main Camera").gameObject.SetActive(true);
            myPlayerGO.GetComponent<PlayerMovement>().enabled = true;
            //((MonoBehaviour) myPlayerGO.GetComponent("CharacterMotor")).enabled = true;
        }
    }
}
