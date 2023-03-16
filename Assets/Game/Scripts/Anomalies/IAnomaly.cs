using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnomaly
{
    bool VerifyAnomaly(string anomalyGuess, string roomGuess);

    void ActivateAnomaly();

    void DeactivateAnomaly();

    bool IsAnomalyActive();
}
