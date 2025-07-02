using api.Models;

namespace api.Services.interfaces;

public interface IWorkSpaceService
{

    public Task<bool> CreateWorkspace(string workSpaceName);


}
