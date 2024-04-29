using SecurePass.Auth.Domain;
using SecurePass.Auth.User.Domain;
using SecurePass.Repository;
using SecurePass.Utils;

namespace SecurePass.Auth.User.Services
{
  public class UserService(IRepository<UserEntity> userRepository, IEncrypt encrypt) : IUserService
  {
    private readonly IRepository<UserEntity> _userRepository = userRepository;
    private readonly IEncrypt _encrypt = encrypt;

    public async Task<int> Create(UserEntity entity)
    {
      UserEntity? isUniqueEmail = await GetByEmail(entity.Email);

      if (isUniqueEmail != null)
      {
        return 2;
      }

      entity.Password = this._encrypt.GetSHA256(entity.Password);

      return await _userRepository.Add(entity);
    }

    public async Task<int> Create(RegisterDto entity)
    {
      UserEntity? isUniqueEmail = await GetByEmail(entity.Email);

      if (isUniqueEmail != null)
      {
        return 2;
      }

      entity.Password = this._encrypt.GetSHA256(entity.Password);

      return await _userRepository.Add(entity);
    }

    public async Task<int> Delete(Guid id)
    {
      return await _userRepository.SoftDelete(id);
    }

    public async Task<List<UserEntity>> GetAll()
    {
      IEnumerable<UserEntity> entity = await _userRepository.GetAll();

      return [.. entity];
    }

    public async Task<UserEntity?> GetById(Guid id)
    {
      var user = await _userRepository.GetById(id);

      return user;
    }

    public async Task<UserEntity?> GetByEmail(string email)
    {
      IEnumerable<UserEntity> user = await _userRepository.GetAll();

      if (user.Any())
      {
        return user.FirstOrDefault(user => user.Email == email);
      }

      return null;
    }

    public async Task<List<UserEntity>?> Search(string term)
    {
      IEnumerable<UserEntity> entity = await _userRepository.GetAll();

      return !entity.Any() ? null : [.. entity.Where(user => user.Name.Contains(term) || user.LastName.Contains(term) || user.Email.Contains(term))];
    }

    public async Task<int> Update(Guid id, UserEntity entity)
    {
      return await _userRepository.Update(entity);
    }
  }
}