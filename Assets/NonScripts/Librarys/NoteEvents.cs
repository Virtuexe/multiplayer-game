using System;

public interface DeckEvents
{
    void CardAdded();
    void CardInserted(int index);
    void CardRemoved(int index);
}
