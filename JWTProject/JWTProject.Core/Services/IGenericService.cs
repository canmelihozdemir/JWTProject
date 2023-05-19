using JWTProject.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.Core.Services
{
    public interface IGenericService<TEntity,TDto> where TEntity : class where TDto:class
    {
        Task<ResponseDto<TDto>> GetByIdAsync(int id);
        Task<ResponseDto<IEnumerable<TDto>>> GetAllAsync();
        Task<ResponseDto<TDto>> AddAsync(TDto entityDto);
        Task<ResponseDto<NoDataDto>> Update(TDto entityDto);
        Task<ResponseDto<NoDataDto>> Remove(TDto entityDto);
        Task<ResponseDto<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate);
    }
}
