using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject ChipPrefab; // Prefab of the cip to spawn
    private GameObject CurrentChip; //active chip in the spawner
    public float FloatingHeigth = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Le spawner a démarré !");
        SpawnChip();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnChip()
    {
        
        if (ChipPrefab)
        {
            Debug.Log("Chip Spawn !");

            CurrentChip = Instantiate(ChipPrefab, transform.position + Vector3.up * FloatingHeigth, Quaternion.identity);

            CurrentChip.transform.SetParent(this.transform);
            CurrentChip.GetComponent<Rigidbody>().isKinematic = true;

            ChipRotation chipScript = CurrentChip.AddComponent<ChipRotation>();
            chipScript.SetSpawner(this);
        }else
        {
            Debug.Log("Chip n'a pas été Spawn !");
        }
    }
}
