using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLineDrawer : MonoBehaviour {

    public LineRenderer m_LineRenderer;
    public Camera m_Camera;
    protected List<Vector3> m_Points;
    private float distanceTraveled = 0f;
    private float RepeatRatio = 0f;
    void Awake () {
        m_LineRenderer = GetComponent<LineRenderer>();
        m_Camera = Camera.main;
        m_Points = new List<Vector3> ();
        RepeatRatio = m_LineRenderer.material.GetFloat ("_RepeatCount") / 5f;
    }

    void Update () {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Reset();
        //}
        //if (Input.GetMouseButton(0))
        //{
        //    Vector3 mousePosition = m_Camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_Camera.transform.position.z + 10f));
        //    BrushMove(mousePosition);
        //}
    }
    public void BrushMove (Vector3 pos) {
        m_Points.Add (pos);
        if (m_LineRenderer.positionCount > 1f) {
            distanceTraveled += Vector3.Distance (m_LineRenderer.GetPosition (m_LineRenderer.positionCount - 1), pos) * 0.5f;
            m_LineRenderer.material.SetFloat ("_RepeatCount", distanceTraveled * RepeatRatio);
        }

        bool shouldfollowStroke = false;
        if (shouldfollowStroke) {
            if (m_LineRenderer.positionCount > 100f) {
                m_Points.RemoveAt (0);
                Vector3[] newPositions = new Vector3[m_Points.Count];
                distanceTraveled = 0f;
                for (int i = 0; i < m_Points.Count; i++) {
                    newPositions[i] = m_Points[i];
                    if (i > 0) {
                        distanceTraveled += Vector3.Distance (newPositions[i], newPositions[i - 1]) * 0.5f;
                    }
                }
                m_LineRenderer.SetPositions (newPositions);
            } else {
                m_LineRenderer.positionCount = m_Points.Count;
                m_LineRenderer.SetPosition (m_LineRenderer.positionCount - 1, pos);
            }
        } else {
            m_LineRenderer.positionCount = m_Points.Count;
            m_LineRenderer.SetPosition (m_LineRenderer.positionCount - 1, pos);

        }

    }
    protected virtual void Reset () {
        distanceTraveled = 0f;
        if (m_LineRenderer != null) {
            m_LineRenderer.positionCount = 0;
        }
        if (m_Points != null) {
            m_Points.Clear ();
        }
    }

}