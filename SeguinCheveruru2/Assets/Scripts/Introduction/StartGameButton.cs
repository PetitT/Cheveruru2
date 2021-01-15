using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    public string sceneName;
    public Animator anim;
    public StringValue chosenScene;
    public string difficulty;

    public List<GameObject> buttons;


    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<SoundFader>().FadeOut();
        buttons.ForEach(t => t.GetComponent<BoxCollider2D>().enabled = false);
        anim.SetTrigger("FadeOut");
        chosenScene.Value = difficulty;
        yield return new WaitForSeconds(2);
        SceneManager.LoadSceneAsync(sceneName);
    }
}
