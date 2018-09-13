using OfficeOpenXml;
using OnlineQuiz.Common;
using OnlineQuiz.Common.ImportExportHelper;
using OnlineQuiz.Model.Entity;
using OnlineQuiz.Model.Infrastructure;
using OnlineQuiz.Service.Services;
using OnlineQuiz.WebApp.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineQuiz.WebApp.Areas.Admin.Controllers
{
    public class QuestionController : BaseController
    {
        private readonly IQuestionService questionService;
        private readonly IUnitOfWork unitOfWork;
        private readonly OnlineQuizDbContext dbContext;

        public QuestionController(IQuestionService questionService,
            IUnitOfWork unitOfWork, OnlineQuizDbContext dbContext)
        {
            this.questionService = questionService;
            this.unitOfWork = unitOfWork;
            this.dbContext = dbContext;
        }
        // GET: Question
        public ActionResult Index()
        {
            return View(dbContext.Questions.Include(x => x.QuestionModule).Include(x => x.QuestionClassification));
        }

        /// <summary>
        /// Hàm nhập câu hỏi từ file excel
        /// </summary>
        /// <param name="importedQuestionFile"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Import(HttpPostedFileBase importedQuestionFile)
        {
            try
            {
                if (!FileValidator(importedQuestionFile))
                    return RedirectToAction("Index");

                int addedQuestions = 0, modifiedQuestions = 0, totalQuestions = 0;

                #region properties
                var properties = new[]
                {
                    new PropertyByName<Question>("ID"),
                    new PropertyByName<Question>("QuestionModuleID"),
                    new PropertyByName<Question>("QuestionClassificationID"),
                    new PropertyByName<Question>("QuestionContent"),
                    new PropertyByName<Question>("AAnswer"),
                    new PropertyByName<Question>("BAnswer"),
                    new PropertyByName<Question>("CAnswer"),
                    new PropertyByName<Question>("DAnswer"),
                    new PropertyByName<Question>("Answer"),
                    new PropertyByName<Question>("ResultAnswer"),
                };
                #endregion

                var manager = new PropertyManager<Question>(properties);

                using (var package = new ExcelPackage(importedQuestionFile.InputStream))
                {
                    var workSheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (workSheet == null)
                    {
                        SetAlert("Bảng tính không hợp lệ.", AlertClass.Error.ToDescriptionString());
                        return RedirectToAction("index");
                    }

                    var iRow = 2;

                    List<Question> questionList = new List<Question>();
                    while (true)
                    {
                        var allColumnsAreEmpty = manager.GetProperties
                            .Select(property => workSheet.Cells[iRow, property.PropertyOrderPosition])
                            .All(cell => cell == null || cell.Value == null || string.IsNullOrEmpty(cell.Value.ToString()));

                        if (allColumnsAreEmpty)
                            break;

                        manager.ReadFromXlsx(workSheet, iRow);
                        var questionContent = manager.GetProperty("QuestionContent").StringValue;
                        var question = questionService.GetByTitle(questionContent);
                        var isNew = question == null;
                        question = question ?? new Question();

                        if (isNew)
                            question.ID = Guid.NewGuid();

                        question.QuestionModuleID = manager.GetProperty("QuestionModuleID").GuidValue;
                        question.QuestionClassificationID = manager.GetProperty("QuestionClassificationID").GuidValue;
                        question.QuestionContent = questionContent;
                        question.AAnswer = manager.GetProperty("AAnswer").StringValue;
                        question.BAnswer = manager.GetProperty("BAnswer").StringValue;
                        question.CAnswer = manager.GetProperty("CAnswer").StringValue;
                        question.DAnswer = manager.GetProperty("DAnswer").StringValue;
                        question.Answer = manager.GetProperty("Answer").StringValue;
                        question.ResultAnswer = manager.GetProperty("ResultAnswer").StringValue;

                        if (isNew)
                        {
                            addedQuestions++;
                            questionList.Add(question);
                        }
                        else
                        {
                            questionService.Update(question);
                            modifiedQuestions++;
                        }

                        iRow++;
                        totalQuestions += 1;
                    }

                    questionService.AddRange(questionList);

                    unitOfWork.Commit();

                    SetAlert($"Bạn đã nhập {totalQuestions} câu hỏi thành công (thêm: {addedQuestions}, cập nhật: {modifiedQuestions}).", AlertClass.Success.ToDescriptionString());
                    return RedirectToAction("index");
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            SetAlert($"Đã xảy ra lỗi trong quá trình nhập dữ liệu.", AlertClass.Error.ToDescriptionString());
            return RedirectToAction("index");
        }

        public ActionResult GetModules()
        {
            try
            {
                var mdata = dbContext.QuestionModules.Select(x => new
                {
                    x.ID,
                    x.Name
                }).ToList();
                return Json(new { data = mdata }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { stactTrace = ex.StackTrace });
            }

        }

        public ActionResult GetLevels()
        {
            try
            {
                var mdata = dbContext.QuestionClassifications.Select(x => new
                {
                    x.ID,
                    x.Name
                }).ToList();
                return Json(new { data = mdata }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { stactTrace = ex.StackTrace });
            }
        }

        [HttpPost]
        public ActionResult SaveEdit([System.Web.Http.FromBody]QuestionViewModel data)
        {
            try
            {
                var entity = questionService.FindById(data.ID);

                entity.QuestionContent = data.QuestionContent;
                entity.AAnswer = data.AAnswer;
                entity.BAnswer = data.BAnswer;
                entity.CAnswer = data.CAnswer;
                entity.DAnswer = data.DAnswer;
                entity.Answer = data.Answer;
                entity.ResultAnswer = data.ResultAnswer;

                questionService.Update(entity);
                unitOfWork.Commit();

                return Json(new
                {
                    status = true,
                    message = "Cập nhật thành công.",
                    mdata = data
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    status = false,
                    message = "Cập nhật không thành công.",
                    stackTrace = string.Format(e.StackTrace + "\n" + e.Message)
                });
            }

        }

        #region  Các hàm validations
        /// <summary>
        ///   Hàm kiểm tra đầu vào của file nhập vào
        /// </summary>
        /// <param name="importedQuestionFile"></param>
        /// <returns></returns>

        public bool FileValidator(HttpPostedFileBase importedQuestionFile)
        {
            if (importedQuestionFile == null || importedQuestionFile.ContentLength <= 0)
            {
                SetAlert("Bạn chưa chọn file.", AlertClass.Error.ToDescriptionString());
                return false;
            }

            if (!importedQuestionFile.FileName.EndsWith(".xlsx"))
            {
                SetAlert("Định dạng file không hợp lệ. Vui lòng chỉ chọn file .xlsx", AlertClass.Error.ToDescriptionString());
                return false;
            }
            return true;
        }
        #endregion
    }

}