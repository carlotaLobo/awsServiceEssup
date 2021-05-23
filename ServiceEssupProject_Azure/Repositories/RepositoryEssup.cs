using ServiceEssupProject_Azure.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using System.Text.RegularExpressions;
using ModelsEssup.Models;

#region PROCEDURES

//ALTER PROCEDURE UPDATEMATERIALOLD
//(@MAT NVARCHAR(30))
//AS
//    print('MATERIAL: '+ @MAT)

//    UPDATE MATERIALS SET ESTADO='OLD' WHERE MATERIAL=@MAT
//GO


//CREATE PROCEDURE PROVIDERSMATERIALSEARCH(@MATERIAL NVARCHAR(40))
//ALTER PROCEDURE[dbo].[PROVIDERSMATERIALSEARCH](@MATERIAL NVARCHAR(40))
//AS
//	SELECT * FROM PROVIDERSMATERIAL WHERE REPLACE(ID_MATERIAL, ' ', '')LIKE '%' + REPLACE(@MATERIAL, ' ', '') + '%'



//GO

//create procedure pointsprovider(@provider nvarchar(30), @media int out)
//as

//    select @media = avg(points) from points where id_provider = @provider


//go

//alter PROCEDURE LIKESBUSINESS
//(@IDBUSINESS NVARCHAR(30))
//AS
//SELECT * FROM providers  
//INNER JOIN likes 
//ON mail = id_provider
//where id_company =@IDBUSINESS
//GO

//alter PROCEDURE  REGISTROSLIKESPAGINACION
//        (@POSICION INT, @EMAIL nvarchar(30) , @REGISTROS INT OUT)
//        AS
//        SELECT @REGISTROS = COUNT(id_company) FROM Likes WHERE id_company = @EMAIL
//        SELECT * FROM
//		(SELECT ROW_NUMBER() OVER(ORDER BY id_company) AS POSICION, Likes.* FROM Likes
//        WHERE id_company = @EMAIL )AS CONSULTA
//        WHERE POSICION >= @POSICION AND POSICION<(@POSICION + 3)
//        GO

//create procedure FINDMATERIALBYSECTOR
//(@SECTOR NVARCHAR(40))
//AS

//    SELECT MATERIAL FROM sectorMaterials WHERE SECTOR = @SECTOR ORDER BY material ASC

//GO


#endregion
#region VISTAS

//CREATE VIEW PROVIDERSMATERIAL
//AS
//SELECT P.MAIL, P.POPULATION, pm.id_material FROM providers P
//INNER JOIN
//providersMaterials PM
//ON P.MAIL = PM.id_providers

//GO

//CREATE VIEW SECTORPROVIDERVIEW
//AS
//SELECT P.MAIL, P.POPULATION, ps.id_sector FROM providers P
//INNER JOIN
//sectorProvider Ps
//ON P.MAIL = Ps.id_provider
//GO

//SELECT MAIL, PWD, ROL
//FROM            providers
//UNION
//SELECT        MAIL, PWD, ROL
//FROM            business

#endregion
namespace ServiceEssupProject_Azure.Repositories
{
    public class RepositoryEssup : IRepositoryEssup
    {
        Context context;
        IMemoryCache MemoryCache;
        public RepositoryEssup(Context context, IMemoryCache MemoryCache)
        {
            this.context = context;
            this.MemoryCache = MemoryCache;
        }

