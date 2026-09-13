using exam_system.Features.Quizzes.AdminManageQuestions.Commands;
using exam_system.Features.Quizzes.AdminManageQuestions.Dtos;
using exam_system.Features.Quizzes.AdminManageQuestions.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Quizzes.AdminManageQuestions.Controllers
{

    [ApiController]

    public class QuestionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuestionsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Route("api/quizzes/questions")]
        public async Task<IActionResult> AddQuestion(
        [FromBody] AddQuestionViewModel model,
        CancellationToken cancellationToken)
            {
                await _mediator.Send(
                    new AddQuestionOrchestrator(
                        model.QuizId,
                        model.Text,
                        model.Explanation,
                        model.OrderIndex,
                        model.Options.Select(option =>
                            new AddQuestionOrchestrator.AddOption(
                                option.OptionText,
                                option.IsCorrect
                            )).ToList()
                    ),
                    cancellationToken);

                return Ok();
        }






        [HttpPut]
        [Route("api/quizzes/questions/{questionId}")]
        public async Task<IActionResult> UpdateQuestion(
    Guid questionId,
    [FromBody] UpdateQuestionViewModel model,
    CancellationToken cancellationToken)
        {
            await _mediator.Send(
                new UpdateQuestionOrchestrator(
                    questionId,
                    model.Text,
                    model.Explanation,
                    model.OrderIndex,
                    model.Options.Select(option =>
                        new UpdateQuestionOrchestrator.UpdateOption(
                            option.OptionText,
                            option.IsCorrect
                        )).ToList()
                ),
                cancellationToken);

            return Ok();
        }



        [HttpDelete]
        [Route("api/quizzes/questions/{questionId}")]
        public async Task<IActionResult> DeleteQuestion(
    Guid questionId,
    CancellationToken cancellationToken)
        {
            await _mediator.Send(
                new DeleteQuestionCommand(questionId),
                cancellationToken);

            return Ok();
        }




    }
}
