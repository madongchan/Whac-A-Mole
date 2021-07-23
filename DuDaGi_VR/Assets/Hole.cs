using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hole : MonoBehaviour
{
    Animator ani;
    bool touchPossible = false;
    AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip catchSound;
    public GameManager GM;
    bool bomb = false;
    
    void Start()
    {
        ani = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Open()
    {
        touchPossible = true;
        audioSource.clip = openSound;
        audioSource.Play();

        if(GM.Gs == GameState.Ready)
        {
            GM.Go();
            
        }
    }
    public void Close()
    {
        touchPossible = false;
    }
    private void OnMouseDown()
    {
        if(touchPossible)
        {
            touchPossible = false;
            ani.SetTrigger("isTouch");
            audioSource.clip = catchSound;
            audioSource.Play();

            if (!bomb)
            {
                GM.score += 100;
            }
            else
            {
                GM.score -= 500;
            }
        }
    }
    public IEnumerator End()
    {
        float randomTime = Random.Range(1.0f, 3.0f);
        float randomD = Random.Range(1.0f, 10.0f);
        yield return new WaitForSeconds(randomTime);

        if(GM.Gs != GameState.End)
        {
            if (randomD >= 5.0f)//20%
            {
                bomb = true;
                ani.SetTrigger("Bomb");
            }
            else
            {
                bomb = false;
                ani.SetTrigger("Open");
            }
        }
    }
    void Update()
    {
        
    }
}
