namespace SecurePass.Auth.User.Services
{
  public interface IUserService
  {
    public Task<List<Domain.User>> GetAll();

    public Task<Domain.User?> GetById(Guid id);

    public Task<int> Create(Domain.User entity);

    public Task<int> Update(Guid id, Domain.User entity);

    public Task<int> Delete(Guid id);

    public Task<List<Domain.User>?> Search(string term);

    public Task<Domain.User?> GetByEmail(string email);
  }
}