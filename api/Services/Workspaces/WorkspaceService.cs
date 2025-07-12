using api.Data.interfaces;
using api.Models;
using api.Services.interfaces;
using api.Services.Users;

namespace api.Services.Workspaces;

public class WorkspaceService : IWorkspaceService

{

    private readonly ILogger<UserService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public WorkspaceService(ILogger<UserService> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }


    public async Task<bool> CreateWorkspace(string workSpaceName)
    {
        Workspace workspace = new Workspace { WorkSpaceName = workSpaceName };

    return  await _unitOfWork.Workspaces.Add(workspace);



    }
}
