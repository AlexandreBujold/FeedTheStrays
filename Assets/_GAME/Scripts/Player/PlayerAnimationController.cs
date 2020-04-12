using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{

    public PlayerController playerController;
    public Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerAnimator != null && playerController != null)
        {
            playerAnimator.SetBool("Moving", (playerController.velocity.magnitude > 0.2f ? true : false));
        }
    }
}
