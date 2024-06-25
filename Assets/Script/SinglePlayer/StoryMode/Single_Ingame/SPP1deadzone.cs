using UnityEngine;
using UnityEngine.SceneManagement;

public class SPP1Deadzone : MonoBehaviour
{
    public bool isExpand;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "P1ball")
        {
            BallController ball = collision.GetComponent<BallController>();
            if (!ball.hasExpanded)
                SceneManager.LoadScene("Fail");
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
                //case "SPFastenF(Clone)":
                //    Fasten_Skill skill3 = collision.GetComponent<Fasten_Skill>();
                //    this.isExpand = skill3.hasExpanded;
                //    break;
                //case "SPForceF(Clone)":
                //    Force_Skill skill4 = collision.GetComponent<Force_Skill>();
                //    this.isExpand = skill4.hasExpanded;
                //    break;
                case "SPInvincibleF(Clone)":
                    Invincible_Skill skill5 = collision.GetComponent<Invincible_Skill>();
                    this.isExpand = skill5.hasExpanded;
                    break;
            }
            if (isExpand == false)
            {
                SceneManager.LoadScene("Fail");
            }
        }
    }
}