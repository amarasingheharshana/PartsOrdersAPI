using PartsOrdersAPI.Models;

namespace PartsOrdersAPI.Interfaces
{
    public interface IPartsService
    {
        IEnumerable<Part> GetAllParts();
        Part AddPart(Part part);
        Part GetPartById(int id);
    }

}
