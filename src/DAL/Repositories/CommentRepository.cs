using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Abstractions.Repositories;
using DAL.DataContext;
using DAL.Entities;

namespace DAL.Repositories
{
    public class CommentRepository: Repository<Comment>, ICommentRepository
    {
        public CommentRepository(ForumContext context) : base(context) {}
        public async Task<Comment> GetByIdAsync(string id)
        {
            var comment = await Context.Set<Comment>().FindAsync(new Guid(id));
            return comment;
        }

        public void Update(Comment comment)
        {
            Context.Set<Comment>().Update(comment);
        }
    }
}