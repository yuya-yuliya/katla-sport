using AutoMapper;
using KatlaSport.Services.ProductManagement;
using DataAccessStoreItem = KatlaSport.DataAccess.ProductStore.StoreItem;
using DataAccessStoreItemRequest = KatlaSport.DataAccess.ProductStore.ProductStoreItemRequest;

namespace KatlaSport.Services.StoreManagment
{
    public class StoreManagmentMappingProfile : Profile
    {
        public StoreManagmentMappingProfile()
        {
            CreateMap<DataAccessStoreItem, ProductStoreItem>();
            CreateMap<DataAccessStoreItemRequest, ProductStoreItemRequest>();

            CreateMap<UpdateRequestRequest, DataAccessStoreItemRequest>();
        }
    }
}
