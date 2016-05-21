using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class BoardComponent : Combatant {
    public Button winButton;

    new public void Die() {

        winButton.gameObject.SetActive(true);

        //base.Die();
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
