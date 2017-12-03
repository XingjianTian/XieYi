using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShot : MonoBehaviour {
    public GameObject explosion;
    public GameObject hero;
    public PlayerControl pc;
    public GameObject CannonOnly;
    public GameObject ShotPoint;
    public float FireCrackersAdded = 0;
	// Use this for initialization
	void Start () {
        pc = hero.GetComponent<PlayerControl>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnExplode()
    {
        // Create a quaternion with a random rotation in the z-axis.
        Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

        // Instantiate the explosion where the rocket is with the random rotation.
        Instantiate(explosion, transform.position, randomRotation);
    }
    void DestroyExpolosion()
    {
        // Destroy this gameobject, this can be called from an Animation Event.
        Destroy(gameObject);
    }
    public IEnumerator RollToShot()
    {
        hero.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        if(!pc.facingRight)
            pc.Flip();
        pc.enabled = false;
        pc.anim.SetTrigger("Ride");
        CannonOnly.GetComponent<SpriteRenderer>().sortingOrder = 4;
        for (float i = 0; i < 45; i += Time.deltaTime * 50)
        {
            CannonOnly.transform.rotation = Quaternion.Euler(0, 0, i);
            hero.transform.position = ShotPoint.transform.position;
            yield return 0;
        }
        //计时器
        for (float i = 0; i < 45; i += Time.deltaTime * 80)
            yield return 0;
        hero.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        
        //hero.GetComponent<PlayerControl>().allowable = true;
        OnExplode();
        hero.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(new Vector3(2.8f, 9.5f, 0));
        yield return new WaitForSeconds(1.8f);
        CannonOnly.GetComponent<SpriteRenderer>().sortingOrder = 1;
        pc.enabled = true;
    }
}
