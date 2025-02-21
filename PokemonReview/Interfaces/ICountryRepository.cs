using PokemonReview.Models;

namespace PokemonReview.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();
        Country GetCountry(int id);
        Country GetCountryByOwner(int id);
        ICollection<Owner> GetOwnersFromACountry(int countryId);
        bool CountryExists(int id);
        bool CreateCountry(Country country);

        bool UpdateCountry(Country country);
        bool Save();

    }
}
