using api.Models;

namespace api.Services.interfaces;

public interface IWorkspaceService
{

    public Task<bool> CreateWorkspace(string workSpaceName);


}
