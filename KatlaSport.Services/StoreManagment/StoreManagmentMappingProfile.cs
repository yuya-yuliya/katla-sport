using AutoMapper;
using KatlaSport.Services.ProductManagement;
using DataAccessStoreItem = KatlaSport.DataAccess.ProductStore.StoreItem;

namespace KatlaSport.Services.StoreManagment
{
    public class StoreManagmentMappingProfile : Profile
    {
        public StoreManagmentMappingProfile()
        {
            CreateMap<DataAccessStoreItem, ProductStoreItem>();
        }
    }
}
