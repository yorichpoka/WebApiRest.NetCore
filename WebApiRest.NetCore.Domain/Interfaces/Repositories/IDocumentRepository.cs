using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Models.MongoDB;

namespace WebApiRest.NetCore.Domain.Interfaces.Repositories
{
    public interface IDocumentRepository
    {
        Task<DocumentModel> Create(DocumentModel obj);

        Task<DocumentModel> Read(string document_id);

        Task Update(DocumentModel obj);

        Task Delete(string document_id);
    }
}