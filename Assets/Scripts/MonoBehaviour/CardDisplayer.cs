using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CardDisplayer : MonoBehaviour
{

    [SerializeField] private CardStats baseCard;
    // Start is called before the first frame update
    [SerializeField] private TMP_Text cardNameContainer;
    [SerializeField] private TMP_Text staminaCostContainer;
    [SerializeField] private TMP_Text[] effectsContainer;
    [SerializeField] private TMP_Text abilitieScoreContainer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}