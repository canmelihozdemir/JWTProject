using AutoMapper.Internal.Mappers;
using JWTProject.Core.Repositories;
using JWTProject.Core.Services;
using JWTProject.Core.UnitOfWorks;
using JWTProject.Repository.Repositories;
using JWTProject.Service.Mapping;
using JWTProject.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.Service.Services
{
    public class GenericService<TEntity, TDto> : IGenericService<TEntity, TDto> where TEntity : class where TDto : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TEntity> _repository;

        public GenericService(IUnitOfWork unitOfWork, IGenericRepository<TEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<ResponseDto<TDto>> AddAsync(TDto entityDto)
        {
            var newEntity=ObjectMapper.Mapper.Map<TEntity>(entityDto);
            await _repository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();

            return ResponseDto<TDto>.Success(ObjectMapper.Mapper.Map<TDto>(newEntity),201);
        }

        public async Task<ResponseDto<IEnumerable<TDto>>> GetAllAsync()
        {
            var entities = ObjectMapper.Mapper.Map<List<TDto>>(await _repository.GetAllAsync());

            return ResponseDto<IEnumerable<TDto>>.Success(entities, 200);
        }

        public async Task<ResponseDto<TDto>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity==null) return ResponseDto<TDto>.Fail("Id not found",404,true);


            return ResponseDto<TDto>.Success(ObjectMapper.Mapper.Map<TDto>(entity),200);
        }

        public async Task<ResponseDto<NoDataDto>> Remove(int id)
        {
            var isExistEntity = await _repository.GetByIdAsync(id);

            if (isExistEntity == null) return ResponseDto<NoDataDto>.Fail("Id not found", 404, true);

            _repository.Remove(isExistEntity);
            await _unitOfWork.CommitAsync();

            return ResponseDto<NoDataDto>.Success(204);
        }

        public async Task<ResponseDto<NoDataDto>> Update(TDto entityDto,int id)
        {
            var isExistEntity = await _repository.GetByIdAsync(id);

            if (isExistEntity == null) return ResponseDto<NoDataDto>.Fail("Id not found", 404, true);

            var updateEntity = ObjectMapper.Mapper.Map<TEntity>(entityDto);

            _repository.Update(updateEntity);
            await _unitOfWork.CommitAsync();

            return ResponseDto<NoDataDto>.Success(204);//204NoContent
        }

        public async Task<ResponseDto<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var list=_repository.Where(predicate);

            return ResponseDto<IEnumerable<TDto>>.Success(ObjectMapper.Mapper.Map<IEnumerable<TDto>>(await list.ToListAsync()),200);
        }
    }
}
