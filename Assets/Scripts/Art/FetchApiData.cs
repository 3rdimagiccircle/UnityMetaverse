using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class FetchApiData : MonoBehaviour
{
    public string url = "https://engine-v2.3rdilab.com/api/elements";
    string yourAPIKey = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiI5MjQ0YTc4Yi0yZDZkLTRlM2QtYTY3Yy04MzQyYTExNmZhMDciLCJqdGkiOiI3NWE1NWUxZWZiMzA5ZGE0NDY3MmEyMzgxZTU2ODFmYzBkNzVmNjBhMmY5Y2Y5NDM1MDVhZTBiYTA5Mjk0MmI4MTZlNWU2M2RlNzAzOTg5YSIsImlhdCI6MTY2ODQyMzYwMC42Mjk3NTUsIm5iZiI6MTY2ODQyMzYwMC42Mjk3NTcsImV4cCI6MTY5OTk1OTYwMC42MDM3MDIsInN1YiI6IjExMCIsInNjb3BlcyI6W119.JVd2quZytsuQaf17f-__T5u33X1KBP1us4GvQWq7L_xMqx2zprO61dD4PiWgv4fNPh_mxX60me9KbJpH5NmMa3Zuklw-hp9S2sIGP4LNFHbAicnxAhSbRcVaZWJAFczH-H1alirhJyrC2n4MgJqcAynX2-AdLuOXKqFIa7utIl601Ckw-UUEp7nWw2TuND82t1H7z6myyvW4npIJf-fqXKe5AwiuIuFNoLN0kI1oI1Sgselzp0fiTesM1FM02xKIVZB5SYt1dfyjPHeF2dMr5Owg_mmitPlzU54S0AxS4g81Bqb6JLPgQhti0dx063uQMALSnIespQEGyifdU7FeNhpOm3w-UykINhbubHmPnl1A7GK15EVV91JkXzeWphVFSdNvhhGWSw66Rwu0tBSDa9sga4L29605wX0mKkfPlP5HM3VT2-enaDz5UC9vz0Z2quB82lnQVr_z7U8IAcOw7UX6bg3HkRosKgU9Jg-dX_R1nhfSFnmznB8tXvirhwCTZEWrPRNU5gww9VY-kE2eni_2zOp0yRsItTnudfiJYqgapmIZk4sTuoYfMGuM3TKE6dp5PKTQfQS_B9CS4B5CvCOotM8pOIIFV9_L96Vo9wfxb1wH0GQhNzArKiQS1_NQeqRU8EFTQPbiQwUQCb7ys5LMQf3O2I50ozpZkCMScTc";
    string accessVal = "application/json";



    public GameObject artObject, artParent;
    public List<Transform> SpawnPoints;

    List<arListing.Datas> activeData = new List<arListing.Datas>();
    //float xScale=15, yScale=15, zScale=0.2f;

    GameObject progressBar;

    private void OnEnable()
    {
      
        Debug.Log("OnEnable");
       
    
        GetData();
    }

    public void GetData()
    {
        StartCoroutine(FetchData());
    }

    public IEnumerator FetchData()
    {
        Debug.Log("FetchData");
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url + "?token=" + yourAPIKey))
        {
            webRequest.SetRequestHeader("Authorization", yourAPIKey);
            webRequest.SetRequestHeader("Accept", accessVal);


            yield return webRequest.SendWebRequest();

            //while (!webRequest.isDone)
            //{
            //    Debug.Log("progress bar--" + webRequest.downloadProgress);
            //    yield return null;
            //   // progressBar.value = filewww.downloadProgress;

            //}


            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
               //Debug.Log("progress--"+webRequest.)
                string jsonResult = webRequest.downloadHandler.text;
                Debug.Log(jsonResult);

                arListing.Root dataRootClass = JsonUtility.FromJson<arListing.Root>(jsonResult.ToString());

                Debug.Log("There are " + dataRootClass.data.Count + " art objects in the list.");
                for (int i = 0; i < dataRootClass.data.Count; i++)
                {
                    //Debug.Log("active count val------" + dataRootClass.data[i].active);
                    if (dataRootClass.data[i].active)
                    {
       
                        activeData.Add(dataRootClass.data[i]);
                        //Debug.Log("all active--" + activeData.Count);
                       
                    }
                }
                if (activeData != null && activeData.Count > 0)
                {
                    //Debug.Log("activeData.Count is--" + activeData.Count);
                    for (int val = 0; val < activeData.Count; val++)
                    {
                        
                        GameObject allArtObjects = Instantiate(artObject.gameObject, SpawnPoints[val].transform.position, Quaternion.identity, SpawnPoints[val]);
                        //allArtObjects.transform.parent = artParent.transform;

                        allArtObjects.transform.localScale = SpawnPoints[val].transform.localScale;

                        allArtObjects.transform.eulerAngles = SpawnPoints[val].transform.eulerAngles;

                        //Debug.Log("all art objects--" + allArtObjects.name);
                        // Debug.Log("text name---" + allArtObjects.transform.GetChild(0).GetChild(0).name);


                        allArtObjects.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = activeData[val].translations.en.name;

                      //  Debug.Log("identifier is--" + activeData[val].identifiers[0].media_url);
//                        Debug.Log("id is--" + activeData[val].id);
                        allArtObjects.name = activeData[val].id.ToString();
                        StartCoroutine(SetImage(activeData[val].identifiers[0].media_url, allArtObjects, allArtObjects.transform.GetChild(allArtObjects.transform.childCount - 1).GetChild(0).gameObject));
                    }
                }

            }
        }
    }



    IEnumerator SetImage(string url, GameObject allArtObjects,GameObject progressBar)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        www.SendWebRequest();

        while (!www.isDone)
        {
           // Debug.Log("progress bar--" + www.downloadProgress);
            yield return null;
          //  progressBar.GetComponent<Image>().fillAmount = www.downloadProgress;
            
        }

        //if (progressBar.GetComponent<Image>().fillAmount >= 0.99f)
        //{
            progressBar.gameObject.SetActive(false);
       // }
        Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        allArtObjects.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", myTexture);

    }

    //IEnumerator setImage(string url, GameObject allArtObjects)
    //{
    //    Debug.Log("media url------" + url);

    //    WWW www = new WWW(url);
    //    //while (!www.isDone)
    //    //{
    //      yield return www;

       
    //    //    Debug.Log("Loading...." + (www.progress * 100) + " %");
          
    //    //}
    //    //if (string.IsNullOrEmpty(www.error))
    //    //{
    //    //    //Success
    //    //}
    //    //else
    //    //{
    //        allArtObjects.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", www.texture);
    //    //}
    //    //Texture2D texture = www.texture;
    //    //end show Image in texture 2D
    //   // 
    //}

   
}


