using System;
using System.Collections.Generic;
using System.Linq;
using BadNews.Models.Comments;
using BadNews.Repositories.Comments;
using Microsoft.AspNetCore.Mvc;

namespace BadNews.Controllers
{
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CommentsRepository commentsRepository;

        public CommentsController(CommentsRepository commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        // GET
        [HttpGet("api/news/{id}/comments")]
        public ActionResult<CommentsDto> GetCommentsForNews(Guid newsId)
        {
            var comments = (IReadOnlyCollection<CommentDto>)commentsRepository.GetComments(newsId).Select(c => new CommentDto()
            {
                User = c.User,
                Value = c.Value,
            }).ToArray();
            
            var dto = new CommentsDto()
            {
                Comments = comments,
                NewsId = newsId
            };
            
            return Ok(dto);
        }
    }
}