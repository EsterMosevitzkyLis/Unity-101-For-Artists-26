using UnityEngine;

public class info : MonoBehaviour
{

    public AudioSource audio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void playButton()
    {
        audio.Play();
    }
}
