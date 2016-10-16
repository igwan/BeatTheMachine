using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIBordersController : MonoBehaviour
{
    [SerializeField] Image[] borders;
    [SerializeField] Color inToleranceColor;
    [SerializeField] Color outToleranceColor;


    void Start()
    {
        ChangeColor(outToleranceColor);
        SoundManager.Instance.musicTempo.AddBeginEvent( () => ChangeColor(inToleranceColor));
        SoundManager.Instance.musicTempo.AddEndEvent( () => ChangeColor(outToleranceColor));
    }

    public void ChangeColor(Color color)
    {
        for(int i = 0; i < borders.Length; i++)
            borders[i].color = color;
    }
}
