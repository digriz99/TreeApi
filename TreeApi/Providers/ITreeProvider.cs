using System;
using System.Threading.Tasks;
using TreeApi.Models;

namespace TreeApi.Providers
{
    public interface ITreeProvider : IDisposable
    {
        Task<World[]> BuildTree();
    }
}
