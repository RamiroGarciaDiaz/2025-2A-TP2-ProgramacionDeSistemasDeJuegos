public class CharacterAbstractFactory : ICharacterAbstractFactory
{
    private readonly ICharacterFactory _defaultFactory;
    public CharacterAbstractFactory(ICharacterFactory defaultFactory)
    {
        _defaultFactory = defaultFactory;
    }

    public ICharacterFactory GetFactory(CharacterPrefab config)
        => _defaultFactory;
}
