using UnityEngine;


namespace UM.SaveSystem
{
    [CreateAssetMenu(menuName = "UM/Saveable/String")]
    public class StringSo : SaveableSO<string>
    {

        public override void Save()
        {
            PlayerPrefs.SetString(VariableName, Value);
            PlayerPrefs.Save();
        }

        public override void Load()
        {
            if (PlayerPrefs.HasKey(VariableName))
                Value = PlayerPrefs.GetString(VariableName);
            else
                Value = defaultValue;
        }
    }
}