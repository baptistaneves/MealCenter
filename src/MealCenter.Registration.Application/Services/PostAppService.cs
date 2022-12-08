using AutoMapper;
using MealCenter.Core.Communication.Mediator;
using MealCenter.Core.Messages.CommonMessages.Notifications;
using MealCenter.Registration.Application.Contracts.Posts;
using MealCenter.Registration.Application.ErrorMessages;
using MealCenter.Registration.Application.Interfaces;
using MealCenter.Registration.Application.Validators.Posts;
using MealCenter.Registration.Domain.Posts;

namespace MealCenter.Registration.Application.Services
{
    public class PostAppService : BaseService, IPostAppService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;

        public PostAppService(IPostRepository postRepository, IMapper mapper, IMediatorHandler mediatorHandler)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _mediatorHandler = mediatorHandler;
        }

        public async Task Add(CreatePost newPost, CancellationToken cancellationToken)
        {
            if (!Validate(new CreatePostValidator(), newPost)) return;

            if(await _postRepository.PostAlreadyExists(newPost.Title))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("post", PostErrorMessages.PostTitleAreadyExists));
                return;
            }

            _postRepository.Add(_mapper.Map<Post>(newPost));

            await _postRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task AddPostComment(CreatePostComment newPostComment, string identityUserId, CancellationToken cancellationToken)
        {
            if (!Validate(new CreatePostCommentValidator(), newPostComment)) return;

            _postRepository.AddPostComment(new PostComment(newPostComment.PostId, identityUserId, newPostComment.Comment));

            await _postRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task AddPostReaction(CreatePostReaction newReaction, string identityUserId, CancellationToken cancellationToken)
        {
            _postRepository.AddPostReaction(new PostReaction(newReaction.PostId, identityUserId, newReaction.ReactionType));

            await _postRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            return await _postRepository.GetAll();
        }

        public async Task<IEnumerable<PostComment>> GetAllPostComment()
        {
            return await _postRepository.GetAllPostComment();
        }

        public async Task<IEnumerable<PostReaction>> GetAllPostReaction()
        {
            return await _postRepository.GetAllPostReaction();
        }

        public async Task<Post> GetById(Guid id)
        {
            return await _postRepository.GetById(id);
        }

        public async Task<PostComment> GetPostCommentById(Guid id)
        {
            return await _postRepository.GetPostCommentById(id);
        }

        public async Task<PostReaction> GetPostReactionById(Guid id)
        {
            return await _postRepository.GetPostReactionById(id);
        }

        public async Task Remove(Guid id, CancellationToken cancellationToken)
        {
            _postRepository.Remove(await _postRepository.GetById(id));

            await _postRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task RemovePostComment(Guid id, CancellationToken cancellationToken)
        {
            _postRepository.RemovePostComment(await _postRepository.GetPostCommentById(id));

            await _postRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task RemovePostReaction(Guid id, CancellationToken cancellationToken)
        {
            _postRepository.RemovePostReaction(await _postRepository.GetPostReactionById(id));

            await _postRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task Update(UpdatePost post, CancellationToken cancellationToken)
        {
            if (!Validate(new UpdatePostValidator(), post)) return;

            if(await _postRepository.PostAlreadyExists(post.Id, post.Title))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("post", PostErrorMessages.PostTitleAreadyExists));
                return;
            }

            _postRepository.Update(_mapper.Map<Post>(post));
            await _postRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task UpdatePostComment(UpdatePostComment postComment, CancellationToken cancellationToken)
        {
            if (!Validate(new UpdatePostCommentValidator(), postComment)) return;

            _postRepository.UpdatePostComment(_mapper.Map<PostComment>(postComment));

            await _postRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public void Dispose()
        {
            _postRepository?.Dispose();
        }
    }
}
