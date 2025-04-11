using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameMenager : MonoBehaviour
{
    [SerializeField]
    GameObject card;

    [SerializeField]
    Vector3[] cards;

    [SerializeField]
    Texture2D[] images;

    [SerializeField]
    float startX;

    [SerializeField]
    float startY;

    [SerializeField]
    float planeZ;

    [SerializeField]
    float deltaX;

    [SerializeField]
    float deltaY;

    [SerializeField]
    int columns = 5;

    [SerializeField]
    int rows = 6;

    [SerializeField]
    GameObject winUI;

    [SerializeField] 
    AudioSource matchedSound;

    [SerializeField] 
    AudioSource wrongSound;

    int pairs;

    InteractiveCard selectedCard1;
    InteractiveCard selectedCard2;

    private void Start()
    {

        if (rows * columns != images.Length * 2)
        {
            Debug.LogWarning("number of r*c in not equal to provided cards, quit...");
            return;
        }

        pairs = columns * rows / 2;

        System.Random random = new System.Random();
        images = images.OrderBy(x => random.Next()).ToArray();

        Camera cam = Camera.main;

        cards = new Vector3[rows * columns];

        Vector3 currentPos = new Vector3(startX, startY, planeZ);

        int counter = 0;

        for (int i = 0; i < cards.Length; i++)
        {
            cards[counter++] = currentPos;

            currentPos.x += deltaX;

            
            Vector3 viewport = cam.WorldToViewportPoint(currentPos);
            if (viewport.x > 1f)
            {
                currentPos.x = startX;
                currentPos.y -= deltaY;
            }
        }

        cards = cards.OrderBy(x => random.Next()).ToArray();

        //Start creating (instantiate) cards, setting images etc
        counter = 0;

        int row = 0;

        foreach (Vector3 pos in cards)
        {
            GameObject go = Instantiate(card);

            go.SetActive(true);
            go.transform.position = pos;

            //Texture
            go.GetComponent<MeshRenderer>().material.SetTexture("_MainTexture", images[row]);

            //Called by InteractiveCards
            go.GetComponent<InteractiveCard>().OnClicked += SelectedCard;

            //Interactive card cover image
            go.GetComponent<InteractiveCard>().imageName = images[row].name;

            counter++;

            //Check if end of row
            if (counter % 2 == 0)
            {
                row++;
            }

        }
    }

    private void SelectedCard(InteractiveCard card, bool selected)
    {
        if (selectedCard1 == null && selected)
        {
            selectedCard1 = card;
        }
        else if (selectedCard1 == card && !selected)
        {
            selectedCard1.ResetMe();
            selectedCard1 = null;
        }
        else if (selectedCard2 != null && card == selectedCard2 && !selected)
        {
            selectedCard2.ResetMe();
            selectedCard2 = null;
        }
        else if ( selectedCard2 == null && card != selectedCard1 && selected)
        {
            selectedCard2 = card;

            if (selectedCard1.Compare(selectedCard2))
            {
                //OK match

                matchedSound.GetComponent<AudioSource>().Play();

                selectedCard1.HideAndDestroy();
                selectedCard2.HideAndDestroy();

                selectedCard1 = null ;
                selectedCard2 = null ;

                pairs--;

                if (pairs == 0)
                {
                    //Game over

                    winUI.SetActive(true);
                    winUI.GetComponent<AudioSource>().Play();
                }
            }
            else
            {
                //Flip back

                wrongSound.GetComponent<AudioSource>().Play();

                selectedCard1.ResetMe();
                selectedCard2.ResetMe();

                selectedCard1 = null;
                selectedCard2 = null;
            }
        }
    }
   
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
