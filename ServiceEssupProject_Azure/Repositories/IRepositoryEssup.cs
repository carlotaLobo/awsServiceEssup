
using ModelsEssup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceEssupProject_Azure.Repositories
{
    public interface IRepositoryEssup
    {
        Task<Usuarios> Login(String email, String pwd);

        #region LIKE
        Task<int> MaxIdLike();
        Task<Likes> FindLikeById(int id);
        List<Likes> LikesBusinessPaginacion(String email, int posicion, ref int registros);
        Task<Likes> FindLikesByProviderAndBusiness(String business, String provider);    
        Task<List<Likes>> FindLikesByBusiness(String key);    
        Task<int> DeleteLike(int id);
        Task<int> DeleteLike(String keybusiness, String keyprovider); 
        Task<List<Provider>> FindLikesBusiness(String business);
        Task<Likes> InsertLikes(String provider, String business);
        Task<Likes> UpdatetLikes(int id, String provider, String business);

        #endregion
        #region POINT        
        Task<Point> FindPointById(int id);
        Task<int> MaxIdPoint();
        Task<List<Point>> Find0PointByBusiness(String business);
        Task<int> FindPointByProvider(String key);
        Task<Point> InsertPoint(String provider, String business);
        Task<Point> UpdatetPoint(int id, String provider, String business, int point);
        #endregion
        #region BUSINESS
        Task<Business> FindBussinesById(String key);
        Task<List<Business>> FindAllBussines();
        Task<Business> InsertBusiness(String mail, String businessname, String address, String population, String cp, String sector,
        String telefono, String pwd);
        Task<Business> UpdatetBusiness(String mail, String businessname, String address, String population, String cp, String sector,
        String telefono, String pwd);
        #endregion
        #region PROVIDER
        Task<List<Provider>> FindAllProvider();        
        Task<Provider> FindProviderById(String key);
        Task<Provider> InsertProvider(String mail, String businessname, String address, String population, String cp, 
            String telefono, String pwd, String descripcion, String img1, String img2, String img3);        
        Task<Provider> UpdatetProvider(String mail, String businessname, String address, String population, String cp,
        String telefono, String pwd, String descripcion, String img1, String img2, String img3);
        #endregion
        #region MATERIAL
        Task<List<Materials>> FindNewMaterials();
        Task<Materials> FindMaterialById(String key);
        Task<List<Materials>> FindAllMaterials();
        Task<Materials> InsertMaterials(String material);
        Task<Materials> UpdatetMaterials(String material);
        Task<int> DeleteMaterialById(string key);
        #endregion
        #region PROVIDER MATERIAL
        Task<List<ProviderMaterial>> FindMaterialByProvider(String key);
        Task<ProviderMaterial> FindProviderMaterialById(int key);
        Task<List<ProviderMaterial>> FindProviderMaterialByMaterial(String Key);
        Task<List<ProviderMaterial>> FindProviderMaterialByProvider(String key);
        Task<int> DeleteProviderMaterialById(int key);
        Task<ProviderMaterial> InsertProviderMaterial(String provider, String material);
        Task<int> LastIdProviderMaterial();
        Task<ProviderMaterial> UpdatetProviderMaterial(int id, String provider, String material);

        #endregion
        #region SECTOR
        Task<Sector> FindSectorById(String key);
        Task<List<Sector>> FindAllSector();
        Task<Sector> InsertSector(String sector);
        Task<Sector> UpdatetSector(String sector);
        #endregion
        #region SECTOR MATERIAL
        Task<List<SectorMaterial>> FindSectorMaterialBySectors(List<String> sectors);
        Task<SectorMaterial> FindSectorMaterialById(int key);
        Task<List<SectorMaterial>> FindMaterialBySector(String key);
        Task<int> DeleteSectorMaterialById(int key);
        Task<List<SectorMaterial>> FindSectorMaterialBySector(String key);
        Task<List<SectorMaterial>> FindMaterialNewByMaterial(String key);
        Task<SectorMaterial> InsertSectorMaterial(String material, String sector);
        Task<SectorMaterial> UpdatetSectorMaterial(int id, String material, String sector);        
        Task<int> LastIdSectorMaterial();

        #endregion
        #region SECTOR PROVIDER        
        Task<List<SectorProvider>> FindSectorProviderBySector(String Key);        
        Task<SectorProvider> FindSectorProviderById(int key);
        Task<List<SectorProvider>> FindSectorByProvider(String key);
        Task<int> DeleteSectorProviderById(int key);
        Task<SectorProvider> InsertSectorProvider(String provider, String sector);
        Task<SectorProvider> UpdatetSectorProvider(int id, String provider, String sector);
        Task<int> LastIdSectorProvider();
        #endregion

        Task<List<ProvidersMaterialView>> FindProviderByMaterialView(String key);
        Task<List<SectorProviderView>> FindProviderBySectorView(String Key);
        Task<List<SectorProviderView>> FindSectorsProviderViewByProvider(string key);
    }
}
