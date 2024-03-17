using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimEvents : MonoBehaviour
{
    private player player;
    void Start()
    {
        player = GetComponentInParent<player>();   
    }

    private void AnimationTrigger()
    {
        player.AttackOver();
    }

    void Update()
    {
        
    }
}
