using SecurePass.Auth.User.Domain;
using SecurePass.Repository;

namespace SecurePass.Auth.User.Services
{
  public class UserService(IRepository<UserEntity> userRepository) : IUserService
  {
    private readonly IRepository<UserEntity> _userRepository = userRepository;

    public async Task Create(UserEntity entity)
    {
      await _userRepository.Add(entity);
    }

    public async Task Delete(Guid id)
    {
      await _userRepository.Delete(id);
    }

    public List<UserEntity> GetAll()
    {
      var entity = _userRepository.GetAll();

      return [.. entity];
    }

    public async Task<UserEntity> GetById(Guid id)
    {
      var user = await _userRepository.GetById(id);

      return user ?? throw new InvalidOperationException("Not Found any User");
    }

    public List<UserEntity> Search(string term)
    {
      var entity = _userRepository.GetAll();

      return [.. entity.Where(user => user.Name.Contains(term) || user.LastName.Contains(term) || user.Email.Contains(term))];
    }

    public async Task Update(Guid id, UserEntity entity)
    {
      await _userRepository.Update(id, entity);
    }
  }
}