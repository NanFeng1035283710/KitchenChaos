using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家声音
/// </summary>
public class PlayerSound : MonoBehaviour
{
    private Player player;
    private float footsepTimer;
    private float footsepTimerMax = 0.1f;
    private void Awake()
    {
        player = GetComponent<Player>();
    }
    private void Update()
    {
        footsepTimer -= Time.deltaTime;
        if (footsepTimer < 0f)
        {
            footsepTimer = footsepTimerMax;
            if (player.IsWalking())
            {
                SoundManager.Instance.PlayFootstepsSound(player.transform.position, 1f);
            }
        }
    }
}
