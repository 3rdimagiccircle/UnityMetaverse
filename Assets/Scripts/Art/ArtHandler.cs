using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;

public class ArtHandler : MonoBehaviour
{
    string url = "https://engine-v2.3rdilab.com/api/elements/";
    string yourAPIKey = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiI5MjQ0YTc4Yi0yZDZkLTRlM2QtYTY3Yy04MzQyYTExNmZhMDciLCJqdGkiOiI3NWE1NWUxZWZiMzA5ZGE0NDY3MmEyMzgxZTU2ODFmYzBkNzVmNjBhMmY5Y2Y5NDM1MDVhZTBiYTA5Mjk0MmI4MTZlNWU2M2RlNzAzOTg5YSIsImlhdCI6MTY2ODQyMzYwMC42Mjk3NTUsIm5iZiI6MTY2ODQyMzYwMC42Mjk3NTcsImV4cCI6MTY5OTk1OTYwMC42MDM3MDIsInN1YiI6IjExMCIsInNjb3BlcyI6W119.JVd2quZytsuQaf17f-__T5u33X1KBP1us4GvQWq7L_xMqx2zprO61dD4PiWgv4fNPh_mxX60me9KbJpH5NmMa3Zuklw-hp9S2sIGP4LNFHbAicnxAhSbRcVaZWJAFczH-H1alirhJyrC2n4MgJqcAynX2-AdLuOXKqFIa7utIl601Ckw-UUEp7nWw2TuND82t1H7z6myyvW4npIJf-fqXKe5AwiuIuFNoLN0kI1oI1Sgselzp0fiTesM1FM02xKIVZB5SYt1dfyjPHeF2dMr5Owg_mmitPlzU54S0AxS4g81Bqb6JLPgQhti0dx063uQMALSnIespQEGyifdU7FeNhpOm3w-UykINhbubHmPnl1A7GK15EVV91JkXzeWphVFSdNvhhGWSw66Rwu0tBSDa9sga4L29605wX0mKkfPlP5HM3VT2-enaDz5UC9vz0Z2quB82lnQVr_z7U8IAcOw7UX6bg3HkRosKgU9Jg-dX_R1nhfSFnmznB8tXvirhwCTZEWrPRNU5gww9VY-kE2eni_2zOp0yRsItTnudfiJYqgapmIZk4sTuoYfMGuM3TKE6dp5PKTQfQS_B9CS4B5CvCOotM8pOIIFV9_L96Vo9wfxb1wH0GQhNzArKiQS1_NQeqRU8EFTQPbiQwUQCb7ys5LMQf3O2I50ozpZkCMScTc";
    string accessVal = "application/json";
    [SerializeField]
    private GameObject CanvasMenu, menuPanel;
    [SerializeField]
    private GameObject[] menuItems;

    [SerializeField]
    private Sprite[] playPauseSprites;

    int videoPlayerIndex;

   
    //float currentValue=5;
    public float speed=5;
    //string id;

    //   List<arListingWithResourceID.Datum> arListAudio = new List<arListingWithResourceID.Datum>();
    List<arListingWithResourceID.Datum> arListvideo = new List<arListingWithResourceID.Datum>();
    List<arListingWithResourceID.Datum> arListAudio = new List<arListingWithResourceID.Datum>();
    //List<arListingWithResourceID.Datum> arListGif = new List<arListingWithResourceID.Datum>();

    string videoUrl = "";
    string jsonResult;

    //bool isLoaderActive = false;

   

    private void OnEnable()
    {
      //  GameObject imgCircleOb = GameObject.Find("loadingImage");
       // imgCircle = imgCircleOb.GetComponent<Image>();
        //menuIcon = GameObject.Find("MenuBtn");
        //print(menuIcon);
        //menuPanel = GameObject.Find("menuPanel");
    }

    //bool isCalled = false;
    public void OnArtMouseDown()
    {
        //if (!isCalled)
        //{
            string artId = gameObject.name;
           // isCalled = true;
            // isLoaderActive = true;
            GetData(artId);
           
       // }
       
    }

    public void GetDataWithResource(string id)
    {
        StartCoroutine(FetchDataWithResource(id));
    }

