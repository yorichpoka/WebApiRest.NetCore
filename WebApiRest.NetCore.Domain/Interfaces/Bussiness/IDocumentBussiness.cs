using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Models.MongoDB;

namespace WebApiRest.NetCore.Domain.Interfaces.Bussiness
{
    public interface IDocumentBussiness
    {
        Task<DocumentModel> Create(DocumentModel obj);

        Task<DocumentModel> Read(string document_id);

        Task Update(DocumentModel obj);

        Task Delete(string document_id);
    }
}