using AutoMapper;
using BLL.Repositories.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using presentationProject.Utility;
using StoryHubPresentation.ViewModels;

namespace StoryHubPresentation.Controllers
{
    public class StoryTellerController : Controller
    {

        #region Dependency Injection

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StoryTellerController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #endregion

        #region Get Actions

        public async Task<IActionResult> Index(string searchValue)
        {
            IEnumerable<StoryTeller> storyTellers;
            if (string.IsNullOrWhiteSpace(searchValue))
            {

                storyTellers = await _unitOfWork.Tellers.GetAllAsync();
                return View(_mapper.Map<IEnumerable<StoryTellerVm>>(storyTellers));
            }
            storyTellers = await _unitOfWork.Tellers.GetAllAsync(x => x.Name.ToLower().Contains(searchValue.ToLower()));
           // _mapper.Map<IEnumerable<StoryTellerVm>>(storyTellers);

            return View(_mapper.Map<IEnumerable<StoryTellerVm>>(storyTellers));
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? id, string viewName = nameof(Details))
        {
            if (!id.HasValue) return BadRequest();

            var teller = await _unitOfWork.Tellers.GetAsync(id.Value);

            if (teller == null) return BadRequest();


            return View(viewName, _mapper.Map<StoryTellerVm>(teller));
        }

        public async Task<IActionResult> Edit(int id)
        {
            return await Details(id, nameof(Edit));
        }

        #endregion

        #region Post Actions

        [HttpPost]
        public async Task<IActionResult> Create(StoryTellerVm storyTellerVm)
        {
            if (ModelState.IsValid)
            {
                storyTellerVm.ImageName = DocumentSettings.UploadFile(storyTellerVm.Image, "Images");
                var employee = _mapper.Map<StoryTellerVm, StoryTeller>(storyTellerVm);
                await _unitOfWork.Tellers.AddAsync(employee);


                if (await _unitOfWork.CompleteAsync() > 0)
                {
                    TempData["Success"] = "Story Teller is Added Successfully";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(storyTellerVm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StoryTellerVm model, [FromRoute] int? id)
        {
            if (id != model.Id) return BadRequest();

            if (!ModelState.IsValid) return View(model);

            try
            {
                if (model.Image != null)
                {
                    model.ImageName = DocumentSettings.UploadFile(model.Image, "Images");
                }
                _unitOfWork.Tellers.UpdateAsync(_mapper.Map<StoryTeller>(model));
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

            var teller = await _unitOfWork.Tellers.GetAsync(id.Value);

            if(teller == null) return BadRequest();

            _unitOfWork.Tellers.DeleteAsync(_mapper.Map<StoryTeller>(teller));

            if (await _unitOfWork.CompleteAsync() > 0)
                DocumentSettings.DeleteFile(teller.ImageName, "Images");

            return RedirectToAction(nameof(Index));

        }

        #endregion


    }
}
