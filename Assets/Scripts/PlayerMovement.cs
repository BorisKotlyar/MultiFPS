using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {

        private float _speed = 10.0f;
        private float _jumpSpeed = 10.0f;
        private Vector3 _direction = Vector3.zero;
        private float _verticalVelocity = 0;
        private CharacterController _cc;
        private Animator _anim;
        private CapsuleCollider _col;

        // Use this for initialization
        void Start ()
        {
            _cc = GetComponent<CharacterController>();
            _anim = GetComponent<Animator>();
            _col = GetComponent<CapsuleCollider>();
        }
	
        // Update is called once per frame
        void Update () {

            // WASD
	        _direction = transform.rotation * new Vector3( Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            if (_direction.magnitude > 1.0f)
            {
                _direction = _direction.normalized;
            }

            _anim.SetFloat("Speed", _direction.magnitude);


            // Handle Jumping
            if (_cc.isGrounded && Input.GetButton("Jump"))
            {
                _verticalVelocity = _jumpSpeed;
            }            
        }

        void FixedUpdate()
        {
            Vector3 dist = _direction*_speed*Time.deltaTime;

            if (_cc.isGrounded && _verticalVelocity < 0)
            {
                print("must not jump");
                _anim.SetBool("Jumping",false);
                _verticalVelocity = Physics.gravity.y*Time.deltaTime;
            }
            else
            {
                if (Mathf.Abs(_verticalVelocity) > _jumpSpeed*0.75f)
                {
                    print("must not jump");
                    _anim.SetBool("Jumping",true);
                }

                _verticalVelocity += Physics.gravity.y*Time.deltaTime;
            }

            dist.y = _verticalVelocity*Time.deltaTime;

            _cc.Move(dist);
        }
    }
}
