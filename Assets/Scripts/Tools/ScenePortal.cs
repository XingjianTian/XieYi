using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePortal : MonoBehaviour
{
    public bool ifAudioFade = false;
    public AudioSource BGM;
    public bool ifshowed = false;
    public bool ifseeall = false;
    public Vector3 PointScene1;
    public Vector3 PointScene2;
    public Vector3 PointScene3;
    public string NextScene;
    public bool ifmoved = false;
    public bool IfDecideDirection = false;
    public GameObject player;
    //public Color originalColor;
    public bool ifopen = false;
    public AudioClip[] dooropenvoice;
    public GameObject came;
    public Camera Came;
    public CameraBlack cb;
    public CameraMoveWithPlayer cmwp;
    public float curColor;
    // Use this for initialization
    void Start()
    {
        if(GameObject.Find("BGM_Audio")!=null)
            BGM = GameObject.Find("BGM_Audio").GetComponent<AudioSource>();
        Came = came.GetComponent<Camera>();
        PointScene1 = new Vector3(11.81f, -3.19f, -3f);
        PointScene2 = new Vector3(0.78f, -0.96f, -3f);
        PointScene3 = new Vector3(-1f, 1.3f, -3f);
        cb = came.GetComponent<CameraBlack>();
        cmwp = came.GetComponent<CameraMoveWithPlayer>();
        //originalColor = Came.backgroundColor;
        curColor = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (ifopen)
        {
            ifAudioFade = true;
            if (name == "SceneDoor1")
            {
                if (!ifseeall)
                {
                    if (!ifshowed)
                    {
                        cmwp.changeToSolidPoint(PointScene1, true);

                        if (Came.orthographicSize <= 11.5f)
                        {
                            Came.orthographicSize += Time.deltaTime * 7;
                        }
                        came.transform.rotation = new Quaternion(0, 0, 0, 0);
                        if (Came.orthographicSize >= 11.5f)
                            ifshowed = true;
                    }

                    if (cmwp.ifMoveWithPlayer)
                    {
                        if (Came.orthographicSize >= 1.5f)
                        {
                            Came.orthographicSize -= Time.deltaTime * 7;
                        }
                        came.transform.rotation = new Quaternion(0, 0, 180, 0);
                        if (Came.orthographicSize <= 1.5f)
                            ifseeall = true;
                    }
                }
                if (ifseeall)
                {
                    GetComponent<Animator>().enabled = true;
                    if (curColor >= -0.9f)
                    {
                        curColor -= Time.deltaTime / 3;
                        if(BGM!=null)
                            BGM.volume -= Time.deltaTime / 4;
                        cb.ma.SetFloat("_Float1", curColor);
                    }
                    Invoke("ChangeScene", 2f);
                }
            }
            else if(name=="SceneDoorPre")
            {
                GetComponent<Animator>().enabled = true;
                if (curColor >= -0.9f)
                {
                    curColor -= Time.deltaTime / 3;
                    if(BGM!=null)
                        BGM.volume -= Time.deltaTime / 4;
                    cb.ma.SetFloat("_Float1", curColor);
                }
                Invoke("ChangeScene", 2f);
            }
            else if (name == "SceneDoor2")
            {
                if (!ifseeall)
                {
                    if (!ifshowed)
                    {
                        cmwp.changeToSolidPoint(PointScene2, true);

                        if (Came.orthographicSize <= 7f)
                        {
                            Came.orthographicSize += Time.deltaTime * 7;
                        }
                        //came.transform.rotation = new Quaternion(0, 0, 0, 0);
                        if (Came.orthographicSize >= 7f)
                            ifshowed = true;
                    }

                    if (cmwp.ifMoveWithPlayer)
                    {
                        if (Came.orthographicSize >= 1.5f)
                        {
                            Came.orthographicSize -= Time.deltaTime * 7;
                        }
                        //came.transform.rotation = new Quaternion(0, 0, 180, 0);
                        if (Came.orthographicSize <= 1.5f)
                            ifseeall = true;
                    }
                }
                if (ifseeall)
                {
                    GetComponent<Animator>().enabled = true;
                    if (curColor >= -0.9f)
                    {
                        curColor -= Time.deltaTime / 3;
                        if (BGM != null)
                            BGM.volume -= Time.deltaTime / 4;
                        cb.ma.SetFloat("_Float1", curColor);
                    }
                    Invoke("ChangeScene", 2f);
                }
            }
            else if (name == "SceneDoor3")
            {
                if (!ifseeall)
                {
                    if (!ifshowed)
                    {
                        cmwp.changeToSolidPoint(PointScene3, true);

                        if (Came.orthographicSize <= 7f)
                        {
                            Came.orthographicSize += Time.deltaTime * 7;
                        }
                        //came.transform.rotation = new Quaternion(0, 0, 0, 0);
                        if (Came.orthographicSize >= 7f)
                            ifshowed = true;
                    }

                    if (cmwp.ifMoveWithPlayer)
                    {
                        if (Came.orthographicSize >= 1.5f)
                        {
                            Came.orthographicSize -= Time.deltaTime * 7;
                        }
                        //came.transform.rotation = new Quaternion(0, 0, 180, 0);
                        if (Came.orthographicSize <= 1.5f)
                            ifseeall = true;
                    }
                }
                if (ifseeall)
                {
                    GetComponent<Animator>().enabled = true;
                    if (curColor >= -0.9f)
                    {
                        curColor -= Time.deltaTime / 3;
                        if (BGM != null)
                            BGM.volume -= Time.deltaTime / 4;
                        cb.ma.SetFloat("_Float1", curColor);
                    }
                    StartCoroutine(NowFinished());
                    Invoke("ChangeScene", 2f);
                }
            }
        }
    }
    IEnumerator NowFinished()
    {
        string dirpath = Application.persistentDataPath + "/Save";
        string filename = dirpath + "/GameData.txt";
        StreamWriter streamWriter = File.CreateText(filename);
        streamWriter.Write("4");
        streamWriter.Close();
        yield return 0;
    }
    void ChangeScene()
    {
        if (curColor <= 0f)
        {
            curColor += Time.deltaTime / 4;
            cb.ma.SetFloat("_Float1", curColor);
        }
        if (!ifmoved)
        {
            SceneManager.LoadScene(NextScene);
            ifmoved = true;
            Physics2D.gravity = new Vector3(0, -9.81f,0);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ifopen = true;
        }
    }
}
