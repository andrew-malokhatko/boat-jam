using System.Collections;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows;

public class PirateSpawner : MonoBehaviour
{
    public float spawnInterval = 5f;
    public GameObject piratePrefab1;
    public GameObject piratePrefab2;
    //public GameObject pirate3;
    //public GameObject pirate4;
    //public GameObject pirate5;
    //public GameObject pirate6;
    public Transform pirateTransform1;
    public Transform pirateTransform2;
    //public Transform pirateTransform3;
    //public Transform pirateTransform4;
    //public Transform pirateTransform5;
    //public Transform pirateTransform6;
    private Vector3 coordsPirate1;
    private Vector3 coordsPirate2;
    private Vector3 coordsPirate3;
    private Vector3 coordsPirate4;
    private Vector3 coordsPirate5;
    private Vector3 coordsPirate6;

    void Start()
    {
        coordsPirate1 = pirateTransform1.position;
        coordsPirate2 = pirateTransform2.position;
        //coordsPirate3 = pirateTransform3.position;
        //coordsPirate4 = pirateTransform4.position;
        //coordsPirate5 = pirateTransform5.position;
        //coordsPirate6 = pirateTransform6.position;
    }

    private void Update()
    {

    }

    public IEnumerator SpawnPirates(string num)
    {
        string numberStr = new string(num.Where(char.IsDigit).ToArray());
        int number = int.Parse(numberStr);

        yield return new WaitForSeconds(spawnInterval);
        if (number == 1)
            Instantiate(piratePrefab1, coordsPirate1, Quaternion.identity);
        else if (number == 2)
            Instantiate(piratePrefab2, coordsPirate2, Quaternion.identity);
        else if (number == 3)
            Instantiate(piratePrefab2, coordsPirate2, Quaternion.identity);
        else if (number == 4)
            Instantiate(piratePrefab2, coordsPirate2, Quaternion.identity);
        else if (number == 5)
            Instantiate(piratePrefab2, coordsPirate2, Quaternion.identity);
        else if (number == 6)
            Instantiate(piratePrefab2, coordsPirate2, Quaternion.identity);
        //yield return new WaitForSeconds(0.01f);
    }
}
