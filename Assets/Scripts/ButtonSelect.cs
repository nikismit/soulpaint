using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{

    [SerializeField]
    private Transform buttonParent;

    [SerializeField]
    private Sprite unselectedSprite;
    [SerializeField]
    private Sprite selectedSprite;

    private int selectedButton = 0;

    private void Start()
    {
        if (buttonParent.GetComponentInChildren<Button>())
        {
            buttonParent.GetComponentsInChildren<Button>()[0].image.sprite = selectedSprite;
        }
        else
        {
            Debug.Log("Parent contains no buttons");
        }
    }

    public void SelectFirst()
    {
        if (buttonParent.GetComponentInChildren<Button>())
        {
            buttonParent.GetComponentsInChildren<Button>()[0].image.sprite = selectedSprite;
        }
        else
        {
            Debug.Log("Parent contains no buttons");
        }
    }

    [ContextMenu("Next Button")]
    public void NextButton()
    {
        if (buttonParent.GetComponentInChildren<Button>())
        {
            buttonParent.GetComponentsInChildren<Button>()[selectedButton].image.sprite = unselectedSprite;

            if (selectedButton >= buttonParent.GetComponentsInChildren<Button>().Length - 1)
            {
                selectedButton = 0;
            }
            else
            {
                selectedButton++;
            }

            buttonParent.GetComponentsInChildren<Button>()[selectedButton].image.sprite = selectedSprite;
        }
        else
        {
            Debug.Log("Parent contains no buttons");
        }
    }

    [ContextMenu("Previous Button")]
    public void PreviousButton()
    {
        if (buttonParent.GetComponentInChildren<Button>())
        {
            buttonParent.GetComponentsInChildren<Button>()[selectedButton].image.sprite = unselectedSprite;

            if (selectedButton <= 0)
            {
                selectedButton = buttonParent.GetComponentsInChildren<Button>().Length -1;
            }
            else
            {
                selectedButton--;
            }

            buttonParent.GetComponentsInChildren<Button>()[selectedButton].image.sprite = selectedSprite;
        }
        else
        {
            Debug.Log("Parent contains no buttons");
        }
    }

    [ContextMenu("Push Button")]
    public void ClickSelectedButton()
    {
        if (buttonParent.GetComponentInChildren<Button>())
        {
            buttonParent.GetComponentsInChildren<Button>()[selectedButton].onClick.Invoke();
            //Debug.Log("I pressed the button and the click selected button is called");
        }
        else
        {
            Debug.Log("Parent contains no buttons");
        }
    }
}
