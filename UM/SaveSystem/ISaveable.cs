
namespace UM.SaveSystem
{
    /// <summary>
    /// Interface for all saveable ScriptableObjects
    /// </summary>
    public interface ISaveable
    {
        string VariableName { get; }
        void Save();
        void Load();
    }
}
