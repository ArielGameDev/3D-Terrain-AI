using UnityEngine;

/**
 * This component animates a door depending on whether a player or an enemy is nearby.
 */
public class PlayAnimOnEnter : MonoBehaviour
{
    private Animation _animation;
    bool active = true;

    void Start()
    {
        _animation = GetComponent<Animation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!active)
            return;

        if (other.tag == "Player")
        {
            _animation.Play();
            active = false;
        }
    }

}
