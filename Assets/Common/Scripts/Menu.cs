using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Menu : MonoBehaviour
{
    public static Menu Inst { get; private set; } // Remove unnecessary '{' character from this line

    void Awake() => Inst = this;

    [SerializeField] GameObject menu;
    [SerializeField] GameObject Settings;
    [SerializeField] GameObject Home;
    private bool isShowing = false;
    private bool isMoving = false;

    void Start()
    {
        Settings.GetComponent<CircleCollider2D>().enabled = false;
        Home.GetComponent<CircleCollider2D>().enabled = false;
    }

    private void OnMouseDown()
    {
        if (!isMoving)
        {
            if (isShowing)
            {
                HideMenu();
            }
            else
            {
                ShowMenu();
            }
        }
    }

    public void ShowMenu()
    {
        isMoving = true;
        Setting.Inst.SettingPanel.SetActive(false);
        Settings.SetActive(true);
        Settings.transform.DOMove(menu.transform.position + new Vector3(0.3f, -1f, 0), 0.5f).OnComplete(() =>
        {
            Home.SetActive(true);
            Home.transform.position = Settings.transform.position;
            Home.transform.DOMove(menu.transform.position + new Vector3(1.1f, -0.3f, 0), 0.5f);
            isMoving = false;
        });
        isShowing = true;
        Settings.GetComponent<CircleCollider2D>().enabled = true;
        Home.GetComponent<CircleCollider2D>().enabled = true;
    }

    public void HideMenu()
    {
        isMoving = true;
        Home.transform.DOMove(menu.transform.position + new Vector3(0.3f, -1f, 0), 0.5f).OnComplete(() =>
        {
            Home.SetActive(false);
            Settings.transform.DOMove(menu.transform.position + new Vector3(0, 0, 0), 0.5f).OnComplete(() =>
            {
                Settings.SetActive(false);
                Home.transform.position = menu.transform.position + new Vector3(0, 0, 0);
                Settings.transform.position = menu.transform.position + new Vector3(0, 0, 0);
                isMoving = false;
            });
        });
        isShowing = false;
        Settings.GetComponent<CircleCollider2D>().enabled = false;
        Home.GetComponent<CircleCollider2D>().enabled = false;
    }
}
