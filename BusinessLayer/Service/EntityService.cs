using SecondaApp.Abstract;

namespace SecondaApp.BusinessLayer.Service
{
    public class EntityService<T> : IEntityService<T> where T : class
    {
        private readonly IRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public EntityService(IRepository<T> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public T GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Create(T entity)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _repository.Insert(entity);
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
            }
            catch
            {
                _unitOfWork?.Rollback();
                throw;
            }
        }

        public void Update(T entity)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _repository.Update(entity);
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
            }
            catch
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _repository.Delete(id);
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
            }
            catch
            {
                _unitOfWork.Rollback();
                throw;
            }
        }
    }
}
