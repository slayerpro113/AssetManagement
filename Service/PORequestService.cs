using System;
using Data.Entities;
using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;
using Data.Utilities.Enumeration;

namespace Service
{
    public class PORequestService : BaseService<PORequest>, IPORequest
    {

        public PORequestService(IUnitOfWork unitOfWork, IRepository<PORequest> poRequestRepository) : base(unitOfWork, poRequestRepository)
        {
        }

        public Enumerations.AddEntityStatus HandlePoRequest(int productId, int employeeId, string description)
        {
            try
            {
                var poRequest = new PORequest();
                if (!String.IsNullOrEmpty(description))
                {
                    poRequest.Description = description;
                }
                poRequest.ProductID = productId;
                poRequest.EmployeeID = employeeId;
                poRequest.RequestStatusID = 1;
                DateTime today = DateTime.Now.Date;
                poRequest.CreatedDate = today;

                AddEntity(poRequest);

                return Enumerations.AddEntityStatus.Success;
            }
            catch(Exception e)
            {
                return Enumerations.AddEntityStatus.Failed;
            }
            
        }
    }
}