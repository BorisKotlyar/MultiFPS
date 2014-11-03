using UnityEngine;

namespace Assets.Scripts
{
    public class NetworkCharacter : Photon.MonoBehaviour
    {

        private Vector3 _realPosition = Vector3.zero;
        private Quaternion _realRotation = Quaternion.identity;
        private Animator _anim;

        // Use this for initialization
        void Start ()
        {
            _anim = GetComponent<Animator>();
        }
	
        // Update is called once per frame
        void Update () {
            if (photonView.isMine)
            {
	        
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, _realPosition, 0.1f);
                transform.rotation = Quaternion.Lerp(transform.rotation, _realRotation, 0.1f);
            }
        }

        void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.isWriting)
            {
                stream.SendNext(transform.position);
                stream.SendNext(transform.rotation);
                stream.SendNext(_anim.GetFloat("Speed"));
                stream.SendNext(_anim.GetBool("Jumping"));
            }
            else
            {
                _realPosition = (Vector3) stream.ReceiveNext();
                _realRotation = (Quaternion) stream.ReceiveNext();
                _anim.SetFloat("Speed", (float)stream.ReceiveNext());
                _anim.SetBool("Jumping", (bool)stream.ReceiveNext());
            }
        }
    }
}
