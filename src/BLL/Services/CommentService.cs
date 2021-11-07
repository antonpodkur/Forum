using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Abstractions;
using BLL.DTOs;
using DAL.Abstractions.Repositories;
using DAL.Abstractions.UnitOfWork;
using DAL.Entities;

namespace BLL.Services
{
    public class CommentService: ICommentService
    {
        
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        
        public CommentService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<CommentDTO> AddAsync(CommentDTO commentDto)
        {
            var comment = _mapper.Map<Comment>(commentDto);
            _unitOfWork.Comments.Add(comment);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<CommentDTO>(comment);
        }

        public async Task<IEnumerable<CommentDTO>> GetAllAsync()
        {
            var comments = await _unitOfWork.Comments.GetAllAsync();
            var commentDtos = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(comments);
            return commentDtos;
        }

        public async Task<CommentDTO> GetByIdAsync(string id)
        {
            var comment = await _unitOfWork.Comments.GetByIdAsync(id);
            var commentDto = _mapper.Map<CommentDTO>(comment);
            return commentDto;
        }

        public async Task UpdateAsync(CommentDTO commentDto)
        {
            var comment = _mapper.Map<Comment>(commentDto);
            _unitOfWork.Comments.Update(comment);
            await _unitOfWork.CompleteAsync();
        }

        public async Task RemoveAsync(string id)
        {
            var comment = await _unitOfWork.Comments.GetByIdAsync(id);
            _unitOfWork.Comments.Remove(comment);
            await _unitOfWork.CompleteAsync();
        }
    }
}