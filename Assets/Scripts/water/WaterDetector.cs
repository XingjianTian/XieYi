using UnityEngine;
using System.Collections;

public class WaterDetector : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D Hit)
    {
		if (Hit.name == "Boat" && Hit.GetComponent<Rigidbody2D>() != null)
        {
        	transform.parent.GetComponent<Water>().Splash(transform.position.x, Hit.GetComponent<Rigidbody2D>().velocity.y*Hit.GetComponent<Rigidbody2D>().mass / 50f);
        }
    }

	/*void OnTriggerStay2D(Collider2D Hit)
    {
        //print(Hit.name);
        if (Hit.GetComponent<Rigidbody2D>() != null)
        {
            int points = Mathf.RoundToInt(Hit.transform.localScale.x * 15f);
            for (int i = 0; i < points; i++)
            {
				transform.parent.GetComponent<Water>().Splash(Hit.transform.position.x - Hit.transform.localScale.x + i * 2 * Hit.transform.localScale.x / points, Hit.GetComponent<Rigidbody2D>().mass * Hit.GetComponent<Rigidbody2D>().velocity.x / 10f / points * 2f);
            }
        }
    }*/
	/*void OnTriggerStay2D(Collider2D Hit)
	{
		//print(Hit.name);
		if (Hit.GetComponent<Rigidbody2D>() != null)
		{
			int points = Mathf.RoundToInt(Hit.transform.localScale.x * 15f);
			for (int i = 0; i < points; i++)
			{
				transform.parent.GetComponent<Water>().Splash(Hit.transform.position.x - Hit.transform.localScale.x + i * 2 * Hit.transform.localScale.x / points, Hit.GetComponent<Rigidbody2D>().mass * Hit.GetComponent<Rigidbody2D>().velocity.x / 10f / points * 2f);
			}
		}
	}*/

}
