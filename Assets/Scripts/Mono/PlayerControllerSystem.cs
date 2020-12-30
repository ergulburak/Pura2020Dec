using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerSystem : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Run,
        Move,
        Crouch,
        Process,
        Attack,
        Jump,
        Roar,
        Dead
    }

    [Header("Karakter State")] public PlayerState playerState = PlayerState.Idle;
    public PlayerState playerActionState = PlayerState.Idle;
    [Header("Kontrol Tuşları")] public KeyCode upKeyCode = KeyCode.W;
    public KeyCode downKeyCode = KeyCode.S;
    public KeyCode rightKeyCode = KeyCode.D;
    public KeyCode leftKeyCode = KeyCode.A;
    public KeyCode jumpKeyCode = KeyCode.Space;
    public KeyCode runKeyCode = KeyCode.LeftShift;
    public KeyCode crouchKeyCode = KeyCode.LeftControl;
    public KeyCode processKeyCode = KeyCode.E;
    public KeyCode roarKeyCode = KeyCode.Q;
    public KeyCode fireKeyCode = KeyCode.Mouse0;
    [Header("Karakter Özellikleri")] public float moveSpeed = 5.0f;
    public float jumpPower = 5.0f;
    public float runMmltiplier = 1.5f;
    public float crouchMultiplier = .5f;
    public float noiceLevel = 0.0f;
    public float camouflage = 0.0f;

    private Rigidbody _rb;
    private bool isGrounded = false;
    private bool isCamouflageble = false;

    public bool IsCamouflageble()
    {
        return isCamouflageble;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (playerState == PlayerState.Dead)
            _rb.velocity = Vector3.zero;
        if (playerState != PlayerState.Dead && playerActionState != PlayerState.Dead)
        {
            if (playerState != PlayerState.Roar && playerState != PlayerState.Process)
            {
                if (Input.GetKey(fireKeyCode))
                {
                    playerState = PlayerState.Attack;
                    if (Input.GetKey(runKeyCode))
                    {
                        playerActionState = PlayerState.Run;
                        if (Input.GetKey(upKeyCode))
                        {
                            transform.position += Vector3.forward * moveSpeed * runMmltiplier * Time.deltaTime;
                        }

                        if (Input.GetKey(downKeyCode))
                        {
                            transform.position += Vector3.back * moveSpeed * runMmltiplier * Time.deltaTime;
                        }

                        if (Input.GetKey(rightKeyCode))
                        {
                            transform.position += Vector3.right * moveSpeed * runMmltiplier * Time.deltaTime;
                        }

                        if (Input.GetKey(leftKeyCode))
                        {
                            transform.position += Vector3.left * moveSpeed * runMmltiplier * Time.deltaTime;
                        }

                        if (Input.GetKeyDown(jumpKeyCode) && isGrounded)
                        {
                            _rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                        }
                    }
                    else if (Input.GetKey(crouchKeyCode))
                    {
                        playerActionState = PlayerState.Crouch;
                        if (Input.GetKey(upKeyCode))
                        {
                            transform.position += Vector3.forward * moveSpeed * crouchMultiplier * Time.deltaTime;
                        }

                        if (Input.GetKey(downKeyCode))
                        {
                            transform.position += Vector3.back * moveSpeed * crouchMultiplier * Time.deltaTime;
                        }

                        if (Input.GetKey(rightKeyCode))
                        {
                            transform.position += Vector3.right * moveSpeed * crouchMultiplier * Time.deltaTime;
                        }

                        if (Input.GetKey(leftKeyCode))
                        {
                            transform.position += Vector3.left * moveSpeed * crouchMultiplier * Time.deltaTime;
                        }

                        if (Input.GetKeyDown(jumpKeyCode) && isGrounded)
                        {
                            _rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                        }
                    }
                    else if (Input.GetKey(upKeyCode) || Input.GetKey(downKeyCode) || Input.GetKey(rightKeyCode) ||
                             Input.GetKey(leftKeyCode))
                    {
                        playerActionState = PlayerState.Move;
                        if (Input.GetKey(upKeyCode))
                        {
                            transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
                        }

                        if (Input.GetKey(downKeyCode))
                        {
                            transform.position += Vector3.back * moveSpeed * Time.deltaTime;
                        }

                        if (Input.GetKey(rightKeyCode))
                        {
                            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
                        }

                        if (Input.GetKey(leftKeyCode))
                        {
                            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
                        }

                        if (Input.GetKeyDown(jumpKeyCode) && isGrounded)
                        {
                            _rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                        }
                    }
                    else
                    {
                        playerActionState = PlayerState.Idle;
                    }

                    if (!isGrounded)
                        playerActionState = PlayerState.Jump;
                }
                else if (Input.GetKey(runKeyCode))
                {
                    playerState = PlayerState.Run;
                    if (Input.GetKey(upKeyCode))
                    {
                        transform.position += Vector3.forward * moveSpeed * runMmltiplier * Time.deltaTime;
                    }

                    if (Input.GetKey(downKeyCode))
                    {
                        transform.position += Vector3.back * moveSpeed * runMmltiplier * Time.deltaTime;
                    }

                    if (Input.GetKey(rightKeyCode))
                    {
                        transform.position += Vector3.right * moveSpeed * runMmltiplier * Time.deltaTime;
                    }

                    if (Input.GetKey(leftKeyCode))
                    {
                        transform.position += Vector3.left * moveSpeed * runMmltiplier * Time.deltaTime;
                    }

                    if (Input.GetKeyDown(jumpKeyCode) && isGrounded)
                    {
                        _rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                    }

                    if (!isGrounded)
                        playerActionState = PlayerState.Jump;
                    else if (Input.GetKey(upKeyCode) || Input.GetKey(downKeyCode) || Input.GetKey(rightKeyCode) ||
                             Input.GetKey(leftKeyCode))
                    {
                        playerActionState = PlayerState.Move;
                    }
                    else
                    {
                        playerActionState = PlayerState.Idle;
                    }
                }
                else if (Input.GetKey(crouchKeyCode))
                {
                    playerState = PlayerState.Crouch;
                    if (Input.GetKey(upKeyCode))
                    {
                        transform.position += Vector3.forward * moveSpeed * crouchMultiplier * Time.deltaTime;
                    }

                    if (Input.GetKey(downKeyCode))
                    {
                        transform.position += Vector3.back * moveSpeed * crouchMultiplier * Time.deltaTime;
                    }

                    if (Input.GetKey(rightKeyCode))
                    {
                        transform.position += Vector3.right * moveSpeed * crouchMultiplier * Time.deltaTime;
                    }

                    if (Input.GetKey(leftKeyCode))
                    {
                        transform.position += Vector3.left * moveSpeed * crouchMultiplier * Time.deltaTime;
                    }

                    if (!isGrounded)
                        playerActionState = PlayerState.Jump;
                    else if (Input.GetKey(upKeyCode) || Input.GetKey(downKeyCode) || Input.GetKey(rightKeyCode) ||
                             Input.GetKey(leftKeyCode))
                    {
                        playerActionState = PlayerState.Move;
                    }
                    else
                    {
                        playerActionState = PlayerState.Idle;
                    }
                }
                else if (Input.GetKey(upKeyCode) || Input.GetKey(downKeyCode) || Input.GetKey(rightKeyCode) ||
                         Input.GetKey(leftKeyCode))
                {
                    playerState = PlayerState.Move;
                    if (Input.GetKey(upKeyCode))
                    {
                        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
                    }

                    if (Input.GetKey(downKeyCode))
                    {
                        transform.position += Vector3.back * moveSpeed * Time.deltaTime;
                    }

                    if (Input.GetKey(rightKeyCode))
                    {
                        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
                    }

                    if (Input.GetKey(leftKeyCode))
                    {
                        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
                    }

                    if (Input.GetKeyDown(jumpKeyCode) && isGrounded)
                    {
                        _rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                    }

                    if (!isGrounded)
                        playerActionState = PlayerState.Jump;
                    else if (Input.GetKey(upKeyCode) || Input.GetKey(downKeyCode) || Input.GetKey(rightKeyCode) ||
                             Input.GetKey(leftKeyCode))
                    {
                        playerActionState = PlayerState.Move;
                    }
                    else
                    {
                        playerActionState = PlayerState.Idle;
                    }
                }
                else if (Input.GetKeyDown(jumpKeyCode) && isGrounded)
                {
                    _rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                }
                else if (Input.GetKey(processKeyCode) && isCamouflageble)
                {
                    playerState = PlayerState.Process;
                    Debug.Log("Processing");
                    WaitingCooldown(1.5f);
                    camouflage = 100.0f;
                }
                else if (Input.GetKey(roarKeyCode))
                {
                    playerState = PlayerState.Roar;
                    Debug.Log("Roar");
                    WaitingCooldown(2.0f);
                    noiceLevel = 100.0f;
                }
                else
                {
                    playerState = PlayerState.Idle;
                }


                if (playerState != PlayerState.Attack && playerState != PlayerState.Crouch)
                {
                    playerActionState = PlayerState.Idle;
                }

                if (playerState == PlayerState.Idle || playerState == PlayerState.Move ||
                    playerState == PlayerState.Run)
                {
                    if (!isGrounded)
                        playerActionState = PlayerState.Jump;
                }

                RaycastHit hit;
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                var layerMask = LayerMask.GetMask("Floor");

                if (Physics.Raycast(ray, out hit, 100, layerMask))
                {
                    Vector3 relativePos = hit.point - transform.position;
                    Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                    transform.rotation = new Quaternion(0, rotation.y, 0, rotation.w);
                }

                if (GameObject.Find("Noice System").GetComponent<NoiceSystem>().NoiceLevel(this.gameObject) >
                    noiceLevel)
                {
                    noiceLevel = GameObject.Find("Noice System").GetComponent<NoiceSystem>()
                        .NoiceLevel(this.gameObject);
                }
                else
                {
                    noiceLevel -= .6f;
                }
            }
        }
    }

    private async Task WaitingCooldown(float delayTime)
    {
        await Task.Delay(TimeSpan.FromSeconds(delayTime));

        playerState = PlayerState.Idle;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Floor")) isGrounded = true;
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Floor")) isGrounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            GameObject.FindObjectOfType<PlayerUISystem>().FinishOpen();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<ZombieSystem>() != null)
        {
            if (other.gameObject.layer == 9 && other.gameObject.GetComponent<ZombieSystem>().zombieState ==
                ZombieSystem.ZombieState.Dead)
            {
                isCamouflageble = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<ZombieSystem>() != null)
        {
            if (other.gameObject.layer == 9 && other.gameObject.GetComponent<ZombieSystem>().zombieState ==
                ZombieSystem.ZombieState.Dead)
            {
                isCamouflageble = false;
            }
        }
    }
}