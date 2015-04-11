using UnityEngine;
using System.Collections;

public class CameraHandler : MonoBehaviour 
{
    private bool changing = false;
    private float changeTo = 0;
    private float speed = 0;
    private Camera myCam;
    private Animation myAnimation;

    void Start()
    {
        myAnimation = GetComponent<Animation>();
        myCam = GetComponent<Camera>();
    }
    public void ChangeSizeTo(float changeTo, float speed)
    {
        changing = true;
        this.speed = speed;
        this.changeTo = changeTo;
    }
    public void PlayDamage()
    {
        myAnimation.Play("CamGetHit");
        Hashtable wrd1 = new Hashtable();
        wrd1.Add("amount", new Vector3(0f, 1f, 0) * 0.5f);
        wrd1.Add("isLocal", true);
        wrd1.Add("time", 0.2f);
        iTween.ShakePosition(Camera.main.gameObject, wrd1);
    }
    void Update()
    {
        if(changing)
        {
            if (myCam.orthographicSize - changeTo >= 0.2f)
                myCam.orthographicSize = Mathf.Lerp(myCam.orthographicSize, changeTo, Time.deltaTime * speed);
            else
                changing = false;
        }
    }
}
