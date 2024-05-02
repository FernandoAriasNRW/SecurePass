using Microsoft.EntityFrameworkCore;
using SecurePass.Auth.Domain;
using SecurePass.Repository;
using SecurePass.Utils;

namespace SecurePass.Auth.User.Services
{
  public class UserService(IRepository<Domain.User> userRepository, IEncrypt encrypt) : IUserService
  {
    private readonly IRepository<Domain.User> _userRepository = userRepository;
    private readonly IEncrypt _encrypt = encrypt;

    public async Task<int> Create(Domain.User entity)
    {
      Domain.User? isUniqueEmail = await GetByEmail(entity.Email);

      if (isUniqueEmail != null)
      {
        return 2;
      }

      entity.Password = this._encrypt.GetSHA256(entity.Password);

      return await _userRepository.Add(entity);
    }

    public async Task<int> Create(RegisterDto entity)
    {
      Domain.User? isUniqueEmail = await GetByEmail(entity.Email);

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

    public async Task<List<Domain.User>> GetAll()
    {
      IEnumerable<Domain.User> entity = await _userRepository.GetAll().ToListAsync();

      return [.. entity];
    }

    public async Task<Domain.User?> GetById(Guid id)
    {
      var user = await _userRepository.GetById().FindAsync(id);

      return user;
    }

    public async Task<Domain.User?> GetByEmail(string email)
    {
      IEnumerable<Domain.User> user = await _userRepository.GetAll().ToListAsync();

      if (user.Any())
      {
        return user.FirstOrDefault(user => user.Email == email);
      }

      return null;
    }

    public async Task<List<Domain.User>?> Search(string term)
    {
      IEnumerable<Domain.User> entity = await _userRepository.GetAll().ToListAsync();

      return !entity.Any() ? null : [.. entity.Where(user => user.Name.Contains(term) || user.LastName.Contains(term) || user.Email.Contains(term))];
    }

    public async Task<int> Update(Guid id, Domain.User entity)
    {
      return await _userRepository.Update(entity);
    }
  }
}