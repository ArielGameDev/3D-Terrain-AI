using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeyYou : MonoBehaviour
{
    [SerializeField] private string Player = "Player";
    [SerializeField] private Text heyYou;
    [SerializeField] private Text pressE;

    void Start()
    {
        pressE.gameObject.SetActive(false);
        heyYou.gameObject.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == Player && heyYou.gameObject.activeSelf == false)
        {
            pressE.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                pressE.gameObject.SetActive(false);
                heyYou.gameObject.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        pressE.gameObject.SetActive(false);
        heyYou.gameObject.SetActive(false);
    }


}
