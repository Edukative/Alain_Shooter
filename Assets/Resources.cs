using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    PlayerController stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "player")
        {
            switch (this.transform.tag)
            {
                case ("hp"):
                    if (stats.health == 100)
                    {
                        //don't anyting
                    }
                    else if (stats.health < 100)
                    {
                        stats.health += 25;
                        Destroy(this.gameObject);
                    }
                    break;
            }
        }
    }
}
