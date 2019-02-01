using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    byte score;

    public Image[] scoreboard;
    bool resize, fade;

    private void Start()
    {
        item_pickup.pickup += UpdateScore;
        item_pickup.final_item += Fade;
        score = 0;
    }

    void UpdateScore()
    {
        resize = true;
    }

    void Fade()
    {
		GameObject hud = GameObject.Find("UI_HealthBar");
		if (hud != null) hud.SetActive(false);
		GameWin();
    }

    private void Update()
    {
        if(resize)
        {
            scoreboard[score].transform.localScale += transform.localScale / 10;
            if(scoreboard[score].transform.localScale.x >= 2)
            {
                resize = false;
                score++;
            }
        }
    }

	private void GameWin()
	{
		PlayerController.gameOver = true;
		Animator logoAnim = GameObject.Find("Logo").GetComponent<Animator>();
		logoAnim.SetTrigger("GameWin");
	}
}
