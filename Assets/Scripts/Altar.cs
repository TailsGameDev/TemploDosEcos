using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : Door
{
    [SerializeField]
    private Transform redPosition = null;
    [SerializeField]
    private Transform greenPosition = null;
    [SerializeField]
    private Transform bluePosition = null;

    private bool redWasPlaced;
    private bool greenWasPlaced;
    private bool blueWasPlaced;

    public override bool TryKey(Key key)
    {
        bool succeed = key.DoorName == "red" || key.DoorName == "blue" || key.DoorName == "green";

        switch (key.DoorName)
        {
            case "red":
                key.transform.position = redPosition.position;
                redWasPlaced = true;
                break;
            case "green":
                key.transform.position = greenPosition.position;
                greenWasPlaced = true;
                break;
            case "blue":
                key.transform.position = bluePosition.position;
                blueWasPlaced = true;
                break;
        }

        if (redWasPlaced && greenWasPlaced && blueWasPlaced)
        {
            Debug.LogError("PARABENS CABRA DA PESTE TU ZEROU O JOGO");
        }

        return succeed;
    }
}
