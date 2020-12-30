using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiceSystem : MonoBehaviour
{
    private int noiceLevel = 0;

    public int NoiceLevel(GameObject other)
    {
        noiceLevel = 0;
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerControllerSystem>().playerState == PlayerControllerSystem.PlayerState.Move)
            {
                noiceLevel = 20;
            }
            else if (other.GetComponent<PlayerControllerSystem>().playerState == PlayerControllerSystem.PlayerState.Run)
            {
                noiceLevel = 40;
            }
            else if (other.GetComponent<PlayerControllerSystem>().playerState == PlayerControllerSystem.PlayerState.Process)
            {
                noiceLevel = 10;
            }
            else if (other.GetComponent<PlayerControllerSystem>().playerState == PlayerControllerSystem.PlayerState.Roar)
            {
                noiceLevel = 100;
            }
            else if (other.GetComponent<PlayerControllerSystem>().playerState ==
                     PlayerControllerSystem.PlayerState.Crouch)
            {
                noiceLevel = 5;
            }
            else if (other.GetComponent<PlayerControllerSystem>().playerState ==
                     PlayerControllerSystem.PlayerState.Attack &&
                     other.GetComponent<PlayerControllerSystem>().playerActionState ==
                     PlayerControllerSystem.PlayerState.Idle)
            {
                noiceLevel = 60;
            }
            else if (other.GetComponent<PlayerControllerSystem>().playerState ==
                     PlayerControllerSystem.PlayerState.Attack &&
                     other.GetComponent<PlayerControllerSystem>().playerActionState ==
                     PlayerControllerSystem.PlayerState.Move)
            {
                noiceLevel = 80;
            }
            else if (other.GetComponent<PlayerControllerSystem>().playerState ==
                     PlayerControllerSystem.PlayerState.Attack &&
                     other.GetComponent<PlayerControllerSystem>().playerActionState ==
                     PlayerControllerSystem.PlayerState.Crouch)
            {
                noiceLevel = 65;
            }
            else if (other.GetComponent<PlayerControllerSystem>().playerState ==
                     PlayerControllerSystem.PlayerState.Attack &&
                     other.GetComponent<PlayerControllerSystem>().playerActionState ==
                     PlayerControllerSystem.PlayerState.Run)
            {
                noiceLevel = 80;
            }

            if (other.GetComponent<PlayerControllerSystem>().playerActionState ==
                PlayerControllerSystem.PlayerState.Jump)
            {
                noiceLevel += 10;
            }
        }

        if (other.CompareTag("Bandit"))
        {
        }

        return noiceLevel;
    }
}