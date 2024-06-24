using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChDeadzone : MonoBehaviour
{
    public bool isExpand;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "P1ball")
        {
            BallController ball = collision.GetComponent<BallController>();
            if (!ball.hasExpanded)
                SceneManager.LoadScene("ChallengeFail");
        }


        if (collision.gameObject.tag == "Item")
        {
            switch (collision.gameObject.name)
            {
                case "SPEndlessF(Clone)":
                    Endless_Skill endless_Skill = collision.GetComponent<Endless_Skill>();
                    this.isExpand = true;
                    break;
                case "SPBlackHoleF(Clone)":
                    BlackHole_Skill skill = collision.GetComponent<BlackHole_Skill>();
                    this.isExpand = skill.hasExpanded;
                    break;

                case "SPInvincibleF(Clone)":
                    Invincible_Skill skill5 = collision.GetComponent<Invincible_Skill>();
                    this.isExpand = skill5.hasExpanded;
                    break;
            }
            if (isExpand == false)
            {
                SceneManager.LoadScene("ChallengeFail");
            }
        }
    }
}