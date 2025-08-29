namespace api.Services.Workspaces.interfaces;

public interface IWorkspaceService
{

    public Task<bool> CreateWorkspace(string workSpaceName);


}
