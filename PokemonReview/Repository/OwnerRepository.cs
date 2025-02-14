using PokemonReview.Data;
using PokemonReview.Interfaces;
using PokemonReview.Models;

namespace PokemonReview.Repository
{
    
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;
        public OwnerRepository(DataContext context)
        {
            _context = context;
        }
        public Owner GetOwner(int ownerId)
        {
            return _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnerOfAPokemon(int PokeId)
        {
            return _context.PokemonOwners.Where(p => p.Pokemon.Id == PokeId).Select(o => o.Owner).ToList();
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.ToList();
        }

        public ICollection<Pokemon> GetPokemonByOwner(int OwnerId)
        {
            return _context.PokemonOwners.Where(p => p.Owner.Id == OwnerId).Select(p => p.Pokemon).ToList();
        }

        public bool OwnerExist(int ownerId)
        {
            return _context.Owners.Any(o => o.Id == ownerId);
        }
    }
}
