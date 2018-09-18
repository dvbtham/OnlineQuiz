﻿using OnlineQuiz.Common.ViewModel;
using OnlineQuiz.Model.Infrastructure;
using OnlineQuiz.Model.Repositories;
using System;
using System.Web.Mvc;

namespace OnlineQuiz.WebApp.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IExamineeRepository examineeRepository;
        private readonly IExamPeriodRepository examPeriodRepository;
        private readonly IQuestionModuleRepository questionModuleRepository;
        private readonly IRegistrationRepository registrationRepository;

        public RegistrationController(IUnitOfWork unitOfWork,
            IExamineeRepository examineeRepository,
            IExamPeriodRepository examPeriodRepository,
            IQuestionModuleRepository questionModuleRepository,
            IRegistrationRepository registrationRepository)
        {
            this.unitOfWork = unitOfWork;
            this.examineeRepository = examineeRepository;
            this.examPeriodRepository = examPeriodRepository;
            this.questionModuleRepository = questionModuleRepository;
            this.registrationRepository = registrationRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveExaminee(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var examinee = examineeRepository.InsertOrUpdate(viewModel.Examinee);
                unitOfWork.Commit();
                viewModel.Examinee = examinee;

                return RedirectToAction("Register", "Login", new { ic = examinee.IdentityCard });
            }
            else
            {
                return RedirectToAction("Register", "Login");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveRegistration(RegisterViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var vm = new InsertRegistrationViewModel
                    {
                        ExamPeriodId = viewModel.ExamPeriod.ID,
                        IdentityCard = viewModel.IdentityCard,
                        InformationTechnologySkillId = Guid.Parse(viewModel.TechSkill.Key),
                        QuestionModuleId = Guid.Parse(viewModel.Module.Key),
                        RegistrationDate = viewModel.RegistrationDate
                    };

                    if (viewModel.TechSkill.Value.Contains("nâng cao"))
                    {
                        registrationRepository.InsertAdvancedModuleRegistration(vm);
                    }
                    else
                        registrationRepository.InsertBasicRegistration(vm);

                    unitOfWork.Commit();

                    return RedirectToAction("Register", "Login", new { ic = vm.IdentityCard });
                }
                else
                {
                    return RedirectToAction("Register", "Login");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult PopulateExamPeriods(string epId)
        {
            var examPeriod = examPeriodRepository.GetById(epId);

            return Json(new
            {
                startDate = examPeriod.StartDate.ToString("dd/MM/yyyy"),
                endDate = examPeriod.EndDate.ToString("dd/MM/yyyy")
            });
        }

        [HttpPost]
        public ActionResult PopulateQuestionModules(string techId)
        {
            var questionModules = questionModuleRepository.GetKeyValueListByTechId(techId);

            return Json(new
            {
                data = questionModules
            });
        }

        [HttpPost]
        public ActionResult PopulateExamineeByIdCard(string idCard)
        {
            var examinee = examineeRepository.FindByIdentityCard(idCard);

            return Json(new
            {
                data = examinee
            });
        }
    }
}