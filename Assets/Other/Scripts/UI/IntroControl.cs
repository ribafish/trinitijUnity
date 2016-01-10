using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroControl : MonoBehaviour {

    //javno vidne spremenljivke (default nastavitve)
    public Text production = null;     //UI za skupino
	public Transform header = null;         //UI za naslov
	public Transform text = null;           //UI za spremno besedilo
    public AudioSource introSound = null;   //spremni zvok
    public float soundBeginHeader = 14.0f;  //kdaj pricne zvok naslova
    public float soundBeginText = 35.0f;    //kdaj pricne zvok spremnega besedila
	public float speed = 30.0f;             //hitrost besedila

    //ostale spremenljivke (default nastavitve)
    Vector3 cameraPosition = Vector3.zero;
    float cameraFarPlane = 0.0f;

    Text headerText = null;
    Text textText = null;
    float textSize = 0.0f;
    int introState = 2;

    float speedNormal = 0;
    float speedFaster = 0;
    
	void Start () 
    {
        //izvedemo samo ce imamo vse objekete
        if (production != null && header != null && text != null && introSound != null)
        {
            introState = 1; //pricni v stanju 1

            Camera mainCamera = Camera.main;                //nalozimo main kamero
            cameraPosition = mainCamera.transform.position; //shranimo pozicijo kamere
            cameraFarPlane = mainCamera.farClipPlane;       //shranimo vidno razdaljo kamere

            headerText = header.GetComponent<Text>();   //Text opcija headerja
            textText = text.GetComponent<Text>();       //Text opcija texta
            textSize = text.GetComponent<RectTransform>().rect.height / 2;    //shranimo zazeljeno velikost

            production.color = new Color(1,1,1,0);

            speedNormal = speed;
            speedFaster = speed * 2;
		}
	}
	   
	void FixedUpdate () 
    {
        if (introState == 1) 
        {
            if (introSound.time < soundBeginHeader)
            {
                Color lowerAlpha = new Color(0,0,0,0.17f) * Time.deltaTime;

                if (introSound.time < (soundBeginHeader / 2))
                    production.color = production.color + lowerAlpha;
                else
                    production.color = production.color - lowerAlpha;
            }
            if (introSound.time > soundBeginHeader)
            {
                if (header.position.z <= (cameraFarPlane + 10))
                {
                    header.position += header.forward * speed * Time.deltaTime;

                    if (header.position.z > (cameraFarPlane - 300) && headerText.color.a > 0.0f)
                    {
                        Color lowerAlpha = new Color(0,0,0,0.1f) * Time.deltaTime;
                        headerText.color = headerText.color - lowerAlpha;
                    }
                }
            }
            if (introSound.time > soundBeginText)
            {
                float textDistance = Vector3.Distance(cameraPosition, text.position);

                if (textDistance <= (cameraFarPlane + textSize + 10))
                {
                    text.position += text.up * speed * Time.deltaTime;
                    textDistance = Vector3.Distance(cameraPosition, text.position);

                    if (textDistance > (cameraFarPlane + textSize - 500) && textText.color.a > 0.0f)
                    {
                        Color lowerAlpha = new Color(0, 0, 0, 0.05f) * Time.deltaTime;
                        textText.color = textText.color - lowerAlpha;
                        introSound.volume = introSound.volume - (0.05f * Time.deltaTime);
                    }
                }
                else
                {
                    introState = 2;
                }
            }
		}
        else if (introState == 2)
        {
            if (introSound.volume > 0.0f)
                introSound.volume = introSound.volume - (0.1f * Time.deltaTime);
            else
                introState = 0;

            //ce je pritisnjen ESC
            Color lowerAlpha = new Color(0, 0, 0, 0.1f) * Time.deltaTime;

            if (production.color.a > 0.0f)
                production.color = production.color - lowerAlpha;
            if (headerText.color.a > 0.0f)
                headerText.color = headerText.color - lowerAlpha;
            if (textText.color.a > 0.0f)
                textText.color = textText.color - lowerAlpha;
        }
        else
        {
            Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene.buildIndex + 1);
        }
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (introSound.time < 4)
                introState = 3;
            else
                introState = 2;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            speed = speedFaster;
            introSound.pitch = 2;
        }
        else
        {
            speed = speedNormal;
            introSound.pitch = 1;
        }
    }
}
