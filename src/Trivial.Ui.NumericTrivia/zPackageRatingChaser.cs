using VsixRatingChaser.Dtos;
using VsixRatingChaser.Interfaces;
using static Trivial.Rating.ChaserGateway;

namespace Trivial.Ui.NumericTrivia
{
    public class PackageRatingChaser
    {
        public void Hunt(IRatingDetailsDto ratingDetailsDto)
        {
            var extensionDetailsDto = new ExtensionDetailsDto
            {
                AuthorName = Vsix.Author,
                ExtensionName = Vsix.Name,
                MarketPlaceUrl = "https://marketplace.visualstudio.com/items?itemName=GregTrevellick.NumericTrivia"
            };

            Probe(ratingDetailsDto, extensionDetailsDto);
        }
    }
}