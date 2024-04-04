using AutoMapper;
using BLL.Repositories.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using presentationProject.Utility;
using StoryHubPresentation.ViewModels;

namespace StoryHubPresentation.Controllers
{
    public class StoryController : Controller
    {
        #region Dependancy Injection
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        } 
        #endregion

        #region Get Actions
        public async Task<IActionResult> Index(string searchValue)
        {
            IEnumerable<Story> stories;
            stories = await _unitOfWork.Stories.GetAllAsync();

            if (string.IsNullOrEmpty(searchValue))
            {
                var mappedStories = _mapper.Map<IEnumerable<StoryVm>>(stories);
                return View(mappedStories);
            }

            return View(_mapper.Map<IEnumerable<StoryVm>>(stories));
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? id, string viewName = nameof(Details))
        {
            if (!id.HasValue) return BadRequest();

            var story = await _unitOfWork.Stories.GetAsync(id.Value);

            if (story == null) return BadRequest();


            return View(viewName, _mapper.Map<StoryVm>(story));
        }

        public async Task<IActionResult> Edit(int id)
        {
            return await Details(id, nameof(Edit));
        }

        #endregion

        #region Post Actions

        [HttpPost]
        public async Task<IActionResult> Create(StoryVm model)
        {
            if (ModelState.IsValid)
            {
                model.ImageName = DocumentSettings.UploadFile(model.Image, "Images");
                var story = _mapper.Map<StoryVm, Story>(model);
                await _unitOfWork.Stories.AddAsync(story);


                if (await _unitOfWork.CompleteAsync() > 0)
                {
                    TempData["Success"] = "Story Teller is Added Successfully";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StoryVm model, [FromRoute] int? id)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid) return View(model);

            try
            {
                if (model.Image != null)
                {
                    model.ImageName = DocumentSettings.UploadFile(model.Image, "Images");
                }
                _unitOfWork.Stories.UpdateAsync(_mapper.Map<Story>(model));
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(model);
        }

        public async Task<IActionResult> Delete([FromRoute] int? id)
        {
            if (id is null) return NotFound();

            var story = await _unitOfWork.Stories.GetAsync(id.Value);

            if (story == null) return BadRequest();

            _unitOfWork.Stories.DeleteAsync(_mapper.Map<Story>(story));

            if (await _unitOfWork.CompleteAsync() > 0)
                DocumentSettings.DeleteFile(story.ImageName, "Images");

            return RedirectToAction(nameof(Index));

        }

        #endregion
    }
}
