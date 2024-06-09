using HocWeb.Infrastructure;

namespace HocWeb.Service.Services
{
    public abstract class BaseService
    {
        protected readonly DataContext _dataContext;
        protected readonly DateTime _now = DateTime.UtcNow.AddHours(7);

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public BaseService(DataContext dataContext) {
            _dataContext = dataContext;
        }
    }
}
