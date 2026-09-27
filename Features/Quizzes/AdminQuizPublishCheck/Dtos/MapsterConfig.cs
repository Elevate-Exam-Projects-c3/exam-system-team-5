using exam_system.Domain.Entities.Quizzes;
using Mapster;

namespace exam_system.Features.Quizzes.AdminQuizPublishCheck.Dtos
{
    public class MapsterConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<Question, GetQuizQuestionsResponse>
                .NewConfig()
                .Map(dest => dest.QuestionId, src => src.Id);

            TypeAdapterConfig<QuestionOption, GetQuestionOptionsResponse>.NewConfig()
                .Map(dest => dest.OptionId, src => src.Id);
        }
    }
}
