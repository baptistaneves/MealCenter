namespace MealCenter.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    public class PostsController : BaseController
    {
        private readonly IPostAppService _postAppService;
        public PostsController(IMediatorHandler mediatorHandler,
                               INotificationHandler<DomainNotification> notifications,
                               IPostAppService postAppService) : base(mediatorHandler, notifications)
        {
            _postAppService = postAppService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllPosts()
        {
            return  Ok(await _postAppService.GetAll());
        }

        [HttpGet(ApiRoutes.Post.GetAllPostComments)]
        [ValidateGuid("postId")]
        public async Task<ActionResult> GetAllPostComments(Guid postId)
        {
            return Ok(await _postAppService.GetAllPostComment(postId));
        }


        [HttpGet(ApiRoutes.Post.GetAllPostReaction)]
        [ValidateGuid("postId")]
        public async Task<ActionResult> GetAllPostReactions(Guid postId)
        {
            return Ok(await _postAppService.GetAllPostReaction(postId));
        }

        [HttpGet(ApiRoutes.Post.GetPostById)]
        [ValidateGuid("id")]
        public async Task<ActionResult> GetPostById(Guid id)
        {
            return Ok(await _postAppService.GetById(id));
        }

        [HttpGet(ApiRoutes.Post.GetPostCommentById)]
        [ValidateGuid("id")]
        public async Task<ActionResult> GetPostCommentById(Guid id)
        {
            return Ok(await _postAppService.GetPostCommentById(id));
        }

        [HttpPost(ApiRoutes.Post.AddPost)]
        [ValidateModel]
        public async Task<ActionResult> AddPost([FromBody] CreatePost createPost, CancellationToken cancellationToken)
        {
            return Response(await _postAppService.Add(createPost, cancellationToken));
        }

        [HttpPost(ApiRoutes.Post.AddPostComment)]
        [ValidateModel]
        public async Task<ActionResult> AddPostComment([FromBody] CreatePostComment postComment, CancellationToken cancellationToken)
        {
            var identityId = HttpContext.GetIdentityIdClaimValue().ToString();
            return Response(await _postAppService.AddPostComment(postComment, identityId, cancellationToken));
        }

        [HttpPost(ApiRoutes.Post.AddPostReaction)]
        public async Task<ActionResult> AddPostReaction([FromBody] CreatePostReaction postReaction, CancellationToken cancellationToken)
        {
            var identityId = HttpContext.GetIdentityIdClaimValue().ToString();
            return Response(await _postAppService.AddPostReaction(postReaction, identityId, cancellationToken));
        }

        [HttpPut(ApiRoutes.Post.UpdatePost)]
        [ValidateGuid("id")]
        [ValidateModel]
        public async Task<ActionResult> UpdatePost(Guid id, [FromBody] UpdatePost updatePost, CancellationToken cancellationToken)
        {
            updatePost.Id = id;
            await _postAppService.Update(HttpContext.GetRestaurantIdClaimValue(), updatePost, cancellationToken);
            return Response();
        }

        [HttpPut(ApiRoutes.Post.UpdatePostComment)]
        [ValidateGuid("postId", "postCommentId")]
        [ValidateModel]
        public async Task<ActionResult> UpdatePostComment(Guid postId, Guid postCommentId, [FromBody] UpdatePostComment postComment, CancellationToken cancellationToken)
        {
            var restaurantId = HttpContext.GetRestaurantIdClaimValue();
            var identityId = HttpContext.GetIdentityIdClaimValue().ToString();

            postComment.Id = postCommentId;

            await _postAppService.UpdatePostComment(postCommentId, postId, restaurantId, identityId, postComment, cancellationToken);
            return Response();
        }

        [HttpDelete(ApiRoutes.Post.RemovePost)]
        [ValidateGuid("id")]
        public async Task<ActionResult> RemovePost(Guid id, CancellationToken cancellationToken)
        {
            await _postAppService.Remove(id, HttpContext.GetRestaurantIdClaimValue(), cancellationToken);
            return Ok();
        }

        [HttpDelete(ApiRoutes.Post.RemovePostComment)]
        [ValidateGuid("postId", "postCommentId")]
        public async Task<ActionResult> RemovePostCommentFromPost(Guid postId, Guid postCommentId, CancellationToken cancellationToken)
        {
            var restaurantId = HttpContext.GetRestaurantIdClaimValue();
            var identityId = HttpContext.GetIdentityIdClaimValue().ToString();

            await _postAppService.RemovePostCommentFromPost(postCommentId, postId, restaurantId, identityId, cancellationToken);

            return Ok();
        }

        [HttpDelete(ApiRoutes.Post.RemovePostReaction)]
        [ValidateGuid("postId", "postReactionId")]
        public async Task<ActionResult> RemovePostReactionFromPost(Guid postId, Guid postReactionId, CancellationToken cancellationToken)
        {
            var restaurantId = HttpContext.GetRestaurantIdClaimValue();
            var identityId = HttpContext.GetIdentityIdClaimValue().ToString();

            await _postAppService.RemovePostReactionFromPost(postReactionId, postId, restaurantId, identityId, cancellationToken);

            return Ok();
        }
    }
}