        #region USUARIOS
        public async Task<Usuarios> Login(String email, String pwd)
        {
            try
            {
                Usuario usuario = (from dato in this.context.Usuario
                                   select dato).Where(p =>
                                  p.Mail == email && p.Pwd == pwd
                          ).FirstOrDefault();
                if (usuario != null)
                {
                    Usuarios usuarios = new Usuarios();

                    if (usuario.Rol == Rol.PROVEEDOR.ToString())
                    {
                        Provider provider = await this.context.Provider.FindAsync(usuario.Mail);
                        usuarios.Provider = provider;
                        return usuarios;
                    }
                    else if (usuario.Rol == Rol.EMPRESA.ToString())
                    {
                        Business business = await this.context.Business.FindAsync(usuario.Mail);
                        usuarios.Business = business;
                        return usuarios;
                    }

                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region BUSINESS
        public async Task<List<Business>> FindAllBussines()
        {
            try
            {

                return await this.context.Business.ToListAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }

        }
        public async Task<Business> FindBussinesById(string key)
        {
            try
            {
                return await this.context.Business.FindAsync(key);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }

        }
        public async Task<Business> InsertBusiness(String mail, String businessname, String address, String population, String cp, String sector,
            String telefono, String pwd)
        {
            try
            {
                Business busine = new Business();
                busine.Rol = Rol.EMPRESA.ToString();
                busine.Fecha = "Apr 16 2020  8:49PM";
                busine.Cp = cp;
                busine.Mail = mail;
                busine.Name = businessname;
                busine.Address = address;
                busine.Population = population;
                busine.Pwd = pwd;
                busine.Tlf = telefono;
                busine.Sector = sector;

                Business business = this.context.Business.Add(busine).Entity;

                await this.context.SaveChangesAsync();
                return business;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
                return null;
            }

        }
        public async Task<Business> UpdatetBusiness(String mail, String businessname, String address, String population, String cp, String sector,
            String telefono, String pwd)
        {
            try
            {
                Business business = await FindBussinesById(mail);
                business.Rol = Rol.EMPRESA.ToString();
                business.Fecha = business.Fecha;
                business.Name = businessname;
                business.Address = address;
                business.Population = population;
                business.Sector = sector;
                business.Pwd = pwd;
                business.Tlf = telefono;
                business.Cp = cp;

                this.context.Entry(business).CurrentValues.SetValues(business);

                await this.context.SaveChangesAsync();

                return business;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
                return null;
            }

        }
        #endregion

        #region PROVIDER

        public async Task<List<Provider>> FindAllProvider()
        {
            try
            {
                return await this.context.Provider.ToListAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }

        }
        public async Task<Provider> FindProviderById(string key)
        {
            try
            {
                return await this.context.Provider.FindAsync(key);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }

        }
        public async Task<Provider> InsertProvider(String mail, String businessname, String address, String population, String cp,
            String telefono, String pwd, String descripcion, String img1, String img2, String img3)
        {
            try
            {
                Provider pro = new Provider();
                pro.Rol = Rol.PROVEEDOR.ToString();
                pro.Date = "Apr 16 2020  8:49PM";
                pro.Mail = mail;
                pro.Name = businessname;
                pro.Address = address;
                pro.Population = population;
                pro.Cp = cp;
                pro.Tlf = telefono;
                pro.Pwd = pwd;
                pro.Description = descripcion;
                pro.Img1 = img1;
                pro.Img2 = img2;
                pro.Img3 = img3;

                Provider provider = this.context.Provider.Add(pro).Entity;
                await this.context.SaveChangesAsync();
                return provider;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }

        }
        public async Task<Provider> UpdatetProvider(String mail, String businessname, String address, String population, String cp,
            String telefono, String pwd, String descripcion, String img1, String img2, String img3)
        {
            try
            {
                Provider provider = await FindProviderById(mail);
                if (img1 == null) { img1 = provider.Img1; } else { provider.Img1 = img1; }
                if (img2 == null) { img2 = provider.Img2; } else { provider.Img2 = img2; }
                if (img3 == null) { img3 = provider.Img3; } else { provider.Img3 = img3; }
                provider.Rol = Rol.PROVEEDOR.ToString();
                provider.Date = provider.Date;
                provider.Name = businessname;
                provider.Address = address;
                provider.Population = population;
                provider.Cp = cp;
                provider.Tlf = telefono;
                provider.Pwd = pwd;
                provider.Description = descripcion;

                this.context.Entry(provider).CurrentValues.SetValues(provider);
                await this.context.SaveChangesAsync();

                return provider;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
                return null;
            }


        }
        #endregion

        #region MATERIALS
        public async Task<List<Materials>> FindNewMaterials()
        {
            return await this.context.Material.Where(m => m.Estado == "NEW").ToListAsync();
        }
        public async Task<Materials> FindMaterialById(string key)
        {
            try
            {
                return await this.context.Material.FindAsync(key);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }

        }
        public async Task<int> DeleteMaterialById(string key)
        {
            try
            {
                this.context.Material.Remove(await this.FindMaterialById(key));
                return await this.context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return 0;
            }

        }
        public async Task<List<Materials>> FindAllMaterials()
        {
            try
            {
                return await this.context.Material.OrderBy(o => o.Material).ToListAsync();

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }
        public async Task<Materials> InsertMaterials(String material)
        {
            try
            {
                List<Materials> m = await this.FindAllMaterials();
                foreach (Materials ma in m)
                {
                    if (ma.Material.ToLower() != material.ToLower())
                    {
                        Materials materials = this.context.Material.Add(new Materials(material, "NEW")).Entity;
                        await this.context.SaveChangesAsync();
                        return materials;
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }

        }
        public async Task<Materials> UpdatetMaterials(String material)
        {
            try
            {
                Materials materials = await this.FindMaterialById(material);
                materials.Material = material;
                await this.context.SaveChangesAsync();
                return materials;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }

        }
        #endregion

        #region SECTOR
        public async Task<List<Sector>> FindAllSector()
        {
            try
            {
                if (this.MemoryCache.Get("sectors") == null)
                {
                    this.MemoryCache.Set("sectors", await this.context.Sector.ToListAsync());
                }

                return this.MemoryCache.Get("sectors") as List<Sector>;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }

        }
        public async Task<Sector> FindSectorById(string key)
        {
            try
            {
                return await this.context.Sector.FindAsync(key);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }

        }
        public async Task<Sector> InsertSector(String sector)
        {
            try
            {
                Sector sectors = new Sector(sector);
                this.context.Add(sectors);
                await this.context.SaveChangesAsync();
                return sectors;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }

        }
        public async Task<Sector> UpdatetSector(String sector)
        {
            try
            {
                Sector sectors = await this.FindSectorById(sector);
                sectors.sector = sector;
                await this.context.SaveChangesAsync();
                return sectors;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }

        }
        #endregion

        #region PROVIDER MATERIAL

        public async Task<List<ProviderMaterial>> FindProviderMaterialByMaterial(String Key)
        {

            try
            {

                return await (from data in this.context.ProviderMaterial
                              where data.Id_Material.Replace(" ", "").Contains(Key.Replace(" ", ""))
                              select data).ToListAsync();

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
                return null;
            }


        }
        public async Task<int> DeleteProviderMaterialById(int key)
        {
            try
            {
                this.context.ProviderMaterial.Remove(await this.FindProviderMaterialById(key));
                return await this.context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
                return 0;
            }
        }
        public async Task<List<ProviderMaterial>> FindMaterialByProvider(string key)
        {
            try
            {
                return await (from data in this.context.ProviderMaterial where data.Id_Providers == key select data).ToListAsync();

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }
        public async Task<ProviderMaterial> FindProviderMaterialById(int key)
        {
            try
            {
                return await this.context.ProviderMaterial.FindAsync(key);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }
        public async Task<List<ProviderMaterial>> FindProviderMaterialByProvider(String key)
        {
            try
            {
                return await (from data in this.context.ProviderMaterial
                              where data.Id_Providers == key
                              select new ProviderMaterial
                              {
                                  Id = data.Id,
                                  Id_Material = data.Id_Material,
                                  Id_Providers = data.Id_Providers
                              }).ToListAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }

        }
        public async Task<ProviderMaterial> InsertProviderMaterial(String provider, String material)
        {
            try
            {
                ProviderMaterial providerMaterial = new ProviderMaterial();
                providerMaterial.Id = await this.LastIdProviderMaterial();
                providerMaterial.Id_Material = material;
                providerMaterial.Id_Providers = provider;
                this.context.ProviderMaterial.Add(providerMaterial);
                await this.context.SaveChangesAsync();
                return providerMaterial;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }

        }
        public async Task<int> LastIdProviderMaterial()
        {
            try
            {
                int id = (await (from data in this.context.ProviderMaterial select data.Id).MaxAsync() + 1);
                if (id == 0) { id = 1; }
                return id;

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return 0;
            }
        }
        public async Task<ProviderMaterial> UpdatetProviderMaterial(int id, String provider, String material)
        {
            try
            {
                ProviderMaterial providerMaterial = await this.FindProviderMaterialById(id);
                providerMaterial.Id_Material = material;
                providerMaterial.Id_Providers = provider;
                await this.context.SaveChangesAsync();
                return providerMaterial;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }

        }
        #endregion

        #region SECTOR MATERIAL
        //public async Task<List<SectorMaterial>> FindSectorMaterialBySectors(List<SectorProvider> sectors)
        //{
        //    return await (from dato in this.context.SectorMaterial
        //                  where sectors.AsEnumerable().Select(s => s.Id_Sector)
        //                  .Contains(dato.Sector)
        //                  select dato).ToListAsync();

        //}
        public async Task<List<SectorMaterial>> FindMaterialNewByMaterial(String key)
        {
            try
            {

                List<SectorMaterial> sm = await (from data in this.context.SectorMaterial
                                                 where data.Material == key
                                                 select data)
                    .OrderByDescending(m => m.Id).ToListAsync();

                return sm;

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }
        public async Task<List<SectorMaterial>> FindSectorMaterialBySectors(List<String> sectors)
        {
            return await (from dato in this.context.SectorMaterial
                          where sectors.Contains(dato.Sector)
                          select dato).ToListAsync();

        }
        public async Task<int> LastIdSectorMaterial()
        {
            try
            {
                int id = await (from data in this.context.SectorMaterial select data.Id).MaxAsync() + 1;
                if (id == 0) { id = 1; }
                return id;

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return 0;
            }
        }
        public async Task<int> DeleteSectorMaterialById(int key)
        {
            try
            {
                this.context.SectorMaterial.Remove(await this.FindSectorMaterialById(key));
                return await this.context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return 0;
            }

        }
        public async Task<List<SectorMaterial>> FindMaterialBySector(string key)
        {

            try
            {
                return await (from data in this.context.SectorMaterial where data.Sector == key select data)
                    .OrderByDescending(m => m.Id).ToListAsync();

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }
        public async Task<SectorMaterial> FindSectorMaterialById(int key)
        {
            try
            {
                return await this.context.SectorMaterial.FindAsync(key);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }
        public async Task<List<SectorMaterial>> FindSectorMaterialBySector(string key)
        {
            try
            {
                return await (from data in this.context.SectorMaterial
                              where data.Sector == key
                              select new SectorMaterial
                              {
                                  Id = data.Id,
                                  Material = data.Material,
                                  Sector = data.Sector
                              }).ToListAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }

        }
        public async Task<SectorMaterial> InsertSectorMaterial(String material, String sector)
        {
            try
            {
                List<SectorMaterial> sm = await this.FindMaterialBySector(sector);


                foreach (SectorMaterial smaterial in sm)
                {
                    if (smaterial.Material != material && smaterial.Sector == sector)
                    {
                        SectorMaterial sectorMaterial = new SectorMaterial();
                        sectorMaterial.Id = await this.LastIdSectorMaterial();
                        sectorMaterial.Sector = sector;
                        sectorMaterial.Material = material;
                        this.context.SectorMaterial.Add(sectorMaterial);
                        await this.context.SaveChangesAsync();

                        return sectorMaterial;
                    }
                }


                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }

        }
        public async Task<SectorMaterial> UpdatetSectorMaterial(int id, String material, String sector)
        {
            try
            {
                SectorMaterial sectorMaterial = await this.FindSectorMaterialById(id);
                sectorMaterial.Material = material;
                sectorMaterial.Sector = sector;

                await this.context.SaveChangesAsync();
                return sectorMaterial;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }

        }

        #endregion

        #region SECTOR PROVIDER
        public async Task<List<SectorProvider>> FindSectorProviderBySector(string Key)
        {
            return await ((from dato in this.context.SectorProvider select dato).Where(sp => sp.Id_Sector == Key)).ToListAsync();
        }
        public async Task<List<SectorProvider>> FindSectorByProvider(string key)
        {
            try
            {
                return await (from data in this.context.SectorProvider where data.Id_Provider == key select data).ToListAsync();

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }

        }
        public async Task<SectorProvider> FindSectorProviderById(int key)
        {
            try
            {
                return await this.context.SectorProvider.FindAsync(key);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }
        public async Task<int> DeleteSectorProviderById(int key)
        {
            try
            {
                this.context.SectorProvider.Remove(await this.FindSectorProviderById(key));
                return await this.context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return 0;
            }

        }
        public async Task<SectorProvider> InsertSectorProvider(String provider, String sector)
        {
            try
            {
                if (this.FindSectorByProvider(provider).Result.Where(s => s.Id_Sector == sector).FirstOrDefault() == null)
                {
                    SectorProvider sectorProvider = new SectorProvider();
                    sectorProvider.Id = await this.LastIdSectorProvider();
                    sectorProvider.Id_Provider = provider;
                    sectorProvider.Id_Sector = sector;

                    this.context.SectorProvider.Add(sectorProvider);

                    await this.context.SaveChangesAsync();
                    return sectorProvider;
                }
                else
                {
                    return null;
                }



            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }

        }
        public async Task<int> LastIdSectorProvider()
        {
            try
            {
                int id = await (from data in this.context.SectorProvider select data.Id).MaxAsync() + 1;
                if (id == 0) { id = 1; }
                return id;


            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return 0;
            }
        }
        public async Task<SectorProvider> UpdatetSectorProvider(int id, String provider, String sector)
        {
            try
            {
                if (this.FindSectorByProvider(provider).Result.Where(s => s.Id_Sector == sector).FirstOrDefault() == null)
                {
                    SectorProvider sectorProvider = await this.FindSectorProviderById(id);
                    sectorProvider.Id_Provider = provider;
                    sectorProvider.Id_Sector = sector;
                    await this.context.SaveChangesAsync();
                    return sectorProvider;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }

        }
        #endregion

        #region LIKES
        public List<Likes> LikesBusinessPaginacion(String email, int posicion, ref int registros)
        {

            registros = this.context.Like.Where(l => l.Id_Company == email).Count();
            var consulta = (this.context.Like.Where(l => l.Id_Company == email)).OrderBy(z => z.Id).Skip(posicion)
                .Take(3).ToList();

            List<Likes> likes = consulta;
            return likes;
        }
        public async Task<List<Provider>> FindLikesBusiness(string business)
        {
            //List<Provider> providers = await (from provider in this.context.Provider
            //                                  join like in this.context.Like on provider.Mail equals like.Id_Company
            //                                  where provider.Mail == emailprovider
            //                                  select provider).ToListAsync();

            List<Provider> providers = await (from provider in this.context.Provider
                                              join like in this.context.Like on provider.Mail equals like.Id_Provider
                                              where like.Id_Company == business
                                              select provider).ToListAsync();

            return providers;

        }
        public async Task<Likes> FindLikesByProviderAndBusiness(String business, String provider)
        {
            return await this.context.Like.Where(z => z.Id_Company == business && z.Id_Provider == provider).FirstOrDefaultAsync();
        }
        public async Task<int> MaxIdLike()
        {
            int id = await (from data in this.context.Like select data.Id).MaxAsync() + 1;
            if (id == 0) { id = 1; }
            return id;
        }
        public async Task<List<Likes>> FindLikesByBusiness(string key)
        {
            try
            {
                return await (from data in this.context.Like where data.Id_Company == key select data).ToListAsync();


            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }
        public async Task<Likes> InsertLikes(String provider, String business)
        {
            try
            {
                Likes like = new Likes(provider, business);
                like.Id = await this.MaxIdLike();
                this.context.Like.Add(like);
                await this.context.SaveChangesAsync();

                return like;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }

        }
        public async Task<Likes> FindLikeById(int id)
        {
            return await this.context.Like.FindAsync(id);
        }
        public async Task<int> DeleteLike(int id)
        {
            this.context.Remove(await this.FindLikeById(id));
            return await this.context.SaveChangesAsync();
        }
        public async Task<int> DeleteLike(string keybusiness, string keyprovider)
        {
            try
            {

                Likes like = await this.context.Like.Where(z => z.Id_Company == keybusiness && z.Id_Provider == keyprovider).FirstOrDefaultAsync();
                this.context.Like.Remove(like);
                return await this.context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return 0;
            }


        }
        public async Task<Likes> UpdatetLikes(int id, String provider, String business)
        {
            try
            {
                Likes likes = await this.FindLikeById(id);
                likes.Id_Provider = provider;
                likes.Id_Company = business;
                await this.context.SaveChangesAsync();
                return likes;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }

        }
        #endregion

        #region POINTS
        public async Task<List<Point>> Find0PointByBusiness(String business)
        {
            return await this.context.Point.Where(z => z.Id_Company == business && z.Points == 0).ToListAsync();
        }
        public async Task<int> FindPointByProvider(string key)
        {
            try
            {
                return (int)(await this.context.Point.Where(z => z.Id_Provider == key).Select(z => z.Points).AverageAsync());
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public async Task<int> MaxIdPoint()
        {
            int id = await (from data in this.context.Point select data.Id).MaxAsync() + 1;
            if (id == 0) { id = 1; }
            return id;
        }
        public async Task<Point> InsertPoint(String provider, String business)
        {
            try
            {
                Point points = new Point(provider, business);
                points.Id = await this.MaxIdPoint();
                this.context.Point.Add(points);

                await this.context.SaveChangesAsync();
                return points;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }

        }
        public async Task<Point> UpdatetPoint(int id, String provider, String business, int point)
        {
            try
            {
                Point points = await this.FindPointById(id);
                points.Id_Company = business;
                points.Id_Provider = provider;
                points.Points = point;
                await this.context.SaveChangesAsync();
                return points;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }

        }
        public async Task<Point> FindPointById(int id)
        {
            return await this.context.Point.FindAsync(id);
        }
        #endregion

        #region MATERIAL PROVIDER VIEW
        //cambiar metodo
        public async Task<List<ProvidersMaterialView>> FindProviderByMaterialView(String key)
        {

            return await (from data in this.context.providersMaterialViews
                          where data.IdMaterial.Replace(" ", "").Contains(key.Replace(" ", ""))
                          select data).ToListAsync();

        }
        #endregion

        #region SECTOR PROVIDER VIEW
        public async Task<List<SectorProviderView>> FindProviderBySectorView(string Key)
        {
            return await (from datos in this.context.SectorProviderViews where datos.Sector == Key select datos).ToListAsync();
        }

        public async Task<List<SectorProviderView>> FindSectorsProviderViewByProvider(string key)
        {
            return await this.context.SectorProviderViews.Where(z => z.Mail == key).ToListAsync();
        }
        #endregion







    }
}
