using PokemonReview.Models;

namespace PokemonReview.Interfaces
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners();
        Owner GetOwner(int ownerId);
        ICollection<Owner> GetOwnerOfAPokemon(int PokeId);
        ICollection<Pokemon> GetPokemonByOwner(int OwnerId);
        bool OwnerExist(int ownerId);
        bool CreateOwner(Owner owner);
        bool UpdateOwner(Owner owner);
        bool Save();
    }
}
