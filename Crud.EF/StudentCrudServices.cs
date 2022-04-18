using Crud.domain.Model;
using Crud.domain.Services;
using Crud.EF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.EF
{
    public class StudentCrudServices : ICrud
    {
        private readonly IDataService<Student> _crudServices;

        public StudentCrudServices()
        {
            _crudServices = new GenericDataService<Student>(new StudentContextFactory());
        }

        public async Task<Student> AddBrand(string stname, string course)
        {
            try
            {   
                if (stname == string.Empty)
                {
                    throw new Exception("Student Name Cannot be Empty");
                }
                else
                {
                    Student br = new Student
                    {
                        stname = stname,
                        course = course
                    };
                    return await _crudServices.Create(br);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteBrand(int id)
        {
            try
            {
                Student delete = await SearchBrandbyID(id);

                return await _crudServices.Delete(delete);



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ICollection<Student>> ListBrands()
        {
            try
            {
                return (ICollection<Student>)await _crudServices.GetAll();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public Task<Student> SearchBrandbyID(int ID)
        {
            try
            {
                return _crudServices.Get(ID);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ICollection<Student>> SearchBrandByName(string stname)
        {
            try
            {
                var listbrand = await ListBrands();
                return listbrand.Where(x => x.stname.StartsWith(stname)).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public async Task<Student> UpdateBrand(int id, string stname, string course)
        {
            try
            {
                Student br = await SearchBrandbyID(id);
                br.stname = stname;
                return await _crudServices.Update(br);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }
    }
}
