using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment commModel)
        {
            return new CommentDto
            {
                Id = commModel.Id,
                Title = commModel.Title,
                Content = commModel.Content,
                CreatedOn = commModel.CreatedOn,
                StockId = commModel.StockId,
            };
        }

        public static Comment ToCommentFromCreateDTO(this CreateCommentRequestDto commDto, int stockId) {
            return new Comment
            {
                Title = commDto.Title,
                Content = commDto.Content,
                StockId = stockId
            };
        }
    }
}