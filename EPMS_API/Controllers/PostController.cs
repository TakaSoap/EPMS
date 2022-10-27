using EPMS_API.Models;
using EPMS_API.Models.Comment;
using EPMS_API.Models.Posts;
using EPMS_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using static EPMS_API.AuthingClient;


namespace EPMS_API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<List<Post>> Get() => await _postService.GetAsync();

        [HttpGet]
        [Route("getall")]
        public async Task<List<PostProcessed>> GetAllPosts()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString()[7..];

            var userId = await GetUserIdByToken(token);

            List<PostProcessed> postsProcessed = new();
            var posts = await _postService.GetAsync();

            foreach (Post post in posts)
            {
                var user = await _postService.GetUserAsync(post.UserId);
                var authingUser = await Client.Users.Detail(user.UserIdAuthing);

                var isSubscribed = true;
                var subscription = await _postService.GetSubscription(userId, post.UserId);

                if (subscription == null)
                {
                    isSubscribed = false;
                }

                var ratings = await _postService.GetRatingsByPostIdAsync(post.Id);
                var internshipBase = await _postService.GetBaseAsync(user.baseId);

                if (internshipBase == null)
                {
                    internshipBase = new InternshipBase
                    {
                        BaseName = "无所属基地"
                    };
                }

                double postRatingValue;

                if (ratings.Count() > 0)
                {
                    postRatingValue = ratings.Average(rating => rating.Score);
                }
                else
                {
                    postRatingValue = 0;
                }

                var postSelfRatingValue = await _postService.GetRatingByUserIdAndPostId(post.UserId, post.Id);

                if (postSelfRatingValue is null)
                {
                    postSelfRatingValue = new Rating()
                    {
                        Score = 0
                    };
                }

                bool isDeletable = false;
                bool isSelf = false;

                if (user.UserType == "student")
                {
                    if (userId == post.UserId)
                    {
                        isDeletable = true;
                        isSelf = true;
                    }
                }
                else
                {
                    isDeletable = true;
                }

                postsProcessed.Add(new PostProcessed(post, authingUser.Photo, user.Name, internshipBase.BaseName, postRatingValue, postSelfRatingValue.Score, isSubscribed, isDeletable, isSelf));
            }

            return postsProcessed;
        }

        [HttpGet]
        [Route("getsamebase")]
        public async Task<List<PostProcessed>> GetSameBasePosts()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString()[7..];

            var userId = await GetUserIdByToken(token);
            var currentUser = await _postService.GetUserAsync(userId);
            var internshipBase = await _postService.GetBaseAsync(currentUser.baseId);

            List<PostProcessed> postsProcessed = new();
            var posts = await _postService.GetAsync();

            foreach (Post post in posts)
            {
                var user = await _postService.GetUserAsync(post.UserId);

                if (user.baseId != currentUser.baseId)
                {
                    continue;
                }

                var isSubscribed = true;
                var subscription = await _postService.GetSubscription(userId, post.UserId);

                if (subscription == null)
                {
                    isSubscribed = false;
                }

                var authingUser = await Client.Users.Detail(user.UserIdAuthing);

                var ratings = await _postService.GetRatingsByPostIdAsync(post.Id);

                double postRatingValue;

                if (ratings.Count() > 0)
                {
                    postRatingValue = ratings.Average(rating => rating.Score);
                }
                else
                {
                    postRatingValue = 0;
                }

                var postSelfRatingValue = await _postService.GetRatingByUserIdAndPostId(post.UserId, post.Id);

                if (postSelfRatingValue is null)
                {
                    postSelfRatingValue = new Rating()
                    {
                        Score = 0
                    };
                }

                bool isDeletable = false;
                bool isSelf = false;

                if (user.UserType == "student")
                {
                    if (userId == post.UserId)
                    {
                        isDeletable = true;
                        isSelf = true;
                    }
                }
                else
                {
                    isDeletable = true;
                }

                postsProcessed.Add(new PostProcessed(post, authingUser.Photo, user.Name, internshipBase.BaseName, postRatingValue, postSelfRatingValue.Score, isSubscribed, isDeletable, isSelf));
            }

            return postsProcessed;
        }

        [HttpGet]
        [Route("getsubscribed")]
        public async Task<List<PostProcessed>> GetSubscribedPosts()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString()[7..];

            var userId = await GetUserIdByToken(token);

            List<PostProcessed> postsProcessed = new();
            var posts = await _postService.GetAsync();

            foreach (Post post in posts)
            {

                if(userId == post.UserId)
                {
                    continue;
                }

                var subscription = await _postService.GetSubscription(userId, post.UserId);

                if (subscription == null)
                {
                    continue;
                }

                var user = await _postService.GetUserAsync(post.UserId);
                var authingUser = await Client.Users.Detail(user.UserIdAuthing);

                var ratings = await _postService.GetRatingsByPostIdAsync(post.Id);
                var internshipBase = await _postService.GetBaseAsync(user.baseId);

                if (internshipBase == null)
                {
                    internshipBase = new InternshipBase
                    {
                        BaseName = "无所属基地"
                    };
                }

                double postRatingValue;

                if (ratings.Count() > 0)
                {
                    postRatingValue = ratings.Average(rating => rating.Score);
                }
                else
                {
                    postRatingValue = 0;
                }

                var postSelfRatingValue = await _postService.GetRatingByUserIdAndPostId(post.UserId, post.Id);

                if (postSelfRatingValue is null)
                {
                    postSelfRatingValue = new Rating()
                    {
                        Score = 0
                    };
                }

                bool isDeletable = false;

                if (user.UserType != "student")
                {
                    isDeletable = true;
                }

                postsProcessed.Add(new PostProcessed(post, authingUser.Photo, user.Name, internshipBase.BaseName, postRatingValue, postSelfRatingValue.Score, true, isDeletable, false));
            }

            return postsProcessed;
        }

        [HttpGet]
        [Route("getself")]
        public async Task<List<PostProcessed>> GetSelfPosts()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString()[7..];

            var userId = await GetUserIdByToken(token);

            List<PostProcessed> postsProcessed = new();
            var posts = await _postService.GetSelfAsync(userId);
            var user = await _postService.GetUserAsync(userId);
            var internshipBase = await _postService.GetBaseAsync(user.baseId);
            var authingUser = await Client.Users.Detail(user.UserIdAuthing);

            foreach (Post post in posts)
            {
                var ratings = await _postService.GetRatingsByPostIdAsync(post.Id);

                double postRatingValue;

                if (ratings.Count() > 0)
                {
                    postRatingValue = ratings.Average(rating => rating.Score);
                }
                else
                {
                    postRatingValue = 0;
                }

                var postSelfRatingValue = await _postService.GetRatingByUserIdAndPostId(post.UserId, post.Id);

                if (postSelfRatingValue is null)
                {
                    postSelfRatingValue = new Rating()
                    {
                        Score = 0
                    };
                }

                postsProcessed.Add(new PostProcessed(post, authingUser.Photo, user.Name, internshipBase.BaseName, postRatingValue, postSelfRatingValue.Score, false, true, true));
            }

            return postsProcessed;
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Post>> Get(string id)
        {
            var post = await _postService.GetAsync(id);

            if (post is null)
            {
                return NotFound();
            }

            return post;
        }


        [HttpPost]
        public async Task<IActionResult> Post(NewPost newPost)
        {
            var postProcessed = new Post();

            var token = HttpContext.Request.Headers["Authorization"].ToString()[7..];

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var claimValue = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            if (claimValue != null)
            {
                var user = await _postService.GetUserByAuthingId(claimValue);

                if (user != null)
                {
                    postProcessed.UserId = user.Id;
                }
                else
                {
                    return UnprocessableEntity();
                }
            }
            else
            {
                return UnprocessableEntity();
            }

            postProcessed.PostTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            postProcessed.FileInfo = newPost.fileInfo;
            postProcessed.PostContent = newPost.content;

            await _postService.CreateAsync(postProcessed);

            return CreatedAtAction(nameof(Get), new { id = postProcessed.Id }, postProcessed);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _postService.RemoveAsync(id);
            return Ok();
        }

        [HttpGet("getcomments/{postId}")]
        public async Task<List<CommentProcessed>> GetCommentsByPostId(string postId)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString()[7..];

            var commentsProcessed = new List<CommentProcessed>();

            var comments = await _postService.GetCommentsByPostIdAsync(postId);

            foreach (var comment in comments)
            {
                var user = await _postService.GetUserAsync(comment.UserId);
                var content = comment.CommentContent;
                var id = comment.Id;
                bool isDeletable = false;

                if (user == null)
                {
                    continue;
                }

                if (user.UserType == "student")
                {
                    var commentUserId = await GetUserIdByToken(token);
                    if (user.Id == commentUserId)
                    {
                        isDeletable = true;
                    }
                }
                else
                {
                    isDeletable = true;
                }

                commentsProcessed.Add(new CommentProcessed(id, user.Name, content, isDeletable));
            }

            return commentsProcessed;
        }

        [HttpPost("comment")]
        public async Task<IActionResult> CreateComment(NewComment newComment)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString()[7..];

            var userId = await GetUserIdByToken(token);

            var comment = new Comment()
            {
                CommentContent = newComment.content,
                PostId = newComment.postId,
                UserId = userId
            };

            await _postService.CreateCommentAsync(comment);
            return CreatedAtAction(nameof(Get), new { id = comment.PostId }, comment);
        }

        [HttpDelete("comment/{id}")]
        public async Task<IActionResult> DeleteComment(string id)
        {
            await _postService.RemoveCommentAsync(id);
            return Ok();
        }

        [HttpPost("rate")]
        public async Task<double> RateByPostId(NewRating newRating)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString()[7..];
            var userId = await GetUserIdByToken(token);

            await _postService.UpdateRatingAsync(new Rating()
            {
                Score = newRating.score,
                PostId = newRating.postId,
                UserId = userId
            });

            var ratings = await _postService.GetRatingsByPostIdAsync(newRating.postId);
            double postRatingValue;

            if (ratings.Count() > 0)
            {
                postRatingValue = ratings.Average(rating => rating.Score);
            }
            else
            {
                postRatingValue = 0;
            }

            return postRatingValue;
        }

        [HttpPost("subscribe/{id}")]
        public async Task<IActionResult> SubscribeByPostId(string id)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString()[7..];
            var userId = await GetUserIdByToken(token);

            var subscription = new Subscription
            {
                SubscriberId = userId,
                SubscribedId = id
            };

            await _postService.SubscribeAsync(subscription);

            return Ok();
        }

        [HttpDelete("subscribe/{id}")]
        public async Task<IActionResult> UnsubscribeByPostId(string id)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString()[7..];
            var userId = await GetUserIdByToken(token);

            var subscription = new Subscription
            {
                SubscriberId = userId,
                SubscribedId = id
            };

            await _postService.UnsubscribeAsync(subscription);

            return Ok();
        }


        private async Task<string> GetUserIdByToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var claimValue = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            if (claimValue != null)
            {
                var user = await _postService.GetUserByAuthingId(claimValue);

                if (user != null)
                {
                    return user.Id;
                }
                else
                {
                    return "null";
                }
            }
            else
            {
                return "null";
            }
        }
    }
}