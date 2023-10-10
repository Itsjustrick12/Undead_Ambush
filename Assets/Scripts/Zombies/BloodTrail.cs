using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodTrail : MonoBehaviour
{
    public GameObject bloodPrefab;
    public int maxBloodInstances = 20;
    public float bloodSpacing = 0.1f;
    public float destroyDelay = 3f;

    private bool finished = false;

    public List<GameObject> bloodInstances = new List<GameObject>();
    public List<Sprite> bloodSprites = new List<Sprite>();
    public Sprite bloodSplat;

    private GameObject effectsContainer;

    private void Start()
    {
        effectsContainer = GameObject.Find("Effects Container");
    }
    

    public void CreateBloodInstance()
    {
        if (!finished)
        {

            // Calculate the position offset based on bloodSpacing
            Vector3 offset = new Vector3(0f, 0f, 0f);
            if (bloodInstances.Count > 0)
            {
                Vector3 previousPosition = bloodInstances[bloodInstances.Count - 1].transform.position;
                offset = (transform.position - previousPosition).normalized * bloodSpacing;
            }

            // Create a new blood instance with offset position
            GameObject blood = Instantiate(bloodPrefab, transform.position + offset, Quaternion.identity);
            blood.GetComponent<SpriteRenderer>().sprite = bloodSprites[Random.Range(1, 3)];
            blood.GetComponent<BloodPiece>().SetTrail(this);
            blood.GetComponent<BloodPiece>().SetDelay(destroyDelay);
            bloodInstances.Add(blood);
            blood.transform.parent = effectsContainer.transform;
            bloodInstances[0].GetComponent<SpriteRenderer>().sprite = bloodSprites[bloodSprites.Count-1];
        }

    }

    public void CreateBloodInstance(bool check)
    {
        if (!finished)
        {

            // Calculate the position offset based on bloodSpacing
            Vector3 offset = new Vector3(0f, 0f, 0f);
            if (bloodInstances.Count > 0)
            {
                Vector3 previousPosition = bloodInstances[bloodInstances.Count - 1].transform.position;
                offset = (transform.position - previousPosition).normalized * bloodSpacing;
            }

            // Create a new blood instance with offset position
            GameObject blood = Instantiate(bloodPrefab, transform.position + offset, Quaternion.identity);
            if (check == true)
            {
                blood.GetComponent<SpriteRenderer>().sprite = bloodSplat;
            }
            blood.GetComponent<BloodPiece>().SetTrail(this);
            blood.GetComponent<BloodPiece>().SetDelay(destroyDelay);
            bloodInstances.Add(blood);
            blood.transform.parent = effectsContainer.transform;
            bloodInstances[0].GetComponent<SpriteRenderer>().sprite = bloodSprites[bloodSprites.Count - 1];
        }

    }


    public void EndTrail ()
    {
        finished = true;
    }
}