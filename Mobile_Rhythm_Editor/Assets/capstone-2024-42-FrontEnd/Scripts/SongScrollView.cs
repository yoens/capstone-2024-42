using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SongScrollView : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform contentPanel;
    public RectTransform sampleListItem;

    public VerticalLayoutGroup VLG;

    bool isSnapped;

    public float snapForce;
    float snapSpeed;

    public Sprite[] album;
    public Image main_image;

    void Start()
    {
        //isSnapped = false;
    }

    void Update()
    {
        int currentItem = Mathf.RoundToInt(contentPanel.localPosition.y / (sampleListItem.rect.height + VLG.spacing));

        if (scrollRect.velocity.magnitude < 100/* && !isSnapped*/)
        {
            /*scrollRect.velocity = Vector2.zero;
            snapSpeed += snapForce * Time.deltaTime;
            contentPanel.localPosition = new Vector3(
                contentPanel.localPosition.x,
                Mathf.MoveTowards(contentPanel.localPosition.y, 0 - (currentItem * (sampleListItem.rect.height + VLG.spacing)), snapSpeed),
                contentPanel.localPosition.z);
            NameLabel.text = ItemNames[currentItem];
            if (contentPanel.localPosition.y == 0 - (currentItem * (sampleListItem.rect.height + VLG.spacing)))
            {
                isSnapped = true;
            }*/
            contentPanel.localPosition = new Vector3(contentPanel.localPosition.x, (currentItem * (sampleListItem.rect.height + VLG.spacing)), contentPanel.localPosition.z);
            main_image.sprite = album[Song.user_song[currentItem]];
        }

        /*if(scrollRect.velocity.magnitude > 200)
        {
            isSnapped = false;
            snapSpeed = 0;
        }*/
    }
}
//0 - (currentItem * (sampleListItem.rect.height + VLG.spacing))