using System.Net.Http.Headers;
using TMPro;
using UnityEngine;

public class testing : MonoBehaviour
{   
    public TextMeshPro texto;
    public EnemyHealth enemy;
    public float vida;
    
    void Start()
    {
        vida = enemy.Health;
    }

    // Update is called once per frame
    void Update()
    {
        vida = enemy.Health;
        texto.text = vida.ToString();
    }
}
