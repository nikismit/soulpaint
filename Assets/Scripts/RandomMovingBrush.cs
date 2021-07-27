using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class RandomMovingBrush : MonoBehaviour {
    public float speed = 2f;
    private float progress_Position = 0f;
    private Vector3 Position_Last = Vector3.zero;
    private Vector3 Position_First = Vector3.zero;
    private float distanceTraveled = 0f;
    public List<GameObject> clonedObjects;
    private SimpleLineDrawer current_SimpleLineDrawer;
    private MeshLineRenderer meshDrawer;


    private VRTK_ControllerEvents leftControllerAlias = null;
    private VRTK_ControllerEvents rightControllerAlias = null;
    public Material materialToPaint;
    float left, right;
    [SerializeField]
    bool newBrush;

    // Start is called before the first frame update
    void Start () {
        Invoke("SetControllerReferences", 1f);
        //  Position_First = Position_Last = this.transform.position;
        //GameObject go = Instantiate (clonedObjects[Random.Range (0, clonedObjects.Count)]);
        // go.transform.position = Vector3.zero;
        //current_SimpleLineDrawer = go.AddComponent<SimpleLineDrawer> ();
    }
    void SetControllerReferences()
    {
        rightControllerAlias = VRTK_DeviceFinder.GetControllerRightHand().GetComponent<VRTK_ControllerEvents>();
    //    leftControllerAlias = VRTK_DeviceFinder.GetControllerLeftHand().GetComponent<VRTK_ControllerEvents>();
    }
    // Update is called once per frame
    void Update () {
        if (rightControllerAlias != null)
        {
            right = rightControllerAlias.GetTriggerAxis();
        }
        // left = leftControllerAlias.GetTriggerAxis();
        if (right >= .5f )
        {
           // float fastTime = Time.time * speed;
          //  Vector3 randomPos = Position_First + new Vector3(progress_Position, 0f, 0f) + new Vector3(
          //      Mathf.Cos(Mathf.PingPong(fastTime * 3f, 9.9f)) * 4.9f,
             //   Mathf.Sin(Mathf.PingPong(fastTime * 3f, 7.1f)) * 3.6f,
             //   Mathf.Sin(Mathf.PingPong(fastTime * 3f, 12.1f)) * 7.6f);
            //this.transform.position = randomPos;
            // distanceTraveled += Vector3.Distance (this.transform.position, Position_Last) * 0.5f;
            Position_Last = rightControllerAlias.transform.position ;
            //if (current_SimpleLineDrawer != null)

            // current_SimpleLineDrawer.BrushMove(Position_Last);
            if (meshDrawer != null)
            {
                meshDrawer.AddPoint(Position_Last, right);
            }

            if (newBrush)
            {
                //  if (current_SimpleLineDrawer == null) return;
                //  DestroyImmediate (current_SimpleLineDrawer.transform.gameObject);
                // progress_Position += 1f;
                //  GameObject go = Instantiate(clonedObjects[Random.Range(0, clonedObjects.Count)]);

                GameObject go = new GameObject();
                go.transform.position = Position_Last;
                go.AddComponent<MeshFilter>();

                go.AddComponent<MeshRenderer>();
                meshDrawer = go.AddComponent<MeshLineRenderer>();
               // current_SimpleLineDrawer = go.AddComponent<SimpleLineDrawer>();

                newBrush = false;
            }
        }
       
    }
}