using UnityEngine;

public class AutoPickUpKey : Key
{
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        player.GetComponent<PlayerHand>().PickUpKey(this);
    }
}
