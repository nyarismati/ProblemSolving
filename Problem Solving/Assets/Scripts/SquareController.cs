using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SquareController : MonoBehaviour
{
    public UnityAction<SquareController> OnSquareDestroyed = delegate { };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (ScoreController.Instance == null) return;

            ScoreController.Instance.IncreaseScore(1);
            OnSquareDestroyed(this);
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
}
