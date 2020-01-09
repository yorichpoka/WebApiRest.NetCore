using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Interfaces.Bussiness.MongoDB;
using WebApiRest.NetCore.Domain.Interfaces.Repositories.MongoDB;
using WebApiRest.NetCore.Domain.Models.MongoDB;

namespace WebApiRest.NetCore.Bussiness.MySql
{
    public class GradeBussinessImpl : IGradeBussiness
    {
        private readonly IGradeRepository _GradeRepository;

        public GradeBussinessImpl(IGradeRepository repository)
        {
            this._GradeRepository = repository;
        }

        public Task<GradeModel> Create(GradeModel obj)
        {
            return
                this._GradeRepository.Create(obj);
        }

        public Task Delete(string grade_id)
        {
            return
                this._GradeRepository.Delete(grade_id);
        }

        public Task Delete(string[] grade_ids)
        {
            return
                this._GradeRepository.Delete(grade_ids);
        }

        public Task<GradeModel> Read(string id)
        {
            return
                this._GradeRepository.Read(id);
        }

        public Task<IEnumerable<GradeModel>> Read(int top)
        {
            return
                this._GradeRepository.Read(top);
        }

        public Task<IEnumerable<GradeModel>> Read()
        {
            return
                this._GradeRepository.Read();
        }

        public Task Update(GradeModel obj)
        {
            return
                this._GradeRepository.Update(obj);
        }
    }
}