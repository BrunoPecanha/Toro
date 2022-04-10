using Toro.Domain.Entity;

namespace Toro.Domain {
    public interface IInvestorService :  IServiceBase<Investor> {
        public void Create(int sceneCommand);       
    }
}
