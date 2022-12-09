using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public GameObject player, cam, tapToPlay;
    public Transform camPosStart;
    bool doOnce,gameStarted;
    void StartGame()
    {
        if(gameStarted)
        {
            player.GetComponent<PlayerMovement>().enabled = true;
            tapToPlay.SetActive(false);
        }
    }
    public void TapToPlay()
    {
        if(!doOnce)
        {
            StartCoroutine(StartRoutine());
        }
    }
    IEnumerator StartRoutine()
    {
        cam.transform.DOMove(camPosStart.position, 1.5f);
        cam.transform.DORotate(new Vector3(25,0,0), 1.5f);
        yield return new WaitForSeconds(1.5f);
        cam.GetComponent<CameraFollow>().enabled = true;
        doOnce = true;
        gameStarted = true;
        StartGame();
    }
}
