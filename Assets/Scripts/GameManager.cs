using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Animator anim;   // For the dorr animation
    public GameObject uiPanel;

    private void Start()
    {
        uiPanel.SetActive(false);
    }
    private void Update()
    {
        GameObject myObject = GameObject.FindWithTag("shrine");

        if(myObject == null)
        {
            Debug.Log("showing null");
            anim.SetBool("Open", true);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == ("spikes"))
        {
            Restart();
        }

        if (collision.gameObject.transform.GetChild(0).CompareTag("shrine"))
        {
            audioManager.instance.CollectAudio();
            GameObject childShrine = collision.gameObject.transform.GetChild(0).gameObject;
            Destroy(childShrine);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("win"))
        {
            Debug.Log("Enter in single door1");
            audioManager.instance.WinAudio();
            uiPanel.SetActive(true);
        }
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}


