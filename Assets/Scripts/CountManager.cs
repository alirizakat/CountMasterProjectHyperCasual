using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;

public class CountManager : MonoBehaviour
{
    public Transform player;
    //public int countCharacter;
    [SerializeField] private GameObject characterPrefab;
    [Range(0f, 1f)][SerializeField] private float distanceFactor, radius;
    public List<GameObject> characterList = new List<GameObject>();
    ObjectPooler objectPooler;

    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        player = transform;
        //countCharacter = transform.childCount - 1;
    }

    void Update()
    {
        MakeCharacterList();
    }

    void MakeCharacterList()
    {
        characterList.Clear();
        List<Transform> tempList = new List<Transform>();
        tempList.Clear();
        foreach (Transform child in transform)
        {
            tempList.Add(child);
        }
        if (tempList.Count > 0)
        {
            foreach (Transform tempObj in tempList)
            {
                characterList.Add(tempObj.gameObject);
            }
        }
        else
        {
            return;
        }
    }

    private void FormatCharacters()
    {
        for (int i = 0; i < player.childCount; i++)
        {
            var x = distanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * radius);
            var z = distanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * radius);

            var newPos = new Vector3(x, 0, z);
            player.transform.GetChild(i).DOLocalMove(newPos, 1f).SetEase(Ease.OutBack);

        }
    }

    private void MakeCharacter(int number)
    {
        for (int i = 0; i < number - 1; i++)
        {
            GameObject temp = objectPooler.SpawnFromPool("characters", transform.position, Quaternion.identity);
            temp.GetComponent<Animator>().SetBool("run", true);
            temp.transform.parent = transform;
            Debug.Log("Number of times");
        }
        FormatCharacters();
    }

    private void DestroyCharacter(int number)
    {
        for (int i = number; i > 0; i--)
        {
            characterList[i].transform.parent = null;
            characterList[i].gameObject.SetActive(false);
        }
        FormatCharacters();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gate"))
        {
            var gateManager = other.GetComponent<GateManager>();
            if (gateManager.multiply)
            {
                MakeCharacter(characterList.Count * gateManager.randomNumber);
            }
            else if (gateManager.negative)
            {
                if (gateManager.randomNumber < characterList.Count)
                {
                    DestroyCharacter(gateManager.randomNumber);
                }
            }
            else
            {
                MakeCharacter(characterList.Count + gateManager.randomNumber);
            }
        }
    }
}
