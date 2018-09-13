using Microsoft.Ajax.Utilities;
using OfficeOpenXml;
using OnlineQuiz.Common;
using OnlineQuiz.Common.ImportExportHelper;
using OnlineQuiz.Model.Entity;
using OnlineQuiz.WebApp.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnlineQuiz.WebApp.Areas.Admin.Controllers
{
    public class ExaminationQuestionsController : BaseController
    {
        private OnlineQuizDbContext db = new OnlineQuizDbContext();

        // GET: Admin/ExaminationQuestions
        public async Task<ActionResult> Index()
        {
            ViewBag.Examinations = new SelectList(db.Examinations.Include(x => x.ExamPeriod).ToList(), "ID", "ExamPeriod.Name");
            var examinationQuestions = db.ExaminationQuestions.Include(e => e.Examination).Include(e => e.Question);
            return View(await examinationQuestions.ToListAsync());
        }

        // GET: Admin/ExaminationQuestions/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExaminationQuestion examinationQuestion = await db.ExaminationQuestions.FindAsync(id);
            if (examinationQuestion == null)
            {
                return HttpNotFound();
            }
            return View(examinationQuestion);
        }

        // GET: Admin/ExaminationQuestions/Create
        public ActionResult Create()
        {
            ViewBag.ExaminationID = new SelectList(db.Examinations, "ID", "Remark");
            ViewBag.QuestionID = new SelectList(db.Questions, "ID", "QuestionContent");
            return View();
        }

        // POST: Admin/ExaminationQuestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ExaminationID,ExamCode,QuestionID,QuestionContent,AAnswer,BAnswer,CAnswer,DAnswer,Answer,ResultAnswer")] ExaminationQuestion examinationQuestion)
        {
            if (ModelState.IsValid)
            {
                examinationQuestion.ID = Guid.NewGuid();
                db.ExaminationQuestions.Add(examinationQuestion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ExaminationID = new SelectList(db.Examinations, "ID", "Remark", examinationQuestion.ExaminationID);
            ViewBag.QuestionID = new SelectList(db.Questions, "ID", "QuestionContent", examinationQuestion.QuestionID);
            return View(examinationQuestion);
        }

        // GET: Admin/ExaminationQuestions/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExaminationQuestion examinationQuestion = await db.ExaminationQuestions.FindAsync(id);
            if (examinationQuestion == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExaminationID = new SelectList(db.Examinations, "ID", "Remark", examinationQuestion.ExaminationID);
            ViewBag.QuestionID = new SelectList(db.Questions, "ID", "QuestionContent", examinationQuestion.QuestionID);
            return View(examinationQuestion);
        }

        // POST: Admin/ExaminationQuestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ExaminationID,ExamCode,QuestionID,QuestionContent,AAnswer,BAnswer,CAnswer,DAnswer,Answer,ResultAnswer")] ExaminationQuestion examinationQuestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(examinationQuestion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ExaminationID = new SelectList(db.Examinations, "ID", "Remark", examinationQuestion.ExaminationID);
            ViewBag.QuestionID = new SelectList(db.Questions, "ID", "QuestionContent", examinationQuestion.QuestionID);
            return View(examinationQuestion);
        }

        // GET: Admin/ExaminationQuestions/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExaminationQuestion examinationQuestion = await db.ExaminationQuestions.FindAsync(id);
            if (examinationQuestion == null)
            {
                return HttpNotFound();
            }
            return View(examinationQuestion);
        }

        // POST: Admin/ExaminationQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            ExaminationQuestion examinationQuestion = await db.ExaminationQuestions.FindAsync(id);
            db.ExaminationQuestions.Remove(examinationQuestion);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Import(string examinationID, HttpPostedFileBase importedQuestionFile)
        {
            try
            {
                Guid.TryParse(examinationID, out Guid examId);
                var examination = db.Examinations.FirstOrDefault(x => x.ID == examId);

                if (examination == null)
                {
                    SetAlert("Kỳ thi không hợp lệ!", AlertClass.Error.ToDescriptionString());
                    return RedirectToAction("Index");
                }


                if (importedQuestionFile == null || importedQuestionFile.ContentLength <= 0)
                {
                    SetAlert("Bạn chưa chọn file.", AlertClass.Error.ToDescriptionString());
                    return RedirectToAction("Index");
                }

                int addedQuestions = 0, examCode = 0;

                #region properties
                var properties = new[]
                {
                    new PropertyByName<ExaminationQuestion>("ID"),
                    new PropertyByName<ExaminationQuestion>("ExamCode"),
                    new PropertyByName<ExaminationQuestion>("QuestionContent"),
                    new PropertyByName<ExaminationQuestion>("AAnswer"),
                    new PropertyByName<ExaminationQuestion>("BAnswer"),
                    new PropertyByName<ExaminationQuestion>("CAnswer"),
                    new PropertyByName<ExaminationQuestion>("DAnswer"),
                    new PropertyByName<ExaminationQuestion>("Answer"),
                    new PropertyByName<ExaminationQuestion>("ResultAnswer"),
                };
                #endregion

                var manager = new PropertyManager<ExaminationQuestion>(properties);

                using (var package = new ExcelPackage(importedQuestionFile.InputStream))
                {
                    var workSheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (workSheet == null)
                    {
                        SetAlert("Bảng tính không hợp lệ.", AlertClass.Error.ToDescriptionString());
                        return RedirectToAction("index");
                    }

                    var iRow = 2;

                    var questionList = new List<ExaminationQuestion>();

                    while (true)
                    {
                        var allColumnsAreEmpty = manager.GetProperties
                            .Select(property => workSheet.Cells[iRow, property.PropertyOrderPosition])
                            .All(cell => cell == null || cell.Value == null || string.IsNullOrEmpty(cell.Value.ToString()));

                        if (allColumnsAreEmpty)
                            break;

                        manager.ReadFromXlsx(workSheet, iRow);

                        var question = db.ExaminationQuestions.Find(manager.GetProperty("ID").GuidValue);
                        var isNew = question == null;
                        var questionContent = manager.GetProperty("QuestionContent").StringValue;
                        var questionByTitle = db.Questions.FirstOrDefault(x => x.QuestionContent == questionContent);
                        if (questionByTitle == null)
                        {
                            iRow++;
                            continue;
                        }
                        question = question ?? new ExaminationQuestion();

                        question.ID = Guid.NewGuid();
                        question.ExaminationID = examId;
                        question.ExamCode = manager.GetProperty("ExamCode").IntValue;
                        question.QuestionID = questionByTitle.ID;
                        question.QuestionContent = questionContent;
                        question.AAnswer = manager.GetProperty("AAnswer").StringValue;
                        question.BAnswer = manager.GetProperty("BAnswer").StringValue;
                        question.CAnswer = manager.GetProperty("CAnswer").StringValue;
                        question.DAnswer = manager.GetProperty("DAnswer").StringValue;
                        question.Answer = manager.GetProperty("Answer").StringValue;
                        question.ResultAnswer = manager.GetProperty("ResultAnswer").StringValue;

                        questionList.Add(question);

                        addedQuestions++;

                        iRow++;
                    }

                    db.ExaminationQuestions.AddRange(questionList);

                    db.SaveChanges();

                    SetAlert($"Tạo thành công {addedQuestions} cho mã đề {examCode}.", AlertClass.Success.ToDescriptionString());
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

        [HttpPost]
        public ActionResult ExamCodeList(string ExaminationID)
        {
            var exams = db.ExaminationQuestions
                .Where(x => x.ExaminationID.ToString() == ExaminationID)
                .Select(x => new KeyValuePair
                {
                    Id = x.ID,
                    Name = "Mã đề " + x.ExamCode.ToString()
                })
                .DistinctBy(x => x.Name);
            return PartialView("_ExamCodeList", exams);
        }

        [HttpPost]
        public ActionResult ExamQuestionList(int examCode)
        {
            var exams = db.ExaminationQuestions.Where(x => x.ExamCode == examCode).ToList();
            return PartialView("_ExamQuestionList", exams);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
