using SecurePass.Auth.User.Domain;

namespace SecurePass.Auth.User.Services
{
  public interface IUserService
  {
    public List<UserEntity> GetAll();

    public Task<UserEntity> GetById(Guid id);

    public Task Create(UserEntity entity);

    public Task Update(Guid id, UserEntity entity);

    public Task Delete(Guid id);

    public List<UserEntity> Search(string term);
  }
}