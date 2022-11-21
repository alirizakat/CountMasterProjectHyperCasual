using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;

public class CountManager : MonoBehaviour
{
    public Transform player;
    private int countCharacter;
    [SerializeField] private GameObject characterPrefab;
    [Range(0f, 1f)][SerializeField] private float distanceFactor, radius;
    public List<GameObject> characterList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        player = transform;
        countCharacter = transform.childCount - 1; 
    }

    // Update is called once per frame
    void Update()
    {
        MakeCharacterList();
    }
    void MakeCharacterList()
    {
        characterList.Clear();
        List<GameObject> tempList = new List<GameObject>();
        tempList = new List<GameObject>(GameObject.FindGameObjectsWithTag("characters"));
        if (tempList.Count > 0)
        {
            foreach(GameObject fooObj in GameObject.FindGameObjectsWithTag("characters")) 
            {
                characterList.Add(fooObj);
            }
        }
        else
        {
            return;
        }
    }
    private void FormatCharacters()
    {
        for(int i = 0; i < player.childCount; i++)
        {
            var x = distanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * radius);
            var z = distanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * radius);

            var newPos = new Vector3 (x,0,z);
            player.transform.GetChild(i).DOLocalMove(newPos, 1f).SetEase(Ease.OutBack);

        }
    }
    private void MakeCharacter(int number)
    {
        for(int i = 0; i < number; i++)
        {
            GameObject temp = Instantiate(characterPrefab, transform.position, Quaternion.identity, transform);
            temp.GetComponent<Animator>().SetBool("run", true);
        }
        countCharacter = transform.childCount - 1;
        FormatCharacters();
    }
    private void DestroyCharacter(int number)
    {
        for(int i = number; i > 0; i--)
        {
            Destroy(characterList[i]);
            characterList.RemoveAt(i);
        }
        countCharacter = transform.childCount - 1;
        FormatCharacters();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Gate"))
        {   
            var gateManager = other.GetComponent<GateManager>();
            if(gateManager.multiply)
            {
                MakeCharacter(countCharacter * gateManager.randomNumber);
            }
            else if(gateManager.negative)
            {
                if(gateManager.randomNumber < countCharacter)
                {
                    DestroyCharacter(countCharacter - gateManager.randomNumber);
                }
            }
            else
            {
                MakeCharacter(countCharacter + gateManager.randomNumber);
            }

        }
    }
}
