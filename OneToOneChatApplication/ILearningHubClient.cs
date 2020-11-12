using System.Threading.Tasks;

namespace OneToOneChatApplication
{
    public interface ILearningHubClient
    {
        Task ReceiveMessage(string message);
    }
}
