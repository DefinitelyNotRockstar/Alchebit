using UnityEngine;
using System.Collections;

public class SortingScript : MonoBehaviour {


    private Renderer m_renderer;

    private void Awake()
    {
        m_renderer = GetComponent<Renderer>();
    }

    // Use this for initialization
    void Start () {
	
	}

    void LateUpdate()
    {

		if (m_renderer != null && m_renderer.isVisible){
			m_renderer.sortingOrder = (int)Camera.main.WorldToScreenPoint(m_renderer.bounds.min).y * -1;
        }

    }


}
