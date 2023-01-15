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

        public async Task<Post> Add(CreatePost newPost, CancellationToken cancellationToken)
        {
            if (!Validate(new CreatePostValidator(), newPost)) return null;

            if(await _postRepository.PostAlreadyExists(newPost.Title))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("post", PostErrorMessages.PostTitleAreadyExists));
                return null;
            }

            var post = _mapper.Map<Post>(newPost);

            _postRepository.Add(post);

            await _postRepository.UnitOfWork.SaveAsync(cancellationToken);
            return post;
        }

        public async Task<PostComment> AddPostComment(CreatePostComment newPostComment, string identityUserId, CancellationToken cancellationToken)
        {
            if (!Validate(new CreatePostCommentValidator(), newPostComment)) return null;

            var postComment = new PostComment(newPostComment.PostId, identityUserId, newPostComment.Comment);
            _postRepository.AddPostComment(postComment);

            await _postRepository.UnitOfWork.SaveAsync(cancellationToken);
            return postComment;
        }

        public async Task<PostReaction> AddPostReaction(CreatePostReaction newReaction, string identityUserId, CancellationToken cancellationToken)
        {
            var postReaction = new PostReaction(newReaction.PostId, identityUserId, newReaction.ReactionType);
            _postRepository.AddPostReaction(postReaction);

            await _postRepository.UnitOfWork.SaveAsync(cancellationToken);
            return postReaction;
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            return await _postRepository.GetAll();
        }

        public async Task<IEnumerable<PostComment>> GetAllPostComment(Guid postId)
        {
            return await _postRepository.GetAllPostComment(postId);
        }

        public async Task<IEnumerable<PostReaction>> GetAllPostReaction(Guid postId)
        {
            return await _postRepository.GetAllPostReaction(postId);
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

        public async Task Remove(Guid id, Guid restaurantId, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetById(id);
            if(post == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Post", PostErrorMessages.PostNotFound));
                return;
            }

            if(post.RestaurantId != restaurantId)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Post", PostErrorMessages.PostDeleteNotNotAuthorized));
                return;
            }

            _postRepository.Remove(post);

            await _postRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task RemovePostCommentFromPost(Guid postCommentId, Guid postId, Guid restaurantId, string identityId, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetPostWithPostCommentsIncludedById(postId);
            if (post == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Post", PostErrorMessages.PostNotFound));
                return;
            }

            var postComment = post.PostComments.FirstOrDefault(pc => pc.Id == postCommentId);
            if (postComment == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Post", PostErrorMessages.PostCommentNotFound));
                return;
            }

            if (post.RestaurantId != restaurantId && postComment.IdentityUserId != identityId)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Post", PostErrorMessages.PostCommentRemovelNotNotAuthorized));
                return;
            }

            _postRepository.RemovePostComment(postComment);

            await _postRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task RemovePostReactionFromPost(Guid postReactionId, Guid postId, Guid restaurantId, string identityId, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetPostWithPostCommentsIncludedById(postId);
            if (post == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Post", PostErrorMessages.PostNotFound));
                return;
            }

            var postReaction = post.PostReactions.FirstOrDefault(pc => pc.Id == postReactionId);
            if (postReaction == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Post", PostErrorMessages.PostReactionNotFound));
                return;
            }

            if (post.RestaurantId != restaurantId && postReaction.IdentityUserId != identityId)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Post", PostErrorMessages.PostReactionRemovelNotNotAuthorized));
                return;
            }

            _postRepository.RemovePostReaction(postReaction);

            await _postRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task Update(Guid postId, Guid restaurantId, UpdatePost postUpdated, CancellationToken cancellationToken)
        {
            if (!Validate(new UpdatePostValidator(), postUpdated)) return;

            var post = await _postRepository.GetById(postId);
            if (post == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Post", PostErrorMessages.PostNotFound));
                return;
            }

            if (post.RestaurantId != restaurantId)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Post", PostErrorMessages.PostUpdateNotNotAuthorized));
                return;
            }

            if (await _postRepository.PostAlreadyExists(postId, postUpdated.Title))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("post", PostErrorMessages.PostTitleAreadyExists));
                return;
            }

            post.UpdatePost(postUpdated.Title, postUpdated.Content);

            _postRepository.Update(post);

            await _postRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task UpdatePostComment(Guid postCommentId, Guid postId, Guid restaurantId, string identityId, UpdatePostComment postCommentUpdated, CancellationToken cancellationToken)
        {
            if (!Validate(new UpdatePostCommentValidator(), postCommentUpdated)) return;

            var post = await _postRepository.GetPostWithPostCommentsIncludedById(postId);
            if (post == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Post", PostErrorMessages.PostNotFound));
                return;
            }

            var postComment = post.PostComments.FirstOrDefault(pc => pc.Id == postCommentId);
            if (postComment == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("PostComment", PostErrorMessages.PostCommentNotFound));
                return;
            }

            if (post.RestaurantId != restaurantId && postComment.IdentityUserId != identityId)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("PostComment", PostErrorMessages.PostCommentRemovelNotNotAuthorized));
                return;
            }

            postComment.UpdatePostComment(postCommentUpdated.Comment);

            _postRepository.UpdatePostComment(postComment);

            await _postRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public void Dispose()
        {
            _postRepository?.Dispose();
        }
    }
}