    public IEnumerator FetchDataWithResource(string id)
    {
        ///api/elements/{id}/resources
       // Debug.Log("FetchData--" + id);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url + id + "/resources"))
        {
            webRequest.SetRequestHeader("Authorization", yourAPIKey);
            webRequest.SetRequestHeader("Accept", accessVal);
           
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                string jsonResourceResult = webRequest.downloadHandler.text;
                Debug.Log(jsonResourceResult);
                arListingWithResourceID.Root dataRootClass = JsonUtility.FromJson<arListingWithResourceID.Root>(jsonResourceResult.ToString());


                for (int i = 0; i < dataRootClass.data.Count; i++)
                {
                   // Debug.Log("type--" + dataRootClass.data[i].files.en.type);
                    if (dataRootClass.data[i].files.en.type == "mp4")
                    {
                     //   Debug.Log("video type--" + dataRootClass.data[i].files.en.type);
                        arListvideo.Add(dataRootClass.data[i]);
                       

                    }
                    if (dataRootClass.data[i].files.en.type == "mpeg")
                    {
                      //  Debug.Log("audio type--" + dataRootClass.data[i].supplement_files.en.type);
                        arListAudio.Add(dataRootClass.data[i]);


                    }

                }

                if (arListvideo != null && arListvideo.Count > 0)
                {
                   
                    for (int val = 0; val < arListvideo.Count; val++)
                    {
                        Debug.Log("media url is--" + arListvideo[val].files.en.media_url);
                        videoUrl = arListvideo[val].files.en.media_url;
                    }
                }

                if (videoUrl == "")
                {
                    menuPanel.transform.GetChild(5).gameObject.GetComponent<Button>().interactable = false;
                }
            }
        }
    }
    
    public void GetData(string id)
    {
        StartCoroutine(FetchData(id));
    }
    public IEnumerator FetchData(string id)
    {
        Debug.Log("FetchData--" + id);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url+id))
        {
            webRequest.SetRequestHeader("Authorization", yourAPIKey);
            webRequest.SetRequestHeader("Accept", accessVal);


            yield return webRequest.SendWebRequest();



            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                string jsonResult = webRequest.downloadHandler.text;
//                Debug.Log(jsonResult);

                dataRootClass = JsonUtility.FromJson<arListingWithID.Root>(jsonResult.ToString());

                GetDataWithResource(id);
                //Debug.Log(dataRootClass.data.translations.en.description);
                //Debug.Log(dataRootClass.message.description);
                CanvasMenu.SetActive(true);

                StartCoroutine(setImage(dataRootClass.data.identifiers[0].media_url, menuPanel.transform.GetChild(0).GetComponent<RawImage>()));

            }
        }
    }

    arListingWithID.Root dataRootClass;
    bool isMenuClicked = false;
    public void ToggleMenuButton()
    {
        Debug.Log(isMenuClicked);
        if (!isMenuClicked)
        {
            isMenuClicked = true;
            Debug.Log("if");
            menuPanel.SetActive(true);
            
        }

        else
        {
            Debug.Log("else");
            menuPanel.SetActive(false);
            isMenuClicked = false;
        }
    }


    //bool isVideoPlay = false, isVideoPause = false;
    public void TogglePlayPauseVideo(int toggleIndex)
    {

        if (menuItems[videoPlayerIndex].transform.GetChild(3).GetComponent<Image>().sprite == playPauseSprites[1])
        {
            Debug.Log("if");
            menuItems[videoPlayerIndex].transform.GetChild(3).GetComponent<Image>().sprite = playPauseSprites[0];
            menuItems[videoPlayerIndex].transform.GetChild(2).GetComponent<VideoPlayer>().Pause();

        }

        // }

        else if (menuItems[videoPlayerIndex].transform.GetChild(3).GetComponent<Image>().sprite == playPauseSprites[0])
        {
            Debug.Log("else");
            menuItems[videoPlayerIndex].transform.GetChild(3).GetComponent<Image>().sprite = playPauseSprites[1];
            menuItems[videoPlayerIndex].transform.GetChild(2).GetComponent<VideoPlayer>().Play();
            //  isVideoPlay = true;
        }
    }

  
    public void OnclickMenuBtnItems(int index)
    {
        foreach (GameObject allChild in menuItems)
            allChild.SetActive(false);
            switch (index)
            {
                case 1:
                menuItems[0].SetActive(true);
                    Debug.Log(dataRootClass.data.translations.en.description);
                    menuItems[0].transform.GetChild(0).GetChild(2).GetComponent<Text>().text = dataRootClass.data.translations.en.description;
               
                    break;

                case 4:
                menuItems[1].SetActive(true);
                StartCoroutine(setImage(dataRootClass.data.identifiers[0].media_url, menuItems[1].transform.GetChild(0).GetChild(2).GetComponent<RawImage>()));
                   
                    break;

                case 5:
                menuItems[2].SetActive(true);
                menuItems[2].transform.GetChild(2).GetComponent<VideoPlayer>().url = videoUrl;
                    videoPlayerIndex = 2;
                 
                    break;
            case 6:
               
                break;
            default:

                    break;
            }
    }

    public void clickToCloseMainMenu()
    {
        foreach (GameObject allChild in menuItems)
            allChild.SetActive(false);
        CanvasMenu.SetActive(false);
    }

    //IEnumerator setImage(string url, RawImage mediaImage)
    //{
    //    Debug.Log("media url------" + url);

    //    WWW www = new WWW(url);
    //    yield return www;

    //    while(!www.isDone)
    //    {
    //        Debug.Log("Loading...." + (www.progress * 100) + " %");
    //    }
    //    //Texture2D texture = www.texture;
    //    //end show Image in texture 2D

    //    mediaImage.texture = www.texture;
    //}


    IEnumerator setImage(string url, RawImage mediaImage)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();


        Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        mediaImage.texture = myTexture;

    }

}
