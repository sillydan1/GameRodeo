using UnityEngine;
using System.Collections;

public class MusicFade : MonoBehaviour 
{
    public bool doThings = true;
    public bool noIntro = false;
    public float minusStuff = 0.02f;
    private float fadeTimer = 0;
    public AudioClip intro;
    public AudioClip loop;
    private AudioSource looper;
    private bool fade = false;

    void Start()
    {
        if (doThings)
        {
            if (noIntro == false)
            {
                GetComponent<AudioSource>().clip = intro;
                GetComponent<AudioSource>().loop = false;
                GetComponent<AudioSource>().volume = 1;
                GetComponent<AudioSource>().Play();
                //instantiate the looping track as a new gameObject in order to "preload" it and not get an odd reoccuring load-glich.
                GameObject looperObj = new GameObject(loop.name + "_Music-player");
                looperObj.transform.position = Vector3.zero;
                looper = looperObj.AddComponent<AudioSource>();
                looper.clip = loop;
                looper.volume = 1;
                looper.pitch = 1;
                looper.loop = true;
                looper.PlayDelayed(intro.length - minusStuff);
            }
            else
            {
                GetComponent<AudioSource>().clip = loop;
            }
            //Debug.Log(intro.length);
        }
    }
    void Update()
    {
        if (looper != null)
        {
            if (fade)
            {
                if (noIntro)
                {
                    GetComponent<AudioSource>().volume = Mathf.Lerp(GetComponent<AudioSource>().volume, 0, fadeTimer);
                }
                else
                {
                    looper.volume = Mathf.Lerp(GetComponent<AudioSource>().volume, 0, fadeTimer);
                }
                fadeTimer += Time.deltaTime * 2;
            }
        }
    }
    public void ChangeVolume(float newVol)
    {
        looper.volume = newVol;
    }
    public void FadeOutMusic()
    {
        fadeTimer = 0;
        fade = true;
    }
}
