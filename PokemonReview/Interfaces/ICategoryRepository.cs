using PokemonReview.Models;

namespace PokemonReview.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        Category GetCategory(string name);
        ICollection<Pokemon> GetPokemonByCategory(int categoryId);
        bool CategoryExists(int id);
        bool CreateCategory(Category category);

        bool UpdateCategory(Category category);
        bool Save();
    }
}
