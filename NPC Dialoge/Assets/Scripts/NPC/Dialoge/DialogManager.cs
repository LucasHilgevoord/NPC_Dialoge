using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    public Text nameText;
    public Text dialogText;
    public Animator anim;
    [SerializeField]
    private Font Textfont;
    [SerializeField]
    private GameObject continueButton;
    [SerializeField]
    private GameObject yesButton;
    [SerializeField]
    private GameObject noButton;
    private bool HasChoice;

    private Queue<string> sentences;

	// Use this for initialization
	void Start () {
        sentences = new Queue<string>();
        nameText.font = Textfont;
        dialogText.font = Textfont;
    }
	

    public void StartDialogue (Dialogue dialogue) {
        anim.SetBool("isOpen", true);
        HasChoice = dialogue.hasChoice;

        nameText.text = dialogue.npcName;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
        if (HasChoice) {
            noButton.SetActive(true);
            yesButton.SetActive(true);
            continueButton.SetActive(false);
        }
        
    }

    public void AcceptMission() {
        noButton.SetActive(false);
        yesButton.SetActive(false);
        continueButton.SetActive(true);
        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if(sentences.Count == 0) {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence) {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            dialogText.text += letter;
            yield return null;
        }
    }
	

    public void EndDialogue() {
        anim.SetBool("isOpen", false);
        Debug.Log("End of conversation");
    }
}
