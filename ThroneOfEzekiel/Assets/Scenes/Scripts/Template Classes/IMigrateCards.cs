using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMigrateCards
{
    public abstract void MigrateCardTo(Card card, CardList newCollection);
}
