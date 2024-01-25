using ApiBlog.Models;
using ApiBlog.Models.DTOs;
using ApiBlog.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBlog.Controllers
{
    [Route("api/posts")] //navegacion hacia el endpoint
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepo;
        private readonly IMapper _mapper;

        public PostsController(IPostRepository postRepo, IMapper mapper)
        {
            _postRepo = postRepo;
            _mapper = mapper;
        }


        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPosts()
        {
                //Model-Post
            var listPosts = _postRepo.GetPosts();

            var listPostsDTO = new List<PostDTO>();

                                //Model-Post
            foreach (var list in listPosts)
            {
                listPostsDTO.Add(_mapper.Map<PostDTO>(list));
            }
            return Ok(listPostsDTO);
        }

        [AllowAnonymous]
        [HttpGet("{postId:int}", Name ="GetPost")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPost(int postId)
        {
            //Model-Post
            var itemPost = _postRepo.GetPost(postId);

            if(itemPost == null)
            {
                return NotFound();
            }

            var itemPostDTO = _mapper.Map<PostDTO>(itemPost);
            return Ok(itemPostDTO);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(201, Type= typeof(PostCreateDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreatePost([FromBody] PostCreateDTO createPostDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_postRepo.ExistPost(createPostDTO.Title) == true)
            {
                ModelState.AddModelError("", "El post ya existe");
                return StatusCode(404, ModelState);
            }

            var post = _mapper.Map<Post>(createPostDTO);
            if (!_postRepo.CreatePost(post))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro{post.Title}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetPost", new {postId=post.Id}, post);
        }

        [Authorize]
        [HttpPatch("{postId:int}", Name ="UpdatePatchPost")]
        [ProducesResponseType(201, Type = typeof(PostCreateDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdatePatchPost(int postId, [FromBody] PostUpdateDTO updatePostDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (updatePostDTO == null || updatePostDTO.Id != postId )
            {
                return BadRequest(ModelState);
            }

            var post = _mapper.Map<Post>(updatePostDTO);

            if (! _postRepo.UpdatePost(post))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro{post.Title}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{postId:int}", Name = "DeletePost")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        public IActionResult DeletePost(int postId)
        {
            if (! _postRepo.ExistPost(postId))
            {
                return NotFound();
            }

            var post = _postRepo.GetPost(postId);

            if(!_postRepo.DeletePost(post))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando e registro{post.Title}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }
}
