public interface ICharacterAbstractFactory
{
    ICharacterFactory GetFactory(CharacterPrefab config);
}
