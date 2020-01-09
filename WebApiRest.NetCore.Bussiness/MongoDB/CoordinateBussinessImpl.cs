using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Interfaces.Bussiness.MongoDB;
using WebApiRest.NetCore.Domain.Interfaces.Repositories.MongoDB;
using WebApiRest.NetCore.Domain.Models.MongoDB;

namespace WebApiRest.NetCore.Bussiness.MySql
{
    public class CoordinateBussinessImpl : ICoordinateBussiness
    {
        private readonly ICoordinateRepository _CoordinateRepository;

        public CoordinateBussinessImpl(ICoordinateRepository repository)
        {
            this._CoordinateRepository = repository;
        }

        public Task<CoordinateModel> Create(CoordinateModel obj)
        {
            return
                this._CoordinateRepository.Create(obj);
        }

        public Task Delete(string grade_id)
        {
            return
                this._CoordinateRepository.Delete(grade_id);
        }

        public Task Delete(string[] grade_ids)
        {
            return
                this._CoordinateRepository.Delete(grade_ids);
        }

        public Task<CoordinateModel> Read(string id)
        {
            return
                this._CoordinateRepository.Read(id);
        }

        public Task<IEnumerable<CoordinateModel>> Read(int top)
        {
            return
                this._CoordinateRepository.Read(top);
        }

        public Task<IEnumerable<CoordinateModel>> Read()
        {
            return
                this._CoordinateRepository.Read();
        }

        public Task Update(CoordinateModel obj)
        {
            return
                this._CoordinateRepository.Update(obj);
        }
    }
}