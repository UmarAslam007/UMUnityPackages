using UnityEngine;

namespace UM.SaveSystem
{
    [CreateAssetMenu(menuName = "UM/Saveable/Int")]
    public class Int : SaveableSO<int>
    {
        public override void Save()
        {
            PlayerPrefs.SetInt(VariableName, Value);
            PlayerPrefs.Save();
        }

        public override void Load()
        {
            if (PlayerPrefs.HasKey(VariableName))
                Value = PlayerPrefs.GetInt(VariableName);
            else
                Value = defaultValue;
        }
    }
}
