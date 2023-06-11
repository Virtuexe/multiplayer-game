using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckScript : DeckEvents
{
    public GameObject thisGameObject;
    int _deck;
    public override int deck
    {
        get
        {
            return _deck;
        }
        set
        {
            _deck = value;
        }
    }
    public override void CardAdded(int card)
    {
        Debug.Log("card added");
        Destroy(thisGameObject);
        thisGameObject = Instantiate(Card.prefab);
        thisGameObject.GetComponent<SpriteRenderer>().sprite = GameManagerScript.Instance.board.cards[card].sprite;
        thisGameObject.transform.position = this.transform.position;
        thisGameObject.transform.rotation = this.transform.rotation;
    }
    public override void CardInserted(int index, int card)
    {

    }
    public override void CardRemoved(int index)
    {

    }
}
