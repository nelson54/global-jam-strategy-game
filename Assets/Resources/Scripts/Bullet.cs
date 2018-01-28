using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletTyper { Normal, ArmorPiercing }

public class Bullet : MonoBehaviour {

    [SerializeField] float BulletDamage;
    [SerializeField] float DestroyTime;
    private float Timer;
    public BulletTyper BulletType;

    // Use this for initialization
    void Start () {
        Timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collider.GetComponent<Enemy>().LoseHealth(BulletDamage, BulletType);
            Destroy(gameObject);
        }
    }

    private void DestroyBulletsAfterTime()
    {
        Timer += Time.deltaTime;
        if (Timer > DestroyTime)
        {
            Destroy(gameObject);
            Timer = 0f;
        }
    }
}
