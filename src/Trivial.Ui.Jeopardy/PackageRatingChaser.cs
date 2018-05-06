using VsixRatingChaser.Dtos;
using VsixRatingChaser.Interfaces;
using static Trivial.Rating.ChaserGateway;

namespace Trivial.Ui.Jeopardy
{
    public class PackageRatingChaser
    {
        public void Hunt(IRatingDetailsDto ratingDetailsDto)
        {
            var extensionDetailsDto = new ExtensionDetailsDto
            {
                AuthorName = Vsix.Author,
                ExtensionName = Vsix.Name,
                MarketPlaceUrl = "https://marketplace.visualstudio.com/items?itemName=GregTrevellick.Jeopardy"
            };

            Probe(ratingDetailsDto, extensionDetailsDto);
        }
    }
}