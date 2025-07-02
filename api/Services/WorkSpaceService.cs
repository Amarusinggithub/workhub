using api.Models;
using api.Repository.interfaces;
using api.Services.interfaces;

namespace api.Services;

public class WorkSpaceService : IWorkSpaceService

{

    private readonly ILogger<UserService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public WorkSpaceService(ILogger<UserService> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }


    public async Task<bool> CreateWorkspace(string workSpaceName)
    {
        WorkSpace workSpace = new WorkSpace { WorkSpaceName = workSpaceName };

    return  await _unitOfWork.WorkSpaces.Add(workSpace);



    }
}
