using UnityEngine;
namespace UM.SaveSystem
{
    [CreateAssetMenu(menuName = "UM/Saveable/Bool")]
    public class Bool : SaveableSO<bool>
    {

        public override void Save()
        {
            PlayerPrefs.SetInt(VariableName, Value ? 1 : 0);
            PlayerPrefs.Save();
        }

        public override void Load()
        {
            if (PlayerPrefs.HasKey(VariableName))
                Value = PlayerPrefs.GetInt(VariableName) == 1;
            else
                Value = defaultValue;
        }
    }
}