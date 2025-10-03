using TMPro;
using UM.EventsSystem;
using UM.SaveSystem;
using UnityEngine;

public class SavingSystem : MonoBehaviour
{
    [SerializeField] private Bool isRunning;
    [SerializeField] private Int levelNo;
    [SerializeField] private StringSo names;
    [SerializeField] private UMEvent umRun,umlevel,umname;
    [SerializeField] private UMStringEvent IntEvent;
    
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI boolText;

    private void Start()
    {
        updatevales();
        
       
    }

    public void IsRunning()
    {
        isRunning.Value = !isRunning.Value;
        isRunning.Load();
        updatevales();
        umRun.Raise();
    }
    public void LevelNo()
    {
          levelNo.Value++;
        updatevales();
        umlevel.Raise();
        IntEvent.Raise(levelNo.Value.ToString());
    }
    public void Name()
    {
        names.Value=nameText.text + levelNo.Value;
        updatevales();
        umname.Raise();
    }

    private void updatevales()
    {
        levelText.text = levelNo.Value.ToString();
        boolText.text = isRunning.Value.ToString();
        nameText.text = names.Value;
    }
}
