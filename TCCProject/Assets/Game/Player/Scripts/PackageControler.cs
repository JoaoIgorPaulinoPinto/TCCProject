using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PackageControler : MonoBehaviour
{

    public GameObject packagePrefab;
    public GameObject dropedPackege;    
    public float timeToWaitBeforeDrop;
    public float timeToWaitBeforeGet;
    public float minDistance;

    public float packageWeight;

    public bool wtPackage;

    [SerializeField] GameObject btn_drop, btn_get;

    public void Btn_Drop()
    {

            dropedPackege = GameObject.Instantiate(packagePrefab, transform.position, packagePrefab.transform.rotation);
            RemoveWeight();
            btn_drop.SetActive(false);
            btn_get.SetActive(true);
        
    }
    public void Btn_Get()
    {

        if (packagePrefab != null)
        {
            if (Vector2.Distance(dropedPackege.transform.position, transform.position) < minDistance)
            {
                Destroy(dropedPackege);
                AddWeight();
                btn_get.SetActive(false);
                btn_drop.SetActive(true);

                dropedPackege = null;
            }
        }
    }
    void AddWeight()
    {
        GetComponent<Rigidbody2D>().mass += packageWeight;
    }
    private void RemoveWeight()
    {
        GetComponent<Rigidbody2D>().mass -= packageWeight;
    }
}
