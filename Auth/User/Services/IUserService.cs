using SecurePass.Auth.User.Domain;

namespace SecurePass.Auth.User.Services
{
  public interface IUserService
  {
    public Task<List<UserEntity>> GetAll();

    public Task<UserEntity?> GetById(Guid id);

    public Task<int> Create(UserEntity entity);

    public Task<int> Update(Guid id, UserEntity entity);

    public Task<int> Delete(Guid id);

    public Task<List<UserEntity>?> Search(string term);

    public Task<UserEntity?> GetByEmail(string email);
  }
}